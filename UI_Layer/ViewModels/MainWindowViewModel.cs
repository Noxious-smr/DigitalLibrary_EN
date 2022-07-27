using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.Services;
using System.Windows.Input;
using UI_Layer.Stores;

namespace UI_Layer.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private NavigationStore _navigationStore;
        public BaseViewModel CurrentView
        {
            get { return _navigationStore.CurrentView; }

        }
        public ICommand NavigateHomeCommand { get => new NavigationCommand<HomeViewModel>(new NavigationService<HomeViewModel>
                                            (_navigationStore, () => new HomeViewModel(_navigationStore))); }
        public ICommand NavigateCategoriesCommand { get => new NavigationCommand<CategoriesViewModel>(new NavigationService<CategoriesViewModel>(_navigationStore, 
            () => new CategoriesViewModel(_navigationStore, new DBCategoryService(), new BooksViewModel(new DBBookService(),
            new DBShelfService(), new DBCabinetService(), new DBCopyService()), new Messanger()))); }
        public ICommand NavigateAddCommand { get => new NavigationCommand<AddViewModel>(new NavigationService<AddViewModel>
                                            (_navigationStore, () => new AddViewModel())); }
        public ICommand NavigateUpdateCommand { get => new NavigationCommand<UpdateViewModel>(new NavigationService<UpdateViewModel>
                                                (_navigationStore, () => new UpdateViewModel())); }
        public ICommand NavigateDashboardCommand { get => new NavigationCommand<DashboardViewModel>(new NavigationService<DashboardViewModel>
                                                    (_navigationStore, () => new DashboardViewModel(new DBEmployeeService(), 
                                                        new DBBorrowService(), new DBCopyService()))); }
        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            navigationStore.CurrentViewChanged += CurrentViewChanges;            
        }

        private void CurrentViewChanges()
        {
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
