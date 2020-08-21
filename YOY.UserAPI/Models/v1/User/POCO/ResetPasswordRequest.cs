using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.User.POCO
{
    public class ResetPasswordRequest
    {
        [Required]
        [NotNull]
        public string Email { set; get; }
    }
}
