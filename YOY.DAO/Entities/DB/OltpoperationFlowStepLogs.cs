using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpoperationFlowStepLogs
    {
        public OltpoperationFlowStepLogs()
        {
            InverseOriginOpStepLog = new HashSet<OltpoperationFlowStepLogs>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public string OwnerId { get; set; }
        public int OwnerType { get; set; }
        public int OperationType { get; set; }
        public Guid? OriginOpStepLogId { get; set; }
        public string OperationFlowCode { get; set; }
        public string Discriminator { get; set; }
        public Guid? ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public Guid? SourceId { get; set; }
        public int SourceType { get; set; }
        public int Step { get; set; }
        public bool IsValid { get; set; }
        public bool Allowed { get; set; }
        public bool FlowCompleter { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual OltpoperationFlowStepLogs OriginOpStepLog { get; set; }
        public virtual ICollection<OltpoperationFlowStepLogs> InverseOriginOpStepLog { get; set; }
    }
}
