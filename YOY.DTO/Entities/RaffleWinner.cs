using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class RaffleWinner
    {
        public Guid RaffleI { set; get; }
        public string UserId { set; get; }
        public string UserName { set; get; }
        public string UserEmail { set; get; }
        public bool Claimed { set; get; }
        public DateTime ClaimedDate { set; get; }
        public Guid ClaimBranchId { set; get; }
        public string ClaimBranchName { set; get; }
    }
}
