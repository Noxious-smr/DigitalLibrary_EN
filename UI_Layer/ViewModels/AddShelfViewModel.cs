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

namespace UI_Layer.ViewModels
{
    public class AddShelfViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly IRepositoryService<Shelf> repositoryService = new DBShelfService();
        private readonly ErrorNotifier errorNotifier;        
        public bool HasErrors => errorNotifier.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public AddShelfViewModel(ErrorNotifier _errorNotifier)
        {
            errorNotifier = _errorNotifier;
            errorNotifier.ErrorsChanged += ErrorNotifier_ErrorsChanged;
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

        public ICommand AddShelfCommand { get => new AddRelayCommand(ShelfAdd, () => CanAdd); }

        private void ShelfAdd(string parameter)
        {
            InsertShelfIntoDB();
        }

        private CabinetViewModel _cabinetViewModel;
        public CabinetViewModel cabinetViewModel 
        { 
            get 
            {
                return _cabinetViewModel;

            } 
            set 
            {                
                _cabinetViewModel = value;                
                ShelfValidator(Shelf_Nr);
            } 
        }
        public int Cabinet_ID
        {
            get
            {
                if (cabinetViewModel is null)
                {
                    return 0;
                }
                else
                {
                    return cabinetViewModel.CabinetID;
                }
            }
        }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
       
        private string shelf_Nr;
        public string Shelf_Nr
        {
            get { return shelf_Nr; }
            set
            {
                ShelfValidator(value.ToUpper());

            }
        }
        private void ShelfValidator(string value)
        {
            OnPropertyChanged(nameof(cabinetViewModel));
            shelf_Nr = value;
            OnPropertyChanged(nameof(Shelf_Nr));
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            errorNotifier.ClearError(nameof(cabinetViewModel));
            errorNotifier.ClearError(nameof(Shelf_Nr));
            ErrorNotifier_ErrorsChanged(null, new DataErrorsChangedEventArgs(nameof(Shelf_Nr)));
            ErrorNotifier_ErrorsChanged(null, new DataErrorsChangedEventArgs(nameof(cabinetViewModel)));

            if (Cabinet_ID == 0)
            {
                errorNotifier.AddError(nameof(Shelf_Nr), "Please select a Cabinet!! Thank you!");
                OnPropertyChanged(nameof(CanAdd));
            }
            if (cabinetViewModel != null)
            {
                if (cabinetViewModel.Shelves.Any(S => S.ShelfNr == value))
                {
                    errorNotifier.AddError(nameof(Shelf_Nr), "Shelf already exists in this Cabinet!! Thank you!");
                    errorNotifier.ClearError(nameof(cabinetViewModel));
                    OnPropertyChanged(nameof(CanAdd));
                }

                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length <= 0)
                {
                    errorNotifier.AddError(nameof(Shelf_Nr), "Shelf number must not be empty, Thank you!");
                    OnPropertyChanged(nameof(CanAdd));
                }
            }
        }
        public void InsertShelfIntoDB()
        {
            repositoryService.InsertItem(new Shelf(0, Cabinet_ID, Shelf_Nr));
            cabinetViewModel.Shelves.Add(new ShelfViewModel(repositoryService.GetItemByNameAndParentID(Shelf_Nr, Cabinet_ID)));
            var CabinetName = cabinetViewModel.CabinetName;
            var shelf_nr = Shelf_Nr;
            cabinetViewModel = null;
            Shelf_Nr = "";
            errorNotifier.ClearError(nameof(cabinetViewModel));
            errorNotifier.ClearError(nameof(Shelf_Nr));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(cabinetViewModel)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Shelf_Nr)));
            Message = $"Shelf {shelf_nr} in Cabinet {CabinetName} Added successfully!!";
            OnPropertyChanged(nameof(ShowMessage));

        }

        public void UpdateShelfInDB(int ID)
        {
            repositoryService.UpdateItem(new Shelf(ID, Cabinet_ID, Shelf_Nr));
        }
        public void DeleteShelfFromDB(int ID)
        {
            repositoryService.DeleteItem(ID);
        }
    }
}
