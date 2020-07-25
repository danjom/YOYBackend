using System;
using System.Collections.Generic;

namespace YOY.DTO.Entities.Misc.Receipt
{
    public class FullReceiptSummary
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
        public string ExtractionStatusName { set; get; }
        public int Purpose { set; get; }
        public string PurposeName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public decimal? PreTaxAmount { set; get; }
        public decimal? TotalAmount { set; get; }
        public int? AmountsMatchStatus { set; get; }
        public string AmountsMatchStatusName { set; get; }
        public bool? ConfirmedByUser { set; get; }
        public decimal? UserEarnedPoints { set; get; }
        public int? PointsType { set; get; }
        public string PointsTypeName { set; get; }
        public decimal? UserEarnedMoneyAmount { set; get; }
        public List<RequestedValidationData> RequestedValidations { set; get; }
    }
}
