using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.PurchasedItem.POCO;

namespace YOY.BusinessAPI.Models.v1.PurchasedItem.SET
{
    public class PurchasedItemSet
    {
        public Guid SaleId { set; get; }
        public int Count { set; get; }
        public List<PurchasedItemData> Items { set; get; }
    }
}
