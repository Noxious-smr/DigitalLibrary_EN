using UI_Layer.Commands;
using DataAccess.DataAccess;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using UI_Layer.Stores;
using System.Linq;
using System.Windows.Input;

namespace UI_Layer.ViewModels
{
    public class DeleteCopyViewModel : BaseViewModel
    {
        private readonly IRepositoryService<Copy> copyRepositoryService;
        private readonly UpdateCopyViewModel updateCopy;
        private readonly Messanger messanger;

        public DeleteCopyViewModel(IRepositoryService<Copy> copyRepositoryService, UpdateCopyViewModel updateCopy, Messanger messanger)
        {
            this.copyRepositoryService = copyRepositoryService;
            this.updateCopy = updateCopy;
            this.messanger = messanger;
            messanger.SelectedCopyChanged += UpdateCopy_SelectedCopyChanged;
            
            DeleteValidator();
        }

        private void UpdateCopy_SelectedCopyChanged()
        {
            DeleteValidator();
        }

        #region Messages
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool Error { get => !string.IsNullOrEmpty(ErrorMessage); } 
        #endregion

        #region Commands
        public ICommand DeleteCopyCommand { get => new UpdateRelayCommand(DeleteCopy, () => !Error && updateCopy.SelectedCopy is not null); }
        #endregion
        private void DeleteCopy()
        {
            copyRepositoryService.DeleteItem(updateCopy.SelectedCopy.ID);
            updateCopy.SelectedBook.AllCopies.Remove(updateCopy.SelectedCopy);
            if (updateCopy.SelectedBook.AllCopies.Count > 0)
            {
                updateCopy.SelectedCopy = updateCopy.SelectedBook.AllCopies.First();
            }
                messanger.WhenCopyDeleted();
            Message = "Copy Deleted successfully!";
            OnPropertyChanged(nameof(ShowMessage));
        }
        
        #region Validaters
        private void DeleteValidator()
        {
            ErrorMessage = "";
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(Error));
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            if (updateCopy.SelectedCopy is not null)
            {
                if (updateCopy.SelectedCopy.IsRented == true)
                {
                    ErrorMessage = $"Cannot delete Copy because it is Borrowed!";
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(Error));
                }
            }
        } 
        #endregion
    }
}
