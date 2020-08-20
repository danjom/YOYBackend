using System;

namespace YOY.DTO.Entities.Misc.MembershipOperation
{
    public class MembershipPointsOp
    {
        public Guid Id { set; get; }
        public Guid MembershipId { set; get; }
        public Guid BeneficiaryTenantId { set; get; }
        public Guid? SourceTenantId { set; get; }
        public Guid? ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public int Type { set; get; }
        public int OriginType { set; get; }
        public bool WithdrawAllowed { set; get; }
        public bool TransferAllowed { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public long AvailablePoints { set; get; }
        public long UsedPoints { set; get; }
        public bool IsActive { set; get; }
        public bool Registered { set; get; }
        public string Details { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime ExpirationDate { set; get; }
    }
}
