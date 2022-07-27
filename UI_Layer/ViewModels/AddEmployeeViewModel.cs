using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using UI_Layer.Services;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;

namespace UI_Layer.ViewModels
{
    public class AddEmployeeViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly IRepositoryService<Employee> repositoryService = new DBEmployeeService();
        private readonly ErrorNotifier errorNotifier;

        public AddEmployeeViewModel(ErrorNotifier errorNotifier)
        {
            this.errorNotifier = errorNotifier;
            this.errorNotifier.ErrorsChanged += ErrorNotifier_ErrorsChanged;
            foreach (var item in repositoryService.GetAllItems())
            {
                Employees.Add(new EmployeeViewModel(item));
            }
        }

        private void ErrorNotifier_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanAdd));
        }
        public bool HasErrors => errorNotifier.HasErrors;
        public bool CanAdd => !HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return errorNotifier.GetErrors(propertyName);
        }
        public ICommand AddEmployeeCommand { get => new AddRelayCommand(EmployeeAdd, () => CanAdd); }

        private void EmployeeAdd(string parameter)
        {
            Task.Run(() => InsertEmployeeIntoDB());
        }

        public ObservableCollection<EmployeeViewModel> Employees { get; set; } = new ObservableCollection<EmployeeViewModel>();
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set 
            { 
                firstName = value;
                EmployeeValidator();
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set 
            { 
                lastName = value;
                EmployeeValidator();
            }
        }

        private DateTime birthdate = DateTime.Now;
        public DateTime Birthdate
        {
            get { return birthdate; }
            set 
            { 
                birthdate = value;
                EmployeeValidator();
                OnPropertyChanged(nameof(Today)); 
            }
        }
        public int Today => Birthdate.Day;

        public void EmployeeValidator()
        {
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            errorNotifier.ClearError(nameof(FirstName));
            errorNotifier.ClearError(nameof(LastName));
            errorNotifier.ClearError(nameof(Birthdate));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Birthdate));
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName))
            {
                errorNotifier.AddError(nameof(FirstName), "First name must not be empty, Thank you!");
                OnPropertyChanged(nameof(CanAdd));
            }
            if (string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName))
            {
                errorNotifier.AddError(nameof(LastName), "Last name must not be empty, Thank you!");
                OnPropertyChanged(nameof(CanAdd));
            }
            if (!(string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName)) && !(string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName)))
            {
                if (Employees.Any(E => E.FirstName.ToUpper() == FirstName.ToUpper() && E.LastName.ToUpper() == LastName.ToUpper()))
                {
                    errorNotifier.AddError(nameof(LastName), "Employee exists, Thank you!");
                    OnPropertyChanged(nameof(CanAdd));
                }
            }
            if ((DateTime.Now.Date.Year - Birthdate.Date.Year ) < 18)
            {
                errorNotifier.AddError(nameof(Birthdate), "Employee age must be at least 18, Thank you!");
                OnPropertyChanged(nameof(CanAdd));
            }
        }
        private void InsertEmployeeIntoDB()
        {
            var UniqueID = Guid.NewGuid().ToString();
            repositoryService.InsertItem(new Employee(0, UniqueID, FirstName, LastName, Birthdate));
            Employees.Add(new EmployeeViewModel(repositoryService.GetByUniqueID(UniqueID)));    
            var EmployeeName = FirstName + " " + LastName;
            FirstName = "";
            LastName = "";
            Birthdate = DateTime.Now;
            errorNotifier.ClearError(nameof(FirstName));
            errorNotifier.ClearError(nameof(LastName));
            errorNotifier.ClearError(nameof(Birthdate));
            //ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(FirstName)));
            //ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(LastName)));
            //ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Birthdate)));
            Message = $"Employee {EmployeeName} Added successfully!!";
            OnPropertyChanged(nameof(ShowMessage));
        }
    }
}
