using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserPhoneNumberLinkLogs
    {
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
