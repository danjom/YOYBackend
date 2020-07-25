using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.ClaimRecord
{
    public class ClaimRecordLineValidationData
    {
        public Guid Id { set; get; }
        public Guid TransactionId { set; get; }
        public string ClaimRefCode { set; get; }
        public DateTime ClaimDate { set; get; }
    }
}
