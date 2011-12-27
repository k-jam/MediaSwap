using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Services
{
    public interface IUserService
    {
        User GetUser(int userId);
        User GetUser(string token);
        bool UserExists(string username);
        void AddItem(int userId, int itemId);
        void RemoveItem(int userId, int itemId);
        User SaveUser(User user);
    }
}
