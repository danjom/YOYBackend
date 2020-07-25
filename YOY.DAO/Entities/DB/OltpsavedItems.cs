using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpsavedItems
    {
        public Guid Id { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public Guid TenantId { get; set; }
        public Guid? TenantHolderId { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Deftenants Tenant { get; set; }
        public virtual OltpproductItemTenantHolders TenantHolder { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
