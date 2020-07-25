using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefbankingInfosView
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
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CountryFlag { get; set; }
        public string CountryName { get; set; }
        public int CountryCurrencyType { get; set; }
        public string CountryPhonePrefix { get; set; }
        public string TenantName { get; set; }
        public string TenantContactName { get; set; }
        public string TenantContactEmail { get; set; }
        public string TenantContactPhone { get; set; }
        public string BranchName { get; set; }
        public string BranchContactPhone { get; set; }
    }
}
