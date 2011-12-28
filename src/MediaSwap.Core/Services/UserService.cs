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
                return context.User.Include("Items").FirstOrDefault(u => u.UserId == userId);
            }
        }

        public Models.User GetUser(string email)
        {
            using (var context = GetContext())
            {
                return context.User.FirstOrDefault(u => u.Email == email);
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
        public void AddItem(int userId, int itemId)
        {
            using (var context = GetContext())
            {
                var user = context.User.Include("Items").FirstOrDefault(i => i.UserId == userId);
                
                user.Items.Add(context.Item.FirstOrDefault(i=>i.ItemId == itemId));
                context.SaveChanges();
                //s.Items.Remove;

            }
        }

        public void RemoveItem(int userId, int itemId)
        {
            using (var context = GetContext())
            {
                var user = context.User.Include("Items").FirstOrDefault(i => i.UserId == userId);
                user.Items.Remove(context.Item.FirstOrDefault(i=>i.ItemId == itemId));
                context.SaveChanges();
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
