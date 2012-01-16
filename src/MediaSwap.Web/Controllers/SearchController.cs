using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

using MediaSwap.Web.ViewModels;
using MediaSwap.Core.Services;

namespace MediaSwap.Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        const int numPerPage = 15;

        public ActionResult Index()
        {
            SearchItemViewModel searchItemViewModel = new SearchItemViewModel();
            //ItemService iss = new ItemService();
            //searchItemViewModel.ItemResults = iss.Search(search.Value.ToString()).ToList();
            return View(searchItemViewModel);
        }

        [HttpPost]
        public ActionResult Index(string search)
        {
            SearchItemViewModel searchItemViewModel = new SearchItemViewModel();
            ItemService iss = new ItemService();
            var queueItems = new QueueService().GetQueue(MediaSwap.Web.Models.MediaSwapIdentity.Current.Id);

            searchItemViewModel.ItemResults = iss.Search(search).Select(s=>new SearchItemViewModel.ItemResult(){ Item  = s}).ToList();
            foreach (var queueItem in queueItems)
            {
               var searchItem =  searchItemViewModel.ItemResults.FirstOrDefault(i => i.Item.ItemId == queueItem.ItemId);
               if (searchItem != null)
               {
                   searchItem.Status = queueItem.Status;
               }
            }
            return View(searchItemViewModel);
        }
    }
}