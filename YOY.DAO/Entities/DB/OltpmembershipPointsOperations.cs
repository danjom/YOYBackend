using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpmembershipPointsOperations
    {
        public OltpmembershipPointsOperations()
        {
            OltpmoneyConversionLogs = new HashSet<OltpmoneyConversionLogs>();
        }

        public Guid Id { get; set; }
        public Guid ProviderMembershipId { get; set; }
        public Guid? BeneficiaryMembershipId { get; set; }
        public Guid BeneficiaryTenantId { get; set; }
        public Guid? BeneficiaryBranchId { get; set; }
        public Guid SourceTenantId { get; set; }
        public Guid? MonetaryFeeLogId { get; set; }
        public Guid? EarningsIncreaserId { get; set; }
        public Guid? ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public int Type { get; set; }
        public int ObjectiveType { get; set; }
        public int OriginType { get; set; }
        public bool WithdrawAllowed { get; set; }
        public bool TransferAllowed { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public bool? Registered { get; set; }
        public string Details { get; set; }
        public decimal AvailablePoints { get; set; }
        public decimal UsedPoints { get; set; }
        public string Code { get; set; }
        public decimal? ConvertedAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual Oltpmemberships BeneficiaryMembership { get; set; }
        public virtual Deftenants BeneficiaryTenant { get; set; }
        public virtual OltpmonetaryFeeLogs MonetaryFeeLog { get; set; }
        public virtual Oltpmemberships ProviderMembership { get; set; }
        public virtual ICollection<OltpmoneyConversionLogs> OltpmoneyConversionLogs { get; set; }
    }
}
