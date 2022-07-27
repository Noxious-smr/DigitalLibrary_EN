using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using UI_Layer.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Layer.ViewModels
{
    public class UpdateViewModel : BaseViewModel
    {
        private readonly Messanger messanger = new Messanger();
        private readonly IRepositoryService<Cabinet> cabinetRepository = new DBCabinetService();
        private readonly IRepositoryService<Shelf> shelfRepository = new DBShelfService();
        private readonly IRepositoryService<Book> bookRepository = new DBBookService();
        private readonly IRepositoryService<Category> categoryRepository = new DBCategoryService();
        private readonly IRepositoryService<Copy> copyRepository = new DBCopyService();
        private readonly IRepositoryService<Employee> employeeRepository = new DBEmployeeService();
        public IEnumerable<Cabinet> Cabinets { get => cabinetRepository.GetAllItems(); }
        public IEnumerable<Shelf> Shelves { get => shelfRepository.GetAllItems(); }
        public IEnumerable<Book> Books { get => bookRepository.GetAllItems(); }
        public IEnumerable<Category> Categories { get => categoryRepository.GetAllItems(); }
        public IEnumerable<Copy> Copies { get => copyRepository.GetAllItems(); }
        public IEnumerable<Employee> Employees { get => employeeRepository.GetAllItems(); }

        #region UpdateViewModels
        public UpdateCabinetViewModel UpdateCabinet { get; set; }
        public UpdateShelfViewModel UpdateShelf { get; set; }
        public UpdateCategoryViewModel UpdateCategory { get; set; }
        public UpdateBookViewModel UpdateBook { get; set; }
        public UpdateCopyViewModel UpdateCopy { get; set; }
        public UpdateEmployeeViewModel UpdateEmployee { get; set; }
        #endregion

        #region Delete ViewModels
        public DeleteCabinetViewModel DeleteCabinet { get; set; }
        public DeleteShelfViewModel DeleteShelf { get; set; }
        public DeleteCategoryViewModel DeleteCategory { get; set; }
        public DeleteBookViewModel DeleteBook { get; set; }
        public DeleteCopyViewModel DeleteCopy { get; set; }
        #endregion
        public UpdateViewModel()
        {
            UpdateCabinet = new UpdateCabinetViewModel(new ErrorNotifier(), cabinetRepository, messanger);
            UpdateShelf = new UpdateShelfViewModel(new ErrorNotifier(), shelfRepository, messanger);
            UpdateCategory = new UpdateCategoryViewModel(new ErrorNotifier(), categoryRepository, messanger);
            UpdateBook = new UpdateBookViewModel(new ErrorNotifier(), bookRepository, UpdateCategory, UpdateCabinet, messanger);
            UpdateEmployee = new UpdateEmployeeViewModel(new ErrorNotifier(), employeeRepository, Employees);

            DeleteCabinet = new DeleteCabinetViewModel(UpdateCabinet, cabinetRepository,messanger);
            DeleteShelf = new DeleteShelfViewModel(shelfRepository, Books, UpdateShelf, messanger);
            DeleteCategory = new DeleteCategoryViewModel(categoryRepository, UpdateBook, UpdateCategory, messanger);
            DeleteBook = new DeleteBookViewModel(bookRepository, UpdateBook, messanger);
            UpdateCopy = new UpdateCopyViewModel(new ErrorNotifier(), copyRepository, UpdateBook, messanger);
            DeleteCopy = new DeleteCopyViewModel(copyRepository, UpdateCopy, messanger);
        }
    ~UpdateViewModel()
        {
            Debug.WriteLine("I'm gone Update ViewModel!!");
        }
    }
}
