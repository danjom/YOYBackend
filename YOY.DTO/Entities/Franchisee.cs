using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class Franchisee
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public bool IsActive { set; get; }
        public string ContactName { set; get; }
        public string ContactEmail { set; get; }
        public string ContactPhoneNumber { set; get; }
        public string LegalName { set; get; }
        public string TaxId { set; get; }
        public string TaxAddress { set; get; }
        public string PaymentSubject { set; get; }
        public string AdditionalNotes { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
