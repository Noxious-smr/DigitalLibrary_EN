using UI_Layer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Layer.Services
{
    internal interface INavigationService<TViewModel> where TViewModel : BaseViewModel
    {
        public void Navigate();
    }
}
