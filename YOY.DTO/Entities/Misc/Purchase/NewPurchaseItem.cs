using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YOY.DTO.Entities.Misc.Purchase
{
    public class NewPurchaseItem
    {
        public Guid PurchaseId { set; get; }
        public Guid OfferId { set; get; }
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
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
        public bool UserEarningsIncreaserApplied { set; get; }
        public Guid? AppliedEarningsIncreaserId { set; get; }
        public decimal IncreasementAmount { set; get; }
        public bool HasPreferences { set; get; }
        public XElement ChosenPreferences { set; get; }
        public string TextChosenPreferences { set; get; }
        public int Status { set; get; }
        public int DeliveryType { set; get; }
        public decimal PayedAmount { set; get; }
        public decimal TenantEarnings { set; get; }
        public double CashbackPercentage { set; get; }
        public decimal CashbackAmount { set; get; }
        public int DealType { set; get; }
        public string ClaimLocation { set; get; }
    }
}
