using System;

namespace YOY.DTO.Entities.Misc.MembershipOperation
{
    public class MembershipPointsOpSummary
    {
        public Guid Id { set; get; }
        public Guid? SourceTenantId { set; get; }
        public string Details { set; get; }
        public int Status { set; get; }
        public long PointsAmount { set; get; }
        public bool SoonToExpire { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime ExpirationDate { set; get; }
    }
}
