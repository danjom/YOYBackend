using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class EarningsIncreaserManager
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

        #region METHODS

        public string GetTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case EarningsIncreaserTypes.Base:
                    typeName = Resources.Base;

                    break;
                case EarningsIncreaserTypes.Special:
                    typeName = Resources.Special;

                    break;
                case EarningsIncreaserTypes.Promotional:
                    typeName = Resources.Promotional;

                    break;
                case EarningsIncreaserTypes.Premium:
                    typeName = Resources.Premium;

                    break;
            }

            return typeName;

        }

        public string GetAccessTypeName(int accessType)
        {
            string typeName = "";

            switch (accessType)
            {
                case EarningsIncreaserAccessTypes.OnPayment:
                    typeName = Resources.OnPayment;

                    break;
                case EarningsIncreaserAccessTypes.OnPurchase:
                    typeName = Resources.OnPurchase;

                    break;
            }

            return typeName;

        }


        public string GetUnlockerTypeName(int unlockerType)
        {
            string typeName = "";

            switch (unlockerType)
            {
                case EarningsIncreaserUnlockerTypes.OpenAccess:
                    typeName = Resources.OpenAccess;

                    break;
                case EarningsIncreaserUnlockerTypes.RewardRedemption:
                    typeName = Resources.RewardRedemption;

                    break;
            }

            return typeName;

        }


        public string GetIncreaserFactorTypeName(int unlockerType)
        {
            string typeName = "";

            switch (unlockerType)
            {
                case EarningsIncreaserFactorTypes.Percentage:
                    typeName = Resources.Percentage;

                    break;
                case EarningsIncreaserFactorTypes.FixedAmount:
                    typeName = Resources.FixedAmount;

                    break;
            }

            return typeName;

        }


        private string GetLevelName(int level)
        {
            string levelName = "";

            switch (level)
            {
                case MembershipLevels.Bronze:
                    levelName = Resources.Bronze;
                    break;
                case MembershipLevels.Silver:
                    levelName = Resources.Silver;
                    break;
                case MembershipLevels.Gold:
                    levelName = Resources.Gold;
                    break;
                case MembershipLevels.Platinum:
                    levelName = Resources.Platinum;
                    break;
                case MembershipLevels.Diamond:
                    levelName = Resources.Diamond;
                    break;
            }

            return levelName;
        }


        private string GetUpperLimitTypeName(int limitType)
        {
            string limitTypeName = "";

            switch (limitType)
            {
                case EarningsLimitTypes.ByPercentage:
                    limitTypeName = Resources.ByPercentage;
                    break;
                case EarningsLimitTypes.ByDirectAmount:
                    limitTypeName = Resources.ByDirectAmount;
                    break;
                case EarningsLimitTypes.ByTotalAmount:
                    limitTypeName = Resources.ByTotalAmount;
                    break;
            }

            return limitTypeName;
        }

        public List<EarningsIncreaser> Gets(Guid? tenantId, int type, int accessType, int unlockerType, int factorType, int releaseState, int expireState, DateTime date, int pageSize, int pageNumber)
        {
            List<EarningsIncreaser> earningsIncreasers = null;

            try
            {
                var query = (dynamic)null;

                if (tenantId != null)
                {
                    if (type != EarningsIncreaserTypes.All)
                    {
                        if (accessType != EarningsIncreaserAccessTypes.All)
                        {
                            if (unlockerType != EarningsIncreaserUnlockerTypes.All)
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }

                                }
                            }
                            else
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.AccessType == accessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (unlockerType != EarningsIncreaserUnlockerTypes.All)
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (accessType != EarningsIncreaserAccessTypes.All)
                        {
                            if (unlockerType != EarningsIncreaserUnlockerTypes.All)
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.AccessType == accessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (unlockerType != EarningsIncreaserUnlockerTypes.All)
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:

                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:

                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ProviderTenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (type != EarningsIncreaserTypes.All)
                    {
                        if (accessType != EarningsIncreaserAccessTypes.All)
                        {
                            if (unlockerType != EarningsIncreaserUnlockerTypes.All)
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }

                                }
                            }
                            else
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.AccessType == accessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (unlockerType != EarningsIncreaserUnlockerTypes.All)
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.Type == type && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (accessType != EarningsIncreaserAccessTypes.All)
                        {
                            if (unlockerType != EarningsIncreaserUnlockerTypes.All)
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.AccessType == accessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (unlockerType != EarningsIncreaserUnlockerTypes.All)
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.UnlockerType == unlockerType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (factorType != EarningsIncreaserFactorTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.IncreaserFactorType == factorType
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.IncreaserFactorType == factorType && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.IncreaserFactorType == factorType && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:

                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.IncreaserFactorType == factorType && x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.IncreaserFactorType == factorType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:

                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.IncreaserFactorType == factorType && x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.IncreaserFactorType == factorType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ReleaseDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ReleaseDate > date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ReleaseDate > date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expireState)
                                            {
                                                case ExpiredStates.All:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ReleaseDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ReleaseDate <= date && x.ExpirationDate > date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = (from x in this._businessObjects.Context.DefearningsIncreasers
                                                             where x.ReleaseDate <= date && x.ExpirationDate <= date
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (query != null)
                {
                    earningsIncreasers = new List<EarningsIncreaser>();
                    EarningsIncreaser earningsIncreaser;

                    foreach (DefearningsIncreasers item in query)
                    {
                        earningsIncreaser = new EarningsIncreaser
                        {
                            Id = item.Id,
                            ProviderTenantId = item.ProviderTenantId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            AccessType = item.AccessType,
                            AccessTypeName = GetAccessTypeName(item.AccessType),
                            UnlockerId = item.UnlockerId,
                            UnlockerType = item.UnlockerType,
                            UnlockerTypeName = GetUnlockerTypeName(item.UnlockerType),
                            IncreaserFactor = item.IncreaserFactor,
                            IncreaserFactorType = item.IncreaserFactorType,
                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                            IncreaserFactorTypeName = GetIncreaserFactorTypeName(item.IncreaserFactorType),
                            UpperEarningsLimitType = item.UpperEarningsLimitType,
                            UpperEarningsLimitTypeName = GetUpperLimitTypeName(item.UpperEarningsLimitType),
                            UpperEarningsLimit = item.UpperEarningsLimit,
                            MinLevel = item.MinLevel,
                            MinLevelName = GetLevelName(item.MinLevel),
                            IsActive = item.IsActive,
                            OneTimeUsage = item.OneTimeUsage,
                            ValidMonthDays = item.ValidMonthDays,
                            ValidWeekDays = item.ValidWeekDays,
                            ValidHours = item.ValidHours,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        earningsIncreasers.Add(earningsIncreaser);
                    }
                }
            }
            catch (Exception e)
            {
                earningsIncreasers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return earningsIncreasers;
        }

        /// <summary>
        /// Retrieve all enabled earnings increasers for a given tenant on a given date
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="minLevel"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<EarningsIncreaser> Gets(Guid tenantId, int level, DateTime date)
        {
            List<EarningsIncreaser> earningsIncreasers = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefearningsIncreasers
                            where x.MinLevel <= level && x.ProviderTenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate > date
                            select x;

                if (query != null)
                {
                    earningsIncreasers = new List<EarningsIncreaser>();
                    EarningsIncreaser earningsIncreaser;

                    foreach (DefearningsIncreasers item in query)
                    {
                        earningsIncreaser = new EarningsIncreaser
                        {
                            Id = item.Id,
                            ProviderTenantId = item.ProviderTenantId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            AccessType = item.AccessType,
                            AccessTypeName = GetAccessTypeName(item.AccessType),
                            UnlockerId = item.UnlockerId,
                            UnlockerType = item.UnlockerType,
                            UnlockerTypeName = GetUnlockerTypeName(item.UnlockerType),
                            IncreaserFactor = item.IncreaserFactor,
                            IncreaserFactorType = item.IncreaserFactorType,
                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                            IncreaserFactorTypeName = GetIncreaserFactorTypeName(item.IncreaserFactorType),
                            UpperEarningsLimitType = item.UpperEarningsLimitType,
                            UpperEarningsLimitTypeName = GetUpperLimitTypeName(item.UpperEarningsLimitType),
                            UpperEarningsLimit = item.UpperEarningsLimit,
                            MinLevel = item.MinLevel,
                            MinLevelName = GetLevelName(item.MinLevel),
                            IsActive = item.IsActive,
                            OneTimeUsage = item.OneTimeUsage,
                            ValidMonthDays = item.ValidMonthDays,
                            ValidWeekDays = item.ValidWeekDays,
                            ValidHours = item.ValidHours,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        earningsIncreasers.Add(earningsIncreaser);
                    }
                }
            }
            catch (Exception e)
            {
                earningsIncreasers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return earningsIncreasers;
        }

        public EarningsIncreaser Get(Guid id)
        {
            EarningsIncreaser earningsIncreaser = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefearningsIncreasers
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (DefearningsIncreasers item in query)
                    {
                        earningsIncreaser = new EarningsIncreaser
                        {
                            Id = item.Id,
                            ProviderTenantId = item.ProviderTenantId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            AccessType = item.AccessType,
                            AccessTypeName = GetAccessTypeName(item.AccessType),
                            UnlockerId = item.UnlockerId,
                            UnlockerType = item.UnlockerType,
                            UnlockerTypeName = GetUnlockerTypeName(item.UnlockerType),
                            IncreaserFactor = item.IncreaserFactor,
                            IncreaserFactorType = item.IncreaserFactorType,
                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                            IncreaserFactorTypeName = GetIncreaserFactorTypeName(item.IncreaserFactorType),
                            UpperEarningsLimitType = item.UpperEarningsLimitType,
                            UpperEarningsLimitTypeName = GetUpperLimitTypeName(item.UpperEarningsLimitType),
                            UpperEarningsLimit = item.UpperEarningsLimit,
                            MinLevel = item.MinLevel,
                            MinLevelName = GetLevelName(item.MinLevel),
                            IsActive = item.IsActive,
                            OneTimeUsage = item.OneTimeUsage,
                            ValidMonthDays = item.ValidMonthDays,
                            ValidWeekDays = item.ValidWeekDays,
                            ValidHours = item.ValidHours,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                earningsIncreaser = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return earningsIncreaser;
        }

        public EarningsIncreaser Post(Guid providerTenantId, int type, int accessType, Guid? unlockerId, int unlockerType, decimal increaseFactor, int increaseFactorType, decimal purchasedAmountBlock, decimal upperEarningsLimit, int upperEarningsLimitType, int minLevel, bool oneTimeUsage, string validMonthDays, string validWeekDays, string validHours, DateTime releaseDate, DateTime expirationDate)
        {
            EarningsIncreaser earningsIncreaser;
            try
            {
                DefearningsIncreasers newEarningsIncreaser = new DefearningsIncreasers
                {
                    Id = Guid.NewGuid(),
                    ProviderTenantId = providerTenantId,
                    Type = type,
                    AccessType = accessType,
                    UnlockerId = unlockerId,
                    UnlockerType = unlockerType,
                    IncreaserFactor = increaseFactor,
                    IncreaserFactorType = increaseFactorType,
                    PurchasedAmountBlock = purchasedAmountBlock,
                    UpperEarningsLimit = upperEarningsLimit,
                    UpperEarningsLimitType = upperEarningsLimitType,
                    MinLevel = minLevel,
                    ValidMonthDays = validMonthDays,
                    ValidWeekDays = validWeekDays,
                    ValidHours = validHours,
                    OneTimeUsage = oneTimeUsage,
                    IsActive = true,
                    ReleaseDate = releaseDate,
                    ExpirationDate = expirationDate,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow

                };

                this._businessObjects.Context.DefearningsIncreasers.Add(newEarningsIncreaser);
                this._businessObjects.Context.SaveChanges();

                earningsIncreaser = new EarningsIncreaser
                {
                    Id = newEarningsIncreaser.Id,
                    ProviderTenantId = newEarningsIncreaser.ProviderTenantId,
                    Type = newEarningsIncreaser.Type,
                    TypeName = GetTypeName(newEarningsIncreaser.Type),
                    AccessType = newEarningsIncreaser.AccessType,
                    AccessTypeName = GetAccessTypeName(newEarningsIncreaser.AccessType),
                    UnlockerId = newEarningsIncreaser.UnlockerId,
                    UnlockerType = newEarningsIncreaser.UnlockerType,
                    UnlockerTypeName = GetUnlockerTypeName(newEarningsIncreaser.UnlockerType),
                    IncreaserFactor = newEarningsIncreaser.IncreaserFactor,
                    IncreaserFactorType = newEarningsIncreaser.IncreaserFactorType,
                    PurchasedAmountBlock = newEarningsIncreaser.PurchasedAmountBlock,
                    IncreaserFactorTypeName = GetIncreaserFactorTypeName(newEarningsIncreaser.IncreaserFactorType),
                    UpperEarningsLimitType = newEarningsIncreaser.UpperEarningsLimitType,
                    UpperEarningsLimitTypeName = GetUpperLimitTypeName(newEarningsIncreaser.UpperEarningsLimitType),
                    UpperEarningsLimit = newEarningsIncreaser.UpperEarningsLimit,
                    MinLevel = newEarningsIncreaser.MinLevel,
                    MinLevelName = GetLevelName(newEarningsIncreaser.MinLevel),
                    IsActive = newEarningsIncreaser.IsActive,
                    OneTimeUsage = newEarningsIncreaser.OneTimeUsage,
                    ValidMonthDays = newEarningsIncreaser.ValidMonthDays,
                    ValidWeekDays = newEarningsIncreaser.ValidWeekDays,
                    ValidHours = newEarningsIncreaser.ValidHours,
                    CreatedDate = newEarningsIncreaser.CreatedDate,
                    UpdatedDate = newEarningsIncreaser.UpdatedDate,
                    ReleaseDate = newEarningsIncreaser.ReleaseDate,
                    ExpirationDate = newEarningsIncreaser.ExpirationDate
                };
            }
            catch (Exception e)
            {
                earningsIncreaser = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return earningsIncreaser;
        }

        public EarningsIncreaser Put(Guid id, int type, int accessType, Guid? unlockerId, int unlockerType, decimal increaserFactor, int increaserFactorType, decimal purchasedAmountBlock, decimal upperEarningsLimit, int upperEarningsLimitType, int minLevel, bool oneTimeUsage, string validMonthDays, string validWeekDays, string validHours, DateTime releaseDate, DateTime expirationDate)
        {
            EarningsIncreaser earningsIncreaser = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefearningsIncreasers
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefearningsIncreasers currentEarningsIncreaser = null;

                    foreach (DefearningsIncreasers item in query)
                    {
                        currentEarningsIncreaser = item;
                    }

                    if (currentEarningsIncreaser != null)
                    {
                        currentEarningsIncreaser.Type = type;
                        currentEarningsIncreaser.AccessType = accessType;
                        currentEarningsIncreaser.UnlockerId = unlockerId;
                        currentEarningsIncreaser.UnlockerType = unlockerType;
                        currentEarningsIncreaser.IncreaserFactor = increaserFactor;
                        currentEarningsIncreaser.IncreaserFactorType = increaserFactorType;
                        currentEarningsIncreaser.PurchasedAmountBlock = purchasedAmountBlock;
                        currentEarningsIncreaser.UpperEarningsLimit = upperEarningsLimit;
                        currentEarningsIncreaser.UpperEarningsLimitType = upperEarningsLimitType;
                        currentEarningsIncreaser.MinLevel = minLevel;
                        currentEarningsIncreaser.OneTimeUsage = oneTimeUsage;
                        currentEarningsIncreaser.ValidMonthDays = validMonthDays;
                        currentEarningsIncreaser.ValidWeekDays = validWeekDays;
                        currentEarningsIncreaser.ValidHours = validHours;
                        currentEarningsIncreaser.ReleaseDate = releaseDate;
                        currentEarningsIncreaser.ExpirationDate = expirationDate;
                        currentEarningsIncreaser.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        earningsIncreaser = new EarningsIncreaser
                        {
                            Id = currentEarningsIncreaser.Id,
                            ProviderTenantId = currentEarningsIncreaser.ProviderTenantId,
                            Type = currentEarningsIncreaser.Type,
                            TypeName = GetTypeName(currentEarningsIncreaser.Type),
                            AccessType = currentEarningsIncreaser.AccessType,
                            AccessTypeName = GetAccessTypeName(currentEarningsIncreaser.AccessType),
                            UnlockerId = currentEarningsIncreaser.UnlockerId,
                            UnlockerType = currentEarningsIncreaser.UnlockerType,
                            UnlockerTypeName = GetUnlockerTypeName(currentEarningsIncreaser.UnlockerType),
                            IncreaserFactor = currentEarningsIncreaser.IncreaserFactor,
                            IncreaserFactorType = currentEarningsIncreaser.IncreaserFactorType,
                            PurchasedAmountBlock = currentEarningsIncreaser.PurchasedAmountBlock,
                            IncreaserFactorTypeName = GetIncreaserFactorTypeName(currentEarningsIncreaser.IncreaserFactorType),
                            UpperEarningsLimitType = currentEarningsIncreaser.UpperEarningsLimitType,
                            UpperEarningsLimitTypeName = GetUpperLimitTypeName(currentEarningsIncreaser.UpperEarningsLimitType),
                            UpperEarningsLimit = currentEarningsIncreaser.UpperEarningsLimit,
                            MinLevel = currentEarningsIncreaser.MinLevel,
                            MinLevelName = GetLevelName(currentEarningsIncreaser.MinLevel),
                            IsActive = currentEarningsIncreaser.IsActive,
                            OneTimeUsage = currentEarningsIncreaser.OneTimeUsage,
                            ValidMonthDays = currentEarningsIncreaser.ValidMonthDays,
                            ValidWeekDays = currentEarningsIncreaser.ValidWeekDays,
                            ValidHours = currentEarningsIncreaser.ValidHours,
                            CreatedDate = currentEarningsIncreaser.CreatedDate,
                            UpdatedDate = currentEarningsIncreaser.UpdatedDate,
                            ReleaseDate = currentEarningsIncreaser.ReleaseDate,
                            ExpirationDate = currentEarningsIncreaser.ExpirationDate
                        };
                    }
                }

            }
            catch (Exception e)
            {
                earningsIncreaser = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return earningsIncreaser;
        }

        public bool Put(Guid id, int changeType)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefearningsIncreasers
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefearningsIncreasers currentEarningsIncreaser = null;

                    foreach (DefearningsIncreasers item in query)
                    {
                        currentEarningsIncreaser = item;
                    }

                    if (currentEarningsIncreaser != null)
                    {
                        switch (changeType)
                        {
                            case ChangeTypes.ActiveState:
                                currentEarningsIncreaser.IsActive = !currentEarningsIncreaser.IsActive;
                                currentEarningsIncreaser.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();
                                success = true;
                                break;
                            case ChangeTypes.OneTimeClaim:
                                currentEarningsIncreaser.OneTimeUsage = !currentEarningsIncreaser.OneTimeUsage;
                                currentEarningsIncreaser.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();
                                success = true;
                                break;
                        }
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

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefearningsIncreasers
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefearningsIncreasers currentEarningsIncreaser = null;

                    foreach (DefearningsIncreasers item in query)
                    {
                        currentEarningsIncreaser = item;
                    }

                    if (currentEarningsIncreaser != null)
                    {
                        this._businessObjects.Context.DefearningsIncreasers.Remove(currentEarningsIncreaser);
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

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new EarningsIncreaserManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public EarningsIncreaserManager(BusinessObjects businessObjects)
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
