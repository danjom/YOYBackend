using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpmoneyTransfers
    {
        public OltpmoneyTransfers()
        {
            OltpmoneyWithdrawals = new HashSet<OltpmoneyWithdrawals>();
            OltppaymentLogs = new HashSet<OltppaymentLogs>();
        }

        public Guid Id { get; set; }
        public decimal TransferedAmount { get; set; }
        public int CurrencyType { get; set; }
        public string BeneficiaryId { get; set; }
        public int BeneficiaryType { get; set; }
        public string BeneficiaryName { get; set; }
        public Guid? DestinationId { get; set; }
        public int DestionationType { get; set; }
        public string DestinationName { get; set; }
        public string ReferenceCode { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifierUserId { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual AspNetUsers ModifierUser { get; set; }
        public virtual ICollection<OltpmoneyWithdrawals> OltpmoneyWithdrawals { get; set; }
        public virtual ICollection<OltppaymentLogs> OltppaymentLogs { get; set; }
    }
}
