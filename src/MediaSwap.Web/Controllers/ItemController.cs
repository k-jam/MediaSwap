using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaSwap.Core.Models;
using MediaSwap.Core.Services;

namespace MediaSwap.Web.Controllers
{
    public class ItemController : Controller
    {

        IItemService _itemService = new ItemService();

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
            _itemService.SaveItem(item);
            return RedirectToAction("Index");
        }
    }
}
