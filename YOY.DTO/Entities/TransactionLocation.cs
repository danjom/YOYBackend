using System;

namespace YOY.DTO.Entities
{
    public class TransactionLocation
    {
        public Guid Id { set; get; }
        public Guid TransactionId { set; get; }
        public Guid TenantId { set; get; }
        public Guid? BranchId { set; get; }
        public string BranchName { set; get; }
        public Guid? BroadcasterId { set; get; }
        public string BroadcasterName { set; get; }
        public double? Latitude { set; get; }
        public double? Longitude { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public int ActionType { set; get; }
        public bool Valid { set; get; }
    }
}
