using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

using MediaSwap.Web.ViewModels;

namespace MediaSwap.Web.Controllers
{
    public class SearchController : Controller
    {
        const int numPerPage = 15;

        public ActionResult Index()
        {
            return View();
        }
         
    }
}