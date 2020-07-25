using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpofferPreferences
    {
        public OltpofferPreferences()
        {
            OltpofferPreferenceOptions = new HashSet<OltpofferPreferenceOptions>();
        }

        public Guid Id { get; set; }
        public Guid OfferId { get; set; }
        public string Name { get; set; }
        public string Hint { get; set; }
        public int InputType { get; set; }
        public int MinOptionsSelected { get; set; }
        public int MaxOptionsSelected { get; set; }
        public bool Mandatory { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltpoffers Offer { get; set; }
        public virtual ICollection<OltpofferPreferenceOptions> OltpofferPreferenceOptions { get; set; }
    }
}
