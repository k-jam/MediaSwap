using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Repositories;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Services
{
    public class ItemService : BaseContext, IItemService
    {
        public Item GetItem(int itemId)
        {
            using (var context = GetContext())
            {
                var item = context.Item.Where(i => i.ItemId == itemId).FirstOrDefault();

                return item;                
            }   
        }

        public Models.Item SaveItem(Models.Item item)
        {
            using (var context = GetContext())
            {
                var existingItem = GetItem(item.ItemId);

                if (existingItem == null)
                {
                    context.Item.Add(item);
                }
                else
                {
                    context.Item.Attach(item);
                }

                context.SaveChanges();

                return item;
            }
        }

        public bool EditItem(Models.Item item)
        {
            using (var context = GetContext())
            {
                context.Entry(item).State = System.Data.EntityState.Modified;

                return context.SaveChanges() > 0;
            }
        }

        public void DeleteItem(int itemId)
        {
            using (var context = GetContext())
            {
                var item = context.Item.Find(itemId);

                context.Item.Remove(item);
                context.SaveChanges() ;
            }
        }

        public IEnumerable<Item> GetItems(int userId)
        {
            using (var context = GetContext())
            {
                var user = context.User.Where(e => e.UserId == userId);

                var items = context.Item.Where(e => e.Users == user);
                
                return items;
            }
        }

        public IEnumerable<Models.Item> Search(string name)
        {
            IEnumerable<Models.Item> items;
            using (var context = GetContext())
            {
                if (name == "")
                {
                    items = context.Item;
                }
                else
                {
                    items = context.Item.Where(i => i.ItemName.ToUpper().Contains(name.ToUpper()));
                }
                return items.ToList();
            }
        }


        
    }
}
