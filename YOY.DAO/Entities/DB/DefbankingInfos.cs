using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefbankingInfos
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string BankName { get; set; }
        public string AccNum1 { get; set; }
        public string AccNum2 { get; set; }
        public string AccNum3 { get; set; }
        public Guid CountryId { get; set; }
        public int Type { get; set; }
        public int CurrencyType { get; set; }
        public bool MainAcc { get; set; }
        public bool? IsActive { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Defbranches Branch { get; set; }
        public virtual Defcountries Country { get; set; }
        public virtual Deftenants Tenant { get; set; }
    }
}
