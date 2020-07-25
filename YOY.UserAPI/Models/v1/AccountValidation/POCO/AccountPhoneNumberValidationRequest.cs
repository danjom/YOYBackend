using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.AccountValidation.POCO
{
    public class AccountPhoneNumberValidationRequest
    {
        [Required]
        [NotNull]
        public string UserId { set; get; }
        [Required]
        [NotNull]
        public string PhoneNumber { set; get; }
        [Required]
        [NotNull]
        public string CountryPhonePrefix { set; get; }
        [Required]
        [NotNull]
        public int ChannelType { set; get; }
    }
}
