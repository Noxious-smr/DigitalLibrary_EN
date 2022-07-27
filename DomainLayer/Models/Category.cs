using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Category
    {
        private int _category_id;
        public int Category_ID
        {
            get { return _category_id; }
            set { _category_id = value; }
        }

        private string category_name;
        public string Category_Name
        {
            get { return category_name; }
            set { category_name = value; }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public Category(int category_id, string category_name, string _imagePath)
        {
            Category_ID = category_id;
            Category_Name = category_name;
            imagePath = _imagePath;
        }
    }
}
