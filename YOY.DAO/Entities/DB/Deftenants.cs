using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Deftenants
    {
        public Deftenants()
        {
            DefbankingInfos = new HashSet<DefbankingInfos>();
            Defbranches = new HashSet<Defbranches>();
            Defbroadcasters = new HashSet<Defbroadcasters>();
            DefcrossSellingCampaignsFirstPurchaseTenant = new HashSet<DefcrossSellingCampaigns>();
            DefcrossSellingCampaignsSecondPurchaseTenant = new HashSet<DefcrossSellingCampaigns>();
            DefdepartmentCategories = new HashSet<DefdepartmentCategories>();
            Defdepartments = new HashSet<Defdepartments>();
            DefearningsIncreasers = new HashSet<DefearningsIncreasers>();
            Deffranchisees = new HashSet<Deffranchisees>();
            DeftenantInformations = new HashSet<DeftenantInformations>();
            DeftenantMembershipLevels = new HashSet<DeftenantMembershipLevels>();
            OltpbroadcastingLogs = new HashSet<OltpbroadcastingLogs>();
            Oltpbtlcontents = new HashSet<Oltpbtlcontents>();
            OltpcashIncentives = new HashSet<OltpcashIncentives>();
            Oltpcheckins = new HashSet<Oltpcheckins>();
            OltpexternallyStoredFiles = new HashSet<OltpexternallyStoredFiles>();
            Oltpfiles = new HashSet<Oltpfiles>();
            Oltpimages = new HashSet<Oltpimages>();
            OltpmembershipPointsOperations = new HashSet<OltpmembershipPointsOperations>();
            Oltpmemberships = new HashSet<Oltpmemberships>();
            OltpmonetaryFeeLogs = new HashSet<OltpmonetaryFeeLogs>();
            OltpmoneyConversionLogs = new HashSet<OltpmoneyConversionLogs>();
            Oltpoffers = new HashSet<Oltpoffers>();
            OltpoperationIssues = new HashSet<OltpoperationIssues>();
            OltpproductItemTenantHolders = new HashSet<OltpproductItemTenantHolders>();
            OltpproductItems = new HashSet<OltpproductItems>();
            Oltppurchases = new HashSet<Oltppurchases>();
            OltpreceiptRequestedValidations = new HashSet<OltpreceiptRequestedValidations>();
            Oltpreceipts = new HashSet<Oltpreceipts>();
            OltprewardToAwards = new HashSet<OltprewardToAwards>();
            OltpsavedItems = new HashSet<OltpsavedItems>();
            Oltpsearchables = new HashSet<Oltpsearchables>();
            OltpshoppingCartItems = new HashSet<OltpshoppingCartItems>();
            OltpuserPaymentRecords = new HashSet<OltpuserPaymentRecords>();
            OltpvalidatePurchaseRegistries = new HashSet<OltpvalidatePurchaseRegistries>();
        }

        public Guid Id { get; set; }
        public int InstanceType { get; set; }
        public bool? IsActive { get; set; }
        public bool Deleted { get; set; }
        public bool Released { get; set; }
        public bool System { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<DefbankingInfos> DefbankingInfos { get; set; }
        public virtual ICollection<Defbranches> Defbranches { get; set; }
        public virtual ICollection<Defbroadcasters> Defbroadcasters { get; set; }
        public virtual ICollection<DefcrossSellingCampaigns> DefcrossSellingCampaignsFirstPurchaseTenant { get; set; }
        public virtual ICollection<DefcrossSellingCampaigns> DefcrossSellingCampaignsSecondPurchaseTenant { get; set; }
        public virtual ICollection<DefdepartmentCategories> DefdepartmentCategories { get; set; }
        public virtual ICollection<Defdepartments> Defdepartments { get; set; }
        public virtual ICollection<DefearningsIncreasers> DefearningsIncreasers { get; set; }
        public virtual ICollection<Deffranchisees> Deffranchisees { get; set; }
        public virtual ICollection<DeftenantInformations> DeftenantInformations { get; set; }
        public virtual ICollection<DeftenantMembershipLevels> DeftenantMembershipLevels { get; set; }
        public virtual ICollection<OltpbroadcastingLogs> OltpbroadcastingLogs { get; set; }
        public virtual ICollection<Oltpbtlcontents> Oltpbtlcontents { get; set; }
        public virtual ICollection<OltpcashIncentives> OltpcashIncentives { get; set; }
        public virtual ICollection<Oltpcheckins> Oltpcheckins { get; set; }
        public virtual ICollection<OltpexternallyStoredFiles> OltpexternallyStoredFiles { get; set; }
        public virtual ICollection<Oltpfiles> Oltpfiles { get; set; }
        public virtual ICollection<Oltpimages> Oltpimages { get; set; }
        public virtual ICollection<OltpmembershipPointsOperations> OltpmembershipPointsOperations { get; set; }
        public virtual ICollection<Oltpmemberships> Oltpmemberships { get; set; }
        public virtual ICollection<OltpmonetaryFeeLogs> OltpmonetaryFeeLogs { get; set; }
        public virtual ICollection<OltpmoneyConversionLogs> OltpmoneyConversionLogs { get; set; }
        public virtual ICollection<Oltpoffers> Oltpoffers { get; set; }
        public virtual ICollection<OltpoperationIssues> OltpoperationIssues { get; set; }
        public virtual ICollection<OltpproductItemTenantHolders> OltpproductItemTenantHolders { get; set; }
        public virtual ICollection<OltpproductItems> OltpproductItems { get; set; }
        public virtual ICollection<Oltppurchases> Oltppurchases { get; set; }
        public virtual ICollection<OltpreceiptRequestedValidations> OltpreceiptRequestedValidations { get; set; }
        public virtual ICollection<Oltpreceipts> Oltpreceipts { get; set; }
        public virtual ICollection<OltprewardToAwards> OltprewardToAwards { get; set; }
        public virtual ICollection<OltpsavedItems> OltpsavedItems { get; set; }
        public virtual ICollection<Oltpsearchables> Oltpsearchables { get; set; }
        public virtual ICollection<OltpshoppingCartItems> OltpshoppingCartItems { get; set; }
        public virtual ICollection<OltpuserPaymentRecords> OltpuserPaymentRecords { get; set; }
        public virtual ICollection<OltpvalidatePurchaseRegistries> OltpvalidatePurchaseRegistries { get; set; }
    }
}
