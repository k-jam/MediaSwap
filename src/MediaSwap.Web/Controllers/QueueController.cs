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

        
        public ActionResult AddToQueue(int userId, int itemId)
        {
            IQueueService queueService = new QueueService();
            queueService.AddItemToQueue(userId, itemId);

            return null;
        }
    }
}
