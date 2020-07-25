using System;

namespace YOY.DTO.Entities.Misc.Receipt
{
    public class FlatReceiptSummary
    {
        public Guid Id { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
        public string TenantName { set; get; }
        public Guid? TenantLogoId { set; get; }
        public DateTime CreatedDate { set; get; }
        public int PicturesCount { set; get; }
        public int PictureExtractionsCount { set; get; }
        public int ExtractionStatus { set; get; }
        public int Purpose { set; get; }
        public int Status { set; get; }
        public decimal? PreTaxAmount { set; get; }
        public decimal? TotalAmount { set; get; }
        public int? AmountsMatchStatus { set; get; }
        public bool? ConfirmedByUser { set; get; }
        public decimal? UserEarnedPoints { set; get; }
        public int? PointsType { set; get; }
        public decimal? UserEarnedMoneyAmount { set; get; }
        public Guid ValidationId { set; get; }
        public bool? RequestValidated { set; get; }
        public int RequestedValidationStatus { set; get; }
        public Guid RecordLineId { set; get; }
        public string RecordLineRefCode { set; get; }
        public Guid RequestClaimRecordId { set; get; }
        public Guid TransactionId { set; get; }
        public string ClaimedDealName { set; get; }
    }
}
