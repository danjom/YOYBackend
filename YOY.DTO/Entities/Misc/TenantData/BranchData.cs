using System;
using System.Collections.Generic;

namespace YOY.DTO.Entities.Misc.TenantData
{
    public class BranchData
    {
        public DTO.Entities.Branch Branch { set; get; }
        public List<string> Address { set; get; }
        public List<Structure.POCO.Pair<Guid, string>> Schedules { set; get; }
        public List<PaymentMethod> PaymentMethods { set; get; }
        public List<DeliveryMethod> DeliveryMethods { set; get; }
    }
}
