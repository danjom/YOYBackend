using System;
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
        public int Type { set; get; }
        [NotNull]
        [Required]
        public int DealType { set; get; }
        [NotNull]
        [Required]
        public int CombineType { set; get; }

    }
}
