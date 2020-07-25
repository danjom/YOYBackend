using System;

namespace YOY.DTO.Entities.Misc.MembershipOperation
{
    public class PointOpToTransfer
    {
        public Guid OpId { set; get; }
        public decimal PointsToTransfer { set; get; }
        public DateTime PointsExpirationDate { set; get; }
    }
}
