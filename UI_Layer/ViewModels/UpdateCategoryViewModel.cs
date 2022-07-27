using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using UI_Layer.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Reflection;
using System.IO;
using System.Linq;
using UI_Layer.Stores;

namespace UI_Layer.ViewModels
{
    public class UpdateCategoryViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly Messanger messanger;
        //public event Action<CategoryViewModel> SelectedCategoryChanged;
        //public event Action<int> CategoryUpdated;
        private readonly IRepositoryService<Category> repositoryService;
        private readonly ErrorNotifier errorNotifier;
        private readonly IFileService imageService;
        private readonly Dictionary<string, string> categoryNames = new Dictionary<string, string>();
        
        public ObservableCollection<CategoryViewModel> Categories { get; } = new ObservableCollection<CategoryViewModel>();

        public UpdateCategoryViewModel(ErrorNotifier errorNotifier, IRepositoryService<Category> repositoryService, Messanger messanger)
        {
            this.errorNotifier = errorNotifier;
            this.repositoryService = repositoryService;
            this.messanger = messanger;
            imageService = new CategoryImageService();
            List<string> currentImages = new List<string>();
            foreach (var item in repositoryService.GetAllItems())
            {
                Categories.Add(new CategoryViewModel(item));
                currentImages.Add(Environment.CurrentDirectory + @"\Books\Categories\" + item.ImagePath);
            }
            categoryNames.Add("Category_Name", "CategoryName");
            categoryNames.Add("Image_Path", "ImagePath");
            List<string> imagesList = new List<string>();
            if (Directory.Exists(Environment.CurrentDirectory + @"\Books\Categories"))
            {
                imagesList.AddRange(Directory.GetFiles(Environment.CurrentDirectory + @"\Books\Categories"));
            } 
            List<string> filesToDelete = new List<string>();
            foreach (var item in imagesList)
            {
                if (!currentImages.Contains(item))
                {
                    filesToDelete.Add(item);
                }
            }
            imageService.DeleteFiles(filesToDelete);
            filesToDelete.Clear();
            SelectedCategory = Categories.Count > 0 ? Categories.First() : null;
            errorNotifier.ClearError(nameof(CategoryName));
        }

        public bool HasErrors => errorNotifier.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void ErrorNotifier_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanUpdate));
        }
        public ICommand GetImagePathCommand { get => new FileServiceCommand(GetImage); }
        private void GetImage()
        {
            ImagePath = imageService.GetFile();
        }

        public ICommand UpdateNameCommand { get => new UpdateRelayCommand(UpdateInToDB, () => CanUpdateCategoryName && SelectedCategory is not null); }
        public ICommand UpdateImagePathCommand { get => new UpdateRelayCommand(UpdateInToDB, () => CanUpdateImagePath && SelectedCategory is not null); }

        private void UpdateInToDB(string parameter)
        {
            int CategoryID = SelectedCategory.CategoryID;
            int CategoryIndex = Categories.IndexOf(SelectedCategory);
            if (parameter.Equals("Image_Path"))
            {
                ImagePath = imageService.CopyFile(ImagePath, "Categories");
            }
            PropertyInfo propertyInfo = GetType().GetProperty(categoryNames[parameter]);
            repositoryService.UpdateItem(SelectedCategory.CategoryID, parameter, propertyInfo.GetValue(this));
            UpdateCategory(CategoryID, CategoryIndex, parameter);
        }

        private void UpdateCategory(int CategoryID, int CategoryIndex, string propertyName)
        {
            Categories.Insert(CategoryIndex, new CategoryViewModel(repositoryService.GetById(CategoryID)));


            //SelectedBook = Books.Where(B => B.BookID == BookID).First();
            //Books.Remove(Books.Where(b => b.BookID == BookID).First());
            SelectedCategory = Categories.ElementAt(CategoryIndex);
            OnPropertyChanged(nameof(SelectedCategory));
            //CategoryUpdated?.Invoke(CategoryIndex);
            messanger.WhenCategoryUpdated(CategoryIndex);
            Categories.RemoveAt(CategoryIndex + 1);
            //errorNotifier.ClearError(nameof(CategoryName));
            //ValidateAll();
            Message = $"Category {SelectedCategory.CategoryName} has been updated successfully!";
            OnPropertyChanged(nameof(ShowMessage));

        }
        public IEnumerable GetErrors(string propertyName)
        {
            return errorNotifier.GetErrors(propertyName);
        }
        public bool CanUpdate => !HasErrors;
        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
        
        private CategoryViewModel selectedCategory;
        public CategoryViewModel SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                if (value is not null)
                {
                    selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    messanger.WhenSelectedCategoryChanged();
                    //CategoryName = value.CategoryName;
                    Message = string.Empty;
                    //OnPropertyChanged(nameof(CategoryName));
                    OnPropertyChanged(nameof(ShowMessage)); 
                }
                else
                {
                    selectedCategory = null;
                    CategoryName = string.Empty;
                    ImagePath = string.Empty;
                    OnPropertyChanged(nameof(CategoryName));
                    OnPropertyChanged(nameof(ImagePath));
                    errorNotifier.ClearError(nameof(CategoryName));
                    errorNotifier.ClearError(nameof(ImagePath));
                }
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                ImagePathValidator();                
            }
        }

        private bool canUpdateImagePath = false;
        public bool CanUpdateImagePath
        {
            get
            {
                return canUpdateImagePath;
            }
            set
            {
                canUpdateImagePath = value;
                OnPropertyChanged(nameof(CanUpdateImagePath));
            }
        }
        
        private string categoryName;
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                CategoryNameValidator();
            }
        }

        private bool canUpdateCategoryName = false;
        public bool CanUpdateCategoryName
        {
            get
            {
                return canUpdateCategoryName;
            }
            set
            {
                canUpdateCategoryName = value;
                OnPropertyChanged(nameof(CanUpdateCategoryName));
            }
        }

        public void CategoryNameValidator()
        {
            canUpdateCategoryName = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            errorNotifier.ClearError(nameof(CategoryName));
            OnPropertyChanged(nameof(CategoryName));
            if (SelectedCategory is not null)
            {
                if (string.IsNullOrEmpty(CategoryName) || string.IsNullOrWhiteSpace(CategoryName) || CategoryName.Length <= 0)
                {
                    errorNotifier.AddError(nameof(CategoryName), "Category name must not be empty, Thank you!");
                    CanUpdateCategoryName = false;
                }
                if (CategoryName == SelectedCategory.CategoryName)
                {
                    errorNotifier.AddError(nameof(CategoryName), "Why repeating the same name ??");
                    CanUpdateCategoryName = false;
                }
            }
            else
            {                
                CanUpdateCategoryName = false;
            }
        }

        public void ImagePathValidator()
        {
            canUpdateImagePath = true;
            Message = "";
            OnPropertyChanged(nameof(ShowMessage));
            errorNotifier.ClearError(nameof(ImagePath));
            OnPropertyChanged(nameof(ImagePath));
            if (SelectedCategory is not null)
            {
                if (string.IsNullOrEmpty(ImagePath) || string.IsNullOrWhiteSpace(ImagePath) || ImagePath.Length <= 0)
                {
                    errorNotifier.AddError(nameof(ImagePath), "Image path must not be empty, Thank you!");
                    CanUpdateImagePath = false;
                } 
            }
            else
            {
                errorNotifier.AddError(nameof(ImagePath), "No more categories!");
                CanUpdateCategoryName = false;
            }
        }

    }
}
