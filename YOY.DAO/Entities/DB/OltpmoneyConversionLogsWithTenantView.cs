using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpmoneyConversionLogsWithTenantView
    {
        public Guid Id { get; set; }
        public Guid? OperationId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string EmployeeUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ConversionCode { get; set; }
        public decimal RequiredPoints { get; set; }
        public decimal MoneyAmount { get; set; }
        public int State { get; set; }
        public string OwnerId { get; set; }
        public string ClaimerId { get; set; }
        public int InternalStatus { get; set; }
        public DateTime LastStatusUpdate { get; set; }
        public Guid PointsOpProviderMembershipId { get; set; }
        public string PointsOpCode { get; set; }
        public decimal PointsOpUsedPoints { get; set; }
        public int PointsOpStatus { get; set; }
        public string TenantName { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string TenantContactName { get; set; }
        public string TenantContactEmail { get; set; }
        public string TenantContactPhone { get; set; }
        public string BranchName { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public int MembershipLevel { get; set; }
        public int ClaimedPromos { get; set; }
        public DateTime? LastPromoReserved { get; set; }
        public DateTime? LastPromoClaimed { get; set; }
    }
}
