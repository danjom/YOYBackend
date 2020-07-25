using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpmonetaryFeeLogs
    {
        public OltpmonetaryFeeLogs()
        {
            OltpmembershipPointsOperations = new HashSet<OltpmembershipPointsOperations>();
        }

        public Guid Id { get; set; }
        public Guid GeneratorTenantId { get; set; }
        public Guid? GeneratorBranchId { get; set; }
        public Guid DebtorTenantId { get; set; }
        public Guid? DebtorFranchiseeId { get; set; }
        public Guid CollectorTenantId { get; set; }
        public Guid? CollectorFranchiseeId { get; set; }
        public string UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? CollectionDueDate { get; set; }
        public int Type { get; set; }
        public int Reason { get; set; }
        public Guid RefId { get; set; }
        public int RefType { get; set; }
        public int Status { get; set; }
        public string CurrencySymbol { get; set; }
        public int CurrencyType { get; set; }
        public decimal Amount { get; set; }

        public virtual Deftenants GeneratorTenant { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<OltpmembershipPointsOperations> OltpmembershipPointsOperations { get; set; }
    }
}
