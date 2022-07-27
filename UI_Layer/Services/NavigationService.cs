using UI_Layer.Stores;
using UI_Layer.ViewModels;
using System;
using System.Diagnostics;

namespace UI_Layer.Services
{
    internal class NavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : BaseViewModel
    {
        private readonly NavigationStore navigationStore;
        private readonly Func<TViewModel> GetViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> getViewModel)
        {
            this.navigationStore = navigationStore;
            GetViewModel = getViewModel;
        }

        public void Navigate()
        {
            navigationStore.CurrentView = GetViewModel();
        }
        ~NavigationService()
        {
            Debug.WriteLine("I'm Gone Navigation Service");
        }
    }
}
