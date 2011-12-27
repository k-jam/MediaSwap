using System;
using System.Web.Mvc;
using System.Web.Security;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

namespace MediaSwap.Web.Controllers
{
    public class UserController : Controller
    {
        private static OpenIdRelyingParty _openId = new OpenIdRelyingParty();

        public ActionResult Index()
        {
            return View();
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
                            var email = sreg.Email;
                            // Do something with the values here, like store them in your database for this user.
                        }

                        FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);
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
    }
}
