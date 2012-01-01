using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaSwap.Core.Models;
using MediaSwap.Core.Services;
using System.Web.Security;
namespace MediaSwap.Web.Util
{
    public static class UserExtension
    {
        public static User ConvertToSiteUser(this System.Security.Principal.IIdentity user)
        {
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (!string.IsNullOrEmpty(ticket.UserData))
                {
                    var userId = Convert.ToInt32(ticket.UserData);
                    IUserService IUserService = new UserService();
                    var mediaSwapUser  =IUserService.GetUser(userId);
                    if (user != null)
                    {
                        return mediaSwapUser;
                    }
                    
                }
                
            }
            return new User();
        }
    }
}