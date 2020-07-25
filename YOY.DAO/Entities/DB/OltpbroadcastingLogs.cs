using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpbroadcastingLogs
    {
        public OltpbroadcastingLogs()
        {
            OltpbroadcastingEvents = new HashSet<OltpbroadcastingEvents>();
            OltpbroadcastingLogRecords = new HashSet<OltpbroadcastingLogRecords>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string UserId { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int BroadcasterType { get; set; }
        public Guid BroadcasterId { get; set; }
        public string MsgTitle { get; set; }
        public string MsgContent { get; set; }
        public DateTime? OpenedDateTime { get; set; }
        public decimal? TriggeredLatitude { get; set; }
        public decimal? TriggeredLongitude { get; set; }
        public int ContainedContentCount { get; set; }
        public string ContainedContentIds { get; set; }
        public bool? ContentRedeemed { get; set; }
        public string RedeemContentIds { get; set; }
        public int ViewedCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Deftenants Tenant { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<OltpbroadcastingEvents> OltpbroadcastingEvents { get; set; }
        public virtual ICollection<OltpbroadcastingLogRecords> OltpbroadcastingLogRecords { get; set; }
    }
}
