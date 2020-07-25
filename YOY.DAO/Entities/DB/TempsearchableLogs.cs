using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempsearchableLogs
    {
        public Guid? TenantId { get; set; }
        public Guid? ReferenceId { get; set; }
        public int? ReferenceType { get; set; }
        public int? ContentType { get; set; }
        public int? SearchCount { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public string Classification { get; set; }
        public string Keywords { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Guid? IndexOwner { get; set; }
    }
}
