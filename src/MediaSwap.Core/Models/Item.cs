using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MediaSwap.Core.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int AmazonId { get; set; }
        public int MediaId { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }
}
