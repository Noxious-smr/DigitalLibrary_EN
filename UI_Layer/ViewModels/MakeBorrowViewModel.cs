using UI_Layer.Commands;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Text.RegularExpressions;
using UI_Layer.Stores;

namespace UI_Layer.ViewModels
{
    public class MakeBorrowViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        //private Regex regex = new Regex(@"^\b[\p{L}]*\b\s\b[\p{L}]*\b$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        private Regex regex = new Regex(@"^(\b\w+\s)+\b\w+$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        private readonly IRepositoryService<Employee> employeeRepositoryService;
        private readonly IRepositoryService<Borrow> borrowRepositoryService;
        private readonly IRepositoryService<Copy> copyRepositoryService;
        private readonly ErrorNotifier errorNotifier;
        public MakeBorrowViewModel(IRepositoryService<Employee> employeeRepositoryService, IRepositoryService<Borrow> borrowRepositoryService, IRepositoryService<Copy> copyRepositoryService, Messanger messanger, ErrorNotifier errorNotifier)
        {
            this.employeeRepositoryService = employeeRepositoryService; 
            this.borrowRepositoryService = borrowRepositoryService;
            this.copyRepositoryService = copyRepositoryService;
            this.errorNotifier = errorNotifier;
            messanger.CategoriesBookSelected += Messanger_CategoriesBookSelected;
            foreach (var item in employeeRepositoryService.GetAllItems())
            {
                Employees.Add(new EmployeeViewModel(item));
            }
        }

        private void Messanger_CategoriesBookSelected(BookViewModel obj)
        {
            if (obj is not null)
            {
                SelectedBook = obj;
                OnPropertyChanged(nameof(SelectedBook));
            } 
        }




        #region Commands
        public ICommand OpenBorrowWindowCommand { get => new AddRelayCommand(OpenBorrowWindow, () => SelectedBook?.AvailableCopies > 0); }
        public ICommand MakeBorrowCommand { get => new AddRelayCommand(MakeBorrow, 
            () => CanAddBorrowDate && CanAddReturnDate && CanAddClientName && SelectedCopy is not null && GivenByEmployee is not null); }
        //BorrowDate > DateTime.Now && ReturnDate > DateTime.Now
        public ICommand CloseMakeBorrowCommand { get => new AddRelayCommand(ClosemakeBorrow, () => true); }
        #endregion


        #region Methods
        private void OpenBorrowWindow(object parameter)
        {
            ShowMakeBorrow = true;
            SelectedBook = parameter as BookViewModel;
            OnPropertyChanged(nameof(SelectedBook));
            BorrowDate = DateTime.Now;
            OnPropertyChanged(nameof(BorrowDate));
        }
        private void ClosemakeBorrow(string parameter)
        {
            ShowMakeBorrow = false;
        }

        private void MakeBorrow(object parameter)
        {
            copyRepositoryService.UpdateItem(SelectedCopy.ID, "IsRented", 1);
            borrowRepositoryService.InsertItem(new Borrow(0, Guid.NewGuid().ToString(), SelectedCopy.ID, ClientName,
                BorrowDate, ReturnDate, DateTime.MinValue, GivenByEmployee.EmployeeID,
                GivenByEmployee.FirstName, 6, "NONE", SelectedCopy.UniqueID));
            SelectedBook.NotRentedCopies.Remove(SelectedCopy);
            if (SelectedBook.NotRentedCopies.Count > 0)
            {
                SelectedCopy = SelectedBook.NotRentedCopies[0];
                OnPropertyChanged(nameof(SelectedCopy));
            }
            OnPropertyChanged(nameof(SelectedBook));
            ClientName = string.Empty;
            GivenByEmployee = null;


        } 
        #endregion

        #region Properties
        public BookViewModel SelectedBook { get; set; }
        private CopyViewModel selectedCopy;

        public CopyViewModel SelectedCopy
        {
            get
            {
                return selectedCopy;
            }
            set
            {
                selectedCopy = value;
                OnPropertyChanged(nameof(SelectedCopy));
            }
        }


        private EmployeeViewModel givenByEmployee;

        public EmployeeViewModel GivenByEmployee
        {
            get
            {
                return givenByEmployee;
            }
            set
            {
                givenByEmployee = value;
                OnPropertyChanged(nameof(GivenByEmployee));
            }
        }


        public ObservableCollection<EmployeeViewModel> Employees { get; set; } = new();

        private bool showMakeBorrow = false;
        public bool ShowMakeBorrow
        {
            get { return showMakeBorrow; }
            set { showMakeBorrow = value; OnPropertyChanged(nameof(ShowMakeBorrow)); }
        }
        private string clientName;

        public string ClientName
        {
            get
            {
                return clientName;
            }
            set
            {
                clientName = value;
                ValidateClientName();
            }
        }

        public bool CanAddClientName { get; set; } = false;
        public bool CanAddBorrowDate { get; set; } = false;
        public bool CanAddReturnDate { get; set; } = false;
        public bool CanAddEmployee { get; set; } = false;


        private DateTime borrowDate = DateTime.UtcNow;
        public DateTime BorrowDate
        {
            get { return borrowDate; }
            set
            {
                borrowDate = value; TodayFromBorrow = value.Day;
                if (value.Date != DateTime.Now.Date)
                {
                    ValidateBorrowDate();
                }
                else
                {
                    errorNotifier.ClearError(nameof(BorrowDate));
                    errorNotifier.OnErrorChanged(nameof(BorrowDate));
                    CanAddBorrowDate = true;
                    OnPropertyChanged(nameof(CanAddBorrowDate));
                }
            }
        }

        private DateTime returnDate = DateTime.UtcNow;
        public DateTime ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value;  TodayFromReturn = value.Day; ValidateReturnDate(); }
        }

        private int todayfromBorrow = DateTime.UtcNow.Day;    
        public int TodayFromBorrow
        {
            get { return todayfromBorrow; }
            set { todayfromBorrow = value; OnPropertyChanged(nameof(TodayFromBorrow)); }
        }
        private int todayfromReturn = DateTime.UtcNow.Day;
        public int TodayFromReturn
        {
            get { return todayfromReturn; }
            set { todayfromReturn = value; OnPropertyChanged(nameof(TodayFromReturn)); }
        }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
        #endregion

        #region Validation
        private void ValidateBorrowDate()
        {
            OnPropertyChanged(nameof(BorrowDate));
            errorNotifier.ClearError(nameof(BorrowDate));
            errorNotifier.OnErrorChanged(nameof(BorrowDate));
            Message = string.Empty;
            OnPropertyChanged(nameof(ShowMessage));
            //CanAddHelper = true;
            CanAddBorrowDate = true;
            OnPropertyChanged(nameof(CanAddBorrowDate));
            if (BorrowDate < DateTime.Now)
            {
                errorNotifier.AddError(nameof(BorrowDate), "Date must be after today's date!!");
                errorNotifier.OnErrorChanged(nameof(BorrowDate));
                CanAddBorrowDate = false;
                OnPropertyChanged(nameof(CanAddBorrowDate));
            }
        }
        private void ValidateReturnDate()
        {
            OnPropertyChanged(nameof(ReturnDate));
            errorNotifier.ClearError(nameof(ReturnDate));
            errorNotifier.OnErrorChanged(nameof(ReturnDate));
            Message = string.Empty;
            OnPropertyChanged(nameof(ShowMessage));
            CanAddReturnDate = true;
            OnPropertyChanged(nameof(CanAddReturnDate));
            if (ReturnDate <= DateTime.Now)
            {
                errorNotifier.AddError(nameof(ReturnDate), "Date must be after today's date!!");
                errorNotifier.OnErrorChanged(nameof(ReturnDate));
                CanAddReturnDate = false;
                OnPropertyChanged(nameof(CanAddReturnDate));
            }

        }
        private void ValidateClientName()
        {
            OnPropertyChanged(nameof(ClientName));
            errorNotifier.ClearError(nameof(ClientName));
            errorNotifier.OnErrorChanged(nameof(ClientName));
            Message = string.Empty;
            OnPropertyChanged(nameof(ShowMessage));
            CanAddClientName = true;
            OnPropertyChanged(nameof(CanAdd));
            if (string.IsNullOrEmpty(ClientName))
            {
                errorNotifier.AddError(nameof(ClientName), "Name cannot be empty!!");
                errorNotifier.OnErrorChanged(nameof(ClientName));
                CanAddClientName = false;
                OnPropertyChanged(nameof(CanAddClientName));
            }
            //if (regex.IsMatch(ClientName))
            //{
            //    errorNotifier.AddError(nameof(ClientName), "Please write in the form [First name (space) Last Name]!!");
            //    errorNotifier.OnErrorChanged(nameof(ClientName));
            //    CanAddClientName = false;
            //    OnPropertyChanged(nameof(CanAddClientName));
            //}
            if (!regex.IsMatch(ClientName))
            {
                errorNotifier.AddError(nameof(ClientName), "Please write in the form [First name] [Last Name] and only letters!!");
                errorNotifier.OnErrorChanged(nameof(ClientName));
                CanAddClientName = false;
                OnPropertyChanged(nameof(CanAddClientName));
            }

        }
        #endregion

        #region ErrorNotification

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => errorNotifier.HasErrors;
        private void ErrorNotifier_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanAdd));
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return errorNotifier.GetErrors(propertyName);
        }
        private bool canAddHelper = false;
        public bool CanAddHelper
        {
            get { return canAddHelper; }
            set { canAddHelper = value; OnPropertyChanged(nameof(CanAddHelper)); }
        }
        public bool CanAdd => !HasErrors && CanAddHelper; 
        #endregion
    }
}
