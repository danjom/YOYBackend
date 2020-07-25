using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class Receipt
    {
        public Guid Id { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
        public Guid? PointsOpId { set; get; }
        public Guid? FranchiseeId { set; get; }
        public int ClaimMarkType { set; get; }
        public string ClaimMarkTypeName { set; get; }
        public string ClaimMark { set; get; }
        public bool ClaimerSubmit { set; get; }
        public bool ValidStructure { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public int PicturesCount { set; get; }
        public int PictureExtractionsCount { set; get; }
        public int ExtrationStatus { set; get; }
        public string ExtrationStatusName { set; get; }
        public int Purpose { set; get; }
        public string PurposeName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public string BusinessName { set; get; }
        public string LegalName { set; get; }
        public string TaxId { set; get; }
        public string PostalCode { set; get; }
        public string BranchName { set; get; }
        public string TicketNumber { set; get; }
        public string ContainedUniqueValues { set; get; }
        public DateTime? PurchaseDate { set; get; }
        public string PurchasedItems { set; get; }
        public string ContainedDeals { set; get; }
        public bool ContainsDeals { set; get; }
        public bool LoyaltyValidation { set; get; }
        public decimal? TaxAmount { set; get; }
        public bool? PreTaxAmountFound { set; get; }
        public decimal? PreTaxAmount { set; get; }
        public bool? TotalAmountFound { set; get; }
        public decimal? TotalAmount { set; get; }
        public string PurchasedAmountPrices { set; get; }
        public int? AmountsMatchStatus { set; get; }
        public string AmountsMatchStatusName { set; get; }
        public bool? TotalAmountInRange { set; get; }
        public bool? ConfirmedByUser { set; get; }
        public decimal? UserEarnedPoints { set; get; }
        public int? PointsType { set; get; }
        public string PointsTypeName { set; get; }
        public decimal? CommissionFeeAmount { set; get; }
        public decimal? RetainedTaxAmount { set; get; }
        public decimal? UserEarnedMoneyAmount { set; get; }
        public bool? NetworkEarningsInvolved { set; get; }
    }
}
