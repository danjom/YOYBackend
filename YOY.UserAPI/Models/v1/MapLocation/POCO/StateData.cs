using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.MapLocation.POCO
{
    public class StateData
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public decimal Latitude { set; get; }
        public decimal Longitude { set; get; }
    }
}
