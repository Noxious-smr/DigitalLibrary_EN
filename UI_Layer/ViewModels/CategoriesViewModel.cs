using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using UI_Layer.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using UI_Layer.Stores;
using System.Collections.Generic;
using System.Windows.Data;
using System.Diagnostics;

namespace UI_Layer.ViewModels
{
    internal class CategoriesViewModel : BaseViewModel, IDisposable
    {
        //private NavigationStore _navigationStore;
        private BooksViewModel _booksViewModel;
        private readonly IRepositoryService<Category> repositoryService;
        private readonly Messanger messanger;
        public ICommand NavigateHomeCommand { get; }
        public CategoriesViewModel(NavigationStore _navigationStore, IRepositoryService<Category> _repositoryService, BooksViewModel booksViewModel, Messanger messanger)
        {
            //this._navigationStore = navigationStore;
            //_navigationStore.AddViewModel(nameof(CategoriesViewModel), this);
            _booksViewModel = booksViewModel;
            repositoryService = _repositoryService;
            this.messanger = messanger;
            MakeBorrow = new(new DBEmployeeService(), new DBBorrowService(), new DBCopyService(), 
                            messanger, new ErrorNotifier());
            Categories = new ObservableCollection<CategoryViewModel>();
            BooksByCategoryID = new ObservableCollection<BookViewModel>();
            NavigateHomeCommand = new NavigationCommand<HomeViewModel>(new NavigationService<HomeViewModel>(_navigationStore, () => new HomeViewModel(_navigationStore)));
            GetCategoriesViewModel();
            GetBooksByCategory(null);
            //AllBooksSearchable.Filter = Filter;
            //MakeBorrow = new MakeBorrowViewModel();
        }

        #region Borrow Part
        private BookViewModel selectedBook;

        public BookViewModel SelectedBook
        {
            get { return selectedBook; }
            set { selectedBook = value; messanger.WhenCategoriesBookSelected(value); }
        }

        public MakeBorrowViewModel MakeBorrow { get; set; }
        
        #endregion

        private bool Filter(object obj)
        {
            if (TitleFilter != string.Empty && ISBN_10Filter == string.Empty)
            {
                return FilterByTitle(obj);
            }
            if (TitleFilter != string.Empty && ISBN_10Filter != string.Empty)
            {
                return FilterByTitle(obj) && FilterByISBN_10(obj);
            }
            if (ISBN_10Filter != string.Empty && TitleFilter == string.Empty)
            {
                return FilterByISBN_10(obj);
            }
            return true;
        }
        private bool FilterByTitle(object obj)
        {
            if (obj is BookViewModel bookVM)
            {
                return bookVM.Title.Contains(TitleFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            else
                return false;
        }
        private bool FilterByISBN_10(object obj)
        {
            if (obj is BookViewModel bookVM)
            {
                return bookVM.ISBN_10.Replace("-", string.Empty).Contains(ISBN_10Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            else
                return false;
        }
        private bool FilterByISBN_13(object obj)
        {
            if (obj is BookViewModel bookVM)
            {
                return bookVM.ISBN_13.Replace("-", string.Empty).Contains(ISBN_13Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            else
                return false;
        }
        private bool FilterByAuthor(object obj)
        {
            if (obj is BookViewModel bookVM)
            {
                return bookVM.Author.Contains(AuthorFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            else
                return false;
        }


        private string titleFilter = string.Empty;
        public string TitleFilter
        {
            get { return titleFilter; }
            set { titleFilter = value; AllBooksSearchable.Filter = FilterByTitle; AllBooksSearchable.Refresh(); }
        }
        private string iSBN_10Filter = string.Empty;
        public string ISBN_10Filter
        {
            get { return iSBN_10Filter; }
            set { iSBN_10Filter = value; AllBooksSearchable.Filter = FilterByISBN_10; AllBooksSearchable.Refresh(); }
        }

        private string iSBN_13Filter = string.Empty;
        public string ISBN_13Filter
        {
            get { return iSBN_13Filter; }
            set { iSBN_13Filter = value; AllBooksSearchable.Filter = FilterByISBN_13; AllBooksSearchable.Refresh(); }
        }
        private string authorFilter = string.Empty;
        public string AuthorFilter
        {
            get { return authorFilter; }
            set { authorFilter = value; AllBooksSearchable.Filter = FilterByAuthor; AllBooksSearchable.Refresh(); }
        }

        public ObservableCollection<CategoryViewModel> Categories { get; set; }
        private void GetCategoriesViewModel()
        {
            foreach (var item in repositoryService.GetAllItems().Select(Cat => new CategoryViewModel(Cat)))
            {
                Categories.Add(item);
            }
        }
        public ObservableCollection<BookViewModel> BooksByCategoryID { get; set; } 
        public ICollectionView AllBooksSearchable { get => CollectionViewSource.GetDefaultView(BooksByCategoryID); }
        public ICommand GetBooksByCategoryCommand { get { return new ServiceCommand(GetBooksByCategory); } }

        private void GetBooksByCategory(object parameter)
        {
            IEnumerable<BookViewModel> books;
            BooksByCategoryID.Clear();
            if (parameter is null)
            {
                books = _booksViewModel.AllBooks;
                CategoryName = "All Books";
                OnPropertyChanged(nameof(CategoryName));
            }
            else
            {
                int catID = (int)parameter;
                books = _booksViewModel.AllBooks.Where(b => b.CategoryID == catID);
                CategoryName = Categories.Where(CVM => CVM.CategoryID == (int)parameter).Select(c => c.CategoryName).FirstOrDefault();
                OnPropertyChanged(nameof(CategoryName));
            }
            foreach (var item in books)
            {
                BooksByCategoryID.Add(item);
            }            
        }
        public string CategoryName { get; set; }
        ~CategoriesViewModel()
        {
            Dispose(false);
            Debug.WriteLine("I'm Gone CategoriesViewModel");
        }
        private bool _disposed = false;
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    BooksByCategoryID.Clear();
                    Categories.Clear();
                    _booksViewModel = null;
                    Debug.WriteLine("I'm Gone CategoriesViewModel");
                }
                _disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
