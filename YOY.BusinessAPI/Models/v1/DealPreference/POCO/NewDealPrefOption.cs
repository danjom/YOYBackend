using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.Misc.POCO;

namespace YOY.BusinessAPI.Models.v1.DealPreference.POCO
{
    public class NewDealPrefOption
    {
        [Required]
        [NotNull]
        public Guid EmployeeId { set; get; }
        [Required]
        [NotNull]
        public string UserId { set; get; }
        [Required]
        [NotNull]
        public Guid TenantId { set; get; }
        [Required]
        [NotNull]
        public Guid PreferenceId { set; get; }
        [Required]
        [NotNull]
        public string Value { set; get; }
        [Required]
        [NotNull]
        public decimal Price { set; get; }
        [Required]
        [AllowNull]
        public decimal? RegularPrice { set; get; }
    }
}
