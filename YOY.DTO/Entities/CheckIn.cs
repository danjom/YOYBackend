using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class CheckIn
    {
        public Guid Id { set; get; }
        public string UserId { set; get; }
        public Guid BranchId { set; get; }
        public Guid TenantId { set; get; }
        public Guid TenantIdPointsGranter { set; get; }
        public Guid? BroadcasterId { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public int PointsAppliedType { set; get; }
        public string PointsAppliedTypeName { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime? ExpirationDate { set; get; }
        public bool IsActive { set; get; }
        public int EarnedPoints { set; get; }
        public bool UsedForRewardClaim { set; get; }
        public string BranchName { set; get; }
        public string TenantName { set; get; }
        public string BroadcasterName { set; get; }
        public string UserName { set; get; }
        public string Username { set; get; }
        public long AccountNumber { set; get; }
    }
}
