using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MediaSwap.Core.Models;
 
namespace MediaSwap.Web.ViewModels
{
    public class SearchItemViewModel
    {
        public class ItemResult
        {
            public Item Item
            {
                get;
                set;
            }
            public QueueStatus? Status
            {
                get;
                set;
            }
        }

        public IEnumerable<ItemResult> ItemResults { get; set; } 
    }
}