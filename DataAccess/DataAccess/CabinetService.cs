using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess
{
    public class CabinetService : IRepositoryService<Cabinet>
    {
        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cabinet> GetAllItems()
        {
            List<Cabinet> cabinets =  new List<Cabinet>()
            {
                new Cabinet(1, "CA_01"),
                new Cabinet(2, "CA_02"),
                new Cabinet(3, "CA_03"),
                new Cabinet(4, "CA_04"),
                new Cabinet(5, "CA_05"),
                new Cabinet(6, "CA_06"),
            };
            return cabinets;
        }

        public Cabinet GetById(int id)
        {
            return null;
        }

        public Cabinet GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Cabinet GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public Cabinet GetItemByNameAndParentID(string name, int ID)
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

        public void InsertItem(Cabinet item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Cabinet item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(int id, string DBProperty, object value)
        {
            throw new NotImplementedException();
        }
    }
}
