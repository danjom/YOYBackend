using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.ValidationAPI.APIKeyAuth.Models.v1.PurchaseDispatch.POCO
{
    public class PurchaseDispatchRequest
    {
        [Required]
        [NotNull]
        public string DeviceKey { set; get; }
        [Required]
        [NotNull]
        public string PurchaseCode { set; get; }
    }
}
