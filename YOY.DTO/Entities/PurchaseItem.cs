using System;
using System.Xml.Linq;

namespace YOY.DTO.Entities
{
    public class PurchaseItem
    {
        public Guid Id { set; get; }
        public Guid PurchaseId { set; get; }
        public Guid? OfferId { set; get; }
        public Guid OfferMainCategoryId { set; get; }
        public string OfferMainHint { set; get; }
        public string OfferComplementaryHint { set; get; }
        public string OfferKeywords { set; get; }
        public int OfferPurchasedQuantity { set; get; }
        public decimal OfferPrice { set; get; }
        public decimal? OfferRegularPrice { set; get; }
        public Guid? OfferImgId { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
        public Guid? DispatchBranchId { set; get; }
        public Guid? DispatchValidationSourceId { set; get; }
        public int? DispatchValidationSourceType { set; get; }
        public string DispatchValidationSourceTypeName { set; get; }
        public Guid ShoppingCartItemId { set; get; }
        public bool UserEarningsIncreserApplied { set; get; }
        public Guid? AppliedUserEarningsIncreaserId { set; get; }
        public decimal? IncreasementAmount { set; get; }
        public bool HasPreferences { set; get; }
        public XElement ChosenPreferences { set; get; }
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
    }
}
