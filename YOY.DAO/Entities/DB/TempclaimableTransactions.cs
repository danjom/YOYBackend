﻿using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempclaimableTransactions
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? CategoryId { get; set; }
        public int TransactionType { get; set; }
        public Guid? ReferenceId { get; set; }
        public int? ReferenceType { get; set; }
        public int ClaimPoints { get; set; }
        public int? TotalClaimsCount { get; set; }
        public decimal Value { get; set; }
        public decimal? RegularValue { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? CodeImg { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int OriginId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int TransactionClaimCount { get; set; }
        public bool OneTimeClaim { get; set; }
        public bool ShowToUser { get; set; }
        public int DealType { get; set; }
        public int? AvailableQuantity { get; set; }
        public int? GeoSegmentationType { get; set; }
        public int? OfferClaimCount { get; set; }
        public string TenantName { get; set; }
        public Guid TenantLogo { get; set; }
        public Guid TenantCountryId { get; set; }
        public int TenantType { get; set; }
        public Guid TenantCategoryId { get; set; }
        public string TenantCategoryName { get; set; }
        public string CurrencySymbol { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchInquiriesPhoneNumber { get; set; }
        public string BranchDescriptiveAddress { get; set; }
        public Guid BranchCityId { get; set; }
        public Guid BranchStateId { get; set; }
        public decimal BranchLatitude { get; set; }
        public decimal BranchLongitude { get; set; }
    }
}
