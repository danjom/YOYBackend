using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities.Misc.Purchase
{
    public class UpdatedPurchaseItem
    {
        public Guid Id { set; get; }
        public Guid PurchaseId { set; get; }
        public int Status { set; get; }
    }
}
