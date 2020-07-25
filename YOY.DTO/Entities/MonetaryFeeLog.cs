using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class MonetaryFeeLog
    {
        public Guid Id { set; get; }
        public Guid GeneratorTenantId { set; get; }
        public string GeneratorTenantName { set; get; }
        public string GeneratorContactName { set; get; }
        public string GeneratorContactEmail { set; get; }
        public string GeneratorContactPhone { set; get; }
        public Guid? GeneratorBranchId { set; get; }
        public string GeneratorBranchName { set; get; }
        public Guid DebtorTenantId { set; get; }
        public string DebtorTenantName { set; get; }
        public string DebtorContactName { set; get; }
        public string DebtorContactEmail { set; get; }
        public string DebtorContactPhone { set; get; }
        public int DebtorTenantPaymentDay { set; get; }
        public Guid CollectorTenantId { set; get; }
        public string CollectorTenantName { set; get; }
        public string CollectorContactName { set; get; }
        public string CollectorContactEmail { set; get; }
        public string CollectorContactPhone { set; get; }
        public string UserId { set; get; }
        public Guid? EmployeeId { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime? CollectionDueDate { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public int Reason { set; get; }
        public string ReasonName { set; get; }
        public Guid? RefId { set; get; }
        public int RefType { set; get; }
        public string RefTypeName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public string CurrencySymbol { set; get; }
        public int CurrencyType { set; get; }
        public string CurrencyTypeName { set; get; }
        public decimal Amount { set; get; }
    }
}
