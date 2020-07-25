using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.Location
{
    public class StatePlainData
    {
        public Guid CountryId { set; get; }
        public string CountryName { set; get; }
        public string CountryFlag { set; get; }
        public Guid StateId { set; get; }
        public string StateName { set; get; }
        public decimal StateLatitude { set; get; }
        public decimal StateLongitude { set; get; }
    }
}
