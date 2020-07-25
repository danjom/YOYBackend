using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.ValidationAPI.APIKeyAuth.Models.v1.PaymentRequest.POCO
{
    public class PaymentRequestStatus
    {
        public string RequestCode { set; get; }
        public int Status { set; get; }
        public bool PaymentCompleted { set; get; } 
        public bool Expired { set; get; }
    }
}
