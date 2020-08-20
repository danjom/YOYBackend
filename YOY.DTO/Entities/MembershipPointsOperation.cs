using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class MembershipPointsOperation
    {
        public Guid Id { set; get; }
        public Guid ProviderMembershipId { set; get; }
        public string ProviderUserName { set; get; }
        public string ProviderUserEmail { set; get; }
        public Guid? BeneficiaryMembershipId { set; get; }
        public string BeneficiaryUserName { set; get; }
        public string BeneficiaryUserEmail { set; get; }
        public Guid BeneficiaryTenantId { set; get; }
        public Guid? BeneficiaryBranchId { set; get; }
        public Guid SourceTenantId { set; get; }
        public string TenantName { set; get; }
        public Guid? MonetaryFeeLogId { set; get; }
        public int? MonetaryFeeLogReason { set; get; }
        public int? MoneraryFeeLogStatus { set; get; }
        public Guid? ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public int ObjectiveType { set; get; }
        public string ObjectiveTypeName { set; get; }
        public int OriginType { set; get; }
        public string OriginTypeName { set; get; }
        public bool WithdrawAllowed { set; get; }
        public bool TransferAllowed { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public decimal AvailablePoints { set; get; }
        public decimal UsedPoints { set; get; }
        public string Code { set; get; }
        public decimal? ConvertedAmount { set; get; }
        public bool IsActive { set; get; }
        public bool Registered { set; get; }
        public string Details { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public Guid? ConversionLogId { set; get; }
    }
}
