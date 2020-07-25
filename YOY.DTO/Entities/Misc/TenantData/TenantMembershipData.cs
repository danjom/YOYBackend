using System;

namespace YOY.DTO.Entities.Misc.TenantData
{
    public class TenantMembershipData
    {
        public Guid TenantId { set; get; }
        public string Name { set; get; }
        public Guid CategoryId { set; get; }
        public bool AcceptsCommunityPointsAsPayment { set; get; }
        public bool AcceptSelfPointsAsPayment { set; get; }
        public int LoyaltyProgramType { set; get; }
        public string CurrencySymbol { set; get; }
        public bool HasMembershipLevels { set; get; }
        public Guid? LandingImgId { set; get; }
        public Guid? LogoId { set; get; }
    }
}
