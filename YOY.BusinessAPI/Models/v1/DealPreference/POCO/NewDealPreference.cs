using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.DealPreference.POCO
{
    public class NewDealPreference
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
        public Guid DealId { set; get; }
        [Required]
        [NotNull]
        public string Name { set; get; }
        [Required]
        [NotNull]
        public string Hint { set; get; }
        [Required]
        [NotNull]
        public int InputType { set; get; }
        [Required]
        [NotNull]
        public int MinOptionsSelected { set; get; }
        [Required]
        [NotNull]
        public int MaxOptionsSelected { set; get; }
        [Required]
        [NotNull]
        public bool IsMandatory { set; get; }
    }
}
