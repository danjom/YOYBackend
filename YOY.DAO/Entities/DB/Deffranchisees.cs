using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Deffranchisees
    {
        public Deffranchisees()
        {
            Defbranches = new HashSet<Defbranches>();
            DefreceiptAnalyzerConfigs = new HashSet<DefreceiptAnalyzerConfigs>();
            Oltpreceipts = new HashSet<Oltpreceipts>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public bool? IsActive { get; set; }
        public bool Deleted { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string TaxAddress { get; set; }
        public string PaymentSubject { get; set; }
        public string AdditionalNotes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Deftenants Tenant { get; set; }
        public virtual ICollection<Defbranches> Defbranches { get; set; }
        public virtual ICollection<DefreceiptAnalyzerConfigs> DefreceiptAnalyzerConfigs { get; set; }
        public virtual ICollection<Oltpreceipts> Oltpreceipts { get; set; }
    }
}
