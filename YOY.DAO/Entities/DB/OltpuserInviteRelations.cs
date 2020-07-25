using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserInviteRelations
    {
        public OltpuserInviteRelations()
        {
            InverseParentRelation = new HashSet<OltpuserInviteRelations>();
        }

        public Guid Id { get; set; }
        public string AncestorUserId { get; set; }
        public string JoinedUserId { get; set; }
        public Guid? ParentRelationId { get; set; }
        public int HerarchyLevel { get; set; }
        public decimal JoiningFirstPurchaseMoney { get; set; }
        public bool JoiningFirstPurchaseMoneyGranted { get; set; }
        public decimal JoiningBonusCommissionMoney { get; set; }
        public decimal RemainingBonusCommissionMoney { get; set; }
        public double JoiningBonusCommissionPercentage { get; set; }
        public decimal AncestorFirstPurchaseMoney { get; set; }
        public bool AncestorFirstPurchaseMoneyGranted { get; set; }
        public double AncestorBonusCommisionPercentage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime JoiningBonusExpirationDate { get; set; }
        public DateTime? AncestorBonusExpirationDate { get; set; }

        public virtual OltpuserInviteRelations ParentRelation { get; set; }
        public virtual ICollection<OltpuserInviteRelations> InverseParentRelation { get; set; }
    }
}
