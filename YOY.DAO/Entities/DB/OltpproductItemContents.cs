using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpproductItemContents
    {
        public Guid Id { get; set; }
        public Guid ProductItemId { get; set; }
        public string ReferenceUrl { get; set; }
        public Guid? ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public int ReferenceContentType { get; set; }
        public int ActionTriggerType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ViewCount { get; set; }

        public virtual OltpproductItems ProductItem { get; set; }
    }
}
