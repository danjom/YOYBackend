using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpbroadcastingEvents
    {
        public OltpbroadcastingEvents()
        {
            InversePreviousEvent = new HashSet<OltpbroadcastingEvents>();
        }

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int BroadcasterType { get; set; }
        public Guid BroadcasterId { get; set; }
        public int EventType { get; set; }
        public int ConfidenceType { get; set; }
        public string Accuracy { get; set; }
        public bool ContentDelivered { get; set; }
        public Guid? BroadcastingLogId { get; set; }
        public bool SequenceStart { get; set; }
        public bool SequenceEnd { get; set; }
        public int EventSequencePos { get; set; }
        public Guid? PreviousEventId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual OltpbroadcastingLogs BroadcastingLog { get; set; }
        public virtual OltpbroadcastingEvents PreviousEvent { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<OltpbroadcastingEvents> InversePreviousEvent { get; set; }
    }
}
