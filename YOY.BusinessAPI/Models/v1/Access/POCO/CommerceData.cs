using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.Access.POCO
{
    public class CommerceData
    {
        public Guid Id { set; get; }
        public Guid EmployeeId { set; get; }
        public string Name { set; get; }
        public string Logo { set; get; }
        public string RoleName { set; get; }
        public string CurrencySymbol { set; get; }
        public string CurrencyName { set; get; }
        public int CurrencyType { set; get; }
        public int AccessLevel { set; get; }
        public List<BranchData> Branches { set; get; }
    }
}
