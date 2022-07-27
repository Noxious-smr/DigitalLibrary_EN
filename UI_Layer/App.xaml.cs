using UI_Layer.Views;
using System.Windows;
using UI_Layer.ViewModels;
using System.Threading;
using UI_Layer.Stores;

namespace UI_Layer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
            NavigationStore navigationStore = new NavigationStore();
            HomeViewModel homeViewModel = new HomeViewModel(navigationStore);
            //CategoriesViewModel categoriesViewModel = new CategoriesViewModel(navigationStore);
            navigationStore.CurrentView = homeViewModel;


            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainWindowViewModel(navigationStore);
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
