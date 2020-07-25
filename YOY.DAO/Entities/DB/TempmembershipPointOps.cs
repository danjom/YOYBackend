using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempmembershipPointOps
    {
        public Guid Id { get; set; }
        public Guid ProviderMembershipId { get; set; }
        public Guid BeneficiaryTenantId { get; set; }
        public Guid? SourceTenantId { get; set; }
        public Guid? ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public int Type { get; set; }
        public int ObjectiveType { get; set; }
        public int Status { get; set; }
        public decimal AvailablePoints { get; set; }
        public decimal UsedPoints { get; set; }
        public bool IsActive { get; set; }
        public bool Registered { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
