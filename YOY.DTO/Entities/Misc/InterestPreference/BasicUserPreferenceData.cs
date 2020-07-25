using System;

namespace YOY.DTO.Entities.Misc.InterestPreference
{
    public class BasicUserPreferenceData
    {
        public Guid Id { set; get; }
		public string Name { set; get; }
		public string Icon { set; get; }
        public decimal? RelevanceScore { set; get; }
    }
}
