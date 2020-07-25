using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltpcheckins
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid BranchId { get; set; }
        public Guid TenantId { get; set; }
        public Guid TenantIdPointsGranter { get; set; }
        public Guid? BroadcasterId { get; set; }
        public int Type { get; set; }
        public int PointsAppliedType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int EarnedPoints { get; set; }
        public bool UsedForRewardClaim { get; set; }

        public virtual Deftenants Tenant { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
