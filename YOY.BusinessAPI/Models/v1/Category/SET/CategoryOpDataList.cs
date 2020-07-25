using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.Category.POCO;

namespace YOY.BusinessAPI.Models.v1.Category.SET
{
    public class CategoryOpDataList
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
        public List<CategoryOpData> Categories { set; get; }
    }
}
