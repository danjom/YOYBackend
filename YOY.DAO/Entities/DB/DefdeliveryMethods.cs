using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefdeliveryMethods
    {
        public DefdeliveryMethods()
        {
            DefbranchDeliveryMethods = new HashSet<DefbranchDeliveryMethods>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string IconName { get; set; }

        public virtual ICollection<DefbranchDeliveryMethods> DefbranchDeliveryMethods { get; set; }
    }
}
