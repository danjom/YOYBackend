using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.Misc.POCO;

namespace YOY.BusinessAPI.Models.v1.Deal.POCO
{
    public class UpdatedDeal
    {
        [Required]
        [NotNull]
        public Guid EmployeeId { set; get; }
        [Required]
        [NotNull]
        public string UserId { set; get; }
        [Required]
        [NotNull]
        public Guid Id { set; get; }
        [Required]
        [NotNull]
        public Guid TenantId { set; get; }
        [Required]
        [NotNull]
        public int OfferType { set; get; }
        [Required]
        [NotNull]
        public Guid MainCategoryId { set; get; }
        [Required]
        [NotNull]
        public string Name { set; get; }
        [Required]
        [NotNull]
        public string MainHint { set; get; }
        [Required]
        [NotNull]
        public string ComplementaryHint { set; get; }
        [Required]
        [NotNull]
        public string Keywords { set; get; }
        [Required]
        [NotNull]
        public string Code { set; get; }
        [Required]
        [NotNull]
        public string Description { set; get; }
        [AllowNull]
        public bool? IsActive { set; get; }
        [Required]
        [NotNull]
        public bool IsExclusive { set; get; }
        [Required]
        [NotNull]
        public bool IsSponsored { set; get; }
        [Required]
        [NotNull]
        public int AvailableQuantity { set; get; }
        [Required]
        [NotNull]
        public string ClaimLocation { set; get; }
        [Required]
        [NotNull]
        public decimal Value { set; get; }
        [Required]
        [NotNull]
        public decimal RegularValue { set; get; }
        [AllowNull]
        [NotNull]
        public double ExtraBonus { set; get; }
        [AllowNull]
        [NotNull]
        public int ExtraBonusType { set; get; }
        [Required]
        [NotNull]
        public char GenderParam { set; get; }
        [Required]
        [NotNull]
        public int StartAgeParam { set; get; }
        [Required]
        [NotNull]
        public int EndAgeParam { set; get; }
        [Required]
        [NotNull]
        [DataType(DataType.DateTime)]
        public DateTime ReleaseDate { set; get; }
        [AllowNull]
        public string ReleaseHour { set; get; }
        [Required]
        [NotNull]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { set; get; }
        [AllowNull]
        public string ExpirationHour { set; get; }
        [AllowNull]
        public string DisplayImageUrl { set; get; }
        [AllowNull]
        public string DisplayImgData { set; get; }
        [Required]
        public double? RelevanceRate { set; get; }
    }
}
