using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MediaSwap.Core.Models
{
    public class Format
    {
        [Key]
        public int FormatTypeId { get; set; }
        [DisplayName("Format")]
        public string FormatTypeName { get; set; }
       
        
    }
}
