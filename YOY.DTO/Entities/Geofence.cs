using System;

namespace YOY.DTO.Entities
{
    public class Geofence
    {
        public Guid Id { set; get; }
        public Guid? ZoneId { set; get; }
        public string ZoneName { set; get; }
        public string ExternalZoneId { set; get; }
        public Guid? DistrictId { set; get; }
        public string DistrictName { set; get; }
        public string Name { set; get; }
        public decimal CenterLatitude { set; get; }
        public decimal CenterLongitude { set; get; }
        public decimal Radius { set; get; }
        public string ExternalId { set; get; }
        public string Label { set; get; }
        public int? ActionType { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
