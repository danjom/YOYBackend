using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefgeozonesView
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid CountryId { get; set; }
        public string DescriptiveAddress { get; set; }
        public string LocationAddress { get; set; }
        public int MinRetriggeredMins { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string DistrictName { get; set; }
        public int? StateUtcTimeZone { get; set; }
    }
}
