using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
namespace MediaSwap.Web.Models
{
    public class MediaSwapIdentity : IIdentity
    {
         private System.Web.Security.FormsAuthenticationTicket ticket;

         public MediaSwapIdentity(System.Web.Security.FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        public string AuthenticationType
        {
            get { return "MediaSwap"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get {
                return ticket.Name;
            }
        }

        public int Id
        {
            get
            {
                return Convert.ToInt32(ticket.UserData.Split(';')[0]);
            }
        }
        public string FriendlyName
        {
            get
            {
                return ticket.UserData.Split(';')[1];
            }
        }
        public string EmailAddress
        {
            get
            {
                return ticket.UserData.Split(';')[2];
            }
        }

        public static MediaSwapIdentity Current
        {
            get
            {
                return HttpContext.Current.User.Identity as MediaSwapIdentity;
            }
        }
    }
}