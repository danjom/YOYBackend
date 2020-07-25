using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.VisitorInspector.POCO
{
    public class IpAddressInspectionViewModel
    {
        [JsonProperty("ip")]
        public string IpAddress { set; get; }
        [JsonProperty("hostname")]
        public string HostName { set; get; }
        [JsonProperty("type")]
        public string Type { set; get; }
        [JsonProperty("continent_name")]
        public string ContinentName { set; get; }
        [JsonProperty("country_code")]
        public string CountryCode { set; get; }
        [JsonProperty("country_name")]
        public string CountryName { set; get; }
        [JsonProperty("region_code")]
        public string RegionCode { set; get; }
        [JsonProperty("region_name")]
        public string RegionName { set; get; }
        [JsonProperty("city")]
        public string City { set; get; }
        [JsonProperty("zip")]
        public string ZipCode { set; get; }
        [JsonProperty("latitude")]
        public double Latitude { set; get; }
        [JsonProperty("longitude")]
        public double Longitude { set; get; }
    }
}
