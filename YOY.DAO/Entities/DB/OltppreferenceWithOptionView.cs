using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltppreferenceWithOptionView
    {
        public Guid PreferenceId { get; set; }
        public Guid OfferId { get; set; }
        public string PreferenceName { get; set; }
        public string PreferenceHint { get; set; }
        public int PreferenceInputType { get; set; }
        public int PreferenceMinOptionsSelected { get; set; }
        public int PreferenceMaxOptionsSelected { get; set; }
        public bool PreferenceIsMandatory { get; set; }
        public bool PreferenceIsActive { get; set; }
        public DateTime PreferenceCreatedDate { get; set; }
        public DateTime PreferenceUpdatedDate { get; set; }
        public Guid? OptionId { get; set; }
        public string OptionValue { get; set; }
        public decimal? OptionPrice { get; set; }
        public decimal? OptionRegularPrice { get; set; }
        public Guid? OptionImgId { get; set; }
        public bool? OptionImgReplacesOfferOnSelect { get; set; }
        public bool? OptionIsActive { get; set; }
        public DateTime? OptionCreatedDate { get; set; }
        public DateTime? OptionUpdatedDate { get; set; }
    }
}
