using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YOY.DAO.Entities.DB
{
    public partial class yoyIj7qM58dCjContext : DbContext
    {
        public yoyIj7qM58dCjContext()
        {
        }

        public yoyIj7qM58dCjContext(DbContextOptions<yoyIj7qM58dCjContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apikeys> Apikeys { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BranchesRelationData> BranchesRelationData { get; set; }
        public virtual DbSet<Defaffinities> Defaffinities { get; set; }
        public virtual DbSet<Defalliances> Defalliances { get; set; }
        public virtual DbSet<DefappInstallations> DefappInstallations { get; set; }
        public virtual DbSet<DefappInstallationsView> DefappInstallationsView { get; set; }
        public virtual DbSet<DefbankingInfos> DefbankingInfos { get; set; }
        public virtual DbSet<DefbankingInfosView> DefbankingInfosView { get; set; }
        public virtual DbSet<DefbranchDeliveryMethods> DefbranchDeliveryMethods { get; set; }
        public virtual DbSet<DefbranchPaymentMethods> DefbranchPaymentMethods { get; set; }
        public virtual DbSet<DefbranchSchedules> DefbranchSchedules { get; set; }
        public virtual DbSet<Defbranches> Defbranches { get; set; }
        public virtual DbSet<DefbranchesView> DefbranchesView { get; set; }
        public virtual DbSet<Defbroadcasters> Defbroadcasters { get; set; }
        public virtual DbSet<DefbroadcastersView> DefbroadcastersView { get; set; }
        public virtual DbSet<DefbroadcastingSchedules> DefbroadcastingSchedules { get; set; }
        public virtual DbSet<Defcities> Defcities { get; set; }
        public virtual DbSet<DefconfigValues> DefconfigValues { get; set; }
        public virtual DbSet<Defcountries> Defcountries { get; set; }
        public virtual DbSet<DefdeliveryMethods> DefdeliveryMethods { get; set; }
        public virtual DbSet<DefdeliveryMethodsView> DefdeliveryMethodsView { get; set; }
        public virtual DbSet<DefdepartmentCategories> DefdepartmentCategories { get; set; }
        public virtual DbSet<DefdepartmentCategoryView> DefdepartmentCategoryView { get; set; }
        public virtual DbSet<Defdepartments> Defdepartments { get; set; }
        public virtual DbSet<Defdistricts> Defdistricts { get; set; }
        public virtual DbSet<DefearningsIncreasers> DefearningsIncreasers { get; set; }
        public virtual DbSet<DeffeaturedSlides> DeffeaturedSlides { get; set; }
        public virtual DbSet<Deffranchisees> Deffranchisees { get; set; }
        public virtual DbSet<Defgeofences> Defgeofences { get; set; }
        public virtual DbSet<DefgeofencesFromGeoTriggerView> DefgeofencesFromGeoTriggerView { get; set; }
        public virtual DbSet<DefgeofencesView> DefgeofencesView { get; set; }
        public virtual DbSet<Defgeotriggers> Defgeotriggers { get; set; }
        public virtual DbSet<DefgeotriggersView> DefgeotriggersView { get; set; }
        public virtual DbSet<Defgeozones> Defgeozones { get; set; }
        public virtual DbSet<DefgeozonesView> DefgeozonesView { get; set; }
        public virtual DbSet<DefhardwareIotdevices> DefhardwareIotdevices { get; set; }
        public virtual DbSet<Defkeywords> Defkeywords { get; set; }
        public virtual DbSet<DefkeywordsView> DefkeywordsView { get; set; }
        public virtual DbSet<DefmembershipLevels> DefmembershipLevels { get; set; }
        public virtual DbSet<DefmembershipLevelsView> DefmembershipLevelsView { get; set; }
        public virtual DbSet<DefpaymentMethods> DefpaymentMethods { get; set; }
        public virtual DbSet<DefpaymentMethodsView> DefpaymentMethodsView { get; set; }
        public virtual DbSet<DefpromotionCampaignMembers> DefpromotionCampaignMembers { get; set; }
        public virtual DbSet<DefpromotionalCampaigns> DefpromotionalCampaigns { get; set; }
        public virtual DbSet<DefreceiptAnalyzerConfigs> DefreceiptAnalyzerConfigs { get; set; }
        public virtual DbSet<DefsearchIndexes> DefsearchIndexes { get; set; }
        public virtual DbSet<Defstates> Defstates { get; set; }
        public virtual DbSet<DefstatesDataView> DefstatesDataView { get; set; }
        public virtual DbSet<DeftenantInformations> DeftenantInformations { get; set; }
        public virtual DbSet<DeftenantInfosView> DeftenantInfosView { get; set; }
        public virtual DbSet<DeftenantMembershipLevels> DeftenantMembershipLevels { get; set; }
        public virtual DbSet<Deftenants> Deftenants { get; set; }
        public virtual DbSet<DeftenantsByStateView> DeftenantsByStateView { get; set; }
        public virtual DbSet<DefuserInterestFactors> DefuserInterestFactors { get; set; }
        public virtual DbSet<EnabledProductCategoriesByTenantCategoryRelationView> EnabledProductCategoriesByTenantCategoryRelationView { get; set; }
        public virtual DbSet<ExceptionLogging> ExceptionLogging { get; set; }
        public virtual DbSet<GeoLocatedTenantsView> GeoLocatedTenantsView { get; set; }
        public virtual DbSet<OltpbroadcastingEvents> OltpbroadcastingEvents { get; set; }
        public virtual DbSet<OltpbroadcastingLogRecords> OltpbroadcastingLogRecords { get; set; }
        public virtual DbSet<OltpbroadcastingLogs> OltpbroadcastingLogs { get; set; }
        public virtual DbSet<OltpbroadcastingLogsDataView> OltpbroadcastingLogsDataView { get; set; }
        public virtual DbSet<OltpbroadcastingPlayerLogs> OltpbroadcastingPlayerLogs { get; set; }
        public virtual DbSet<OltpbroadcastingPlayerLogsView> OltpbroadcastingPlayerLogsView { get; set; }
        public virtual DbSet<OltpbtlcontentItems> OltpbtlcontentItems { get; set; }
        public virtual DbSet<Oltpbtlcontents> Oltpbtlcontents { get; set; }
        public virtual DbSet<OltpbtlcontentsView> OltpbtlcontentsView { get; set; }
        public virtual DbSet<OltpcashbackIncentives> OltpcashbackIncentives { get; set; }
        public virtual DbSet<Oltpcategories> Oltpcategories { get; set; }
        public virtual DbSet<OltpcategoriesView> OltpcategoriesView { get; set; }
        public virtual DbSet<OltpcategoryRelations> OltpcategoryRelations { get; set; }
        public virtual DbSet<OltpcategoryRelationsView> OltpcategoryRelationsView { get; set; }
        public virtual DbSet<OltpcheckInsView> OltpcheckInsView { get; set; }
        public virtual DbSet<Oltpcheckins> Oltpcheckins { get; set; }
        public virtual DbSet<OltpclaimRecordLines> OltpclaimRecordLines { get; set; }
        public virtual DbSet<OltpclaimRecordLinesView> OltpclaimRecordLinesView { get; set; }
        public virtual DbSet<OltpclaimRecords> OltpclaimRecords { get; set; }
        public virtual DbSet<OltpcontentLocations> OltpcontentLocations { get; set; }
        public virtual DbSet<Oltpemployees> Oltpemployees { get; set; }
        public virtual DbSet<OltpemployeesView> OltpemployeesView { get; set; }
        public virtual DbSet<OltpexternallyStoredFiles> OltpexternallyStoredFiles { get; set; }
        public virtual DbSet<Oltpfiles> Oltpfiles { get; set; }
        public virtual DbSet<OltphttpcallInvokationLogs> OltphttpcallInvokationLogs { get; set; }
        public virtual DbSet<Oltpimages> Oltpimages { get; set; }
        public virtual DbSet<OltpinvoicingInfos> OltpinvoicingInfos { get; set; }
        public virtual DbSet<OltpmembershipPointsOperations> OltpmembershipPointsOperations { get; set; }
        public virtual DbSet<OltpmembershipPointsOperationsView> OltpmembershipPointsOperationsView { get; set; }
        public virtual DbSet<Oltpmemberships> Oltpmemberships { get; set; }
        public virtual DbSet<OltpmembershipsView> OltpmembershipsView { get; set; }
        public virtual DbSet<OltpmonetaryFeeLogs> OltpmonetaryFeeLogs { get; set; }
        public virtual DbSet<OltpmonetaryFeeLogsView> OltpmonetaryFeeLogsView { get; set; }
        public virtual DbSet<OltpmoneyConversionLogs> OltpmoneyConversionLogs { get; set; }
        public virtual DbSet<OltpmoneyConversionLogsView> OltpmoneyConversionLogsView { get; set; }
        public virtual DbSet<OltpmoneyConversionLogsWithTenantView> OltpmoneyConversionLogsWithTenantView { get; set; }
        public virtual DbSet<OltpmoneyTransfers> OltpmoneyTransfers { get; set; }
        public virtual DbSet<OltpmoneyWithdrawals> OltpmoneyWithdrawals { get; set; }
        public virtual DbSet<OltpofferPreferenceOptions> OltpofferPreferenceOptions { get; set; }
        public virtual DbSet<OltpofferPreferences> OltpofferPreferences { get; set; }
        public virtual DbSet<Oltpoffers> Oltpoffers { get; set; }
        public virtual DbSet<OltpoffersView> OltpoffersView { get; set; }
        public virtual DbSet<OltpoperationFlowStepLogs> OltpoperationFlowStepLogs { get; set; }
        public virtual DbSet<OltpoperationIssues> OltpoperationIssues { get; set; }
        public virtual DbSet<OltpoperationIssuesView> OltpoperationIssuesView { get; set; }
        public virtual DbSet<OltppaymentInfos> OltppaymentInfos { get; set; }
        public virtual DbSet<OltppaymentInfosView> OltppaymentInfosView { get; set; }
        public virtual DbSet<OltppaymentLogs> OltppaymentLogs { get; set; }
        public virtual DbSet<OltppaymentRequests> OltppaymentRequests { get; set; }
        public virtual DbSet<OltppreferenceWithOptionView> OltppreferenceWithOptionView { get; set; }
        public virtual DbSet<OltpproductItemContents> OltpproductItemContents { get; set; }
        public virtual DbSet<OltpproductItemTenantHolders> OltpproductItemTenantHolders { get; set; }
        public virtual DbSet<OltpproductItems> OltpproductItems { get; set; }
        public virtual DbSet<OltppurchaseDeliveryDetails> OltppurchaseDeliveryDetails { get; set; }
        public virtual DbSet<OltppurchasedItems> OltppurchasedItems { get; set; }
        public virtual DbSet<OltppurchasedItemsView> OltppurchasedItemsView { get; set; }
        public virtual DbSet<Oltppurchases> Oltppurchases { get; set; }
        public virtual DbSet<OltpraffleWinners> OltpraffleWinners { get; set; }
        public virtual DbSet<OltpreceiptPictures> OltpreceiptPictures { get; set; }
        public virtual DbSet<OltpreceiptRequestedValidations> OltpreceiptRequestedValidations { get; set; }
        public virtual DbSet<OltpreceiptSummariesView> OltpreceiptSummariesView { get; set; }
        public virtual DbSet<Oltpreceipts> Oltpreceipts { get; set; }
        public virtual DbSet<OltpreceiptsView> OltpreceiptsView { get; set; }
        public virtual DbSet<OltprewardToAwards> OltprewardToAwards { get; set; }
        public virtual DbSet<OltprewardedUsers> OltprewardedUsers { get; set; }
        public virtual DbSet<Oltprewards> Oltprewards { get; set; }
        public virtual DbSet<OltprewardsView> OltprewardsView { get; set; }
        public virtual DbSet<OltpsavedItems> OltpsavedItems { get; set; }
        public virtual DbSet<OltpsearchLogs> OltpsearchLogs { get; set; }
        public virtual DbSet<Oltpsearchables> Oltpsearchables { get; set; }
        public virtual DbSet<OltpsearchablesView> OltpsearchablesView { get; set; }
        public virtual DbSet<OltpshoppingCartItems> OltpshoppingCartItems { get; set; }
        public virtual DbSet<OltpshoppingCartItemsView> OltpshoppingCartItemsView { get; set; }
        public virtual DbSet<OltptextMessageLogs> OltptextMessageLogs { get; set; }
        public virtual DbSet<OltptransactionLocations> OltptransactionLocations { get; set; }
        public virtual DbSet<OltptransactionLocationsView> OltptransactionLocationsView { get; set; }
        public virtual DbSet<Oltptransactions> Oltptransactions { get; set; }
        public virtual DbSet<OltptransactionsView> OltptransactionsView { get; set; }
        public virtual DbSet<OltpuserInterestRecords> OltpuserInterestRecords { get; set; }
        public virtual DbSet<OltpuserInterests> OltpuserInterests { get; set; }
        public virtual DbSet<OltpuserInterestsView> OltpuserInterestsView { get; set; }
        public virtual DbSet<OltpuserInviteRelations> OltpuserInviteRelations { get; set; }
        public virtual DbSet<OltpuserLocationLogs> OltpuserLocationLogs { get; set; }
        public virtual DbSet<OltpuserPersonalIdLinkLogs> OltpuserPersonalIdLinkLogs { get; set; }
        public virtual DbSet<OltpuserPhoneNumberLinkLogs> OltpuserPhoneNumberLinkLogs { get; set; }
        public virtual DbSet<OltpvalidatePurchaseRegistries> OltpvalidatePurchaseRegistries { get; set; }
        public virtual DbSet<OltpvisitorsLog> OltpvisitorsLog { get; set; }
        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }
        public virtual DbSet<TempbroadcasterBranchesRelatedData> TempbroadcasterBranchesRelatedData { get; set; }
        public virtual DbSet<TempbroadcastingOffersLogs> TempbroadcastingOffersLogs { get; set; }
        public virtual DbSet<TempcashbackIncentivesPreferenceBranches> TempcashbackIncentivesPreferenceBranches { get; set; }
        public virtual DbSet<TempclaimableTransactions> TempclaimableTransactions { get; set; }
        public virtual DbSet<Tempclubs> Tempclubs { get; set; }
        public virtual DbSet<TempmembershipDetails> TempmembershipDetails { get; set; }
        public virtual DbSet<TempmembershipPointOps> TempmembershipPointOps { get; set; }
        public virtual DbSet<TempofferDetails> TempofferDetails { get; set; }
        public virtual DbSet<TempoffersPreferenceBranches> TempoffersPreferenceBranches { get; set; }
        public virtual DbSet<Temppreferences> Temppreferences { get; set; }
        public virtual DbSet<TemprewardDetails> TemprewardDetails { get; set; }
        public virtual DbSet<TemprewardOverviews> TemprewardOverviews { get; set; }
        public virtual DbSet<TempsearchableLogs> TempsearchableLogs { get; set; }
        public virtual DbSet<Tempstates> Tempstates { get; set; }
        public virtual DbSet<TempuserInterestsWithFactor> TempuserInterestsWithFactor { get; set; }
        public virtual DbSet<UserDataForTokenView> UserDataForTokenView { get; set; }
        public virtual DbSet<UserDataView> UserDataView { get; set; }
        public virtual DbSet<UserWithLocationAndMembershipDataView> UserWithLocationAndMembershipDataView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.Default.default_connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apikeys>(entity =>
            {
                entity.ToTable("APIKeys");

                entity.HasIndex(e => new { e.HashedKey, e.Discriminator })
                    .HasName("IX_APIKeys_HashedValue")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiresUtcdate)
                    .HasColumnName("ExpiresUTCDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.HashedKey)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.IssuedUtcdate)
                    .HasColumnName("IssuedUTCDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastUsageDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.AccountCode)
                    .IsUnique();

                entity.HasIndex(e => e.AccountNumber)
                    .HasName("AK_AspNetUsers_AccountNumber")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.HasIndex(e => e.ReferenceCode);

                entity.HasIndex(e => e.UserName)
                    .IsUnique();

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNumber).ValueGeneratedOnAdd();

                entity.Property(e => e.AppleId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPhonePrefix)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Fbid)
                    .HasColumnName("FBId")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.GoogleId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.InvitorUserId).HasMaxLength(450);

                entity.Property(e => e.Language)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastAppOpen).HasColumnType("datetime");

                entity.Property(e => e.LastOfferRedemption).HasColumnType("datetime");

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.MaxDailyNotifications).HasDefaultValueSql("((9))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PersonalId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveEmailMarketing)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReceiveSmsmarketing)
                    .IsRequired()
                    .HasColumnName("ReceiveSMSMarketing")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReferenceCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.InvitorUser)
                    .WithMany(p => p.InverseInvitorUser)
                    .HasForeignKey(d => d.InvitorUserId)
                    .HasConstraintName("FK_AspNetUsers_AspNetUsers");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_AspNetUsers_DEFStates");
            });

            modelBuilder.Entity<BranchesRelationData>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BranchesRelationData");

                entity.Property(e => e.BranchHolderDepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchHolderName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Defaffinities>(entity =>
            {
                entity.HasKey(e => new { e.FirstReferenceId, e.SecondReferenceId });

                entity.ToTable("DEFAffinities");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Defalliances>(entity =>
            {
                entity.ToTable("DEFAlliances");

                entity.HasIndex(e => new { e.SponsorId, e.SponsoredId })
                    .HasName("IX_DEFAlliances_Tenants");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.DefalliancesSponsor)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFAlliances_SponsorDEFTenants");

                entity.HasOne(d => d.Sponsored)
                    .WithMany(p => p.DefalliancesSponsored)
                    .HasForeignKey(d => d.SponsoredId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFAlliances_SponsoredDEFTenants");
            });

            modelBuilder.Entity<DefappInstallations>(entity =>
            {
                entity.ToTable("DEFAppInstallations");

                entity.HasIndex(e => new { e.InstallationId, e.Username })
                    .HasName("IX_DEFAppInstallations_InstallationId")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.InstallationId)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.DefappInstallations)
                    .HasPrincipalKey(p => p.UserName)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_DEFAppInstallations_AspNetUsers");
            });

            modelBuilder.Entity<DefappInstallationsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFAppInstallationsView");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InstallationId)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.LastDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<DefbankingInfos>(entity =>
            {
                entity.ToTable("DEFBankingInfos");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccNum1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccNum2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccNum3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.DefbankingInfos)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_DEFBankingInfos_DEFBranches");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.DefbankingInfos)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBankingInfos_DEFCountries");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.DefbankingInfos)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBankingInfos_DEFTenats");
            });

            modelBuilder.Entity<DefbankingInfosView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFBankingInfosView");

                entity.Property(e => e.AccNum1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccNum2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccNum3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchContactPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CountryFlag)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPhonePrefix)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TenantContactEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantContactName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TenantContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DefbranchDeliveryMethods>(entity =>
            {
                entity.HasKey(e => new { e.BranchId, e.MethodId })
                    .HasName("PK__DEFBranc__AEAEAE406BBDFF09");

                entity.ToTable("DEFBranchDeliveryMethods");

                entity.HasIndex(e => e.BranchId)
                    .HasName("IX_DEFBranchDeliveryMethods_Branch");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MaxItemsPerDelivery).HasDefaultValueSql("((-1))");

                entity.Property(e => e.UnitDistance).HasDefaultValueSql("((-1))");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.DefbranchDeliveryMethods)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBranchDeliveryMethods_DEFBranches");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.DefbranchDeliveryMethods)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBranchDeliveryMethods_DEFDeliveryMethods");
            });

            modelBuilder.Entity<DefbranchPaymentMethods>(entity =>
            {
                entity.HasKey(e => new { e.BranchId, e.MethodId })
                    .HasName("PK__DEFBranc__AEAEAE408D6EF742");

                entity.ToTable("DEFBranchPaymentMethods");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.DefbranchPaymentMethods)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBranchPaymentMethods_DEFBranches");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.DefbranchPaymentMethods)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBranchPaymentMethods_DEFPaymentMethods");
            });

            modelBuilder.Entity<DefbranchSchedules>(entity =>
            {
                entity.ToTable("DEFBranchSchedules");

                entity.HasIndex(e => e.BranchId)
                    .HasName("IX_DEFBranchSchedules_Branch");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.DefbranchSchedules)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_DEFBranchSchedules_DEFBranches");
            });

            modelBuilder.Entity<Defbranches>(entity =>
            {
                entity.ToTable("DEFBranches");

                entity.HasIndex(e => e.BranchHolderId);

                entity.HasIndex(e => e.CityId);

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.StateId);

                entity.HasIndex(e => e.TenantId);

                entity.HasIndex(e => new { e.TenantId, e.Name })
                    .HasName("UC_DEFBranches_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DescriptiveAddress)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.HashedCode)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(11, 8)")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.LocationAddress)
                    .IsRequired()
                    .HasColumnType("xml");

                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(11, 8)")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.OrderTakingType).HasDefaultValueSql("((1))");

                entity.Property(e => e.OrdersPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.BranchHolderDepartment)
                    .WithMany(p => p.Defbranches)
                    .HasForeignKey(d => d.BranchHolderDepartmentId)
                    .HasConstraintName("FK_DEFBranches_DEFBranchHolderDepartments");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Defbranches)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBranches_DEFCities");

                entity.HasOne(d => d.Franchisee)
                    .WithMany(p => p.Defbranches)
                    .HasForeignKey(d => d.FranchiseeId)
                    .HasConstraintName("FK_DEFBranches_DEFFranchisees");

                entity.HasOne(d => d.Geofence)
                    .WithMany(p => p.Defbranches)
                    .HasForeignKey(d => d.GeofenceId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_DEFBranches_DEFGeofences");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Defbranches)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBranches_DEFStates");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Defbranches)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBranches_DEFTenants");
            });

            modelBuilder.Entity<DefbranchesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFBranchesView");

                entity.Property(e => e.BranchHolderContactPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BranchHolderDepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchHolderEmail)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.BranchHolderName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DescriptiveAddress)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseeContactEmail)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseeContactName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseeContactPhone)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FranchiseeLegalName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.GeofenceExternalId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GeofenceName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.HashedCode)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.LocationAddress)
                    .IsRequired()
                    .HasColumnType("xml");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.OrdersPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Defbroadcasters>(entity =>
            {
                entity.ToTable("DEFBroadcasters");

                entity.HasIndex(e => e.BeaconType)
                    .HasName("IX_DEFBeacons_BeaconType");

                entity.HasIndex(e => e.ExternalId)
                    .HasName("AK_DEFBroadcasters_ExternalId")
                    .IsUnique();

                entity.HasIndex(e => e.TenantId)
                    .HasName("IX_DEFBeacons_TenantId");

                entity.HasIndex(e => new { e.BranchId, e.DepartmentId })
                    .HasName("IX_DEFBeacons_BranchDept");

                entity.HasIndex(e => new { e.BranchId, e.FriendlyName })
                    .HasName("IX_DEFBeacons_FriendlyName")
                    .IsUnique();

                entity.HasIndex(e => new { e.CountryId, e.Name })
                    .HasName("IX_DEFBeacons_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FileId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FileMimeType)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FriendlyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InStoreLocation)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.InstanceId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastCheckDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NamespaceId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PurposeType).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid)
                    .HasColumnName("UUID")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Defbroadcasters)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DEFBeacons_DEFBranches");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Defbroadcasters)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_DEFBeacons_DEFDepartments");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Defbroadcasters)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFBroadcasters_DEFStates");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Defbroadcasters)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_DEFBeacons_DEFTenants");
            });

            modelBuilder.Entity<DefbroadcastersView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFBroadcastersView");

                entity.Property(e => e.BrachName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FileId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FileMimeType)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FriendlyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InStoreLocation)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.InstanceId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LastCheckDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NamespaceId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid)
                    .HasColumnName("UUID")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DefbroadcastingSchedules>(entity =>
            {
                entity.ToTable("DEFBroadcastingSchedules");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<Defcities>(entity =>
            {
                entity.ToTable("DEFCities");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UtctimeDifference)
                    .HasColumnName("UTCTimeDifference")
                    .HasDefaultValueSql("((-6))");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Defcities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFCities_DEFStates");
            });

            modelBuilder.Entity<DefconfigValues>(entity =>
            {
                entity.ToTable("DEFConfigValues");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AppName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastestFbversion)
                    .IsRequired()
                    .HasColumnName("LastestFBVersion")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastestWebVersion)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastestiOsversion)
                    .IsRequired()
                    .HasColumnName("LastestiOSVersion")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SupportEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SupportNumber)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<Defcountries>(entity =>
            {
                entity.ToTable("DEFCountries");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ContentSegmentationType).HasDefaultValueSql("((1))");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Flag)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LanguageCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberPrefix)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DefdeliveryMethods>(entity =>
            {
                entity.ToTable("DEFDeliveryMethods");

                entity.HasIndex(e => e.IconName)
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.IconName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<DefdeliveryMethodsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFDeliveryMethodsView");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.IconName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DefdepartmentCategories>(entity =>
            {
                entity.ToTable("DEFDepartmentCategories");

                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.DepartmentId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.DefdepartmentCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_DEFDepartmentCategories_DEFCategories");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DefdepartmentCategories)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_DEFDepartmentCategories_DEFDepartments");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.DefdepartmentCategories)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFDepartmentCategories_DEFTenants");
            });

            modelBuilder.Entity<DefdepartmentCategoryView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFDepartmentCategoryView");

                entity.Property(e => e.CategoryCarrouselImg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryDescription)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryIcon)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentCategoryName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Defdepartments>(entity =>
            {
                entity.ToTable("DEFDepartments");

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => new { e.TenantId, e.Name })
                    .HasName("UC_DEFDepartments_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Defdepartments)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_DEFDepartments_DEFTenants");
            });

            modelBuilder.Entity<Defdistricts>(entity =>
            {
                entity.ToTable("DEFDistricts");

                entity.HasIndex(e => new { e.CityId, e.Name })
                    .HasName("UC_DEFDistricts_Name")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Defdistricts)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_DEFDistricts_DEFCities");
            });

            modelBuilder.Entity<DefearningsIncreasers>(entity =>
            {
                entity.ToTable("DEFEarningsIncreasers");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IncreaserFactor).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PurchasedAmountBlock).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.UnlockCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpperEarningsLimit).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ValidHours)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ValidMonthDays)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ValidWeekDays)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProviderTenant)
                    .WithMany(p => p.DefearningsIncreasers)
                    .HasForeignKey(d => d.ProviderTenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFEarningsIncreasers_DEFTenants");
            });

            modelBuilder.Entity<DeffeaturedSlides>(entity =>
            {
                entity.ToTable("DEFFeaturedSlides");

                entity.HasIndex(e => e.CountryId);

                entity.HasIndex(e => e.StateId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.DeffeaturedSlides)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_DEFFeaturedSlides_OLTPImages");
            });

            modelBuilder.Entity<Deffranchisees>(entity =>
            {
                entity.ToTable("DEFFranchisees");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdditionalNotes)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhoneNumber)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LegalName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentSubject)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TaxAddress)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TaxId)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Deffranchisees)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFFranchisees_DEFTenants");
            });

            modelBuilder.Entity<Defgeofences>(entity =>
            {
                entity.ToTable("DEFGeofences");

                entity.HasIndex(e => e.ExternalId);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CenterLatitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.CenterLongitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Radius).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Defgeofences)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_DEFGeofences_DEFDistricts");

                entity.HasOne(d => d.Geozone)
                    .WithMany(p => p.Defgeofences)
                    .HasForeignKey(d => d.GeozoneId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_DEFGeofences_DEFGeozones");
            });

            modelBuilder.Entity<DefgeofencesFromGeoTriggerView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFGeofencesFromGeoTriggerView");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CenterLatitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.CenterLongitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Radius).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ZoneExternalId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ZoneName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DefgeofencesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFGeofencesView");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CenterLatitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.CenterLongitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Radius).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ZoneExternalId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ZoneName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Defgeotriggers>(entity =>
            {
                entity.ToTable("DEFGeotriggers");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExternalId)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TriggerType).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Geofence)
                    .WithMany(p => p.Defgeotriggers)
                    .HasForeignKey(d => d.GeofenceId)
                    .HasConstraintName("FK_DEFGeotriggers_DEFGeofences");
            });

            modelBuilder.Entity<DefgeotriggersView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFGeotriggersView");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExternalId)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GeofenceCenterLatitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.GeofenceCenterLongitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.GeofenceExternalId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GeofenceName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.GeofenceRadius).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Defgeozones>(entity =>
            {
                entity.ToTable("DEFGeozones");

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DescriptiveAddress)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LocationAddress).HasColumnType("xml");

                entity.Property(e => e.MinRetriggeredMins).HasDefaultValueSql("((10))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Defgeozones)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFGeozones_DEFCountry");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Defgeozones)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_DEFGeozones_DEFDistrict");
            });

            modelBuilder.Entity<DefgeozonesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFGeozonesView");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DescriptiveAddress)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.LocationAddress).HasColumnType("xml");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DefhardwareIotdevices>(entity =>
            {
                entity.ToTable("DEFHardwareIOTDevices");

                entity.HasIndex(e => e.BranchId);

                entity.HasIndex(e => e.TenantId);

                entity.HasIndex(e => e.UniqueKey)
                    .HasName("AK_DEFHardwareIOTDevices_UniqueKey")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FirmwareVersion)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.HardwareVersion)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.InstallationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastMaintenanceDate).HasColumnType("datetime");

                entity.Property(e => e.LastRequestDate).HasColumnType("datetime");

                entity.Property(e => e.UniqueKey)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<Defkeywords>(entity =>
            {
                entity.ToTable("DEFKeywords");

                entity.HasIndex(e => new { e.CategoryId, e.Word })
                    .HasName("IX_DEFKeywords_Word")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Language).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Defkeywords)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_DEFKeywords_OLTPCategories");
            });

            modelBuilder.Entity<DefkeywordsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFKeywordsView");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DefmembershipLevels>(entity =>
            {
                entity.ToTable("DEFMembershipLevels");

                entity.HasIndex(e => e.Level)
                    .HasName("IX_DEFMembershipTypes_Level")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("IX_DEFMembershipTypes_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IconUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<DefmembershipLevelsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFMembershipLevelsView");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.EnabledActions)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EnabledMoneyAmounts)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.IconUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DefpaymentMethods>(entity =>
            {
                entity.ToTable("DEFPaymentMethods");

                entity.HasIndex(e => e.IconName)
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.IconName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentBeforeShipping)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<DefpaymentMethodsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFPaymentMethodsView");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.IconName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DefpromotionCampaignMembers>(entity =>
            {
                entity.ToTable("DEFPromotionCampaignMembers");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.PromotionalCampaign)
                    .WithMany(p => p.DefpromotionCampaignMembers)
                    .HasForeignKey(d => d.PromotionalCampaignId)
                    .HasConstraintName("FK_DEFPromotionCampaignMembers_DEFPromotionalCampaigns");
            });

            modelBuilder.Entity<DefpromotionalCampaigns>(entity =>
            {
                entity.ToTable("DEFPromotionalCampaigns");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.UnlockCode)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.FeaturedSlide)
                    .WithMany(p => p.DefpromotionalCampaigns)
                    .HasForeignKey(d => d.FeaturedSlideId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_DEFPromotionalCampaigns_DEFFeaturedSliders");
            });

            modelBuilder.Entity<DefreceiptAnalyzerConfigs>(entity =>
            {
                entity.ToTable("DEFReceiptAnalyzerConfigs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BranchNameLinePos)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.BranchPostalCodePos)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.CharsToRemoveRegex)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimMark)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.CommerceName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CommerceNameLinePos)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DateRegex)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeRegex)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ExtraUniqueFields)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.LegalName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LegalPostalCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.LinesPerPurchaseItem)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.MoneyAmountRegex)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCodeRegex)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PreTaxAmountFullDataRegex)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PreTaxAmountInverseOrderPos)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PreTaxAmountValueRegex)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseItemRegex)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasedItemsStartLineRegex)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasedItemsStartMark)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.TaxId)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.TaxIdRegex)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TaxesPercentage).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TicketNumberRegex)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TimeRegex)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmountFullDataRegex)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmountInverseOrderPos)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmountToleranceRange)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmountValueRegex)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Franchisee)
                    .WithMany(p => p.DefreceiptAnalyzerConfigs)
                    .HasForeignKey(d => d.FranchiseeId)
                    .HasConstraintName("FK_DEFReceiptAnalyzerConfigs_DEFFranchisees");
            });

            modelBuilder.Entity<DefsearchIndexes>(entity =>
            {
                entity.ToTable("DEFSearchIndexes");

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AppName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Service).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Defstates>(entity =>
            {
                entity.ToTable("DEFStates");

                entity.HasIndex(e => new { e.CountryId, e.Name })
                    .HasName("IX_DEFStates_Name")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CenterLatitude)
                    .HasColumnType("decimal(11, 8)")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.CenterLongitude)
                    .HasColumnType("decimal(11, 8)")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UtcTimeZone).HasDefaultValueSql("((-5))");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Defstates)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFStates_DEFCountries");
            });

            modelBuilder.Entity<DefstatesDataView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFStatesDataView");

                entity.Property(e => e.CenterLatitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.CenterLongitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CountryFlag)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPhonePrefix)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeftenantInformations>(entity =>
            {
                entity.ToTable("DEFTenantInformations");

                entity.HasIndex(e => e.TenantId)
                    .HasName("IX_DEFTenantInformations_Tenant");

                entity.HasIndex(e => new { e.CategoryId, e.RelevanceStatus })
                    .HasName("IX_DEFTenantInformations_Category");

                entity.HasIndex(e => new { e.CountryId, e.Name })
                    .HasName("UC_DEFTenantInformations_Name")
                    .IsUnique();

                entity.HasIndex(e => new { e.CountryId, e.RelevanceStatus })
                    .HasName("IX_DEFTenantInformations_Country");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdditionalNotes)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessStructureType).HasDefaultValueSql("((1))");

                entity.Property(e => e.CampaignDefaultContentMsg)
                    .HasMaxLength(480)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignDefaultTitleMsg)
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.ConsumerCashbackPercentage).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DealConditions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.DealRules)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultCommissionFeePercentage).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.InStoreDealClaimInstructions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.IncentiveClaimInstructions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.IncentiveConditions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.IncentiveRules)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Language).HasDefaultValueSql("((1))");

                entity.Property(e => e.LegalName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LoyaltyProgramType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.OnlineDealClaimInstructions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.PayerType).HasDefaultValueSql("((1))");

                entity.Property(e => e.PaymentSubject)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneDealClaimInstructions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiptClaimMarkType).HasDefaultValueSql("((1))");

                entity.Property(e => e.ReferenceCodeType).HasDefaultValueSql("((1))");

                entity.Property(e => e.TaxAddress)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TaxId)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.TrialExpiration).HasColumnType("datetime");

                entity.Property(e => e.TypeId).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Website)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.DeftenantInformations)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFTenantInformations_OLTPCategories");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.DeftenantInformations)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFTenantInformations_DEFCountries");

                entity.HasOne(d => d.EmailBgNavigation)
                    .WithMany(p => p.DeftenantInformationsEmailBgNavigation)
                    .HasForeignKey(d => d.EmailBg)
                    .HasConstraintName("FK_DEFTenantInformations_OLTPImagesEmailBg");

                entity.HasOne(d => d.LandingImgNavigation)
                    .WithMany(p => p.DeftenantInformationsLandingImgNavigation)
                    .HasForeignKey(d => d.LandingImg)
                    .HasConstraintName("FK_DEFTenantInformations_OLTPImagesLanding");

                entity.HasOne(d => d.LogoNavigation)
                    .WithMany(p => p.DeftenantInformationsLogoNavigation)
                    .HasForeignKey(d => d.Logo)
                    .HasConstraintName("FK_DEFTenantInformations_OLTPImagesLogo");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.DeftenantInformations)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_DEFTenantInformations_DEFTenants");
            });

            modelBuilder.Entity<DeftenantInfosView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFTenantInfosView");

                entity.Property(e => e.AdditionalNotes)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignDefaultContentMsg)
                    .HasMaxLength(480)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignDefaultTitleMsg)
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.CommerceCategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CommerceClassificationName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ConsumerCashbackPercentage).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DealConditions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.DealRules)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultCommissionFeePercentage).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.InStoreDealClaimInstructions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.IncentiveClaimInstructions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.IncentiveConditions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.IncentiveRules)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.LegalName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.OnlineDealClaimInstructions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentSubject)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneDealClaimInstructions)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TaxAddress)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TaxId)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.TrialExpiration).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Website)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeftenantMembershipLevels>(entity =>
            {
                entity.HasKey(e => new { e.TenantId, e.LevelId })
                    .HasName("PK__DEFTenan__5E044423F86209A6");

                entity.ToTable("DEFTenantMembershipLevels");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.EnabledActions)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EnabledMoneyAmounts)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EvaluationMonths).HasDefaultValueSql("((2))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LoyaltyCashBackPercentage).HasDefaultValueSql("((1))");

                entity.Property(e => e.MaxGeneratedPoints).HasDefaultValueSql("((10))");

                entity.Property(e => e.MaxPurchasesCount).HasDefaultValueSql("((10))");

                entity.Property(e => e.MaxRewardRedemptions).HasDefaultValueSql("((3))");

                entity.Property(e => e.MinGeneratedPoints).HasDefaultValueSql("((1))");

                entity.Property(e => e.MinPurchasesCount).HasDefaultValueSql("((1))");

                entity.Property(e => e.MonetaryConversionFactor).HasDefaultValueSql("((0.25))");

                entity.Property(e => e.PointsLifeSpanMonths).HasDefaultValueSql("((12))");

                entity.Property(e => e.PointsToMoneyEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.DeftenantMembershipLevels)
                    .HasForeignKey(d => d.LevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFTenantMembershipLevels_DEFMembershipLevels");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.DeftenantMembershipLevels)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEFTenantMembershipLevels_DEFTenants");
            });

            modelBuilder.Entity<Deftenants>(entity =>
            {
                entity.ToTable("DEFTenants");

                entity.HasIndex(e => e.InstanceType);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.InstanceType).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<DeftenantsByStateView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DEFTenantsByStateView");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DefuserInterestFactors>(entity =>
            {
                entity.ToTable("DEFUserInterestFactors");

                entity.HasIndex(e => new { e.InterestType, e.InterestId })
                    .HasName("IX_DEFUserInterestRelevances_Interest");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DatesRange)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DaysOfWeekRange)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.HoursRange)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MonthsRange)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<EnabledProductCategoriesByTenantCategoryRelationView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("EnabledProductCategoriesByTenantCategoryRelationView");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExceptionLogging>(entity =>
            {
                entity.ToTable("_ExceptionLogging");

                entity.Property(e => e.ExceptionMsg)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ExceptionType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExceptionUrl)
                    .HasColumnName("ExceptionURL")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Logdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ThrownClass)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GeoLocatedTenantsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GeoLocatedTenantsView");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OltpbroadcastingEvents>(entity =>
            {
                entity.ToTable("OLTPBroadcastingEvents");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Accuracy)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.BroadcastingLog)
                    .WithMany(p => p.OltpbroadcastingEvents)
                    .HasForeignKey(d => d.BroadcastingLogId)
                    .HasConstraintName("FK_OLTPBroadcastingEvents_OLTPMessageLogs");

                entity.HasOne(d => d.PreviousEvent)
                    .WithMany(p => p.InversePreviousEvent)
                    .HasForeignKey(d => d.PreviousEventId)
                    .HasConstraintName("FK_OLTPBroadcastingEvents_OLTPBroadcastingEvents");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpbroadcastingEvents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPBroadcastingEvents_AspNetUsers");
            });

            modelBuilder.Entity<OltpbroadcastingLogRecords>(entity =>
            {
                entity.ToTable("OLTPBroadcastingLogRecords");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.BroadcastingLog)
                    .WithMany(p => p.OltpbroadcastingLogRecords)
                    .HasForeignKey(d => d.BroadcastingLogId)
                    .HasConstraintName("FK_OLTPBroadcastingLogRecords_OLTPBroadcastingLogs");
            });

            modelBuilder.Entity<OltpbroadcastingLogs>(entity =>
            {
                entity.ToTable("OLTPBroadcastingLogs");

                entity.HasIndex(e => new { e.BroadcasterType, e.BroadcasterId })
                    .HasName("IX_OLTPBroadcastingLogs_BroadcasterId");

                entity.HasIndex(e => new { e.UserId, e.SentDate })
                    .HasName("IX_OLTPBroadcastingLogs_UserId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContainedContentIds)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.MsgContent)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.MsgTitle)
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.OpenedDateTime).HasColumnType("datetime");

                entity.Property(e => e.RedeemContentIds)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.SentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.TriggeredLatitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.TriggeredLongitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpbroadcastingLogs)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_OLTPBroadcastingLogs_DEFTenants");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpbroadcastingLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_OLTPBroadcastingLogs_AspnetUsers");
            });

            modelBuilder.Entity<OltpbroadcastingLogsDataView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPBroadcastingLogsDataView");

                entity.Property(e => e.ContainedContentIds)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.MsgContent)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.MsgTitle)
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.OpenedDateTime).HasColumnType("datetime");

                entity.Property(e => e.RedeemContentIds)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.SentDate).HasColumnType("datetime");

                entity.Property(e => e.TriggeredLatitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.TriggeredLongitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<OltpbroadcastingPlayerLogs>(entity =>
            {
                entity.ToTable("OLTPBroadcastingPlayerLogs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.EventType).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.OltpbroadcastingPlayerLogs)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPBroadcastingPlayerLogs_DEFBranches");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.OltpbroadcastingPlayerLogs)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPBroadcastingPlayerLogs_DEFDepartments");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.OltpbroadcastingPlayerLogs)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPBroadcastingPlayerLogs_OLTPEmployees");
            });

            modelBuilder.Entity<OltpbroadcastingPlayerLogsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPBroadcastingPlayerLogsView");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.BroadcasterName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OltpbtlcontentItems>(entity =>
            {
                entity.ToTable("OLTPBTLContentItems");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContainedProducts)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ReferenceUrl)
                    .HasColumnName("ReferenceURL")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Content)
                    .WithMany(p => p.OltpbtlcontentItems)
                    .HasForeignKey(d => d.ContentId)
                    .HasConstraintName("FK_OLTPBTLContentItems_OLTPImageContents");
            });

            modelBuilder.Entity<Oltpbtlcontents>(entity =>
            {
                entity.ToTable("OLTPBTLContents");

                entity.HasIndex(e => e.DealType);

                entity.HasIndex(e => e.ExpirationDate);

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.ReleaseDate);

                entity.HasIndex(e => e.TenantId)
                    .HasName("IX_OLTPBTLContents_Tenant");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.GeoSegmentationType).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Oltpbtlcontents)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPBTLContents_OLTPCategories");

                entity.HasOne(d => d.DisplayImage)
                    .WithMany(p => p.Oltpbtlcontents)
                    .HasForeignKey(d => d.DisplayImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPBTLContents_OLTPImages");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Oltpbtlcontents)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_OLTPBTLContents_DEFTenants");
            });

            modelBuilder.Entity<OltpbtlcontentsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPBTLContentsView");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OltpcashbackIncentives>(entity =>
            {
                entity.ToTable("OLTPCashbackIncentives");

                entity.HasIndex(e => e.Type)
                    .HasName("IX_OLTPCashbackIncentives_CashbackType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Conditions)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Keywords)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.MaxValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.MinMembershipLevel).HasDefaultValueSql("((1))");

                entity.Property(e => e.MinPurchasedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PreviousUnitValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PurchasedAmountBlock).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Rules)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UnitValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ValidHours)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ValidMonthDays)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ValidWeekDays)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpcashbackIncentives)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPCashbackIncentives_DEFTenants");
            });

            modelBuilder.Entity<Oltpcategories>(entity =>
            {
                entity.ToTable("OLTPCategories");

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => new { e.ParentCategory, e.Name })
                    .HasName("UC_OLTPCategories_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CarrouselImg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PurposeType).HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.ParentCategoryNavigation)
                    .WithMany(p => p.InverseParentCategoryNavigation)
                    .HasForeignKey(d => d.ParentCategory)
                    .HasConstraintName("FK_OLTPCategories_OLTPCategories");
            });

            modelBuilder.Entity<OltpcategoriesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPCategoriesView");

                entity.Property(e => e.CarrouselImg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ParentCategoryName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OltpcategoryRelations>(entity =>
            {
                entity.ToTable("OLTPCategoryRelations");

                entity.HasIndex(e => e.ReferenceType);

                entity.HasIndex(e => new { e.ReferenceType, e.ReferenceId, e.CategoryId })
                    .HasName("AK_OLTPCategoryRelations_Column")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.OltpcategoryRelations)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPTenantCategories_OLTPCategories");

                entity.HasOne(d => d.GeneratorRelation)
                    .WithMany(p => p.InverseGeneratorRelation)
                    .HasForeignKey(d => d.GeneratorRelationId)
                    .HasConstraintName("FK_OLTPCategoryRelations_DEFCategoryRelations");
            });

            modelBuilder.Entity<OltpcategoryRelationsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPCategoryRelationsView");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OltpcheckInsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPCheckInsView");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.BroadcasterName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Oltpcheckins>(entity =>
            {
                entity.ToTable("OLTPCheckins");

                entity.HasIndex(e => e.BranchId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.PointsAppliedType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type).HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Oltpcheckins)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPCheckins_DEFTenants");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Oltpcheckins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPCheckins_AspNetUsers");
            });

            modelBuilder.Entity<OltpclaimRecordLines>(entity =>
            {
                entity.ToTable("OLTPClaimRecordLines");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClaimRefCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ValidationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.OltpclaimRecordLines)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPClaimRecordLines_OLTPTransactions");
            });

            modelBuilder.Entity<OltpclaimRecordLinesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPClaimRecordLinesView");

                entity.Property(e => e.ClaimRefCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ValidationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OltpclaimRecords>(entity =>
            {
                entity.ToTable("OLTPClaimRecords");

                entity.HasIndex(e => new { e.ReferenceType, e.ReferenceId })
                    .HasName("IX_OLTPUsageRecords_ReferenceId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastUsage)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.PreviousUsage).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<OltpcontentLocations>(entity =>
            {
                entity.HasKey(e => new { e.ReferenceId, e.LocationId })
                    .HasName("PK__OLTPCont__8FD6705026DB0AAA");

                entity.ToTable("OLTPContentLocations");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<Oltpemployees>(entity =>
            {
                entity.ToTable("OLTPEmployees");

                entity.HasIndex(e => e.AccessKey)
                    .IsUnique();

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.TenantId);

                entity.HasIndex(e => new { e.MembershipId, e.BranchId, e.TenantId })
                    .HasName("IX_OLTPEmployees_MembershipId")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccessAllowed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AccessKey)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.AuthorizedValidatorPhoneNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.RoleId).IsRequired();

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Oltpemployees)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OLTPEmployees_DEFBranches");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.InverseCreator)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("FK_OLTPEmployees_OLTPEmployees");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.Oltpemployees)
                    .HasForeignKey(d => d.MembershipId)
                    .HasConstraintName("FK_OLTPEmployees_OLTPMemberships");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Oltpemployees)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPEmployees_AspNetRoles");
            });

            modelBuilder.Entity<OltpemployeesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPEmployeesView");

                entity.Property(e => e.AccessKey)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.AuthorizedValidatorPhoneNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.BrachName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.RoleName).HasMaxLength(256);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<OltpexternallyStoredFiles>(entity =>
            {
                entity.ToTable("OLTPExternallyStoredFiles");

                entity.HasIndex(e => new { e.ReferenceType, e.ReferenceId })
                    .HasName("IX_OLTPExternallyStoredFiles_Reference");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExternalId)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.MimeType)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpexternallyStoredFiles)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPExternallyStoredFiles_DEFTenants");
            });

            modelBuilder.Entity<Oltpfiles>(entity =>
            {
                entity.ToTable("OLTPFiles");

                entity.HasIndex(e => e.ContentType);

                entity.HasIndex(e => e.FileExtension);

                entity.HasIndex(e => e.Reference);

                entity.HasIndex(e => e.TenantId)
                    .HasName("IX_OLTPFiles_Tenant");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Checksum).HasMaxLength(256);

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FileExtension).HasMaxLength(20);

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Oltpfiles)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPFiles_DEFTenants");
            });

            modelBuilder.Entity<OltphttpcallInvokationLogs>(entity =>
            {
                entity.ToTable("OLTPHttpcallInvokationLogs");

                entity.HasIndex(e => new { e.Controller, e.Version, e.Call })
                    .HasName("IX_OLTPHttpcallInvokationLogs_Controller");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ApiSouce).HasDefaultValueSql("((1))");

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.Params).IsUnicode(false);

                entity.Property(e => e.RequesterId).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.LastValidCall)
                    .WithMany(p => p.InverseLastValidCall)
                    .HasForeignKey(d => d.LastValidCallId)
                    .HasConstraintName("FK_OLTPHttpcallInvokationLogs_OLTPHttpcallInvokationLogs");
            });

            modelBuilder.Entity<Oltpimages>(entity =>
            {
                entity.ToTable("OLTPImages");

                entity.HasIndex(e => e.ExternalId)
                    .IsUnique();

                entity.HasIndex(e => e.ReferenceId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AppTransformation)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExternalId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Folder)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Format)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.WebTransformation)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Oltpimages)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPImages_DEFTenants");
            });

            modelBuilder.Entity<OltpinvoicingInfos>(entity =>
            {
                entity.ToTable("OLTPInvoicingInfos");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdditionalDetails)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.InvoicingId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.InvoicingName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpinvoicingInfos)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_OLTPInvoicingInfos_AspNetUsers");
            });

            modelBuilder.Entity<OltpmembershipPointsOperations>(entity =>
            {
                entity.ToTable("OLTPMembershipPointsOperations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AvailablePoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Code)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ConvertedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Details)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Registered)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UsedPoints).HasColumnType("decimal(19, 2)");

                entity.HasOne(d => d.BeneficiaryMembership)
                    .WithMany(p => p.OltpmembershipPointsOperationsBeneficiaryMembership)
                    .HasForeignKey(d => d.BeneficiaryMembershipId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPMembershipPointsOperations_OLTPBeneficiaryMemberships");

                entity.HasOne(d => d.BeneficiaryTenant)
                    .WithMany(p => p.OltpmembershipPointsOperations)
                    .HasForeignKey(d => d.BeneficiaryTenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMembershipPointsOperations_DEFTenants");

                entity.HasOne(d => d.MonetaryFeeLog)
                    .WithMany(p => p.OltpmembershipPointsOperations)
                    .HasForeignKey(d => d.MonetaryFeeLogId)
                    .HasConstraintName("FK_OLTPMembershipPointsOperations_OLTPMonetaryFeeLogs");

                entity.HasOne(d => d.ProviderMembership)
                    .WithMany(p => p.OltpmembershipPointsOperationsProviderMembership)
                    .HasForeignKey(d => d.ProviderMembershipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMembershipPointsOperations_OLTPProviderMemberships");
            });

            modelBuilder.Entity<OltpmembershipPointsOperationsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPMembershipPointsOperationsView");

                entity.Property(e => e.AvailablePoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.BeneficiaryUserEmail).HasMaxLength(256);

                entity.Property(e => e.BeneficiaryUserName).HasMaxLength(30);

                entity.Property(e => e.Code)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ConvertedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Details)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.ProviderUserEmail)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ProviderUserName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UsedPoints).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<Oltpmemberships>(entity =>
            {
                entity.ToTable("OLTPMemberships");

                entity.HasIndex(e => new { e.TenantId, e.UserId })
                    .HasName("IX_OLTPMemberships_User_Tenant")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClaimedRewardsStartDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastLevelEvaluation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastPromoClaimed).HasColumnType("datetime");

                entity.Property(e => e.LastPromoReserved).HasColumnType("datetime");

                entity.Property(e => e.MembershipLevel).HasDefaultValueSql("((1))");

                entity.Property(e => e.OriginType).HasDefaultValueSql("((1))");

                entity.Property(e => e.ReceiveEmailMarketing)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReceiveSmsmarketing)
                    .IsRequired()
                    .HasColumnName("ReceiveSMSMarketing")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UsedLoyaltyPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Oltpmemberships)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMemberships_DEFTenants");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Oltpmemberships)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMemberships_AspNetUsers");
            });

            modelBuilder.Entity<OltpmembershipsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPMembershipsView");

                entity.Property(e => e.AvailablePoints).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.ClaimedRewardsStartDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.EnabledMoneyAmounts)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.LastLevelEvaluation).HasColumnType("datetime");

                entity.Property(e => e.LastPromoClaimed).HasColumnType("datetime");

                entity.Property(e => e.LastPromoReserved).HasColumnType("datetime");

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ReceiveSmsmarketing).HasColumnName("ReceiveSMSMarketing");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UsedLoyaltyPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<OltpmonetaryFeeLogs>(entity =>
            {
                entity.ToTable("OLTPMonetaryFeeLogs");

                entity.HasIndex(e => e.DebtorTenantId);

                entity.HasIndex(e => e.Status);

                entity.HasIndex(e => new { e.RefType, e.RefId })
                    .HasName("IX_OLTPMonetaryFeeLogs_Refs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CollectionDueDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.GeneratorTenant)
                    .WithMany(p => p.OltpmonetaryFeeLogs)
                    .HasForeignKey(d => d.GeneratorTenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMonetaryFeeLogs_DEFTenants");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpmonetaryFeeLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMonetaryFeeLogs_AspNetUsers");
            });

            modelBuilder.Entity<OltpmonetaryFeeLogsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPMonetaryFeeLogsView");

                entity.Property(e => e.Amount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CollectionDueDate).HasColumnType("datetime");

                entity.Property(e => e.CollectorContactEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CollectorContactName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CollectorContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CollectorTenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.DebtorContactEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DebtorContactName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DebtorContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DebtorFranchiseeContactEmail)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DebtorFranchiseeContactName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DebtorFranchiseeContactPhoneNumber)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DebtorFranchiseeLegalName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DebtorTenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.GeneratorBranchName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.GeneratorContactEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GeneratorContactName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GeneratorContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GeneratorTenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserEmail).HasMaxLength(256);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UserName).HasMaxLength(30);
            });

            modelBuilder.Entity<OltpmoneyConversionLogs>(entity =>
            {
                entity.ToTable("OLTPMoneyConversionLogs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClaimerId).HasMaxLength(450);

                entity.Property(e => e.ConversionCode)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.EmployeeUserName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.InternalStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastStatusUpdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.MoneyAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.RequiredPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.OltpmoneyConversionLogs)
                    .HasForeignKey(d => d.OperationId)
                    .HasConstraintName("FK_OLTPMoneyConversionLogs_OLTPMembershipPointsOperations");

                entity.HasOne(d => d.PaymentLog)
                    .WithMany(p => p.OltpmoneyConversionLogs)
                    .HasForeignKey(d => d.PaymentLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMoneyConversionLogs_OLTPPaymentLogs");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpmoneyConversionLogs)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMoneyConversionLogs_DEFTenants");
            });

            modelBuilder.Entity<OltpmoneyConversionLogsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPMoneyConversionLogsView");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimerId).HasMaxLength(450);

                entity.Property(e => e.ConversionCode)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeUserName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LastPromoClaimed).HasColumnType("datetime");

                entity.Property(e => e.LastPromoReserved).HasColumnType("datetime");

                entity.Property(e => e.LastStatusUpdate).HasColumnType("datetime");

                entity.Property(e => e.MoneyAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.PointsOpCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PointsOpUsedPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.RequiredPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<OltpmoneyConversionLogsWithTenantView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPMoneyConversionLogsWithTenantView");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimerId).HasMaxLength(450);

                entity.Property(e => e.ConversionCode)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeUserName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LastPromoClaimed).HasColumnType("datetime");

                entity.Property(e => e.LastPromoReserved).HasColumnType("datetime");

                entity.Property(e => e.LastStatusUpdate).HasColumnType("datetime");

                entity.Property(e => e.MoneyAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OwnerEmail)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PointsOpCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PointsOpUsedPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.RequiredPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TenantContactEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantContactName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TenantContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<OltpmoneyTransfers>(entity =>
            {
                entity.ToTable("OLTPMoneyTransfers");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BeneficiaryId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.BeneficiaryName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DestinationName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifierUserId).HasMaxLength(450);

                entity.Property(e => e.ReferenceCode)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.TransferedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.ModifierUser)
                    .WithMany(p => p.OltpmoneyTransfers)
                    .HasForeignKey(d => d.ModifierUserId)
                    .HasConstraintName("FK_OLTPMoneyTransfers_AspNetUsers");
            });

            modelBuilder.Entity<OltpmoneyWithdrawals>(entity =>
            {
                entity.ToTable("OLTPMoneyWithdrawals");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BeneficiaryId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.BeneficiaryName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.FollowUpCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LocalCurrencyAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.LocalCurrencyCommissionFee).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.LocalCurrencyRetainedTaxesAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.RequiredPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ServiceAccountRefId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceAccountType)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceInstanceName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateUserModifierId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.WithdrawalCode)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.OltpmoneyWithdrawals)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMoneyWithdrawals_DEFCountries");

                entity.HasOne(d => d.LiquidationMoneyTransfer)
                    .WithMany(p => p.OltpmoneyWithdrawals)
                    .HasForeignKey(d => d.LiquidationMoneyTransferId)
                    .HasConstraintName("FK_OLTPMoneyWithdrawals_OLTPMoneyTransfers");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.OltpmoneyWithdrawals)
                    .HasForeignKey(d => d.MembershipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMoneyWithdrawals_OLTPMemberships");

                entity.HasOne(d => d.UpdateUserModifier)
                    .WithMany(p => p.OltpmoneyWithdrawalsUpdateUserModifier)
                    .HasForeignKey(d => d.UpdateUserModifierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMoneyWithdrawals_AspNetUsersModifier");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpmoneyWithdrawalsUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPMoneyWithdrawal_AspNetUsers");
            });

            modelBuilder.Entity<OltpofferPreferenceOptions>(entity =>
            {
                entity.ToTable("OLTPOfferPreferenceOptions");

                entity.HasIndex(e => new { e.PreferenceId, e.Value })
                    .HasName("IX_OLTPOfferPreferenceOptions_Value")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Price).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.RegularPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.OltpofferPreferenceOptions)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPOfferPreferenceOptions_OLTPImages");

                entity.HasOne(d => d.Preference)
                    .WithMany(p => p.OltpofferPreferenceOptions)
                    .HasForeignKey(d => d.PreferenceId)
                    .HasConstraintName("FK_OLTPOfferPreferenceOptions_OLTPOfferPreferences");
            });

            modelBuilder.Entity<OltpofferPreferences>(entity =>
            {
                entity.ToTable("OLTPOfferPreferences");

                entity.HasIndex(e => new { e.OfferId, e.Name })
                    .HasName("IX_OLTPOfferPreferences_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Hint)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.OltpofferPreferences)
                    .HasForeignKey(d => d.OfferId)
                    .HasConstraintName("FK_OLTPOffferPreferences_OLTPOffers");
            });

            modelBuilder.Entity<Oltpoffers>(entity =>
            {
                entity.ToTable("OLTPOffers");

                entity.HasIndex(e => e.ExpirationDate);

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.OfferType);

                entity.HasIndex(e => e.ReleaseDate);

                entity.HasIndex(e => e.TenantId)
                    .HasName("IX_OLTPOffers_Tenant");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AvailableQuantity).HasDefaultValueSql("((-1))");

                entity.Property(e => e.BroadcastingMsg)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.BroadcastingScheduleType).HasDefaultValueSql("((1))");

                entity.Property(e => e.BroadcastingTitle)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimInstructions)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DealType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayType).HasDefaultValueSql("((2))");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.GeoSegmentationType).HasDefaultValueSql("((1))");

                entity.Property(e => e.IncentiveVariation).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsExclusive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastBroadcastingUsage).HasColumnType("datetime");

                entity.Property(e => e.MainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaxClaimsPerUser).HasDefaultValueSql("((-1))");

                entity.Property(e => e.MaxIncentive).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.MinIncentive).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PublishingStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.PurchasesCountStartDate).HasColumnType("datetime");

                entity.Property(e => e.PurposeType).HasDefaultValueSql("((1))");

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.RelevanceRate).HasDefaultValueSql("((5))");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.TargettingParams)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(19, 2)")
                    .HasDefaultValueSql("((-1))");

                entity.HasOne(d => d.CodeImgNavigation)
                    .WithMany(p => p.OltpoffersCodeImgNavigation)
                    .HasForeignKey(d => d.CodeImg)
                    .HasConstraintName("FK_OLTPOffers_OLTPCodeImages");

                entity.HasOne(d => d.DisplayImage)
                    .WithMany(p => p.OltpoffersDisplayImage)
                    .HasForeignKey(d => d.DisplayImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPOffers_OLTPImages");

                entity.HasOne(d => d.MainCategory)
                    .WithMany(p => p.Oltpoffers)
                    .HasForeignKey(d => d.MainCategoryId)
                    .HasConstraintName("FK_OLTPOffers_OLTPCategories");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Oltpoffers)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_OLTPOffers_DEFTenants");
            });

            modelBuilder.Entity<OltpoffersView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPOffersView");

                entity.Property(e => e.BroadcastingMsg)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.BroadcastingTitle)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimInstructions)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IncentiveVariation).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastBroadcastingUsage).HasColumnType("datetime");

                entity.Property(e => e.MainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaxIncentive).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.MinIncentive).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasesCountStartDate).HasColumnType("datetime");

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.TargettingParams)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<OltpoperationFlowStepLogs>(entity =>
            {
                entity.ToTable("OLTPOperationFlowStepLogs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Discriminator)
                    .HasMaxLength(72)
                    .IsUnicode(false);

                entity.Property(e => e.OperationFlowCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.OriginOpStepLog)
                    .WithMany(p => p.InverseOriginOpStepLog)
                    .HasForeignKey(d => d.OriginOpStepLogId)
                    .HasConstraintName("FK_OLTPOperationFlowStepLogs_OLTPOperationFlowStepLogs");
            });

            modelBuilder.Entity<OltpoperationIssues>(entity =>
            {
                entity.ToTable("OLTPOperationIssues");

                entity.HasIndex(e => new { e.RefType, e.RefId })
                    .HasName("IX_OLTPOperationIssues_Refs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Comments)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.ContactInfo)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpoperationIssues)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OLTPOperationIssues_DEFTenants");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpoperationIssues)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPOperationIssues_AspNetUsers");
            });

            modelBuilder.Entity<OltpoperationIssuesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPOperationIssuesView");

                entity.Property(e => e.Comments)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.ContactInfo)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.TenantContactEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantContactName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.TenantContactPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<OltppaymentInfos>(entity =>
            {
                entity.ToTable("OLTPPaymentInfos");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.CardHolder).HasMaxLength(45);

                entity.Property(e => e.CardLastDigits).HasMaxLength(45);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CvcCheck)
                    .HasColumnName("Cvc_Check")
                    .HasMaxLength(45);

                entity.Property(e => e.ExpMonth)
                    .HasColumnName("Exp_Month")
                    .HasMaxLength(2);

                entity.Property(e => e.ExpYear)
                    .HasColumnName("Exp_Year")
                    .HasMaxLength(5);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.OltppaymentInfos)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPPaymentInfos_DEFCountries");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltppaymentInfos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPPaymentInfos_AspNetUsers");
            });

            modelBuilder.Entity<OltppaymentInfosView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPPaymentInfosView");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.CardHolder).HasMaxLength(45);

                entity.Property(e => e.CardLastDigits).HasMaxLength(45);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CountryFlag)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CvcCheck)
                    .HasColumnName("Cvc_Check")
                    .HasMaxLength(45);

                entity.Property(e => e.ExpMonth)
                    .HasColumnName("Exp_Month")
                    .HasMaxLength(2);

                entity.Property(e => e.ExpYear)
                    .HasColumnName("Exp_Year")
                    .HasMaxLength(5);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<OltppaymentLogs>(entity =>
            {
                entity.ToTable("OLTPPaymentLogs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CashbackTotalAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DebitedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.EarningsIncreasementAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.LiquidationDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ResultMessage)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.AppliedCashbackIncentive)
                    .WithMany(p => p.OltppaymentLogs)
                    .HasForeignKey(d => d.AppliedCashbackIncentiveId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPPaymentLogs_OLTPCashbackIncentives");

                entity.HasOne(d => d.AppliedUserEarningsIncreaser)
                    .WithMany(p => p.OltppaymentLogs)
                    .HasForeignKey(d => d.AppliedUserEarningsIncreaserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPPaymentLogs_DEFEarningsIncreasers");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.OltppaymentLogs)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPPaymentLogs_DEFBranches");

                entity.HasOne(d => d.LiquidationMoneyTransfer)
                    .WithMany(p => p.OltppaymentLogs)
                    .HasForeignKey(d => d.LiquidationMoneyTransferId)
                    .HasConstraintName("FK_OLTPPaymentLogs_OLTPMoneyTransfers");

                entity.HasOne(d => d.PaymentInfo)
                    .WithMany(p => p.OltppaymentLogs)
                    .HasForeignKey(d => d.PaymentInfoId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPPaymentLogs_OLTPPaymentInfos");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltppaymentLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPPaymentLogs_AspNetUsers");
            });

            modelBuilder.Entity<OltppaymentRequests>(entity =>
            {
                entity.ToTable("OLTPPaymentRequests");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.OpCode)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.PayerUserId).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.OltppaymentRequests)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_OLTPPaymentRequests_DEFBranches");

                entity.HasOne(d => d.PaymentLog)
                    .WithMany(p => p.OltppaymentRequests)
                    .HasForeignKey(d => d.PaymentLogId)
                    .HasConstraintName("FK_OLTPPaymentRequests_OLTPPaymentLogs");
            });

            modelBuilder.Entity<OltppreferenceWithOptionView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPPreferenceWithOptionView");

                entity.Property(e => e.OptionCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.OptionPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OptionRegularPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OptionUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.OptionValue)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PreferenceHint)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceUpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OltpproductItemContents>(entity =>
            {
                entity.ToTable("OLTPProductItemContents");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ReferenceUrl)
                    .HasColumnName("ReferenceURL")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.ProductItem)
                    .WithMany(p => p.OltpproductItemContents)
                    .HasForeignKey(d => d.ProductItemId)
                    .HasConstraintName("FK_OLTPBTLContentItems_OLTPProductItems");
            });

            modelBuilder.Entity<OltpproductItemTenantHolders>(entity =>
            {
                entity.ToTable("OLTPProductItemTenantHolders");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdditionalPurchasePoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.AdditionalScanningPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CommissionFee)
                    .HasColumnType("decimal(19, 2)")
                    .HasDefaultValueSql("((10))");

                entity.Property(e => e.CommissionFeeType).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.InStoreLocation)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.NameInReceipt)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.ProductItem)
                    .WithMany(p => p.OltpproductItemTenantHolders)
                    .HasForeignKey(d => d.ProductItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPProductItemTenantHolders_OLTPProductItems");

                entity.HasOne(d => d.TenantHolder)
                    .WithMany(p => p.OltpproductItemTenantHolders)
                    .HasForeignKey(d => d.TenantHolderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPProductItemTenantHolders_DEFTenants");
            });

            modelBuilder.Entity<OltpproductItems>(entity =>
            {
                entity.ToTable("OLTPProductItems");

                entity.HasIndex(e => new { e.TenantId, e.Name })
                    .HasName("IX_OLTPProductItems_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.CommissionFee)
                    .HasColumnType("decimal(19, 2)")
                    .HasDefaultValueSql("((10))");

                entity.Property(e => e.CommissionFeeType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DealType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasePoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ScanningPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.OltpproductItems)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPProductItems_OLTPCategories");

                entity.HasOne(d => d.DisplayImg)
                    .WithMany(p => p.OltpproductItems)
                    .HasForeignKey(d => d.DisplayImgId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPProductItems_OLTPImages");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpproductItems)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_OLTPProductItems_DEFTenants");
            });

            modelBuilder.Entity<OltppurchaseDeliveryDetails>(entity =>
            {
                entity.ToTable("OLTPPurchaseDeliveryDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CancellationFee).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliverPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.DeliveryAddress)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryContactPhoneNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryContactPhoneNumberPrefix)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DispatchAddress)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.DispatchContactPhoneNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.DispatchContactPhoneNumberPrefix)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderCommunicationPayloads).IsUnicode(false);

                entity.Property(e => e.RealDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduledDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DispatchBranch)
                    .WithMany(p => p.OltppurchaseDeliveryDetails)
                    .HasForeignKey(d => d.DispatchBranchId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPPurchaseDeliveryDetails_DEFBranches");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.OltppurchaseDeliveryDetails)
                    .HasForeignKey(d => d.PurchaseId)
                    .HasConstraintName("FK_OLTPPurchaseDeliveryDetails_OLTPPurchases");
            });

            modelBuilder.Entity<OltppurchasedItems>(entity =>
            {
                entity.ToTable("OLTPPurchasedItems");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CashbackAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ChosenPreferences).HasColumnType("xml");

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IncreasementAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OfferComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.OfferKeywords)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OfferMainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OfferPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OfferRegularPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PayedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TenantEarnings).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.AppliedUserEarningsIncreaser)
                    .WithMany(p => p.OltppurchasedItems)
                    .HasForeignKey(d => d.AppliedUserEarningsIncreaserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPPurchasedItems_DEFEarningsIncreasers");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.OltppurchasedItems)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPPurchasedItems_OLTPOffers");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.OltppurchasedItems)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPPurchasedItems_OLTPPurchases");
            });

            modelBuilder.Entity<OltppurchasedItemsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPPurchasedItemsView");

                entity.Property(e => e.CashbackAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ChosenPreferences).HasColumnType("xml");

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IncreasementAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OfferComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.OfferKeywords)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OfferMainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OfferPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OfferRegularPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PayedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PurchaseCode)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PurchaseNumericCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseTotalCashbackTotalAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PurchaseTotalPayedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PurchaseTotalTenantEarnings).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PurchaseUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.TenantEarnings).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Oltppurchases>(entity =>
            {
                entity.ToTable("OLTPPurchases");

                entity.HasIndex(e => e.PurchaseCode)
                    .HasName("IX_OLTPPurchases_Code")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.PurchaseCode)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseNumericCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCashbackTotalAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TotalPayedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TotalTenantEarnings).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.AppliedUserEarningsIncreaser)
                    .WithMany(p => p.Oltppurchases)
                    .HasForeignKey(d => d.AppliedUserEarningsIncreaserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPPurchases_DEFEarningsIncreasers");

                entity.HasOne(d => d.DispatchBranch)
                    .WithMany(p => p.Oltppurchases)
                    .HasForeignKey(d => d.DispatchBranchId)
                    .HasConstraintName("FK_OLTPPurchases_DEFBranches");

                entity.HasOne(d => d.PaymentLog)
                    .WithMany(p => p.Oltppurchases)
                    .HasForeignKey(d => d.PaymentLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPPurchases_OLTPPaymentLogs");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Oltppurchases)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPPurchases_DEFTenants");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Oltppurchases)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPPurchases_AspNetUsers");
            });

            modelBuilder.Entity<OltpraffleWinners>(entity =>
            {
                entity.HasKey(e => new { e.RaffleId, e.UserId });

                entity.ToTable("OLTPRaffleWinners");

                entity.Property(e => e.ClaimDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.ClaimBranch)
                    .WithMany(p => p.OltpraffleWinners)
                    .HasForeignKey(d => d.ClaimBranchId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPRaffleWinners_DEFBranches");

                entity.HasOne(d => d.Raffle)
                    .WithMany(p => p.OltpraffleWinners)
                    .HasForeignKey(d => d.RaffleId)
                    .HasConstraintName("FK_OLTPRaffleWinners_OLTPRaffles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpraffleWinners)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_OLTPRaffleWinners_AspNetUsers");
            });

            modelBuilder.Entity<OltpreceiptPictures>(entity =>
            {
                entity.ToTable("OLTPReceiptPictures");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContainedUniqueValues)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FullContent).IsUnicode(false);

                entity.Property(e => e.PurchasedItems).IsUnicode(false);

                entity.Property(e => e.RelevantContent).IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Img)
                    .WithMany(p => p.OltpreceiptPictures)
                    .HasForeignKey(d => d.ImgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPReceiptPictures_OLTPImages");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.OltpreceiptPictures)
                    .HasForeignKey(d => d.ReceiptId)
                    .HasConstraintName("FK_OLTPReceiptPictures_OLTPReceipts");
            });

            modelBuilder.Entity<OltpreceiptRequestedValidations>(entity =>
            {
                entity.ToTable("OLTPReceiptRequestedValidations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.RegisteredDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.OltpreceiptRequestedValidations)
                    .HasForeignKey(d => d.ReceiptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPReceiptRequestedValidations_OLTPReceipts");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpreceiptRequestedValidations)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPReceiptRequestedValidations_DEFTenants");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpreceiptRequestedValidations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPReceiptRequestedValidations_AspNetUsers");
            });

            modelBuilder.Entity<OltpreceiptSummariesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPReceiptSummariesView");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PreTaxAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.RecordLineRefCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TransactionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserEarnedMoneyAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UserEarnedPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Oltpreceipts>(entity =>
            {
                entity.ToTable("OLTPReceipts");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BranchName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimMark)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CommissionFeeAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ContainedDeals).IsUnicode(false);

                entity.Property(e => e.ContainedUniqueValues)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LegalName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PicturesCount).HasDefaultValueSql("((1))");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PreTaxAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.PurchaseItemsPrices)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasedItems).IsUnicode(false);

                entity.Property(e => e.RetainedTaxAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TaxId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TicketNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserEarnedMoneyAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UserEarnedPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Franchisee)
                    .WithMany(p => p.Oltpreceipts)
                    .HasForeignKey(d => d.FranchiseeId)
                    .HasConstraintName("FK_OLTPReceipts_DEFFranchisees");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Oltpreceipts)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPReceipts_DEFTenants");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Oltpreceipts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPReceipts_AspNetUsers");
            });

            modelBuilder.Entity<OltpreceiptsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPReceiptsView");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimMark)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ContainedDeals).IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LegalName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PictureFullContent).IsUnicode(false);

                entity.Property(e => e.PicturePurchasedItems).IsUnicode(false);

                entity.Property(e => e.PictureRelevantContent).IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.PurchasedItems).IsUnicode(false);

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TaxId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TicketNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<OltprewardToAwards>(entity =>
            {
                entity.ToTable("OLTPRewardToAwards");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.OriginatorName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.OltprewardToAwards)
                    .HasForeignKey(d => d.OfferId)
                    .HasConstraintName("FK_OLTPRewardToAwards_OLTPOffers");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltprewardToAwards)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPRewardToAwards_DEFBranches");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltprewardToAwards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_OLTPRewardToAwards_AspNetUsers");
            });

            modelBuilder.Entity<OltprewardedUsers>(entity =>
            {
                entity.ToTable("OLTPRewardedUsers");

                entity.HasIndex(e => e.AccountNumber)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltprewardedUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPRewardedUsers_AspNetUsers");
            });

            modelBuilder.Entity<Oltprewards>(entity =>
            {
                entity.ToTable("OLTPRewards");

                entity.HasIndex(e => e.RewardId)
                    .HasName("IX_OLTPRaffles_RewardId")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MinMembershipLevel).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RaffleDate).HasColumnType("datetime");

                entity.Property(e => e.TimeOutDaysBetweenRedemption).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.EarningsIncreaser)
                    .WithMany(p => p.Oltprewards)
                    .HasForeignKey(d => d.EarningsIncreaserId)
                    .HasConstraintName("FK_OLTPRaffles_DEFEarningsIncreasers");

                entity.HasOne(d => d.Reward)
                    .WithOne(p => p.Oltprewards)
                    .HasForeignKey<Oltprewards>(d => d.RewardId)
                    .HasConstraintName("FK_OLTPRaffles_OLTPOffers");
            });

            modelBuilder.Entity<OltprewardsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPRewardsView");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IncreaserFactor).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RaffleDate).HasColumnType("datetime");

                entity.Property(e => e.RewardDescription)
                    .IsRequired()
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.RewardName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<OltpsavedItems>(entity =>
            {
                entity.ToTable("OLTPSavedItems");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.TenantHolder)
                    .WithMany(p => p.OltpsavedItems)
                    .HasForeignKey(d => d.TenantHolderId)
                    .HasConstraintName("FK_OLTPSavedItems_OLTPItemTenantHolders");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpsavedItems)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPSavedItems_DEFTenants");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpsavedItems)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPSavedItems_AspNetUsers");
            });

            modelBuilder.Entity<OltpsearchLogs>(entity =>
            {
                entity.ToTable("OLTPSearchLogs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpsearchLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPSearchLogs_AspNetUsers");

                entity.HasOne(d => d.Oltpsearchables)
                    .WithMany(p => p.OltpsearchLogs)
                    .HasForeignKey(d => new { d.ReferenceId, d.IndexId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPSearchLogs_OLTPSearchables");
            });

            modelBuilder.Entity<Oltpsearchables>(entity =>
            {
                entity.HasKey(e => new { e.ReferenceId, e.IndexOwner })
                    .HasName("PK__OLTPSear__D05ABA868B098E3A");

                entity.ToTable("OLTPSearchables");

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.ReferenceType);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Classification)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Icon)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.LastSearch).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Oltpsearchables)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_OLTPSearchables_DEFCountryId");

                entity.HasOne(d => d.IndexOwnerNavigation)
                    .WithMany(p => p.Oltpsearchables)
                    .HasForeignKey(d => d.IndexOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPSearchables_DEFSearchIndexes");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Oltpsearchables)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_OLTPSearchables_DEFTenants");
            });

            modelBuilder.Entity<OltpsearchablesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPSearchablesView");

                entity.Property(e => e.AppName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Classification)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Icon)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.IndexName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.LastSearch).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OltpshoppingCartItems>(entity =>
            {
                entity.ToTable("OLTPShoppingCartItems");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ChosenPreferences).HasColumnType("xml");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.OltpshoppingCartItems)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPShoppingCartItems_OLTPOffers");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpshoppingCartItems)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPShoppingCartItems_DEFTenants");
            });

            modelBuilder.Entity<OltpshoppingCartItemsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPShoppingCartItemsView");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ChosenPreferences).HasColumnType("xml");

                entity.Property(e => e.ClaimInstructions)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IncentiveVariation).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.MainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaxIncentive).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.MinIncentive).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.OfferCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.OfferUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.PurchasesCountStartDate).HasColumnType("datetime");

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.TargettingParams)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<OltptextMessageLogs>(entity =>
            {
                entity.ToTable("OLTPTextMessageLogs");

                entity.HasIndex(e => e.TenantId);

                entity.HasIndex(e => new { e.PurposeType, e.ReferenceType })
                    .HasName("IX_OLTPTextMessageLogs_PurposeType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContainedCode)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.GatewayDirection)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.GatewayErrorMsg)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.GatewayMsgId)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.GatewayMsgStatus)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.GatewayMsgUri)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.GatewayPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.GatewayPriceCurrency)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GatewaySentDate).HasColumnType("datetime");

                entity.Property(e => e.GatewayUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.LocationData)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MediaUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiverUserId).HasMaxLength(450);

                entity.Property(e => e.SenderPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.TargetPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<OltptransactionLocations>(entity =>
            {
                entity.ToTable("OLTPTransactionLocations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BranchName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.BroadcasterName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.OltptransactionLocations)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPTransactionLocations_OLTPTransactions");
            });

            modelBuilder.Entity<OltptransactionLocationsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPTransactionLocationsView");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.BroadcasterName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CompletedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Oltptransactions>(entity =>
            {
                entity.ToTable("OLTPTransactions");

                entity.HasIndex(e => e.Name);

                entity.HasIndex(e => e.ReferenceType)
                    .HasName("IX_OLTPTransactions_RedeemedType");

                entity.HasIndex(e => e.TenantId)
                    .HasName("IX_OLTPMembershipTransactions_TenantId");

                entity.HasIndex(e => new { e.UserId, e.ReferenceId })
                    .HasName("IX_OLTPTransactions_ReferenceId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Completed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CompletedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DealType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Description)
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.SelectedAspects)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShowToUser)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId).IsRequired();

                entity.Property(e => e.Validated)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");

                entity.HasOne(d => d.CodeImgNavigation)
                    .WithMany(p => p.Oltptransactions)
                    .HasForeignKey(d => d.CodeImg)
                    .HasConstraintName("FK_OLTPTransactions_OLTPImagesCode");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Oltptransactions)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OLTPTransactions_OLTPEmployees");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Oltptransactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPTransactions_AspNetUsers");
            });

            modelBuilder.Entity<OltptransactionsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPTransactionsView");

                entity.Property(e => e.Code)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CompletedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.SelectedAspects)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<OltpuserInterestRecords>(entity =>
            {
                entity.ToTable("OLTPUserInterestRecords");

                entity.HasIndex(e => new { e.UserId, e.InterestId })
                    .HasName("IX_OLTPUserInterestRecords_UserInterest");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.HerarchyLevel).HasDefaultValueSql("((4))");

                entity.Property(e => e.Score).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.OltpuserInterests)
                    .WithMany(p => p.OltpuserInterestRecords)
                    .HasForeignKey(d => new { d.UserId, d.InterestId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPUserInterestRecords_OLTPUserInterests");
            });

            modelBuilder.Entity<OltpuserInterests>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.InterestId })
                    .HasName("PK__tmp_ms_x__7580FE8A43CF1011");

                entity.ToTable("OLTPUserInterests");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_DEFUserInterests_UserId");

                entity.HasIndex(e => new { e.InterestType, e.InterestId })
                    .HasName("IX_DEFUserInterests_Interest");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.HerarchyLevel).HasDefaultValueSql("((4))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OriginType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Score).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpuserInterests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_DEFUserInterests_UserId");
            });

            modelBuilder.Entity<OltpuserInterestsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OLTPUserInterestsView");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Score).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<OltpuserInviteRelations>(entity =>
            {
                entity.ToTable("OLTPUserInviteRelations");

                entity.HasIndex(e => e.AncestorUserId);

                entity.HasIndex(e => new { e.AncestorUserId, e.JoinedUserId })
                    .HasName("AK_OLTPUserInviteRelations_SK")
                    .IsUnique();

                entity.HasIndex(e => new { e.JoinedUserId, e.HerarchyLevel })
                    .HasName("IX_OLTPUserInviteRelations_JoinedUserId")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AncestorBonusExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.AncestorFirstPurchaseMoney).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.AncestorUserId).IsRequired();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.JoinedUserId).IsRequired();

                entity.Property(e => e.JoiningBonusCommissionMoney).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.JoiningBonusExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.JoiningFirstPurchaseMoney).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.RemainingBonusCommissionMoney).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.ParentRelation)
                    .WithMany(p => p.InverseParentRelation)
                    .HasForeignKey(d => d.ParentRelationId)
                    .HasConstraintName("FK_OLTPUserInviteRelations_OLTPUserInviteRelations");
            });

            modelBuilder.Entity<OltpuserLocationLogs>(entity =>
            {
                entity.ToTable("OLTPUserLocationLogs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OltpuserLocationLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPUserLocationLogs_AspNetUsers");
            });

            modelBuilder.Entity<OltpuserPersonalIdLinkLogs>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PersonalId });

                entity.ToTable("OLTPUserPersonalIdLinkLogs");

                entity.Property(e => e.PersonalId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<OltpuserPhoneNumberLinkLogs>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PhoneNumber });

                entity.ToTable("OLTPUserPhoneNumberLinkLogs");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<OltpvalidatePurchaseRegistries>(entity =>
            {
                entity.ToTable("OLTPValidatePurchaseRegistries");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClubPointsGenerated).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.WalletPointsGenerated).HasColumnType("decimal(19, 2)");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.OltpvalidatePurchaseRegistries)
                    .HasForeignKey(d => d.MembershipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPValidatePurchaseRegistries_OLTPMemberships");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.OltpvalidatePurchaseRegistries)
                    .HasForeignKey(d => d.ReceiptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPValidatePurchaseRegistries_OLTPReceipts");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.OltpvalidatePurchaseRegistries)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OLTPValidatePurchaseRegistries_DEFTenants");
            });

            modelBuilder.Entity<OltpvisitorsLog>(entity =>
            {
                entity.ToTable("OLTPVisitorsLog");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContinentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeviceModel)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Hostname)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.OsVersion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RegionCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiresUtc)
                    .HasColumnName("ExpiresUTC")
                    .HasColumnType("datetime");

                entity.Property(e => e.HashedValue).HasMaxLength(2048);

                entity.Property(e => e.IssuedUtc)
                    .HasColumnName("IssuedUTC")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TempbroadcasterBranchesRelatedData>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPBroadcasterBranchesRelatedData");

                entity.Property(e => e.BroadcasterExternalId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignDefaultContentMsg)
                    .HasMaxLength(480)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignDefaultTitleMsg)
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.ContainedTenantCampaignDefaultContentMsg)
                    .HasMaxLength(480)
                    .IsUnicode(false);

                entity.Property(e => e.ContainedTenantCampaignDefaultTitleMsg)
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.ContainedTenantCategoryScore).HasColumnType("decimal(19, 3)");

                entity.Property(e => e.ContainedTenantDefaultCommissionPercentage).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ContainedTenantName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ContainedTenantScore).HasColumnType("decimal(19, 3)");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.TenantCategoryScore).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TenantDefaultCommissionPercentage).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TenantName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TenantScore).HasColumnType("decimal(16, 3)");
            });

            modelBuilder.Entity<TempbroadcastingOffersLogs>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPBroadcastingOffersLogs");

                entity.Property(e => e.BranchDescriptiveAddress)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.BranchInquiriesPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BranchLatitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchLongitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BroadcastingMsg)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.BroadcastingTitle)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimInstructions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LogRecordExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.MainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MsgContent)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.MsgTitle)
                    .IsRequired()
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.OfferExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.PreferenceIcon)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.PurchasesCountStartDate).HasColumnType("datetime");

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.RelevanceScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.SentDate).HasColumnType("datetime");

                entity.Property(e => e.TargettingParams)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<TempcashbackIncentivesPreferenceBranches>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPCashbackIncentivesPreferenceBranches");

                entity.Property(e => e.BranchDescriptiveAddress)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.BranchInquiriesPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BranchLatitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchLongitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Keywords)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.MaxValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.MinPurchasedAmount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceIcon)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.PreviousUnitValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PurchasedAmountBlock).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Rules)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.TenantCategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.UnitValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ValidHours)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ValidMonthDays)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ValidWeekDays)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TempclaimableTransactions>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPClaimableTransactions");

                entity.Property(e => e.BranchDescriptiveAddress)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.BranchInquiriesPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BranchLatitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchLongitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CompletedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.TenantCategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<Tempclubs>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPClubs");

                entity.Property(e => e.AvailablePoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.BranchDescriptiveAddress)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.BranchInquiriesPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimDate).HasColumnType("datetime");

                entity.Property(e => e.ClaimInstructions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastClaimDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.RaffleDate).HasColumnType("datetime");

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.RelevanceScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.Username).HasMaxLength(256);

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<TempmembershipDetails>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPMembershipDetails");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.BranchPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimedRewardsStartDate).HasColumnType("datetime");

                entity.Property(e => e.EnabledMoneyAmounts)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.LevelEnabledActions)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.LevelIconUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MembershipLevelLastEvaluation).HasColumnType("datetime");

                entity.Property(e => e.MembershipUsedPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TenantCurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.UserName).HasMaxLength(64);
            });

            modelBuilder.Entity<TempmembershipPointOps>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPMembershipPointOps");

                entity.Property(e => e.AvailablePoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Details)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.UsedPoints).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<TempofferDetails>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPOfferDetails");

                entity.Property(e => e.BroadcastingMsg)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.BroadcastingTitle)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimInstructions)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IncentiveVariation).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastBroadcastingUsage).HasColumnType("datetime");

                entity.Property(e => e.MainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaxIncentive).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.MinIncentive).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasesCountStartDate).HasColumnType("datetime");

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.TargettingParams)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<TempoffersPreferenceBranches>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPOffersPreferenceBranches");

                entity.Property(e => e.BranchDescriptiveAddress)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.BranchInquiriesPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BranchLatitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchLongitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimInstructions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.MainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceIcon)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreferenceScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.PurchasesCountStartDate).HasColumnType("datetime");

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.RelevanceScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.TargettingParams)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.TenantCategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenantScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<Temppreferences>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPPreferences");

                entity.Property(e => e.Icon)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Score).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<TemprewardDetails>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPRewardDetails");

                entity.Property(e => e.AvailablePoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.BranchDescriptiveAddress)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.BranchInquiriesPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BranchLatitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchLongitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimDate).HasColumnType("datetime");

                entity.Property(e => e.ClaimInstructions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EarningsIncreaserExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.EarningsIncreaserReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.MainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasesCountStartDate).HasColumnType("datetime");

                entity.Property(e => e.RaffleDate).HasColumnType("datetime");

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.RelevanceScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.TenantCategoryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.TenantScore).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.UserName)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<TemprewardOverviews>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPRewardOverviews");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimInstructions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.ComplementaryHint)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Conditions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EarningsIncreaserExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.EarningsIncreaserReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Keywords)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.MainHint)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(360)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasesCountStartDate).HasColumnType("datetime");

                entity.Property(e => e.RaffleDate).HasColumnType("datetime");

                entity.Property(e => e.RegularValue).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Rules)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<TempsearchableLogs>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPSearchableLogs");

                entity.Property(e => e.Category)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Classification)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Details)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Icon)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tempstates>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPStates");

                entity.Property(e => e.CountryFlag)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.StateLatitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.StateLongitude).HasColumnType("decimal(14, 10)");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TempuserInterestsWithFactor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMPUserInterestsWithFactor");

                entity.Property(e => e.DatesRange)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DaysOfWeekRange)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.HoursRange)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MonthsRange)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Score).HasColumnType("decimal(16, 3)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<UserDataForTokenView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserDataForTokenView");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AvailablePoints).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CountryFlag)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencySymbol)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Language)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastestFbversion)
                    .IsRequired()
                    .HasColumnName("LastestFBVersion")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastestWebVersion)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastestiOsversion)
                    .IsRequired()
                    .HasColumnName("LastestiOSVersion")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ProfilePicUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsedPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<UserDataView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserDataView");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AppleId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCurrencySymbol)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CountryFlag)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPhonePrefix)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Fbid)
                    .HasColumnName("FBId")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.GoogleId)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.InvitorUserId).HasMaxLength(450);

                entity.Property(e => e.LastAppOpen).HasColumnType("datetime");

                entity.Property(e => e.LastOfferRedemption).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.ProfilePicUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveSmsmarketing).HasColumnName("ReceiveSMSMarketing");

                entity.Property(e => e.ReferenceCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<UserWithLocationAndMembershipDataView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserWithLocationAndMembershipDataView");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AvailablePoints).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCurrencySymbol)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CountryFlag)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPhonePrefix)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Language)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PersonalId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicUrl)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsedPoints).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
