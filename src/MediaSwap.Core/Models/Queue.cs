using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaSwap.Core.Models
{
    public enum QueueStatus
    {
        Reserved = 0,
        Loaned = 1
    }

    public class Queue
    {
        public int QueueId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }        
        public DateTime RequestDate { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public int QueueStatusValue { get; set; }
        public QueueStatus Status
        {
            get { return (QueueStatus)QueueStatusValue; }
            set { QueueStatusValue = (int)value; }
        }
    }
}
