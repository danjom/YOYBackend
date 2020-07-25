using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.Geozone
{
    public class SimpleGeozone
    {
        public Guid Id { set; get; }
        public string ExternalId { set; get; }
        public string Name { set; get; }
        public Guid CountryId { set; get; }
        public string CountryName { set; get; }
        public int MinRetriggeredMins { set; get; }
        public bool IsActive { set; get; }
    }
}
