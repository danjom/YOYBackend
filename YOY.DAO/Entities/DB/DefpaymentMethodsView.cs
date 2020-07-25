using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefpaymentMethodsView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowProgrammedOrders { get; set; }
        public bool PaymentBeforeShipping { get; set; }
        public bool RequieresCall { get; set; }
        public bool OnlyOnline { get; set; }
        public string IconName { get; set; }
        public Guid BranchId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
