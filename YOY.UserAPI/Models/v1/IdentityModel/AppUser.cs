using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.IdentityModel
{
    public class AppUser : IdentityUser
    {
        [Key]
        override public string Id { get; set; }

        [Required]
        public string Name { set; get; }
        public string ProfilePicUrl { set; get; }
        public string Gender { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public string CountryPhonePrefix { set; get; }
        public string FBId { set; get; }
        public string AppleId { set; get; }
        public string GoogleId { set; get; }
        public Guid? StateId { set; get; }
        public string AccountCode { set; get; }
        public string ReferenceCode { set; get; }
        public string InvitorUserId { set; get; }

    }
}
