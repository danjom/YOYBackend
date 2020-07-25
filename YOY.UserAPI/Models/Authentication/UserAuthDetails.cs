using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.Authentication
{
    public class UserAuthDetails
    {
        public string UserId { set; get; }
        public long AccountNumber { get; set; }
        public string AccountCode { set; get; }
        public string Name { get; set; }
        public string ProfilePicUrl { set; get; }
        public string Username { get; set; }
        public Guid? CountryId { set; get; }
        public string CurrencySymbol { set; get; }
        public int? CurrencyType { set; get; }
        public string Language { set; get; }
        public Guid? StateId { set; get; }
        public string StateName { set; get; }
        public string CountryFlag { set; get; }
        public string CountryCode { set; get; }
        public int? UtcTimeZone { set; get; }
        public int MembershipLevel { set; get; }
        public bool ShowPrefrencesChooser { set; get; }
        public bool ShowLocationChooser { set; get; }
        public bool AskBirthdate { set; get; }
        public decimal AvailablePoints { set; get; }
        public string WalletAmount { set; get; }
        public string IntroVideoLink { set; get; }
        public int AndroidVersion { set; get; }
        public string iOSVersion { set; get; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Token { get; set; }
        public string RefreshToken { set; get; }
        public DateTime? RefreshTokenExpirationUtcDate { set; get; }
    }
}
