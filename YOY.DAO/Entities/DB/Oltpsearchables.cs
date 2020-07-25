using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltpsearchables
    {
        public Oltpsearchables()
        {
            OltpsearchLogs = new HashSet<OltpsearchLogs>();
        }

        public Guid TenantId { get; set; }
        public Guid ReferenceId { get; set; }
        public Guid? CountryId { get; set; }
        public int ReferenceType { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public string Classification { get; set; }
        public string Keywords { get; set; }
        public string Details { get; set; }
        public int ContentType { get; set; }
        public bool IsActive { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Guid IndexOwner { get; set; }
        public long SearchCount { get; set; }
        public DateTime? LastSearch { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Defcountries Country { get; set; }
        public virtual DefsearchIndexes IndexOwnerNavigation { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual ICollection<OltpsearchLogs> OltpsearchLogs { get; set; }
    }
}
