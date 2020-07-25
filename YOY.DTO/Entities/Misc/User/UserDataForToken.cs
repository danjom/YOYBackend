using System;

namespace YOY.DTO.Entities.Misc.User
{
    public class UserDataForToken
    {
        public string UserId { set; get; }
        public string ProfilePic { set; get; }
        public long AccountNumber { set; get; }
        public string AccountCode { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public bool EmailConfirmed { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public Guid? StateId { set; get; }
        public string StateName { set; get; }
        public int? StateUtcTimeZone { set; get; }
        public string Language { set; get; }
        public Guid? CountryId { set; get; }
        public string CountryCode { set; get; }
        public string CountryFlag { set; get; }
        public string CountryName { set; get; }
        public string CurrencySymbol { set; get; }
        public int? CurrencyType { set; get; }
        public Guid MembershipId { set; get; }
        public decimal UserPoints { set; get; }
        public int MembershipLevel { set; get; }
        public decimal AvailablePoints { set; get; }
        public int LastestAndroidVersion { set; get; }
        public string LastestiOSVersion { set; get; }
        public string LastestFBVersion { set; get; }
    }
}
