using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.ThirdpartyLogin.User.POCO
{
    public class UserIdentificationWithPassword
    {
        public string Id { get; set; }
        public string Email { set; get; }
        public string TempPassword { set; get; }
        public bool PhoneValidationRequired { set; get; }
    }
}
