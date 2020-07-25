﻿using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpshoppingCartItemsView
    {
        public Guid Id { get; set; }
        public Guid OfferId { get; set; }
        public Guid TenantId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public bool HasPreferences { get; set; }
        public string ChosenPreferences { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid MainCategoryId { get; set; }
        public int OfferType { get; set; }
        public int DealType { get; set; }
        public int RewardType { get; set; }
        public int PurposeType { get; set; }
        public int GeoSegmentationType { get; set; }
        public int DisplayType { get; set; }
        public string Name { get; set; }
        public string MainHint { get; set; }
        public string ComplementaryHint { get; set; }
        public string Keywords { get; set; }
        public string Code { get; set; }
        public Guid? CodeImg { get; set; }
        public string Description { get; set; }
        public int MinsToUnlock { get; set; }
        public bool OfferIsActive { get; set; }
        public bool IsExclusive { get; set; }
        public bool IsSponsored { get; set; }
        public bool HasUniqueCodes { get; set; }
        public bool OfferHasPreferences { get; set; }
        public int AvailableQuantity { get; set; }
        public bool OneTimeRedemption { get; set; }
        public int MaxClaimsPerUser { get; set; }
        public int MinPurchasesCountToRedeem { get; set; }
        public DateTime? PurchasesCountStartDate { get; set; }
        public string ClaimLocation { get; set; }
        public decimal Value { get; set; }
        public decimal? RegularValue { get; set; }
        public double ExtraBonus { get; set; }
        public int ExtraBonusType { get; set; }
        public decimal MinIncentive { get; set; }
        public decimal MaxIncentive { get; set; }
        public decimal? IncentiveVariation { get; set; }
        public int IncentiveVariationType { get; set; }
        public int ClaimCount { get; set; }
        public int RedeemCount { get; set; }
        public Guid? DisplayImageId { get; set; }
        public string TargettingParams { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Rules { get; set; }
        public string Conditions { get; set; }
        public string ClaimInstructions { get; set; }
        public double RelevanceRate { get; set; }
        public DateTime OfferCreatedDate { get; set; }
        public DateTime OfferUpdatedDate { get; set; }
        public string CategoryName { get; set; }
        public Guid? CategoryParentId { get; set; }
        public int? CategoryRelevanceStatus { get; set; }
    }
}
