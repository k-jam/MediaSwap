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

        
        [HttpGet]
        public ActionResult AddGame()
        {
         

            var addItemViewModel = new AddItemViewModel();
            addItemViewModel.ItemTypeName = "Game";
            addItemViewModel.ItemTypes = new ItemTypeService().GetItemTypes("Game");
            return View("AddItem",addItemViewModel);
        }


        [HttpPost]
        public ActionResult AddItem(AddItemViewModel addItemViewModel)
        {

            var existingItem =  _itemService.Search(addItemViewModel.ItemName).Where(i => i.ItemType.ItemTypeId == addItemViewModel.ItemType.ItemTypeId).FirstOrDefault();
            if (existingItem == null)
            {
                existingItem  = new Item() { ItemName = addItemViewModel.ItemName, AmazonId = addItemViewModel.AmazonId, ItemType = addItemViewModel.ItemType };
                _itemService.SaveItem(existingItem);
            }
            var userService = new UserService();
            
            userService.AddItem(MediaSwapIdentity.Current.Id, existingItem.ItemId);
            addItemViewModel.ItemTypes = new ItemTypeService().GetItemTypes(existingItem.ItemType.ItemTypeId);
            addItemViewModel.ItemTypeName = existingItem.ItemType.ItemTypeName;
            addItemViewModel.Status = "Item has been successfully added.";
            return View(addItemViewModel);
        }
        [HttpGet]
        public ActionResult AddMovie()
        {

            var addItemViewModel = new AddItemViewModel();
            addItemViewModel.ItemTypeName = "Movie";
            addItemViewModel.ItemTypes = new ItemTypeService().GetItemTypes("Movie");
            return View("AddItem", addItemViewModel);
        }


    }
}
