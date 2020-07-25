using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class OperationIssue
    {
        public Guid Id { set; get; }
        public Guid? TenantId { set; get; }
        public Guid? BranchId { set; get; }
        public string UserId { set; get; }
        public Guid? RefId { set; get; }
        public int RefType { set; get; }
        public string RefTypeName { set; get; }
        public int IssueType { set; get; }
        public string IssueTypeName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public string Details { set; get; }
        public string Comments { set; get; }
        public string ContactInfo { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime LastUpdate { set; get; }
        public string TenantName { set; get; }
        public Guid? TenantLogo { set; get; }
        public string TenantContactName { set; get; }
        public string TenantContactPhone { set; get; }
        public string TenantContactEmail { set; get; }
        public string UserName { set; get; }
        public string UserEmail { set; get; }
        public long UserAccountNumber { set; get; }

    }
}
