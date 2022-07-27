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
using System.Diagnostics;

namespace UI_Layer.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {
        private readonly IRepositoryService<Book> booksService;
        private readonly IRepositoryService<Shelf> shelfService;
        private readonly IRepositoryService<Cabinet> cabinetService;
        private readonly IRepositoryService<Copy> copyService;

        public BooksViewModel(IRepositoryService<Book> booksService, IRepositoryService<Shelf> repositoryService, IRepositoryService<Cabinet> cabinetService, IRepositoryService<Copy> _copyRepository)
        {
            this.booksService = booksService;
            this.shelfService = repositoryService;
            this.cabinetService = cabinetService;
            copyService = _copyRepository;
            GetBooks();
            this.cabinetService = cabinetService;            
        }
        private ObservableCollection<BookViewModel> allBooks = new ObservableCollection<BookViewModel>();
        public ObservableCollection<BookViewModel> AllBooks { get => allBooks; }        
        private void GetBooks()
        {
            try
            {
                var books = booksService.GetAllItems().Select(book => new BookViewModel(book, shelfService, cabinetService, copyService));
                foreach (var item in books)
                {
                    allBooks.Add(item);
                }  
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception handled: {e.Message}");
            }
        }
    }
}
