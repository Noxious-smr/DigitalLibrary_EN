using DomainLayer.Models;

namespace UI_Layer.ViewModels
{
    public class CopyViewModel : BaseViewModel
    {
        private Copy _copy;
        public int ID
        {
            get { return _copy.ID; }
            set { _copy.ID = value; OnPropertyChanged(nameof(ID)); }
        }

        public string UniqueID
        {
            get { return _copy.UniqueID; }
            set { _copy.UniqueID = value; OnPropertyChanged(nameof(UniqueID)); }
        }
        public int BookID
        {
            get { return _copy.Book_ID; }
            set { _copy.Book_ID = value; OnPropertyChanged(nameof(BookID)); }
        }

        public bool IsRented
        {
            get { return _copy.IsRented; }
            set { _copy.IsRented = value; OnPropertyChanged(nameof(IsRented)); }
        }
        
        public string State
        {
            get { return _copy.State; }
            set { _copy.State = value; OnPropertyChanged(nameof(State)); }
        }
        public CopyViewModel(Copy copy)
        {
            _copy = copy;
        }
    }
}
