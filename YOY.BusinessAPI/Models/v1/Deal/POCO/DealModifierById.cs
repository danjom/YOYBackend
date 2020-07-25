using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.Deal.POCO
{
    public class DealModifierById
    {
        public Guid Id { set; get; }
        public int OfferType { set; get; }
        public Guid EmployeeId { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
    }
}
