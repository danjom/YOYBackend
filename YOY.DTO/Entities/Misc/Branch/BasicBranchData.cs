using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.Branch
{
    public class BasicBranchData
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string InquiriesPhoneNumber { set; get; }
        public string DescriptiveAddress { set; get; }
        public Guid StateId { set; get; }
        public Guid CityId { set; get; }
        public decimal Latitude { set; get; }
        public decimal Longitude { set; get; }
        public double? Distance { set; get; }
        public bool Enabled { set; get; }
    }
}
