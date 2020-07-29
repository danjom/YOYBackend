using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.CashbackIncentive.POCO
{
    public class IncentiveModifierById
    {
        public Guid Id { set; get; }
        public Guid EmployeeId { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
    }
}
