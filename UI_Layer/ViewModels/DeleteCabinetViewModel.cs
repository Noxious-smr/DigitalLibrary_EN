using UI_Layer.Commands;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using System.Linq;
using System.Windows.Input;
using UI_Layer.Stores;

namespace UI_Layer.ViewModels
{
    public class DeleteCabinetViewModel : BaseViewModel
    {
        IRepositoryService<Cabinet> cabinetRepositoryService;
        private readonly UpdateCabinetViewModel updateCabinet;
        private readonly Messanger messanger;

        public DeleteCabinetViewModel(UpdateCabinetViewModel updateCabinet, IRepositoryService<Cabinet> cabinetRepositoryService, Messanger messanger)
        {
            this.updateCabinet = updateCabinet;
            this.cabinetRepositoryService = cabinetRepositoryService;
            this.messanger = messanger;
            messanger.SelectedCabinetChanged += UpdateCabinet_SelectedCabinetChanged;

            messanger.SelectedShelfChanged += UpdateShelf_SelectedShelfChanged;
            
        }

        private void UpdateShelf_SelectedShelfChanged()
        {
            if (SelectedCabinet is not null && SelectedCabinet.Shelves.Count == 0)
            {
                ErrorMessage = "";
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(Error));
                Message = "This Cabinet can be deleted (^_^)";
                OnPropertyChanged(nameof(ShowMessage)); 
            }
        }

        private void UpdateCabinet_SelectedCabinetChanged()
        {
            SelectedCabinet = updateCabinet.SelectedCabinet;
        }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool Error { get => !string.IsNullOrEmpty(ErrorMessage); }


        #region Properties
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
                DeleteValidator();
            }
        }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
        #endregion

        #region Commands
        public ICommand DeleteCabinetCommand { get => new UpdateRelayCommand(DeleteCabinet, () => !Error && SelectedCabinet is not null); }
        #endregion
        private void DeleteCabinet()
        {
            cabinetRepositoryService.DeleteItem(SelectedCabinet.CabinetID);
            updateCabinet.Cabinets.Remove(SelectedCabinet);
            updateCabinet.SelectedCabinet = updateCabinet.Cabinets.Count>0 ? updateCabinet.Cabinets[0] : null;
            Message = "Cabinet Deleted successfully!";
            OnPropertyChanged(nameof(ShowMessage));
        }

        private void DeleteValidator()
        {
            ErrorMessage = "";
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(Error));
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            if (SelectedCabinet is not null)
            {
                if (SelectedCabinet.Shelves.Any(S => S.CabinetID == SelectedCabinet.CabinetID))
                {
                    ErrorMessage = $"Cannot delete Cabinet {SelectedCabinet.CabinetName} because it has shelves in it!";
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(Error));
                } 
                else
                {
                    Message = "This Cabinet can be deleted (^_^)";
                    OnPropertyChanged(nameof(ShowMessage));
                }
            }
        }
    }
}
