using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.ThirdpartyLogin.Google.POCO
{
    public class GoogleSDKData
    {
        [Required]
        public string GoogleToken { get; set; }

        public override string ToString()
        {
            return "GoogleToken: " + GoogleToken;
        }
    }
}
