using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaSwap.Core.Models;
using System.Web.Mvc;

namespace MediaSwap.Web.ViewModels
{
    public class AddItemViewModel : Item
    {

        public string ItemTypeName { get; set; }
        public IEnumerable<ItemType> ItemTypes { get; set; }
        public string Status { get; set; }
      
    }
}