using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Temppreferences
    {
        public Guid InterestId { get; set; }
        public string UserId { get; set; }
        public int? InterestType { get; set; }
        public string Name { get; set; }
        public int? Relevance { get; set; }
        public string Icon { get; set; }
        public bool? IsActive { get; set; }
        public decimal? Score { get; set; }
        public int? OriginType { get; set; }
        public int? HerarchyLevel { get; set; }
    }
}
