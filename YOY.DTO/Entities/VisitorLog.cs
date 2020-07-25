using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities
{
    public class VisitorLog
    {
        public Guid Id { set; get; }
        public string IpAddress { set; get; }
        public string Hostname { set; get; }
        public string Type { set; get; }
        public string ContinentName { set; get; }
        public string CountryCode { set; get; }
        public string CountryName { set; get; }
        public string RegionCode { set; get; }
        public string RegionName { set; get; }
        public string City { set; get; }
        public string ZipCode { set; get; }
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        public int DeviceType { set; get; }
        public string DeviceTypeName { set; get; }
        public string DeviceTypeModel { set; get; }
        public string OsVersion { set; get; }
        public bool AllowedAccess { set; get; }
        public DateTime CreatedDate { set; get; }
    }
}
