using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpofferPreferenceOptions
    {
        public Guid Id { get; set; }
        public Guid PreferenceId { get; set; }
        public Guid OfferId { get; set; }
        public string Value { get; set; }
        public decimal Price { get; set; }
        public decimal? RegularPrice { get; set; }
        public Guid? ImageId { get; set; }
        public bool ReplacesOfferImgOnSelect { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltpimages Image { get; set; }
        public virtual OltpofferPreferences Preference { get; set; }
    }
}
