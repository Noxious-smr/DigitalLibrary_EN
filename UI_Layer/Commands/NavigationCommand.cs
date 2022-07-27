using UI_Layer.Services;
using UI_Layer.Stores;
using UI_Layer.ViewModels;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace UI_Layer.Commands
{
    internal class NavigationCommand<TViewModel> : ICommand where TViewModel : BaseViewModel
    {
        private readonly INavigationService<TViewModel> navigationService;

        public NavigationCommand(INavigationService<TViewModel> navigationService)
        {
            this.navigationService = navigationService;
        }

        public event EventHandler CanExecuteChanged;
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}
        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            navigationService.Navigate();
        }
        ~NavigationCommand()
        {
            Debug.WriteLine("I'm gone Navigation Command");
        }
    }
}
