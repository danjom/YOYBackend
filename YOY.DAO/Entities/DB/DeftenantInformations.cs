using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DeftenantInformations
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public int TypeId { get; set; }
        public int BusinessStructureType { get; set; }
        public int PayerType { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string TaxAddress { get; set; }
        public string PaymentSubject { get; set; }
        public string AdditionalNotes { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public Guid? Logo { get; set; }
        public Guid? CarrouselImg { get; set; }
        public Guid? EmailBg { get; set; }
        public Guid? LandingImg { get; set; }
        public int RelevanceStatus { get; set; }
        public Guid CountryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int PaymentDay { get; set; }
        public string CurrencySymbol { get; set; }
        public int CurrencyType { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public Guid CategoryId { get; set; }
        public bool HasMembershipLevels { get; set; }
        public int LoyaltyProgramType { get; set; }
        public bool AcceptsCommunityPointsAsPayment { get; set; }
        public bool AcceptsSelfPointsAsPayment { get; set; }
        public DateTime? TrialExpiration { get; set; }
        public string DealRules { get; set; }
        public string DealConditions { get; set; }
        public string IncentiveRules { get; set; }
        public string IncentiveConditions { get; set; }
        public string InStoreDealClaimInstructions { get; set; }
        public string OnlineDealClaimInstructions { get; set; }
        public string PhoneDealClaimInstructions { get; set; }
        public string IncentiveClaimInstructions { get; set; }
        public string CampaignDefaultTitleMsg { get; set; }
        public string CampaignDefaultContentMsg { get; set; }
        public int Language { get; set; }
        public string Website { get; set; }
        public int DealsClaimMethod { get; set; }
        public int CheckInType { get; set; }
        public int ReferenceCodeType { get; set; }
        public decimal DefaultCommissionFeePercentage { get; set; }
        public decimal ConsumerCashbackPercentage { get; set; }
        public int ReceiptClaimMarkType { get; set; }
        public bool Deleted { get; set; }

        public virtual Oltpcategories Category { get; set; }
        public virtual Defcountries Country { get; set; }
        public virtual Oltpimages EmailBgNavigation { get; set; }
        public virtual Oltpimages LandingImgNavigation { get; set; }
        public virtual Oltpimages LogoNavigation { get; set; }
        public virtual Deftenants Tenant { get; set; }
    }
}
