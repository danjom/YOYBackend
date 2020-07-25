using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.Category.POCO
{
    public class CategoryOpData
    {
        [Required]
        [NotNull]
        public Guid Id { set; get; }
    }
}
