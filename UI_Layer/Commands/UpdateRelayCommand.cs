using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI_Layer.Commands
{
    public class UpdateRelayCommand : ICommand
    {
        private readonly Action action;
        private readonly Action<string> action2;
        private readonly Func<bool> func;
        private readonly Func<string,bool> func2;
        public UpdateRelayCommand(Action action, Func<bool> func)
        {
            this.action = action;
            this.func = func;
        }

        public UpdateRelayCommand(Action<string> action2, Func<bool> func)
        {
            this.action2 = action2;
            this.func = func;
        }
        public UpdateRelayCommand(Action<string> action2, Func<string,bool> func)
        {
            this.action2 = action2;
            this.func2 = func;
        }
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

        public bool CanExecute(object parameter)
        {
                if (func is not null)
                {
                    return func() && !string.IsNullOrEmpty(parameter.ToString());
                }
                else
                {
                    return func2(parameter.ToString()) && !string.IsNullOrEmpty(parameter.ToString());
                } 
        }

        public void Execute(object parameter)
        {
            if (action is not null)
            {
                action();
            }
            else
            {
                action2(parameter.ToString());
            }
        }
    }
}
