using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltprewardedUsers
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public long AccountNumber { get; set; }
        public bool GotDonut { get; set; }
        public bool GotBonus { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
