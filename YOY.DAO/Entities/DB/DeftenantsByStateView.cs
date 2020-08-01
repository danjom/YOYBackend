using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DeftenantsByStateView
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid? Logo { get; set; }
        public string LogoUrl { get; set; }
        public Guid? WhiteLogo { get; set; }
        public string WhiteLogoUrl { get; set; }
        public Guid? LandingImg { get; set; }
        public Guid? CarrouselImg { get; set; }
        public string CarrouselImgUrl { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public string CurrencySymbol { get; set; }
        public int CurrencyType { get; set; }
        public bool HasMembershipLevels { get; set; }
        public int LoyaltyProgramType { get; set; }
        public bool AcceptsCommunityPointsAsPayment { get; set; }
        public bool AcceptsSelfPointsAsPayment { get; set; }
        public int CheckInType { get; set; }
        public string ContactPhone { get; set; }
        public Guid CategoryId { get; set; }
        public int RelevanceStatus { get; set; }
        public string CategoryName { get; set; }
        public int TypeId { get; set; }
        public int BusinessStructureType { get; set; }
        public int PayerType { get; set; }
        public int UtcTimeZone { get; set; }
        public int Language { get; set; }
        public bool Released { get; set; }
        public int InstanceType { get; set; }
        public int? BranchesInState { get; set; }
    }
}
