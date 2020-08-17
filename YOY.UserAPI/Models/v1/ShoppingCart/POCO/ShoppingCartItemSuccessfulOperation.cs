using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.ShoppingCart.POCO
{
    public class ShoppingCartItemSuccessfulOperation
    {
        public int OperationType { set; get; }
        public Guid Id { set; get; }
        public Guid DealId { set; get; }
        public Guid CommerceId { set; get; }
        public int Quantity { set; get; }
        public string Message { set; get; }
    }
}
