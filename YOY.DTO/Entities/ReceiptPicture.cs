using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class ReceiptPicture
    {
        public Guid Id { set; get; }
        public Guid ReceiptId { set; get; }
        public Guid ImgId { set; get; }
        public int Position { set; get; }
        public int ExtractionStatus { set; get; }
        public string ExtrationStatusName { set; get; }
        public string FullContent { set; get; }
        public string ContainedUniqueValues { set; get; }
        public string RelevantContent { set; get; }
        public string PurchasedItems { set; get; }
        public bool? ContainsDeals { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
