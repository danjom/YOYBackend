using System;

namespace YOY.DTO.Entities.Misc.ValidPurchaseRegistries
{
    public class ValidPurchaseRegistry
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid MembershipId { set; get; }
        public Guid ReceiptId { set; get; }
        public decimal TotalAmount { set; get; }
        public int PointsGenerationType { set; get; }
        public string PointsGenerationTypeName { set; get; }
        public decimal? ClubPointsGenerated { set; get; }
        public decimal? WalletPointsGenerated { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime ExpirationDate { set; get; }
    }
}
