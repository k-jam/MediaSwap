using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MediaSwap.Core.Models;
 
namespace MediaSwap.Web.ViewModels
{
    public class SearchItemViewModel
    {
        public IEnumerable<Item> ItemResults { get; set; } 
    }
}