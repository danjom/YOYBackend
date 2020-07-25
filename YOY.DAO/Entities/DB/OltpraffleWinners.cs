using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpraffleWinners
    {
        public Guid RaffleId { get; set; }
        public string UserId { get; set; }
        public bool Claimed { get; set; }
        public DateTime? ClaimDate { get; set; }
        public Guid? ClaimBranchId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Defbranches ClaimBranch { get; set; }
        public virtual Oltprewards Raffle { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
