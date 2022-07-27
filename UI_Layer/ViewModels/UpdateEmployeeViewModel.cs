using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI_Layer.ViewModels
{
    public class UpdateEmployeeViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly IRepositoryService<Employee> repositoryService;
        private readonly ErrorNotifier errorNotifier;
        public ObservableCollection<EmployeeViewModel> Employees { get; set; } = new ObservableCollection<EmployeeViewModel>();
        public UpdateEmployeeViewModel(ErrorNotifier errorNotifier, IRepositoryService<Employee> repositoryService, IEnumerable<Employee> employees)
        {            
            this.errorNotifier = errorNotifier;
            this.repositoryService = repositoryService;
            //Parallel.ForEach(employees, employee => Employees.Add(new EmployeeViewModel(employee)));
            foreach (var item in employees)
            {
                Employees.Add(new EmployeeViewModel(item));
            }
            SelectedEmployee = Employees.First();
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void ErrorNotifier_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanUpdate));
        }
        public bool HasErrors => errorNotifier.HasErrors;
        public bool CanUpdate => !HasErrors;
        public bool CanUpdateEmployee { get; set; } = false;

        public IEnumerable GetErrors(string propertyName)
        {
            return errorNotifier.GetErrors(propertyName);
        }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }

        #region Commands
        public ICommand UpdateEmployeeCommand { get => new UpdateRelayCommand(UpdateInDB, () => CanUpdate && CanUpdateEmployee); }
        #endregion
        private void UpdateInDB()
        {
            try
            {
                repositoryService.UpdateItem(new Employee(SelectedEmployee.EmployeeID, SelectedEmployee.UniqueID, FirstName, LastName, Birthdate));
                SelectedEmployee.FirstName = FirstName;
                SelectedEmployee.LastName = LastName;
                SelectedEmployee.Birthdate = Birthdate.ToString();
                Message = "Employee updated successfully!";
                OnPropertyChanged(nameof(ShowMessage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #region Properties
        private EmployeeViewModel selectedEmployee; 
        public EmployeeViewModel SelectedEmployee
        {
            get 
            { 
                return selectedEmployee; 
            }
            set 
            { 
                selectedEmployee = value; 
                OnPropertyChanged(nameof(SelectedEmployee));
                firstName = SelectedEmployee.FirstName;
                lastName = SelectedEmployee.LastName;
                birthdate = DateTime.Parse(SelectedEmployee.Birthdate);
                Day = birthdate.Day;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(Birthdate));
                OnPropertyChanged(nameof(Day));
            }
        }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
                EmployeeValidator();
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
                EmployeeValidator();
            }
        }

        private DateTime birthdate;
        public DateTime Birthdate
        {
            get
            {
                return birthdate;
            }
            set
            {
                birthdate = value;
                Day = value.Day;
                OnPropertyChanged(nameof(Birthdate));
                OnPropertyChanged(nameof(Day));
                EmployeeValidator();
            }
        }
        public int Day { get; set; }
        #endregion
        #region Validators

        private void ClearAllErrors()
        {
            errorNotifier.ClearError(nameof(FirstName));
            errorNotifier.ClearError(nameof(LastName));
            errorNotifier.ClearError(nameof(Birthdate));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(FirstName)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(LastName)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Birthdate)));
        }

        private void EmployeeValidator()
        {
            CanUpdateEmployee = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            ClearAllErrors();
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName))
            {
                errorNotifier.AddError(nameof(Birthdate), "First name and last name must not be empty, Thank you!");
                ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Birthdate)));
                OnPropertyChanged(nameof(CanUpdate));
                CanUpdateEmployee = false;
            }
            if (!(string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName)) && !(string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName)))
            {
                if (Employees.Any(E => E.FirstName.ToUpper() == FirstName.ToUpper() && E.LastName.ToUpper() == LastName.ToUpper() && DateTime.Parse(E.Birthdate) == Birthdate.Date))
                {
                    errorNotifier.AddError(nameof(Birthdate), "Employee exists, Thank you!");
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Birthdate)));
                    OnPropertyChanged(nameof(CanUpdate));
                    CanUpdateEmployee = false;
                }
                if ((DateTime.Now.Date.Year - Birthdate.Date.Year) < 18)
                {
                    errorNotifier.AddError(nameof(Birthdate), "Employee age must be at least 18, Thank you!");
                    OnPropertyChanged(nameof(CanUpdate));
                    CanUpdateEmployee = false;
                }
            }
        }
        #endregion
    }
}
