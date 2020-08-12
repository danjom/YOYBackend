﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.CashIncentive.POCO
{
    public class IncentiveDatesModifier
    {
        public Guid Id { set; get; }
        public Guid EmployeeId { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime ExpirationDate { set; get; }
    }
}
