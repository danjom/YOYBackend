using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpreceiptsView
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? FranchiseeId { get; set; }
        public int ClaimMarkType { get; set; }
        public string ClaimMark { get; set; }
        public bool ClaimerSubmit { get; set; }
        public bool ValidStructure { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PicturesCount { get; set; }
        public int PictureExtractionsCount { get; set; }
        public int ExtractionStatus { get; set; }
        public int Purpose { get; set; }
        public int Status { get; set; }
        public string BusinessName { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string PostalCode { get; set; }
        public string BranchName { get; set; }
        public string TicketNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string PurchasedItems { get; set; }
        public string ContainedDeals { get; set; }
        public bool ContainsDeals { get; set; }
        public bool LoyaltyValidation { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool? ConfirmedByUser { get; set; }
        public Guid PictureId { get; set; }
        public Guid PictureReceiptId { get; set; }
        public Guid PictureImgId { get; set; }
        public int PicturePosition { get; set; }
        public int PictureExtractionStatus { get; set; }
        public string PictureFullContent { get; set; }
        public string PictureRelevantContent { get; set; }
        public string PicturePurchasedItems { get; set; }
        public bool? PictureContainsDeals { get; set; }
    }
}
