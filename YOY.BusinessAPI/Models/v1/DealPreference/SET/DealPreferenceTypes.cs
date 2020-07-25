using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.DealPreference.POCO;

namespace YOY.BusinessAPI.Models.v1.DealPreference.SET
{
    public class DealPreferenceTypes
    {
        public int Count { set; get; }
        public List<DealPreferenceType> Types { set; get; }
    }
}
