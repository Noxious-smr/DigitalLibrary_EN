using UI_Layer.ViewModels;
using System;

namespace UI_Layer.Stores
{
    public class Messanger
    {
        public event Action SelectedCabinetChanged;
        public event Action SelectedShelfChanged;
        public event Action SelectedCabinetHasNoShelves;
        public event Action SelectedCopyChanged;
        public event Action CopyDeleted;
        public event Action BookUpdated;
        public event Action BookDeleted;
        public event Action<int> CategoryUpdated;
        public event Action SelectedCategoryChanged;
        public event Action SelectedBookChanged;
        public event Action DashboardBookReturned;
        public event Action<BookViewModel> CategoriesBookSelected;

        public void WhenDashboardBookReturned()
        {
            DashboardBookReturned?.Invoke();
        }
        public void WhenSelectedCabinetChanged()
        {
            SelectedCabinetChanged?.Invoke();
        }

        public void WhenSelectedCabinetHasNoShelves()
        {
            SelectedCabinetHasNoShelves?.Invoke();
        }
        public void WhenSelectedShelfChanged()
        {
            SelectedShelfChanged?.Invoke();
        }
        public void WhenCopyDeleted()
        {
            CopyDeleted?.Invoke();
        }
        public void WhenSelectedCopyChanged()
        {
            SelectedCopyChanged?.Invoke();
        }
        public void WhenBookUpdated()
        {
            BookUpdated?.Invoke();
        }

        public void WhenBookDeleted()
        {
            BookDeleted?.Invoke();
        }
        public void WhenCategoryUpdated(int index)
        {
            CategoryUpdated?.Invoke(index);
        }
        public void WhenSelectedCategoryChanged()
        {
            SelectedCategoryChanged?.Invoke();
        }
        public void WhenSelectedBookChanged()
        {
            SelectedBookChanged?.Invoke();
        }

        public void WhenCategoriesBookSelected(BookViewModel bookViewModel)
        {
            CategoriesBookSelected?.Invoke(bookViewModel);
        }
    }
}
