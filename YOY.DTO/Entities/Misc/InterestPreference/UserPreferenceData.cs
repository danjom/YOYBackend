using System;

namespace YOY.DTO.Entities.Manager.Misc.InterestPreference
{
    public class UserPreferenceData
    {
        public Guid Id { set; get; }
        public int Type { set; get; }
        public string Name { set; get; }
        public bool IsSelected { set; get; }
        public string BaseImgUrl { set; get; }
        public string SelectedImgUrl { set; get; }
        public string UnSeletedImgUrl { set; get; }
    }
}