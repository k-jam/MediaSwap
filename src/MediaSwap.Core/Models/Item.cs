﻿using System;
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

        [Display(Name="Item Name")]
        [Required]
        public string ItemName { get; set; }

        [Display(Name="Amazon Id")]
        public string AmazonId { get; set; }

        public virtual ItemType ItemType { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Queue> Queues { get; set; }
    }
}
