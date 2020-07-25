using System;

namespace YOY.DTO.Entities
{
    public class BroadcastingLog
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string UserId { set; get; }
        public string Username { set; get; }
        public string UserPhoneNumber { set; get; }
        public string UserEmail { set; get; }
        public long UserAccNumber { set; get; }
        public DateTime SentDate { set; get; }
        public DateTime? ExpirationDate { set; get; }
        public int BroadcasterType { set; get; }
        public string BroadcasterTypeName { set; get; }
        public Guid? BroadcasterId { set; get; }
        public string MsgTitle { set; get; }
        public string MsgContent { set; get; }
        public DateTime? OpenedDate { set; get; }
        public decimal? TriggeredLatitude { set; get; }
        public decimal? TriggeredLongitude { set; get; }
        public int ContainedContentCount { set; get; }
        public string ContainedContentIds { set; get; }
        public bool? ContentRedeemed { set; get; }
        public string RedeemedContentIds { set; get; }
        public int ViewCount { set; get; }
    }
}
