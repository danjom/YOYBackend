using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class GeoLocatedTenantsView
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid? Logo { get; set; }
        public Guid? LandingImg { get; set; }
        public Guid CountryId { get; set; }
        public int RelevanceStatus { get; set; }
        public Guid StateId { get; set; }
        public string State { get; set; }
        public string CurrencySymbol { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TypeId { get; set; }
        public int BusinessStructureType { get; set; }
        public int PayerType { get; set; }
        public int UtcTimeZone { get; set; }
        public int Language { get; set; }
        public bool Released { get; set; }
        public int InstanceType { get; set; }
    }
}
