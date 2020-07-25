using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefdepartmentCategories
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid TenantId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltpcategories Category { get; set; }
        public virtual Defdepartments Department { get; set; }
        public virtual Deftenants Tenant { get; set; }
    }
}
