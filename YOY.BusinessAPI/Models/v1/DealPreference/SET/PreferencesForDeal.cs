using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.DealPreference.POCO;
using YOY.DTO.Entities.Misc.OfferPreference;

namespace YOY.BusinessAPI.Models.v1.DealPreference.SET
{
    public class PreferencesForDeal
    {
        public Guid DealId { set; get; }
        public int Count { set; get; }
        public List<DealPreferenceData> PreferencesData { set; get; }
    }
}
