using System;

namespace YOY.DTO.Entities.Misc.Location
{
    public class State
    {
        public Guid Id { set; get; }
        public Guid CountryId { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string CountryName { set; get; }
        public string CountryCode { set; get; }
        public string CountryFlag { set; get; }
        public bool IsActive { set; get; }
        public bool InOperation { set; get; }
        public Guid? NearestStateId { set; get; }
        public int UtcTimeZone { set; get; }
        public decimal CentralLatitude { set; get; }
        public decimal CentralLongitude { set; get; }
    }
}
