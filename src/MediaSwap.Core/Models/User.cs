using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
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
 
        [Required]
        public string Email { get; set; }

        public string OpenIdUrl { get; set; }

        public string Gravatar { get; set; }
        
        [Required]
        [Display(Name="Geo Loocation")]
        public string GeoLocation { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}
