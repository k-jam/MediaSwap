﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Repositories;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Services
{
    public class QueueService : BaseContext, IQueueService
    {
        public Queue AddItemToQueue(int userId, int itemId, int ownerId)
        {
            using (var context = GetContext())
            {
              //  var queue = context.Queue.FirstOrDefault(q => q.Requester.UserId == userId && q.ItemId == itemId);

               // if (queue == null)
               // {
                  var  queue = new Queue();

                    queue.ItemId = itemId;
                    queue.RequesterId = userId;
                    queue.OwnerId = ownerId;
                    queue.RequestDate = DateTime.Now;
                    queue.Status = QueueStatus.Reserved;
                    context.Queue.Add(queue);

                //}
                //else
                //{
                  //  context.Queue.Attach(queue);
                //}

                

                context.SaveChanges();

                return queue;
            }
        }

        public void RemoveItemFromQueue(int  queueId)
        {
            using (var context = GetContext())
            {
                var queue = context.Queue.FirstOrDefault(q => q.QueueId == queueId);
                context.Queue.Remove(queue);

                context.SaveChanges();
            }
        }

        public IEnumerable<Queue> GetQueue(int userId, bool showReturned = false)
        {
            using (var context = GetContext())
            {
                var queue = context.Queue.Include("Item").Include("Owner").Where(q => q.Requester.UserId == userId && q.ReturnDate == null);
                
                if (showReturned)
                {
                    queue = queue.Where(q => q.ReturnDate != null);
                }

                return queue.ToList();
            }
        }
        public Queue GetQueueItem(int queueId)
        {
            using (var context = GetContext())
            {
                var queue = context.Queue.Where(q=>q.QueueId == queueId).FirstOrDefault();
                return queue;
            }
        }

        public void BorrowQueueItem(int queueId)
        {
            using (var context = GetContext())
            {
                var queue = context.Queue.FirstOrDefault(q => q.QueueId == queueId);
                if ((QueueStatus)queue.QueueStatusValue == QueueStatus.Reserved)
                {
                    queue.BorrowDate = DateTime.Now;
                    queue.QueueStatusValue = (int)QueueStatus.Loaned;
                }

                context.SaveChanges();
            }
        }
        public void ReturnQueueItem(int queueId)
        {
            using (var context = GetContext())
            {
                var queue = context.Queue.FirstOrDefault(q => q.QueueId == queueId);
                if ((QueueStatus)queue.QueueStatusValue == QueueStatus.Loaned)
                {
                    queue.ReturnDate = DateTime.Now;
                    queue.QueueStatusValue = (int) QueueStatus.Returned;
                }
                
                context.SaveChanges();
            }
        }
        public void SaveQueue(Queue queue)
        {
            using (var context = GetContext())
            {

                context.Queue.Attach(queue);
                context.Entry(queue).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
