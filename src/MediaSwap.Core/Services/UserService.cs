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
            using (var context = GetContext())
            {
                return context.User.FirstOrDefault(u => u.Token == token);
            }
        }

        public Models.User SaveUser(Models.User user)
        {

            using (var context = GetContext())
            {
                var existingUser = context.User.FirstOrDefault(u => u.UserId == user.UserId);

                if (existingUser == null)
                {
                    context.User.Add(user);
                }
                else
                {
                    existingUser.UserName = user.UserName;
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.OpenIdUrl = user.OpenIdUrl;
                    existingUser.Token = user.Token;
                    existingUser.Gravatar = user.Gravatar;
                    existingUser.GeoLocation = user.GeoLocation;
                    existingUser.Email = user.Email;

                }

                context.SaveChanges();

                return user;
            }
        }

        public bool UserExists(string username)
        {
            using (var context = GetContext())
            {
                return context.User.Count(u => u.UserName == username) > 0;
            }
        }
    }
}
