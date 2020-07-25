using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class PaymentMethod
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid BranchId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public bool IsActive { set; get; }
        public bool AllowProgrammedOrders { set; get; }
        public bool PaymentBeforeShipping { set; get; }
        public bool RequiresCall { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public bool OnlyOnline { set; get; }
        public double BankFee { set; get; }
        public string IconName { set; get; }
        public bool CanBeEdited { set; get; }
    }
}
