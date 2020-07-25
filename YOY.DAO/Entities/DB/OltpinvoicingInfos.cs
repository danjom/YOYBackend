using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpinvoicingInfos
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int Type { get; set; }
        public int InvoicingIdType { get; set; }
        public string InvoicingId { get; set; }
        public string InvoicingName { get; set; }
        public string AdditionalDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
