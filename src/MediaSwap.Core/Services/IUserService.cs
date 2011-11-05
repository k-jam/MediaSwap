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
        User SaveUser(User user);
    }
}
