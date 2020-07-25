using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class BankingInfo
    {
        public Guid Id { set; get; }
        public string BankName { set; get; }
        public string OwnerName { set; get; }
        public string OwnerId { set; get; }
        public Guid TenantId { set; get; }
        public Guid BranchName { set; get; }
        public string AccNum1 { set; get; }
        public string AccNum2 { set; get; }
        public string AccNum3 { set; get; }
        public Guid CountryId { set; get; }
        public string CountryName { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public int CurrencyType { set; get; }
        public string CurrencyTypeName { set; get; }
        public bool MainAcc { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreationDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
