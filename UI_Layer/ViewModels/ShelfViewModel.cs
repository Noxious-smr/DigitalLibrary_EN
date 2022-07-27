using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Layer.ViewModels
{
    public class ShelfViewModel : BaseViewModel
    {
        private readonly Shelf _shelf;
        public int ShelfID
        {
            get
            {
                return _shelf.ID;
            }
            set
            {
                _shelf.ID = value;
                OnPropertyChanged(nameof(ShelfID));
            }
        }
        public int CabinetID
        {
            get
            {
                return _shelf.Cabinet_ID;
            }
            set
            {
                _shelf.Cabinet_ID = value;
                OnPropertyChanged(nameof(CabinetID));
            }
        }
        public string ShelfNr
        {
            get
            {
                return _shelf.Shelf_Nr;
            }
            set
            {
                _shelf.Shelf_Nr = value;
                OnPropertyChanged(nameof(ShelfNr));
            }
        }
        public ShelfViewModel(Shelf shelf)
        {
            _shelf = shelf;
        }



    }
}
