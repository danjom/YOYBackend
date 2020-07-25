using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.DTO.Entities.Manager.Misc.InterestPreference;

namespace YOY.UserAPI.Models.v1.UserPreference.SET
{
    public class EnabledPreferences
    {
        public List<UserPreferenceData> Categories { set; get; }
        public int ContentType { set; get; }
        public List<UserPreferenceData> Commerces { set; get; }
    }
}
