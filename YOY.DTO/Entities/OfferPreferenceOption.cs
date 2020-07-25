using System;

namespace YOY.DTO.Entities
{
    public class OfferPreferenceOption
    {
        public Guid Id { set; get; }
        public Guid PreferenceId { set; get; }
        public Guid OfferId { set; get; }
        public string Value { set; get; }
        public decimal Price { set; get; }
        public decimal? RegularPrice { set; get; }
        public Guid? ImageId { set; get; }
        public bool ReplacesOfferImgOnSelect { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
