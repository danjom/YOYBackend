using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltpreceipts
    {
        public Oltpreceipts()
        {
            OltpreceiptPictures = new HashSet<OltpreceiptPictures>();
            OltpreceiptRequestedValidations = new HashSet<OltpreceiptRequestedValidations>();
            OltpvalidatePurchaseRegistries = new HashSet<OltpvalidatePurchaseRegistries>();
        }

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? PointsOpId { get; set; }
        public Guid? FranchiseeId { get; set; }
        public int ClaimMarkType { get; set; }
        public string ClaimMark { get; set; }
        public bool ClaimerSubmit { get; set; }
        public bool ValidStructure { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
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
        public string ContainedUniqueValues { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string PurchasedItems { get; set; }
        public string ContainedDeals { get; set; }
        public bool ContainsDeals { get; set; }
        public bool LoyaltyValidation { get; set; }
        public decimal? TaxAmount { get; set; }
        public bool? PreTaxAmountFound { get; set; }
        public decimal? PreTaxAmount { get; set; }
        public bool? TotalAmountFound { get; set; }
        public decimal? TotalAmount { get; set; }
        public string PurchaseItemsPrices { get; set; }
        public int? AmountsMatchStatus { get; set; }
        public bool? TotalAmountInRange { get; set; }
        public bool? ConfirmedByUser { get; set; }
        public decimal? UserEarnedPoints { get; set; }
        public int? PointsType { get; set; }
        public decimal? CommissionFeeAmount { get; set; }
        public decimal? RetainedTaxAmount { get; set; }
        public decimal? UserEarnedMoneyAmount { get; set; }
        public bool? NetworkEarningInvolved { get; set; }

        public virtual Deffranchisees Franchisee { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<OltpreceiptPictures> OltpreceiptPictures { get; set; }
        public virtual ICollection<OltpreceiptRequestedValidations> OltpreceiptRequestedValidations { get; set; }
        public virtual ICollection<OltpvalidatePurchaseRegistries> OltpvalidatePurchaseRegistries { get; set; }
    }
}
