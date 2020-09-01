using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.CashierAPI.Models.v1.Access.POCO
{
    public class BranchData
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string AccessKey { set; get; }
    }
}
