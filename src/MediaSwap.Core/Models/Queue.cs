using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MediaSwap.Core.Models
{
    public enum QueueStatus
    {
        Reserved = 0,
        Loaned = 1
    }

    public class Queue
    {
        [Key]
        public int QueueId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        [ForeignKey("RequesterId")]
        public User Requester { get; set; }
        public int RequesterId { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int QueueStatusValue { get; set; }
        public QueueStatus Status
        {
            get { return (QueueStatus)QueueStatusValue; }
            set { QueueStatusValue = (int)value; }
        }
    }
}
