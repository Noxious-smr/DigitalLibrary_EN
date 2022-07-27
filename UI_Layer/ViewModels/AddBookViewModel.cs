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
    public class AddBookViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly IRepositoryService<Book> repositoryService = new DBBookService();
        private readonly IFileService imageService;
        private readonly IFileService descriptionService;
        private readonly ErrorNotifier errorNotifier;
        public bool HasErrors => errorNotifier.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        
        public AddBookViewModel(ErrorNotifier _errorNotifier)
        {
            errorNotifier = _errorNotifier;
            errorNotifier.ErrorsChanged += ErrorNotifier_ErrorsChanged;
            imageService = new ImageService();
            descriptionService = new DescriptionService();
            foreach (var item in repositoryService.GetAllItems())
            {
                Books.Add(new BookViewModel(item, new DBShelfService(), new DBCabinetService(), new DBCopyService()));
            }
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
        public bool CanAdd => !HasErrors && CanAddHelper ;

        public ObservableCollection<BookViewModel> Books { get; set; } = new ObservableCollection<BookViewModel>();
        public int ComboBoxIndex { get => 0; }

        private bool canAddHelper = false;
        public bool CanAddHelper
        {
            get { return canAddHelper; }
            set { canAddHelper = value; OnPropertyChanged(nameof(CanAddHelper)); }
        }

        public Array Languages => Enum.GetValues(typeof(Language));

        public ICommand AddBookCommand { get => new AddRelayCommand(AddBook, () => CanAdd); }

        private void AddBook(string obj)
        {            
            InsertBookIntoDB();
        }
        public ICommand GetImagePathCommand { get => new FileServiceCommand(GetImage); }
        public ICommand GetDescriptionPathCommand { get => new FileServiceCommand(GetDescription); }

        private void GetImage()
        {
            ImagePath = imageService.GetFile();
        }
        private void GetDescription()
        {
            Description = descriptionService.GetFile();
        }

        public void BookValidator(string sender)
        {
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            switch (sender)
            {
                case nameof(Title):
                    OnPropertyChanged(nameof(Title));
                    errorNotifier.ClearError(nameof(Title));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Title)));
                    CanAddHelper = true;
                    if (string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title))
                    {
                        errorNotifier.AddError(nameof(Title), "Title must not be empty Thank you!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    if (Books.Where(B => B.Title.ToLower() == Title.ToLower()).Any())
                    {
                        errorNotifier.AddError(nameof(Title), "Book already exists, Thank you!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    
                    errorNotifier.ClearError(nameof(ISBN_10));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_10)));
                    if (string.IsNullOrEmpty(ISBN_10) || string.IsNullOrWhiteSpace(ISBN_10) || ISBN_10.Length < 10 || ISBN_10.Length > 10
                        || Books.Any(B => B.ISBN_10.Replace("-", "") == ISBN_10))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_13));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_13)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ISBN_13) || ISBN_13.Length < 13 || ISBN_13.Length > 13
                        || Books.Any(B => B.ISBN_13.Replace("-", "") == ISBN_13))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ImagePath));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ImagePath))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(PageCount));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(PageCount)));
                    if (PageCount < 0 || !PageCount.GetType().Equals(typeof(int)))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Publisher));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Publisher)));
                    if (string.IsNullOrEmpty(Publisher) || string.IsNullOrWhiteSpace(Publisher))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Author));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Author)));
                    if (string.IsNullOrEmpty(Author) || string.IsNullOrWhiteSpace(Author))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Description));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Description)));
                    if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    break;
                case nameof(ISBN_10):
                    OnPropertyChanged(nameof(ISBN_10));
                    CanAddHelper = true;
                    errorNotifier.ClearError(nameof(Title));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Title)));

                    if (string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title) || Books.Where(B => B.Title.ToLower() == Title.ToLower()).Any())
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_10));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_10)));
                    if (string.IsNullOrEmpty(ISBN_10) || string.IsNullOrWhiteSpace(ISBN_10) || ISBN_10.Length < 10 || ISBN_10.Length > 10)
                    {
                        errorNotifier.AddError(nameof(ISBN_10), "ISBN_10 must not be empty and should be a number,\nISBN_10 should be 10 numbers long!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    
                    if (Books.Any(B => B.ISBN_10.Replace("-", "") == ISBN_10))
                    {
                        errorNotifier.AddError(nameof(ISBN_10), "ISBN_10 exists!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_13));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_13)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ISBN_13) || ISBN_13.Length < 13 || ISBN_13.Length > 13
                        || Books.Any(B => B.ISBN_13.Replace("-", "") == ISBN_13))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ImagePath));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ImagePath))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(PageCount));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(PageCount)));
                    if (PageCount == 0)
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Publisher));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Publisher)));
                    if (string.IsNullOrEmpty(Publisher) || string.IsNullOrWhiteSpace(Publisher))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Author));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Author)));
                    if (string.IsNullOrEmpty(Author) || string.IsNullOrWhiteSpace(Author))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Description));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Description)));
                    if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    break;
                case nameof(ISBN_13):
                    OnPropertyChanged(nameof(ISBN_13));
                    CanAddHelper = true;
                    errorNotifier.ClearError(nameof(Title));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Title)));
                    if (string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_10));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_10)));
                    if (string.IsNullOrEmpty(ISBN_10) || string.IsNullOrWhiteSpace(ISBN_10) || ISBN_10.Length < 10 || ISBN_10.Length > 10
                        || Books.Any(B => B.ISBN_10.Replace("-", "") == ISBN_10))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_13));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_13)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ISBN_13) || ISBN_13.Length < 13 || ISBN_13.Length > 13)
                    {
                        errorNotifier.AddError(nameof(ISBN_13), "ISBN_13 must not be empty and should be a number,\nISBN_13 should be 13 numbers long!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    if (Books.Any(B => B.ISBN_13.Replace("-", "") == ISBN_13))
                    {
                        errorNotifier.AddError(nameof(ISBN_13), "ISBN_13 exists!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ImagePath));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ImagePath))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(PageCount));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(PageCount)));
                    if (PageCount == 0)
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Publisher));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Publisher)));
                    if (string.IsNullOrEmpty(Publisher) || string.IsNullOrWhiteSpace(Publisher))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Author));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Author)));
                    if (string.IsNullOrEmpty(Author) || string.IsNullOrWhiteSpace(Author))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Description));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Description)));
                    if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    break;
                case nameof(ImagePath):
                    OnPropertyChanged(nameof(ImagePath));
                    CanAddHelper = true;
                    errorNotifier.ClearError(nameof(Title));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Title)));
                    if (string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_10));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_10)));
                    if (string.IsNullOrEmpty(ISBN_10) || string.IsNullOrWhiteSpace(ISBN_10) || ISBN_10.Length < 10 || ISBN_10.Length > 10
                        || Books.Any(B => B.ISBN_10.Replace("-", "") == ISBN_10))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_13));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_13)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ISBN_13) || ISBN_13.Length < 13 || ISBN_13.Length > 13
                        || Books.Any(B => B.ISBN_13.Replace("-", "") == ISBN_13))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ImagePath));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
                    if (string.IsNullOrEmpty(ImagePath) || string.IsNullOrWhiteSpace(ImagePath))
                    {
                        errorNotifier.AddError(nameof(ImagePath), "Please provide a valid path !");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(PageCount));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(PageCount)));
                    if (PageCount == 0)
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Publisher));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Publisher)));
                    if (string.IsNullOrEmpty(Publisher) || string.IsNullOrWhiteSpace(Publisher))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Author));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Author)));
                    if (string.IsNullOrEmpty(Author) || string.IsNullOrWhiteSpace(Author))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Description));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Description)));
                    if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    break;
                case nameof(PageCount):
                    OnPropertyChanged(nameof(PageCount));
                    CanAddHelper = true;
                    errorNotifier.ClearError(nameof(Title));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Title)));
                    if (string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_10));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_10)));
                    if (string.IsNullOrEmpty(ISBN_10) || string.IsNullOrWhiteSpace(ISBN_10) || ISBN_10.Length < 10 || ISBN_10.Length > 10
                        || Books.Any(B => B.ISBN_10.Replace("-", "") == ISBN_10))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_13));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_13)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ISBN_13) || ISBN_13.Length < 13 || ISBN_13.Length > 13
                        || Books.Any(B => B.ISBN_13.Replace("-", "") == ISBN_13))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ImagePath));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
                    if (string.IsNullOrEmpty(ImagePath) || string.IsNullOrWhiteSpace(ImagePath))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(PageCount));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(PageCount)));
                    if (PageCount == 0 )
                    {
                        errorNotifier.AddError(nameof(PageCount), "Page count must be a number, Thank you!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Publisher));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Publisher)));
                    if (string.IsNullOrEmpty(Publisher) || string.IsNullOrWhiteSpace(Publisher))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Author));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Author)));
                    if (string.IsNullOrEmpty(Author) || string.IsNullOrWhiteSpace(Author))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Description));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Description)));
                    if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    break;
                case nameof(Publisher):
                    OnPropertyChanged(nameof(Publisher));
                    CanAddHelper = true;
                    errorNotifier.ClearError(nameof(Title));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Title)));
                    if (string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_10));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_10)));
                    if (string.IsNullOrEmpty(ISBN_10) || string.IsNullOrWhiteSpace(ISBN_10) || ISBN_10.Length < 10 || ISBN_10.Length > 10
                        || Books.Any(B => B.ISBN_10.Replace("-", "") == ISBN_10))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_13));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_13)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ISBN_13) || ISBN_13.Length < 13 || ISBN_13.Length > 13
                        || Books.Any(B => B.ISBN_13.Replace("-", "") == ISBN_13))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ImagePath));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
                    if (string.IsNullOrEmpty(ImagePath) || string.IsNullOrWhiteSpace(ImagePath))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(PageCount));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(PageCount)));
                    if (PageCount == 0)
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    errorNotifier.ClearError(nameof(Publisher));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Publisher)));
                    if (string.IsNullOrEmpty(Publisher) || string.IsNullOrWhiteSpace(Publisher))
                    {
                        errorNotifier.AddError(nameof(Publisher), "Publisher cannot be empty, Thank you!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Author));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Author)));
                    if (string.IsNullOrEmpty(Author) || string.IsNullOrWhiteSpace(Author))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Description));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Description)));
                    if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    break;
                case nameof(Author):
                    OnPropertyChanged(nameof(Author));
                    CanAddHelper = true;
                    errorNotifier.ClearError(nameof(Title));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Title)));
                    if (string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_10));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_10)));
                    if (string.IsNullOrEmpty(ISBN_10) || string.IsNullOrWhiteSpace(ISBN_10) || ISBN_10.Length < 10 || ISBN_10.Length > 10
                        || Books.Any(B => B.ISBN_10.Replace("-", "") == ISBN_10))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_13));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_13)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ISBN_13) || ISBN_13.Length < 13 || ISBN_13.Length > 13
                        || Books.Any(B => B.ISBN_13.Replace("-", "") == ISBN_13))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ImagePath));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
                    if (string.IsNullOrEmpty(ImagePath) || string.IsNullOrWhiteSpace(ImagePath))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(PageCount));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(PageCount)));
                    if (PageCount == 0)
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Publisher));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Publisher)));
                    if (string.IsNullOrEmpty(Publisher) || string.IsNullOrWhiteSpace(Publisher))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Author));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Author)));
                    if (string.IsNullOrEmpty(Author) || string.IsNullOrWhiteSpace(Author))
                    {
                        errorNotifier.AddError(nameof(Author), "Author cannot be empty, Thank you!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Description));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Description)));
                    if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }
                    break;
                case nameof(Description):
                    OnPropertyChanged(nameof(Description));
                    CanAddHelper = true;
                    errorNotifier.ClearError(nameof(Title));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Title)));
                    if (string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_10));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_10)));
                    if (string.IsNullOrEmpty(ISBN_10) || string.IsNullOrWhiteSpace(ISBN_10) || ISBN_10.Length < 10 || ISBN_10.Length > 10
                        || Books.Any(B => B.ISBN_10.Replace("-", "") == ISBN_10))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ISBN_13));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ISBN_13)));
                    if (string.IsNullOrEmpty(ISBN_13) || string.IsNullOrWhiteSpace(ISBN_13) || ISBN_13.Length < 13 || ISBN_13.Length > 13
                        || Books.Any(B => B.ISBN_13.Replace("-", "") == ISBN_13))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(ImagePath));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
                    if (string.IsNullOrEmpty(ImagePath) || string.IsNullOrWhiteSpace(ImagePath))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(PageCount));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(PageCount)));
                    if (PageCount == 0)
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Publisher));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Publisher)));
                    if (string.IsNullOrEmpty(Publisher) || string.IsNullOrWhiteSpace(Publisher))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Author));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Author)));
                    if (string.IsNullOrEmpty(Author) || string.IsNullOrWhiteSpace(Author))
                    {
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }

                    errorNotifier.ClearError(nameof(Description));
                    ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Description)));
                    if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                    {
                        errorNotifier.AddError(nameof(Description), "Description cannot be empty, Thank you!");
                        CanAddHelper = false;
                        OnPropertyChanged(nameof(CanAdd));
                    }                    
                    break;

                default:
                    break;
            }            
        }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }

        #region Properties
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                BookValidator(nameof(Title));
            }
        }

        private string isbn_10 = string.Empty;
        public string ISBN_10
        {
            get { return isbn_10; }
            set
            {
                isbn_10 = value;
                BookValidator(nameof(ISBN_10));
            }
        }

        private string isbn_13 = string.Empty;
        public string ISBN_13
        {
            get { return isbn_13; }
            set
            {
                isbn_13 = value;
                BookValidator(nameof(ISBN_13));
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                BookValidator(nameof(ImagePath));
            }
        }

        private int pageCount;
        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                BookValidator(nameof(PageCount));
            }
        }

        private string publisher;
        public string Publisher
        {
            get { return publisher; }
            set
            {
                publisher = value;
                BookValidator(nameof(Publisher));
            }
        }

        private DateTime published = DateTime.Now;
        public DateTime Published
        {
            get { return published; }
            set
            {
                published = value;
                BookValidator(nameof(Published));
                OnPropertyChanged(nameof(Today));
            }
        }
        public int Today => Published.Day;

        private string author;
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                BookValidator(nameof(Author));
            }
        }

        private string language;
        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                BookValidator(nameof(Language));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                BookValidator(nameof(Description));
            }
        }

        private CategoryViewModel category;
        public CategoryViewModel Category
        {
            get { return category; }
            set
            {
                category = value;
                errorNotifier.ClearError(nameof(Category));
                if (value == null)
                {
                    errorNotifier.AddError(nameof(Category), "Category cannot be empty, Thank you!");
                }
                OnPropertyChanged(nameof(Category));
            }
        }

        private ShelfViewModel shelf;
        public ShelfViewModel Shelf
        {
            get { return shelf; }
            set
            {
                shelf = value;
                errorNotifier.ClearError(nameof(Shelf));
                if (value == null)
                {
                    errorNotifier.AddError(nameof(Shelf), "Shelf cannot be empty Thank you!");
                }
                OnPropertyChanged(nameof(Shelf));
            }
        } 
        #endregion
        public void InsertBookIntoDB()
        {
            var TitleTemp = Title;
            repositoryService.InsertItem(new Book(0, ISBN_10, ISBN_13, Title.Replace("'",@"''"),
                imageService.CopyFile(ImagePath, "Images"), PageCount, Publisher.Replace("'", @"''"), 
                Published, Author.Replace("'", @"''"), Language, descriptionService.CopyFile(Description, "Descriptions"),
                Category.CategoryID, Shelf.ShelfID));
            Books.Add(new BookViewModel(repositoryService.GetByName(Title.Replace("'", @"''")), new DBShelfService(), new DBCabinetService(), new DBCopyService()));
            Title = "";
            ISBN_10 = "";
            ISBN_13 = "";
            ImagePath = "";
            PageCount = 0;
            Publisher = "";
            Published = DateTime.Now;
            Author = "";
            Language = null;
            Description = "";
            Category = null;
            Shelf = null;
            errorNotifier.ClearError(nameof(Title));
            errorNotifier.ClearError(nameof(ISBN_10));
            errorNotifier.ClearError(nameof(ISBN_13));
            errorNotifier.ClearError(nameof(ImagePath));
            errorNotifier.ClearError(nameof(PageCount));
            errorNotifier.ClearError(nameof(Publisher));
            errorNotifier.ClearError(nameof(Published));
            errorNotifier.ClearError(nameof(Author));
            errorNotifier.ClearError(nameof(Language));
            errorNotifier.ClearError(nameof(Description));
            errorNotifier.ClearError(nameof(Category));
            errorNotifier.ClearError(nameof(Shelf));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(Title)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(ISBN_10)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(ISBN_13)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(ImagePath)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(PageCount)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(Publisher)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(Published)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(Author)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(Language)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(Description)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(Category)));
            ErrorNotifier_ErrorsChanged(this,new DataErrorsChangedEventArgs(nameof(Shelf)));
            Message = $"Book {TitleTemp} successfully added!";   
            OnPropertyChanged(nameof(ShowMessage));
        }

        public void UpdateBookInDB(int ID)
        {
            repositoryService.UpdateItem(new Book(ID, ISBN_10, ISBN_13, Title, ImagePath, PageCount, Publisher,
                Published, Author, Language, Description, Category.CategoryID, Shelf.ShelfID));
        }
        public void DeleteBookFromDB(int ID)
        {
            repositoryService.DeleteItem(ID);
        }        
    }
}
