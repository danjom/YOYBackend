using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class RewardToAward
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string UserId { set; get; }
        public Guid OfferId { set; get; }
        public Guid OriginatorId { set; get; }
        public int OriginatorType { set; get; }
        public string OriginatorName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public DateTime CreationDate { set; get; }
        public DateTime ExpirationDate { set; get; }
    }
}
