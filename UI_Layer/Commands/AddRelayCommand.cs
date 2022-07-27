using UI_Layer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI_Layer.Commands
{
    public class AddRelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add 
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        private readonly Action<string> action;
        private readonly Func<bool> func;
        private readonly Action<object> borrowAction;
        public AddRelayCommand(Action<object> borrowAction, Func<bool> func)
        {
            this.borrowAction = borrowAction;
            this.func = func;
        }
        public AddRelayCommand(Action<string> action, Func<bool> func)
        {
            this.action = action;
            this.func = func;
        }
        public bool CanExecute(object parameter)
        {
            if (action is null)
            {
                return func();
            }
            else 
            {
                return func() && !string.IsNullOrEmpty(parameter.ToString());
            }
            
        }

        public void Execute(object parameter)
        {
            if (action is null)
            {
                borrowAction(parameter);
            }
            else
            {
                action(parameter.ToString());                
            }
            
        }
    }
}
