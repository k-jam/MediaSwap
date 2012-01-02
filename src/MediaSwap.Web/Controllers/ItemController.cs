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
    [Authorize]
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
        public ActionResult AddGame()
        {
         

            var addItemViewModel = new AddItemViewModel();
            addItemViewModel.ItemTypeName = "Game";
            addItemViewModel.ItemTypes = new ItemTypeService().GetItemTypes("Game");
            return View("AddItem",addItemViewModel);
        }

        [HttpGet]
        public ActionResult AddMovie()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }


            var addItemViewModel = new AddItemViewModel();
            addItemViewModel.ItemTypeName = "Movie";
            addItemViewModel.ItemTypes = new ItemTypeService().GetItemTypes("Movie");
            return View("AddItem", addItemViewModel);
        }

        [HttpGet]
        public ActionResult AddItem(int itemTypeId)
        {
            if(!User.Identity.IsAuthenticated){
                return RedirectToAction("Login","User");
            }
            var addItemViewModel = new AddItemViewModel();
            addItemViewModel.ItemTypes =  new ItemTypeService().GetItemTypes(itemTypeId);
            return View(addItemViewModel);
        }

        [HttpPost]
        public ActionResult AddItem(Item item)
        {
           item.ItemType = new ItemTypeService().GetItemTypes(item.ItemType.ItemTypeId).FirstOrDefault();
            _itemService.SaveItem(item);
            var addItemViewModel = new AddItemViewModel();
            addItemViewModel.ItemTypeName = item.ItemType.ItemTypeName;
         
           
            addItemViewModel.ItemName = item.ItemName;
            addItemViewModel.AmazonId = item.AmazonId;
            addItemViewModel.ItemTypes = new ItemTypeService().GetItemTypes(item.ItemType.ItemTypeName);

            return View(addItemViewModel);
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
