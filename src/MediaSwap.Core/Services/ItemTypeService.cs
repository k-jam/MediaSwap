using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Repositories;

namespace MediaSwap.Core.Services
{
    public class ItemTypeService : BaseContext, IItemTypeService
    {
        public IEnumerable<Models.ItemType> GetItemTypes()
        {
            using (var context = GetContext())
            {



                return context.ItemType.Include("Format").ToList();
            }
        }
        public IEnumerable<Models.ItemType> GetItemTypes(string itemType)
        {
            using (var context = GetContext())
            {
                return context.ItemType.Include("Format").Where(m => m.ItemTypeName == itemType).ToList();
            }

        }
        public IEnumerable<Models.ItemType> GetItemTypes(int itemTypeId)
        {
            using (var context = GetContext())
            {
                return context.ItemType.Include("Format").Where(m => m.ItemTypeId == itemTypeId).ToList();
            }
        }


        Models.ItemType IItemTypeService.GetItemTypes(int itemTypeId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Models.ItemType> IItemTypeService.GetItemTypes()
        {
            throw new NotImplementedException();
        }
    }
}
