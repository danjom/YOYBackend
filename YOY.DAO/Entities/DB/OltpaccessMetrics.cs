using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpaccessMetrics
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int ReferenceType { get; set; }
        public Guid? ReferenceId { get; set; }
        public string LocationData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
