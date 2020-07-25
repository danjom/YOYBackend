using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class UserDataForTokenView
    {
        public string Id { get; set; }
        public long AccountNumber { get; set; }
        public string AccountCode { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePicUrl { get; set; }
        public string Language { get; set; }
        public Guid? StateId { get; set; }
        public string StateName { get; set; }
        public int? StateUtcTimeZone { get; set; }
        public Guid? CountryId { get; set; }
        public string CountryCode { get; set; }
        public string CountryFlag { get; set; }
        public string CountryName { get; set; }
        public string CurrencySymbol { get; set; }
        public int? CurrencyType { get; set; }
        public Guid MembershipId { get; set; }
        public decimal UsedPoints { get; set; }
        public int MembershipLevel { get; set; }
        public decimal? AvailablePoints { get; set; }
        public int LastestAndroidVersion { get; set; }
        public string LastestiOsversion { get; set; }
        public string LastestFbversion { get; set; }
        public string LastestWebVersion { get; set; }
    }
}
