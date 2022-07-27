using UI_Layer.Commands;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Data;

namespace UI_Layer.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {        
        private readonly IRepositoryService<Employee> _employeeRepository;
        private readonly IRepositoryService<Borrow> _borrowRepository;
        private readonly IRepositoryService<Copy> _copyRepository;
        public DashboardViewModel(IRepositoryService<Employee> _employeeRepository, IRepositoryService<Borrow> _borrowRepository, IRepositoryService<Copy> _copyRepository)
        {
            this._employeeRepository = _employeeRepository;
            this._borrowRepository = _borrowRepository;
            this._copyRepository = _copyRepository;
            foreach (var item in _employeeRepository.GetAllItems())
            {
                Employees.Add(new EmployeeViewModel(item));
            }
            foreach (var item in _borrowRepository.GetAllItems())
            {
                Borrows.Add(new BorrowViewModel(item));
            }
            
        }
        public ObservableCollection<BorrowViewModel> Borrows { get; set; } = new();
        public ICollectionView AllBorrowsSearchable { get => CollectionViewSource.GetDefaultView(Borrows); }
        #region Filtering
        private bool FilterByCopyID(object obj)
        {
            if (obj is BorrowViewModel borrowVM)
            {
                return borrowVM.CopyID.ToString().Contains(CopyIDFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            else
                return false;
        }
        private bool FilterByCopyIdentifier(object obj)
        {
            if (obj is BorrowViewModel borrowVM)
            {
                return borrowVM.CopyIdentifier.Contains(CopyIdentifierFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            else
                return false;
        }
        private bool FilterByClientName(object obj)
        {
            if (obj is BorrowViewModel borrowVM)
            {
                return borrowVM.ClientName.Contains(ClientNameFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            else
                return false;
        }

        private bool FilterByBorrowDate(object obj)
        {
            if (obj is BorrowViewModel borrowVM)
            {
                return borrowVM.BorrowDate.Date.ToString().Contains(BorrowDateFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            else
                return false;
        }


        private string copyIDFilter = string.Empty;
        public string CopyIDFilter
        {
            get { return copyIDFilter; }
            set { copyIDFilter = value; AllBorrowsSearchable.Filter = FilterByCopyID; AllBorrowsSearchable.Refresh(); }
        }

        private string copyIdentifierFilter = string.Empty;
        public string CopyIdentifierFilter
        {
            get { return copyIdentifierFilter; }
            set { copyIdentifierFilter = value; AllBorrowsSearchable.Filter = FilterByCopyIdentifier; AllBorrowsSearchable.Refresh(); }
        }

        private string clientNameFilter = string.Empty;
        public string ClientNameFilter
        {
            get { return clientNameFilter; }
            set { clientNameFilter = value; AllBorrowsSearchable.Filter = FilterByClientName; AllBorrowsSearchable.Refresh(); }
        }

        private string borrowDateFilter = string.Empty;
        public string BorrowDateFilter
        {
            get { return borrowDateFilter; }
            set { borrowDateFilter = value; AllBorrowsSearchable.Filter = FilterByBorrowDate; AllBorrowsSearchable.Refresh(); }
        }
        #endregion

        private BorrowViewModel selectedBorrow;
        public BorrowViewModel SelectedBorrow
        {
            get
            {
                return selectedBorrow;
            }
            set
            {
                selectedBorrow = value;
                OnPropertyChanged(nameof(SelectedBorrow));
            }
        }


        public ObservableCollection<EmployeeViewModel> Employees { get; set; } = new();

        private EmployeeViewModel selectedEmployee;
        public EmployeeViewModel SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public ICommand BorrowReturnCommand { get => new UpdateRelayCommand(BorrowReturn, () => SelectedEmployee is not null); }

        private void BorrowReturn()
        {
            //MessageBox.Show(ShowBorrows.SelectedBorrow.ClientName);
            _borrowRepository.UpdateItem(new Borrow(SelectedBorrow.BorrowID, SelectedBorrow.UniqueID, SelectedBorrow.CopyID,
                                        SelectedBorrow.ClientName, SelectedBorrow.BorrowDate, SelectedBorrow.ReturnDate,
                                        DateTime.Now, 1, SelectedBorrow.GivenBy,
                                        SelectedEmployee.EmployeeID, SelectedEmployee.FirstName, SelectedBorrow.CopyIdentifier));
            _copyRepository.UpdateItem(SelectedBorrow.CopyID, "IsRented", 0);
            var TempBorrow = SelectedBorrow;
            int index = Borrows.IndexOf(SelectedBorrow);
            Borrows.Remove(SelectedBorrow);
            TempBorrow.ActualReturnDate = DateTime.Now;
            TempBorrow.ReturnedBy = SelectedEmployee.FirstName + SelectedEmployee.LastName;
            Borrows.Insert(index,TempBorrow);
            //OnPropertyChanged(nameof(SelectedBorrow.ActualReturnDate));
            //SelectedBorrow = SelectedBorrow;
            //OnPropertyChanged(nameof(SelectedBorrow));
            //messanger.WhenDashboardBookReturned();
        }
        //public DateTime ActualReturnDate { get => ShowBorrows.SelectedBorrow is not null? ShowBorrows.SelectedBorrow.ActualReturnDate : DateTime.MinValue; } 

    }
}
