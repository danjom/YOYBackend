using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YOY.CashierAPI.Models.Authentication
{
    public class UserAuthDetails
    {
        public string UserId { set; get; }
        public string Name { get; set; }
        public string ProfilePicUrl { set; get; }
        public string Username { get; set; }
        public string Language { set; get; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Token { get; set; }
        public string RefreshToken { set; get; }
        public DateTime? RefreshTokenExpirationUtcDate { set; get; }
    }
}
