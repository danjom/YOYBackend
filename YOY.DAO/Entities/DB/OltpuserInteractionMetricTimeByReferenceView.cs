using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserInteractionMetricTimeByReferenceView
    {
        public string UserId { get; set; }
        public int ReferenceType { get; set; }
        public Guid ReferenceId { get; set; }
        public int? TotalTimeInSeconds { get; set; }
        public int? MetricsCount { get; set; }
    }
}
