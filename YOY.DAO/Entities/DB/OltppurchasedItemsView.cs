using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltppurchasedItemsView
    {
        public Guid Id { get; set; }
        public Guid PurchaseId { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public Guid ShoppingCartItemId { get; set; }
        public Guid? OfferId { get; set; }
        public Guid OfferMainCategoryId { get; set; }
        public string OfferMainHint { get; set; }
        public string OfferComplementaryHint { get; set; }
        public string OfferKeywords { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal? OfferRegularPrice { get; set; }
        public int OfferPurchasedQuantity { get; set; }
        public Guid? OfferImg { get; set; }
        public string OfferImgUrl { get; set; }
        public bool UserEarningsIncreaserApplied { get; set; }
        public Guid? AppliedUserEarningsIncreaserId { get; set; }
        public decimal? IncreasementAmount { get; set; }
        public bool HasPreferences { get; set; }
        public string ChosenPreferences { get; set; }
        public int Status { get; set; }
        public int DeliveryType { get; set; }
        public decimal PayedAmount { get; set; }
        public decimal TenantEarnings { get; set; }
        public double CashbackPercentage { get; set; }
        public decimal CashbackAmount { get; set; }
        public int DealType { get; set; }
        public string ClaimLocation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string PurchaseCode { get; set; }
        public string PurchaseNumericCode { get; set; }
        public Guid? DispatchBranchId { get; set; }
        public Guid? DispatchValidationSourceId { get; set; }
        public int? DispatchValidationSourceType { get; set; }
        public Guid PaymentLogId { get; set; }
        public int PurchaseStatus { get; set; }
        public int PurchaseDealType { get; set; }
        public int PurchaseDeliveryType { get; set; }
        public Guid? PurchaseAppliedEarningsIncreaserId { get; set; }
        public decimal PurchaseTotalPayedAmount { get; set; }
        public decimal PurchaseTotalTenantEarnings { get; set; }
        public double PurchaseTotalCashbackPercentage { get; set; }
        public decimal PurchaseTotalCashbackTotalAmount { get; set; }
        public DateTime PurchaseCreatedDate { get; set; }
        public DateTime PurchaseUpdatedDate { get; set; }
    }
}
