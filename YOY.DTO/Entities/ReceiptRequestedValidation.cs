using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class ReceiptRequestedValidation
    {
        public Guid Id { set; get; }
        public Guid ReceiptId { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public string ReferenceTypeName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public bool Validated { set; get; }
        public DateTime RegisteredDate { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
