using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
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
    public class UpdateShelfViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        //public event Action<ShelfViewModel> SelectedShelfChanged;
        //public event Action<ShelfViewModel> SelectedCabinetHasNoShelves;
        private readonly IRepositoryService<Shelf> repositoryService;
        private readonly ErrorNotifier errorNotifier;
        private readonly Messanger messanger;

        public UpdateShelfViewModel(ErrorNotifier errorNotifier, IRepositoryService<Shelf> repositoryService, Messanger messanger)
        {
            this.repositoryService = repositoryService;
            this.errorNotifier = errorNotifier;            
            this.messanger = messanger;
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
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }

        private CabinetViewModel selectedCabinet = null;
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
                if (value is not null)
                {
                    if (value.Shelves.Count == 0)
                    {
                        //SelectedCabinetHasNoShelves?.Invoke(null);
                        messanger.WhenSelectedCabinetHasNoShelves();
                    } 
                }
                if (!string.IsNullOrEmpty(Shelf_Nr))
                {
                    ShelfValidator(Shelf_Nr);
                }
            }
        }

        private ShelfViewModel _selectedShelf = null;
        public ShelfViewModel SelectedShelf
        {
            get
            {
                return _selectedShelf;
            }
            set
            {
                _selectedShelf = value;
                OnPropertyChanged(nameof(SelectedShelf));
                //SelectedShelfChanged?.Invoke(SelectedShelf);
                messanger.WhenSelectedShelfChanged();
                if (!string.IsNullOrEmpty(Shelf_Nr))
                {
                    ShelfValidator(Shelf_Nr);
                }
            }
        }

        private string _shelf_Nr;
        public string Shelf_Nr
        {
            get
            {
                return _shelf_Nr;
            }
            set
            {
                ShelfValidator(value.ToUpper());
            }
        }

        private void ShelfValidator(string value)
        {
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            errorNotifier.ClearError(nameof(Shelf_Nr));
            _shelf_Nr = value;
            OnPropertyChanged(nameof(Shelf_Nr));

            if (SelectedCabinet is null || SelectedShelf is null)
            {
                errorNotifier.AddError(nameof(Shelf_Nr), "Please select a Cabinet and Shelf, Thank you!");
                ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Shelf_Nr)));
                OnPropertyChanged(nameof(CanUpdate));
            }
            else
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length <= 1)
                {
                    errorNotifier.AddError(nameof(Shelf_Nr), "Shelf must not be empty less than one character long, Thank you!");
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Shelf_Nr)));
                    OnPropertyChanged(nameof(CanUpdate));
                }
                if (SelectedCabinet.Shelves.Any(S => S.ShelfNr == value && S.ShelfID != SelectedShelf.ShelfID && S.CabinetID == SelectedCabinet.CabinetID))
                {
                    errorNotifier.AddError(nameof(Shelf_Nr), "Shelf already exists!! Thank you!");
                    errorNotifier.ClearError(nameof(SelectedCabinet));
                    errorNotifier.ClearError(nameof(SelectedShelf));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Shelf_Nr)));
                    OnPropertyChanged(nameof(CanUpdate));
                }
            }
        }

        

        public ICommand UpdateShelfCommand { get => new UpdateRelayCommand(UpdateShelfInDB, () => CanUpdate && SelectedCabinet != null && SelectedShelf != null); }

        private void UpdateShelfInDB()
        {
            try
            {
                repositoryService.UpdateItem(new Shelf(SelectedShelf.ShelfID, SelectedShelf.CabinetID, Shelf_Nr));
                SelectedShelf.ShelfNr = Shelf_Nr;
                Message = $"Shelf {Shelf_Nr} Updated Successfully!";
                OnPropertyChanged(nameof(ShowMessage));
            }
            catch (Exception e)
            {
                Message = $"Database Error: {e.Message}";
                OnPropertyChanged(nameof(ShowMessage));
            }
        }
    }
}
