using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.DTO.Entities.Misc.Structure.POCO;
using YOY.UserAPI.Models.v1.MapLocation.POCO;

namespace YOY.UserAPI.Models.v1.MapLocation.SET
{
    public class StatesInCountry
    {
        public Pair<Guid, string> Country { set; get; }
        public string CountryFlagUrl { set; get; }
        public List<StateData> States { set; get; }
    }
}
