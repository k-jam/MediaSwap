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

        public Models.ItemType GetItemTypes(int itemTypeId)
        {
            using (var context = GetContext())
            {
                return context.ItemType.FirstOrDefault(m => m.ItemTypeId == itemTypeId);
            }
        }
    }
}
