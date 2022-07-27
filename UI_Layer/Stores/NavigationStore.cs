using UI_Layer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Layer.Stores
{
    internal class NavigationStore
    {
        public event Action CurrentViewChanged;

        private BaseViewModel previousView;
        public BaseViewModel PreviousView
        {
            get { return previousView; }
            set { previousView = value; }
        }

        private BaseViewModel currentView;
        public BaseViewModel CurrentView
        {
            get { return currentView; }
            set 
            { 
                if (PreviousView is CategoriesViewModel)
                {
                    ((CategoriesViewModel)PreviousView).Dispose();
                }
                PreviousView = value;
                currentView = value;
                OnCurrentViewChanged(); }
        }

        private void OnCurrentViewChanged()
        {
            CurrentViewChanged?.Invoke();
        }

        ~NavigationStore()
        {
            Debug.WriteLine("I'm gone Navigation Store");
        }
    }    
}
