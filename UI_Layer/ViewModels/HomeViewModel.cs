using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.Services;
using System.Windows.Input;
using UI_Layer.Stores;
using System.Diagnostics;

namespace UI_Layer.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        public ICommand NavigateCategoriesCommand { get; }
        public HomeViewModel(NavigationStore _navigationStore)
        {
            //_navigationStore.AddViewModel(nameof(HomeViewModel), this);
             NavigateCategoriesCommand = new NavigationCommand<CategoriesViewModel>(new NavigationService<CategoriesViewModel>(_navigationStore, () => 
             new CategoriesViewModel(_navigationStore, new DBCategoryService(), new BooksViewModel(new DBBookService(), new DBShelfService(), new DBCabinetService(), new DBCopyService()), new Messanger())));
        }
        ~HomeViewModel()
        {
            Debug.WriteLine("I'm Gone HomeViewModel");
        }
    }
}
