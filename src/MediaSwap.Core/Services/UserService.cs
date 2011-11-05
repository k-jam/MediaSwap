using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Repositories;

namespace MediaSwap.Core.Services
{
    public class UserService : BaseContext, IUserService
    {
        public Models.User GetUser(int userId)
        {
            using (var context = GetContext())
            {
                return context.User.FirstOrDefault(u => u.UserId == userId);
            }
        }

        public Models.User GetUser(string token)
        {
            throw new NotImplementedException();
        }

        public Models.User SaveUser(Models.User user)
        {
            throw new NotImplementedException();
        }
    }
}
