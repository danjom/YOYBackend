using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpbroadcastingLogRecords
    {
        public Guid Id { get; set; }
        public Guid BroadcastingLogId { get; set; }
        public Guid TenantId { get; set; }
        public string UserId { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ViewCount { get; set; }
        public int RedemptionCount { get; set; }

        public virtual OltpbroadcastingLogs BroadcastingLog { get; set; }
    }
}
