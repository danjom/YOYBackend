using System;

namespace YOY.DTO.Entities.Misc.Location
{
    public class Country
    {
        public Guid Id { set; get; }
        public string Code { set; get; }
        public string PhoneNumberPrefix { set; get; }
        public string CurrencySymbol { set; get; }
        public int CurrencyType { set; get; }
        public string Name { set; get; }
        public bool IsActive { set; get; }
        public string Flag { set; get; }
    }
}
