﻿using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltppaymentLogs
    {
        public OltppaymentLogs()
        {
            OltpmoneyConversionLogs = new HashSet<OltpmoneyConversionLogs>();
            OltppaymentRequests = new HashSet<OltppaymentRequests>();
            Oltppurchases = new HashSet<Oltppurchases>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public string UserId { get; set; }
        public Guid? PaymentInfoId { get; set; }
        public Guid? LiquidationMoneyTransferId { get; set; }
        public Guid? ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public int CurrencyType { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal DebitedAmount { get; set; }
        public bool CashbackUsedAsPayment { get; set; }
        public bool CashbackIncentiveApplied { get; set; }
        public Guid? AppliedCashbackIncentiveId { get; set; }
        public bool UserEarningsIncreaserApplied { get; set; }
        public Guid? AppliedUserEarningsIncreaserId { get; set; }
        public decimal? EarningsIncreasementAmount { get; set; }
        public double CashbackPercentage { get; set; }
        public decimal CashbackTotalAmount { get; set; }
        public int Status { get; set; }
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? LiquidationDate { get; set; }
        public int PaymentGateway { get; set; }

        public virtual OltpcashbackIncentives AppliedCashbackIncentive { get; set; }
        public virtual DefearningsIncreasers AppliedUserEarningsIncreaser { get; set; }
        public virtual Defbranches Branch { get; set; }
        public virtual OltpmoneyTransfers LiquidationMoneyTransfer { get; set; }
        public virtual OltppaymentInfos PaymentInfo { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<OltpmoneyConversionLogs> OltpmoneyConversionLogs { get; set; }
        public virtual ICollection<OltppaymentRequests> OltppaymentRequests { get; set; }
        public virtual ICollection<Oltppurchases> Oltppurchases { get; set; }
    }
}