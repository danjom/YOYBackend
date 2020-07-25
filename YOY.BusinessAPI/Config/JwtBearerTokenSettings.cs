using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Config
{
    public class JwtBearerTokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiryTimeInMinutes { get; set; }
        public int RefreshTokenExpiryTimeInHours { set; get; }
    }
}
