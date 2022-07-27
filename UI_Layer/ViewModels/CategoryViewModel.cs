using DomainLayer.Models;
using UI_Layer.Services;

namespace UI_Layer.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private Category _category;
        private readonly IFileService _imageService;
        public int CategoryID
        {
            get
            {
                return _category.Category_ID;
            }
            set
            {
                _category.Category_ID = value;
                OnPropertyChanged(nameof(CategoryID));
            }
        }
        public string CategoryName
        {
            get
            {
                return _category.Category_Name;
            }
            set
            {
                _category.Category_Name = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }
        public string ImagePath
        {
            get { return _imageService.FileRead(_category.ImagePath); }
            set { _category.ImagePath = value; }
        }

        public CategoryViewModel(Category category)
        {
            _imageService = new CategoryImageService();
            _category = category;            
        }
    }
}
