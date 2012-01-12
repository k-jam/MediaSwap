using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaSwap.Core.Models;

namespace MediaSwap.Core.Services
{

    public interface IQueueService
    {
        IEnumerable<Queue> GetQueue(int userId, bool showReturned = false);
        Queue AddItemToQueue(int userId, int itemId, int ownerId);
        void RemoveItemFromQueue(Queue queue);
    }
}
