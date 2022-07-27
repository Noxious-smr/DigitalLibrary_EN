using UI_Layer.Commands;
using DomainLayer.Abstractions;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using UI_Layer.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Windows.Input;
using System.Diagnostics;
using UI_Layer.Stores;
using DataAccess.DataAccess;

namespace UI_Layer.ViewModels
{
    public class UpdateBookViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly Messanger messanger;
        private readonly IRepositoryService<Book> repositoryService;
        private readonly UpdateCategoryViewModel updateCategory;
        private readonly UpdateCabinetViewModel updateCabinet;
        private readonly ErrorNotifier errorNotifier;
        private readonly IFileService imageService;
        private readonly IFileService descriptionService;
        private readonly Dictionary<string, string> booksNames = new Dictionary<string, string>();
        public ObservableCollection<BookViewModel> Books { get; set; } = new ();
        public ObservableCollection<CategoryViewModel> Categories { get; set; } = new();
        public ObservableCollection<CabinetViewModel> Cabinets { get; set; } = new();
        public int ComboBoxIndex { get; set; }
        public Array Languages = Enum.GetValues(typeof(Language));
        public List<string> languages { get; set; } = new();
        

        public UpdateBookViewModel(ErrorNotifier errorNotifier, IRepositoryService<Book> repositoryService, UpdateCategoryViewModel updateCategory, UpdateCabinetViewModel updateCabinet, Messanger messanger)
        {            
            booksNames.Add("ISBN_10", "CanUpdateISBN_10");
            booksNames.Add("ISBN_13", "CanUpdateISBN_13");
            booksNames.Add("Title", "CanUpdateTitle");
            booksNames.Add("ImagePath", "CanUpdateImagePath");
            booksNames.Add("PageCount", "CanUpdatePageCount");
            booksNames.Add("Publisher", "CanUpdatePublisher");
            booksNames.Add("Published", "CanUpdatePublished");
            booksNames.Add("Author", "CanUpdateAuthor");
            booksNames.Add("Language", "CanUpdateLanguage");
            booksNames.Add("Description", "CanUpdateDescription");
            booksNames.Add("Category_ID", "CanUpdateCategory");
            booksNames.Add("Shelf_ID", "CanUpdateShelf");
            this.updateCategory = updateCategory;
            this.updateCabinet = updateCabinet;
            this.messanger = messanger;
            Categories = updateCategory.Categories;
            Cabinets = updateCabinet.Cabinets;
            foreach (var item in Languages)
            {
                languages.Add(item.ToString());
            }
            this.errorNotifier = errorNotifier;
            this.repositoryService = repositoryService;
            imageService = new ImageService();
            descriptionService = new DescriptionService();
            List<string> currentImages = new List<string>();
            List<string> currentDescriptions = new List<string>();
            foreach (var item in repositoryService.GetAllItems())
            {
                Books.Add(new BookViewModel(item, new DBShelfService(), new DBCabinetService(), new DBCopyService()));
                currentImages.Add(Environment.CurrentDirectory + @"\Books\Images\" + item.ImagePath);
                currentDescriptions.Add(Environment.CurrentDirectory + @"\Books\Descriptions\" + item.Description);
            }
            List<string> imagesList = new List<string>();
            List<string> descriptionsList = new List<string>();
            if (Directory.Exists(Environment.CurrentDirectory + @"\Books\Images"))
            {
                imagesList.AddRange(Directory.GetFiles(Environment.CurrentDirectory + @"\Books\Images"));
            }
            if (Directory.Exists(Environment.CurrentDirectory + @"\Books\Descriptions"))
            {
                descriptionsList.AddRange(Directory.GetFiles(Environment.CurrentDirectory + @"\Books\Descriptions"));
            }
            //List<string> imagesList = new List<string>(Directory.GetFiles(Environment.CurrentDirectory + @"\Books\Images"));
            //List<string> descriptionsList = new List<string>(Directory.GetFiles(Environment.CurrentDirectory + @"\Books\Descriptions"));
            List<string> filesToDelete = new List<string>();
            foreach (var item in imagesList)
            {
                if (!currentImages.Contains(item))
                {
                    filesToDelete.Add(item);
                }
            }
            foreach (var item in descriptionsList)
            {
                if (!currentDescriptions.Contains(item))
                {
                    filesToDelete.Add(item);
                }
            }
            imageService.DeleteFiles(filesToDelete);
            filesToDelete.Clear();
            BooksFirstItemIndex = 0;
            OnPropertyChanged(nameof(BooksFirstItemIndex));
            SelectedBook = Books.Count > 0 ? Books[0] : null;            
            messanger.CategoryUpdated += UpdateCategory_CategoryUpdated;
            messanger.BookDeleted += Messanger_BookDeleted;
        }

        private void Messanger_BookDeleted()
        {
            if (Books.Count > 0)
            {
                SelectedBook = Books.First() ;    
            }
            else
            {
                SelectedBook= null; 
            }
        }

        private void UpdateCategory_CategoryUpdated(int index)
        {
            if (index == CurrentCategoryIndex)
            {
                Category = Categories[index];
                OnPropertyChanged(nameof(Category));

            }
        }

        public int BooksFirstItemIndex { get; set; }

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

        /// <summary>
        /// Commands
        /// </summary>
        #region Commands
        public ICommand GetImagePathCommand { get => new FileServiceCommand(GetImage); }
        public ICommand GetDescriptionPathCommand { get => new FileServiceCommand(GetDescription); }
        public ICommand UpdateTitleCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdateISBN_10Command { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdateISBN_13Command { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdateImagePathCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdatePageCountCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdatePublisherCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdatePublishedCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdateAuthorCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdateLanguageCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdateDescriptionCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdateCategoryCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        public ICommand UpdateShelfCommand { get => new UpdateRelayCommand(UpdateIntoDB, CanUpdateVariable); }
        
        #endregion


        #region Commands Delegates
        private void GetImage()
        {
            ImagePath = imageService.GetFile();
        }
        private void GetDescription()
        {
            Description = descriptionService.GetFile();
        } 
        private bool CanUpdateVariable(string parameter)
        {
            if (parameter == "Language")
            {
                return CanUpdateLangugae;
            }
            if (parameter is not null)
            {
                PropertyInfo propertyInfo = GetType().GetProperty(booksNames[parameter]);
                return (bool)propertyInfo.GetValue(this); 
            }
            return false;
        }

        private void UpdateIntoDB(string parameter)
        {
            PropertyInfo PropertyName = GetType().GetProperty(parameter);
            int BookID = SelectedBook.BookID;
            int BookIndex = Books.IndexOf(SelectedBook);
            switch (PropertyName.GetValue(this).GetType().Name.ToLower())
            {
                case "string":
                    Debug.WriteLine($"{PropertyName.Name} is in case 1 as {PropertyName.PropertyType.Name} and has a value {PropertyName.GetValue(this)}");
                    try
                    {
                        repositoryService.UpdateItem(SelectedBook.BookID, parameter, PropertyName.GetValue(this).ToString().Replace("'", @"''"));
                        UpdateBook(PropertyName.Name);
                    }
                    catch (Exception)
                    {                        
                    }
                    break;
                case "datetime":
                    Debug.WriteLine($"{PropertyName.Name} is in Case 2 as  {PropertyName.PropertyType.Name} and has a value {DateTime.Parse(PropertyName.GetValue(this).ToString()).ToString("yyyy-M-d")}");
                    try
                    {
                        repositoryService.UpdateItem(SelectedBook.BookID, parameter, DateTime.Parse(PropertyName.GetValue(this).ToString()).ToString("yyyy-M-d"));
                        UpdateBook(PropertyName.Name);
                    }
                    catch (Exception)
                    {                        
                    }
                    break;
                default:
                    Debug.WriteLine($"{PropertyName.Name} is {PropertyName.PropertyType.Name} and has a value {PropertyName.GetValue(this)}");
                    try
                    {
                        repositoryService.UpdateItem(SelectedBook.BookID, parameter, PropertyName.GetValue(this));
                        UpdateBook(PropertyName.Name);
                    }
                    catch (Exception)
                    {
                    }
                    break;
            }
        }
        private void UpdateBook(string propertyName)
        {
            //Books.Insert(BookIndex, new BookViewModel(repositoryService.GetById(BookID),
            //    new DBShelfService(),
            //    new DBCabinetService(),
            //    new DBCopyService()));
            //SelectedBook = Books.ElementAt(BookIndex);
            //OnPropertyChanged(nameof(SelectedBook));
            ////BookUpdated?.Invoke(BookIndex);
            //messanger.WhenBookUpdated(BookIndex);
            //Books.RemoveAt(BookIndex + 1);
            //ValidateAll();
            var TempBook = SelectedBook;
            int index = Books.IndexOf(TempBook);
            var NewBook = new BookViewModel(repositoryService.GetById(TempBook.BookID),
            new DBShelfService(), new DBCabinetService(), new DBCopyService());
            Books.Insert(index,NewBook);
            Books.Remove(TempBook);
            SelectedBook = Books[index];
            ValidateAll();
            messanger.WhenBookUpdated();
            Message = $"{propertyName} updated successfully!";
            OnPropertyChanged(nameof(ShowMessage));

        }
        #endregion

        #region Properties
        private BookViewModel selectedBook;
        public BookViewModel SelectedBook 
        { 
            get 
            { 
                return selectedBook; 
            }
            set 
            {
                if (value is not null)
                {
                    selectedBook = value;
                    //SelectedBookChanged?.Invoke(SelectedBook);
                    OnPropertyChanged(nameof(SelectedBook));
                    messanger.WhenSelectedBookChanged();
                    title = value.Title;
                    isbn_10 = value.ISBN_10.Replace("-", string.Empty);
                    isbn_13 = value.ISBN_13.Replace("-", string.Empty);
                    pageCount = value.PageCount;
                    publisher = value.Publisher;
                    ComboBoxIndex = languages.IndexOf(value.Language);
                    author = value.Author;
                    published = DateTime.Parse(value.Published);
                    Today = published.Day;
                    Message = string.Empty;
                    Category = Categories.Where(C => SelectedBook.CategoryID == C.CategoryID).First();

                    Cabinet = Cabinets.Where(C => C.CabinetID == SelectedBook.CabinetID).First();
                    Shelf = Shelves.Where(S => S.ShelfID == SelectedBook.ShelfID).First();
                    //OnPropertyChanged(nameof(Cabinet));
                    //OnPropertyChanged(nameof(Shelf));

                    //OnPropertyChanged(nameof(ShowMessage));
                    //OnPropertyChanged(nameof(Title));
                    //OnPropertyChanged(nameof(Published));
                    //OnPropertyChanged(nameof(Today));
                    //OnPropertyChanged(nameof(Publisher));
                    //OnPropertyChanged(nameof(ISBN_10));
                    //OnPropertyChanged(nameof(ISBN_13));
                    //OnPropertyChanged(nameof(Language));
                    //OnPropertyChanged(nameof(Author));
                    //OnPropertyChanged(nameof(PageCount));
                    //OnPropertyChanged(nameof(ComboBoxIndex));
                    //OnPropertyChanged(nameof(Category)); 
                    OnPropertyChanged(string.Empty);
                }
                else
                {
                    //OnPropertyChanged(nameof(SelectedBook));
                    Message = string.Empty;
                    title = null;
                    published = DateTime.Now;
                    publisher = null;
                    author = null;
                    isbn_10 = null;
                    isbn_13 = null;
                    pageCount =0;
                    //OnPropertyChanged(nameof(Cabinet));
                    //OnPropertyChanged(nameof(Shelf));
                    //OnPropertyChanged(nameof(ShowMessage));
                    //OnPropertyChanged(nameof(Title));
                    //OnPropertyChanged(nameof(Published));
                    //OnPropertyChanged(nameof(Today));
                    //OnPropertyChanged(nameof(Publisher));
                    //OnPropertyChanged(nameof(ISBN_10));
                    //OnPropertyChanged(nameof(ISBN_13));
                    //OnPropertyChanged(nameof(Language));
                    //OnPropertyChanged(nameof(Author));
                    //OnPropertyChanged(nameof(PageCount));
                    //OnPropertyChanged(nameof(ComboBoxIndex));
                    //OnPropertyChanged(nameof(Category));
                    OnPropertyChanged(string.Empty);
                }
            }
        }        

        private string isbn_10 = string.Empty;
        public string ISBN_10
        {
            get
            {
                return isbn_10;
            }
            set
            {
                isbn_10 = value;
                ISBN_10Validator();
            }
        }
        public bool CanUpdateISBN_10 { get; set; } = false;
        
        private string isbn_13 = string.Empty;
        public string ISBN_13
        {
            get
            {
                return isbn_13;
            }
            set
            {
                isbn_13 = value;
                ISBN_13Validator();
            }
        }
        public bool CanUpdateISBN_13 { get; set; } = false;

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                TitleValidator();
            }
        }
        public bool CanUpdateTitle { get; set; } = false;

        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                ImagePathValidator();
            }
        }
        public bool CanUpdateImagePath { get; set; } = false;

        private int pageCount;
        public int PageCount
        {
            get
            {
                return pageCount;
            }
            set
            {
                pageCount = value;
                if (SelectedBook is null)
                {
                    OnPropertyChanged(nameof(PageCount));
                }
                else
                {
                    PageCountValidator();
                }
            }
        }
        public bool CanUpdatePageCount { get; set; } = false;

        private string publisher;
        public string Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                publisher = value;
                PublisherValidator();
            }
        }
        public bool CanUpdatePublisher { get; set; } = false;
        
        private DateTime published = DateTime.Now;
        public DateTime Published
        {
            get { return published; }
            set
            {
                published = value;
                Today = value.Day;
                OnPropertyChanged(nameof(Today));
                PublishedValidator();
            }
        }
        public int Today { get; set; }
        public bool CanUpdatePublished { get; set; } = false;
        

        private string author;
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                AuthorValidator();
            }
        }
        public bool CanUpdateAuthor { get; set; } = false;

        public string Language { get; set; } = ViewModels.Language.English.ToString();
        public bool CanUpdateLangugae { get => true; }        

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                DescriptionValidator();
            }
        }
        public bool CanUpdateDescription { get; set; } = false;

        public int CurrentCategoryIndex { get; set; }

        private CategoryViewModel category;
        public CategoryViewModel Category 
        {
            get
            {
                return category;
            }
            set
            {
                if (value is not null)
                {
                    CurrentCategoryIndex = Categories.IndexOf(value);
                }
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        public int Category_ID { get => Category.CategoryID; }
        public bool CanUpdateCategory { get => true; }

        private CabinetViewModel cabinet;
        public CabinetViewModel Cabinet
        {
            get
            {
                return cabinet;
            }
            set
            {
                cabinet = value;
                OnPropertyChanged(nameof(Cabinet));
                OnPropertyChanged(nameof(Shelves));
            }
        }
        public ObservableCollection<ShelfViewModel> Shelves { get => Cabinet?.Shelves; }
        public ShelfViewModel Shelf { get; set; }
        public bool CanUpdateShelf { get => Shelf is not null; }
        public int Shelf_ID { get => Shelf.ShelfID; }
        #endregion

        #region Validators
        private void ClearAllErrors()
        {
            errorNotifier.ClearError(nameof(Title));
            errorNotifier.ClearError(nameof(ISBN_10));
            errorNotifier.ClearError(nameof(ISBN_13));
            errorNotifier.ClearError(nameof(ImagePath));
            errorNotifier.ClearError(nameof(PageCount));
            errorNotifier.ClearError(nameof(Publisher));
            errorNotifier.ClearError(nameof(Author));
            errorNotifier.ClearError(nameof(Description));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Title)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_10)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_13)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(PageCount)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Publisher)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Author)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Description)));
        }
        private void ValidateAll()
        {            
            ISBN_10Validator();
            ISBN_13Validator();
            TitleValidator();
            ImagePathValidator();
            DescriptionValidator();
            PublishedValidator();
            PublisherValidator();
            AuthorValidator();
            PageCountValidator();

        }
        public void ISBN_10Validator()
        {
            CanUpdateISBN_10 = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            //errorNotifier.ClearError(nameof(ISBN_10));
            ClearAllErrors();
            OnPropertyChanged(nameof(ISBN_10));
            if (ISBN_10 is not null)
            {
                if (string.IsNullOrEmpty(ISBN_10) || string.IsNullOrWhiteSpace(ISBN_10) || ISBN_10.Length <= 0)
                {
                    errorNotifier.AddError(nameof(ISBN_10), "ISBN_10 cannot be empty, Thank you!");
                    CanUpdateISBN_10 = false;
                }
                if (ISBN_10.Length != 10)
                {
                    errorNotifier.AddError(nameof(ISBN_10), "ISBN_10 Must be 10 numbers long, Thank you!");
                    CanUpdateISBN_10 = false;
                }
                if (Books.Any(B => B.ISBN_10.Replace("-", "") == ISBN_10))
                {
                    errorNotifier.AddError(nameof(ISBN_10), "ISBN_10 already exists, Thank you!");
                    CanUpdateISBN_10 = false;
                } 
            }
            else
            {
                CanUpdateISBN_10 = false;
            }
        }

        public void ISBN_13Validator()
        {
            CanUpdateISBN_13 = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            ClearAllErrors();
            //errorNotifier.ClearError(nameof(ISBN_13));
            OnPropertyChanged(nameof(ISBN_13));
            if (ISBN_13 is not null)
            {
                if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ISBN_13) || ISBN_13.Length <= 0)
                {
                    errorNotifier.AddError(nameof(ISBN_13), "ISBN_13 cannot be empty, Thank you!");
                    CanUpdateISBN_13 = false;
                }
                if (ISBN_13.Length != 13)
                {
                    errorNotifier.AddError(nameof(ISBN_13), "ISBN_13 Must be 13 numbers long, Thank you!");
                    CanUpdateISBN_13 = false;
                }
                if (Books.Any(B => B.ISBN_13.Replace("-", "") == ISBN_13))
                {
                    errorNotifier.AddError(nameof(ISBN_13), "ISBN_13 already exists, Thank you!");
                    CanUpdateISBN_13 = false;
                } 
            }
            else
            {
                CanUpdateISBN_13 = false;
            }
        }

        public void TitleValidator()
        {
            CanUpdateTitle = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            ClearAllErrors();
            //errorNotifier.ClearError(nameof(Title));
            OnPropertyChanged(nameof(Title));
            if (string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title) || Title.Length <= 0)
            {
                errorNotifier.AddError(nameof(Title), "Title cannot be empty, Thank you!");
                CanUpdateTitle = false;
            }
            if (Books.Any(B => B.Title == Title))
            {
                errorNotifier.AddError(nameof(Title), "Book Title already exists, Thank you!");
                CanUpdateTitle = false;
            }
        }

        public void ImagePathValidator()
        {
            CanUpdateImagePath = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            ClearAllErrors();
            //errorNotifier.ClearError(nameof(ImagePath));
            OnPropertyChanged(nameof(ImagePath));
            if (string.IsNullOrEmpty(ImagePath) || string.IsNullOrWhiteSpace(ImagePath) || ImagePath.Length <= 0)
            {
                errorNotifier.AddError(nameof(ImagePath), "Please select an Image, Thank you!");
                CanUpdateImagePath = false;
            }
        }

        public void PageCountValidator()
        {
            CanUpdatePageCount = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            ClearAllErrors();
            //errorNotifier.ClearError(nameof(PageCount));
            OnPropertyChanged(nameof(PageCount));
            if (SelectedBook is not null)
            {
                if (PageCount == 0)
                {
                    errorNotifier.AddError(nameof(PageCount), "Page Count cannot be zero, Thank you!");
                    CanUpdatePageCount = false;
                } 
            }
            else
            {
                CanUpdatePageCount = false;
            }
        }

        public void PublisherValidator()
        {
            CanUpdatePublisher = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            ClearAllErrors();
            //errorNotifier.ClearError(nameof(Publisher));
            OnPropertyChanged(nameof(Publisher));
            if (string.IsNullOrEmpty(Publisher) || string.IsNullOrWhiteSpace(Publisher))
            {
                errorNotifier.AddError(nameof(Publisher), "Publisher cannot be empty, Thank you!");
                CanUpdatePublisher = false;
            }
        }

        public void PublishedValidator()
        {
            CanUpdatePublished = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            ClearAllErrors();
            //errorNotifier.ClearError(nameof(Published));
            OnPropertyChanged(nameof(Published));
            if (string.IsNullOrEmpty(Published.ToString()) || string.IsNullOrWhiteSpace(Published.ToString()))
            {
                errorNotifier.AddError(nameof(Published), "Published cannot be empty, Thank you!");
                CanUpdatePublished = false;
            }
        }

        public void AuthorValidator()
        {
            if (Author is not null)
            {
                CanUpdateAuthor = true;
                Message = "";
                OnPropertyChanged(nameof(ShowMessage));
                ClearAllErrors();
                //errorNotifier.ClearError(nameof(Author));
                OnPropertyChanged(nameof(Author));
                if (string.IsNullOrEmpty(Author.ToString()) || string.IsNullOrWhiteSpace(Author.ToString()))
                {
                    errorNotifier.AddError(nameof(Author), "Author cannot be empty, Thank you!");
                    CanUpdateAuthor = false;
                } 
            }
            else
            {
                CanUpdateAuthor = false;
            }

        }

        public void DescriptionValidator()
        {
            CanUpdateDescription = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            ClearAllErrors();
            //errorNotifier.ClearError(nameof(Description));
            OnPropertyChanged(nameof(Description));
            if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description) || Description.Length <= 0)
            {
                errorNotifier.AddError(nameof(Description), "Please select a file, Thank you!");
                CanUpdateDescription = false;
            }
        }
        #endregion
    }
}
