using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpclaimRecords
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
        public DateTime? PreviousUsage { get; set; }
        public DateTime LastUsage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
