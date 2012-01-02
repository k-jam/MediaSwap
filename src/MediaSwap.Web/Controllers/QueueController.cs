using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaSwap.Core.Services;
namespace MediaSwap.Web.Controllers
{
    public class QueueController : Controller
    {
        //
        // GET: /Queue/

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddItem(int itemId)
        {
            IQueueService queueService = new QueueService();
            var user = MediaSwap.Web.Models.MediaSwapIdentity.Current;
            queueService.AddItemToQueue(user.Id, itemId);

            return null;
        }
    }
}
