using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class Membership
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string UserId { set; get; }
        public decimal AvailableLoyaltyPoints { set; get; }
        public decimal SoonToExpire { set; get; }
        public decimal UsedLoyaltyPoints { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public bool IsActive { set; get; }
        public int OriginType { set; get; }
        public bool ReceiveSMSMarketing { set; get; }
        public bool ReceiveEmailMarketing { set; get; }
        public int CustomerRanking { set; get; }
        public int MembershipLevel { set; get; }
        public string MembershipLevelName { set; get; }
        public int ClaimedRewards { set; get; }
        public DateTime? ClaimedRewardsStartDate { set; get; }
        public int ClaimedPromos { set; get; }
        public DateTime? LastPromoClaimed { set; get; }
        public DateTime? LastPromoReserved { set; get; }
        public DateTime LastLevelEvaluation { set; get; }
        public long AccountNumber { set; get; }
        public bool EmailVerified { set; get; }
        public bool MobilePhoneVerified { set; get; }
        public bool IsNewUser { set; get; }
        public string MembershipIdentifier { set; get; }
        public string Name { set; get; }
        public string PhoneNumber { set; get; }
        public string Username { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public string Gender { set; get; }
        public bool Blocked { set; get; }
        public double LoyaltyCashBackPercentage { set; get; }
        public int MinGeneratedPoints { set; get; }
        public int MaxGeneratedPoints { set; get; }
        public int EvaluationMonths { set; get; }
        public int PointsLifeSpanMonths { set; get; }
        public int CheckInPoints { set; get; }
        public double MonetaryConversionFactor { set; get; }
        public bool PointsToMoneyEnabled { set; get; }
        public string EnabledMoneyAmounts { set; get; }
    }
}
