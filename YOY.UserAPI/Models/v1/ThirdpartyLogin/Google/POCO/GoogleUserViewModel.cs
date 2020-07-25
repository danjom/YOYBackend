using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.ThirdpartyLogin.Google.POCO
{
    public class GoogleUserViewModel
    {
        public string JwtId { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public bool? VerifiedEmail { get; set; }

        public string Picture { get; set; }
    }
}
