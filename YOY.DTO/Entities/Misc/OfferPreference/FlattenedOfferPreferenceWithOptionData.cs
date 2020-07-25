using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace YOY.DTO.Entities.Misc.OfferPreference
{
    public class FlatOfferPreferenceWithOptionData
    {
        public Guid PreferenceId { set; get; }
        public Guid OfferId { set; get; }
        public string PreferenceName { set; get; }
        public string PreferenceHint { set; get; }
        public int PreferenceInputType { set; get; }
        public int PreferenceMinOptionsSelected { set; get; }
        public int PreferenceMaxOptionsSelected { set; get; }
        public bool PreferenceIsMandatory { set; get; }
        public bool PreferenceIsActive { set; get; }
        public DateTime PreferenceCreatedDate { set; get; }
        public DateTime PreferenceUpdatedDate { set; get; }
        public Guid? OptionId { set; get; }
        public string OptionValue { set; get; }
        public decimal? OptionPrice { set; get; }
        public decimal? OptionRegularPrice { set; get; }
        public Guid? OptionImgId { set; get; }
        public bool? OptionImgReplacesOfferOnSelect { set; get; }
        public bool? OptionIsActive { set; get; }
        public DateTime? OptionCreatedDate { set; get; }
        public DateTime? OptionUpdatedDate { set; get; }
    }
}
