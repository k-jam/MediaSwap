using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MediaSwap.Core.Models
{
    public class ItemType
    {
        [Key]
        public int ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public virtual Format Format { get; set; }
      

        public IEnumerable<Item> Items { get; set; }
    }
}
