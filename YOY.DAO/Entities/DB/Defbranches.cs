using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defbranches
    {
        public Defbranches()
        {
            DefbankingInfos = new HashSet<DefbankingInfos>();
            DefbranchDeliveryMethods = new HashSet<DefbranchDeliveryMethods>();
            DefbranchPaymentMethods = new HashSet<DefbranchPaymentMethods>();
            DefbranchSchedules = new HashSet<DefbranchSchedules>();
            Defbroadcasters = new HashSet<Defbroadcasters>();
            OltpbroadcastingPlayerLogs = new HashSet<OltpbroadcastingPlayerLogs>();
            Oltpemployees = new HashSet<Oltpemployees>();
            OltppaymentLogs = new HashSet<OltppaymentLogs>();
            OltppaymentRequests = new HashSet<OltppaymentRequests>();
            OltppurchaseDeliveryDetails = new HashSet<OltppurchaseDeliveryDetails>();
            Oltppurchases = new HashSet<Oltppurchases>();
            OltpraffleWinners = new HashSet<OltpraffleWinners>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid? FranchiseeId { get; set; }
        public Guid StateId { get; set; }
        public Guid CityId { get; set; }
        public Guid? BranchHolderId { get; set; }
        public Guid? BranchHolderDepartmentId { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public string OrdersPhoneNumber { get; set; }
        public bool IndependantOwner { get; set; }
        public bool? IsActive { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string LocationAddress { get; set; }
        public string DescriptiveAddress { get; set; }
        public int OrderTakingType { get; set; }
        public Guid? GeofenceId { get; set; }
        public string HashedCode { get; set; }

        public virtual Defdepartments BranchHolderDepartment { get; set; }
        public virtual Defcities City { get; set; }
        public virtual Deffranchisees Franchisee { get; set; }
        public virtual Defgeofences Geofence { get; set; }
        public virtual Defstates State { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual ICollection<DefbankingInfos> DefbankingInfos { get; set; }
        public virtual ICollection<DefbranchDeliveryMethods> DefbranchDeliveryMethods { get; set; }
        public virtual ICollection<DefbranchPaymentMethods> DefbranchPaymentMethods { get; set; }
        public virtual ICollection<DefbranchSchedules> DefbranchSchedules { get; set; }
        public virtual ICollection<Defbroadcasters> Defbroadcasters { get; set; }
        public virtual ICollection<OltpbroadcastingPlayerLogs> OltpbroadcastingPlayerLogs { get; set; }
        public virtual ICollection<Oltpemployees> Oltpemployees { get; set; }
        public virtual ICollection<OltppaymentLogs> OltppaymentLogs { get; set; }
        public virtual ICollection<OltppaymentRequests> OltppaymentRequests { get; set; }
        public virtual ICollection<OltppurchaseDeliveryDetails> OltppurchaseDeliveryDetails { get; set; }
        public virtual ICollection<Oltppurchases> Oltppurchases { get; set; }
        public virtual ICollection<OltpraffleWinners> OltpraffleWinners { get; set; }
    }
}
