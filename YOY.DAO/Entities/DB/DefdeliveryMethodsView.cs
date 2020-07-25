using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefdeliveryMethodsView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool FixedPrice { get; set; }
        public double UnitDistance { get; set; }
        public decimal UnitPrice { get; set; }
        public int MaxItemsPerDelivery { get; set; }
        public bool IsActive { get; set; }
        public Guid BranchId { get; set; }
    }
}
