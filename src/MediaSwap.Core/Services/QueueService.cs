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
        public Queue AddItemToQueue(int userId, int itemId)
        {
            using (var context = GetContext())
            {
                var queue = context.Queue.FirstOrDefault(q => q.UserId == userId && q.ItemId == itemId);

                if (queue == null)
                {
                    queue = new Queue();
                }

                queue.ItemId = itemId;
                queue.UserId = userId;
                queue.RequestDate = DateTime.Now;
                queue.Status = QueueStatus.Reserved;

                context.Queue.Attach(queue);

                context.SaveChanges();

                return queue;
            }
        }

        public void RemoveItemFromQueue(Queue queue)
        {
            using (var context = GetContext())
            {
                context.Queue.Attach(queue);
                context.Queue.Remove(queue);

                context.SaveChanges();
            }
        }

        public IEnumerable<Queue> GetQueue(int userId, bool showReturned = false)
        {
            using (var context = GetContext())
            {
                var queue = context.Queue.Where(q => q.UserId == userId);
                
                if (!showReturned)
                {
                    queue = queue.Where(q => q.ReturnDate != null);
                }

                return queue.ToList();
            }
        }
    }
}
