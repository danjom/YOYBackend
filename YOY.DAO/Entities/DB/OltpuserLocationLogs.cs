using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserLocationLogs
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Source { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
