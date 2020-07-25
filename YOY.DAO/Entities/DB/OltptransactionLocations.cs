using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltptransactionLocations
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid? BroadcasterId { get; set; }
        public string BroadcasterName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ActionType { get; set; }
        public bool Valid { get; set; }

        public virtual Oltptransactions Transaction { get; set; }
    }
}
