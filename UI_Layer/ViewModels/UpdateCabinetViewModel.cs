using UI_Layer.Commands;
using DataAccess.DataAccess;
using DomainLayer.Abstractions;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using UI_Layer.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI_Layer.ViewModels
{
    public class UpdateCabinetViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        //public event Action<CabinetViewModel> SelectedCabinetChanged;
        private readonly IRepositoryService<Cabinet> repositoryService;
        private readonly ErrorNotifier errorNotifier;
        private readonly Messanger messanger;
        public ObservableCollection<CabinetViewModel> Cabinets { get; } = new ObservableCollection<CabinetViewModel>();
        public UpdateCabinetViewModel(ErrorNotifier errorNotifier, IRepositoryService<Cabinet> repositoryService, Messanger messanger)
        {
            this.repositoryService = repositoryService;
            this.errorNotifier = errorNotifier;
            this.messanger = messanger;
            foreach (var item in repositoryService.GetAllItems())
            {
                Cabinets.Add(new CabinetViewModel(item));
            }
        }
        public bool HasErrors => errorNotifier.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void ErrorNotifier_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanUpdate));
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return errorNotifier.GetErrors(propertyName);
        }
        public bool CanUpdate => !HasErrors;
        public ICommand UpdateCabinetCommand { get => new UpdateRelayCommand(UpdateCabinetInDB, () => CanUpdate && SelectedCabinet != null); }
        private void UpdateCabinetInDB()
        {
            try
            {
                repositoryService.UpdateItem(new Cabinet(SelectedCabinet.CabinetID, Name));
                Cabinets.Where(C => C.CabinetID == SelectedCabinet.CabinetID).First().CabinetName = Name;
                Message = $"Cabinet {Name} Updated Successfully!";
                OnPropertyChanged(nameof(ShowMessage));
            }
            catch (Exception e)
            {
                Message = $"Database Error: {e.Message}";
                OnPropertyChanged(nameof(ShowMessage));
            }
            
        }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);
       
        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }

        private CabinetViewModel selectedCabinet;
        public CabinetViewModel SelectedCabinet 
        {
            get
            {
                return selectedCabinet;
            }
            set
            {
                selectedCabinet = value;
                OnPropertyChanged(nameof(SelectedCabinet));
                messanger.WhenSelectedCabinetChanged();
                //SelectedCabinetChanged?.Invoke(value);
                //CabinetValidator(Name);
            }
        }
     
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
            
            if (SelectedCabinet is null)
            {
                errorNotifier.AddError(nameof(Name), "Please choose a cabinet!! Thank you!");
                ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Name)));
                OnPropertyChanged(nameof(CanUpdate));
            }
            else
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length <= 1)
                {
                    errorNotifier.AddError(nameof(Name), "Cabinet Name must be more than one character long, Thank you!");
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Name)));
                    OnPropertyChanged(nameof(CanUpdate));
                }

                if (Cabinets.Any(C => C.CabinetName == value && C.CabinetID != SelectedCabinet.CabinetID))
                {
                    errorNotifier.AddError(nameof(Name), "Cabinet already exists!! Thank you!");
                    errorNotifier.ClearError(nameof(SelectedCabinet));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Name)));
                    OnPropertyChanged(nameof(CanUpdate));
                }
            }
        }        
    }
}
