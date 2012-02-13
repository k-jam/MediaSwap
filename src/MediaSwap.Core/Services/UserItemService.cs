using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Repositories;
using MediaSwap.Core.Models;
namespace MediaSwap.Core.Services
{
    public class UserItemService : BaseContext, IUserItemService
    {

        public void Add(int itemId, int userId)
        {
            using (var context = GetContext())
            {
                var user = context.User.Include("Items").FirstOrDefault(u => u.UserId == userId);
                var item = context.Item.Include("ItemType").FirstOrDefault(i => i.ItemId == itemId);
                user.Items.Add(item);
                context.SaveChanges();
            }
        }

        public IEnumerable<Item> GetItems(int userId, string itemTypeName)
        {

            using (var context = GetContext())
            {
                return context.User.Include("Items").Include("Items.ItemType").Include("Items.ItemType.Format").FirstOrDefault(u => u.UserId == userId).Items.Where(i=>i.ItemType.ItemTypeName == itemTypeName).ToList();
            }

        }
        public IEnumerable<Item> GetItems(int userId)
        {
            using (var context = GetContext())
            {
                return context.User.Include("Items").Include("Items.Queues").Include("Items.ItemType").Include("Items.ItemType.Format").FirstOrDefault(u => u.UserId == userId).Items.ToList();
            }
        }
    }
}
