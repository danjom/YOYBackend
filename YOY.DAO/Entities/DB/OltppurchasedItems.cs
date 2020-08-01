using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltppurchasedItems
    {
        public Guid Id { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid? OfferId { get; set; }
        public Guid OfferMainCategoryId { get; set; }
        public string OfferMainHint { get; set; }
        public string OfferComplementaryHint { get; set; }
        public string OfferKeywords { get; set; }
        public int OfferPurchasedQuantity { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal? OfferRegularPrice { get; set; }
        public Guid? OfferImg { get; set; }
        public string OfferImgUrl { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public Guid ShoppingCartItemId { get; set; }
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

        public virtual DefearningsIncreasers AppliedUserEarningsIncreaser { get; set; }
        public virtual Oltpoffers Offer { get; set; }
        public virtual Oltppurchases Purchase { get; set; }
    }
}
