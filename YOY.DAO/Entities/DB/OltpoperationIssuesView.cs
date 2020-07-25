using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpoperationIssuesView
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public string UserId { get; set; }
        public Guid? RefId { get; set; }
        public int RefType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Details { get; set; }
        public string Comments { get; set; }
        public string ContactInfo { get; set; }
        public int IssueType { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdate { get; set; }
        public string TenantName { get; set; }
        public Guid? TenantLogo { get; set; }
        public string TenantContactEmail { get; set; }
        public string TenantContactPhone { get; set; }
        public string TenantContactName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public long UserAccountNumber { get; set; }
    }
}
