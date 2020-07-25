using System;

namespace YOY.DTO.Entities
{
    public class ConfigValues
    {
        public int Id { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public bool Enabled { set; get; }
        public int LastestAndroidVersion { set; get; }
        public string LastestiOSVersion { set; get; }
        public string LastestWebVersion { set; get; }
        public string LastestFBVersion { set; get; }
        public string SupportEmail { set; get; }
        public string SupportNumber { set; get; }
        public string CompanyName { set; get; }
        public string AppName { set; get; }
    }
}
