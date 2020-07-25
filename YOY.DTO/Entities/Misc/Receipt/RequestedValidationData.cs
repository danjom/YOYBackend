using System;

namespace YOY.DTO.Entities.Misc.Receipt
{
    public class RequestedValidationData
    {
        public Guid Id { set; get; }
        public bool? Validated { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public Guid RecordLineId { set; get; }
        public string RefCode { set; get; }
        public Guid ClaimRecordId { set; get; }
        public Guid TransactionId { set; get; }
        public string ClaimedDealName { set; get; }
    }
}
