using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities
{
    public class SavedItem
    {
        public Guid Id { set; get; }
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public string ReferenceTypeName { set; get; }
        public Guid TenantId { set; get; }
        public Guid? TenantHolderId { set; get; }
        public string UserId { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
