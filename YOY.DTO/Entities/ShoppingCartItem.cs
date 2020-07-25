using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YOY.DTO.Entities
{
    public class ShoppingCartItem
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string UserId { set; get; }
        public Guid OfferId { set; get; }
        public int Quantity { set; get; }
        public bool HasPreferences { set; get; }
        public XElement ChosenPreferences { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public Offer Offer { set; get; }
    }
}
