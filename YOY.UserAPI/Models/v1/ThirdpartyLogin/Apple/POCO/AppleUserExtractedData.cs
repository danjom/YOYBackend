using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.ThirdpartyLogin.Apple.POCO
{
    public class AppleUserExtractedData
    {
        [Required]
        [NotNull]
        [DataType(DataType.Text)]
        [Display(Name = "AppleId")]
        public string AppleId { set; get; }

        [NotNull]
        [DataType(DataType.Text)]
        [Display(Name = "Password")]
        public string Password { set; get; }

        [Required]
        [NotNull]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { set; get; }

        [Required]
        [NotNull]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Email")]
        public string Email { set; get; }

        [AllowNull]
        [DataType(DataType.Text)]
        [Display(Name = "Gender")]
        public string Gender { set; get; }
    }
}
