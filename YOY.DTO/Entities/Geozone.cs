using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace YOY.DTO.Entities
{
    public class Geozone
    {
        public Guid Id { set; get; }
        public string ExternalId { set; get; }
        public string Name { set; get; }
        public Guid CountryId { set; get; }
        public Guid? DistrictId { set; get; }
        public string CountryName { set; get; }
        public string DistrictName { set; get; }
        public XElement LocationAddress { set; get; }
        public string LocationAddressData { set; get; }
        public string DescriptiveAddress { set; get; }
        public int MinRetriggeredMins { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
