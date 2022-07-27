using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using UI_Layer.Services;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace UI_Layer.ViewModels
{
    public class AddCategoryViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly IRepositoryService<Category> repositoryService = new DBCategoryService();
        private readonly ErrorNotifier errorNotifier;
        private readonly IFileService imageService;
        public bool HasErrors => errorNotifier.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public AddCategoryViewModel(ErrorNotifier _errorNotifier)
        {
            errorNotifier = _errorNotifier;
            errorNotifier.ErrorsChanged += ErrorNotifier_ErrorsChanged;
            Categories = new ObservableCollection<CategoryViewModel>();
            foreach (var item in repositoryService.GetAllItems())
            {
                Categories.Add(new CategoryViewModel(item));
            }
            imageService = new CategoryImageService();
        }

        private void ErrorNotifier_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanAdd));
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return errorNotifier.GetErrors(propertyName);
        }
        public bool CanAdd => !HasErrors;
        public ObservableCollection<CategoryViewModel> Categories { get; set; }
        public ICommand GetImagePathCommand { get => new FileServiceCommand(GetImage); }
        private void GetImage()
        {
            ImagePath = imageService.GetFile();
        }

        public ICommand AddCategoryCommand { get => new AddRelayCommand(InsertCategoryIntoDB, () => CanAdd); }
        private string category_name;
        public string Category_Name
        {
            get { return category_name; }
            set
            {
                category_name = value;
                CategoryValidator();
            }
        }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
        public void CategoryValidator()
        {
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            errorNotifier.ClearError(nameof(Category_Name));
            errorNotifier.ClearError(nameof(ImagePath));
            OnPropertyChanged(nameof(Category_Name));
            OnPropertyChanged(nameof(ImagePath));
            if (string.IsNullOrEmpty(Category_Name) || string.IsNullOrWhiteSpace(Category_Name) || Category_Name.Length <= 0)
            {
                errorNotifier.AddError(nameof(Category_Name), "Category name must not be empty, Thank you!");
                OnPropertyChanged(nameof(CanAdd));
            }
            if (string.IsNullOrEmpty(ImagePath) || string.IsNullOrWhiteSpace(ImagePath) || ImagePath.Length <= 0)
            {
                errorNotifier.AddError(nameof(ImagePath), "Image path must not be empty, Thank you!");
                OnPropertyChanged(nameof(CanAdd));
            }
        }
        
        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {  
                imagePath = value;
                CategoryValidator();
            }
        }

        public void InsertCategoryIntoDB(string parameter)
        {
            //parameter = Category_Name;
            repositoryService.InsertItem(new Category(0, Category_Name, imageService.CopyFile(ImagePath, "Categories")));
            Categories.Add(new CategoryViewModel(repositoryService.GetByName(Category_Name)));
            var categoryName = Category_Name;
            Category_Name = "";
            ImagePath = "";
            errorNotifier.ClearError(nameof(Category_Name));
            errorNotifier.ClearError(nameof(ImagePath));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(Category_Name)));
            ErrorNotifier_ErrorsChanged(this, new DataErrorsChangedEventArgs(nameof(ImagePath)));
            Message = $"Category {Category_Name} Added successfully!!";
            OnPropertyChanged(nameof(ShowMessage));
        }

        public void UpdateCategoryInDB(int ID)
        {
            repositoryService.UpdateItem(new Category(ID, Category_Name, ImagePath));
        }
        public void DeleteCategoryFromDB(int ID)
        {
            repositoryService.DeleteItem(ID);
        }
    }
}
