using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaSwap.Core.Models
{
    public class User
    { 
        public int UserId { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gravatar { get; set; }
        public string GeoLocation { get; set; }
        public IEnumerable<Item> Items { get; set; }

    }
}
