using System;
using System.Web.Mvc;
using System.Web.Security;
using MediaSwap.Core.Models;
using MediaSwap.Core.Services;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

namespace MediaSwap.Web.Controllers
{
    public class UserController : Controller
    {
        private static OpenIdRelyingParty _openId = new OpenIdRelyingParty();
        IUserService IUserService;
        //
        // GET: /User/
        public UserController()
        {
            IUserService = new UserService();
        }
        public ActionResult Index(int id)
        {
            return View(IUserService.GetUser(id));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                IUserService.SaveUser(user);
            }
            return View(user);
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(IUserService.GetUser(id));
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                IUserService.SaveUser(user);
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult UserItems(int id)
        {

           var user = IUserService.GetUser(id);


           IItemService itemService = new ItemService();
           
           var allItems = itemService.GetItems();
           
           var editItems = new List<ViewModels.UserItemViewModel>();
           foreach (var item in allItems)
           {
               var addItem = new ViewModels.UserItemViewModel() {  Item = item  };
               if ( user.Items.FirstOrDefault(uitem => uitem.ItemId == addItem.Item.ItemId) != null)
               {
                   addItem.Owned = true;
               }
               editItems.Add(addItem);
           }

           return View(new ViewModels.UserItemEditViewModel() { User = user, Items = editItems });

        }
        [HttpPost]
        public ActionResult UserItems(int userId, List<int> owned )
        {
            owned = owned ?? new List<int>();
            var user = IUserService.GetUser(userId);
            var userItem = user.Items;
           foreach(var itemId in owned)
           {
                if(!user.Items.Any(uitem=>uitem.ItemId == itemId))
                {
                    IUserService.AddItem (user.UserId , itemId);
                }
           }

           foreach (var item in userItem)
           {
               if (!owned.Any(id => id == item.ItemId))
               {
                   IUserService.RemoveItem(userId, item.ItemId);
               }
           }
            return RedirectToAction("UserItems",new {@id = userId});
        }
        public string IsUserNameAvailable(string username)
        {
            var isAvailable = !IUserService.UserExists(username);
            return (isAvailable).ToString();
        }
 
    }
}
