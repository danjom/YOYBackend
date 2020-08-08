using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.InterestPreference;
using YOY.DTO.Entities.Misc.TenantData;
using System.Collections.Generic;

namespace YOY.DTO.Entities.Misc.CashIncentive
{
    public class CashIncentiveDataWithBranches
    {
        public Entities.CashIncentive CashbackIncentive { set; get; }
        public BasicTenantData Tenant { set; get;}
        public List<BasicBranchData> Branches { set; get; }
        public BasicUserPreferenceData Preference { set; get; }
        public bool ExactLocationBased { set; get; }
        public int SelectorType { set; get; }
    }
}
