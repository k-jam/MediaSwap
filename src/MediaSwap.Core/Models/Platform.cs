using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MediaSwap.Core.Models
{
    public class Platform
    {
        [Key]
        public int PlatformTypeId { get; set; }
        public string PlatformTypeName { get; set; }

    }
}
