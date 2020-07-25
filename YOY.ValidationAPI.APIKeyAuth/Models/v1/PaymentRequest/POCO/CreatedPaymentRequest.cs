using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.ValidationAPI.APIKeyAuth.Models.v1.PaymentRequest.POCO
{
    public class CreatedPaymentRequest
    {
        public string RequestCode { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string MessageToDisplay { set; get; }
    }
}
