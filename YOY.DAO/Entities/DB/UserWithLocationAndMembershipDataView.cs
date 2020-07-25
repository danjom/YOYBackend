using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class UserWithLocationAndMembershipDataView
    {
        public string Id { get; set; }
        public string ProfilePicUrl { get; set; }
        public long AccountNumber { get; set; }
        public string AccountCode { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CountryPhonePrefix { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Language { get; set; }
        public Guid? StateId { get; set; }
        public string StateName { get; set; }
        public int? StateUtcTimeZone { get; set; }
        public bool? StateActiveState { get; set; }
        public bool? StateOperationState { get; set; }
        public Guid? StateNearestNeighbor { get; set; }
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryFlag { get; set; }
        public string CountryCode { get; set; }
        public int? ContentSegmentationType { get; set; }
        public string CountryCurrencySymbol { get; set; }
        public int? CountryCurrencyType { get; set; }
        public Guid MembershipId { get; set; }
        public decimal UsedPoints { get; set; }
        public int MembershipLevel { get; set; }
        public decimal? AvailablePoints { get; set; }
    }
}
