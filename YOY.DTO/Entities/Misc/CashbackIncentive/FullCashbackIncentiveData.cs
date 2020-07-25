using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.InterestPreference;
using YOY.DTO.Entities.Misc.TenantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.CashbackIncentive
{
    public class FullCashbackIncentiveData
    {
        public Entities.CashbackIncentive CashbackIncentive { set; get; }
        public BasicTenantData Tenant { set; get; }
        public List<BasicBranchData> Branches { set; get; }
        public BasicUserPreferenceData Preference { set; get; }
        public int SelectorType { set; get; }
    }
}
