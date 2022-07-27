using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess
{
    public class CopyService : IRepositoryService<Copy>
    {
        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Copy> GetAllItems()
        {
            List<Copy> copies = new List<Copy>()
            {
                new Copy(1, Guid.NewGuid().ToString(), 1, false, "Good"),
                new Copy(2,Guid.NewGuid().ToString(), 1, true, "Not bad"),
                new Copy(3,Guid.NewGuid().ToString(), 2, true, "Acceptable"),
                new Copy(4,Guid.NewGuid().ToString(), 3, false, "Good"),
                new Copy(5,Guid.NewGuid().ToString(), 3, false, "Good"),
                new Copy(6,Guid.NewGuid().ToString(), 4, true, "Not bad"),
                new Copy(7,Guid.NewGuid().ToString(), 4, false, "Acceptable"),
                new Copy(8,Guid.NewGuid().ToString(), 5, true, "Good"),
                new Copy(9,Guid.NewGuid().ToString(), 5, false, "Good"),
                new Copy(10,Guid.NewGuid().ToString(), 1, false, "Not bad"),
                new Copy(11,Guid.NewGuid().ToString(), 4, true, "Acceptable"),
                new Copy(12,Guid.NewGuid().ToString(), 3, false, "Good"),
                new Copy(13,Guid.NewGuid().ToString(), 1, true, "Good"),
                new Copy(14,Guid.NewGuid().ToString(), 2, true, "Good"),
                new Copy(15,Guid.NewGuid().ToString(), 2, false, "Good"),
                new Copy(16,Guid.NewGuid().ToString(), 4, true, "Good"),
                new Copy(17,Guid.NewGuid().ToString(), 3, true, "Good"),
                new Copy(18,Guid.NewGuid().ToString(), 4, false, "Good"),
                new Copy(19,Guid.NewGuid().ToString(), 6, false, "Good"),
                new Copy(20,Guid.NewGuid().ToString(), 5, true, "Good"),
                new Copy(21,Guid.NewGuid().ToString(), 6, true, "Good"),
            };
            return copies;
        }

        public Copy GetById(int id)
        {
            return null;
        }

        public Copy GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Copy GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public Copy GetItemByNameAndParentID(string name, int ID)
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

        public void InsertItem(Copy item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Copy item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(int id, string DBProperty, object value)
        {
            throw new NotImplementedException();
        }
    }
}
