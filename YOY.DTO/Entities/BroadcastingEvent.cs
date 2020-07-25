using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class BroadcastingEvent
    {
        public Guid Id { set; get; }
        public string UserId { set; get; }
        public int BroadcasterType { set; get; }
        public string BroadcasterTypeName { set; get; }
        public Guid BroadcasterId { set; get; }
        public string BroadcasterName { set; get; }
        public int EventType { set; get; }
        public string EventTypeName { set; get; }
        public int ConfidenceType { set; get; }
        public string ConfidenceTypeName { set; get; }
        public string Accuracy { set; get; }
        public bool ContentDelivered { set; get; }
        public Guid? BroadcastingLogId { set; get; }
        public bool SequenceStart { set; get; }
        public bool SequenceEnd { set; get; }
        public int EventSequencePos { set; get; }
        public Guid? PreviousEventId { set; get; }
        public decimal? Longitude { set; get; }
        public decimal? Latitude { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }

    }
}
