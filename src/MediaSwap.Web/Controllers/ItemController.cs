using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaSwap.Core.Models;
using MediaSwap.Core.Services;
using MediaSwap.Web.ViewModels;
using MediaSwap.Web.Models;
namespace MediaSwap.Web.Controllers
{
    public class ItemController : Controller
    {

        IItemService _itemService = new ItemService();

        //
        // GET: /Item/

        public ActionResult Index()
        {
            var vm = new ItemMaintenanceViewModel() { };


            var itemTypeService = new ItemTypeService();
            
           
            vm.ItemTypes = itemTypeService.GetItemTypes();
            vm.Items = _itemService.GetItems().Select(item => new ViewModels.DisplayItemViewModel() { ItemId = item.ItemId, Name = item.ItemName, Type = item.ItemType });

           
            return View("Items",vm);
        }
        

        [HttpGet]
        public ActionResult Items()
        {

            var items = _itemService.GetItems();
            return View(items);
        }

        [HttpGet]
        public ActionResult AddItem(int itemTypeId)
        {
            if(!User.Identity.IsAuthenticated){
                return RedirectToAction("Login","User");
            }
            return View(new Item() { ItemType = new ItemTypeService().GetItemTypes(itemTypeId) });
        }

        [HttpPost]
        public ActionResult AddItem(Item item)
        {
            _itemService.SaveItem(item);
            return View(item);
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
