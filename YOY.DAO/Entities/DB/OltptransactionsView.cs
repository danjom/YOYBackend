using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltptransactionsView
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid? CreatorId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? CategoryId { get; set; }
        public int TransactionType { get; set; }
        public int? ReferenceType { get; set; }
        public Guid? ReferenceId { get; set; }
        public int ClaimPoints { get; set; }
        public decimal Value { get; set; }
        public decimal? RegularValue { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? CodeImg { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int OriginId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int TransactionClaimCount { get; set; }
        public int? TotalClaimCount { get; set; }
        public bool OneTimeClaim { get; set; }
        public bool ShowToUser { get; set; }
        public double? Score { get; set; }
        public string Comment { get; set; }
        public string SelectedAspects { get; set; }
        public int DealType { get; set; }
        public int PointsEarnStatus { get; set; }
        public int GeneratedPoints { get; set; }
        public bool Validated { get; set; }
    }
}
