using System;

namespace YOY.DTO.Entities.Misc.Department
{
    public class DepartmentCategory
    {
        public Guid TenantId { set; get; }
        public Guid DepartmentId { set; get; }
        public Guid CategoryId { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
