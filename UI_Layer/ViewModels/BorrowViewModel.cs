using DomainLayer.Models;
using System;

namespace UI_Layer.ViewModels
{
    public class BorrowViewModel : BaseViewModel
    {
        private readonly Borrow borrow;

        public BorrowViewModel(Borrow borrow)
        {
            this.borrow = borrow;
        }

        public int BorrowID
        {
            get
            {
                return borrow.Borrow_ID;
            }
            set
            {
                borrow.Borrow_ID = value;
                OnPropertyChanged(nameof(BorrowID));
            }
        }

        public string UniqueID
        {
            get
            {
                return borrow.UniqueID;
            }
            set
            {
                borrow.UniqueID = value;
                OnPropertyChanged(nameof(UniqueID));
            }
        }

        public int CopyID
        {
            get
            {
                return borrow.Copy_ID;
            }
            set
            {
                borrow.Copy_ID = value;
                OnPropertyChanged(nameof(CopyID));
            }
        }
        public string ClientName    
        {
            get
            {
                return borrow.ClientName;
            }
            set
            {
                borrow.ClientName = value;
                OnPropertyChanged(nameof(ClientName));
            }
        }
        
        public DateTime BorrowDate
        {
            get 
            { 
                return borrow.BorrowDate; 
            }
            set 
            { 
                borrow.BorrowDate = value;
                OnPropertyChanged(nameof(BorrowDate));
            }
        }

        public DateTime ReturnDate
        {
            get
            {
                return borrow.ReturnDate;
            }
            set
            {
                borrow.ReturnDate = value;
                OnPropertyChanged(nameof(ReturnDate));
            }
        }

        public DateTime ActualReturnDate
        {
            get
            {
                return borrow.ActualReturnDate;
            }
            set
            {
                borrow.ActualReturnDate = value;
                OnPropertyChanged(nameof(ActualReturnDate));
            }
        }

        public string GivenBy
        {
            get
            {
                return borrow.GivenBy;
            }
            set
            {
                borrow.GivenBy = value;
                OnPropertyChanged(nameof(GivenBy));
            }
        }

        public string ReturnedBy
        {
            get
            {
                return borrow.ReturnedBy;
            }
            set
            {
                borrow.ReturnedBy = value;
                OnPropertyChanged(nameof(ReturnedBy));
            }
        }

        public string CopyIdentifier
        {
            get
            {
                return borrow.Copy_Identifier;
            }
            set
            {
                borrow.Copy_Identifier = value;
                OnPropertyChanged(nameof(CopyIdentifier));
            }
        }
        //private readonly IRepositoryService<Borrow> borrowRepositoryService;
        //private readonly Messanger messanger;

        //public BorrowViewModel(IRepositoryService<Borrow> borrowRepositoryService, Messanger messanger)
        //{
        //    this.borrowRepositoryService = borrowRepositoryService;
        //    this.messanger = messanger;
        //    messanger.DashboardBookReturned += Messanger_DashboardBookReturned;
        //    foreach (var item in borrowRepositoryService.GetAllItems())
        //    {
        //        Borrows.Add(item);
        //    }
        //}

        //private void Messanger_DashboardBookReturned()
        //{
        //    OnPropertyChanged(nameof(SelectedBorrow));
        //}

        //public void WriteintoDB()
        //{

        //}

        //public DateTime ActualReturnDate { get; set; }
        //public ObservableCollection<Borrow> Borrows { get; set; } = new();

        //private Borrow selectedBorrow;
        //public Borrow SelectedBorrow
        //{
        //    get
        //    {
        //        return selectedBorrow;
        //    }
        //    set
        //    {
        //        selectedBorrow = value;
        //        OnPropertyChanged(nameof(SelectedBorrow));
        //    }
        //}
    }
}
