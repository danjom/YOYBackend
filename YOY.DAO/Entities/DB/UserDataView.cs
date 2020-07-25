using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class UserDataView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CountryPhonePrefix { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public long AccountNumber { get; set; }
        public string AccountCode { get; set; }
        public string InvitorUserId { get; set; }
        public string ReferenceCode { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Gender { get; set; }
        public string ProfilePicUrl { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool ReceiveSmsmarketing { get; set; }
        public bool ReceiveEmailMarketing { get; set; }
        public DateTime? LastAppOpen { get; set; }
        public DateTime? LastOfferRedemption { get; set; }
        public int MaxDailyNotifications { get; set; }
        public string Fbid { get; set; }
        public string AppleId { get; set; }
        public string GoogleId { get; set; }
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
    }
}
