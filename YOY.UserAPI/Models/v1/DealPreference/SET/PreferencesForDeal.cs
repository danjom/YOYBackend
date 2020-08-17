using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.UserAPI.Models.v1.DealPreference.POCO;

namespace YOY.UserAPI.Models.v1.DealPreference.SET
{
    public class PreferencesForDeal
    {
        public Guid DealId { set; get; }
        public int Count { set; get; }
        public List<DealPreferenceData> Preferences { set; get; }
    }
}
