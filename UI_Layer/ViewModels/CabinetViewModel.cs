using DataAccess.DataAccess;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using System.Collections.ObjectModel;
using System.Linq;

namespace UI_Layer.ViewModels
{
    public class CabinetViewModel : BaseViewModel
    {
        private readonly Cabinet _cabinet;
        private readonly IRepositoryService<Shelf> shelfrepositoryService;
        public int CabinetID
        {
            get
            {
                return _cabinet.ID;
            }
            set
            {
                _cabinet.ID = value;
                OnPropertyChanged(nameof(CabinetID));
            }
        }

        public string CabinetName
        {
            get
            {
                return _cabinet.Name;
            }
            set
            {
                _cabinet.Name = value;
                OnPropertyChanged(nameof(CabinetName));
            }
        }
        private int numberOfShleves;

        public int NumberOfShleves
        {
            get
            {
                return numberOfShleves;
            }
            set
            {
                numberOfShleves = Shelves.Count;
                OnPropertyChanged(nameof(NumberOfShleves));
            }
        }
        public ObservableCollection<ShelfViewModel> Shelves { get;}
        public CabinetViewModel(Cabinet cabinet)
        {
            Shelves = new ObservableCollection<ShelfViewModel>();
            shelfrepositoryService = new DBShelfService();
            _cabinet = cabinet;
            //_cabinet.Shelves = shelfrepositoryService.GetAllItems().Where(S => S.Cabinet_ID == CabinetID).ToList();
            UpdateShelves();
            NumberOfShleves = Shelves.Count;
        }

        private void UpdateShelves()
        {
            var shelves = shelfrepositoryService.GetAllItems().Where(S => S.Cabinet_ID == CabinetID).ToList();
            foreach (var item in shelves)
            {
                Shelves.Add(new ShelfViewModel(item));
            }
        }
    }
}
