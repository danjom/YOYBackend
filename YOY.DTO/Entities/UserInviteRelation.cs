using System;

namespace YOY.DTO.Entities
{
    public class UserInviteRelation
    {
        public Guid Id { set; get; }
        public string AncestorUserId { set; get; }
        public string JoinedUserId { set; get; }
        public Guid? ParentRelationId { set; get; }
        public int HerarchyLevel { set; get; }
        public decimal JoiningFirstPurchaseMoney { set; get; }
        public bool JoiningFirstPurchaseMoneyGranted { set; get; }
        public decimal JoiningBonusCommissionMoney { set; get; }
        public decimal RemainingBonusCommissionMoney { set; get; }
        public double JoiningBonusCommissionPercentage { set; get; }
        public decimal AncestorFirstPurchaseMoney { set; get; }
        public bool AncestorFirstPurchaseMoneyGranted { set; get; }
        public double AncestorBonusCommisionPercentage { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime JoiningBonusExpirationDate { set; get; }
        public DateTime? AncestorBonusExpirationDate { set; get; }
    }
}
