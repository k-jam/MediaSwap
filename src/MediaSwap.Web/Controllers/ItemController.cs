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

        IUserItemService IUserItemService = new UserItemService();
       


        public ActionResult Index()
        {
         
            return View(IUserItemService.GetItems(MediaSwapIdentity.Current.Id));
        }

        public ActionResult Movies()
        {
            return View(IUserItemService.GetItems(MediaSwapIdentity.Current.Id,"Movie"));
        }

        public ActionResult Games()
        {
            return View(IUserItemService.GetItems(MediaSwapIdentity.Current.Id, "Game"));
        }
        [HttpGet]
        public ActionResult AddGame()
        {
         

            var addItemViewModel = new AddItemViewModel();
            addItemViewModel.ItemTypeName = "Game";
            addItemViewModel.ItemTypes = new ItemTypeService().GetItemTypes("Game");
            return View("Add",addItemViewModel);
        }


        [HttpPost]
        public ActionResult AddItem(AddItemViewModel addItemViewModel)
        {
            IItemService _itemService = new ItemService();
            var existingItem = _itemService.Search(addItemViewModel.ItemName, addItemViewModel.ItemType.ItemTypeId).FirstOrDefault();
            if (existingItem == null)
            {
                existingItem  = new Item() { ItemName = addItemViewModel.ItemName, AmazonId = addItemViewModel.AmazonId, ItemType = addItemViewModel.ItemType };
                _itemService.SaveItem(existingItem);
            }
            IUserItemService.Add(existingItem.ItemId, MediaSwapIdentity.Current.Id);
            addItemViewModel.ItemTypes = new ItemTypeService().GetItemTypes(existingItem.ItemType.ItemTypeId);
            addItemViewModel.ItemTypeName = existingItem.ItemType.ItemTypeName;
            addItemViewModel.Status = "Item has been successfully added.";
            return View("Add",addItemViewModel);
        }

        [HttpGet]
        public ActionResult DeleteItem(int id)
        {
            IItemService _itemService = new ItemService();
            var item = _itemService.GetItem(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult DeleteItem(Item item)
        {
            IItemService _itemService = new ItemService();

            try
            {
                _itemService.DeleteItem(item.ItemId);
                return RedirectToAction("Index");
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(item);
            }
        }
      
        [HttpGet]
        public ActionResult AddMovie()
        {

            var addItemViewModel = new AddItemViewModel();
            addItemViewModel.ItemTypeName = "Movie";
            addItemViewModel.ItemTypes = new ItemTypeService().GetItemTypes("Movie");
            return View("Add", addItemViewModel);
        }

      
    }
}
