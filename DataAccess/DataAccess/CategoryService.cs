using System;
using System.Collections.Generic;
using DomainLayer.Models;
using DomainLayer.Abstractions;

namespace DataAccess.DataAccess
{
    public class CategoryService : IRepositoryService<Category>
    {
        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllItems()
        {
            List<Category> _categories = new List<Category>()
            {
                new Category(category_id: 1, category_name: "Programing", _imagePath: @"/Images/Categories/Programing.png"),
                new Category(category_id: 2, category_name: "Math", _imagePath: @"/Images/Categories/Math.png"),
                new Category(category_id: 3, category_name: "Physics", _imagePath: @"/Images/Categories/Physics_001.png"),
                new Category(category_id: 4, category_name: "Chemistry", _imagePath: @"/Images/Categories/Chemistry.png"),
                new Category(category_id: 5, category_name: "Biology", _imagePath: @"/Images/Categories/Biology.png"),
                new Category(category_id: 6, category_name: "Social", _imagePath: @"/Images/Categories/Social.jpg")
            };
            return _categories;
        }

        public Category GetById(int id)
        {
            return null;
        }

        public Category GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Category GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public Category GetItemByNameAndParentID(string name, int ID)
        {
            throw new NotImplementedException();
        }

        public int GetItemIDByName(string name)
        {
            throw new NotImplementedException();
        }

        public int GetItemIDByName(string name, int ID)
        {
            throw new NotImplementedException();
        }

        public void InsertItem(Category item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Category item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(int id, string DBProperty, object value)
        {
            throw new NotImplementedException();
        }
    }
}
