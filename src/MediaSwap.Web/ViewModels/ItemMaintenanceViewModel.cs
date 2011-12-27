using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaSwap.Core.Models;
using MediaSwap.Web.Models;
namespace MediaSwap.Web.ViewModels
{
    public class ItemMaintenanceViewModel
       
    {
        public IEnumerable<ItemType> ItemTypes
        {
            get;
            set;
        }
        public IEnumerable<DisplayItemViewModel> Items
        {
            get;
            set;
        }
           
    }
}