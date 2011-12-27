using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaSwap.Core.Models;
using MediaSwap.Core.Services;
namespace MediaSwap.Web.Controllers
{
    public class UserController : Controller
    {
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
