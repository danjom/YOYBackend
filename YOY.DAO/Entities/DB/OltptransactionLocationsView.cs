using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltptransactionLocationsView
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public Guid? BroadcasterId { get; set; }
        public string BroadcasterName { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ActionType { get; set; }
        public bool Valid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
