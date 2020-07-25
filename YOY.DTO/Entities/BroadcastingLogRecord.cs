using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class BroadcastingLogRecord
    {
        public Guid Id { set; get; }
        public Guid BroadcastingLogId { set; get; }
        public Guid TenantId { set; get; }
        public string UserId { set; get; }
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public int ViewCount { set; get; }
        public int RedemptionCount { set; get; }
    }
}
