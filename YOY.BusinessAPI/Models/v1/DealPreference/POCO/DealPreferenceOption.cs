using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.Misc.POCO;

namespace YOY.BusinessAPI.Models.v1.DealPreference.POCO
{
    public class DealPreferenceOption
    {
        public Guid Id { set; get; }
        public Guid PreferenceId { set; get; }
        public Guid DealId { set; get; }
        public string Value { set; get; }
        public decimal Price { set; get; }
        public decimal RegularPrice { set; get; }
        public string DisplayImgUrl { set; get; }
    }
}
