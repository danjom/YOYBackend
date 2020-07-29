using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.User.POCO
{
    public class SuccessfulUpdatedProfile
    {
        public bool DisplayMsg { set; get; }
        public string MsgTitle { set; get; }
        public string MsgContent { set; get; }
        public UserProfile UserProfile { set; get; }
    }
}
