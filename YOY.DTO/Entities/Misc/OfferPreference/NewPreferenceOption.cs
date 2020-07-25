using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities.Misc.OfferPreference
{
    public class NewPreferenceOption
    {
        public Guid PreferenceId { set; get; }
        public Guid OfferId { set; get; }
        public string Value { set; get; }
        public decimal Price { set; get; }
        public decimal? RegularPrice { set; get; }
        public Guid? ImageId { set; get; }
        public bool ReplacesOfferImageOnSelect { set; get; }
    }
}
