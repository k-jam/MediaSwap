using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

using MediaSwap.Core.Models;
using MediaSwap.Core.Services;
using System.Web.Routing;
using System.Web;
using MediaSwap.Web.Models;
namespace MediaSwap.Web.Controllers
{
    
    public class ProfileController : Controller
    {
        private static OpenIdRelyingParty _openId = new OpenIdRelyingParty();
        IProfileService IUserService;

        public ProfileController()
        {
            IUserService = new ProfileService();
        }
        [Authorize]
        public ActionResult Index()
        {
            return View(IUserService.GetUser((HttpContext.User.Identity as MediaSwapIdentity).Id));
        }
        [HttpGet]
        
        public ActionResult Create()
        {
            return View(new User() { });
        }

        [HttpPost]
        
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {


                Session["SignupProfile"] = user;
            }

           return RedirectToAction("Authenticate");
        }
        
        public ActionResult Login()
        {
        
                return View();
           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [ValidateInput(false)]
        public ActionResult Authenticate(string returnUrl)
        {
            string googleOpenId = "https://www.google.com/accounts/o8/id";
            var response = _openId.GetResponse();

            if (response == null)
            {
                Identifier id;
                if (Identifier.TryParse(googleOpenId, out id))
                {
                    try
                    {
                        var request = _openId.CreateRequest(googleOpenId);

                        request.AddExtension(new ClaimsRequest()
                        {
                            Email = DemandLevel.Require
                        });

                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        ViewData["Message"] = ex.Message;
                        return View("Login");
                    }
                }
                else
                {
                    ViewData["Message"] = "Invalid identifier";
                    return View("Login");
                }
            }
            else
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;

                        var sreg = response.GetExtension<ClaimsResponse>();
                        
                        if (sreg != null)
                        {
                            User user = Session["SignupProfile"] as User;
                            if (user != null)
                            {
                                user.Email = sreg.Email;
                                user.Token = sreg.Email;

                                IUserService.SaveUser(user);
                            }
                            else
                            {
                                user = IUserService.GetUser(sreg.Email);
                                if (user == null)
                                {
                                    return RedirectToAction("Create");
                                }
                            }
                            // Do something with the values here, like store them in your database for this user.
                            var userData = string.Format("{0};{1};{2}", user.UserId, user.UserName, user.Email);
                            var ticket = new FormsAuthenticationTicket(
                                           2, // magic number used by FormsAuth
                                           response.ClaimedIdentifier, // username
                                           DateTime.Now,
                                           DateTime.Now.AddMinutes(30),
                                           false, // "remember me"
                                           userData);
                            
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                            Response.SetCookie(cookie);
                            Response.Redirect(Request.QueryString["ReturnUrl"] ?? "/");
      
                           
                        }
                        
                       
                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Item");
                        }
                    case AuthenticationStatus.Canceled:
                        ViewData["Message"] = "Canceled at provider";
                        return View("Login");
                    case AuthenticationStatus.Failed:
                        ViewData["Message"] = response.Exception.Message;
                        return View("Login");
                }
            }

            return new EmptyResult();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {

            return View(IUserService.GetUser(MediaSwap.Web.Models.MediaSwapIdentity.Current.Id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(User user)
        {
            user.UserId = MediaSwap.Web.Models.MediaSwapIdentity.Current.Id;
            if (ModelState.IsValid)
            {
                IUserService.SaveUser(user);
            }
            return View("Index",user);
        }

        
      
        public string IsUserNameAvailable(string username)
        {
            var isAvailable = !IUserService.UserExists(username);
            return (isAvailable).ToString();
        }
 
    }
}
