using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefstatesData
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int UtcTimeZone { get; set; }
        public decimal CenterLatitude { get; set; }
        public decimal CenterLongitude { get; set; }
        public bool IsActive { get; set; }
        public bool InOperation { get; set; }
        public Guid? NearestStateId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CurrencySymbol { get; set; }
        public int CurrencyType { get; set; }
        public string Flag { get; set; }
        public string PhoneNumberPrefix { get; set; }
    }
}
