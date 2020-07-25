using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Membership;
using YOY.DTO.Entities.Misc.MembershipOperation;
using YOY.DTO.Entities.Misc.ValidPurchaseRegistries;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class MembershipManager
    {
        #region PROPERTIES_AND_RESOURCES

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private readonly BusinessObjects _businessObjects;

        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS METHODS                                                                                                                                  //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        #region MEMBERSHIPS

        private string GetGenderName(string gender)
        {
            string genderName = gender switch
            {
                "M" => Resources.Male,
                "F" => Resources.Female,
                "O" => Resources.Other,
                "-" => Resources.NotSpecified,
                _ => "-",
            };
            return genderName;
        }

        /// <summary>
        /// Retrieves all subscriptions of tenant
        /// </summary>
        /// <returns></returns>
        public List<Membership> Gets(int activeState, int pageSize, int pageNumber)
        {
            List<Membership> subscriptions = new List<Membership>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.OltpmembershipsView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.OltpmembershipsView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.OltpmembershipsView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                Membership subscription = null;
                foreach (OltpmembershipsView item in query)
                {

                    subscription = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AvailableLoyaltyPoints = item.AvailablePoints ?? 0,
                        SoonToExpire = 0,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = false,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        DateOfBirth = item.DateOfBirth,
                        Gender = item.Gender,
                        Username = item.Username,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel,
                        MembershipLevelName = item.LevelName,
                        LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                        MaxGeneratedPoints = item.MaxGeneratedPoints,
                        MinGeneratedPoints = item.MinGeneratedPoints,
                        EvaluationMonths = item.EvaluationMonths,
                        PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                        CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                        MonetaryConversionFactor = item.MonetaryConversionFactor,
                        PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                        EnabledMoneyAmounts = item.EnabledMoneyAmounts
                    };

                    if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                    {
                        subscription.IsNewUser = true;
                    }

                    subscriptions.Add(subscription);
                }
            }
            catch (Exception e)
            {
                subscriptions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return subscriptions;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Retrieve all memberships from a user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Membership> Gets(string userId, int pageSize, int pageNumber)
        {
            List<Membership> subscriptions = new List<Membership>();

            try
            {
                var query = (from x in this._businessObjects.Context.OltpmembershipsView
                             where x.UserId == userId
                             orderby x.LastPromoClaimed descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                Membership subscription = null;

                foreach (OltpmembershipsView item in query)
                {
                    subscription = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AvailableLoyaltyPoints = item.AvailablePoints ?? 0,
                        SoonToExpire = 0,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = false,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        DateOfBirth = item.DateOfBirth,
                        Gender = item.Gender,
                        Username = item.Username,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel,
                        MembershipLevelName = item.LevelName,
                        LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                        MaxGeneratedPoints = item.MaxGeneratedPoints,
                        MinGeneratedPoints = item.MinGeneratedPoints,
                        EvaluationMonths = item.EvaluationMonths,
                        PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                        CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                        MonetaryConversionFactor = item.MonetaryConversionFactor,
                        PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                        EnabledMoneyAmounts = item.EnabledMoneyAmounts
                    };

                    if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                    {
                        subscription.IsNewUser = true;
                    }

                    subscriptions.Add(subscription);
                }
            }
            catch (Exception e)
            {
                subscriptions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return subscriptions;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Retrieve all memberships from a user
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public List<Membership> Gets(long accountNumber, int activeState, int blockedState, bool getSoonToExpirePoints, int pageSize, int pageNumber)
        {
            List<Membership> subscriptions = new List<Membership>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (blockedState)
                        {
                            case BlockedStates.All:
                                query = (from x in this._businessObjects.Context.OltpmembershipsView
                                         where x.AccountNumber == accountNumber
                                         orderby x.LastPromoClaimed descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case BlockedStates.Allowed:
                                query = (from x in this._businessObjects.Context.OltpmembershipsView
                                         where x.AccountNumber == accountNumber && !x.Blocked
                                         orderby x.LastPromoClaimed descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case BlockedStates.Blocked:
                                query = (from x in this._businessObjects.Context.OltpmembershipsView
                                         where x.AccountNumber == accountNumber && x.Blocked
                                         orderby x.LastPromoClaimed descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                    case ActiveStates.Active:
                        switch (blockedState)
                        {
                            case BlockedStates.All:
                                query = (from x in this._businessObjects.Context.OltpmembershipsView
                                         where x.AccountNumber == accountNumber && x.IsActive
                                         orderby x.LastPromoClaimed descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case BlockedStates.Allowed:
                                query = (from x in this._businessObjects.Context.OltpmembershipsView
                                         where x.AccountNumber == accountNumber && x.IsActive && !x.Blocked
                                         orderby x.LastPromoClaimed descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case BlockedStates.Blocked:
                                query = (from x in this._businessObjects.Context.OltpmembershipsView
                                         where x.AccountNumber == accountNumber && x.IsActive && x.Blocked
                                         orderby x.LastPromoClaimed descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:
                        switch (blockedState)
                        {
                            case BlockedStates.All:
                                query = (from x in this._businessObjects.Context.OltpmembershipsView
                                         where x.AccountNumber == accountNumber && !x.IsActive
                                         orderby x.LastPromoClaimed descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case BlockedStates.Allowed:
                                query = (from x in this._businessObjects.Context.OltpmembershipsView
                                         where x.AccountNumber == accountNumber && !x.IsActive && !x.Blocked
                                         orderby x.LastPromoClaimed descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case BlockedStates.Blocked:
                                query = (from x in this._businessObjects.Context.OltpmembershipsView
                                         where x.AccountNumber == accountNumber && !x.IsActive && x.Blocked
                                         orderby x.LastPromoClaimed descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                }

                Membership subscription = null;
                foreach (OltpmembershipsView item in query)
                {


                    subscription = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AvailableLoyaltyPoints = item.AvailablePoints ?? 0,
                        SoonToExpire = 0,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = false,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        DateOfBirth = item.DateOfBirth,
                        Gender = item.Gender,
                        Username = item.Username,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel,
                        MembershipLevelName = item.LevelName,
                        LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                        MaxGeneratedPoints = item.MaxGeneratedPoints,
                        MinGeneratedPoints = item.MinGeneratedPoints,
                        EvaluationMonths = item.EvaluationMonths,
                        PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                        CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                        MonetaryConversionFactor = item.MonetaryConversionFactor,
                        PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                        EnabledMoneyAmounts = item.EnabledMoneyAmounts
                    };

                    if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                    {
                        subscription.IsNewUser = true;
                    }

                    if (subscription != null && getSoonToExpirePoints)
                    {
                        //Now it's time to count the available points
                        var queryPoints = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                          where x.BeneficiaryTenantId == subscription.TenantId && x.ProviderMembershipId == subscription.Id && x.Status == MembershipPointsOperationStatuses.Accessible && x.Type == MembershipPointsOperationTypes.PointsBalance &&
                                          (x.ReferenceType == MembershipPointsOperationReferenceTypes.NoRef || x.ReferenceType == MembershipPointsOperationReferenceTypes.Transaction || x.ReferenceType == MembershipPointsOperationReferenceTypes.InvitedUser) &&
                                          x.ExpirationDate > DateTime.UtcNow
                                          select x;

                        if (queryPoints != null)
                        {
                            foreach (OltpmembershipPointsOperations itemOps in queryPoints)
                            {
                                if ((itemOps.ExpirationDate - DateTime.UtcNow).Days <= MembershipConfigValues.SoonToExpireDaysLeftIndicator)
                                {
                                    subscription.SoonToExpire += itemOps.AvailablePoints;
                                }
                            }
                        }
                    }

                    subscriptions.Add(subscription);
                }
            }
            catch (Exception e)
            {
                subscriptions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return subscriptions;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Returns all subscriptions created
        /// in less that the days quantity
        /// passed by parameter
        /// </summary>
        /// <param name="daysFromSubscription"></param>
        /// <param name="subscriptionDate"></param>
        /// <returns></returns>
        public List<Membership> Gets(DateTime subscriptionDate, int daysFromSubscription, int pageSize, int pageNumber)
        {
            List<Membership> subscriptions = new List<Membership>();

            try
            {
                var query = (from x in this._businessObjects.Context.OltpmembershipsView
                             where x.TenantId == this._businessObjects.Tenant.TenantId &&
                                     (subscriptionDate - x.CreatedDate).Days <= daysFromSubscription
                             orderby x.LastPromoClaimed descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                Membership subscription = null;
                foreach (OltpmembershipsView item in query)
                {
                    subscription = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AvailableLoyaltyPoints = item.AvailablePoints ?? 0,
                        SoonToExpire = 0,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = false,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        DateOfBirth = item.DateOfBirth,
                        Gender = item.Gender,
                        Username = item.Username,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel,
                        MembershipLevelName = item.LevelName,
                        LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                        MaxGeneratedPoints = item.MaxGeneratedPoints,
                        MinGeneratedPoints = item.MinGeneratedPoints,
                        EvaluationMonths = item.EvaluationMonths,
                        PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                        CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                        MonetaryConversionFactor = item.MonetaryConversionFactor,
                        PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                        EnabledMoneyAmounts = item.EnabledMoneyAmounts
                    };

                    if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                    {
                        subscription.IsNewUser = true;
                    }

                    subscriptions.Add(subscription);
                }
            }
            catch (Exception e)
            {
                subscriptions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return subscriptions;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Retrieves the subscription for a username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Membership Get(string userId, Guid tenantId)
        {
            Membership subscription = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpmembershipsView
                            where x.TenantId == tenantId && x.UserId == userId
                            select x;



                foreach (OltpmembershipsView item in query)
                {
                    subscription = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        SoonToExpire = 0,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = false,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel
                    };

                    if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                    {
                        subscription.IsNewUser = true;
                    }
                }

            }
            catch (Exception e)
            {
                subscription = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return subscription;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves the subscription for a username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Membership Get(string key, int userKey, Guid tenantId, bool getSoonToExpirePoints)
        {
            Membership subscription = null;

            try
            {
                var query = (dynamic)null;


                switch (userKey)
                {
                    case UserKeys.UserId:
                        query = from x in this._businessObjects.Context.OltpmembershipsView
                                where x.TenantId == tenantId && x.UserId == key
                                select x;
                        break;
                    case UserKeys.Username:
                        query = from x in this._businessObjects.Context.OltpmembershipsView
                                where x.TenantId == tenantId && x.Username == key
                                select x;
                        break;

                }



                foreach (OltpmembershipsView item in query)
                {
                    subscription = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AvailableLoyaltyPoints = item.AvailablePoints ?? 0,
                        SoonToExpire = 0,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = false,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        DateOfBirth = item.DateOfBirth,
                        Gender = item.Gender,
                        Username = item.Username,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel,
                        MembershipLevelName = item.LevelName,
                        LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                        MaxGeneratedPoints = item.MaxGeneratedPoints,
                        MinGeneratedPoints = item.MinGeneratedPoints,
                        EvaluationMonths = item.EvaluationMonths,
                        PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                        CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                        MonetaryConversionFactor = item.MonetaryConversionFactor,
                        PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                        EnabledMoneyAmounts = item.EnabledMoneyAmounts
                    };

                    if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                    {
                        subscription.IsNewUser = true;
                    }
                }

                if (subscription != null && getSoonToExpirePoints)
                {
                    //Now it's time to count the available points
                    var queryPoints = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                      where x.BeneficiaryTenantId == subscription.TenantId && x.ProviderMembershipId == subscription.Id && x.Status == MembershipPointsOperationStatuses.Accessible &&
                                      x.Type == MembershipPointsOperationTypes.PointsBalance && x.ExpirationDate > DateTime.UtcNow
                                      select x;

                    if (queryPoints != null)
                    {
                        foreach (OltpmembershipPointsOperations item in queryPoints)
                        {
                            if ((item.ExpirationDate.Subtract(DateTime.UtcNow)).Days <= MembershipConfigValues.SoonToExpireDaysLeftIndicator)
                            {
                                subscription.SoonToExpire += item.AvailablePoints;
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                subscription = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return subscription;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        public Membership Get(long accountNumber, Guid tenantId, bool getSoonToExpirePoints)
        {
            Membership subscription = null;

            try
            {

                var query = from x in this._businessObjects.Context.OltpmembershipsView
                            where x.TenantId == tenantId && x.AccountNumber == accountNumber
                            select x;


                foreach (OltpmembershipsView item in query)
                {
                    subscription = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AvailableLoyaltyPoints = item.AvailablePoints ?? 0,
                        SoonToExpire = 0,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = false,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        Username = item.Username,
                        DateOfBirth = item.DateOfBirth,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel,
                        MembershipLevelName = item.LevelName,
                        LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                        MaxGeneratedPoints = item.MaxGeneratedPoints,
                        MinGeneratedPoints = item.MinGeneratedPoints,
                        EvaluationMonths = item.EvaluationMonths,
                        PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                        CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                        MonetaryConversionFactor = item.MonetaryConversionFactor,
                        PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                        EnabledMoneyAmounts = item.EnabledMoneyAmounts
                    };

                    subscription.Gender = GetGenderName(item.Gender);

                    if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                    {
                        subscription.IsNewUser = true;
                    }
                }

                if (subscription != null && getSoonToExpirePoints)
                {
                    //Now it's time to count the available points
                    var queryPoints = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                      where x.BeneficiaryTenantId == subscription.TenantId && x.ProviderMembershipId == subscription.Id && x.Status == MembershipPointsOperationStatuses.Accessible &&
                                      x.Type == MembershipPointsOperationTypes.PointsBalance && x.ExpirationDate > DateTime.UtcNow
                                      select x;

                    if (queryPoints != null)
                    {
                        foreach (OltpmembershipPointsOperations item in queryPoints)
                        {
                            if ((item.ExpirationDate.Subtract(DateTime.UtcNow)).Days <= MembershipConfigValues.SoonToExpireDaysLeftIndicator)
                            {
                                subscription.SoonToExpire += item.AvailablePoints;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                subscription = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return subscription;
        }

        public Membership Get(string username, Guid tenantId, bool getSoonToExpirePoints)
        {
            Membership subscription = null;

            try
            {

                var query = from x in this._businessObjects.Context.OltpmembershipsView
                            where x.TenantId == tenantId && x.Username == username
                            select x;


                foreach (OltpmembershipsView item in query)
                {
                    subscription = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AvailableLoyaltyPoints = item.AvailablePoints ?? 0,
                        SoonToExpire = 0,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = false,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        Username = item.Username,
                        DateOfBirth = item.DateOfBirth,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel,
                        MembershipLevelName = item.LevelName,
                        LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                        MaxGeneratedPoints = item.MaxGeneratedPoints,
                        MinGeneratedPoints = item.MinGeneratedPoints,
                        EvaluationMonths = item.EvaluationMonths,
                        PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                        CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                        MonetaryConversionFactor = item.MonetaryConversionFactor,
                        PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                        EnabledMoneyAmounts = item.EnabledMoneyAmounts
                    };

                    subscription.Gender = GetGenderName(item.Gender);

                    if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                    {
                        subscription.IsNewUser = true;
                    }
                }

                if (subscription != null && getSoonToExpirePoints)
                {
                    //Now it's time to count the available points
                    var queryPoints = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                      where x.BeneficiaryTenantId == subscription.TenantId && x.ProviderMembershipId == subscription.Id && x.Status == MembershipPointsOperationStatuses.Accessible &&
                                      x.Type == MembershipPointsOperationTypes.PointsBalance && x.ExpirationDate > DateTime.UtcNow
                                      select x;

                    if (queryPoints != null)
                    {
                        foreach (OltpmembershipPointsOperations item in queryPoints)
                        {
                            if ((item.ExpirationDate.Subtract(DateTime.UtcNow)).Days <= MembershipConfigValues.SoonToExpireDaysLeftIndicator)
                            {
                                subscription.SoonToExpire += item.AvailablePoints;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                subscription = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return subscription;
        }


        /// <summary>
        /// Retrieve a specific subscription
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        public Membership Get(Guid subscriptionId, Guid tenantId, bool getSoonToExpirePoints)
        {
            Membership subscription = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpmembershipsView
                            where x.TenantId == tenantId && x.Id == subscriptionId
                            select x;

                foreach (OltpmembershipsView item in query)
                {

                    subscription = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AvailableLoyaltyPoints = 0,
                        SoonToExpire = 0,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = false,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        Username = item.Username,
                        DateOfBirth = item.DateOfBirth,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel,
                        MembershipLevelName = item.LevelName,
                        LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                        MaxGeneratedPoints = item.MaxGeneratedPoints,
                        MinGeneratedPoints = item.MinGeneratedPoints,
                        EvaluationMonths = item.EvaluationMonths,
                        PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                        CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                        MonetaryConversionFactor = item.MonetaryConversionFactor,
                        PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                        EnabledMoneyAmounts = item.EnabledMoneyAmounts
                    };

                    subscription.Gender = GetGenderName(item.Gender);

                    if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                    {
                        subscription.IsNewUser = true;
                    }

                }

                if (subscription != null && getSoonToExpirePoints)
                {
                    //Now it's time to count the available points
                    var queryPoints = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                      where x.BeneficiaryTenantId == subscription.TenantId && x.ProviderMembershipId == subscription.Id && x.Status == MembershipPointsOperationStatuses.Accessible &&
                                      x.Type == MembershipPointsOperationTypes.PointsBalance && x.ExpirationDate > DateTime.UtcNow
                                      select x;

                    if (queryPoints != null)
                    {
                        foreach (OltpmembershipPointsOperations item in queryPoints)
                        {
                            if (item.ExpirationDate.Subtract(DateTime.UtcNow).Days <= MembershipConfigValues.SoonToExpireDaysLeftIndicator)
                            {
                                subscription.SoonToExpire += item.AvailablePoints;
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                subscription = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return subscription;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Creates a new subscription for a user to a tenant
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="originType"></param>
        /// <returns></returns>
        public Membership Post(string userKey, int keyType, Guid tenantId, int originType, int welcomePoints, int objectiveType)
        {
            Membership membership = null;
            Oltpmemberships subscription = null;

            try
            {
                string userId = "";

                switch (keyType)
                {
                    case UserKeys.UserId:
                        userId = userKey;
                        break;
                    case UserKeys.Username:
                        var queryUsers = from x in this._businessObjects.Context.AspNetUsers
                                         where x.UserName == userKey
                                         select x;

                        if (queryUsers != null)
                        {
                            foreach (AspNetUsers item in queryUsers)
                            {
                                userId = item.Id;
                            }
                        }
                        break;

                }

                subscription = new Oltpmemberships
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    UserId = userId,
                    OriginType = originType,
                    IsActive = true,
                    Blocked = false,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    UsedLoyaltyPoints = 0,
                    CustomerRanking = 0,
                    ReceiveEmailMarketing = true,
                    ReceiveSmsmarketing = true,
                    LastLevelEvaluation = DateTime.UtcNow,
                    LastPromoClaimed = null,
                    LastPromoReserved = null,
                    ClaimedPromos = 0,
                    ClaimedRewards = 0,
                    ClaimedRewardsStartDate = null,
                    MembershipLevel = 1//Thr 1st level will be always 1
                };

                OltpmembershipPointsOperations newOperation = new OltpmembershipPointsOperations
                {
                    Id = Guid.NewGuid(),
                    ProviderMembershipId = subscription.Id,
                    BeneficiaryMembershipId = null,
                    BeneficiaryTenantId = subscription.TenantId,
                    BeneficiaryBranchId = null,
                    SourceTenantId = subscription.TenantId,
                    MonetaryFeeLogId = null,
                    ReferenceId = null,
                    ReferenceType = MembershipPointsOperationReferenceTypes.NoRef,
                    ObjectiveType = objectiveType,
                    Type = MembershipPointsOperationTypes.PointsBalance,
                    Status = MembershipPointsOperationStatuses.Accessible,
                    AvailablePoints = welcomePoints,
                    Details = Resources.JoinWelcomePoints,
                    UsedPoints = 0,
                    Code = "",
                    ConvertedAmount = 0,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddMonths(MembershipConfigValues.DefaultPointsLifeSpanMonths),
                    IsActive = true,
                    Registered = true,
                };

                this._businessObjects.Context.Oltpmemberships.Add(subscription);
                this._businessObjects.Context.OltpmembershipPointsOperations.Add(newOperation);
                this._businessObjects.Context.SaveChanges();

                var query = from x in this._businessObjects.Context.OltpmembershipsView
                            where x.Id == subscription.Id
                            select x;


                foreach (OltpmembershipsView item in query)
                {

                    membership = new Membership
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        SoonToExpire = 0,
                        AvailableLoyaltyPoints = welcomePoints,
                        UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        OriginType = item.OriginType,
                        ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                        ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                        CustomerRanking = item.CustomerRanking,
                        IsNewUser = true,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        Username = item.Username,
                        DateOfBirth = item.DateOfBirth,
                        ClaimedRewards = item.ClaimedRewards,
                        ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                        ClaimedPromos = item.ClaimedPromos,
                        LastPromoClaimed = item.LastPromoClaimed,
                        LastPromoReserved = item.LastPromoReserved,
                        LastLevelEvaluation = item.LastLevelEvaluation,
                        Blocked = item.Blocked,
                        MembershipLevel = item.MembershipLevel,
                        MembershipLevelName = item.LevelName,
                        LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                        MaxGeneratedPoints = item.MaxGeneratedPoints,
                        MinGeneratedPoints = item.MinGeneratedPoints,
                        EvaluationMonths = item.EvaluationMonths,
                        PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                        CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                        MonetaryConversionFactor = item.MonetaryConversionFactor
                    };

                    membership.Gender = GetGenderName(item.Gender);

                }

            }
            catch (Exception e)
            {
                this._businessObjects.Context.Oltpmemberships.Remove(subscription);
                this._businessObjects.Context.SaveChanges();

                membership = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return membership;
        }//METHOD POST ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Creates a new subscription for a user to a tenant
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="originType"></param>
        /// <returns></returns>
        public Membership Post(long accountNumber, Guid tenantId, int originType, int welcomePoints, int objectiveType)
        {
            Membership membership = null;
            Oltpmemberships subscription = null;

            try
            {

                var queryUsers = from x in this._businessObjects.Context.AspNetUsers
                                 where x.AccountNumber == accountNumber
                                 select x;

                AspNetUsers currentUser = null;

                if (queryUsers != null)
                {
                    foreach (AspNetUsers item in queryUsers)
                    {
                        currentUser = item;
                    }
                }

                if (currentUser != null)
                {
                    subscription = new Oltpmemberships
                    {
                        Id = Guid.NewGuid(),
                        TenantId = tenantId,
                        UserId = currentUser.Id,
                        OriginType = originType,
                        IsActive = true,
                        Blocked = false,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        UsedLoyaltyPoints = 0,
                        CustomerRanking = 0,
                        LastPromoReserved = null,
                        LastPromoClaimed = null,
                        LastLevelEvaluation = DateTime.UtcNow,
                        ClaimedPromos = 0,
                        ClaimedRewards = 0,
                        ClaimedRewardsStartDate = null,
                        ReceiveEmailMarketing = true,
                        ReceiveSmsmarketing = true,
                        MembershipLevel = 1
                    };

                    OltpmembershipPointsOperations newOperation = new OltpmembershipPointsOperations
                    {
                        Id = Guid.NewGuid(),
                        ProviderMembershipId = subscription.Id,
                        BeneficiaryMembershipId = null,
                        BeneficiaryTenantId = subscription.TenantId,
                        BeneficiaryBranchId = null,
                        SourceTenantId = subscription.TenantId,
                        MonetaryFeeLogId = null,
                        ReferenceId = null,
                        ReferenceType = MembershipPointsOperationReferenceTypes.NoRef,
                        ObjectiveType = objectiveType,
                        Type = MembershipPointsOperationTypes.PointsBalance,
                        Status = MembershipPointsOperationStatuses.Accessible,
                        Details = Resources.JoinWelcomePoints,
                        AvailablePoints = welcomePoints,
                        UsedPoints = 0,
                        Code = "",
                        ConvertedAmount = 0,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        ExpirationDate = DateTime.UtcNow.AddMonths(MembershipConfigValues.DefaultPointsLifeSpanMonths),
                        IsActive = true,
                        Registered = true
                    };

                    this._businessObjects.Context.Oltpmemberships.Add(subscription);
                    this._businessObjects.Context.OltpmembershipPointsOperations.Add(newOperation);
                    this._businessObjects.Context.SaveChanges();

                    var query = from x in this._businessObjects.Context.OltpmembershipsView
                                where x.TenantId == tenantId && x.Id == subscription.Id
                                select x;


                    foreach (OltpmembershipsView item in query)
                    {

                        membership = new Membership
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            UserId = item.UserId,
                            AccountNumber = item.AccountNumber,
                            AvailableLoyaltyPoints = welcomePoints,
                            SoonToExpire = 0,
                            UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            IsActive = item.IsActive,
                            OriginType = item.OriginType,
                            ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                            ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                            CustomerRanking = item.CustomerRanking,
                            IsNewUser = true,
                            Name = item.Name,
                            PhoneNumber = item.PhoneNumber,
                            Username = item.Username,
                            DateOfBirth = item.DateOfBirth,
                            ClaimedRewards = item.ClaimedRewards,
                            ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                            ClaimedPromos = item.ClaimedPromos,
                            LastPromoClaimed = item.LastPromoClaimed,
                            LastPromoReserved = item.LastPromoReserved,
                            LastLevelEvaluation = item.LastLevelEvaluation,
                            Blocked = item.Blocked,
                            MembershipLevel = item.MembershipLevel,
                            MembershipLevelName = item.LevelName,
                            LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                            MaxGeneratedPoints = item.MaxGeneratedPoints,
                            MinGeneratedPoints = item.MinGeneratedPoints,
                            EvaluationMonths = item.EvaluationMonths,
                            PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                            CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                            MonetaryConversionFactor = item.MonetaryConversionFactor,
                            PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                            EnabledMoneyAmounts = item.EnabledMoneyAmounts
                        };

                        membership.Gender = GetGenderName(item.Gender);

                    }
                }

            }
            catch (Exception e)
            {
                this._businessObjects.Context.Oltpmemberships.Remove(subscription);
                this._businessObjects.Context.SaveChanges();

                membership = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return membership;
        }//METHOD POST ENDS ----------------------------------------------------------------------------------------------------------------------------- //




        /// <summary>
        /// Updates loyalty events date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventDate"></param>
        /// <param name="interactionType"></param>
        /// <returns></returns>
        public bool Put(string userId, DateTime eventDate, int interactionType)
        {
            //Membership membership = null;
            bool success = false;

            try
            {
                var query = (dynamic)null;

                query = from x in this._businessObjects.Context.Oltpmemberships
                        where x.TenantId == this._businessObjects.Tenant.TenantId && x.User.Id == userId //NOT SURE IF THIS WORKS
                        select x;


                Oltpmemberships subscription = null;

                foreach (Oltpmemberships item in query)
                {
                    subscription = item;
                }

                if (subscription == null)
                {
                    subscription = new Oltpmemberships
                    {
                        Id = Guid.NewGuid(),
                        TenantId = this._businessObjects.Tenant.TenantId,
                        UserId = userId,
                        OriginType = MembershipCreationReasonTypes.UserJoin,
                        IsActive = true,
                        Blocked = false,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        UsedLoyaltyPoints = 0,
                        CustomerRanking = 0,
                        LastPromoReserved = null,
                        LastPromoClaimed = null,
                        LastLevelEvaluation = DateTime.UtcNow,
                        ClaimedPromos = 0,
                        ClaimedRewards = 0,
                        ClaimedRewardsStartDate = null,
                        ReceiveEmailMarketing = true,
                        ReceiveSmsmarketing = true,
                        MembershipLevel = 1
                    };
                }

                if (subscription != null)
                {
                    switch (interactionType)
                    {
                        case MembershipActionTypes.OfferClaimed:
                            subscription.LastPromoClaimed = eventDate;
                            subscription.UpdatedDate = DateTime.UtcNow;
                            ++subscription.ClaimedPromos;
                            break;
                        case MembershipActionTypes.OfferRedeemed:
                            subscription.LastPromoReserved = eventDate;
                            subscription.UpdatedDate = DateTime.UtcNow;
                            break;
                        case MembershipActionTypes.RewardClaimed:
                            ++subscription.ClaimedRewards;
                            subscription.UpdatedDate = DateTime.UtcNow;
                            break;

                    }

                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }

            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        public Membership Put(string userId, bool smsMarketing, bool emailMarketing)
        {
            Membership membership = null;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpmemberships
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.UserId == userId
                            select x;

                Oltpmemberships subscription = null;

                foreach (var item in query)
                {
                    subscription = item;
                }

                if (subscription != null)
                {
                    subscription.ReceiveSmsmarketing = smsMarketing;
                    subscription.ReceiveEmailMarketing = emailMarketing;
                    subscription.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    var queryFullData = from x in this._businessObjects.Context.OltpmembershipsView
                                        where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == subscription.Id
                                        select x;


                    foreach (OltpmembershipsView item in queryFullData)
                    {

                        membership = new Membership
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            UserId = item.UserId,
                            AccountNumber = item.AccountNumber,
                            AvailableLoyaltyPoints = item.AvailablePoints ?? 0,
                            SoonToExpire = 0,
                            UsedLoyaltyPoints = item.UsedLoyaltyPoints,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            IsActive = item.IsActive,
                            OriginType = item.OriginType,
                            ReceiveEmailMarketing = item.ReceiveEmailMarketing,
                            ReceiveSMSMarketing = item.ReceiveSmsmarketing,
                            CustomerRanking = item.CustomerRanking,
                            IsNewUser = false,
                            Name = item.Name,
                            PhoneNumber = item.PhoneNumber,
                            Username = item.Username,
                            DateOfBirth = item.DateOfBirth,
                            ClaimedRewards = item.ClaimedRewards,
                            ClaimedRewardsStartDate = item.ClaimedRewardsStartDate,
                            ClaimedPromos = item.ClaimedPromos,
                            LastPromoClaimed = item.LastPromoClaimed,
                            LastPromoReserved = item.LastPromoReserved,
                            LastLevelEvaluation = item.LastLevelEvaluation,
                            Blocked = item.Blocked,
                            MembershipLevel = item.MembershipLevel,
                            MembershipLevelName = item.LevelName,
                            LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                            MaxGeneratedPoints = item.MaxGeneratedPoints,
                            MinGeneratedPoints = item.MinGeneratedPoints,
                            EvaluationMonths = item.EvaluationMonths,
                            PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                            CheckInPoints = item.CheckInPoints ?? MembershipConfigValues.DefaultCheckInPoints,
                            MonetaryConversionFactor = item.MonetaryConversionFactor,
                            PointsToMoneyEnabled = item.PointsToMoneyEnabled,
                            EnabledMoneyAmounts = item.EnabledMoneyAmounts
                        };

                        membership.Gender = GetGenderName(item.Gender);

                        if ((DateTime.UtcNow - subscription.CreatedDate).TotalDays < 7)
                        {
                            membership.IsNewUser = true;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                membership = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return membership;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Method used to changes membership active state
        /// if state is inactive user can't access to his account
        /// for the current tenant
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="smsMarketing"></param>
        /// <param name="emailMarketing"></param>
        /// <returns></returns>
        public bool Put(string userId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpmemberships
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.UserId == userId
                            select x;

                Oltpmemberships subscription = null;

                foreach (Oltpmemberships item in query)
                {
                    subscription = item;
                }

                //Active state can only be changed if not blocked
                if (subscription != null && !subscription.Blocked)
                {
                    subscription.IsActive = !subscription.IsActive;
                    subscription.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }

            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //



        /// <summary>
        /// Deletes a subscription
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Delete(string userId)
        {
            bool success = false;

            try
            {
                Oltpmemberships subscription = null;

                var query = from x in this._businessObjects.Context.Oltpmemberships
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.UserId == userId
                            select x;

                foreach (Oltpmemberships item in query)
                {
                    subscription = item;
                }

                if (subscription != null)
                {
                    this._businessObjects.Context.Oltpmemberships.Remove(subscription);
                    this._businessObjects.Context.SaveChanges();
                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }//METHODS DELETE ENDS -------------------------------------------------------------------------------------------------------------------------- //


        public bool Delete(Guid subscriptionId)
        {
            bool success = false;

            try
            {
                Oltpmemberships subscription = null;

                var query = from x in this._businessObjects.Context.Oltpmemberships
                            where x.Id == subscriptionId
                            select x;

                foreach (Oltpmemberships item in query)
                {
                    subscription = item;
                }

                if (subscription != null)
                {
                    //Set the subscription to inactive
                    subscription.IsActive = false;
                    subscription.UpdatedDate = DateTime.UtcNow;
                    //this._businessObjects.Context.OltpmembershipsView.DeleteOnSubmit(subscription);
                    this._businessObjects.Context.SaveChanges();
                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }//METHODS DELETE ENDS -------------------------------------------------------------------------------------------------------------------------- //


        #endregion

        #region MEMBERSHIPDATA

        private List<FlattenedMembershipData> GetsUserMembershipsDataByState(string userId, Guid stateId, Guid countryId)
        {
            List<FlattenedMembershipData> membershipLevels = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetUserMembershipsByState(stateId, countryId, userId)
                            where (x.IsBlockedMembership == null || !(bool)x.IsBlockedMembership) && (x.IsActiveMembership == null || (bool)x.IsActiveMembership)
                            select x;

                if (query != null)
                {
                    membershipLevels = new List<FlattenedMembershipData>();
                    FlattenedMembershipData membershipLevelData = null;

                    foreach (TempmembershipDetails item in query)
                    {

                        membershipLevelData = new FlattenedMembershipData
                        {
                            UserMembership = new MembershipData
                            {
                                Id = item.MembershipId,
                                TenantId = item.TenantId,
                                CurrentLevel = item.CurrentMembershipLevel,
                                CurrentLevelName = "",
                                UsedPoints = item.MembershipUsedPoints,
                                IsBlocked = item.IsBlockedMembership,
                                IsActive = item.IsActiveMembership,
                                CustomerRanking = item.CustomerRanking,
                                UserId = item.UserId,
                                AccountNumber = item.AccountNumber,
                                AccountCode = item.AccountCode,
                                EmailConfirmed = item.EmailConfirmed,
                                UserName = item.UserName,
                                UserGeneratedPoints = 0,
                                UserPurchasesCount = 0,
                                AvailablePoints = 0,
                                AvailablePurchasesCount = 0,
                                SoonToExpirePoints = 0,
                                LastLevelEvaluation = item.MembershipLevelLastEvaluation,
                                DaysForNextEvaluation = item.MembershipLevelLastEvaluation != null ? (int)Math.Ceiling((((DateTime)item.MembershipLevelLastEvaluation).AddMonths(item.LevelEvaluationMonts) - DateTime.UtcNow).TotalDays) : -1,
                                MinGeneratedPoints = 0,
                                MaxGeneratedPoints = 0,
                                MaxPurchasesCount = 0,
                                MinPurchasesCount = 0,
                                MaxRewardRedemptions = 0,
                                MonetaryConversionFactor = 0,
                                PointsToMoneyEnabled = item.LevelPointsToMoneyEnabled,
                                EnabledMoneyAmounts = "",
                                ClaimedRewards = item.ClaimedRewards,
                                ClaimedRewardsStartDate = item.ClaimedRewardsStartDate

                            },
                            TenantData = new DTO.Entities.Misc.TenantData.TenantMembershipData
                            {
                                TenantId = item.TenantId,
                                CategoryId = item.TenantCategoryId,
                                Name = item.TenantName,
                                AcceptsCommunityPointsAsPayment = item.AcceptsCommunityPointsAsPayment,
                                AcceptSelfPointsAsPayment = item.AcceptsSelfPointsAsPayment,
                                LoyaltyProgramType = item.LoyaltyProgramType,
                                CurrencySymbol = item.TenantCurrencySymbol,
                                HasMembershipLevels = item.TenantHasMembershipLevels,
                                LandingImgId = item.TenantLandingImg,
                                LogoId = item.TenantLogo
                            },
                            Branch = new DTO.Entities.Misc.Branch.MinBranchData
                            {
                                Id = item.BranchId,
                                Name = item.BranchName,
                                Address = item.BranchAddress,
                                PhoneNumber = item.BranchPhoneNumber
                            },
                            Level = new DTO.Entities.Misc.MembershipLevel.LevelData
                            {
                                Id = item.LevelId,
                                Name = item.LevelName,
                                Pos = item.LevelPos,
                                IconUrl = item.LevelIconUrl,
                                LoyaltyCashBackPercentage = item.LevelLoyaltyCashBackPercentage,
                                MonetaryConversionFactor = item.LevelMonetaryConversionFactor,
                                MinGeneratedPoints = item.LevelMinGeneratedPoints,
                                MaxGeneratedPoints = item.LevelMaxGeneratedPoints,
                                MinPurchasesCount = item.LevelMinPurchasesCount,
                                MaxPurchasesCount = item.LevelMaxPurchasesCount,
                                MaxRewardRedemptions = item.LevelMaxRewardRedemptions,
                                EvaluationMonths = item.LevelEvaluationMonts,
                                EnabledActions = item.LevelEnabledActions,
                                PointsLifeSpanMonths = item.LevelPointsLifeSpanMonths,
                                CheckInPoints = item.LevelCheckInPoints,
                                PointsToMoneyEnabled = item.LevelPointsToMoneyEnabled,
                                EnabledMonetaryAmounts = item.EnabledMoneyAmounts
                            },
                            IsMember = item.MembershipId != null,
                            UserLevel = item.CurrentMembershipLevel ?? 0
                        };

                        membershipLevels.Add(membershipLevelData);

                    }
                }

            }
            catch (Exception e)
            {
                membershipLevels = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return membershipLevels;
        }

        private List<FlattenedMembershipData> GetUserMembershipForTenant(string userId, Guid tenantId)
        {
            List<FlattenedMembershipData> membershipLevels = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetUserMembershipForTenant(tenantId, userId)
                            where (x.IsBlockedMembership == null || !(bool)x.IsBlockedMembership) && (x.IsActiveMembership == null || (bool)x.IsActiveMembership)
                            select x;

                if (query != null)
                {
                    membershipLevels = new List<FlattenedMembershipData>();
                    FlattenedMembershipData membershipLevelData = null;

                    foreach (TempmembershipDetails item in query)
                    {

                        membershipLevelData = new FlattenedMembershipData
                        {
                            UserMembership = new MembershipData
                            {
                                Id = item.MembershipId,
                                TenantId = item.TenantId,
                                CurrentLevel = item.CurrentMembershipLevel,
                                CurrentLevelName = "",
                                UsedPoints = item.MembershipUsedPoints,
                                IsBlocked = item.IsBlockedMembership,
                                IsActive = item.IsActiveMembership,
                                CustomerRanking = item.CustomerRanking,
                                UserId = item.UserId,
                                AccountNumber = item.AccountNumber,
                                AccountCode = item.AccountCode,
                                EmailConfirmed = item.EmailConfirmed,
                                UserName = item.UserName,
                                UserGeneratedPoints = 0,
                                UserPurchasesCount = 0,
                                AvailablePoints = 0,
                                AvailablePurchasesCount = 0,
                                SoonToExpirePoints = 0,
                                LastLevelEvaluation = item.MembershipLevelLastEvaluation,
                                DaysForNextEvaluation = item.MembershipLevelLastEvaluation != null ? (int)Math.Ceiling((((DateTime)item.MembershipLevelLastEvaluation).AddMonths(item.LevelEvaluationMonts) - DateTime.UtcNow).TotalDays) : -1,
                                MaxGeneratedPoints = 0,
                                MinGeneratedPoints = 0,
                                MaxPurchasesCount = 0,
                                MinPurchasesCount = 0,
                                MaxRewardRedemptions = 0,
                                MonetaryConversionFactor = 0,
                                PointsToMoneyEnabled = item.LevelPointsToMoneyEnabled,
                                EnabledMoneyAmounts = "",
                                ClaimedRewards = item.ClaimedRewards,
                                ClaimedRewardsStartDate = item.ClaimedRewardsStartDate
                            },
                            TenantData = new DTO.Entities.Misc.TenantData.TenantMembershipData
                            {
                                TenantId = item.TenantId,
                                CategoryId = item.TenantCategoryId,
                                Name = item.TenantName,
                                AcceptsCommunityPointsAsPayment = item.AcceptsCommunityPointsAsPayment,
                                AcceptSelfPointsAsPayment = item.AcceptsSelfPointsAsPayment,
                                LoyaltyProgramType = item.LoyaltyProgramType,
                                CurrencySymbol = item.TenantCurrencySymbol,
                                HasMembershipLevels = item.TenantHasMembershipLevels,
                                LandingImgId = item.TenantLandingImg,
                                LogoId = item.TenantLogo
                            },
                            Branch = new DTO.Entities.Misc.Branch.MinBranchData
                            {
                                Id = item.BranchId,
                                Name = item.BranchName,
                                Address = item.BranchAddress,
                                PhoneNumber = item.BranchPhoneNumber
                            },
                            Level = new DTO.Entities.Misc.MembershipLevel.LevelData
                            {
                                Id = item.LevelId,
                                Name = item.LevelName,
                                Pos = item.LevelPos,
                                IconUrl = item.LevelIconUrl,
                                LoyaltyCashBackPercentage = item.LevelLoyaltyCashBackPercentage,
                                MonetaryConversionFactor = item.LevelMonetaryConversionFactor,
                                MinGeneratedPoints = item.LevelMinGeneratedPoints,
                                MaxGeneratedPoints = item.LevelMaxGeneratedPoints,
                                MinPurchasesCount = item.LevelMinPurchasesCount,
                                MaxPurchasesCount = item.LevelMaxPurchasesCount,
                                MaxRewardRedemptions = item.LevelMaxRewardRedemptions,
                                EvaluationMonths = item.LevelEvaluationMonts,
                                EnabledActions = item.LevelEnabledActions,
                                PointsLifeSpanMonths = item.LevelPointsLifeSpanMonths,
                                CheckInPoints = item.LevelCheckInPoints,
                                PointsToMoneyEnabled = item.LevelPointsToMoneyEnabled,
                                EnabledMonetaryAmounts = item.EnabledMoneyAmounts
                            },
                            IsMember = item.MembershipId != null,
                            UserLevel = item.CurrentMembershipLevel ?? 0
                        };

                        membershipLevels.Add(membershipLevelData);

                    }
                }

            }
            catch (Exception e)
            {
                membershipLevels = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return membershipLevels;
        }


        public List<FullMembershipData> GetMembershipsForUserByState(Guid stateId, Guid countryId, string userId, DateTime dateTime, bool includeBranchList)
        {
            List<FullMembershipData> membershipsData = new List<FullMembershipData>();

            try
            {
                List<FlattenedMembershipData> flattenedMemberships = this.GetsUserMembershipsDataByState(userId, stateId, countryId);

                if (flattenedMemberships?.Count > 0)
                {
                    FullMembershipData currentMembership;
                    IEnumerable<IGrouping<Guid, FlattenedMembershipData>> groupedByTenantId = flattenedMemberships.GroupBy(x => x.TenantData.TenantId);
                    int userLevelPos = 0;

                    FlattenedMembershipData[] membershipsGroup = null;
                    IEnumerable<IGrouping<Guid, FlattenedMembershipData>> groupedByLevelId = null;

                    foreach (IGrouping<Guid, FlattenedMembershipData> membershipDataGroup in groupedByTenantId)
                    {
                        membershipsGroup = membershipDataGroup.ToArray();

                        userLevelPos = membershipsGroup[0].UserMembership.CurrentLevel ?? 0;

                        currentMembership = new FullMembershipData
                        {
                            TenantData = membershipsGroup[0].TenantData,
                            UserMembership = membershipsGroup[0].UserMembership,
                            PointsOps = new List<MembershipPointsOpSummary>(),
                            Levels = new List<DTO.Entities.Misc.MembershipLevel.LevelData>(),
                            Branches = new List<DTO.Entities.Misc.Branch.MinBranchData>(),
                            IsMember = membershipsGroup[0].IsMember
                        };

                        groupedByLevelId = membershipsGroup.GroupBy(x => x.Level.Id);

                        foreach (IGrouping<Guid, FlattenedMembershipData> levelDataGroup in groupedByLevelId)
                        {
                            currentMembership.Levels.Add(levelDataGroup.ElementAt(0).Level);

                            if (levelDataGroup.ElementAt(0).Level.Pos == userLevelPos)
                            {
                                currentMembership.UserMembership.CurrentLevel = levelDataGroup.ElementAt(0).Level.Pos;
                                currentMembership.UserMembership.CurrentLevelName = levelDataGroup.ElementAt(0).Level.Name;
                                currentMembership.UserMembership.MinGeneratedPoints = levelDataGroup.ElementAt(0).Level.MinGeneratedPoints;
                                currentMembership.UserMembership.MaxGeneratedPoints = levelDataGroup.ElementAt(0).Level.MaxGeneratedPoints;
                                currentMembership.UserMembership.MinPurchasesCount = levelDataGroup.ElementAt(0).Level.MinPurchasesCount;
                                currentMembership.UserMembership.MaxPurchasesCount = levelDataGroup.ElementAt(0).Level.MaxPurchasesCount;
                                currentMembership.UserMembership.MaxRewardRedemptions = levelDataGroup.ElementAt(0).Level.MaxRewardRedemptions;
                                currentMembership.UserMembership.MonetaryConversionFactor = levelDataGroup.ElementAt(0).Level.MonetaryConversionFactor;
                                currentMembership.UserMembership.PointsToMoneyEnabled = levelDataGroup.ElementAt(0).Level.PointsToMoneyEnabled;
                                currentMembership.UserMembership.EnabledMoneyAmounts = levelDataGroup.ElementAt(0).Level.EnabledMonetaryAmounts;
                            }
                        }

                        currentMembership.Levels = currentMembership.Levels.OrderBy(x => x.Pos).ToList();

                        //The branches aren't required for now
                        /*for (int i = 0; i < offersGroup.Length; ++i)
                        {
                            currentReward.Branches.Add(offersGroup[i].Branch);
                        }*/

                        membershipsData.Add(currentMembership);
                    }

                    //Now will get the points and actions count per each membership
                    List<MembershipPointsOp> membershipPointsOps = this.GetsUserMembershipsPointsByState(userId, stateId, countryId, MembershipPointsOperationStatuses.All);

                    List<ValidPurchaseRegistry> purchaseRegistries = this.GetsUserValidPurchaseRegistriesByState(userId, stateId, countryId, MembershipPointsOperationStatuses.All);

                    if (membershipPointsOps?.Count > 0)
                    {
                        //Variables for points operations
                        IEnumerable<IGrouping<Guid, MembershipPointsOp>> opsGroupedByMembershipId = membershipPointsOps.GroupBy(x => x.MembershipId);
                        IEnumerable<IGrouping<Guid, MembershipPointsOp>> opsGroupedById = null;
                        MembershipPointsOp[] opsGroupById = null;
                        MembershipPointsOpSummary opSummary = null;

                        //Variables for purchases count
                        IEnumerable<IGrouping<Guid, ValidPurchaseRegistry>> registriesGroupedByMembershipId = purchaseRegistries.GroupBy(x => x.MembershipId);
                        IEnumerable<IGrouping<Guid, ValidPurchaseRegistry>> registriesGroupedById = null;
                        ValidPurchaseRegistry[] registriesGroupById = null;

                        foreach (FullMembershipData fullMembership in membershipsData)
                        {
                            //If user has a membership for the corresponding tenant  
                            if (fullMembership.IsMember && fullMembership.UserMembership.Id != null)
                            {
                                //Logic for points operations
                                foreach (IGrouping<Guid, MembershipPointsOp> opsDataGroupByMembershipId in opsGroupedByMembershipId)
                                {
                                    //Groups by operation id to avoid to count one operation more then once
                                    opsGroupedById = opsDataGroupByMembershipId.GroupBy(x => x.Id);

                                    foreach (IGrouping<Guid, MembershipPointsOp> opsDataGroupById in opsGroupedById)
                                    {
                                        opsGroupById = opsDataGroupById.ToArray();

                                        //If the current group corresponds to the membership
                                        if (opsGroupById[0].MembershipId == fullMembership.UserMembership.Id && opsGroupById[0].BeneficiaryTenantId == fullMembership.UserMembership.TenantId)
                                        {
                                            //If the operation is active, it's related to points balance and doesn't have a reference(happens when invites a friend) or refereces a transaction(happens with check-in and offers claim)
                                            if (opsGroupById[0].IsActive && opsGroupById[0].Type == MembershipPointsOperationTypes.PointsBalance)
                                            {
                                                if (opsGroupById[0].AvailablePoints > 0 && opsGroupById[0].ExpirationDate > dateTime)
                                                {
                                                    opSummary = new MembershipPointsOpSummary
                                                    {
                                                        Id = opsGroupById[0].Id,
                                                        SourceTenantId = opsGroupById[0].SourceTenantId,
                                                        Details = opsGroupById[0].Details ?? Resources.NoDetails,
                                                        PointsAmount = opsGroupById[0].AvailablePoints,
                                                        Status = opsGroupById[0].Status,
                                                        SoonToExpire = false,
                                                        CreatedDate = opsGroupById[0].CreatedDate,
                                                        ExpirationDate = opsGroupById[0].ExpirationDate
                                                    };

                                                    if ((opsGroupById[0].ExpirationDate - dateTime).TotalDays <= MembershipConfigValues.SoonToExpireDaysLeftIndicator)
                                                    {
                                                        opSummary.SoonToExpire = true;
                                                    }

                                                    fullMembership.PointsOps.Add(opSummary);
                                                }

                                                //If the current operation was registered after the last evaluation
                                                if (opsGroupById[0].CreatedDate > fullMembership.UserMembership.LastLevelEvaluation &&
                                                (opsGroupById[0].ReferenceType < MembershipPointsOperationReferenceTypes.MembershipOperation) && (opsGroupById[0].Status == MembershipPointsOperationStatuses.Accessible || opsGroupById[0].Status == MembershipPointsOperationStatuses.FundingPending))
                                                {
                                                    fullMembership.UserMembership.UserGeneratedPoints += (int)(opsGroupById[0].AvailablePoints + opsGroupById[0].UsedPoints);
                                                }

                                                //If the operation has points and hasn't expired
                                                if (opsGroupById[0].AvailablePoints > 0 && opsGroupById[0].ExpirationDate > dateTime && opsGroupById[0].Status == MembershipPointsOperationStatuses.Accessible)
                                                {
                                                    fullMembership.UserMembership.AvailablePoints += opsGroupById[0].AvailablePoints;

                                                    if ((opsGroupById[0].ExpirationDate - dateTime).TotalDays <= MembershipConfigValues.SoonToExpireDaysLeftIndicator)
                                                    {
                                                        fullMembership.UserMembership.SoonToExpirePoints += opsGroupById[0].AvailablePoints;
                                                    }
                                                }
                                            }

                                        }
                                    }

                                }

                                //Logic for purchases registries
                                foreach (IGrouping<Guid, ValidPurchaseRegistry> registriesDataGroupByMembershipId in registriesGroupedByMembershipId)
                                {
                                    //Groups by registry id to avoid to count one operation more then once
                                    registriesGroupedById = registriesDataGroupByMembershipId.GroupBy(x => x.Id);

                                    foreach (IGrouping<Guid, ValidPurchaseRegistry> registriesDataGroupById in registriesGroupedById)
                                    {
                                        registriesGroupById = registriesDataGroupById.ToArray();

                                        //If the current group corresponds to the membership
                                        if (registriesGroupById[0].MembershipId == fullMembership.UserMembership.Id && registriesGroupById[0].TenantId == fullMembership.UserMembership.TenantId)
                                        {

                                            //If the current registry was registered after the last evaluation
                                            if (registriesGroupById[0].CreatedDate > fullMembership.UserMembership.LastLevelEvaluation)
                                            {
                                                ++fullMembership.UserMembership.UserPurchasesCount;
                                            }

                                            //If the registry hasn't expired
                                            if (registriesGroupById[0].ExpirationDate > dateTime)
                                            {
                                                ++fullMembership.UserMembership.AvailablePurchasesCount;
                                            }

                                        }

                                    }
                                }

                            }
                        }

                        membershipsData = membershipsData.OrderByDescending(x => x.UserMembership.UserGeneratedPoints).ToList();

                    }



                }

            }
            catch (Exception e)
            {
                membershipsData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return membershipsData;

        }


        public List<FullMembershipData> GetUserMembershipForTenant(Guid tenantId, string userId, DateTime dateTime, bool includeBranchList)
        {
            List<FullMembershipData> membershipsData = new List<FullMembershipData>();

            try
            {
                List<FlattenedMembershipData> flattenedMemberships = this.GetUserMembershipForTenant(userId, tenantId);

                if (flattenedMemberships?.Count > 0)
                {
                    FullMembershipData currentMembership;
                    IEnumerable<IGrouping<Guid, FlattenedMembershipData>> groupedByTenantId = flattenedMemberships.GroupBy(x => x.TenantData.TenantId);
                    int userLevelPos = 0;

                    FlattenedMembershipData[] membershipsGroup = null;
                    IEnumerable<IGrouping<Guid, FlattenedMembershipData>> groupedByLevelId = null;

                    foreach (IGrouping<Guid, FlattenedMembershipData> membershipDataGroup in groupedByTenantId)
                    {
                        membershipsGroup = membershipDataGroup.ToArray();

                        userLevelPos = membershipsGroup[0].UserMembership.CurrentLevel ?? 0;

                        currentMembership = new FullMembershipData
                        {
                            TenantData = membershipsGroup[0].TenantData,
                            UserMembership = membershipsGroup[0].UserMembership,
                            Levels = new List<DTO.Entities.Misc.MembershipLevel.LevelData>(),
                            Branches = new List<DTO.Entities.Misc.Branch.MinBranchData>(),
                            PointsOps = new List<MembershipPointsOpSummary>(),
                            IsMember = membershipsGroup[0].IsMember
                        };

                        groupedByLevelId = membershipsGroup.GroupBy(x => x.Level.Id);

                        foreach (IGrouping<Guid, FlattenedMembershipData> levelDataGroup in groupedByLevelId)
                        {
                            currentMembership.Levels.Add(levelDataGroup.ElementAt(0).Level);

                            if (levelDataGroup.ElementAt(0).Level.Pos == userLevelPos)
                            {
                                currentMembership.UserMembership.CurrentLevelName = levelDataGroup.ElementAt(0).Level.Name;
                                currentMembership.UserMembership.MinGeneratedPoints = levelDataGroup.ElementAt(0).Level.MinGeneratedPoints;
                                currentMembership.UserMembership.MaxGeneratedPoints = levelDataGroup.ElementAt(0).Level.MaxGeneratedPoints;
                                currentMembership.UserMembership.MinPurchasesCount = levelDataGroup.ElementAt(0).Level.MinPurchasesCount;
                                currentMembership.UserMembership.MaxPurchasesCount = levelDataGroup.ElementAt(0).Level.MaxPurchasesCount;
                                currentMembership.UserMembership.MaxRewardRedemptions = levelDataGroup.ElementAt(0).Level.MaxRewardRedemptions;
                                currentMembership.UserMembership.MonetaryConversionFactor = levelDataGroup.ElementAt(0).Level.MonetaryConversionFactor;
                                currentMembership.UserMembership.PointsToMoneyEnabled = levelDataGroup.ElementAt(0).Level.PointsToMoneyEnabled;
                                currentMembership.UserMembership.EnabledMoneyAmounts = levelDataGroup.ElementAt(0).Level.EnabledMonetaryAmounts;
                            }
                        }

                        currentMembership.Levels = currentMembership.Levels.OrderBy(x => x.Pos).ToList();

                        //The branches aren't required for now
                        /*for (int i = 0; i < offersGroup.Length; ++i)
                        {
                            currentReward.Branches.Add(offersGroup[i].Branch);
                        }*/

                        membershipsData.Add(currentMembership);
                    }

                    DateTime lastLevelEvaluationDate = membershipsData?.Count > 0 ? (DateTime)membershipsData[0].UserMembership.LastLevelEvaluation : DateTime.UtcNow;

                    //Now will get the points and actions count per each membership
                    List<MembershipPointsOp> membershipPointsOps = this.GetsUserMembershipsPointsForTenant(userId, tenantId, lastLevelEvaluationDate, dateTime, MembershipPointsOperationStatuses.All); ;

                    List<ValidPurchaseRegistry> purchaseRegistries = this.GetsUserValidPurchaseRegistriesForTenant(userId, tenantId, lastLevelEvaluationDate, dateTime);

                    if (membershipPointsOps?.Count > 0)
                    {
                        IEnumerable<IGrouping<Guid, MembershipPointsOp>> opsGroupedByMembershipId = membershipPointsOps.GroupBy(x => x.MembershipId);
                        IEnumerable<IGrouping<Guid, MembershipPointsOp>> opsGroupedById = null;
                        MembershipPointsOp[] opsGroupById = null;
                        MembershipPointsOpSummary opSummary = null;

                        //Variables for purchases count
                        IEnumerable<IGrouping<Guid, ValidPurchaseRegistry>> registriesGroupedByMembershipId = purchaseRegistries.GroupBy(x => x.MembershipId);
                        IEnumerable<IGrouping<Guid, ValidPurchaseRegistry>> registriesGroupedById = null;
                        ValidPurchaseRegistry[] registriesGroupById = null;

                        foreach (FullMembershipData fullMembership in membershipsData)
                        {
                            //If user has a membership for the corresponding tenant  
                            if (fullMembership.IsMember && fullMembership.UserMembership.Id != null)
                            {

                                //Logic for points operations
                                foreach (IGrouping<Guid, MembershipPointsOp> opsDataGroupByMembershipId in opsGroupedByMembershipId)
                                {
                                    //Groups by operation id to avoid to count one operation more then once
                                    opsGroupedById = opsDataGroupByMembershipId.GroupBy(x => x.Id);

                                    foreach (IGrouping<Guid, MembershipPointsOp> opsDataGroupById in opsGroupedById)
                                    {
                                        opsGroupById = opsDataGroupById.ToArray();

                                        //If the current group correspondes to the membership
                                        if (opsGroupById[0].MembershipId == fullMembership.UserMembership.Id && opsGroupById[0].BeneficiaryTenantId == fullMembership.UserMembership.TenantId)
                                        {
                                            //If the operation is active, it's related to points balance and doesn't have a reference(happens when invites a friend) or refereces a transaction(happens with check-in and offers claim)
                                            if (opsGroupById[0].IsActive && opsGroupById[0].Type == MembershipPointsOperationTypes.PointsBalance)
                                            {
                                                if (opsGroupById[0].AvailablePoints > 0 && opsGroupById[0].ExpirationDate > dateTime)
                                                {
                                                    opSummary = new MembershipPointsOpSummary
                                                    {
                                                        Id = opsGroupById[0].Id,
                                                        SourceTenantId = opsGroupById[0].SourceTenantId,
                                                        Details = opsGroupById[0].Details ?? Resources.NoDetails,
                                                        PointsAmount = opsGroupById[0].AvailablePoints,
                                                        Status = opsGroupById[0].Status,
                                                        SoonToExpire = false,
                                                        CreatedDate = opsGroupById[0].CreatedDate,
                                                        ExpirationDate = opsGroupById[0].ExpirationDate
                                                    };

                                                    if ((opsGroupById[0].ExpirationDate - dateTime).TotalDays <= MembershipConfigValues.SoonToExpireDaysLeftIndicator)
                                                    {
                                                        opSummary.SoonToExpire = true;
                                                    }

                                                    fullMembership.PointsOps.Add(opSummary);
                                                }


                                                //If the current operation was registered after the last evaluation
                                                if (opsGroupById[0].CreatedDate >= fullMembership.UserMembership.LastLevelEvaluation &&
                                                (opsGroupById[0].ReferenceType < MembershipPointsOperationReferenceTypes.MembershipOperation) && (opsGroupById[0].Status == MembershipPointsOperationStatuses.Accessible || opsGroupById[0].Status == MembershipPointsOperationStatuses.FundingPending))
                                                {
                                                    fullMembership.UserMembership.UserGeneratedPoints += (int)(opsGroupById[0].AvailablePoints + opsGroupById[0].UsedPoints);
                                                }

                                                //If the operation has points and hasn't expired
                                                if (opsGroupById[0].AvailablePoints > 0 && opsGroupById[0].ExpirationDate > dateTime && opsGroupById[0].Status == MembershipPointsOperationStatuses.Accessible)
                                                {
                                                    fullMembership.UserMembership.AvailablePoints += opsGroupById[0].AvailablePoints;

                                                    if ((opsGroupById[0].ExpirationDate - dateTime).TotalDays <= MembershipConfigValues.SoonToExpireDaysLeftIndicator)
                                                    {
                                                        fullMembership.UserMembership.SoonToExpirePoints += opsGroupById[0].AvailablePoints;
                                                    }
                                                }
                                            }

                                        }
                                    }

                                }

                                //Logic for purchases registries
                                foreach (IGrouping<Guid, ValidPurchaseRegistry> registriesDataGroupByMembershipId in registriesGroupedByMembershipId)
                                {
                                    //Groups by registry id to avoid to count one operation more then once
                                    registriesGroupedById = registriesDataGroupByMembershipId.GroupBy(x => x.Id);

                                    foreach (IGrouping<Guid, ValidPurchaseRegistry> registriesDataGroupById in registriesGroupedById)
                                    {
                                        registriesGroupById = registriesDataGroupById.ToArray();

                                        //If the current group corresponds to the membership
                                        if (registriesGroupById[0].MembershipId == fullMembership.UserMembership.Id && registriesGroupById[0].TenantId == fullMembership.UserMembership.TenantId)
                                        {

                                            //If the current registry was registered after the last evaluation
                                            if (registriesGroupById[0].CreatedDate > fullMembership.UserMembership.LastLevelEvaluation)
                                            {
                                                ++fullMembership.UserMembership.UserPurchasesCount;
                                            }

                                            //If the registry hasn't expired
                                            if (registriesGroupById[0].ExpirationDate > dateTime)
                                            {
                                                ++fullMembership.UserMembership.AvailablePurchasesCount;
                                            }

                                        }

                                    }
                                }

                            }
                        }

                        membershipsData = membershipsData.OrderByDescending(x => x.UserMembership.UserGeneratedPoints).ToList();

                    }

                }

            }
            catch (Exception e)
            {
                membershipsData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return membershipsData;

        }

        #endregion

        #region MISCELLANEOUS

        public DateTime Get(string userId, Guid? tenantId, int valueToRetrieve)
        {
            DateTime interestValue = DateTime.MinValue;

            try
            {
                Oltpmemberships membership = (dynamic)null;


                switch (valueToRetrieve)
                {
                    case MembershipInterestDates.LastOfferReserved:
                        if (tenantId != null)
                        {
                            membership = (from x in this._businessObjects.Context.Oltpmemberships
                                          where x.UserId == userId && x.TenantId == (Guid)tenantId
                                          orderby x.LastPromoReserved descending
                                          select x).First();
                        }
                        else
                        {
                            membership = (from x in this._businessObjects.Context.Oltpmemberships
                                          where x.UserId == userId
                                          orderby x.LastPromoReserved descending
                                          select x).First();
                        }
                        break;
                    case MembershipInterestDates.LastOfferClaimed:
                        if (tenantId != null)
                        {
                            membership = (from x in this._businessObjects.Context.Oltpmemberships
                                          where x.UserId == userId && x.TenantId == (Guid)tenantId
                                          orderby x.LastPromoClaimed descending
                                          select x).First();
                        }
                        else
                        {
                            membership = (from x in this._businessObjects.Context.Oltpmemberships
                                          where x.UserId == userId
                                          orderby x.LastPromoClaimed descending
                                          select x).First();
                        }
                        break;
                    case MembershipInterestDates.LastLevelEvaluation:
                        if (tenantId != null)
                        {
                            membership = (from x in this._businessObjects.Context.Oltpmemberships
                                          where x.UserId == userId && x.TenantId == (Guid)tenantId
                                          orderby x.LastLevelEvaluation descending
                                          select x).First();
                        }
                        else
                        {
                            membership = (from x in this._businessObjects.Context.Oltpmemberships
                                          where x.UserId == userId
                                          orderby x.LastLevelEvaluation descending
                                          select x).First();
                        }
                        break;
                }

                if (membership != null)
                {
                    switch (valueToRetrieve)
                    {
                        case MembershipInterestDates.LastOfferReserved:
                            interestValue = membership.LastPromoReserved ?? DateTime.MinValue;
                            break;
                        case MembershipInterestDates.LastOfferClaimed:
                            interestValue = membership.LastPromoClaimed ?? DateTime.MinValue;
                            break;
                        case MembershipInterestDates.LastLevelEvaluation:
                            interestValue = membership.LastLevelEvaluation;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                interestValue = DateTime.MinValue;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return interestValue;
        }

        #endregion


        #region MEMBERSHIPOPSDATA

        private List<MembershipPointsOp> GetsUserMembershipsPointsByState(string userId, Guid stateId, Guid countryId, int status)
        {
            List<MembershipPointsOp> opsData = null;

            try
            {
                var query = (dynamic)null;

                if (status != MembershipPointsOperationStatuses.All)
                {
                    query = from x in this._businessObjects.FuncsHandler.GetUserMembershipPointsByState(stateId, countryId, userId)
                            where x.IsActive && x.Status == status
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.FuncsHandler.GetUserMembershipPointsByState(stateId, countryId, userId)
                            where x.IsActive
                            select x;
                }


                if (query != null)
                {
                    opsData = new List<MembershipPointsOp>();
                    MembershipPointsOp op = null;

                    foreach (TempmembershipPointOps item in query)
                    {

                        op = new MembershipPointsOp
                        {
                            Id = item.Id,
                            MembershipId = item.ProviderMembershipId,
                            BeneficiaryTenantId = item.BeneficiaryTenantId,
                            SourceTenantId = item.SourceTenantId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            Type = item.Type,
                            Status = item.Status,
                            StatusName = this.GetOperationStatusName(item.Status),
                            AvailablePoints = (long)item.AvailablePoints,
                            UsedPoints = (long)item.UsedPoints,
                            IsActive = item.IsActive,
                            Registered = item.Registered,
                            Details = item.Details,
                            CreatedDate = item.CreatedDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        opsData.Add(op);

                    }
                }

            }
            catch (Exception e)
            {
                opsData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return opsData;
        }

        private List<MembershipPointsOp> GetsUserMembershipsPointsForTenant(string userId, Guid tenantId, DateTime lastLevelEvaluationDate, DateTime dateTime, int status)
        {
            List<MembershipPointsOp> opsData = null;

            try
            {
                var query = (dynamic)null;

                if (status != MembershipPointsOperationStatuses.All)
                {
                    query = from x in this._businessObjects.FuncsHandler.GetUserMembershipPointsForTenant(tenantId, userId, lastLevelEvaluationDate, dateTime)
                            where x.IsActive && x.Status == status
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.FuncsHandler.GetUserMembershipPointsForTenant(tenantId, userId, lastLevelEvaluationDate, dateTime)
                            where x.IsActive
                            select x;
                }


                if (query != null)
                {
                    opsData = new List<MembershipPointsOp>();
                    MembershipPointsOp op = null;

                    foreach (TempmembershipPointOps item in query)
                    {

                        op = new MembershipPointsOp
                        {
                            Id = item.Id,
                            MembershipId = item.ProviderMembershipId,
                            BeneficiaryTenantId = item.BeneficiaryTenantId,
                            SourceTenantId = item.SourceTenantId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            Type = item.Type,
                            Status = item.Status,
                            StatusName = this.GetOperationStatusName(item.Status),
                            AvailablePoints = (long)item.AvailablePoints,
                            UsedPoints = (long)item.UsedPoints,
                            IsActive = item.IsActive,
                            Registered = item.Registered,
                            Details = item.Details,
                            CreatedDate = item.CreatedDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        opsData.Add(op);

                    }
                }

            }
            catch (Exception e)
            {
                opsData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return opsData;
        }

        #endregion

        #region MEMBERSHIPOPERATIONS

        public string GetOperationTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case MembershipPointsOperationTypes.PointsBalance:
                    typeName = Resources.PointsBalance;
                    break;
                case MembershipPointsOperationTypes.TransferPoints:
                    typeName = Resources.TransferPoints;
                    break;
                case MembershipPointsOperationTypes.ConvertPoints:
                    typeName = Resources.ConvertPoints;
                    break;
            }

            return typeName;
        }

        public string GetOperationStatusName(int status)
        {
            string statusName = "";

            switch (status)
            {
                case MembershipPointsOperationStatuses.Accessible:
                    statusName = Resources.AccessiblePoints;
                    break;
                case MembershipPointsOperationStatuses.FundingPending:
                    statusName = Resources.FundingPending;
                    break;
                case MembershipPointsOperationStatuses.ValidationPending:
                    statusName = Resources.ValidationPending;
                    break;
                case MembershipPointsOperationStatuses.InvalidPoints:
                    statusName = Resources.InvalidPoints;
                    break;
                case MembershipPointsOperationStatuses.PointsNotFunded:
                    statusName = Resources.PointsNotFunded;
                    break;
            }

            return statusName;
        }

        public string GetOperationObjectiveTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case MembershipPointsOperationObjectiveTypes.CommerceLoyalty:
                    typeName = Resources.CommerceLoyalty;
                    break;
                case MembershipPointsOperationObjectiveTypes.YOYWallet:
                    typeName = Resources.YOYWallet;
                    break;
            }

            return typeName;
        }

        /// <summary>
        /// Returns a points count based in the operation type
        /// </summary>
        /// <param name="membershipId"></param>
        /// <param name="tenantId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public decimal Get(Guid membershipId, Guid tenantId, int type, int status, DateTime dateTime)
        {
            decimal pointsCount = 0;

            try
            {
                //Needs to retrieve the points balance
                var query = (dynamic)null;

                switch (type)
                {
                    case MembershipPointsOperationTypes.PointsBalance:

                        if (status != MembershipPointsOperationStatuses.All)
                        {
                            query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                    where x.BeneficiaryTenantId == tenantId && x.ProviderMembershipId == membershipId && x.Type == type && x.Status == status && x.AvailablePoints > 0 && x.ExpirationDate > dateTime
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                    where x.BeneficiaryTenantId == tenantId && x.ProviderMembershipId == membershipId && x.Type == type && x.AvailablePoints > 0 && x.ExpirationDate > dateTime
                                    select x;
                        }

                        if (query != null)
                        {
                            pointsCount = 0;
                            foreach (OltpmembershipPointsOperations item in query)
                            {
                                pointsCount += item.AvailablePoints;
                            }
                        }

                        break;
                    case MembershipPointsOperationTypes.TransferPoints:

                        if (status != MembershipPointsOperationStatuses.All)
                        {
                            query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                    where x.BeneficiaryTenantId == tenantId && x.ProviderMembershipId == membershipId && x.Type == type && x.Status == status && x.UsedPoints > 0 && x.CreatedDate > dateTime
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                    where x.BeneficiaryTenantId == tenantId && x.ProviderMembershipId == membershipId && x.Type == type && x.UsedPoints > 0 && x.CreatedDate > dateTime
                                    select x;
                        }

                        if (query != null)
                        {
                            pointsCount = 0;
                            foreach (OltpmembershipPointsOperations item in query)
                            {
                                pointsCount += item.UsedPoints;
                            }
                        }

                        break;
                    case MembershipPointsOperationTypes.ConvertPoints:

                        if (status != MembershipPointsOperationStatuses.All)
                        {
                            query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                    where x.BeneficiaryTenantId == tenantId && x.ProviderMembershipId == membershipId && x.Type == type && x.Status == status && x.ConvertedAmount > 0 && x.CreatedDate > dateTime
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                    where x.BeneficiaryTenantId == tenantId && x.ProviderMembershipId == membershipId && x.Type == type && x.ConvertedAmount > 0 && x.CreatedDate > dateTime
                                    select x;
                        }


                        if (query != null)
                        {
                            pointsCount = 0;
                            foreach (OltpmembershipPointsOperations item in query)
                            {
                                pointsCount += (decimal)item.ConvertedAmount;
                            }
                        }

                        break;
                }

            }
            catch (Exception e)
            {
                pointsCount = -1;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return pointsCount;
        }

        public List<MembershipPointsOperation> Gets(Guid providerMembershipId, Guid beneficiaryMembershipId, int status, DateTime startDate, DateTime endDate, int pageSize, int pageNumber)
        {
            List<MembershipPointsOperation> operations = null;

            try
            {
                var query = (dynamic)null;

                if (status != MembershipPointsOperationStatuses.All)
                {
                    query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                             where x.Type == MembershipPointsOperationTypes.TransferPoints && x.ProviderMembershipId == providerMembershipId && x.BeneficiaryMembershipId == beneficiaryMembershipId && x.Status == status && x.CreatedDate >= startDate & x.CreatedDate < endDate
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }
                else
                {
                    query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                             where x.Type == MembershipPointsOperationTypes.TransferPoints && x.ProviderMembershipId == providerMembershipId && x.BeneficiaryMembershipId == beneficiaryMembershipId && x.CreatedDate >= startDate & x.CreatedDate < endDate
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }



                if (query != null)
                {
                    MembershipPointsOperation operation = null;
                    operations = new List<MembershipPointsOperation>();

                    foreach (OltpmembershipPointsOperationsView item in query)
                    {
                        operation = new MembershipPointsOperation
                        {
                            Id = item.Id,
                            ProviderMembershipId = item.ProviderMembershipId,
                            ProviderUserName = item.ProviderUserName,
                            ProviderUserEmail = item.ProviderUserEmail,
                            BeneficiaryMembershipId = item.BeneficiaryMembershipId,
                            BeneficiaryUserName = item.BeneficiaryUserName,
                            BeneficiaryUserEmail = item.BeneficiaryUserEmail,
                            BeneficiaryTenantId = item.BeneficiaryTenantId,
                            BeneficiaryBranchId = item.BeneficiaryBranchId,
                            SourceTenantId = item.SourceTenantId,
                            MonetaryFeeLogId = item.MonetaryFeeLogId,
                            MonetaryFeeLogReason = item.MonetaryFeeLogReason,
                            MoneraryFeeLogStatus = item.MonetaryFeeLogStatus,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            Type = item.Type,
                            TypeName = this.GetOperationTypeName(item.Type),
                            ObjectiveType = item.ObjectiveType,
                            ObjectiveTypeName = this.GetOperationObjectiveTypeName(item.ObjectiveType),
                            Status = item.Status,
                            StatusName = this.GetOperationStatusName(item.Status),
                            AvailablePoints = item.AvailablePoints,
                            UsedPoints = item.UsedPoints,
                            Code = item.Code,
                            ConvertedAmount = item.ConvertedAmount,
                            IsActive = item.IsActive,
                            Registered = item.Registered,
                            Details = item.Details,
                            CreatedDate = item.CreatedDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        operations.Add(operation);
                    }
                }
            }
            catch (Exception e)
            {
                operations = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return operations;
        }

        public List<MembershipPointsOperation> Gets(Guid refId, int membershipRefIdType, int operationType, int activeState, int expiredState, int status, DateTime date, int pageSize, int pageNumber)
        {
            List<MembershipPointsOperation> operations = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (membershipRefIdType)
                        {
                            case MembershipPointsOperationRefIdTypes.All:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.ExpirationDate >= date && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.ProviderMembership:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.ProviderMembershipId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.BeneficiaryMembership:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:

                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryMembershipId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.BeneficiaryTenant:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.BeneficiaryTenantId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.SourceTenant:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.SourceTenantId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                        }
                        break;
                    case ActiveStates.Active:
                        switch (membershipRefIdType)
                        {
                            case MembershipPointsOperationRefIdTypes.All:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId)
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.ProviderMembership:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.BeneficiaryMembership:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.BeneficiaryTenant:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.SourceTenant:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                        }
                        break;
                    case ActiveStates.Inactive:
                        switch (membershipRefIdType)
                        {
                            case MembershipPointsOperationRefIdTypes.All:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId)
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && (x.ProviderMembershipId == refId || x.BeneficiaryMembershipId == refId || x.BeneficiaryTenantId == refId || x.SourceTenantId == refId) && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.ProviderMembership:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.ProviderMembershipId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.BeneficiaryMembership:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId & x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryMembershipId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.BeneficiaryTenant:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.BeneficiaryTenantId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;

                                }

                                break;
                            case MembershipPointsOperationRefIdTypes.SourceTenant:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.Status == status
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.Type == operationType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Valid:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.ExpirationDate >= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (operationType == MembershipPointsOperationTypes.All)
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (status != MembershipPointsOperationStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.Status == status && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                                         where !x.IsActive && x.SourceTenantId == refId && x.Type == operationType && x.ExpirationDate < date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        break;

                                }

                                break;
                        }
                        break;
                }

                if (query != null)
                {
                    operations = new List<MembershipPointsOperation>();
                    MembershipPointsOperation operation = null;

                    foreach (OltpmembershipPointsOperationsView item in query)
                    {
                        operation = new MembershipPointsOperation
                        {
                            Id = item.Id,
                            ProviderMembershipId = item.ProviderMembershipId,
                            ProviderUserName = item.ProviderUserName,
                            ProviderUserEmail = item.ProviderUserEmail,
                            BeneficiaryMembershipId = item.BeneficiaryMembershipId,
                            BeneficiaryUserName = item.BeneficiaryUserName,
                            BeneficiaryUserEmail = item.BeneficiaryUserEmail,
                            BeneficiaryTenantId = item.BeneficiaryTenantId,
                            BeneficiaryBranchId = item.BeneficiaryBranchId,
                            SourceTenantId = item.SourceTenantId,
                            MonetaryFeeLogId = item.MonetaryFeeLogId,
                            MonetaryFeeLogReason = item.MonetaryFeeLogReason,
                            MoneraryFeeLogStatus = item.MonetaryFeeLogStatus,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            Type = item.Type,
                            TypeName = this.GetOperationTypeName(item.Type),
                            ObjectiveType = item.ObjectiveType,
                            ObjectiveTypeName = this.GetOperationObjectiveTypeName(item.ObjectiveType),
                            Status = item.Status,
                            StatusName = this.GetOperationStatusName(item.Status),
                            AvailablePoints = item.AvailablePoints,
                            UsedPoints = item.UsedPoints,
                            Code = item.Code,
                            ConvertedAmount = item.ConvertedAmount,
                            IsActive = item.IsActive,
                            Registered = item.Registered,
                            Details = item.Details,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        operations.Add(operation);
                    }
                }

            }
            catch (Exception e)
            {
                operations = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return operations;
        }

        public MembershipPointsOperation Get(Guid id, int nothing)
        {
            MembershipPointsOperation operation = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (OltpmembershipPointsOperationsView item in query)
                    {
                        operation = new MembershipPointsOperation
                        {
                            Id = item.Id,
                            ProviderMembershipId = item.ProviderMembershipId,
                            ProviderUserName = item.ProviderUserName,
                            ProviderUserEmail = item.ProviderUserEmail,
                            BeneficiaryMembershipId = item.BeneficiaryMembershipId,
                            BeneficiaryUserName = item.BeneficiaryUserName,
                            BeneficiaryUserEmail = item.BeneficiaryUserEmail,
                            BeneficiaryTenantId = item.BeneficiaryTenantId,
                            BeneficiaryBranchId = item.BeneficiaryBranchId,
                            SourceTenantId = item.SourceTenantId,
                            MonetaryFeeLogId = item.MonetaryFeeLogId,
                            MonetaryFeeLogReason = item.MonetaryFeeLogReason,
                            MoneraryFeeLogStatus = item.MonetaryFeeLogStatus,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            Type = item.Type,
                            TypeName = this.GetOperationTypeName(item.Type),
                            ObjectiveType = item.ObjectiveType,
                            ObjectiveTypeName = this.GetOperationObjectiveTypeName(item.ObjectiveType),
                            Status = item.Status,
                            StatusName = this.GetOperationStatusName(item.Status),
                            AvailablePoints = item.AvailablePoints,
                            UsedPoints = item.UsedPoints,
                            Code = item.Code,
                            ConvertedAmount = item.ConvertedAmount,
                            IsActive = item.IsActive,
                            Registered = item.Registered,
                            Details = item.Details,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                operation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return operation;
        }

        public MembershipPointsOperation Get(string code, int type)
        {
            MembershipPointsOperation operation = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                            where x.Code == code && x.Type == type
                            select x;

                if (query != null)
                {
                    foreach (OltpmembershipPointsOperationsView item in query)
                    {
                        operation = new MembershipPointsOperation
                        {
                            Id = item.Id,
                            ProviderMembershipId = item.ProviderMembershipId,
                            ProviderUserName = item.ProviderUserName,
                            ProviderUserEmail = item.ProviderUserEmail,
                            BeneficiaryMembershipId = item.BeneficiaryMembershipId,
                            BeneficiaryUserName = item.BeneficiaryUserName,
                            BeneficiaryUserEmail = item.BeneficiaryUserEmail,
                            BeneficiaryTenantId = item.BeneficiaryTenantId,
                            BeneficiaryBranchId = item.BeneficiaryBranchId,
                            SourceTenantId = item.SourceTenantId,
                            MonetaryFeeLogId = item.MonetaryFeeLogId,
                            MonetaryFeeLogReason = item.MonetaryFeeLogReason,
                            MoneraryFeeLogStatus = item.MonetaryFeeLogStatus,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            Type = item.Type,
                            TypeName = this.GetOperationTypeName(item.Type),
                            ObjectiveType = item.ObjectiveType,
                            ObjectiveTypeName = this.GetOperationObjectiveTypeName(item.ObjectiveType),
                            Status = item.Status,
                            StatusName = this.GetOperationStatusName(item.Status),
                            AvailablePoints = item.AvailablePoints,
                            UsedPoints = item.UsedPoints,
                            Code = item.Code,
                            ConvertedAmount = item.ConvertedAmount,
                            IsActive = item.IsActive,
                            Registered = item.Registered,
                            Details = item.Details,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                operation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return operation;
        }

        /// <summary>
        /// Gets the count of enabled actions that user has registred within a time frame
        /// </summary>
        /// <param name="membershipId"></param>
        /// <param name="tenantId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int Get(Guid membershipId, Guid tenantId, DateTime startDate, DateTime endDate)
        {
            int? actionsCount;
            try
            {
                actionsCount = this._businessObjects.StoredProcsHandler.GetMembershipActionsCountForUser(membershipId, tenantId, startDate, endDate);
            }
            catch (Exception e)
            {
                actionsCount = -1;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return (int)actionsCount;
        }

        public bool Put(Guid operationId)
        {
            bool success = false;


            try
            {
                var query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                            where x.Id == operationId
                            select x;

                if (query != null)
                {
                    OltpmembershipPointsOperations operation = null;
                    foreach (OltpmembershipPointsOperations item in query)
                    {
                        operation = item;
                    }

                    if (operation != null)
                    {
                        operation.IsActive = !operation.IsActive;
                        operation.UpdatedDate = DateTime.UtcNow;
                        this._businessObjects.Context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Put(Guid operationId, bool registerStatus)
        {
            bool success = false;


            try
            {
                var query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                            where x.Id == operationId
                            select x;

                if (query != null)
                {
                    OltpmembershipPointsOperations operation = null;
                    foreach (OltpmembershipPointsOperations item in query)
                    {
                        operation = item;
                    }

                    if (operation != null)
                    {
                        operation.Registered = registerStatus;
                        operation.UpdatedDate = DateTime.UtcNow;
                        this._businessObjects.Context.SaveChanges();
                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Put(Guid operationId, int status)
        {
            bool success = false;


            try
            {
                var query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                            where x.Id == operationId
                            select x;

                if (query != null)
                {
                    OltpmembershipPointsOperations operation = null;
                    foreach (OltpmembershipPointsOperations item in query)
                    {
                        operation = item;
                    }

                    if (operation != null)
                    {
                        operation.Status = status;
                        operation.UpdatedDate = DateTime.UtcNow;
                        this._businessObjects.Context.SaveChanges();
                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Put(Guid operationId, Guid feeLogId)
        {
            bool success = false;


            try
            {
                var query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                            where x.Id == operationId
                            select x;

                if (query != null)
                {
                    OltpmembershipPointsOperations operation = null;
                    foreach (OltpmembershipPointsOperations item in query)
                    {
                        operation = item;
                    }

                    if (operation != null)
                    {
                        operation.MonetaryFeeLogId = feeLogId;
                        operation.UpdatedDate = DateTime.UtcNow;
                        this._businessObjects.Context.SaveChanges();
                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        /// <summary>
        /// Updates loyalty points balance
        /// </summary>
        /// <param name="username"></param>
        /// <param name="loyaltyPoints"></param>
        /// <returns></returns>
        public Guid? Put(Guid membershipId, decimal loyaltyPoints, Guid beneficiaryTenantId, Guid sourceTenantId, Guid? monetaryFeeLogId, Guid? referenceId, int referenceType, int validMonths, int objectiveType, int status, string details)
        {
            Guid? operationId = null;

            try
            {

                if (loyaltyPoints > 0)//Add points
                {

                    OltpmembershipPointsOperations newOperation = new OltpmembershipPointsOperations
                    {
                        Id = Guid.NewGuid(),
                        ProviderMembershipId = membershipId,
                        BeneficiaryMembershipId = null,
                        BeneficiaryTenantId = beneficiaryTenantId,
                        BeneficiaryBranchId = null,
                        SourceTenantId = sourceTenantId,
                        MonetaryFeeLogId = monetaryFeeLogId,
                        ReferenceId = referenceId,
                        ReferenceType = referenceType,
                        Type = MembershipPointsOperationTypes.PointsBalance,
                        ObjectiveType = objectiveType,
                        Status = status,
                        AvailablePoints = loyaltyPoints,
                        UsedPoints = 0,
                        Code = "",
                        ConvertedAmount = 0,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        ExpirationDate = DateTime.UtcNow.AddMonths(validMonths),
                        IsActive = true,
                        Registered = true,
                        Details = details,

                    };

                    this._businessObjects.Context.OltpmembershipPointsOperations.Add(newOperation);
                    this._businessObjects.Context.SaveChanges();

                    operationId = newOperation.Id;
                }
                else //Redeem points
                {
                    //Needs to turn it to possitive to deduct from the available points
                    loyaltyPoints *= -1;
                    decimal pointsToUse = loyaltyPoints;

                    var query = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                where x.ProviderMembershipId == membershipId && x.BeneficiaryTenantId == beneficiaryTenantId && x.IsActive && x.Type == MembershipPointsOperationTypes.PointsBalance && x.Status == MembershipPointsOperationStatuses.Accessible && x.AvailablePoints > 0 && x.ExpirationDate > DateTime.UtcNow
                                orderby x.ExpirationDate ascending
                                select x;

                    if (query != null)
                    {
                        List<OltpmembershipPointsOperations> operations = query.ToList();

                        for (int i = 0; i <= operations.Count && loyaltyPoints > 0; ++i)
                        {
                            if (operations[i].AvailablePoints >= loyaltyPoints)
                            {
                                operations[i].AvailablePoints -= loyaltyPoints;
                                operations[i].UsedPoints += loyaltyPoints;

                                loyaltyPoints = 0;//No more points to redeem
                            }
                            else //if loyaltyPoints is greater than the operation available points
                            {
                                loyaltyPoints -= operations[i].AvailablePoints;

                                operations[i].UsedPoints += operations[i].AvailablePoints;
                                operations[i].AvailablePoints = 0;
                            }
                        }

                        if (loyaltyPoints == 0)
                        {

                            //Now will register the used points in the membership
                            var queryMembership = from x in this._businessObjects.Context.Oltpmemberships
                                                  where x.Id == membershipId
                                                  select x;

                            if (queryMembership != null)
                            {
                                foreach (Oltpmemberships item in queryMembership)
                                {
                                    item.UsedLoyaltyPoints += pointsToUse;
                                    item.UpdatedDate = DateTime.UtcNow;
                                }
                            }

                            this._businessObjects.Context.SaveChanges();
                            operationId = Guid.Empty;
                        }
                    }
                }



            }
            catch (Exception e)
            {
                operationId = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return operationId;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Process a points transfer operation between users
        /// </summary>
        /// <param name="username"></param>
        /// <param name="loyaltyPoints"></param>
        /// <returns></returns>
        public bool Put(Guid providerMembershipId, Guid beneficiaryMembershipId, decimal loyaltyPoints, int validMonts, Guid beneficiaryTenantId, Guid sourceTenantId, DateTime date, int objectiveType, int status, string detailsSender, string detailsReceiver)
        {
            bool allowed = false;
            bool success = false;

            try
            {
                List<PointOpToTransfer> pointOpToTransfers = new List<PointOpToTransfer>();
                PointOpToTransfer pointOpToTransfer = null;

                //--------------------------------------------------------------------------------------------------
                //1st needs to get deduct the points from the provider membership

                var providerQuery = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                    where x.ProviderMembershipId == providerMembershipId && x.IsActive && x.Type == MembershipPointsOperationTypes.PointsBalance && x.Status == MembershipPointsOperationStatuses.Accessible && x.AvailablePoints > 0 && x.ExpirationDate > date
                                    orderby x.ExpirationDate ascending
                                    select x;

                if (providerQuery != null)
                {
                    List<OltpmembershipPointsOperations> providerOps = providerQuery.ToList();
                    decimal pointsToRedeem = loyaltyPoints;

                    for (int i = 0; i < providerOps.Count && pointsToRedeem > 0; ++i)
                    {
                        if (providerOps[i].AvailablePoints >= pointsToRedeem)
                        {
                            providerOps[i].AvailablePoints -= pointsToRedeem;
                            providerOps[i].UsedPoints += pointsToRedeem;
                            providerOps[i].UpdatedDate = DateTime.UtcNow;

                            pointOpToTransfer = new PointOpToTransfer
                            {
                                OpId = providerOps[i].Id,
                                PointsToTransfer = pointsToRedeem,
                                PointsExpirationDate = providerOps[i].ExpirationDate
                            };

                            pointOpToTransfers.Add(pointOpToTransfer);

                            pointsToRedeem = 0;//No more points to redeem

                        }
                        else //if loyaltyPoints is greater than the operation available points
                        {
                            pointsToRedeem -= providerOps[i].AvailablePoints;

                            providerOps[i].UsedPoints += providerOps[i].AvailablePoints;
                            providerOps[i].AvailablePoints = 0;
                            providerOps[i].UpdatedDate = DateTime.UtcNow;

                            pointOpToTransfer = new PointOpToTransfer
                            {
                                OpId = providerOps[i].Id,
                                PointsToTransfer = providerOps[i].AvailablePoints,
                                PointsExpirationDate = providerOps[i].ExpirationDate
                            };

                            pointOpToTransfers.Add(pointOpToTransfer);
                        }
                    }

                    if (pointsToRedeem == 0)
                    {
                        allowed = true;
                    }
                }

                //---------------------------------------------------------------------------------------------------

                //2nd, needs to create the transfer operation

                if (allowed)
                {
                    OltpmembershipPointsOperations transferOperation = new OltpmembershipPointsOperations
                    {
                        Id = Guid.NewGuid(),
                        ProviderMembershipId = providerMembershipId,
                        BeneficiaryMembershipId = beneficiaryMembershipId,
                        BeneficiaryTenantId = beneficiaryTenantId,
                        BeneficiaryBranchId = null,
                        SourceTenantId = sourceTenantId,
                        MonetaryFeeLogId = null,
                        ReferenceId = null,
                        ReferenceType = MembershipPointsOperationReferenceTypes.NoRef,
                        ObjectiveType = objectiveType,
                        Type = MembershipPointsOperationTypes.TransferPoints,
                        Status = status,
                        AvailablePoints = 0,
                        UsedPoints = loyaltyPoints,
                        Code = "",
                        ConvertedAmount = 0,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        ExpirationDate = DateTime.MaxValue,
                        IsActive = true,
                        Registered = true,
                        Details = detailsSender
                    };

                    this._businessObjects.Context.OltpmembershipPointsOperations.Add(transferOperation);

                    //--------------------------------------------------------------------------------------------------------
                    //Finally needs to create the points balance operations to count the points for the beneficiary user

                    if (pointOpToTransfers?.Count > 0)
                    {
                        List<OltpmembershipPointsOperations> pointsTransferAddOps = new List<OltpmembershipPointsOperations>();
                        OltpmembershipPointsOperations pointsTransferAddOp = null;

                        foreach (PointOpToTransfer opItem in pointOpToTransfers)
                        {

                            pointsTransferAddOp = new OltpmembershipPointsOperations
                            {
                                Id = Guid.NewGuid(),
                                ProviderMembershipId = beneficiaryMembershipId,
                                BeneficiaryMembershipId = null,
                                BeneficiaryTenantId = beneficiaryTenantId,
                                BeneficiaryBranchId = null,
                                SourceTenantId = sourceTenantId,
                                MonetaryFeeLogId = null,
                                ReferenceId = opItem.OpId,//references the operation from where the points were taken
                                ReferenceType = MembershipPointsOperationReferenceTypes.MembershipOperation,
                                Type = MembershipPointsOperationTypes.PointsBalance,
                                ObjectiveType = objectiveType,
                                Status = status,
                                AvailablePoints = opItem.PointsToTransfer,
                                UsedPoints = 0,
                                Code = "",
                                ConvertedAmount = 0,
                                CreatedDate = DateTime.UtcNow,
                                UpdatedDate = DateTime.UtcNow,
                                ExpirationDate = opItem.PointsExpirationDate, //the expiration dates matches the date of the operation from where points has been taken
                                IsActive = true,
                                Registered = true,
                                Details = detailsReceiver
                            };

                            pointsTransferAddOps.Add(pointsTransferAddOp);

                        }

                        this._businessObjects.Context.OltpmembershipPointsOperations.AddRange(pointsTransferAddOps);

                        //-----------------------------------------------------------------------------------------------------
                        //Now it's the time to send all the changed to database

                        this._businessObjects.Context.SaveChanges();
                        success = true;
                    }

                }

            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //

        ////NEEDS TO BE CHECKED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// <summary>
        /// Points conversion to money
        /// </summary>
        /// <param name="membershipId"></param>
        /// <param name="convertedAmount"></param>
        /// <returns></returns>
        public MembershipPointsOperation Put(string userId, Guid providerMembershipId, Guid providerTenantId, Guid beneficiaryTenantId, Guid? beneficiaryBranchId, string beneficiaryTenantName, decimal convertedAmount, long requiredPoints, decimal correspondingCodeAmount, int objectiveType, int status, string details, DateTime date)
        {
            MembershipPointsOperation operation = null;
            bool allowed = false;

            try
            {
                OltpmembershipsView providerMembership = (from x in this._businessObjects.Context.OltpmembershipsView
                                                          where x.Id == providerMembershipId && x.IsActive && !x.Blocked
                                                          select x).FirstOrDefault();

                if (true)// (providerMembership != null)
                {

                    if (requiredPoints > 0)
                    {
                        //--------------------------------------------------------------------------------------------------
                        //1st needs to get deduct the points from the provider membership

                        var providerQuery = from x in this._businessObjects.Context.OltpmembershipPointsOperations
                                            where x.ProviderMembershipId == providerMembershipId && x.IsActive && x.Type == MembershipPointsOperationTypes.PointsBalance && x.Status == MembershipPointsOperationStatuses.Accessible && x.AvailablePoints > 0 && x.ExpirationDate > date
                                            orderby x.ExpirationDate ascending
                                            select x;

                        if (providerQuery != null)
                        {
                            List<OltpmembershipPointsOperations> providerOps = providerQuery.ToList();
                            decimal pointsToRedeem = requiredPoints;

                            for (int i = 0; i < providerOps.Count && pointsToRedeem > 0; ++i)
                            {
                                if (providerOps[i].AvailablePoints >= pointsToRedeem)
                                {
                                    providerOps[i].AvailablePoints -= pointsToRedeem;
                                    providerOps[i].UsedPoints += pointsToRedeem;

                                    pointsToRedeem = 0;//No more points to redeem
                                }
                                else //if loyaltyPoints is greater than the operation available points
                                {
                                    pointsToRedeem -= providerOps[i].AvailablePoints;

                                    providerOps[i].UsedPoints += providerOps[i].AvailablePoints;
                                    providerOps[i].AvailablePoints = 0;
                                }
                            }

                            if (pointsToRedeem == 0)
                            {
                                allowed = true;
                            }
                        }

                        //---------------------------------------------------------------------------------------------------

                        //2nd, needs to create the conversion operation
                        if (allowed)
                        {
                            string conversionCode = "";

                            //NEEDS TO GENERATE THE CONVERSION CODE
                            conversionCode = AssignOperationCode(beneficiaryTenantName, date);


                            OltpmembershipPointsOperations convertOperation = new OltpmembershipPointsOperations
                            {
                                Id = Guid.NewGuid(),
                                ProviderMembershipId = providerMembershipId,
                                BeneficiaryMembershipId = null,
                                BeneficiaryTenantId = beneficiaryTenantId,
                                BeneficiaryBranchId = beneficiaryBranchId,
                                SourceTenantId = providerTenantId,
                                MonetaryFeeLogId = null,
                                ReferenceId = null,
                                ReferenceType = MembershipPointsOperationReferenceTypes.NoRef,
                                ObjectiveType = objectiveType,
                                Type = MembershipPointsOperationTypes.ConvertPoints,
                                Status = status,
                                AvailablePoints = 0,
                                UsedPoints = requiredPoints,
                                Code = conversionCode,
                                ConvertedAmount = convertedAmount,
                                CreatedDate = DateTime.UtcNow,
                                ExpirationDate = DateTime.UtcNow.AddHours(3),
                                IsActive = true,
                                Registered = false,//In this case the transaction hasn't been registered
                                Details = details
                            };

                            this._businessObjects.Context.OltpmembershipPointsOperations.Add(convertOperation);

                            //Finally will create the money convert log
                            OltpmoneyConversionLogs moneyConversionLog = new OltpmoneyConversionLogs
                            {
                                Id = Guid.NewGuid(),
                                OperationId = convertOperation.Id,
                                TenantId = beneficiaryTenantId,
                                BranchId = beneficiaryBranchId,
                                EmployeeId = null,
                                EmployeeUserName = "",
                                CreatedDate = convertOperation.CreatedDate,
                                ConversionCode = convertOperation.Code,
                                RequiredPoints = requiredPoints,
                                MoneyAmount = convertedAmount,
                                State = MoneyConversionLogStates.PointsConverted,
                                OwnerId = userId,
                                ClaimerId = "",
                                InternalStatus = MoneyConversionLogInternalStatuses.Created,
                                LastStatusUpdate = DateTime.UtcNow
                            };

                            this._businessObjects.Context.OltpmoneyConversionLogs.Add(moneyConversionLog);
                            //Now stores the records in database
                            this._businessObjects.Context.SaveChanges();

                            var query = from x in this._businessObjects.Context.OltpmembershipPointsOperationsView
                                        where x.Id == convertOperation.Id
                                        select x;

                            if (query != null)
                            {
                                foreach (OltpmembershipPointsOperationsView item in query)
                                {
                                    operation = new MembershipPointsOperation
                                    {
                                        Id = item.Id,
                                        ProviderMembershipId = item.ProviderMembershipId,
                                        ProviderUserName = item.ProviderUserName,
                                        ProviderUserEmail = item.ProviderUserEmail,
                                        BeneficiaryMembershipId = item.BeneficiaryMembershipId,
                                        BeneficiaryUserName = item.BeneficiaryUserName,
                                        BeneficiaryUserEmail = item.BeneficiaryUserEmail,
                                        BeneficiaryTenantId = item.BeneficiaryTenantId,
                                        BeneficiaryBranchId = item.BeneficiaryBranchId,
                                        SourceTenantId = item.SourceTenantId,
                                        MonetaryFeeLogId = item.MonetaryFeeLogId,
                                        MonetaryFeeLogReason = item.MonetaryFeeLogReason,
                                        MoneraryFeeLogStatus = item.MonetaryFeeLogStatus,
                                        ReferenceId = item.ReferenceId,
                                        ReferenceType = item.ReferenceType,
                                        Type = item.Type,
                                        TypeName = this.GetOperationTypeName(item.Type),
                                        ObjectiveType = item.ObjectiveType,
                                        ObjectiveTypeName = this.GetOperationObjectiveTypeName(item.ObjectiveType),
                                        AvailablePoints = item.AvailablePoints,
                                        UsedPoints = item.UsedPoints,
                                        Code = item.Code,
                                        ConvertedAmount = item.ConvertedAmount,
                                        IsActive = item.IsActive,
                                        Registered = item.Registered,
                                        Details = item.Details,
                                        CreatedDate = item.CreatedDate,
                                        ExpirationDate = item.ExpirationDate,
                                        ConversionLogId = moneyConversionLog.Id
                                    };
                                }
                            }

                        }

                    }

                }
            }
            catch (Exception e)
            {
                operation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return operation;
        }

        private string AssignOperationCode(string commerceName, DateTime date)
        {
            Random r = new Random();
            Guid g;
            string code;

            int? codeCount;
            do
            {
                g = Guid.NewGuid();
                string guidString = Convert.ToBase64String(g.ToByteArray());
                guidString = guidString.Replace("=", "");
                guidString = guidString.Replace("+", "");

                int rInt = r.Next(0, guidString.Length - 4);
                guidString = guidString.Substring(rInt, 4);

                commerceName = commerceName.Trim().Substring(0, 2);
                char[] comNameArray = commerceName.ToArray();

                Random random = new Random();
                int switchPos1 = random.Next(0, 2);
                int switchPos2 = random.Next(0, 2);

                if (switchPos1 != switchPos2)
                {
                    char temp = comNameArray[switchPos1];

                    comNameArray[switchPos1] = comNameArray[switchPos2];
                    comNameArray[switchPos2] = temp;
                }

                commerceName = new string(comNameArray);

                code = commerceName + guidString;


                //Replace invalid chars and convert it all to capital letters
                code = code.Replace('/', 'l').Replace('\\', 'l').Replace('_', 'u').Replace('-', 'h').Replace('~', 'j').ToLower();

                //Verifies if the code exists
                codeCount = this._businessObjects.StoredProcsHandler.CheckConversionOperationCodeExistence(code, date.AddMonths(-6));

            } while (!(codeCount == 0));

            return code;
        }

        #endregion

        #region VALIDPURCHASEREGISTRIES

        private List<ValidPurchaseRegistry> GetsUserValidPurchaseRegistriesByState(string userId, Guid stateId, Guid countryId, int status)
        {
            List<ValidPurchaseRegistry> purchaseRegistries = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetUserValidPurchasesRegistiriesByState(stateId, countryId, userId)
                            select x;


                if (query != null)
                {
                    purchaseRegistries = new List<ValidPurchaseRegistry>();
                    ValidPurchaseRegistry registry = null;

                    foreach (OltpvalidatePurchaseRegistries item in query)
                    {
                        registry = new ValidPurchaseRegistry
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MembershipId = item.MembershipId,
                            ReceiptId = item.ReceiptId,
                            TotalAmount = item.TotalAmount,
                            PointsGenerationType = item.PointsGenerationType,
                            ClubPointsGenerated = item.ClubPointsGenerated,
                            WalletPointsGenerated = item.WalletPointsGenerated,
                            CreatedDate = item.CreatedDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        purchaseRegistries.Add(registry);

                    }
                }

            }
            catch (Exception e)
            {
                purchaseRegistries = null;//ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchaseRegistries;
        }

        private List<ValidPurchaseRegistry> GetsUserValidPurchaseRegistriesForTenant(string userId, Guid tenantId, DateTime lastLevelEvaluationDate, DateTime dateTime)
        {
            List<ValidPurchaseRegistry> purchaseRegistries = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetUserValidPurchaseRegistriesForTenant(tenantId, userId, lastLevelEvaluationDate, dateTime)
                            select x;


                if (query != null)
                {
                    purchaseRegistries = new List<ValidPurchaseRegistry>();
                    ValidPurchaseRegistry registry = null;

                    foreach (OltpvalidatePurchaseRegistries item in query)
                    {
                        registry = new ValidPurchaseRegistry
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MembershipId = item.MembershipId,
                            ReceiptId = item.ReceiptId,
                            TotalAmount = item.TotalAmount,
                            PointsGenerationType = item.PointsGenerationType,
                            ClubPointsGenerated = item.ClubPointsGenerated,
                            WalletPointsGenerated = item.WalletPointsGenerated,
                            CreatedDate = item.CreatedDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        purchaseRegistries.Add(registry);

                    }
                }

            }
            catch (Exception e)
            {
                purchaseRegistries = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchaseRegistries;
        }

        #endregion


        #region MONEYCONVERSIONLOGS

        public bool Post(Guid? operationId, Guid tenantId, Guid branchId, Guid? employeeId, string employeeUsername, string code, int state, string ownerId, string claimerId, int internalStatus)
        {
            bool success;
            try
            {
                OltpmoneyConversionLogs moneyConversionLog = new OltpmoneyConversionLogs
                {
                    Id = Guid.NewGuid(),
                    OperationId = operationId,
                    TenantId = tenantId,
                    BranchId = branchId,
                    EmployeeId = employeeId,
                    EmployeeUserName = employeeUsername,
                    ConversionCode = code,
                    State = state,
                    OwnerId = ownerId,
                    ClaimerId = claimerId,
                    InternalStatus = internalStatus,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    LastStatusUpdate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpmoneyConversionLogs.Add(moneyConversionLog);
                this._businessObjects.Context.SaveChanges();

                success = true;
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public MembershipManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD PRODUCT PREFERENCE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
