using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpoperationIssues
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? BranchId { get; set; }
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

        public virtual Deftenants Tenant { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
