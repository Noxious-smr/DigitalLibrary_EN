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
using System.Collections.Generic;

namespace UI_Layer.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        private readonly Book _book;
        private readonly IRepositoryService<Shelf> _shelfRespository;
        private readonly IRepositoryService<Cabinet> _cabinetRespository;
        private readonly IRepositoryService<Copy> _copyRespository;
        private readonly IFileService _descriptionService;
        private readonly IFileService _imageService;
        private readonly List<CabinetViewModel> cabinets;
        private readonly List<ShelfViewModel> shelves;

        
        public int BookID
        {
            get { return _book.ID; }
            set { _book.ID = value; OnPropertyChanged(nameof(BookID)); }
        }
        public string ISBN_10
        {
            get
            {
                return _book.ISBN_10.Insert(9,"-");
            }
            set
            {
                _book.ISBN_10 = value;
                OnPropertyChanged(nameof(ISBN_10));
            }
        }
        public string ISBN_13
        {
            get
            {
                string newISBN_13 = _book.ISBN_13.Insert(3,"-");
                newISBN_13 = newISBN_13.Insert(5,"-");
                newISBN_13 = newISBN_13.Insert(8,"-");
                newISBN_13 = newISBN_13.Insert(15,"-");
                return newISBN_13;
            }
            set
            {
                _book.ISBN_13 = value;
                OnPropertyChanged(nameof(ISBN_13));
            }
        }
        public string Title
        {
            get
            {
                return _book.Title;
            }
            set
            {
                _book.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string ImagePath
        {
            get { return _imageService.FileRead(_book.ImagePath); }
            set { _book.ImagePath = value; OnPropertyChanged(nameof(ImagePath)); }
        }
        public int PageCount
        {
            get
            {
                return _book.PageCount;
            }
            set
            {
                _book.PageCount = value;
                OnPropertyChanged(nameof(PageCount));
            }
        }
        public string Publisher
        {
            get
            {
                return _book.Publisher;
            }
            set
            {
                _book.Publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }
        public string Published
        {
            get
            {
                return _book.Published.Date.ToString("d");
            }
            set
            {
                _book.Published = DateTime.Parse(value);
                OnPropertyChanged(nameof(Published));
            }
        }
        public string Author
        {
            get
            {
                return _book.Author;
            }
            set
            {
                _book.Author = value;
                OnPropertyChanged(nameof(Author));
            }
        }
        public string Language
        {
            get
            {
                return _book.Language;
            }
            set
            {
                _book.Language = value;
                OnPropertyChanged(nameof(Language));
            }
        }
        public string Description
        {
            get
            {
                return _descriptionService.FileRead(_book.Description);
            }
            set
            {
                _book.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public int CategoryID
        {
            get
            {
                return _book.CategoryID;
            }
            set
            {
                _book.CategoryID = value;
                OnPropertyChanged(nameof(CategoryID));
            }
        }
        public int ShelfID
        {
            get
            {
                return _book.ShelfID;
            }
            set
            {
                _book.ShelfID = value;
                OnPropertyChanged(nameof(ShelfID));
            }
        }
        public int CabinetID
        {
            get
            {
                if (shelves.Count != 0)
                {
                    return shelves.Where(S => S.ShelfID == ShelfID).First().CabinetID; 
                }
                else
                {
                    return 0;
                }
            }
        }
        public string Cabinet_Name
        {
            get
            {
                if (cabinets.Count != 0)
                    return cabinets.Where(C => C.CabinetID == CabinetID).First().CabinetName;
                else
                    return string.Empty;
            }
        }
        public string Shelf_Nr
        {
            get
            {
                if (shelves.Count != 0)
                {
                    return shelves.Where(S => S.ShelfID == ShelfID).First().ShelfNr; 
                }
                else
                    return string.Empty;
            }
        }
        public int Quantity
        {
            get => AllCopies.Count;
        }
        public int AvailableCopies
        {
            get => Quantity - RentedCount; 
           
        }
                
        public int RentedCount
        {
            get => RentedCopies.Count;
        }


        public ObservableCollection<CopyViewModel> RentedCopies { get; }
        public ObservableCollection<CopyViewModel> NotRentedCopies { get; }
        public ObservableCollection<CopyViewModel> AllCopies { get; }
        public BookViewModel(Book book, IRepositoryService<Shelf> repositoryService, IRepositoryService<Cabinet> cabinetRespository, IRepositoryService<Copy> copyRepository)
        {
            _shelfRespository = repositoryService;
            _cabinetRespository = cabinetRespository;
            _descriptionService = new DescriptionService();
            _imageService = new ImageService();
            _copyRespository = copyRepository;            
            _cabinetRespository = cabinetRespository;
            _book = book;
            RentedCopies = new ObservableCollection<CopyViewModel>();
            NotRentedCopies = new ObservableCollection<CopyViewModel>();
            AllCopies = new ObservableCollection<CopyViewModel>();
            cabinets = new List<CabinetViewModel>();
            shelves = new List<ShelfViewModel>();
            UpdateCopies();
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(AvailableCopies));
            OnPropertyChanged(nameof(RentedCount));
        }

        private void UpdateCopies()
        {
            var copies = _copyRespository.GetAllItems().Where(C => C.Book_ID == _book.ID).ToList();
            if (copies != null)
            {
                //var allCopies = copies.Select(copy => new CopyViewModel(new Copy(copy.ID, copy.UniqueID,  copy.Book_ID, copy.IsRented, copy.State)));
                //foreach (var copy in copies)
                //    AllCopies.Add(new CopyViewModel(copy));

                //var rentedCopies = copies.Where(cop => cop.IsRented).Select(copy => new CopyViewModel(new Copy(copy.ID, copy.UniqueID, copy.Book_ID, copy.IsRented, copy.State)));
                foreach (var copy in copies)
                {
                    if (copy.IsRented)
                    {
                        RentedCopies.Add(new CopyViewModel(copy));
                    }
                    else
                    {
                        NotRentedCopies.Add(new CopyViewModel(copy));
                    }
                    AllCopies.Add(new CopyViewModel(copy));
                }

            }
            foreach (var item in _cabinetRespository.GetAllItems())
            {
                cabinets.Add(new CabinetViewModel(item));
            }
            foreach (var item in _shelfRespository.GetAllItems())
            {
                shelves.Add(new ShelfViewModel(item));
            }

        }
    }
}
