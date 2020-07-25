using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities
{
    public class InvoicingInfo
    {
        public Guid Id { set; get; }
        public string UserId { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public int InvoicingIdType { set; get; }
        public string InvoicingIdTypeName { set; get; }
        public string InvoicingId { set; get; }
        public string InvoicingName { set; get; }
        public string AdditionalDetails { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
