using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defcountries
    {
        public Defcountries()
        {
            DefbankingInfos = new HashSet<DefbankingInfos>();
            Defgeozones = new HashSet<Defgeozones>();
            Defstates = new HashSet<Defstates>();
            DeftenantInformations = new HashSet<DeftenantInformations>();
            OltpmoneyWithdrawals = new HashSet<OltpmoneyWithdrawals>();
            OltppaymentInfos = new HashSet<OltppaymentInfos>();
            Oltpsearchables = new HashSet<Oltpsearchables>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string LanguageCode { get; set; }
        public string PhoneNumberPrefix { get; set; }
        public int CurrencyType { get; set; }
        public string CurrencySymbol { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public string Flag { get; set; }
        public int ContentSegmentationType { get; set; }

        public virtual ICollection<DefbankingInfos> DefbankingInfos { get; set; }
        public virtual ICollection<Defgeozones> Defgeozones { get; set; }
        public virtual ICollection<Defstates> Defstates { get; set; }
        public virtual ICollection<DeftenantInformations> DeftenantInformations { get; set; }
        public virtual ICollection<OltpmoneyWithdrawals> OltpmoneyWithdrawals { get; set; }
        public virtual ICollection<OltppaymentInfos> OltppaymentInfos { get; set; }
        public virtual ICollection<Oltpsearchables> Oltpsearchables { get; set; }
    }
}
