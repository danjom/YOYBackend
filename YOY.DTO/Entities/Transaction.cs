using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace YOY.DTO.Entities
{
    public class Transaction
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string UserId { set; get; }
        public string UserName { set; get; }

        public int TransactionType { set; get; }
        public string TransactionTypeName { set; get; }
        public Guid? ReferenceId { set; get; }
        public int? ReferenceType { set; get; }
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public string Code { set; get; }
        public Guid? CodeImg { set; get; }
        public int ClaimPoints { set; get; }
        public decimal Value { set; get; }
        public decimal? RegularValue { set; get; }
        public string Description { set; get; }
        public  string Name { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string CreatedDateLiteral { set; get; }
        public bool Completed { set; get; }
        public Guid? Creator { set; get; }
        public string CreatorName { set; get; }
        public int OriginId { set; get; }
        public string OriginName { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime? CompletedDate { set; get; }
        public DateTime? ExpirationDate { set; get; }
        public int TotalClaimCount { set; get; }
        public int TransactionClaimCount { set; get; }
        public bool OneTimeClaim { set; get; }
        public bool ShowToUser { set; get; }
        public double? Score { set; get; }
        public string Comment { set; get; }
        public Guid? CategoryId { set; get; }
        public int PointsEarnStatus { set; get; }
        public int GeneratedPoints { set; get; }
        public bool Validated { set; get; }
    }
}
