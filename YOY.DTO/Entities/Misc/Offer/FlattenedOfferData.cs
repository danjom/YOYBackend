using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.InterestPreference;
using YOY.DTO.Entities.Misc.TenantData;

namespace YOY.DTO.Entities.Misc.Offer
{
    public class FlattenedOfferData
    {
        public Entities.Offer Offer { set; get; } 
        public BasicBranchData Branch { set; get; }
        public BasicUserPreferenceData Preference { set; get; }
        public BasicTenantData Tenant { set; get; }
        public bool ExactLocationBased { set; get; }
        public int SelectorType { set; get; }
    }
}
