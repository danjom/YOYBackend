using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpreceiptSummariesView
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PicturesCount { get; set; }
        public int PictureExtractionsCount { get; set; }
        public int ExtractionStatus { get; set; }
        public int Purpose { get; set; }
        public int Status { get; set; }
        public decimal? PreTaxAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? AmountsMatchStatus { get; set; }
        public bool? ConfirmedByUser { get; set; }
        public decimal? UserEarnedPoints { get; set; }
        public int? PointsType { get; set; }
        public decimal? UserEarnedMoneyAmount { get; set; }
        public string TenantName { get; set; }
        public Guid? TenantLogoId { get; set; }
        public Guid ValidationId { get; set; }
        public bool RequestValidated { get; set; }
        public int RequestValidationStatus { get; set; }
        public Guid RecordLineId { get; set; }
        public string RecordLineRefCode { get; set; }
        public Guid RequestClaimRecordId { get; set; }
        public Guid TransactionId { get; set; }
        public string TransactionName { get; set; }
    }
}
