﻿using System;
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
                var item = context.Item.Include("ItemType.Format").Include("Queues").Where(i => i.ItemId == itemId).FirstOrDefault();

                return item;                
            }   
        }

        public Models.Item SaveItem(Models.Item item)
        {
            using (var context = GetContext())
            {
                var existingItem = GetItem(item.ItemId);
                
                item.ItemType = context.ItemType.FirstOrDefault(it => it.ItemTypeId == item.ItemType.ItemTypeId);
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
                var item = context.Item.Include("Queues").First(e => e.ItemId == itemId);

                if (item.Queues.Any(e => e.QueueStatusValue == 1))
                {
                    throw new ApplicationException("Cannot delete a loaned item");
                }

                context.Item.Remove(item);
                context.SaveChanges() ;
            }
        }

        public IEnumerable<Models.Item> Search(string name, int? mediaTypeId = null)
        {
            IEnumerable<Models.Item> items;
            using (var context = GetContext())
            {
                if (name == "")
                {
                    items = context.Item.Include("ItemType.Format");
                }
                else
                {
                    items = context.Item.Include("ItemType.Format").Where(i => i.ItemName.ToUpper().Contains(name.ToUpper()));
                }
                if (mediaTypeId != null)
                {
                    items.Where(i => i.ItemType.ItemTypeId == mediaTypeId);
                }
                return items.ToList();
            }
        }

        public IEnumerable<Item> GetItems(int? itemTypeId = null, bool onlyAvailable = false)
        {
            using (var context = GetContext())
            {
                IQueryable<Item> items;

                items = from i in context.Item.Include("ItemType.Format")
                        select i;

                if (onlyAvailable)
                {
                    items = from i in items where !context.Queue.Any(q => q.ItemId == i.ItemId && q.ReturnDate != null)
                            select i;
                }

                

                return items.ToList();
            }
        }

        public Item GetItemWithOwner(int itemId)
        {
            using (var context = GetContext())
            {
                var items = from item in context.Item.Include("Users")
                            where item.ItemId == itemId
                            select item;

                return items.FirstOrDefault();
            }
        }
    }
}
