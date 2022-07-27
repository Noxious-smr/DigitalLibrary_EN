using UI_Layer.Commands;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using UI_Layer.Stores;

namespace UI_Layer.ViewModels
{
    public class DeleteShelfViewModel : BaseViewModel
    {
        private readonly IRepositoryService<Shelf> shelfRepositoryService;
        private readonly IEnumerable<Book> books;
        private readonly UpdateShelfViewModel updateShelf;
        private readonly Messanger messanger;

        public DeleteShelfViewModel(IRepositoryService<Shelf> shelfRepositoryService, IEnumerable<Book> books, UpdateShelfViewModel updateShelf, Messanger messanger)
        {
            this.shelfRepositoryService = shelfRepositoryService;
            this.books = books;
            this.updateShelf = updateShelf;
            this.messanger = messanger;
            messanger.SelectedShelfChanged += UpdateShelf_SelectedShelfChanged;
            messanger.SelectedCabinetHasNoShelves += UpdateShelf_SelectedCabinetHasNoShelves;
        }

        private void UpdateShelf_SelectedCabinetHasNoShelves()
        {
            SelectedShelf = null;
            DeleteValidator();
        }
        //private void UpdateShelf_SelectedCabinetHasNoShelves(ShelfViewModel obj)
        //{
        //    SelectedShelf = null;
        //    DeleteValidator();
        //}

        private void UpdateShelf_SelectedShelfChanged()
        {
            SelectedShelf = updateShelf.SelectedShelf;
            //if (obj is not null)
            //{
            //    SelectedShelf = obj;
            //}
        }
        //private void UpdateShelf_SelectedShelfChanged(ShelfViewModel obj)
        //{
        //    SelectedShelf = updateShelf.SelectedShelf;
        //    //if (obj is not null)
        //    //{
        //    //    SelectedShelf = obj;
        //    //}
        //}

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool Error { get => !string.IsNullOrEmpty(ErrorMessage); }

        #region Commands
        public ICommand DeleteShelfCommand { get => new UpdateRelayCommand(DeleteShelf, () => !Error && SelectedShelf is not null); }
        #endregion
        private void DeleteShelf()
        {
            shelfRepositoryService.DeleteItem(SelectedShelf.ShelfID);
            updateShelf.SelectedCabinet.Shelves.Remove(SelectedShelf);
            if (updateShelf.SelectedCabinet.Shelves.Count > 0)
            {
                updateShelf.SelectedShelf = updateShelf.SelectedCabinet.Shelves[0]; 
            }
            Message = "Shelf Deleted successfully!";
            OnPropertyChanged(nameof(ShowMessage));
        }

        #region Properties
        private ShelfViewModel selectedShelf;
        public ShelfViewModel SelectedShelf
        {
            get
            {
                return selectedShelf;
            }
            set
            {
                selectedShelf = value;
                DeleteValidator();
            }
        }
        #endregion
        #region Validator
        private void DeleteValidator()
        {
            ErrorMessage = "";
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(Error));
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            if (SelectedShelf is not null)
            {
                if (books.Any(B => B.ShelfID == SelectedShelf.ShelfID))
                {
                    ErrorMessage = $"Cannot delete Shelf {SelectedShelf.ShelfNr} because it has books in it!";
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(Error));
                } 
            }
            else
            {
                ErrorMessage = $"This Cabinet has no Shelves in it!";
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(Error));
            }
        } 
        #endregion

    }
}
