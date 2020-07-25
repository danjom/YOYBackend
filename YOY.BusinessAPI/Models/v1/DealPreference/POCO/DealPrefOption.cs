using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.DealPreference.POCO
{
    public class DealPrefOption
    {
        [Required]
        [NotNull]
        public Guid Id { set; get; }
        [Required]
        [NotNull]
        public string Value { set; get; }
        [Required]
        [NotNull]
        public decimal Price { set; get; }
        [Required]
        [AllowNull]
        public decimal? RegularPrice { set; get; }
        [Required]
        [NotNull]
        public int AppliedAction { set; get; }
    }
}
