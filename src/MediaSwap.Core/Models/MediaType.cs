using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaSwap.Core.Models
{
    public class MediaType
    {
        public int MediaTypeId { get; set; }
        public string MediaTypeName { get; set; }
        public int PlatformTypeId { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
