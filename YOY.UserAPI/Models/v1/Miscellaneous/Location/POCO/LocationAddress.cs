using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Miscellaneous.Location.POCO
{
    public class LocationAddress
    {
        public List<Guid> LocationIds { set; get; }
        public List<string> LocationNames { set; get; }
    }
}
