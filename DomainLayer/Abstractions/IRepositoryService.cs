using System.Collections.Generic;

namespace DomainLayer.Abstractions
{
    public interface IRepositoryService<Item> 
    {
        public IEnumerable<Item> GetAllItems();
        public Item GetById(int id);
        public Item GetByName(string name);
        public int GetItemIDByName(string name);
        public int GetItemIDByName(string name, int ID);
        public Item GetItemByNameAndParentID(string name, int ID);
        public Item GetByUniqueID(string uniqueId);
        public void InsertItem(Item item);
        public void UpdateItem(Item item);
        public void UpdateItem(int id, string DBProperty, object value);
        public void DeleteItem(int id);
    }
}
