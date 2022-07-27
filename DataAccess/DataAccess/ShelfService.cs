using DomainLayer.Models;
using DomainLayer.Abstractions;
using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess
{
    public class ShelfService : IRepositoryService<Shelf>
    {
        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shelf> GetAllItems()
        {
            List<Shelf> shelves = new List<Shelf>()
            {
                new Shelf(1, 1, "001"),
                new Shelf(2, 1, "002"),
                new Shelf(3, 1, "003"),
                new Shelf(4, 1, "004"),
                new Shelf(5, 2, "001"),
                new Shelf(6, 2, "002"),
                new Shelf(7, 2, "003"),
                new Shelf(8, 2, "004"),
                new Shelf(9, 3, "001"),
                new Shelf(10, 3, "002"),
                new Shelf(11, 3, "003"),
                new Shelf(12, 3, "003"),
                new Shelf(13, 4, "001"),
                new Shelf(14, 4, "002"),
                new Shelf(15, 4, "003"),
                new Shelf(16, 4, "004"),
                new Shelf(17, 5, "001"),
                new Shelf(18, 5, "002"),
                new Shelf(19, 5, "003"),
                new Shelf(20, 5, "004"),
                new Shelf(21, 6, "001"),
                new Shelf(22, 6, "002"),
                new Shelf(23, 6, "003"),
                new Shelf(24, 6, "004"),
            };
            return shelves;
        }

        public Shelf GetById(int id)
        {
            return null;
        }

        public Shelf GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Shelf GetByUniqueID(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public Shelf GetItemByNameAndParentID(string name, int ID)
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

        public void InsertItem(Shelf item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Shelf item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(int id, string DBProperty, object value)
        {
            throw new NotImplementedException();
        }
    }
}
