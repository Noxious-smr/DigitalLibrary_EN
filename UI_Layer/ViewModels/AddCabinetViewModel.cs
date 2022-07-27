using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace UI_Layer.ViewModels
{
    public class AddCabinetViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly IRepositoryService<Cabinet> repositoryService;
        private readonly ErrorNotifier errorNotifier;        
        public bool HasErrors => errorNotifier.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public AddCabinetViewModel(ErrorNotifier _errorNotifier)
        {
            repositoryService = new DBCabinetService();
            errorNotifier = _errorNotifier;
            errorNotifier.ErrorsChanged += ErrorNotifier_ErrorsChanged;
            Cabinets = new ObservableCollection<CabinetViewModel>();
            foreach (var item in repositoryService.GetAllItems())
            {
                Cabinets.Add(new CabinetViewModel(item));
            }
        }        
        private void ErrorNotifier_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanAdd));
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return errorNotifier.GetErrors(propertyName);
        }
        public bool CanAdd => !HasErrors;
        public ICommand AddCabinetCommand { get => new AddRelayCommand(CabinetAdd, () => CanAdd); }
        private void CabinetAdd(string parameter)
        {
            InsertCabinetIntoDB();
        }
        public ObservableCollection<CabinetViewModel> Cabinets { get; set; }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                CabinetValidator(value.ToUpper());
            }
        }       
        private void CabinetValidator(string value)
        {
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            errorNotifier.ClearError(nameof(Name));
            name = value;
            OnPropertyChanged(nameof(Name));
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length <= 1)
            {
                errorNotifier.AddError(nameof(Name), "Cabinet Name must be more than one character long, Thank you!");
                OnPropertyChanged(nameof(CanAdd));
            }
            if (Cabinets.Where(C => C.CabinetName == value).Any())
            {
                errorNotifier.AddError(nameof(Name), "Cabinet already exists!! Thank you!");
                OnPropertyChanged(nameof(CanAdd));
            }

        }
        public void InsertCabinetIntoDB()
        {
            repositoryService.InsertItem(new Cabinet(0, Name));
            try
            {
                Cabinets.Add(new CabinetViewModel(repositoryService.GetByName(Name)));
                var cabinetName = Name;
                Name = "";
                errorNotifier.ClearError(nameof(Name));
                ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Name)));
                Message = $"Cabinet {cabinetName} Added Successfully!";
                OnPropertyChanged(nameof(ShowMessage));
            }
            catch (Exception e)
            {

                errorNotifier.AddError(Name, e.Message);
            }
        }

        public void UpdateCabinetInDB(int ID)
        {
            repositoryService.UpdateItem(new Cabinet(ID, Name));
        }
        public void DeleteCabinetFromDB(int ID)
        {
            repositoryService.DeleteItem(ID);
        }

        
    }
}
