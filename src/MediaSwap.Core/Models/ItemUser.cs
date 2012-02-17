using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MediaSwap.Core.Models
{
    public class ItemUser
    {
        [Key]
        [Column("Item_ItemId", Order = 0)]
        public int ItemId { get; set; }

        [Key]
        [Column("User_UserId", Order = 1)]
        public int UserId { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
