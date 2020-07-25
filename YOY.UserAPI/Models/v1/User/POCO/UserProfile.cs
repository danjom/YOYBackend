using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.User.POCO
{
    public class UserProfile
    {
        public string Id { set; get; }
        public long AccountNumber { set; get; }
        public string Name { set; get; }
        public bool ValidBirthDate { set; get; }
        public string FriendlyBirthDate { set; get; }
        public DateTime? BirthDate { set; get; }
        public string CountryPhonePrefix { set; get; }
        public string PhoneNumber { set; get; }
        public bool PhoneNumberConfirmed { set; get; }
        public string Email { set; get; }
        public bool EmailConfirmed { set; get; }
        public string Gender { set; get; }
        public string GenderAbbr { set; get; }
        public string StateName { set; get; }
        public string CountryFlag { set; get; }
        public int MembershipLevel { set; get; }
        public string MembershipLevelName { set; get; }
        public string WalletAmount { set; get; }

    }
}
