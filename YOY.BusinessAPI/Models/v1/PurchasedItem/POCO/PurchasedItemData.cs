using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.PurchasedItem.POCO
{
    public class PurchasedItemData
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public string Preferences { set; get; }
    }
}
