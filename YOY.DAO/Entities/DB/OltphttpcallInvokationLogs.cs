using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltphttpcallInvokationLogs
    {
        public OltphttpcallInvokationLogs()
        {
            InverseLastValidCall = new HashSet<OltphttpcallInvokationLogs>();
        }

        public Guid Id { get; set; }
        public string RequesterId { get; set; }
        public int ApiSouce { get; set; }
        public string Controller { get; set; }
        public int Call { get; set; }
        public int CallType { get; set; }
        public int Version { get; set; }
        public int? OperationSystem { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int StatusCode { get; set; }
        public string Params { get; set; }
        public string Message { get; set; }
        public int MinRetriggeredMins { get; set; }
        public int RemainingMinToTrigger { get; set; }
        public Guid? LastValidCallId { get; set; }
        public bool RetrievedContent { get; set; }

        public virtual OltphttpcallInvokationLogs LastValidCall { get; set; }
        public virtual ICollection<OltphttpcallInvokationLogs> InverseLastValidCall { get; set; }
    }
}
