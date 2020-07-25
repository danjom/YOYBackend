using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpmembershipsView
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string UserId { get; set; }
        public decimal UsedLoyaltyPoints { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public int OriginType { get; set; }
        public bool ReceiveSmsmarketing { get; set; }
        public bool ReceiveEmailMarketing { get; set; }
        public int CustomerRanking { get; set; }
        public int ClaimedPromos { get; set; }
        public DateTime? LastPromoClaimed { get; set; }
        public DateTime? LastPromoReserved { get; set; }
        public DateTime LastLevelEvaluation { get; set; }
        public bool Blocked { get; set; }
        public int MembershipLevel { get; set; }
        public int ClaimedRewards { get; set; }
        public DateTime? ClaimedRewardsStartDate { get; set; }
        public string LevelName { get; set; }
        public decimal? AvailablePoints { get; set; }
        public double LoyaltyCashBackPercentage { get; set; }
        public int MinGeneratedPoints { get; set; }
        public int MaxGeneratedPoints { get; set; }
        public int MinPurchasesCount { get; set; }
        public int MaxPurchasesCount { get; set; }
        public int MaxRewardRedemptions { get; set; }
        public int EvaluationMonths { get; set; }
        public int PointsLifeSpanMonths { get; set; }
        public int? CheckInPoints { get; set; }
        public double MonetaryConversionFactor { get; set; }
        public bool PointsToMoneyEnabled { get; set; }
        public string EnabledMoneyAmounts { get; set; }
        public long AccountNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
