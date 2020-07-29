using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.Misc.POCO;

namespace YOY.BusinessAPI.Models.v1.Deal.POCO
{
    public class NewDeal
    {
        [Required]
        [NotNull]
        public Guid EmployeeId { set; get; }
        [Required]
        [NotNull]
        public string UserId { set; get; }
        [Required]
        [NotNull]
        public Guid TenantId { set; get; }
        [Required]
        [NotNull]
        public Guid BranchId { set; get; }
        [Required]
        [NotNull]
        public Guid MainCategoryId { set; get; }
        [Required]
        [NotNull]
        public int DealType { set; get; }
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
        [AllowNull]
        [Required]
        public decimal? RegularValue { set; get; }
        [Required]
        [NotNull]
        public int ExtraBonusType { set; get; }
        [Required]
        [NotNull]
        public double ExtraBonus { set; get; }
        [Required]
        [NotNull]
        public char GenderParam { set; get; }
        [Required]
        [NotNull]
        public int StartAgeParam { set; get; }
        [Required]
        [NotNull]
        public int EndAgeParam { set; get; }
        [NotNull]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReleaseDate { set; get; }
        [Required]
        [NotNull]
        public string ReleaseHour { set; get; }
        [Required]
        [NotNull]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { set; get; }
        [Required]
        [NotNull]
        public string ExpirationHour { set; get; }
        [NotNull]
        [Required]
        public string DisplayImgData { set; get; }
        [AllowNull]
        [Required]
        public double? RelevanceRate { set; get; }
    }
}
