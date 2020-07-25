using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.MapLocation.SET
{
    public class AvailableStates
    {
        public int Count { set; get; }
        public List<StatesInCountry> Locations { set; get; }
        public Guid CurrentCountryId { set; get; }
        public string CurrentCountryCode { set; get; }
        public string CurrentCountryName { set; get; }
        public Guid CurrentStateId { set; get; }
        public string CurrentStateName { set; get; }
        public bool SelectedLocationExists { set; get; }
        public bool ShowPreferences { set; get; }
    }
}
