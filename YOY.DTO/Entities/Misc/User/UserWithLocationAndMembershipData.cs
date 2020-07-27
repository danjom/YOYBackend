using System;

namespace YOY.DTO.Entities.Misc.User
{
    public class UserWithLocationAndMembershipData
    {
        public string Id { set; get; }
        public string PersonalId { set; get; }
        public string ProfilePic { set; get; }
        public long AccountNumber { set; get; }
        public string AccountCode { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
        public bool EmailConfirmed { set; get; }
        public string CountryPhonePrefix { set; get; }
        public string PhoneNumber { set; get; }
        public bool PhoneNumberConfirmed { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string GenderName { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public string Language { set; get; }
        public Guid? StateId { set; get; }
        public string StateName { set; get; }
        public int? StateUtcTimeZone { set; get; }
        public bool? StateActiveState { set; get; }
        public bool? StateOperationState { set; get; }
        public Guid? StateNearestNeighbor { set; get; }
        public Guid? CountryId { set; get; }
        public string CountryName { set; get; }
        public string CountryCode { set; get; }
        public string CountryFlag { set; get; }
        public string CurrencySymbol { set; get; }
        public int? CurrencyType { set; get; }
        public int? ContentSegmentationType { set; get; }
        public Guid MembershipId { set; get; }
        public decimal UsedPoints { set; get; }
        public int MembershipLevel { set; get; }
        public decimal? AvailablePoints { set; get; }
    }
}
