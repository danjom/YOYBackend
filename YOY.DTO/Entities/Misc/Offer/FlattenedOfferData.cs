using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.InterestPreference;
using YOY.DTO.Entities.Misc.TenantData;

namespace YOY.DTO.Entities.Misc.Offer
{
    public class FlattenedOfferData
    {
        public BasicOfferData Offer { set; get; } 
        public BasicUserPreferenceData Preference { set; get; }
        public BasicTenantData Tenant { set; get; }
        public bool ExactLocationBased { set; get; }
        public int SelectorType { set; get; }
    }
}
