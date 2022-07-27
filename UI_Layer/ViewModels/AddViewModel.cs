using System.Diagnostics;
using UI_Layer.ErrorNotification;

namespace UI_Layer.ViewModels
{
    public class AddViewModel : BaseViewModel
    {
        public AddCabinetViewModel AddCabinet { get; set; }
        public AddShelfViewModel AddShelf { get; set; }
        public AddCategoryViewModel AddCategory { get; set; }

        public AddBookViewModel AddBook { get; set; }
        public AddCopyViewModel AddCopy { get; set; }
        public AddEmployeeViewModel AddEmployee { get; set; }
        public AddViewModel()
        {
            AddCabinet = new AddCabinetViewModel(new ErrorNotifier());
            AddShelf = new AddShelfViewModel(new ErrorNotifier());
            AddCategory = new AddCategoryViewModel(new ErrorNotifier());
            AddBook = new AddBookViewModel(new ErrorNotifier());
            AddCopy = new AddCopyViewModel(new ErrorNotifier());
            AddEmployee = new AddEmployeeViewModel(new ErrorNotifier());
        }
        ~AddViewModel()
        {
            Debug.WriteLine("I'm gone AddViewModel!!");
        }
    }
}
