using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.DealPreference.POCO
{
    public class DealPreferenceData
    {
        public Guid Id { set; get; }
        public Guid DealId { set; get; }
        public Guid TenantId { set; get; }
        public string Name { set; get; }
        public string Hint { set; get; }
        public int InputType { set; get; }
        public string InputTypeName { set; get; }
        public int MinRequiredSelectedOptions { set; get; }
        public int MaxRequiredOptions { set; get; }
        public bool IsMandatory { set; get; }
        public List<DealPreferenceOption> Options { set; get; }
    }
}
