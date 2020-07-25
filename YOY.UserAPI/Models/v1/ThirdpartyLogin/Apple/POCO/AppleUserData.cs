using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.ThirdpartyLogin.Apple.POCO
{
    public class AppleUserData
    {
        public string AppleUserId { get; set; }

        public string GivenName { set; get; }

        public string FamilyName { set; get; }

        public string Email { set; get; }

        public int RealUserState { set; get; }

        public string IdentityToken { set; get; }
    }
}
