using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DAO.Entities.Manager.Misc.InteractionMetrics
{
    public class UserInteractionMetricsForReference
    {
        public string UserId { set; get; }
        public int ReferenceType { set; get; }
        public Guid ReferenceId { set; get; }
        public int MetricsCount { set; get; }
        public int TotalTimeInSeconds { set; get; }
    }
}
