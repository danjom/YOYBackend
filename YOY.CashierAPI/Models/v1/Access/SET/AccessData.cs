using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.CashierAPI.Models.v1.Access.POCO;

namespace YOY.CashierAPI.Models.v1.Access.SET
{
    public class AccessData
    {
        public string Username { set; get; }
        public bool HasAccess { set; get; }
        public List<CommerceData> Commerces { set; get; }
    }
}
