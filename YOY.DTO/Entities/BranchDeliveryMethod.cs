using System;

namespace YOY.DTO.Entities
{
    public class BranchDeliveryMethod
    {
        public Guid BranchId { set; get; }
        public Guid MethodId { set; get; }

        /*
        [Display(Name = "IsActiveQuestion", ResourceType = typeof(Resources.Resources))]
        public bool IsActive { set; get; }

        [Display(Name = "UnitPrice", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "UnitPriceRequired")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "InvalidUnitPrice")]
        public decimal UnitPrice { set; get; }

        [Display(Name = "MaxItemsPerDelivery", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "MaxItemsPerDeliveryRequired")]
        [Range(-1, 1000, ErrorMessageResourceType = typeof(Resources.Resources),
                  ErrorMessageResourceName = "InvalidMaxItemsPerDelivery")]
        public int MaxItemsPerDelivery { set; get; }

        [Display(Name = "FixedPrice", ResourceType = typeof(Resources.Resources))]
        public bool FixedPrice { set; get; }

        [Display(Name = "UnitDistance", ResourceType = typeof(Resources.Resources))]
        public float UnitDistance { set; get; }
        */
    }
}
