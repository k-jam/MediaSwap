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
        IQueueService IQueueService = new QueueService();
        //
        // GET: /Queue/
        [Authorize]
        public ActionResult Index()
        {
            var queueItems = IQueueService.GetQueue(MediaSwap.Web.Models.MediaSwapIdentity.Current.Id);
            return View(queueItems);
        }

        //protected override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    if (MediaSwap.Web.Models.MediaSwapIdentity.Current == null)
        //    {
        //        HttpContext.Response.AddHeader("REQUIRES_AUTH", "1");
        //    }
        //    base.OnAuthorization(filterContext);
        //}


        [Authorize]
        public ActionResult AddItem(int itemId)
        {
            IQueueService queueService = new QueueService();
            var user = MediaSwap.Web.Models.MediaSwapIdentity.Current;

            var itemService = new ItemService();
            var item = itemService.GetItemWithOwner(itemId);
            
            queueService.AddItemToQueue(user.Id, itemId, item.Users.FirstOrDefault().UserId);

            
            var queueItem = new MediaSwap.Web.ViewModels.SearchItemViewModel.ItemResult();
            queueItem.Status = MediaSwap.Core.Models.QueueStatus.Reserved;
            queueItem.Item  = itemService.GetItem(item.ItemId);
            return View("~/Views/Search/_ItemView.cshtml", queueItem);
        }

        public ActionResult Borrow(int queueId)
        {
            IQueueService queueService = new QueueService();
            var queue = queueService.GetQueueItem(queueId);
            queue.BorrowDate = DateTime.Today;
            queue.Status = Core.Models.QueueStatus.Loaned;
            queueService.SaveQueue(queue);
            return RedirectToAction("Index");
        }

        public ActionResult Return(int queueId)
        {
            IQueueService queueService = new QueueService();
            var queue = queueService.GetQueueItem(queueId);
            queue.ReturnDate = DateTime.Today;
            queue.Status = Core.Models.QueueStatus.Returned;
            queueService.SaveQueue(queue);
            return RedirectToAction("Index");
        }
    }
}
