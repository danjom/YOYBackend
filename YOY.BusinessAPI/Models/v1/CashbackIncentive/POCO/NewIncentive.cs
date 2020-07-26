﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.CashbackIncentive.POCO
{
    public class NewIncentive
    {
        [NotNull]
        [Required]
        public int EarningsType { set; get; }
        [NotNull]
        [Required]
        public int DisplayType { set; get; }
        [NotNull]
        [Required]
        public int Type { set; get; }
        [NotNull]
        [Required]
        public int DealType { set; get; }
        [NotNull]
        [Required]
        public int CombineType { set; get; }
        [NotNull]
        [Required]
        public double UnitValue { set; get; }
        [NotNull]
        [Required]
        public double PreviousUnitValue { set; get; }
        [NotNull]
        [Required]
        public int MinMembershipLevel { set; get; }
        [NotNull]
        [Required]
        public decimal MinPurchasedAmount { set; get; }
        [NotNull]
        [Required]
        public decimal PurchasedAmountBlock { set; get; }
        [NotNull]
        [Required]
        public decimal MaxValue { set; get; }
        [NotNull]
        [Required]
        public int AvailableQuantity { set; get; }
        [NotNull]
        [Required]
        public string Name { set; get; }
        [NotNull]
        [Required]
        public string Description { set; get; }
        [NotNull]
        [Required]
        public string Keywords { set; get; }
        [NotNull]
        [Required]
        public bool IsSponsored { set; get; }
        [NotNull]
        [Required]
        public DateTime ReleaseDate { set; get; }
        [NotNull]
        [Required]
        public DateTime ExpirationDate { set; get; }
        [NotNull]
        [Required]
        public string ValidWeekDays { set; get; }
        [NotNull]
        [Required]
        public string ValidMonthDays { set; get; }
        [NotNull]
        [Required]
        public string ValidHours { set; get; }
        [NotNull]
        [Required]
        public int MaxUsagePerUser { set; get; }

    }
}