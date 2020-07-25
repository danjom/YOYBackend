using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.ValidationAPI.APIKeyAuth.Models.v1.PaymentRequest.POCO
{
    public class NewPaymentRequest
    {
        [Required]
        [NotNull]
        public string DeviceKey { set; get; }
        [Required]
        [NotNull]
        [Range(0.1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { set; get; }
        [Required]
        [NotNull]
        [Range(1, 10, ErrorMessage = "CurrencyType must be valid")]
        public int CurrencyType { set; get; }
    }
}
