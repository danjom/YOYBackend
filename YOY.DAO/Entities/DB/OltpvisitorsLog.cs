using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpvisitorsLog
    {
        public Guid Id { get; set; }
        public string IpAddress { get; set; }
        public string Hostname { get; set; }
        public string Type { get; set; }
        public string ContinentName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public double? IpLatitude { get; set; }
        public double? IpLongitude { get; set; }
        public double? VisitorLongitude { get; set; }
        public double? VisitorLatitude { get; set; }
        public int DeviceType { get; set; }
        public string DeviceModel { get; set; }
        public string OsVersion { get; set; }
        public bool AllowedAccess { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
