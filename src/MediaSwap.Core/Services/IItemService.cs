using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Services
{
    public interface IItemService
    {
        IEnumerable<Item> GetItem(int itemId);
        Item SaveItem(Item item);
        IEnumerable<Item> Search(string name);

    }
}
