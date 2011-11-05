using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaSwap.Core.Models;

namespace MediaSwap.Web.Controllers
{
    public class ItemController : Controller
    {
        //
        // GET: /Item/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItem(Item item)
        {
            return RedirectToAction("Index");
        }
    }
}
