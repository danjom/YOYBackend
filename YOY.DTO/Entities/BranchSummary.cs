using System;

namespace YOY.DTO.Entities
{
    public class BranchSummary
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string Name { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreationDate { set; get; }
        public bool IsOpen { set; get; }
    }
}
