using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.InteractionMetric
{
    public class NewInteractionMetricRecord
    {
        [Required]
        [NotNull]
        public string UserId { set; get; }
        [Required]
        [NotNull]
        public int ReferenceType { set; get; }
        [Required]
        [NotNull]
        public Guid ReferenceId { set; get; }
        [Required]
        [NotNull]
        public string Location { set; get; }
        [Required]
        [NotNull]
        public int DwellTimeInSeconds { set; get; }

    }
}
