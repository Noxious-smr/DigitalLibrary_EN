
namespace DomainLayer.Models
{
    public class Shelf
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int cabinet_ID;
        public int Cabinet_ID
        {
            get { return cabinet_ID; }
            set { cabinet_ID = value; }
        }

        private string shelf_Nr;
        public string Shelf_Nr
        {
            get { return shelf_Nr; }
            set { shelf_Nr = value; }
        }

        public Shelf(int iD, int cabinet_ID, string shelf_Nr)
        {
            ID = iD;
            Cabinet_ID = cabinet_ID;
            Shelf_Nr = shelf_Nr;
        }

    }
}
