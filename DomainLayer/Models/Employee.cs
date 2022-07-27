using System;

namespace DomainLayer.Models
{
    public class Employee
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

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private DateTime dateTime;
        public DateTime Birthdate
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        public Employee(int id, string uniqueID, string firstName, string lastName, DateTime birthdate)
        {
            ID = id;
            UniqueID = uniqueID;
            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
        }
    }
}
