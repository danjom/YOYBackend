using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.Misc.POCO;

namespace YOY.BusinessAPI.Models.v1.Deal.POCO
{
    public class DealData
    {
        [Required]
        public Guid Id { set; get; }
        [Required]
        public Guid TenantId { set; get; }
        [Required]
        public Guid MainCategoryId { set; get; }
        [AllowNull]
        public string MainCategoryName { set; get; }
        [Required]
        public int OfferType { set; get; }
        [AllowNull]
        public string OfferTypeName { set; get; }
        [Required]
        public int DealType { set; get; }
        [AllowNull]
        public string DealTypeName { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string MainHint { set; get; }
        [Required]
        public string ComplementaryHint { set; get; }
        [Required]
        public string Keywords { set; get; }
        [Required]
        public string Code { set; get; }
        [Required]
        public string Description { set; get; }
        [Required]
        public bool IsActive { set; get; }
        [Required]
        public bool IsExclusive { set; get; }
        [Required]
        public bool IsSponsored { set; get; }
        [Required]
        public bool HasPreferences { set; get; }
        [Required]
        public int AvailableQuantity { set; get; }
        [Required]
        public string ClaimLocation { set; get; }
        [Required]
        public decimal Value { set; get; }
        [Required]
        public decimal RegularValue { set; get; }
        [Required]
        public bool HasExtraBonus { set; get; }
        [AllowNull]
        public double ExtraBonus { set; get; }
        [AllowNull]
        public int ExtraBonusType { set; get; }
        [AllowNull]
        public string ExtraBonusTypeName { set; get; }
        [AllowNull]
        public char GenderParam { set; get; }
        [AllowNull]
        public int StartAgeParam { set; get; }
        [AllowNull]
        public int EndAgeParam { set; get; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReleaseFullDateTime { set; get; }
        [Required]
        [DataType(DataType.DateTime)]
        public string ReleaseDateComponent { set; get; }
        [Required]
        public string ReleaseHourComponent { set; get; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationFullDateTime { set; get; }
        [Required]
        [DataType(DataType.DateTime)]
        public string ExpirationDateComponent { set; get; }
        [Required]
        public string ExpirationHourComponent { set; get; }
        [AllowNull]
        public string DisplayImageUrl { set; get; }
        [AllowNull]
        public int PurchasedCount { set; get; }
        [AllowNull]
        public double RelevanceRate { set; get; }
        [AllowNull]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { set; get; }
        [AllowNull]
        public string PublishedState { set; get; }


    }
}
