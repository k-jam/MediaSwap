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
        public ActionResult Items()
        {
            var items = _itemService.GetItems(1);
            return View(items);
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

        [HttpGet]
        public ActionResult EditItem(int id)
        {
            var item = _itemService.GetItem(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult EditItem(Item item)
        {
            _itemService.EditItem(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteItem(int id)
        {
            var item = _itemService.GetItem(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult DeleteItem(Item item)
        {
            _itemService.DeleteItem(item.ItemId);
            return RedirectToAction("Index");
        }
    }
}
