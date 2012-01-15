using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Models;
namespace MediaSwap.Core.Services
{
    public interface IUserItemService
    {
        void Add(int itemId, int userId);
        IEnumerable<Item> GetItems(int userId);
        IEnumerable<Item> GetItems(int userId, string itemTypeName);
    }
}
