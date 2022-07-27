
namespace DomainLayer.Models
{
    public class Cabinet
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Cabinet(int iD, string name)
        {
            ID = iD;
            Name = name;
        }
    }
}
