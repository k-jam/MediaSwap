using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MediaSwap.Core.Models
{
    public class MediaType
    {
        [Key]
        public int MediaTypeId { get; set; }
        public string MediaTypeName { get; set; }
        public int PlatformTypeId { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
