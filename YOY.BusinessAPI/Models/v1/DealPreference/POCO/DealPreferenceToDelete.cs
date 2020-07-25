using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.DealPreference.POCO
{
    public class DealPreferenceToDelete
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
        public Guid Id { set; get; }
        [Required]
        [NotNull]
        public Guid DealId { set; get; }
    }
}
