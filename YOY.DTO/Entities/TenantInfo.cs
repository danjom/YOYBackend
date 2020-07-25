using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class TenantInfo
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string Name { set; get; }
        public string LegalName { set; get; }
        public string TaxId { set; get; }
        public string TaxAddress { set; get; }
        public string PaymentSubject { set; get; }
        public string AdditionalNotes { set; get; }
        public string Description { set; get; }
        
        public Guid? Logo { set; get; }
        public Guid? CarrouselImgId { set; get; }
        public Guid? EmailsBackground { set; get; }
        public Guid? LandingImg { set; get; }
        public Guid CountryId { set; get; }
        public string CountryName { set; get; }
        public int RelevanceStatus { set; get; }
        public string RelevanceStatusName { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public int PaymentDay { set; get; }
        public string CurrencySymbol { set; get; }
        public int CurrencyType { set; get; }
        public string CurrencyTypeName { set; get; }
        public string ContactName { set; get; }
        public string ContactPhone { set; get; }
        public string ContactEmail { set; get; }
        public Guid CategoryId { set; get; }
        public string CategoryName { set; get; }
        public Guid? ParentCategory { set; get; }
        public string Keywords { set; get; }
        public string DealRules { set; get; }
        public string DealConditions { set; get; }
        public string IncentiveRules { set; get; }
        public string IncentiveConditions { set; get; }
        public string InStoreDealClaimInstructions { set; get; }
        public string OnlineDealClaimInstructions { set; get; }
        public string PhoneDealClaimInstructions { set; get; }
        public string IncentiveClaimInstructions { set; get; }
        public bool IsActive { set; get; }
        public bool Released { set; get; }
        public DateTime? TrialExpiration { set; get; }
        public int TypeId { set; get; }
        public string TypeName { set; get; }
        public int BusinessStructureType { set; get; }
        public string BusinessStructureTypeName { set; get; }
        public int PayerType { set; get; }
        public string PayerTypeName { set; get; }
        public int InstanceType { set; get; }
        public string InstanceTypeName { set; get; }
        public int Language { set; get; }
        public string Website { set; get; }
        public string LanguageName { set; get; }
        public string CampaignDefaultTitleMsg { set; get; }
        public string CampaignDefaultContentMsg { set; get; }
        public int LoyaltyProgramType { set; get; }
        public string LoyaltyProgramTypeName { set; get; }
        public bool HasMembershipLevels { set; get; }
        public bool AcceptsCommunityPointsAsPayment { set; get; }
        public bool AcceptsSelfPointsAsPayment { set; get; }
        public int CheckInType { set; get; }
        public string CheckInTypeName { set; get; }
        public int ReferenceCodeType { set; get; }
        public string ReferenceCodeTypeName { set; get; }
        public decimal DefaultCommissionFeePercentage { set; get; }
        public decimal ConsumerCashbackPercentage { set; get; }
        public int DealClaimMethod { set; get; }
        public string DealClaimMethodName { set; get; }
    }
}
