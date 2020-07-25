using System;

namespace YOY.DTO.Entities.Misc.Transaction
{
    public class TransactionClaimValues
    {
        public Guid TransactionId { set; get; }
        public int ClaimCount { set; get; }
        public Guid UsageRecordLineId { set; get; }
        public string ClaimRefCode { set; get; }
    }
}
