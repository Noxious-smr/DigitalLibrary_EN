using DomainLayer.Models;
using System;

namespace UI_Layer.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        private readonly Employee _employee;

        public int EmployeeID
        {
            get
            {
                return _employee.ID;
            }
            set
            {
                _employee.ID = value;
                OnPropertyChanged(nameof(EmployeeID));
            }
        }

        public string UniqueID
        {
            get
            {
                return _employee.UniqueID;
            }
            set
            {
                _employee.UniqueID = value;
                OnPropertyChanged(nameof(UniqueID));
            }
        }

        public string FirstName
        {
            get
            {
                return _employee.FirstName;
            }
            set
            {
                _employee.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get
            {
                return _employee.LastName;
            }
            set
            {
                _employee.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Birthdate
        {
            get
            {
                return _employee.Birthdate.Date.ToString("d");
            }
            set
            {
                _employee.Birthdate = DateTime.Parse(value);
                OnPropertyChanged(nameof(Birthdate));
            }
        }
        public EmployeeViewModel(Employee employee)
        {
            _employee = employee;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
