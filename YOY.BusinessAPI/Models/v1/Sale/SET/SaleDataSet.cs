﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.Sale.POCO;

namespace YOY.BusinessAPI.Models.v1.Sale.SET
{
    public class SaleDataSet
    {
        public int TotalRecords { set; get; }
        public string TotalAmount { set; get; }
        public string WithdrawedAmount { set; get; }
        public string PendingAmount { set; get; }
        public Guid TenantId { set; get; }
        public Guid BranchId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public List<SaleData> Sales { set; get; }
    }
}
