using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Borrow
    {
        public Borrow(int borrow_ID, string uniqueID, int copy_ID, string clientName, DateTime borrowDate, DateTime returnDate, DateTime actualReturnDate, int givenByEmployee, string givenBy, int returnedByEmployee, string returnedBy, string copy_Identifier)
        {
            Borrow_ID = borrow_ID;
            UniqueID = uniqueID;
            Copy_ID = copy_ID;
            ClientName = clientName;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            ActualReturnDate = actualReturnDate;
            GivenByEmployee = givenByEmployee;
            GivenBy = givenBy;
            ReturnedByEmployee = returnedByEmployee;
            ReturnedBy = returnedBy;
            Copy_Identifier = copy_Identifier;
        }

        private int borrow_ID;
        public int Borrow_ID
        {
            get { return borrow_ID; }
            set { borrow_ID = value; }
        }

        private string uniqueID;
        public string UniqueID
        {
            get { return uniqueID; }
            set { uniqueID = value; }
        }

        private int copy_ID;
        public int Copy_ID
        {
            get { return copy_ID; }
            set { copy_ID = value; }
        }

        private string clientName;
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }

        private DateTime borrowDate;
        public DateTime BorrowDate
        {
            get { return borrowDate; }
            set { borrowDate = value; }
        }

        private DateTime returnDate;
        public DateTime ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; }
        }

        private DateTime actualReturnDate;
        public DateTime ActualReturnDate
        {
            get { return actualReturnDate; }
            set { actualReturnDate = value; }
        }

        private int givenByEmployee;
        public int GivenByEmployee
        {
            get { return givenByEmployee; }
            set { givenByEmployee = value; }
        }

        private string givenBy;

        public string GivenBy
        {
            get { return givenBy; }
            set { givenBy = value; }
        }


        private int returnedByEmployee;       

        public int ReturnedByEmployee
        {
            get { return returnedByEmployee; }
            set { returnedByEmployee = value; }
        }

        private string returnedBy;

        public string ReturnedBy
        {
            get { return returnedBy; }
            set { returnedBy = value; }
        }


        private string copy_Identifier;

        public string Copy_Identifier
        {
            get { return copy_Identifier; }
            set { copy_Identifier = value; }
        }


    }
}
