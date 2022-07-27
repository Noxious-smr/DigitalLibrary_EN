
namespace DomainLayer.Models
{
    public class Copy
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string uniqueID;

        public string UniqueID
        {
            get { return uniqueID; }
            set { uniqueID = value; }
        }


        private int book_ID;
        public int Book_ID
        {
            get { return book_ID; }
            set { book_ID = value; }
        }

        private bool isRented;
        public bool IsRented
        {
            get { return isRented; }
            set { isRented = value; }
        }

        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public Copy(int iD, string _uniqueID, int _book_ID, bool isRented, string state)
        {
            ID = iD;
            UniqueID = _uniqueID;
            Book_ID = _book_ID;
            IsRented = isRented;
            State = state;
        }
    }
}
