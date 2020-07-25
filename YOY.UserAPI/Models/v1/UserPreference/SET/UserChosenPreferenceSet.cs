using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.UserAPI.Models.v1.UserPreference.POCO;

namespace YOY.UserAPI.Models.v1.UserPreference.SET
{
    public class UserChosenPreferenceSet
    {
        public string UserId { set; get; }
        public List<ChosenPreference> Categories { set; get; }
        public List<ChosenPreference> Commerces { set; get; }
    }
}
