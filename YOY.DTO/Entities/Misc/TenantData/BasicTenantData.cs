using System;

namespace YOY.DTO.Entities.Misc.TenantData
{
    public class BasicTenantData
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public Guid Logo { set; get; }
        public Guid CountryId { set; get; }
        public Guid CategoryId { set; get; }
        public string CategoryName { set; get; }
        public int Type { set; get; }
        public string CurrencySymbol { set; get; }
        public decimal? RelevanceScore { set; get; }
        public Guid? NearestBranchId { set; get; }
        public string NearestBranchName { set; get; }
        public decimal? NearestBranchLatitude { set; get; }
        public decimal? NearestBranchLongitude { set; get; }
        public double? NearesBranchDistance { set; get; }

        //For Club
        public Guid? LandingImg { set; get; }
        public Guid MemberShipId { set; get; }
        public bool? IsMember { set; get; }
        public decimal? PointsBalance { set; get; }
    }
}
