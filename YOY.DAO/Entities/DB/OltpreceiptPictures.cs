using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpreceiptPictures
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }
        public Guid ImgId { get; set; }
        public int Position { get; set; }
        public int ExtractionStatus { get; set; }
        public string FullContent { get; set; }
        public string ContainedUniqueValues { get; set; }
        public string RelevantContent { get; set; }
        public string PurchasedItems { get; set; }
        public bool? ContainsDeals { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltpimages Img { get; set; }
        public virtual Oltpreceipts Receipt { get; set; }
    }
}
