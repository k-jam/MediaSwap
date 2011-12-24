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
 
        public string IsUserNameAvailable(string username)
        {
            var isAvailable = !IUserService.UserExists(username);
            return (isAvailable).ToString();
        }
 
    }
}
