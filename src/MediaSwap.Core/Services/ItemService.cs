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

        public IEnumerable<Item> GetItems(int? mediaTypeId = null, bool onlyAvailable = false)
        {
            using (var context = GetContext())
            {
                IQueryable<Item> items;

                items = from i in context.Item
                        select i;

                if (onlyAvailable)
                {
                    items = from i in items where !context.Queue.Any(q => q.ItemId == i.ItemId && q.ReturnDate != null)
                            select i;
                }

                if (mediaTypeId.HasValue)
                {
                    items = items.Where(i => i.MediaId == mediaTypeId);
                }

                return items.ToList();
            }
        }
    }
}
