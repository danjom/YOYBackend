using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.Authentication
{
    public class UserAuthDetails
    {
        public string UserId { set; get; }
        public string Name { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Token { get; set; }
        public string RefreshToken { set; get; }
        public DateTime? RefreshTokenExpirationUtcDate { set; get; }
    }
}
