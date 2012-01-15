using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Services
{
    public interface IItemService
    {
        Item GetItem(int itemId);
        Item SaveItem(Item item);
        bool EditItem(Item item);
        void DeleteItem(int itemId);

       
        IEnumerable<Item> Search(string name,int? mediaTypeId =null);

        IEnumerable<Item> GetItems(int? mediaTypeId = null, bool onlyAvailable = false);
        

    }
}
