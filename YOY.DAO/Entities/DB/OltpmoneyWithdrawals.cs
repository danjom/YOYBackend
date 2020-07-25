using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpmoneyWithdrawals
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid MembershipId { get; set; }
        public Guid? LiquidationMoneyTransferId { get; set; }
        public int TransferType { get; set; }
        public decimal RequiredPoints { get; set; }
        public double MonetaryConversionFactor { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid CountryId { get; set; }
        public decimal LocalCurrencyAmount { get; set; }
        public decimal LocalCurrencyRetainedTaxesAmount { get; set; }
        public decimal LocalCurrencyCommissionFee { get; set; }
        public string CurrencySymbol { get; set; }
        public int CurrencyType { get; set; }
        public int Status { get; set; }
        public int ServiceAccountRefType { get; set; }
        public string ServiceAccountRefId { get; set; }
        public string ServiceAccountType { get; set; }
        public string ServiceInstanceName { get; set; }
        public string WithdrawalCode { get; set; }
        public string FollowUpCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdateUserModifierId { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public virtual Defcountries Country { get; set; }
        public virtual OltpmoneyTransfers LiquidationMoneyTransfer { get; set; }
        public virtual Oltpmemberships Membership { get; set; }
        public virtual AspNetUsers UpdateUserModifier { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
