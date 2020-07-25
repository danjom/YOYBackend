using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpmonetaryFeeLogsView
    {
        public Guid Id { get; set; }
        public Guid GeneratorTenantId { get; set; }
        public Guid? GeneratorBranchId { get; set; }
        public string GeneratorTenantName { get; set; }
        public string GeneratorContactName { get; set; }
        public string GeneratorContactEmail { get; set; }
        public string GeneratorContactPhone { get; set; }
        public string GeneratorBranchName { get; set; }
        public Guid DebtorTenantId { get; set; }
        public string DebtorTenantName { get; set; }
        public int DebtorPaymentDay { get; set; }
        public string DebtorContactName { get; set; }
        public string DebtorContactEmail { get; set; }
        public string DebtorContactPhone { get; set; }
        public Guid? DebtorFranchiseeId { get; set; }
        public string DebtorFranchiseeLegalName { get; set; }
        public string DebtorFranchiseeContactName { get; set; }
        public string DebtorFranchiseeContactEmail { get; set; }
        public string DebtorFranchiseeContactPhoneNumber { get; set; }
        public Guid CollectorTenantId { get; set; }
        public string CollectorTenantName { get; set; }
        public string CollectorContactName { get; set; }
        public string CollectorContactEmail { get; set; }
        public string CollectorContactPhone { get; set; }
        public Guid? CollectorFranchiseeId { get; set; }
        public string UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CollectionDueDate { get; set; }
        public int Type { get; set; }
        public int Reason { get; set; }
        public int RefType { get; set; }
        public Guid RefId { get; set; }
        public int Status { get; set; }
        public decimal Amount { get; set; }
        public string CurrencySymbol { get; set; }
        public int CurrencyType { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
