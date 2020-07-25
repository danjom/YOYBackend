using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltppaymentInfosView
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string CardHolder { get; set; }
        public string CardLastDigits { get; set; }
        public int? Funding { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Preferred { get; set; }
        public string Token { get; set; }
        public string Brand { get; set; }
        public Guid CountryId { get; set; }
        public string CvcCheck { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CountryFlag { get; set; }
        public string CountryCurrencySymbol { get; set; }
        public int CountryCurrencyType { get; set; }
    }
}
