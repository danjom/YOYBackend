using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defdepartments
    {
        public Defdepartments()
        {
            Defbranches = new HashSet<Defbranches>();
            Defbroadcasters = new HashSet<Defbroadcasters>();
            DefdepartmentCategories = new HashSet<DefdepartmentCategories>();
            OltpbroadcastingPlayerLogs = new HashSet<OltpbroadcastingPlayerLogs>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool CoversLocation { get; set; }

        public virtual Deftenants Tenant { get; set; }
        public virtual ICollection<Defbranches> Defbranches { get; set; }
        public virtual ICollection<Defbroadcasters> Defbroadcasters { get; set; }
        public virtual ICollection<DefdepartmentCategories> DefdepartmentCategories { get; set; }
        public virtual ICollection<OltpbroadcastingPlayerLogs> OltpbroadcastingPlayerLogs { get; set; }
    }
}
