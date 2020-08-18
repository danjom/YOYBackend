using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YOY.DTO.Entities.Misc.Purchase
{
    public class FlattenedPurchaseItem
    {
        public Guid Id { set; get; }
        public Guid PurchaseId { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
        public Guid? OfferId { set; get; }
        public Guid OfferMainCategoryId { set; get; }
        public string OfferName { set; get; }
        public string OfferMainHint { set; get; }
        public string OfferComplementaryHint { set; get; }
        public string OfferKeywords { set; get; }
        public int OfferPurchasedQuantity { set; get; }
        public decimal OfferPrice { set; get; }
        public decimal? OfferRegularPrice { set; get; }
        public Guid? OfferImgId { set; get; }
        public string OfferImgUrl { set; get; }
        public bool UserEarningsIncreserApplied { set; get; }
        public Guid? AppliedUserEarningsIncreaserId { set; get; }
        public decimal? IncreasementAmount { set; get; }
        public bool HasPreferences { set; get; }
        public XElement ChosenPreferences { set; get; }
        public string TextChosenPreferences { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public int DeliveryType { set; get; }
        public string DeliveryTypeName { set; get; }
        public decimal PayedAmount { set; get; }
        public decimal TenantEarnings { set; get; }
        public double CashbackPercentage { set; get; }
        public decimal CashbackAmount { set; get; }
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public string ClaimLocation { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string PurchaseCode { set; get; }
        public string PurchaseNumericCode { set; get; }
        public Guid? DispatchBranchId { set; get; }
        public Guid? DispatchValidationSourceId { set; get; }
        public int? DispatchValidationSourceType { set; get; }
        public string DispatchValidationSourceTypeName { set; get; }
        public Guid? PaymentLogId { set; get; }
        public int PurchaseStatus { set; get; }
        public string PurchaseStatusName { set; get; }
        public int PurchaseDealType { set; get; }
        public string PurchaseDealTypeName { set; get; }
        public int PurchaseDeliveryType { set; get; }
        public string PurchaseDeliveryTypeName { set; get; }
        public Guid? PurchaseAppliedEarningsIncreaserId { set; get; }
        public decimal PurchaseTotalAmount { set; get; }
        public decimal PurchaseTotalTenantEarnings { set; get; }
        public double PurchaseTotalCashbackPercentage { set; get; }
        public decimal PurchaseTotalCashbackAmount { set; get; }
        public DateTime PurchaseCreatedDate { set; get; }
        public DateTime PurchaseUpdatedDate { set; get; }
    }
}
