using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.Misc.POCO
{
    public class ExternallyStoredImg
    {
        [Required]
        public string ExternalId { set; get; }
        [Required]
        public string Format { set; get; }
        [Required]
        public string Version { set; get; }
        [Required]
        public string Signature { set; get; }
        [AllowNull]
        public int? Height { set; get; }
        [AllowNull]
        public int? Width { set; get; }
    }
}
