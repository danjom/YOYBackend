using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class MoneyConversionLog
    {
        public Guid Id { set; get; }
        public Guid? OperationId { set; get; }
        public Guid TenantId { set; get; }
        public Guid? BranchId { set; get; }
        public Guid? EmployeeId { set; get; }
        public string EmployeeUserName { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedDateLiteral { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string ConversionCode { set; get; }
        public decimal RequiredPoints { set; get; }
        public decimal MoneyAmount { set; get; }
        public int State { set; get; }
        public string StateName { set; get; }
        public string OwnerId { set; get; }
        public string ClaimerId { set; get; }
        public int InternalStatus { set; get; }
        public string InternalStatusName { set; get; }
        public DateTime LastStatusUpdate { set; get; }
        public Guid PointsOpProviderMembershipId { set; get; }
        public string PointsOpCode { set; get; }
        public decimal PointsOpUsedPoints { set; get; }
        public string BranchName { set; get; }
        public string UserName { set; get; }
        public string UserEmail { set; get; }
        public int MembershipLevel { set; get; }
        public string MembershipLevelName { set; get; }
        public int ClaimedPromos { set; get; }
        public DateTime? LastPromoReservedDate { set; get; }
        public DateTime? LastPromoClaimedDate { set; get; }
        public string OwnerName { set; get; }
        public string OwnerEmail { set; get; }
        public string TenantName { set; get; }
        public string TenantContactName { set; get; }
        public string TenantContactEmail { set; get; }
        public string TenantContactPhone { set; get; }

    }
}
