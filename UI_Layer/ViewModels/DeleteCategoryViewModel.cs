using UI_Layer.Commands;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using System.Linq;
using System.Windows.Input;
using UI_Layer.Stores;

namespace UI_Layer.ViewModels
{
    public class DeleteCategoryViewModel : BaseViewModel
    {
        private readonly Messanger messanger;
        private readonly IRepositoryService<Category> categoryService;
        private readonly UpdateBookViewModel updateBook;
        private readonly UpdateCategoryViewModel updateCategory;

        public DeleteCategoryViewModel(IRepositoryService<Category> categoryService, UpdateBookViewModel updateBook, UpdateCategoryViewModel updateCategory, Messanger messanger)
        {
            this.categoryService = categoryService;
            this.updateBook = updateBook;
            this.updateCategory = updateCategory;
            this.messanger = messanger;
            messanger.SelectedCategoryChanged += UpdateCategoryViewModel_SelectedCategoryChanged;
        }

        private void UpdateCategoryViewModel_SelectedCategoryChanged()
        {
            DeleteValidator();
        }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool Error { get => !string.IsNullOrEmpty(ErrorMessage); }

        #region Commands
        public ICommand DeleteCategoryCommand { get => new UpdateRelayCommand(DeleteCategory, () => !Error && updateCategory.SelectedCategory is not null); }
        #endregion
        private void DeleteCategory()
        {
            categoryService.DeleteItem(updateCategory.SelectedCategory.CategoryID);
            var TempCategory = updateCategory.SelectedCategory;
            if (updateCategory.Categories.Count > 0)
            {
                updateCategory.SelectedCategory = updateCategory.Categories[updateCategory.Categories.Count - 1];
            }
            updateCategory.Categories.Remove(TempCategory);
            Message = "Category Deleted successfully!";
            OnPropertyChanged(nameof(ShowMessage));
        }

        #region Properties
        #endregion
        #region Validators
        private void DeleteValidator()
        {
            ErrorMessage = "";
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(Error));
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            if (updateCategory.SelectedCategory is not null)
            {
                if (updateBook.Books.Any(B => B.CategoryID == updateCategory.SelectedCategory.CategoryID))
                {
                    ErrorMessage = $"Cannot delete Category {updateCategory.SelectedCategory.CategoryName} because it has books in it!";
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(Error));
                }
            }
            else
            {
                ErrorMessage = $"No more categories!";
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(Error));
            }
        }
        #endregion
    }
}
