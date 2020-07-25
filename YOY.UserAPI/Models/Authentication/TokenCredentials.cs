using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.Authentication
{
    public class TokenCredentials
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string ClientId { set; get; }

        [Required]
        public string Language { set; get; }

        [AllowNull]
        public string Password { get; set; }

        [AllowNull]
        public string RefreshToken { get; set; }
    }
}
