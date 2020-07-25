using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Tempclubs
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid CategoryId { get; set; }
        public int OfferType { get; set; }
        public string Name { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public int MinsToClaim { get; set; }
        public bool IsActive { get; set; }
        public bool IsExclusive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int AvailableQuantity { get; set; }
        public Guid? DisplayImageId { get; set; }
        public int RedeemCount { get; set; }
        public int ClaimCount { get; set; }
        public int GeoSegmentationType { get; set; }
        public bool IsFeatured { get; set; }
        public int DealType { get; set; }
        public string ClaimLocation { get; set; }
        public string Rules { get; set; }
        public string Conditions { get; set; }
        public string ClaimInstructions { get; set; }
        public decimal Value { get; set; }
        public decimal? RegularValue { get; set; }
        public int RewardType { get; set; }
        public int PurposeType { get; set; }
        public bool OneTimeRedemption { get; set; }
        public DateTime? LastClaimDate { get; set; }
        public decimal? AvailablePoints { get; set; }
        public Guid? MembershipIdd { get; set; }
        public Guid RaffleId { get; set; }
        public string Notes { get; set; }
        public DateTime RaffleDate { get; set; }
        public Guid RewardId { get; set; }
        public int RaffleType { get; set; }
        public Guid? ClaimBranchId { get; set; }
        public bool Claimed { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public decimal? RelevanceScore { get; set; }
        public string TenantName { get; set; }
        public Guid TenantLogo { get; set; }
        public int TenantType { get; set; }
        public Guid TenantCategoryId { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal? TenantScore { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchInquiriesPhoneNumber { get; set; }
        public string BranchDescriptiveAddress { get; set; }
        public Guid BranchCityId { get; set; }
        public Guid BranchStateId { get; set; }
    }
}
