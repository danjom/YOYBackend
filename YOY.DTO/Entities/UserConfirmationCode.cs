using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class UserConfirmationCode
    {
        public Guid CodeId { set; get; }
        public string Code { set; get; }
        public string UserId { set; get; }
        public bool IsActive { set; get; }
        public DateTime? CodeLastUsage { set; get; }
        public DateTime CodeCreationDate { set; get; }
        public Guid? BranchId { set; get; }
        public Guid? TenantId { set; get; }
        public DateTime? TenantCodeCreationDate { set; get; }
        public int? UsageReason { set; get; }
        public Guid? UsageRefId { set; get; }
        public string TenantName { set; get; }
        public string BranchName { set; get; }
        public string DescriptiveAddress { set; get; }
    }
}
