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
        IEnumerable<Item> GetItems(int userId);
        IEnumerable<Item> Search(string name);

    }
}
