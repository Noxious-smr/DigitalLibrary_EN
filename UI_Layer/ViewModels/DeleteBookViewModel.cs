using UI_Layer.Commands;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Linq;
using System.Windows.Input;
using UI_Layer.Stores;

namespace UI_Layer.ViewModels
{
    public class DeleteBookViewModel : BaseViewModel
    {
        private readonly Messanger messanger;
        private readonly IRepositoryService<Book> bookRepositoryService;
        private readonly UpdateBookViewModel updateBook;

        public DeleteBookViewModel(IRepositoryService<Book> bookRepositoryService, UpdateBookViewModel updateBook, Messanger messanger)
        {
            this.bookRepositoryService = bookRepositoryService;
            this.updateBook = updateBook;
            this.messanger = messanger;
            messanger.SelectedBookChanged += UpdateBook_SelectedBookChanged;
            messanger.CopyDeleted += Messanger_CopyDeleted;
            DeleteValidator();
        }

        private void Messanger_CopyDeleted()
        {
            DeleteValidator();
            if (updateBook.SelectedBook.AllCopies.Count == 0)
            {
                ErrorMessage = "";
                OnPropertyChanged(nameof(Error));
                Message = $"This Book {updateBook.SelectedBook.Title} can now be Deleted";
                OnPropertyChanged(nameof(ShowMessage));
            }
        }

        private void UpdateBook_SelectedBookChanged()
        {
            DeleteValidator();
        }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool Error { get => !string.IsNullOrEmpty(ErrorMessage); }

        #region Commands
        public ICommand DeleteBookCommand { get => new UpdateRelayCommand(DeleteBook, () => !Error && updateBook.SelectedBook is not null); }
        #endregion
        private void DeleteBook()
        {
            //int index = updateBook.Books.IndexOf(SelectedBook);
            //var TempBook = SelectedBook;
            //bookRepositoryService.DeleteItem(SelectedBook.BookID);
            //if (updateBook.Books.Count > 1)
            //{
            //    if (index > 0)
            //    {
            //        SelectedBook = updateBook.Books[index - 1];
            //        updateBook.SelectedBook = updateBook.Books[index - 1];
            //        messanger.WhenBookDeleted();
            //    }
            //    if (index == 0)
            //    {
            //        SelectedBook = updateBook.Books[index + 1];
            //        updateBook.SelectedBook = updateBook.Books[index + 1];
            //        messanger.WhenBookDeleted();
            //    }
            //}
            //updateBook.Books.Remove(TempBook);
            //if (updateBook.Books.Count == 0)
            //{
            //    updateBook.SelectedBook = null;
            //    SelectedBook = null;
            //}

            //bookRepositoryService.DeleteItem(updateBook.SelectedBook.BookID);
            var TempBook = updateBook.SelectedBook;
            //if (updateBook.Books.Count > 0)
            //{
            //    //updateBook.SelectedBook = updateBook.Books.First();
            //    messanger.WhenBookDeleted();
            //}
            //OnPropertyChanged(nameof(SelectedBook));
            try
            {
                bookRepositoryService.DeleteItem(updateBook.SelectedBook.BookID);
                updateBook.Books.Remove(updateBook.SelectedBook);
                messanger.WhenBookDeleted();
                Message = "Book Deleted successfully!";
                OnPropertyChanged(nameof(ShowMessage));
                if (updateBook.Books.Count == 0)
                {
                    ErrorMessage = "No more books";
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(Error));
                }
            }
            catch (Exception)
            {

                ErrorMessage = "Something went wrong !!";
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(Error));
            }
        }

        #region Properties
        //private BookViewModel selectedBook;
        //public BookViewModel SelectedBook
        //{
        //    get
        //    {
        //        return selectedBook;
        //    }
        //    set
        //    {
        //        selectedBook = value;
        //        DeleteValidator();
        //    }
        //}
        #endregion
        #region Validators
        private void DeleteValidator()
        {
            ErrorMessage = "";
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(Error));
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            if (updateBook.SelectedBook is not null)
            {
                if (updateBook.SelectedBook.AllCopies.Any(C => C.BookID == updateBook.SelectedBook.BookID))
                {
                    ErrorMessage = $"Cannot delete Book {updateBook.SelectedBook.Title} because it has copies!";
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(Error));
                }
            }
            else
            {
                ErrorMessage = "No Books !!";
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(Error));
            }
        }
        #endregion
    }
}
