using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.Access.POCO
{
    public class TenantData
    {
        public Guid TenantId { set; get; }
        public Guid? Logo { set; get; }
        public string Name { set; get; }
        public int CurrencyType { set; get; }
        public string CurrencyTypeName { set; get; }
        public string CurrencySymbol { set; get; }
    }
}
