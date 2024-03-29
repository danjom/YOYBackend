﻿using System;
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
        public string DiscountHint { get; set; }
        public string CashbackHint { get; set; }
        public Guid? Logo { get; set; }
        public string LogoUrl { get; set; }
        public Guid? WhiteLogo { get; set; }
        public string WhiteLogoUrl { get; set; }
        public Guid? CarrouselImg { get; set; }
        public string CarrouselImgUrl { get; set; }
        public Guid? Thumbnail { get; set; }
        public string ThumbnailUrl { get; set; }
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
        public bool AcceptsPay { get; set; }
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
        public int ReferenceCodeType { get; set; }
        public double DefaultCommissionFeePercentage { get; set; }
        public double ConsumerCashbackPercentage { get; set; }
        public int ReceiptClaimMarkType { get; set; }
        public bool Deleted { get; set; }

        public virtual Oltpimages CarrouselImgNavigation { get; set; }
        public virtual Oltpcategories Category { get; set; }
        public virtual Defcountries Country { get; set; }
        public virtual Oltpimages LogoNavigation { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual Oltpimages ThumbnailNavigation { get; set; }
    }
}
