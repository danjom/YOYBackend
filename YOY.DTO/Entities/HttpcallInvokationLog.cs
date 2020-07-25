using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class HttpcallInvokationLog
    {
        public Guid Id { set; get; }
        public string RequesterId { set; get; }
        public string Controller { set; get; }
        public int Call { set; get; }
        public int Version { set; get; }
        public int CallType { set; get; }
        public int StatusCode { set; get; }
        public int? OperationSystem { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string Params { set; get; }
        public int MinRetriggeredMins { set; get; }
        public int RemainingMinsToTrigger { set; get; }
        public Guid? LastValidCall { set; get; }
        public DateTime? LastValidCallDate { set; get; }
        public bool RetrievedContent { set; get; }
        public string Message { set; get; }

    }
}
