using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DTO.Entities.Misc.TenantData;
using YOY.DTO.Entities.Misc.ObjectState.POCO;
using YOY.DTO.Entities.Misc.Structure.POCO;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities;
using YOY.DAO.Entities.DB;
using Microsoft.Data.SqlClient;

namespace YOY.DAO.Entities.Manager
{
    public class TenantManager
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

        #region METHODS
        
        private string GetLanguageName(int language)
        {
            string languageName = language switch
            {
                Languages.Spanish => Resources.Spanish,
                Languages.English => Resources.English,
                _ => "--",
            };
            return languageName;
        }

        private string GetCurrencyTypeName(int currencyType)
        {
            string currencyTypeName = currencyType switch
            {
                CurrencyTypes.MexicanPeso => Resources.MexicanPesoCurrency,
                CurrencyTypes.CostaRicanColon => Resources.CostaRicanColonCurrency,
                CurrencyTypes.ColombianPeso => Resources.ColombianPesoCurrency,
                CurrencyTypes.USDollar => Resources.USDollarCurrency,
                _ => "--",
            };
            return currencyTypeName;
        }

        private string GetCheckInTypeName(int checkInType)
        {
            string checkInTypeName = checkInType switch
            {
                TenantEnabledCheckInTypes.None => Resources.None,
                TenantEnabledCheckInTypes.WalkIn => Resources.WalkIn,
                TenantEnabledCheckInTypes.Cashier => Resources.Cashier,
                TenantEnabledCheckInTypes.WalkInAndCashier => Resources.WalkInCashier,
                _ => "--",
            };
            return checkInTypeName;
        }

        private string GetReferenceCodeTypeName(int referenceCodeType)
        {
            string referenceCodeTypeName = referenceCodeType switch
            {
                TenantReferenceCodeTypes.None => Resources.None,
                TenantReferenceCodeTypes.AllPromos => Resources.AllOffers,
                TenantReferenceCodeTypes.ExclusiveOffers => Resources.ExclusiveOffers,
                TenantReferenceCodeTypes.ValidReceipts => Resources.ValidReceipts,
                _ => "--",
            };
            return referenceCodeTypeName;
        }

        private string GetLoyaltyProgramTypeName(int loyaltyProgramType)
        {
            string loyaltyProgramTypeName = loyaltyProgramType switch
            {
                LoyaltyProgramTypes.None => Resources.None,
                LoyaltyProgramTypes.Basic => Resources.Basic,
                LoyaltyProgramTypes.Plus => Resources.Plus,
                LoyaltyProgramTypes.Premium => Resources.Premium,
                _ => "--",
            };
            return loyaltyProgramTypeName;
        }

        private string GetClaimMethodName(int claimMethod)
        {
            string claimMethodName = claimMethod switch
            {
                DealClaimMethods.UserApp => Resources.UserApp,
                DealClaimMethods.CashierApp => Resources.CashierApp,
                _ => "--",
            };
            return claimMethodName;
        }

        private string GetTypeName(int tenantType)
        {
            string tenantTypeName = tenantType switch
            {
                TenantTypes.Commerce => Resources.Commerce,
                TenantTypes.Brand => Resources.Brand,
                TenantTypes.Holder => Resources.Holder,
                _ => "--",
            };
            return tenantTypeName;
        }

        private string GetInstanceTypeName(int instanceType)
        {
            string instanceTypeName = instanceType switch
            {
                TenantInstanceTypes.Core => Resources.Core,
                TenantInstanceTypes.Business => Resources.RetailBusiness,
                TenantInstanceTypes.ShoppingMall => Resources.ShoppingMall,
                TenantInstanceTypes.Supermarket => Resources.Supermarket,
                _ => "--",
            };
            return instanceTypeName;
        }

        private string GetBusinessStructureTypeName(int businessStructureType)
        {
            string businessStructureTypeName = businessStructureType switch
            {
                TenantBusinessStructureTypes.BrandHolder => Resources.BrandHolder,
                TenantBusinessStructureTypes.Franchises => Resources.Franchises,
                TenantBusinessStructureTypes.BrandHolderAndFrachises => Resources.BrandHolderAndFrachises,
                _ => "--",
            };
            return businessStructureTypeName;
        }

        private string GetPayerTypeName(int payerType)
        {
            string payerTypeName = payerType switch
            {
                TenantPayerTypes.FreeService => Resources.FreeService,
                TenantPayerTypes.BrandHolder => Resources.BrandHolder,
                TenantPayerTypes.Franchisees => Resources.Franchisee,
                _ => "--",
            };
            return payerTypeName;
        }

        private string GetRelevanceStatusName(int relevanceStatus)
        {
            string relevanceStatusName = relevanceStatus switch
            {
                TenantRelevanceStatuses.SingleLocation => Resources.SingleLocation,
                TenantRelevanceStatuses.SmallBusiness => Resources.SmallBusiness,
                TenantRelevanceStatuses.CityBusiness => Resources.CityBusiness,
                TenantRelevanceStatuses.StateBusiness => Resources.StateBusiness,
                TenantRelevanceStatuses.RegionalBusiness => Resources.RegionalBusiness,
                TenantRelevanceStatuses.NationalBusiness => Resources.NationalBusiness,
                TenantRelevanceStatuses.InternationalBusiness => Resources.InternationalBusiness,
                TenantRelevanceStatuses.AnchorBusiness => Resources.AnchorBusiness,
                _ => "--",
            };
            return relevanceStatusName;
        }

        /// <summary>
        /// Retrieve all Tenants Data
        /// </summary>
        /// <returns></returns>
        public List<Object> Gets(Guid? countryId, int activeState, int dataStructureType, int releasedState, int pageSize, int pageNumber)
        {
            List<Object> tenants = new List<Object>();
            TenantInfo currentTenant = null;
            MinTenantInfo currentTenantMin = null;

            try
            {
                var query = (dynamic)null;
                
                switch(activeState){
                    case ActiveStates.All:
                        switch (releasedState)
                        {
                            case ReleaseStates.All:
                                if(countryId != null)
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                            where x.CountryId == countryId
                                            orderby x.Name ascending
                                            select x).Skip(pageSize*pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                
                                break;
                            case ReleaseStates.Released:
                                if(countryId != null)
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                            where x.Released && x.CountryId == countryId
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                            where x.Released
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ReleaseStates.NotReleased:
                                if(countryId != null)
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                            where !x.Released && x.CountryId == countryId
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                            where !x.Released
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }
                        
                        break;
                    case ActiveStates.Active:
                        switch (releasedState)
                        {
                            case ReleaseStates.All:
                                if(countryId != null)
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                            where x.IsActive && x.CountryId == countryId
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where x.IsActive
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ReleaseStates.Released:
                                if(countryId != null)
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where x.Released && x.IsActive && x.CountryId == countryId
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where x.Released && x.IsActive
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ReleaseStates.NotReleased:
                                if(countryId != null)
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where !x.Released && x.IsActive && x.CountryId == countryId
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where !x.Released && x.IsActive
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                
                                break;
                        }
                        
                        break;
                    case ActiveStates.Inactive:
                        switch (releasedState)
                        {
                            case ReleaseStates.All:
                                if(countryId != null)
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where !x.IsActive && x.CountryId == countryId
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where !x.IsActive
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ReleaseStates.Released:
                                if(countryId != null)
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where !x.IsActive && x.Released && x.CountryId == countryId
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where !x.IsActive && x.Released
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ReleaseStates.NotReleased:
                                if (countryId != null)
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                             where !x.IsActive && !x.Released && x.CountryId == countryId
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeftenantInfosView
                                            where !x.IsActive && !x.Released
                                            orderby x.Name ascending
                                            select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }
                        
                        break;
                }


                if (query != null)
                {
                    switch (dataStructureType)
                    {
                        case TenantStructureTypes.Complete:

                            foreach (DeftenantInfosView tenant in query)
                            {
                                //If not the base commerce
                                if (tenant.Id != Guid.Empty)
                                {
                                    currentTenant = new TenantInfo
                                    {
                                        Id = tenant.Id,
                                        TenantId = tenant.TenantId,
                                        Name = tenant.Name,
                                        LegalName = tenant.LegalName,
                                        TaxId = tenant.TaxId,
                                        TaxAddress = tenant.TaxAddress,
                                        PaymentSubject = tenant.PaymentSubject,
                                        AdditionalNotes = tenant.AdditionalNotes,
                                        Description = tenant.Description,
                                        Logo = tenant.Logo,
                                        LogoUrl = tenant.LogoUrl,
                                        WhiteLogo = tenant.WhiteLogo,
                                        WhiteLogoUrl = tenant.WhiteLogoUrl,
                                        CarrouselImgId = tenant.CarrouselImg,
                                        CarrouselImgUrl = tenant.CarrouselImgUrl,
                                        EmailsBackground = tenant.EmailBg,
                                        LandingImg = tenant.LandingImg,
                                        CountryId = tenant.CountryId,
                                        CountryName = tenant.CountryName,
                                        RelevanceStatus = tenant.RelevanceStatus,
                                        RelevanceStatusName = this.GetRelevanceStatusName(tenant.RelevanceStatus),
                                        CreatedDate = tenant.CreatedDate,
                                        UpdatedDate = tenant.UpdatedDate,
                                        PaymentDay = tenant.PaymentDay,
                                        CurrencySymbol = tenant.CurrencySymbol,
                                        CurrencyType = tenant.CurrencyType,
                                        CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                                        ContactName = tenant.ContactName,
                                        ContactEmail = tenant.ContactEmail,
                                        ContactPhone = tenant.ContactPhone,
                                        Keywords = tenant.Keywords,
                                        IsActive = tenant.IsActive,
                                        CategoryId = tenant.CategoryId,
                                        CategoryName = tenant.CommerceCategoryName,
                                        TrialExpiration = tenant.TrialExpiration,
                                        DealRules = tenant.DealRules,
                                        DealConditions = tenant.DealConditions,
                                        IncentiveRules = tenant.IncentiveRules,
                                        IncentiveConditions = tenant.IncentiveConditions,
                                        InStoreDealClaimInstructions = tenant.InStoreDealClaimInstructions,
                                        OnlineDealClaimInstructions = tenant.OnlineDealClaimInstructions,
                                        PhoneDealClaimInstructions = tenant.PhoneDealClaimInstructions,
                                        IncentiveClaimInstructions = tenant.IncentiveClaimInstructions,
                                        TypeId = tenant.TypeId,
                                        TypeName = this.GetTypeName(tenant.TypeId),
                                        InstanceType = tenant.InstanceType,
                                        InstanceTypeName = this.GetInstanceTypeName(tenant.InstanceType),
                                        BusinessStructureType = tenant.BusinessStructureType,
                                        BusinessStructureTypeName = this.GetBusinessStructureTypeName(tenant.BusinessStructureType),
                                        PayerType = tenant.PayerType,
                                        PayerTypeName = this.GetPayerTypeName(tenant.PayerType),
                                        Language = tenant.Language,
                                        LanguageName = this.GetLanguageName(tenant.Language),
                                        Website = tenant.Website,
                                        Released = tenant.Released,
                                        CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                                        CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                                        HasMembershipLevels = tenant.HasMembershipLevels,
                                        LoyaltyProgramType = tenant.LoyaltyProgramType,
                                        LoyaltyProgramTypeName = this.GetLoyaltyProgramTypeName(tenant.LoyaltyProgramType),
                                        AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                                        AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                                        CheckInType = tenant.CheckInType,
                                        CheckInTypeName = this.GetCheckInTypeName(tenant.CheckInType),
                                        ReferenceCodeType = tenant.ReferenceCodeType,
                                        ReferenceCodeTypeName = this.GetReferenceCodeTypeName(tenant.ReferenceCodeType),
                                        DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                                        ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                                        DealClaimMethod = tenant.DealsClaimMethod,
                                        DealClaimMethodName = this.GetClaimMethodName(tenant.DealsClaimMethod),
                                    };

                                    tenants.Add(currentTenant);
                                }

                            }

                            break;
                        case TenantStructureTypes.Min:

                            foreach (DeftenantInfosView tenant in query)
                            {
                                //If not the base commerce
                                if(tenant.TenantId != Guid.Empty)
                                {
                                    currentTenantMin = new MinTenantInfo
                                    {
                                        Id = tenant.Id,
                                        TenantId = tenant.TenantId,
                                        Name = tenant.Name,
                                        IsActive = tenant.IsActive,
                                        Logo = tenant.Logo,
                                        LogoUrl = tenant.LogoUrl,
                                        WhiteLogo = tenant.WhiteLogo,
                                        WhiteLogoUrl = tenant.WhiteLogoUrl,
                                        CarrouselImgId = tenant.CarrouselImg,
                                        CarrouselImgUrl = tenant.CarrouselImgUrl,
                                        LandingImg = tenant.LandingImg,
                                        CountryId = tenant.CountryId,
                                        CountryName = tenant.CountryName,
                                        CurrencySymbol = tenant.CurrencySymbol,
                                        CurrencyType = tenant.CurrencyType,
                                        CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                                        CategoryId = tenant.CategoryId,
                                        ClassificationId = (Guid)tenant.CommerceClassificationId,
                                        PreferenceId = (Guid)tenant.PreferenceId,
                                        CategoryName = tenant.CommerceCategoryName,
                                        TypeId = tenant.TypeId,
                                        InstanceType = tenant.InstanceType,
                                        BusinessStructureType = tenant.BusinessStructureType,
                                        PayerType = tenant.PayerType,
                                        HasMembershipLevels = tenant.HasMembershipLevels,
                                        LoyaltyProgramType = tenant.LoyaltyProgramType,
                                        Language = tenant.Language,
                                        Released = tenant.Released,
                                        CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                                        CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                                        AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                                        AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                                        CheckInType = tenant.CheckInType,
                                        ReferenceCodeType = tenant.ReferenceCodeType,
                                        DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                                        ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                                        DealClaimMethod = tenant.DealsClaimMethod
                                    };

                                    tenants.Add(currentTenantMin);
                                }

                            }

                            break;
                    }
                    
                }
                    
            }
            catch (Exception e)
            {
                tenants = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenants;
        }

        /// <summary>
        /// Retrieve the active/inactive count
        /// </summary>
        /// <returns></returns>
        public List<Pair<int, int>> Gets()
        {
            List<Pair<int, int>> activeStateCounts = new List<Pair<int, int>>();
            int activeCount = 0 ;
            int inactiveCount = 0;
            Pair<int, int> currentCount = null;

            try
            {
                var query = from x in this._businessObjects.Context.DeftenantInfosView
                            where x.TenantId != Guid.Empty
                            orderby x.Name
                            select x;

                if (query != null)
                {
                    foreach (DeftenantInfosView item in query)
                    {
                        if (item.IsActive)
                            ++activeCount;
                        else
                            ++inactiveCount;

                    }

                    currentCount = new Pair<int, int>
                    {
                        Key = ActiveStates.Active,
                        Value = activeCount,
                    };
                    activeStateCounts.Add(currentCount);

                    currentCount = new Pair<int, int>
                    {
                        Key = ActiveStates.Inactive,
                        Value = inactiveCount,
                    };
                    activeStateCounts.Add(currentCount);


                }

            }
            catch (Exception e)
            {
                activeStateCounts = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return activeStateCounts;
        }


        public List<Pair<Guid, string>> Gets(int instanceType, int type, int activeState, Guid countryId)
        {
            List<Pair<Guid, string>> tenants = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        if (instanceType != TenantInstanceTypes.All)
                        {
                            if (type != TenantTypes.All)
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.InstanceType == instanceType && x.TypeId == type
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.InstanceType == instanceType
                                        select x;
                            }
                        }
                        else
                        {
                            if (type != TenantTypes.All)
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.TypeId == type
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId
                                        select x;
                            }
                        }

                        break;
                    case ActiveStates.Active:

                        if (instanceType != TenantInstanceTypes.All)
                        {
                            if (type != TenantTypes.All)
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.IsActive && x.CountryId == countryId && x.InstanceType == instanceType && x.TypeId == type
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.IsActive && x.CountryId == countryId && x.InstanceType == instanceType
                                        select x;
                            }
                        }
                        else
                        {
                            if (type != TenantTypes.All)
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.IsActive && x.CountryId == countryId && x.TypeId == type
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.IsActive && x.CountryId == countryId
                                        select x;
                            }
                        }

                        break;
                    case ActiveStates.Inactive:

                        if (instanceType != TenantInstanceTypes.All)
                        {
                            if (type != TenantTypes.All)
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where !x.IsActive && x.CountryId == countryId && x.InstanceType == instanceType && x.TypeId == type
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where !x.IsActive && x.CountryId == countryId && x.InstanceType == instanceType
                                        select x;
                            }
                        }
                        else
                        {
                            if (type != TenantTypes.All)
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where !x.IsActive && x.CountryId == countryId && x.TypeId == type
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.DeftenantInfosView
                                        where !x.IsActive && x.CountryId == countryId
                                        select x;
                            }
                        }

                        break;
                }

                if(query != null)
                {
                    Pair<Guid, string> tenantData = null;
                    tenants = new List<Pair<Guid, string>>();

                    foreach(DeftenantInfosView item in query)
                    {
                        tenantData = new Pair<Guid, string>
                        {
                            Key = item.TenantId,
                            Value = item.Name
                        };

                        tenants.Add(tenantData);
                    }
                }
            }
            catch (Exception e)
            {
                tenants = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenants;
        }

        /// <summary>
        /// Retrieve all Tenants Data for a category
        /// </summary>
        /// <returns></returns>
        public List<MinTenantInfo> Gets(int activeState, int pageSize, int pageNumber)
        {
            List<MinTenantInfo> tenants = new List<MinTenantInfo>();
            MinTenantInfo currentTenant = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.DeftenantInfosView
                                orderby x.Name ascending
                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.DeftenantInfosView
                                where x.IsActive
                                orderby x.Name ascending
                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.DeftenantInfosView
                                where !x.IsActive
                                orderby x.Name ascending
                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                if (query != null)
                    foreach (DeftenantInfosView tenant in query)
                    {
                        currentTenant = new MinTenantInfo
                        {
                            Id = tenant.Id,
                            TenantId = tenant.TenantId,
                            Name = tenant.Name,
                            IsActive = tenant.IsActive,
                            Logo = tenant.Logo,
                            LogoUrl = tenant.LogoUrl,
                            WhiteLogo = tenant.WhiteLogo,
                            WhiteLogoUrl = tenant.WhiteLogoUrl,
                            CarrouselImgId = tenant.CarrouselImg,
                            CarrouselImgUrl = tenant.CarrouselImgUrl,
                            LandingImg = tenant.LandingImg,
                            CountryId = tenant.CountryId,
                            CountryName = tenant.CountryName,
                            CurrencySymbol = tenant.CurrencySymbol,
                            CurrencyType = tenant.CurrencyType,
                            CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                            CategoryId = tenant.CategoryId,
                            ClassificationId = (Guid)tenant.CommerceClassificationId,
                            PreferenceId = (Guid)tenant.PreferenceId,
                            CategoryName = tenant.CommerceCategoryName,
                            TypeId = tenant.TypeId,
                            InstanceType = tenant.InstanceType,
                            BusinessStructureType = tenant.BusinessStructureType,
                            PayerType = tenant.PayerType,
                            HasMembershipLevels = tenant.HasMembershipLevels,
                            LoyaltyProgramType = tenant.LoyaltyProgramType,
                            Language = tenant.Language,
                            Released = tenant.Released,
                            CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                            CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                            AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                            AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                            CheckInType = tenant.CheckInType,
                            ReferenceCodeType = tenant.ReferenceCodeType,
                            DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                            ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                            DealClaimMethod = tenant.DealsClaimMethod
                        };

                        tenants.Add(currentTenant);

                    }
            }
            catch (Exception e)
            {
                tenants = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenants;
        }

        /// <summary>
        /// Retrieve all Tenants Data for a category
        /// </summary>
        /// <returns></returns>
        public List<MinTenantInfo> Gets(Guid categoryId, int activeState, int pageSize, int pageNumber)
        {
            List<MinTenantInfo> tenants = new List<MinTenantInfo>();
            MinTenantInfo currentTenant = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.DeftenantInfosView
                                where x.CategoryId == categoryId
                                orderby x.Name ascending
                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.DeftenantInfosView
                                where x.CategoryId == categoryId && x.IsActive
                                orderby x.Name ascending
                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.DeftenantInfosView
                                where x.CategoryId == categoryId && !x.IsActive
                                orderby x.Name ascending
                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                if (query != null)
                    foreach (DeftenantInfosView tenant in query)
                    {
                        currentTenant = new MinTenantInfo
                        {
                            Id = tenant.Id,
                            TenantId = tenant.TenantId,
                            Name = tenant.Name,
                            IsActive = tenant.IsActive,
                            Logo = tenant.Logo,
                            LogoUrl = tenant.LogoUrl,
                            WhiteLogo = tenant.WhiteLogo,
                            WhiteLogoUrl = tenant.WhiteLogoUrl,
                            CarrouselImgId = tenant.CarrouselImg,
                            CarrouselImgUrl = tenant.CarrouselImgUrl,
                            LandingImg = tenant.LandingImg,
                            CountryId = tenant.CountryId,
                            CountryName = tenant.CountryName,
                            CurrencySymbol = tenant.CurrencySymbol,
                            CurrencyType = tenant.CurrencyType,
                            CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                            CategoryId = tenant.CategoryId,
                            ClassificationId = (Guid)tenant.CommerceClassificationId,
                            PreferenceId = (Guid)tenant.PreferenceId,
                            CategoryName = tenant.CommerceCategoryName,
                            TypeId = tenant.TypeId,
                            InstanceType = tenant.InstanceType,
                            BusinessStructureType = tenant.BusinessStructureType,
                            PayerType = tenant.PayerType,
                            HasMembershipLevels = tenant.HasMembershipLevels,
                            LoyaltyProgramType = tenant.LoyaltyProgramType,
                            Language = tenant.Language,
                            Released = tenant.Released,
                            CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                            CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                            AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                            AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                            CheckInType = tenant.CheckInType,
                            ReferenceCodeType = tenant.ReferenceCodeType,
                            DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                            ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                            DealClaimMethod = tenant.DealsClaimMethod
                        };

                        tenants.Add(currentTenant);

                    }
            }
            catch (Exception e)
            {
                tenants = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenants;
        }

        /// <summary>
        /// Retrieve all Tenants Data for a category
        /// </summary>
        /// <returns></returns>
        public List<MinTenantInfo> Gets(Guid categoryId, Guid countryId, int activeState, int releasedState, int pageSize, int pageNumber)
        {
            List<MinTenantInfo> tenants = new List<MinTenantInfo>();
            MinTenantInfo currentTenant = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (releasedState)
                        {
                            case ReleaseStates.All:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.CategoryId == categoryId
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.Released:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.CategoryId == categoryId && x.Released
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.NotReleased:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.CategoryId == categoryId && !x.Released
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        
                        break;
                    case ActiveStates.Active:
                        switch (releasedState)
                        {
                            case ReleaseStates.All:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.CategoryId == categoryId && x.IsActive
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.Released:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.CategoryId == categoryId && x.IsActive && x.Released
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.NotReleased:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.CategoryId == categoryId && x.IsActive && !x.Released
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        
                        break;
                    case ActiveStates.Inactive:
                        switch (releasedState)
                        {
                            case ReleaseStates.All:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.CategoryId == categoryId && !x.IsActive
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.Released:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.CategoryId == categoryId && !x.IsActive && x.Released
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.NotReleased:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.CategoryId == categoryId && !x.IsActive && !x.Released
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        
                        break;
                }


                if (query != null)
                    foreach (DeftenantInfosView tenant in query)
                    {
                        currentTenant = new MinTenantInfo
                        {
                            Id = tenant.Id,
                            TenantId = tenant.TenantId,
                            Name = tenant.Name,
                            IsActive = tenant.IsActive,
                            Logo = tenant.Logo,
                            LogoUrl = tenant.LogoUrl,
                            WhiteLogo = tenant.WhiteLogo,
                            WhiteLogoUrl = tenant.WhiteLogoUrl,
                            CarrouselImgId = tenant.CarrouselImg,
                            CarrouselImgUrl = tenant.CarrouselImgUrl,
                            LandingImg = tenant.LandingImg,
                            CountryId = tenant.CountryId,
                            CountryName = tenant.CountryName,
                            CurrencySymbol = tenant.CurrencySymbol,
                            CurrencyType = tenant.CurrencyType,
                            CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                            CategoryId = tenant.CategoryId,
                            ClassificationId = (Guid)tenant.CommerceClassificationId,
                            PreferenceId = (Guid)tenant.PreferenceId,
                            CategoryName = tenant.CommerceClassificationName,
                            TypeId = tenant.TypeId,
                            InstanceType = tenant.InstanceType,
                            BusinessStructureType = tenant.BusinessStructureType,
                            PayerType = tenant.PayerType,
                            HasMembershipLevels = tenant.HasMembershipLevels,
                            LoyaltyProgramType = tenant.LoyaltyProgramType,
                            Language = tenant.Language,
                            Released = tenant.Released,
                            CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                            CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                            AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                            AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                            CheckInType = tenant.CheckInType,
                            ReferenceCodeType = tenant.ReferenceCodeType,
                            DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                            ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                            DealClaimMethod = tenant.DealsClaimMethod
                        };

                        tenants.Add(currentTenant);

                    }
            }
            catch (Exception e)
            {
                tenants = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenants;
        }

        /// <summary>
        /// Retrieves tenants by country
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public List<MinTenantInfo> Gets(Guid countryId, int activeState, int releasedState, int pageSize, int pageNumber)
        {
            List<MinTenantInfo> tenants = new List<MinTenantInfo>();
            MinTenantInfo currentTenant = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (releasedState)
                        {
                            case ReleaseStates.All:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.Released:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && x.Released
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.NotReleased:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.CountryId == countryId && !x.Released
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        
                        break;
                    case ActiveStates.Active:
                        switch (releasedState)
                        {
                            case ReleaseStates.All:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.IsActive && x.CountryId == countryId
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.Released:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.IsActive && x.Released && x.CountryId == countryId
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.NotReleased:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where x.IsActive && !x.Released && x.CountryId == countryId
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        
                        break;
                    case ActiveStates.Inactive:
                        switch (releasedState)
                        {
                            case ReleaseStates.All:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where !x.IsActive && x.CountryId == countryId
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.Released:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where !x.IsActive && x.Released && x.CountryId == countryId
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ReleaseStates.NotReleased:
                                query = (from x in this._businessObjects.Context.DeftenantInfosView
                                        where !x.IsActive && !x.Released && x.CountryId == countryId
                                        orderby x.Name ascending
                                        select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        
                        break;
                }


                if (query != null)
                    foreach (DeftenantInfosView tenant in query)
                    {
                        currentTenant = new MinTenantInfo
                        {
                            Id = tenant.Id,
                            TenantId = tenant.TenantId,
                            Name = tenant.Name,
                            IsActive = tenant.IsActive,
                            Logo = tenant.Logo,
                            LogoUrl = tenant.LogoUrl,
                            WhiteLogo = tenant.WhiteLogo,
                            WhiteLogoUrl = tenant.WhiteLogoUrl,
                            CarrouselImgId = tenant.CarrouselImg,
                            CarrouselImgUrl = tenant.CarrouselImgUrl,
                            LandingImg = tenant.LandingImg,
                            CountryId = tenant.CountryId,
                            CountryName = tenant.CountryName,
                            CurrencySymbol = tenant.CurrencySymbol,
                            CurrencyType = tenant.CurrencyType,
                            CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                            CategoryId = tenant.CategoryId,
                            ClassificationId = (Guid)tenant.CommerceClassificationId,
                            PreferenceId = (Guid)tenant.PreferenceId,
                            CategoryName = tenant.CommerceCategoryName,
                            TypeId = tenant.TypeId,
                            InstanceType = tenant.InstanceType,
                            BusinessStructureType = tenant.BusinessStructureType,
                            PayerType = tenant.PayerType,
                            HasMembershipLevels = tenant.HasMembershipLevels,
                            LoyaltyProgramType = tenant.LoyaltyProgramType,
                            Language = tenant.Language,
                            Released = tenant.Released,
                            CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                            CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                            AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                            AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                            CheckInType = tenant.CheckInType,
                            ReferenceCodeType = tenant.ReferenceCodeType,
                            DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                            ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                            DealClaimMethod = tenant.DealsClaimMethod
                        };

                        tenants.Add(currentTenant);

                    }
            }
            catch (Exception e)
            {
                tenants = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenants;
        }

        /// <summary>
        /// Retrieve complete tenant information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public TenantInfo Get(Guid id, int keyType)
        {
            TenantInfo currentTenant = null;

            try
            {
                var query = (dynamic) null;

                if(id != Guid.Empty)//IF NOT THE BASE TENANT(GUID.EMPTY IS THE BASE COMMERCE ID
                {
                    switch (keyType)
                    {
                        case CommerceKeys.PrimaryKey:
                            query = from x in this._businessObjects.Context.DeftenantInfosView
                                where x.Id == id
                                select x;
                            break;
                        case CommerceKeys.TenantKey:
                            query = from x in this._businessObjects.Context.DeftenantInfosView
                                where x.TenantId == id
                                select x;
                            break;
                    }


                    if (query != null)
                    {
                        foreach (DeftenantInfosView tenant in query)
                        {
                            currentTenant = new TenantInfo
                            {
                                Id = tenant.Id,
                                TenantId = tenant.TenantId,
                                Name = tenant.Name,
                                LegalName = tenant.LegalName,
                                TaxId = tenant.TaxId,
                                TaxAddress = tenant.TaxAddress,
                                PaymentSubject = tenant.PaymentSubject,
                                AdditionalNotes = tenant.AdditionalNotes,
                                Description = tenant.Description,
                                Logo = tenant.Logo,
                                LogoUrl = tenant.LogoUrl,
                                WhiteLogo = tenant.WhiteLogo,
                                WhiteLogoUrl = tenant.WhiteLogoUrl,
                                CarrouselImgId = tenant.CarrouselImg,
                                CarrouselImgUrl = tenant.CarrouselImgUrl,
                                EmailsBackground = tenant.EmailBg,
                                LandingImg = tenant.LandingImg,
                                CountryId = tenant.CountryId,
                                CountryName = tenant.CountryName,
                                RelevanceStatus = tenant.RelevanceStatus,
                                RelevanceStatusName = this.GetRelevanceStatusName(tenant.RelevanceStatus),
                                CreatedDate = tenant.CreatedDate,
                                UpdatedDate = tenant.UpdatedDate,
                                PaymentDay = tenant.PaymentDay,
                                CurrencySymbol = tenant.CurrencySymbol,
                                CurrencyType = tenant.CurrencyType,
                                CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                                ContactName = tenant.ContactName,
                                ContactEmail = tenant.ContactEmail,
                                ContactPhone = tenant.ContactPhone,
                                Keywords = tenant.Keywords,
                                IsActive = tenant.IsActive,
                                CategoryId = tenant.CategoryId,
                                CategoryName = tenant.CommerceCategoryName,
                                TrialExpiration = tenant.TrialExpiration,
                                DealRules = tenant.DealRules,
                                DealConditions = tenant.DealConditions,
                                IncentiveRules = tenant.IncentiveRules,
                                IncentiveConditions = tenant.IncentiveConditions,
                                InStoreDealClaimInstructions = tenant.InStoreDealClaimInstructions,
                                OnlineDealClaimInstructions = tenant.OnlineDealClaimInstructions,
                                PhoneDealClaimInstructions = tenant.PhoneDealClaimInstructions,
                                IncentiveClaimInstructions = tenant.IncentiveClaimInstructions,
                                TypeId = tenant.TypeId,
                                TypeName = this.GetTypeName(tenant.TypeId),
                                InstanceType = tenant.InstanceType,
                                InstanceTypeName = this.GetInstanceTypeName(tenant.InstanceType),
                                BusinessStructureType = tenant.BusinessStructureType,
                                BusinessStructureTypeName = this.GetBusinessStructureTypeName(tenant.BusinessStructureType),
                                PayerType = tenant.PayerType,
                                PayerTypeName = this.GetPayerTypeName(tenant.PayerType),
                                Language = tenant.Language,
                                LanguageName = this.GetLanguageName(tenant.Language),
                                Website = tenant.Website,
                                Released = tenant.Released,
                                CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                                CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                                HasMembershipLevels = tenant.HasMembershipLevels,
                                LoyaltyProgramType = tenant.LoyaltyProgramType,
                                LoyaltyProgramTypeName = this.GetLoyaltyProgramTypeName(tenant.LoyaltyProgramType),
                                AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                                AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                                CheckInType = tenant.CheckInType,
                                CheckInTypeName = this.GetCheckInTypeName(tenant.CheckInType),
                                ReferenceCodeType = tenant.ReferenceCodeType,
                                ReferenceCodeTypeName = this.GetReferenceCodeTypeName(tenant.ReferenceCodeType),
                                DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                                ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                                DealClaimMethod = tenant.DealsClaimMethod,
                                DealClaimMethodName = this.GetClaimMethodName(tenant.DealsClaimMethod)
                            };

                        }
                    }
                        
                }
                else //IN CASE IT'S THE BASE COMMERCE(ONLY USED FOR MANAGEMENT OBJECTS WHEN THERE IS NO SPECIFIC TENANT
                {
                    switch (keyType)
                    {
                        case CommerceKeys.PrimaryKey:
                            query = from x in this._businessObjects.Context.DeftenantInformations
                                    where x.Id == id
                                    select x;
                            break;
                        case CommerceKeys.TenantKey:
                            query = from x in this._businessObjects.Context.DeftenantInformations
                                    where x.TenantId == id
                                    select x;
                            break;
                    }


                    if (query != null)
                        foreach (DeftenantInformations tenant in query)
                        {
                            currentTenant = new TenantInfo
                            {
                                Id = tenant.Id,
                                TenantId = tenant.TenantId,
                                Name = tenant.Name,
                                LegalName = tenant.LegalName,
                                TaxId = tenant.TaxId,
                                TaxAddress = tenant.TaxAddress,
                                PaymentSubject = tenant.PaymentSubject,
                                AdditionalNotes = tenant.AdditionalNotes,
                                Description = tenant.Description,
                                Logo = tenant.Logo,
                                LogoUrl = tenant.LogoUrl,
                                WhiteLogo = tenant.WhiteLogo,
                                WhiteLogoUrl = tenant.WhiteLogoUrl,
                                CarrouselImgId = tenant.CarrouselImg,
                                CarrouselImgUrl = tenant.CarrouselImgUrl,
                                EmailsBackground = tenant.EmailBg,
                                LandingImg = tenant.LandingImg,
                                CountryId = tenant.CountryId,
                                CountryName = "",//NOT NEEDED
                                RelevanceStatus = TenantRelevanceStatuses.None,
                                RelevanceStatusName = "",
                                CreatedDate = tenant.CreatedDate,
                                UpdatedDate = tenant.UpdatedDate,
                                PaymentDay = tenant.PaymentDay,
                                CurrencySymbol = tenant.CurrencySymbol,
                                CurrencyType = tenant.CurrencyType,
                                CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                                ContactName = tenant.ContactName,
                                ContactEmail = tenant.ContactEmail,
                                ContactPhone = tenant.ContactPhone,
                                Keywords = tenant.Keywords,
                                IsActive = true,//NOT NEEDED
                                CategoryId = tenant.CategoryId,
                                CategoryName = "",//NOT NEEDED
                                TrialExpiration = tenant.TrialExpiration,
                                DealRules = tenant.DealRules,
                                DealConditions = tenant.DealConditions,
                                IncentiveRules = tenant.IncentiveRules,
                                IncentiveConditions = tenant.IncentiveConditions,
                                InStoreDealClaimInstructions = tenant.InStoreDealClaimInstructions,
                                OnlineDealClaimInstructions = tenant.OnlineDealClaimInstructions,
                                PhoneDealClaimInstructions = tenant.PhoneDealClaimInstructions,
                                IncentiveClaimInstructions = tenant.IncentiveClaimInstructions,
                                TypeId = tenant.TypeId,
                                TypeName = this.GetTypeName(tenant.TypeId),
                                InstanceType = TenantInstanceTypes.Core,
                                InstanceTypeName = "-",
                                BusinessStructureType = tenant.BusinessStructureType,
                                BusinessStructureTypeName = this.GetBusinessStructureTypeName(tenant.BusinessStructureType),
                                PayerType = tenant.PayerType,
                                PayerTypeName = this.GetPayerTypeName(tenant.PayerType),
                                Language = tenant.Language,
                                LanguageName = this.GetLanguageName(tenant.Language),
                                Website = tenant.Website,
                                Released = true, //NOT NEEDED
                                CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                                CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                                HasMembershipLevels = tenant.HasMembershipLevels,
                                LoyaltyProgramType = tenant.LoyaltyProgramType,
                                LoyaltyProgramTypeName = this.GetLoyaltyProgramTypeName(tenant.LoyaltyProgramType),
                                AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                                AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                                CheckInType = tenant.CheckInType,
                                CheckInTypeName = this.GetCheckInTypeName(tenant.CheckInType),
                                ReferenceCodeType = tenant.ReferenceCodeType,
                                ReferenceCodeTypeName = this.GetReferenceCodeTypeName(tenant.ReferenceCodeType),
                                DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                                ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                                DealClaimMethod = tenant.DealsClaimMethod,
                                DealClaimMethodName = this.GetClaimMethodName(tenant.DealsClaimMethod)
                            };

                        }
                }
                
            }
            catch (Exception e)
            {
                currentTenant = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return currentTenant;
        }

        /// <summary>
        /// Retrieve complete tenant information
        /// </summary>
        /// <returns></returns>
        public Object Get(int dataStructureType, Guid id, int idType)
        {
            TenantInfo currentTenant = null;
            MinTenantInfo currentTenantMin = null;
            Object data = null;

            try
            {
                var query = (dynamic)null;

                if(id != Guid.Empty)//IF NOT THE BASE TENANT(GUID.EMPTY IS THE BASE COMMERCE ID
                {
                    switch (idType)
                    {
                        case CommerceKeys.PrimaryKey:
                            query = from x in this._businessObjects.Context.DeftenantInfosView
                                    where x.Id == id
                                    select x;
                            break;
                        case CommerceKeys.TenantKey:
                            query = from x in this._businessObjects.Context.DeftenantInfosView
                                    where x.TenantId == id
                                    select x;
                            break;

                    }


                    foreach (DeftenantInfosView tenant in query)
                    {
                        switch (dataStructureType)
                        {
                            case TenantStructureTypes.Complete:
                                currentTenant = new TenantInfo
                                {
                                    Id = tenant.Id,
                                    TenantId = tenant.TenantId,
                                    Name = tenant.Name,
                                    LegalName = tenant.LegalName,
                                    TaxId = tenant.TaxId,
                                    TaxAddress = tenant.TaxAddress,
                                    PaymentSubject = tenant.PaymentSubject,
                                    AdditionalNotes = tenant.AdditionalNotes,
                                    Description = tenant.Description,
                                    Logo = tenant.Logo,
                                    LogoUrl = tenant.LogoUrl,
                                    WhiteLogo = tenant.WhiteLogo,
                                    WhiteLogoUrl = tenant.WhiteLogoUrl,
                                    CarrouselImgId = tenant.CarrouselImg,
                                    CarrouselImgUrl = tenant.CarrouselImgUrl,
                                    EmailsBackground = tenant.EmailBg,
                                    LandingImg = tenant.LandingImg,
                                    CountryId = tenant.CountryId,
                                    CountryName = tenant.CountryName,
                                    RelevanceStatus = tenant.RelevanceStatus,
                                    RelevanceStatusName = this.GetRelevanceStatusName(tenant.RelevanceStatus),
                                    CreatedDate = tenant.CreatedDate,
                                    UpdatedDate = tenant.UpdatedDate,
                                    PaymentDay = tenant.PaymentDay,
                                    CurrencySymbol = tenant.CurrencySymbol,
                                    CurrencyType = tenant.CurrencyType,
                                    CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                                    ContactName = tenant.ContactName,
                                    ContactEmail = tenant.ContactEmail,
                                    ContactPhone = tenant.ContactPhone,
                                    Keywords = tenant.Keywords,
                                    IsActive = tenant.IsActive,
                                    CategoryId = tenant.CategoryId,
                                    CategoryName = tenant.CommerceCategoryName,
                                    TrialExpiration = tenant.TrialExpiration,
                                    DealRules = tenant.DealRules,
                                    DealConditions = tenant.DealConditions,
                                    IncentiveRules = tenant.IncentiveRules,
                                    IncentiveConditions = tenant.IncentiveConditions,
                                    InStoreDealClaimInstructions = tenant.InStoreDealClaimInstructions,
                                    OnlineDealClaimInstructions = tenant.OnlineDealClaimInstructions,
                                    PhoneDealClaimInstructions = tenant.PhoneDealClaimInstructions,
                                    IncentiveClaimInstructions = tenant.IncentiveClaimInstructions,
                                    TypeId = tenant.TypeId,
                                    TypeName = this.GetTypeName(tenant.TypeId),
                                    InstanceType = tenant.InstanceType,
                                    InstanceTypeName = this.GetInstanceTypeName(tenant.InstanceType),
                                    BusinessStructureType = tenant.BusinessStructureType,
                                    BusinessStructureTypeName = this.GetBusinessStructureTypeName(tenant.BusinessStructureType),
                                    PayerType = tenant.PayerType,
                                    PayerTypeName = this.GetPayerTypeName(tenant.PayerType),
                                    Language = tenant.Language,
                                    LanguageName = this.GetLanguageName(tenant.Language),
                                    Website = tenant.Website,
                                    Released = tenant.Released,
                                    CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                                    CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                                    HasMembershipLevels = tenant.HasMembershipLevels,
                                    LoyaltyProgramType = tenant.LoyaltyProgramType,
                                    LoyaltyProgramTypeName = this.GetLoyaltyProgramTypeName(tenant.LoyaltyProgramType),
                                    AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                                    AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                                    CheckInType = tenant.CheckInType,
                                    CheckInTypeName = this.GetCheckInTypeName(tenant.CheckInType),
                                    ReferenceCodeType = tenant.ReferenceCodeType,
                                    ReferenceCodeTypeName = this.GetReferenceCodeTypeName(tenant.ReferenceCodeType),
                                    DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                                    ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                                    DealClaimMethod = tenant.DealsClaimMethod,
                                    DealClaimMethodName = this.GetClaimMethodName(tenant.DealsClaimMethod)
                                };

                                data = currentTenant;
                                break;
                            case TenantStructureTypes.Min:
                                currentTenantMin = new MinTenantInfo
                                {
                                    Id = tenant.Id,
                                    TenantId = tenant.TenantId,
                                    Name = tenant.Name,
                                    IsActive = tenant.IsActive,
                                    Logo = tenant.Logo,
                                    LogoUrl = tenant.LogoUrl,
                                    WhiteLogo = tenant.WhiteLogo,
                                    WhiteLogoUrl = tenant.WhiteLogoUrl,
                                    CarrouselImgId = tenant.CarrouselImg,
                                    CarrouselImgUrl = tenant.CarrouselImgUrl,
                                    LandingImg = tenant.LandingImg,
                                    CurrencySymbol = tenant.CurrencySymbol,
                                    CurrencyType = tenant.CurrencyType,
                                    CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                                    CategoryId = tenant.CategoryId,
                                    ClassificationId = (Guid)tenant.CommerceClassificationId,
                                    PreferenceId = (Guid)tenant.PreferenceId,
                                    CategoryName = tenant.CommerceCategoryName,
                                    CountryId = tenant.CountryId,
                                    CountryName = tenant.CountryName,
                                    TypeId = tenant.TypeId,
                                    InstanceType = tenant.InstanceType,
                                    BusinessStructureType = tenant.BusinessStructureType,
                                    PayerType = tenant.PayerType,
                                    HasMembershipLevels = tenant.HasMembershipLevels,
                                    LoyaltyProgramType = tenant.LoyaltyProgramType,
                                    Language = tenant.Language,
                                    Released = tenant.Released,
                                    CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                                    CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                                    AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                                    AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                                    CheckInType = tenant.CheckInType,
                                    ReferenceCodeType = tenant.ReferenceCodeType,
                                    DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                                    ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                                    DealClaimMethod = tenant.DealsClaimMethod
                                };

                                data = currentTenantMin;
                                break;
                        }

                    }
                }
                else //IN CASE IT'S THE BASE COMMERCE(ONLY USED FOR MANAGEMENT OBJECTS WHEN THERE IS NO SPECIFIC TENANT
                {
                    switch (idType)
                    {
                        case CommerceKeys.PrimaryKey:
                            query = from x in this._businessObjects.Context.DeftenantInformations
                                    where x.Id == id
                                    select x;
                            break;
                        case CommerceKeys.TenantKey:
                            query = from x in this._businessObjects.Context.DeftenantInformations
                                    where x.TenantId == id
                                    select x;
                            break;

                    }


                    foreach (DeftenantInformations tenant in query)
                    {
                        switch (dataStructureType)
                        {
                            case TenantStructureTypes.Complete:
                                currentTenant = new TenantInfo
                                {
                                    Id = tenant.Id,
                                    TenantId = tenant.TenantId,
                                    Name = tenant.Name,
                                    LegalName = tenant.LegalName,
                                    TaxId = tenant.TaxId,
                                    TaxAddress = tenant.TaxAddress,
                                    PaymentSubject = tenant.PaymentSubject,
                                    AdditionalNotes = tenant.AdditionalNotes,
                                    Description = tenant.Description,
                                    Logo = tenant.Logo,
                                    LogoUrl = tenant.LogoUrl,
                                    WhiteLogo = tenant.WhiteLogo,
                                    WhiteLogoUrl = tenant.WhiteLogoUrl,
                                    CarrouselImgId = tenant.CarrouselImg,
                                    CarrouselImgUrl = tenant.CarrouselImgUrl,
                                    EmailsBackground = tenant.EmailBg,
                                    LandingImg = tenant.LandingImg,
                                    CountryId = tenant.CountryId,
                                    CountryName = "",//NOT NEEDED
                                    RelevanceStatus = TenantRelevanceStatuses.None,
                                    RelevanceStatusName = "",
                                    CreatedDate = tenant.CreatedDate,
                                    UpdatedDate = tenant.UpdatedDate,
                                    PaymentDay = tenant.PaymentDay,
                                    CurrencySymbol = tenant.CurrencySymbol,
                                    CurrencyType = tenant.CurrencyType,
                                    CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                                    ContactName = tenant.ContactName,
                                    ContactEmail = tenant.ContactEmail,
                                    ContactPhone = tenant.ContactPhone,
                                    Keywords = tenant.Keywords,
                                    IsActive = true,//NOT NEEDED
                                    CategoryId = tenant.CategoryId,
                                    CategoryName = "",//NOT NEEDED
                                    TrialExpiration = tenant.TrialExpiration,
                                    DealRules = tenant.DealRules,
                                    DealConditions = tenant.DealConditions,
                                    IncentiveRules = tenant.IncentiveRules,
                                    IncentiveConditions = tenant.IncentiveConditions,
                                    InStoreDealClaimInstructions = tenant.InStoreDealClaimInstructions,
                                    OnlineDealClaimInstructions = tenant.OnlineDealClaimInstructions,
                                    PhoneDealClaimInstructions = tenant.PhoneDealClaimInstructions,
                                    IncentiveClaimInstructions = tenant.IncentiveClaimInstructions,
                                    TypeId = tenant.TypeId,
                                    TypeName = this.GetTypeName(tenant.TypeId),
                                    InstanceType = TenantInstanceTypes.Core,
                                    InstanceTypeName = "-",
                                    BusinessStructureType = tenant.BusinessStructureType,
                                    BusinessStructureTypeName = this.GetBusinessStructureTypeName(tenant.BusinessStructureType),
                                    PayerType = tenant.PayerType,
                                    PayerTypeName = this.GetPayerTypeName(tenant.PayerType),
                                    Language = tenant.Language,
                                    LanguageName = this.GetLanguageName(tenant.Language),
                                    Website = tenant.Website,
                                    Released = true,//NOT NEEDED
                                    CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                                    CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                                    HasMembershipLevels = tenant.HasMembershipLevels,
                                    LoyaltyProgramType = tenant.LoyaltyProgramType,
                                    LoyaltyProgramTypeName = this.GetLoyaltyProgramTypeName(tenant.LoyaltyProgramType),
                                    AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                                    AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                                    CheckInType = tenant.CheckInType,
                                    CheckInTypeName = this.GetCheckInTypeName(tenant.CheckInType),
                                    ReferenceCodeType = tenant.ReferenceCodeType,
                                    ReferenceCodeTypeName = this.GetReferenceCodeTypeName(tenant.ReferenceCodeType),
                                    DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                                    ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                                    DealClaimMethod = tenant.DealsClaimMethod,
                                    DealClaimMethodName = this.GetClaimMethodName(tenant.DealsClaimMethod)
                                };

                                data = currentTenant;
                                break;
                            case TenantStructureTypes.Min:
                                currentTenantMin = new MinTenantInfo
                                {
                                    Id = tenant.Id,
                                    TenantId = tenant.TenantId,
                                    Name = tenant.Name,
                                    IsActive = true,//NOT NEEDED
                                    Logo = tenant.Logo,
                                    LogoUrl = tenant.LogoUrl,
                                    WhiteLogo = tenant.WhiteLogo,
                                    WhiteLogoUrl = tenant.WhiteLogoUrl,
                                    CarrouselImgId = tenant.CarrouselImg,
                                    CarrouselImgUrl = tenant.CarrouselImgUrl,
                                    LandingImg = tenant.LandingImg,
                                    CurrencySymbol = tenant.CurrencySymbol,
                                    CurrencyType = tenant.CurrencyType,
                                    CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                                    CategoryId = tenant.CategoryId,
                                    ClassificationId = Guid.Empty,//NOT NEEDED
                                    PreferenceId = Guid.Empty,//NOT NEEDED
                                    CategoryName = "",//NOT NEEDED
                                    CountryId = tenant.CountryId,
                                    CountryName = "",//NOT NEEDED
                                    TypeId = tenant.TypeId,
                                    InstanceType = TenantInstanceTypes.Core,
                                    BusinessStructureType = tenant.BusinessStructureType,
                                    PayerType = tenant.PayerType,
                                    HasMembershipLevels = tenant.HasMembershipLevels,
                                    LoyaltyProgramType = tenant.LoyaltyProgramType,
                                    Language = tenant.Language,
                                    Released = true,//NOT NEEDED
                                    CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                                    CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                                    AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                                    AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                                    CheckInType = tenant.CheckInType,
                                    ReferenceCodeType = tenant.ReferenceCodeType,
                                    DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                                    ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                                    DealClaimMethod = tenant.DealsClaimMethod
                                };

                                data = currentTenantMin;
                                break;
                        }

                    }
                }
                
            }
            catch (Exception e)
            {
                currentTenant = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return data;
        }


        /// <summary>
        /// Creates a new tenant
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public Guid? Post(bool isActive,int instanceType)
        {
            Guid? id;
            try
            {
                Deftenants newTenant = new Deftenants
                {
                    Id = Guid.NewGuid(),
                    InstanceType = instanceType,
                    IsActive = isActive,
                    Released = false//Isn't released because doesn't have any data needed to work as tenant in the platform
                };

                this._businessObjects.Context.Deftenants.Add(newTenant);
                this._businessObjects.Context.SaveChanges();

                id = newTenant.Id;
            }
            catch (Exception e)
            {
                id = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return id;
        }


        /// <summary>
        /// Creates a tenant profile
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="name"></param>
        /// <param name="nameInReceipt"></param>
        /// <param name="rfcInReceipt"></param>
        /// <param name="description"></param>
        /// <param name="keywords"></param>
        /// <param name="countryId"></param>
        /// <param name="paymentDay"></param>
        /// <param name="currencySymbol"></param>
        /// <param name="contactName"></param>
        /// <param name="contactEmail"></param>
        /// <param name="contactPhone"></param>
        /// <param name="categoryId"></param>
        /// <param name="trialExpiration"></param>
        /// <param name="dealRules"></param>
        /// <param name="dealConditions"></param>
        /// <param name="instoreDealClaimInstructions"></param>
        /// <param name="onlineDealClaimInstructions"></param>
        /// <param name="phoneDealClaimInstructions"></param>
        /// <param name="hasMembershipLevels"></param>
        /// <param name="typeId"></param>
        /// <param name="language"></param>
        /// <param name="website"></param>
        /// <param name="campaignTitle"></param>
        /// <param name="campaignMsg"></param>
        /// <param name="loyaltyProgramEnabled"></param>
        /// <param name="acceptsCommunityPointsAsPayment"></param>
        /// <param name="acceptsSelfPointsAsPayment"></param>
        /// <param name="checkInType"></param>
        /// <param name="commissionFeePercentage"></param>
        /// <param name="dealClaimMethod"></param>
        /// <param name="relevanceStatus"></param>
        /// <returns></returns>
        public TenantInfo Post(Guid tenantId, string name, string legalName, string taxId, string taxAddress, string paymentSubject, 
            string notes, string description, string keywords, Guid countryId, int paymentDay, string currencySymbol, int currencyType, string contactName, 
            string contactEmail, string contactPhone, Guid categoryId, DateTime? trialExpiration, string dealRules, string dealConditions, 
            string incentiveRules, string incentiveConditions, string instoreDealClaimInstructions, string onlineDealClaimInstructions, 
            string phoneDealClaimInstructions, string incentiveClaimInstructions, bool hasMembershipLevels, int typeId, int businessStructureType, 
            int payerType, int referenceCodeType, int language, string website, string campaignTitle, string campaignMsg, int loyaltyProgramType, 
            bool acceptsCommunityPointsAsPayment, bool acceptsSelfPointsAsPayment, int checkInType, decimal defaultCommissionFeePercentage, 
            decimal consumerCashbackPercentage, int dealClaimMethod, int relevanceStatus)
        {
            DeftenantInformations info = null;
            TenantInfo tenantInfo = null;

            try
            {
                info = new DeftenantInformations
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    Name = name,
                    LegalName = legalName,
                    TaxId = taxId,
                    TaxAddress = taxAddress,
                    PaymentSubject = paymentSubject,
                    AdditionalNotes = notes,
                    Description = description,
                    Keywords = keywords,
                    CountryId = countryId,
                    RelevanceStatus = relevanceStatus,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    PaymentDay = paymentDay,
                    CurrencySymbol = currencySymbol,
                    CurrencyType = currencyType,
                    ContactEmail = contactEmail,
                    ContactName = contactName,
                    ContactPhone = contactPhone,
                    CategoryId = categoryId,
                    TrialExpiration = trialExpiration,
                    DealRules = dealRules,
                    DealConditions = dealConditions,
                    IncentiveRules = incentiveRules,
                    IncentiveConditions = incentiveConditions,
                    InStoreDealClaimInstructions = instoreDealClaimInstructions,
                    OnlineDealClaimInstructions = onlineDealClaimInstructions,
                    PhoneDealClaimInstructions = phoneDealClaimInstructions,
                    IncentiveClaimInstructions = incentiveClaimInstructions,
                    TypeId = typeId,
                    BusinessStructureType = businessStructureType,
                    PayerType = payerType,
                    Language = language,
                    Website = website,
                    CampaignDefaultTitleMsg = campaignTitle,
                    CampaignDefaultContentMsg = campaignMsg,
                    HasMembershipLevels = hasMembershipLevels,
                    LoyaltyProgramType = loyaltyProgramType,
                    AcceptsCommunityPointsAsPayment = acceptsCommunityPointsAsPayment,
                    AcceptsSelfPointsAsPayment = acceptsCommunityPointsAsPayment,
                    CheckInType = checkInType,
                    ReferenceCodeType = referenceCodeType,
                    DefaultCommissionFeePercentage = defaultCommissionFeePercentage,
                    ConsumerCashbackPercentage = consumerCashbackPercentage,
                    DealsClaimMethod = dealClaimMethod
                };

                this._businessObjects.Context.DeftenantInformations.Add(info);
                this._businessObjects.Context.SaveChanges();

                var query = from x in this._businessObjects.Context.DeftenantInfosView
                            where x.Id == info.Id
                            select x;

                if(query != null)
                {
                    foreach(DeftenantInfosView tenant in query)
                    {
                        tenantInfo = new TenantInfo
                        {
                            Id = tenant.Id,
                            TenantId = tenant.TenantId,
                            Name = tenant.Name,
                            LegalName = tenant.LegalName,
                            TaxId = tenant.TaxId,
                            TaxAddress = tenant.TaxAddress,
                            PaymentSubject = tenant.PaymentSubject,
                            AdditionalNotes = tenant.AdditionalNotes,
                            Description = tenant.Description,
                            Logo = tenant.Logo,
                            LogoUrl = tenant.LogoUrl,
                            WhiteLogo = tenant.WhiteLogo,
                            WhiteLogoUrl = tenant.WhiteLogoUrl,
                            CarrouselImgId = tenant.CarrouselImg,
                            CarrouselImgUrl = tenant.CarrouselImgUrl,
                            EmailsBackground = tenant.EmailBg,
                            LandingImg = tenant.LandingImg,
                            CountryId = tenant.CountryId,
                            CountryName = tenant.CountryName,
                            RelevanceStatus = tenant.RelevanceStatus,
                            RelevanceStatusName = this.GetRelevanceStatusName(tenant.RelevanceStatus),
                            CreatedDate = tenant.CreatedDate,
                            UpdatedDate = tenant.UpdatedDate,
                            PaymentDay = tenant.PaymentDay,
                            CurrencySymbol = tenant.CurrencySymbol,
                            CurrencyType = tenant.CurrencyType,
                            CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                            ContactName = tenant.ContactName,
                            ContactEmail = tenant.ContactEmail,
                            ContactPhone = tenant.ContactPhone,
                            Keywords = tenant.Keywords,
                            IsActive = tenant.IsActive,
                            CategoryId = tenant.CategoryId,
                            CategoryName = tenant.CommerceCategoryName,
                            TrialExpiration = tenant.TrialExpiration,
                            DealRules = tenant.DealRules,
                            DealConditions = tenant.DealConditions,
                            IncentiveRules = tenant.IncentiveRules,
                            IncentiveConditions = tenant.IncentiveConditions,
                            InStoreDealClaimInstructions = tenant.InStoreDealClaimInstructions,
                            OnlineDealClaimInstructions = tenant.OnlineDealClaimInstructions,
                            PhoneDealClaimInstructions = tenant.PhoneDealClaimInstructions,
                            IncentiveClaimInstructions = tenant.IncentiveClaimInstructions,
                            TypeId = tenant.TypeId,
                            TypeName = this.GetTypeName(tenant.TypeId),
                            InstanceType = tenant.InstanceType,
                            InstanceTypeName = this.GetInstanceTypeName(tenant.InstanceType),
                            BusinessStructureType = tenant.BusinessStructureType,
                            BusinessStructureTypeName = this.GetBusinessStructureTypeName(tenant.BusinessStructureType),
                            PayerType = tenant.PayerType,
                            PayerTypeName = this.GetPayerTypeName(tenant.PayerType),
                            Language = tenant.Language,
                            LanguageName = this.GetLanguageName(tenant.Language),
                            Website = tenant.Website,
                            Released = tenant.Released,
                            CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                            CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                            HasMembershipLevels = tenant.HasMembershipLevels,
                            LoyaltyProgramType = tenant.LoyaltyProgramType,
                            LoyaltyProgramTypeName = this.GetLoyaltyProgramTypeName(tenant.LoyaltyProgramType),
                            AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                            AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                            CheckInType = tenant.CheckInType,
                            CheckInTypeName = this.GetCheckInTypeName(tenant.CheckInType),
                            ReferenceCodeType = tenant.ReferenceCodeType,
                            ReferenceCodeTypeName = this.GetReferenceCodeTypeName(tenant.ReferenceCodeType),
                            DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                            ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                            DealClaimMethod = tenant.DealsClaimMethod,
                            DealClaimMethodName = this.GetClaimMethodName(tenant.DealsClaimMethod)
                        };
                    }
                }
                
            }
            catch (Exception e)
            {
                tenantInfo = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenantInfo;
        }


        /// <summary>
        /// Updates a commerce profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="keywords"></param>
        /// <param name="countryId"></param>
        /// <param name="paymentDay"></param>
        /// <param name="currencySymbol"></param>
        /// <param name="contactName"></param>
        /// <param name="contactEmail"></param>
        /// <param name="contactPhone"></param>
        /// <param name="website"></param>
        /// <param name="categoryId"></param>
        /// <param name="trialExpiration"></param>
        /// <param name="dealRules"></param>
        /// <param name="dealConditions"></param>
        /// <param name="instoreDealClaimInstructions"></param>
        /// <param name="onlineDealClaimInstructions"></param>
        /// <param name="phoneDealClaimInstructions"></param>
        /// <param name="hasMembershipLevel"></param>
        /// <param name="typeId"></param>
        /// <param name="campaignTitle"></param>
        /// <param name="campaignMsg"></param>
        /// <param name="loyaltyProgramEnabled"></param>
        /// <param name="acceptsCommunityPointsAsPayment"></param>
        /// <param name="acceptsSelfPointsAsPayment"></param>
        /// <param name="pointsRedemptionType"></param>
        /// <param name="dealClaimMethod"></param>
        /// <returns></returns>
        public TenantInfo Put(Guid id, string name, string nameInReceipt, string taxIdInReceipt, string description, string keywords, Guid countryId, int paymentDay,
            string currencySymbol, int currencyType, string contactName, string contactEmail, string contactPhone, string website, Guid categoryId, DateTime? trialExpiration, 
            string dealRules, string dealConditions, string incentiveRules, string incentiveConditions, string instoreDealClaimInstructions, string onlineDealClaimInstructions, 
            string phoneDealClaimInstructions, string incentiveClaimInstructions, bool hasMembershipLevel, int typeId, int businessStructureType, int payerType, 
            int referenceCodeType, string campaignTitle, string campaignMsg, int loyaltyProgramType, bool acceptsCommunityPointsAsPayment, bool acceptsSelfPointsAsPayment, 
            int checkInType, decimal defaultCommissionFeePercentage, decimal consumerCashbackPercentage, int dealClaimMethod, int relevanceStatus)
        {
            DeftenantInformations info = null;
            TenantInfo tenantInfo = null;

            try
            {
                var query = from x in this._businessObjects.Context.DeftenantInformations
                            where x.Id == id
                            select x;

                foreach(DeftenantInformations item in query)
                {
                    info = item;
                }

                if(info != null)
                {
                    info.Name = name;
                    info.Description = description;
                    info.Keywords = keywords;
                    info.CountryId = countryId;
                    info.PaymentDay = paymentDay;
                    info.CurrencySymbol = currencySymbol;
                    info.CurrencyType = currencyType;
                    info.ContactEmail = contactEmail;
                    info.ContactName = contactName;
                    info.ContactPhone = contactPhone;
                    info.Website = website;
                    info.CategoryId = categoryId;
                    info.TrialExpiration = trialExpiration;
                    info.DealRules = dealRules;
                    info.DealConditions = dealConditions;
                    info.IncentiveRules = incentiveRules;
                    info.IncentiveConditions = incentiveConditions;
                    info.InStoreDealClaimInstructions = instoreDealClaimInstructions;
                    info.OnlineDealClaimInstructions = onlineDealClaimInstructions;
                    info.PhoneDealClaimInstructions = phoneDealClaimInstructions;
                    info.IncentiveClaimInstructions = incentiveClaimInstructions;
                    info.TypeId = typeId;
                    info.BusinessStructureType = businessStructureType;
                    info.PayerType = payerType;
                    info.CampaignDefaultTitleMsg = campaignTitle;
                    info.CampaignDefaultContentMsg = campaignMsg;
                    info.HasMembershipLevels = hasMembershipLevel;
                    info.LoyaltyProgramType = loyaltyProgramType;
                    info.AcceptsCommunityPointsAsPayment = acceptsCommunityPointsAsPayment;
                    info.AcceptsSelfPointsAsPayment = acceptsSelfPointsAsPayment;
                    info.CheckInType = checkInType;
                    info.ReferenceCodeType = referenceCodeType;
                    info.DefaultCommissionFeePercentage = defaultCommissionFeePercentage;
                    info.ConsumerCashbackPercentage = consumerCashbackPercentage;
                    info.DealsClaimMethod = dealClaimMethod;
                    info.RelevanceStatus = relevanceStatus;
                    info.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.DeftenantInformations.Add(info);
                    this._businessObjects.Context.SaveChanges();

                    var queryView = from x in this._businessObjects.Context.DeftenantInfosView
                                    where x.Id == info.Id
                                    select x;

                    if (query != null)
                    {
                        foreach (DeftenantInfosView tenant in queryView)
                        {
                            tenantInfo = new TenantInfo
                            {
                                Id = tenant.Id,
                                TenantId = tenant.TenantId,
                                Name = tenant.Name,
                                LegalName = tenant.LegalName,
                                TaxId = tenant.TaxId,
                                TaxAddress = tenant.TaxAddress,
                                PaymentSubject = tenant.PaymentSubject,
                                AdditionalNotes = tenant.AdditionalNotes,
                                Description = tenant.Description,
                                Logo = tenant.Logo,
                                LogoUrl = tenant.LogoUrl,
                                WhiteLogo = tenant.WhiteLogo,
                                WhiteLogoUrl = tenant.WhiteLogoUrl,
                                CarrouselImgId = tenant.CarrouselImg,
                                CarrouselImgUrl = tenant.CarrouselImgUrl,
                                EmailsBackground = tenant.EmailBg,
                                LandingImg = tenant.LandingImg,
                                CountryId = tenant.CountryId,
                                CountryName = tenant.CountryName,
                                RelevanceStatus = tenant.RelevanceStatus,
                                RelevanceStatusName = this.GetRelevanceStatusName(tenant.RelevanceStatus),
                                CreatedDate = tenant.CreatedDate,
                                UpdatedDate = tenant.UpdatedDate,
                                PaymentDay = tenant.PaymentDay,
                                CurrencySymbol = tenant.CurrencySymbol,
                                CurrencyType = tenant.CurrencyType,
                                CurrencyTypeName = this.GetCurrencyTypeName(tenant.CurrencyType),
                                ContactName = tenant.ContactName,
                                ContactEmail = tenant.ContactEmail,
                                ContactPhone = tenant.ContactPhone,
                                Keywords = tenant.Keywords,
                                IsActive = tenant.IsActive,
                                CategoryId = tenant.CategoryId,
                                CategoryName = tenant.CommerceCategoryName,
                                TrialExpiration = tenant.TrialExpiration,
                                DealRules = tenant.DealRules,
                                DealConditions = tenant.DealConditions,
                                IncentiveRules = tenant.IncentiveRules,
                                IncentiveConditions = tenant.IncentiveConditions,
                                InStoreDealClaimInstructions = tenant.InStoreDealClaimInstructions,
                                OnlineDealClaimInstructions = tenant.OnlineDealClaimInstructions,
                                PhoneDealClaimInstructions = tenant.PhoneDealClaimInstructions,
                                IncentiveClaimInstructions = tenant.IncentiveClaimInstructions,
                                TypeId = tenant.TypeId,
                                TypeName = this.GetTypeName(tenant.TypeId),
                                InstanceType = tenant.InstanceType,
                                InstanceTypeName = this.GetInstanceTypeName(tenant.InstanceType),
                                BusinessStructureType = tenant.BusinessStructureType,
                                BusinessStructureTypeName = this.GetBusinessStructureTypeName(tenant.BusinessStructureType),
                                PayerType = tenant.PayerType,
                                PayerTypeName = this.GetPayerTypeName(tenant.PayerType),
                                Language = tenant.Language,
                                LanguageName = this.GetLanguageName(tenant.Language),
                                Website = tenant.Website,
                                Released = tenant.Released,
                                CampaignDefaultTitleMsg = tenant.CampaignDefaultTitleMsg,
                                CampaignDefaultContentMsg = tenant.CampaignDefaultContentMsg,
                                HasMembershipLevels = tenant.HasMembershipLevels,
                                LoyaltyProgramType = tenant.LoyaltyProgramType,
                                LoyaltyProgramTypeName = this.GetLoyaltyProgramTypeName(tenant.LoyaltyProgramType),
                                AcceptsCommunityPointsAsPayment = tenant.AcceptsCommunityPointsAsPayment,
                                AcceptsSelfPointsAsPayment = tenant.AcceptsSelfPointsAsPayment,
                                CheckInType = tenant.CheckInType,
                                CheckInTypeName = this.GetCheckInTypeName(tenant.CheckInType),
                                ReferenceCodeType = tenant.ReferenceCodeType,
                                ReferenceCodeTypeName = this.GetReferenceCodeTypeName(tenant.ReferenceCodeType),
                                DefaultCommissionFeePercentage = tenant.DefaultCommissionFeePercentage,
                                ConsumerCashbackPercentage = tenant.ConsumerCashbackPercentage,
                                DealClaimMethod = tenant.DealsClaimMethod,
                                DealClaimMethodName = this.GetClaimMethodName(tenant.DealsClaimMethod)
                            };
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                tenantInfo = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenantInfo;
        }

        /// <summary>
        /// Changes tenant's active state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObjectStateUpdate Put(Guid id, int changeType)
        {
            ObjectStateUpdate result = new ObjectStateUpdate();

            try
            {
                var query = from x in this._businessObjects.Context.Deftenants
                            where x.Id == id
                            select x;

                Deftenants tenant = null;
                foreach (Deftenants item in query)
                {
                    tenant = item;
                }

                if (tenant != null)
                {
                    switch (changeType)
                    {
                        case ChangeTypes.ActiveState:

                            tenant.IsActive = !tenant.IsActive;
                            //this._businessObjects.Context.Deftenants.Update(tenant);
                            this._businessObjects.Context.SaveChanges();

                            result.NewState = (bool)tenant.IsActive;
                            result.Success = true;
                            break;
                        case ChangeTypes.ReleasedState:

                            tenant.Released = !tenant.Released;
                            this._businessObjects.Context.SaveChanges();

                            result.NewState = tenant.Released;
                            result.Success = true;
                            break;
                    }
                    
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return result;
        }

        /// <summary>
        /// Sets tenant's images
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imgId"></param>
        /// <param name="imgType"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public Guid? Put(Guid id, Guid imgId, string imgUrl, int imgType)
        {
            Guid? currentImgId = null;

            try
            {
                var query = from x in this._businessObjects.Context.DeftenantInformations
                            where x.Id == id
                            select x;

                DeftenantInformations tenant = null;
                
                foreach (DeftenantInformations item in query)
                {
                    tenant = item;
                }

                if (tenant != null)
                {
                    switch (imgType)
                    {
                        case TenantImgTypes.Logo:

                            currentImgId = tenant.Logo;
                            tenant.Logo = imgId;
                            tenant.LogoUrl = imgUrl;
                            tenant.UpdatedDate = DateTime.UtcNow;

                            break;
                        case TenantImgTypes.LandingImg:

                            currentImgId = tenant.LandingImg;
                            tenant.LandingImg = imgId;
                            tenant.UpdatedDate = DateTime.UtcNow;

                            break;
                        case TenantImgTypes.EmailBackground:

                            currentImgId = tenant.EmailBg;
                            tenant.EmailBg = imgId;
                            tenant.UpdatedDate = DateTime.UtcNow;

                            break;
                        case TenantImgTypes.CarrousedImg:

                            currentImgId = tenant.CarrouselImg;
                            tenant.CarrouselImg = imgId;
                            tenant.CarrouselImgUrl = imgUrl;
                            tenant.UpdatedDate = DateTime.UtcNow;

                            break;
                        case TenantImgTypes.WhiteLogo:

                            currentImgId = tenant.WhiteLogo;
                            tenant.WhiteLogo = imgId;
                            tenant.WhiteLogoUrl = imgUrl;
                            tenant.UpdatedDate = DateTime.UtcNow;

                            break;
                    }

                    this._businessObjects.Context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                currentImgId = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return currentImgId;
        }

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                Deftenants currentTenant = (from x in this._businessObjects.Context.Deftenants
                                            where x.Id == id
                                            select x).FirstOrDefault();

                if (currentTenant != null)
                {
                    currentTenant.Deleted = true;
                    currentTenant.UpdatedDate = DateTime.UtcNow;

                    DeftenantInformations currentInfo = (from x in this._businessObjects.Context.DeftenantInformations
                                                        where x.TenantId == id
                                                        select x).FirstOrDefault();

                    if(currentInfo != null)
                    {
                        currentInfo.Deleted = true;
                        currentInfo.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();
                    }

                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
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
        /// Creates a new TenantManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public TenantManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD FILE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
