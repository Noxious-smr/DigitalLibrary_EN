using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI_Layer.Commands
{
    public class ServiceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> action;

        public ServiceCommand(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
