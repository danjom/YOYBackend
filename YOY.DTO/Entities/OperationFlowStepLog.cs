using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities
{
    public class OperationFlowStepLog
    {
        public Guid Id { set; get; }
        public Guid Tenant { set; get; }
        public Guid? BranchId { set; get; }
        public string OwnerId { set; get; }
        public int OwnerType { set; get; }
        public string OwnerTypeName { set; get; }
        public int OperationType { set; get; }
        public string OperationTypeName { set; get; }
        public Guid? OriginOpStepLogId { set; get; }
        public string OperationFlowCode { set; get; }
        public string Discriminator { set; get; }
        public Guid? ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public string ReferenceTypeName { set; get; }
        public Guid? SourceId { set; get; }
        public int SourceType { set; get; }
        public string SourceTypeName { set; get; }
        public int Step { set; get; }
        public bool IsValid { set; get; }
        public bool Allowed { set; get; }
        public bool FlowCompleter { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
