using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefconfigValues
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool? Enabled { get; set; }
        public int LastestAndroidVersion { get; set; }
        public string LastestiOsversion { get; set; }
        public string LastestWebVersion { get; set; }
        public string LastestFbversion { get; set; }
        public string SupportEmail { get; set; }
        public string SupportNumber { get; set; }
        public string CompanyName { get; set; }
        public string AppName { get; set; }
    }
}
