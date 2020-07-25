using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class UserData
    {
        public string Id { set; get; }
        public string UserName { set; get; }
        public string NormalizedUserName { set; get; }
        public string Email { set; get; }
        public string NormalizedEmail { set; get; }
        public bool EmailConfirmed { set; get; }
        public string CountryPhonePrefix { set; get; }
        public string PhoneNumber { set; get; }
        public bool PhoneNumberConfirmed { set; get; }
        public long AccountNumber { set; get; }
        public string AccountCode { set; get; }
        public string ReferenceCode { set; get; }
        public string Name { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public string DateOfBirthLiteral { set; get; }
        public string Gender { set; get; }
        public string GenderName { set; get; }
        public DateTime CreatedDate { set; get; }
        public int MaxDailyAlerts { set; get; }
        public DateTime? LastAppOpen { set; get; }
        public DateTime? LastRedemption { set; get; }
        public Guid? StateId { set; get; }
        public string StateName { set; get; }
        public Guid? CountryId { set; get; }
        public string CountryName { set; get; }
        public int UtcTimeDiff { set; get; }
    }
}
