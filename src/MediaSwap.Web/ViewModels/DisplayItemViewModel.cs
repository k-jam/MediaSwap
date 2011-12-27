using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaSwap.Core.Models;
namespace MediaSwap.Web.ViewModels
{   
    public class DisplayItemViewModel
    {

        
        public int ItemId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public ItemType Type
        {
            get;
            set;
        }
    }
}