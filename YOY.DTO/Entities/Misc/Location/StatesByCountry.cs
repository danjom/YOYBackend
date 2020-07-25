using System;
using System.Collections.Generic;

namespace YOY.DTO.Entities.Misc.Location
{
    public class StatesByCountry
    {
        public Structure.POCO.Pair<Guid, string> Country { set; get; }
        public string ContryFlag { set; get; }
        public List<StateBaseData> States { set; get; }
    }
}
