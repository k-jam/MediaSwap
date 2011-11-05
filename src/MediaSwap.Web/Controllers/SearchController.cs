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
            searchItemViewModel.ItemResults = iss.Search(search).ToList();
            return View(searchItemViewModel);
        }
    }
}