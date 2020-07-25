using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class MoneyWithdrawal
    {
        public Guid Id { set; get; }
        public string UserId { set; get; }
        public Guid MembershipId { set; get; }
        public Guid? LiquidationMoneyTransferId { set; get; }
        public int TransferType { set; get; }
        public string TransferTypeName { set; get; }
        public decimal RequiredPoints { set; get; }
        public double MonetaryConversionFactor { set; get; }
        public string BeneficiaryName { set; get; }
        public string BeneficiaryId { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public Guid CountryId { set; get; }
        public string CountryName { set; get; }
        public decimal LocalCurrencyAmount { set; get; }
        public decimal LocalCurrencyRetainedTaxesAmount { set; get; }
        public decimal LocalCurrencyCommissionFee { set; get; }
        public string CurrencySymbol { set; get; }
        public int CurrencyType { set; get; }
        public string CurrrencyTypeName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public int ServiceAccontRefType { set; get; }
        public string ServiceAccountRefId { set; get; }
        public string ServiceAccountType { set; get; }
        public string ServiceInstanceName { set; get; }
        public string WithdrawCode { set; get; }
        public string FollowUpCode { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string UpdatedUserModifierId { set; get; }
        public DateTime? ExpirationDate { set; get; }
    }
}
