using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            DefappInstallations = new HashSet<DefappInstallations>();
            InverseInvitorUser = new HashSet<AspNetUsers>();
            OltpbroadcastingEvents = new HashSet<OltpbroadcastingEvents>();
            OltpbroadcastingLogs = new HashSet<OltpbroadcastingLogs>();
            Oltpcheckins = new HashSet<Oltpcheckins>();
            OltpinvoicingInfos = new HashSet<OltpinvoicingInfos>();
            Oltpmemberships = new HashSet<Oltpmemberships>();
            OltpmonetaryFeeLogs = new HashSet<OltpmonetaryFeeLogs>();
            OltpmoneyTransfers = new HashSet<OltpmoneyTransfers>();
            OltpmoneyWithdrawalsUpdateUserModifier = new HashSet<OltpmoneyWithdrawals>();
            OltpmoneyWithdrawalsUser = new HashSet<OltpmoneyWithdrawals>();
            OltpoperationIssues = new HashSet<OltpoperationIssues>();
            OltppaymentInfos = new HashSet<OltppaymentInfos>();
            OltppaymentLogs = new HashSet<OltppaymentLogs>();
            Oltppurchases = new HashSet<Oltppurchases>();
            OltpraffleWinners = new HashSet<OltpraffleWinners>();
            OltpreceiptRequestedValidations = new HashSet<OltpreceiptRequestedValidations>();
            Oltpreceipts = new HashSet<Oltpreceipts>();
            OltprewardToAwards = new HashSet<OltprewardToAwards>();
            OltprewardedUsers = new HashSet<OltprewardedUsers>();
            OltpsavedItems = new HashSet<OltpsavedItems>();
            OltpsearchLogs = new HashSet<OltpsearchLogs>();
            Oltptransactions = new HashSet<Oltptransactions>();
            OltpuserInterests = new HashSet<OltpuserInterests>();
            OltpuserLocationLogs = new HashSet<OltpuserLocationLogs>();
            OltpuserPaymentRecords = new HashSet<OltpuserPaymentRecords>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string CountryPhonePrefix { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public long AccountNumber { get; set; }
        public string AccountCode { get; set; }
        public string PersonalId { get; set; }
        public string ProfilePicUrl { get; set; }
        public string InvitorUserId { get; set; }
        public string ReferenceCode { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? ReceiveSmsmarketing { get; set; }
        public bool? ReceiveEmailMarketing { get; set; }
        public int MaxDailyNotifications { get; set; }
        public string Fbid { get; set; }
        public string AppleId { get; set; }
        public string GoogleId { get; set; }
        public DateTime? LastAppOpen { get; set; }
        public DateTime? LastOfferRedemption { get; set; }
        public Guid? StateId { get; set; }
        public string Language { get; set; }

        public virtual AspNetUsers InvitorUser { get; set; }
        public virtual Defstates State { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual ICollection<DefappInstallations> DefappInstallations { get; set; }
        public virtual ICollection<AspNetUsers> InverseInvitorUser { get; set; }
        public virtual ICollection<OltpbroadcastingEvents> OltpbroadcastingEvents { get; set; }
        public virtual ICollection<OltpbroadcastingLogs> OltpbroadcastingLogs { get; set; }
        public virtual ICollection<Oltpcheckins> Oltpcheckins { get; set; }
        public virtual ICollection<OltpinvoicingInfos> OltpinvoicingInfos { get; set; }
        public virtual ICollection<Oltpmemberships> Oltpmemberships { get; set; }
        public virtual ICollection<OltpmonetaryFeeLogs> OltpmonetaryFeeLogs { get; set; }
        public virtual ICollection<OltpmoneyTransfers> OltpmoneyTransfers { get; set; }
        public virtual ICollection<OltpmoneyWithdrawals> OltpmoneyWithdrawalsUpdateUserModifier { get; set; }
        public virtual ICollection<OltpmoneyWithdrawals> OltpmoneyWithdrawalsUser { get; set; }
        public virtual ICollection<OltpoperationIssues> OltpoperationIssues { get; set; }
        public virtual ICollection<OltppaymentInfos> OltppaymentInfos { get; set; }
        public virtual ICollection<OltppaymentLogs> OltppaymentLogs { get; set; }
        public virtual ICollection<Oltppurchases> Oltppurchases { get; set; }
        public virtual ICollection<OltpraffleWinners> OltpraffleWinners { get; set; }
        public virtual ICollection<OltpreceiptRequestedValidations> OltpreceiptRequestedValidations { get; set; }
        public virtual ICollection<Oltpreceipts> Oltpreceipts { get; set; }
        public virtual ICollection<OltprewardToAwards> OltprewardToAwards { get; set; }
        public virtual ICollection<OltprewardedUsers> OltprewardedUsers { get; set; }
        public virtual ICollection<OltpsavedItems> OltpsavedItems { get; set; }
        public virtual ICollection<OltpsearchLogs> OltpsearchLogs { get; set; }
        public virtual ICollection<Oltptransactions> Oltptransactions { get; set; }
        public virtual ICollection<OltpuserInterests> OltpuserInterests { get; set; }
        public virtual ICollection<OltpuserLocationLogs> OltpuserLocationLogs { get; set; }
        public virtual ICollection<OltpuserPaymentRecords> OltpuserPaymentRecords { get; set; }
    }
}
