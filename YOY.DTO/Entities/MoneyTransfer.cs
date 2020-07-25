using System;

namespace YOY.DTO.Entities
{
    public class MoneyTransfer
    {
        public Guid Id { set; get; }
        public decimal TransferedAmount { set; get; }
        public int CurrencyType { set; get; }
        public string CurrencyTypeName { set; get; }
        public string BeneficiaryId { set; get; }
        public int BeneficiaryType { set; get; }
        public string BeneficiaryTypeName { set; get; }
        public Guid DestinationId { set; get; }
        public int DestinationType { set; get; }
        public string DestinationTypeName { set; get; }
        public string DestinationName { set; get; }
        public string ReferenceCode { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public DateTime CreatedDate { set; get; }
        public string ModifierUserId { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
