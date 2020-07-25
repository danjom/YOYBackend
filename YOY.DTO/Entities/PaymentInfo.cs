using System;

namespace YOY.DTO.Entities
{
    public class PaymentInfo
    {
        public Guid Id { set; get; }
        public string UserId { set; get; }
        public string CardHolder { set; get; }
        public string CardLastDigits { set; get; }
        public int? Funding { set; get; }
        public string FundingName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string Token { set; get; }
        public string Brand { set; get; }
        public Guid CountryId { set; get; }
        public string CountryName { set; get; }
        public string CountryFlag { set; get; }
        public int CountryCurrencyType { set; get; }
        public string CountryCurrencySymbol { set; get; }
        public string Cvc_Check { set; get; }
        public string Exp_Year { set; get; }
        public string Exp_Month { set; get; }
    }
}
