using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data;
using DataAnnotationsExtensions;
using System.Web.Mvc;
namespace MediaSwap.Core.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Token { get; set; }
        
       [Required]

        [Display(Name="User Name")]
        public string UserName { get; set; }
        
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name="Last Name")]
        public string LastName { get; set; }
 
       
        public string Email { get; set; }


        public ICollection<Item> Items { get; set; }

    }
}
