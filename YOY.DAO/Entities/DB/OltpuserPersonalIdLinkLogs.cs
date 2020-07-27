using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserPersonalIdLinkLogs
    {
        public string UserId { get; set; }
        public string PersonalId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
