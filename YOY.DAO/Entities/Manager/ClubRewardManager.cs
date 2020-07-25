using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.Reward;
using YOY.Values;
using YOY.Values.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class ClubRewardManager
    {
        #region PRIVATE_PROPERTIES

        // ------------------------------------------------------------------------------------------------------------------------------ //
        // CLASS PRIVATE PROPERTIES                                                                                                       //
        // ------------------------------------------------------------------------------------------------------------------------------ //

        private BusinessObjects _businessObjects;

        // ------------------------------------------------------------------------------------------------------------------------------ //
        // CLASS PUBLIC PROPERTIES                                                                                                        //
        // ------------------------------------------------------------------------------------------------------------------------------ //

        #endregion

        // ------------------------------------------------------------------------------------------------------------------------------ //
        // CLASS PUBLIC METHODS                                                                                                           //
        // ------------------------------------------------------------------------------------------------------------------------------ //

        #region REWARDS

        private string GetPublishState(DateTime releaseDate, DateTime? expirationDate, DateTime refDate)
        {
            string publishState;
            if (releaseDate > refDate)
            {
                publishState = Resources.NotReleased;
            }
            else
            {
                publishState = refDate > expirationDate ? Resources.Expired : Resources.Released;
            }

            return publishState;
        }

        private string GetMembershipLevelName(int level)
        {
            string levelName = level switch
            {
                MembershipLevels.Bronze => Resources.Bronze,
                MembershipLevels.Silver => Resources.Silver,
                MembershipLevels.Gold => Resources.Gold,
                MembershipLevels.Platinum => Resources.Platinum,
                MembershipLevels.Diamond => Resources.Diamond,
                _ => "--",
            };
            return levelName;
        }

        private string GetDealTypeName(int dealType)
        {
            string typeName = dealType switch
            {
                DealTypes.InStore => Resources.Instore,
                DealTypes.Online => Resources.Deal,
                DealTypes.Phone => Resources.PhoneCall,
                _ => "--",
            };
            return typeName;
        }

        private string GetRaffleTypeName(int raffleType)
        {
            string typeName = raffleType switch
            {
                RaffleTypes.Open => Resources.Reward,
                RaffleTypes.ByRaffle => Resources.Raffle,
                RaffleTypes.PerPurchases => Resources.PerPurchases,
                _ => "--",
            };
            return typeName;
        }

        private string GetRewardUsageTypeName(int raffleUsageType)
        {
            string typeName = raffleUsageType switch
            {
                RewardUsageTypes.AllPurpose => Resources.AllPurpose,
                RewardUsageTypes.YOYBenefits => Resources.YOYBenefits,
                RewardUsageTypes.CommerceLoyaltyProgram => Resources.CommerceLoyalty,
                _ => "--",
            };
            return typeName;
        }

        private string GetPromoTypeName(int offerType)
        {
            string typeName = offerType switch
            {
                OfferTypes.Reward => Resources.Reward,
                OfferTypes.Offer => Resources.Offer,
                OfferTypes.Coupon => Resources.Coupon,
                OfferTypes.CashbackIncentive => Resources.CashbackIncentive,
                OfferTypes.Special => Resources.SpecialProduct,
                _ => "--",
            };
            return typeName;
        }

        private string GetCodeTypeName(int codeType)
        {
            string typeName = codeType switch
            {
                CodeTypes.Text => Resources.Text,
                CodeTypes.Img => Resources.Img,
                _ => "--",
            };
            return typeName;
        }

        private string GetRewardTypeName(int rewardType)
        {
            string typeName = rewardType switch
            {
                RewardTypes.Deal => Resources.Deal,
                RewardTypes.Prize => Resources.Prize,
                RewardTypes.Game => Resources.Game,
                RewardTypes.Gift => Resources.Gift,
                _ => "--",
            };
            return typeName;
        }

        private string GetPurposeTypeName(int purposeType)
        {
            string typeName = purposeType switch
            {
                OfferPurposeTypes.Reward => Resources.Reward,
                OfferPurposeTypes.Deal => Resources.Deal,
                _ => "--",
            };
            return typeName;
        }

        private string GetObjectiveTypeName(int objectiveType)
        {
            string typeName = objectiveType switch
            {
                ObjectiveTypes.PointsRedemption => Resources.PointsRedemption,
                ObjectiveTypes.RewardPurchases => Resources.RewardPurchases,
                ObjectiveTypes.MediaInteraction => Resources.MediaInteraction,
                ObjectiveTypes.GenerateTraffic => Resources.GenerateTraffic,
                ObjectiveTypes.UpSelling => Resources.UpSelling,
                ObjectiveTypes.ReturningIncentive => Resources.ReturningIncentive,
                ObjectiveTypes.CrossSelling => Resources.CrossSelling,
                _ => "",
            };
            return typeName;
        }

        private string GetGeoSegmentationTypeName(int segmentationType)
        {
            string typeName = segmentationType switch
            {
                GeoSegmentationTypes.Country => Resources.CountrySegmentation,
                GeoSegmentationTypes.State => Resources.StateSegmentation,
                GeoSegmentationTypes.City => Resources.CitySegmentation,
                _ => "",
            };
            return typeName;
        }

        private string GetDisplayTypeName(int displayType)
        {
            string typeName = displayType switch
            {
                DisplayTypes.ListingsOnly => Resources.ListingsOnly,
                DisplayTypes.BroadcastingAndListings => Resources.BroadcastingAndListings,
                DisplayTypes.BroadcastingOnly => Resources.BroadcastingOnly,
                _ => "",
            };
            return typeName;
        }


        /// <summary>
        /// Gets all the rewards for a given tenant
        /// </summary>
        /// <param name="activeState"></param>
        /// <param name="expiredState"></param>
        /// <param name="releaseState"></param>
        /// <param name="dateTime"></param>
        /// <param name="offerPurpose"></param>
        /// <returns></returns>
        public List<RewardOverview> Gets(int activeState, int expiredState, int releaseState, DateTime dateTime, int offerPurpose, int raffleUsageType, bool filterByTenant, int pageSize, int pageNumber)
        {
            List<RewardOverview> rewards = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }
                                break;
                            case ExpiredStates.Valid:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }
                                break;
                            case ExpiredStates.Expired:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        //NON SENSE CASE, JUST TO COMPLETE ALL THE CASES
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
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
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }
                                break;
                            case ExpiredStates.Valid:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.IsActive && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.IsActive && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }
                                break;
                            case ExpiredStates.Expired:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.IsActive && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.IsActive && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        //NON SENSE CASE, JUST TO COMPLETE ALL THE CASES

                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
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
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where !x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where !x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where !x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where !x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where !x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where !x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }
                                break;
                            case ExpiredStates.Valid:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where !x.IsActive && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where !x.IsActive && x.ExpirationDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where !x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where !x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where !x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where !x.IsActive && x.ExpirationDate > dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }
                                break;
                            case ExpiredStates.Expired:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where !x.IsActive && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where !x.IsActive && x.ExpirationDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where !x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where !x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        //NON SENSE CASE, JUST TO COMPLETE ALL THE CASES

                                        if (filterByTenant)
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (raffleUsageType != RewardUsageTypes.All)
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetRewardsByUsageTypeForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose, raffleUsageType)
                                                         where !x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.FuncsHandler.GetAllRewardsForTenantWithoutRedemptions(this._businessObjects.Tenant.TenantId, offerPurpose)
                                                         where !x.IsActive && x.ExpirationDate <= dateTime && x.ReleaseDate > dateTime
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
                    RewardOverview reward = null;
                    rewards = new List<RewardOverview>();

                    foreach (TemprewardOverviews item in query)
                    {
                        reward = new RewardOverview
                        {
                            Id = item.Id,
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            Name = item.Name,
                            Code = item.Code,
                            Value = item.Value,
                            RaffleType = item.RaffleType,
                            RaffleTypeName = GetRaffleTypeName(item.RaffleType),
                            RewardUsageType = item.RewardUsageType,
                            RewardUsageTypeName = GetRewardUsageTypeName(item.RewardUsageType),
                            WinnersCount = (int)item.WinnersCount,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            MinMembershipLevel = item.RewardMinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.RewardMinMembershipLevel),
                            TimeOutDaysBetweenRedemption = item.RewardTimeOutDaysBetweenRedemption,
                        };

                        reward.PublishState = GetPublishState(item.ReleaseDate, item.ExpirationDate, dateTime);

                        rewards.Add(reward);
                    }
                }
            }
            catch (Exception e)
            {
                rewards = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return rewards;
        }

        /// <summary>
        /// Gets a reward
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ClubReward Get(Guid id, Guid tenantId)
        {
            ClubReward reward = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetRewardDetails(tenantId, id)
                            select x;

                if (query != null)
                {

                    foreach (TemprewardOverviews item in query)
                    {
                        reward = new ClubReward
                        {
                            Offer = new Offer
                            {
                                Id = item.Id,
                                TenantId = item.TenantId,
                                MainCategoryId = item.MainCategoryId,
                                MainCategoryName = item.CategoryName,
                                OfferType = item.OfferType,
                                DealType = item.DealType,
                                DealTypeName = GetDealTypeName(item.DealType),
                                RewardType = item.RewardType,
                                RewardTypeName = GetRewardTypeName(item.RewardType),
                                PurposeType = item.PurposeType,
                                PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                                GeoSegmentationType = item.GeoSegmentationType,
                                GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                                DisplayType = item.DisplayType,
                                DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                                Name = item.Name,
                                MainHint = item.MainHint,
                                ComplementaryHint = item.ComplementaryHint,
                                Keywords = item.Keywords,
                                Description = item.Description,
                                Code = item.Code ?? "",
                                MinsToUnlock = item.MinsToUnlock,
                                IsActive = item.IsActive,
                                IsExclusive = item.IsExclusive,
                                IsSponsored = item.IsSponsored,
                                AvailableQuantity = item.AvailableQuantity,
                                OneTimeRedemption = item.OneTimeRedemption,
                                MaxClaimsPerUser = item.MaxClaimsPerUser,
                                MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                PurchasesCountStartDate = item.PurchasesCountStartDate,
                                ClaimLocation = item.ClaimLocation,
                                Value = item.Value,
                                RegularValue = item.RegularValue,
                                RedeemCount = item.RedeemCount,
                                ClaimCount = item.ClaimCount,
                                ReleaseDate = item.ReleaseDate,
                                ExpirationDate = item.ExpirationDate,
                                DisplayImgId = item.DisplayImageId,
                                Rules = item.Rules ?? Resources.NoRulesAvailable,
                                Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                RelevanceRate = item.RelevanceRate,
                                CreatedDate = item.CreatedDate
                            },

                            RedemptionAllowed = false,
                            UserWonIt = false,
                            RaffleDate = item.RaffleDate,
                            RaffleType = item.RaffleType,
                            RaffleTypeName = GetRaffleTypeName(item.RaffleType),
                            RewardUsageType = item.RewardUsageType,
                            RewardUsageTypeName = GetRewardUsageTypeName(item.RewardUsageType),
                            MinMembershipLevel = item.RewardMinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.RewardMinMembershipLevel),
                            TimeOutDaysBetweenRedemption = item.RewardTimeOutDaysBetweenRedemption,
                            EarningsIncreaserId = item.RewardEarningsIncreaserId,
                            RequiredPurchasesToRedeem = item.MinPurchasesCountToRedeem,
                            UserVisitsCount = 0 //This value needs to be calculated in a further step

                        };

                        reward.PublishState = GetPublishState(item.ReleaseDate.Date, item.ExpirationDate, DateTime.UtcNow);
                    }
                }
            }
            catch (Exception e)
            {
                reward = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return reward;
        }


        private List<FlattenedRewardData> GetsRewardsDataForUser(string userId, Guid stateId, DateTime dateTime, int offerPurpose, int raffleUsageType)
        {
            List<FlattenedRewardData> rewards = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetAvailableRewardsForUserByState(userId, stateId, offerPurpose, raffleUsageType, dateTime)
                            where (x.AvailableQuantity == -1 || x.AvailableQuantity > 0)
                            select x;

                if (query != null)
                {
                    rewards = new List<FlattenedRewardData>();
                    FlattenedRewardData reward = null;

                    foreach (TemprewardDetails item in query)
                    {

                        reward = new FlattenedRewardData
                        {
                            Offer = new Offer
                            {
                                Id = item.Id,
                                TenantId = item.TenantId,
                                MainCategoryId = item.MainCategoryId,
                                MainCategoryName = "",//Not needed
                                OfferType = item.OfferType,
                                OfferTypeName = "",//Not needed
                                DealType = item.DealType,
                                DealTypeName = "",//Not needed
                                RewardType = item.RewardType,
                                RewardTypeName = "",//Not needed
                                PurposeType = item.PurposeType,
                                PurposeTypeName = "",//Not needed
                                GeoSegmentationType = item.GeoSegmentationType,
                                GeoSegmentationTypeName = "",//Not needed
                                DisplayType = item.DisplayType,
                                DisplayTypeName = "",//Not needed
                                Name = item.Name,
                                MainHint = item.MainHint,
                                ComplementaryHint = item.ComplementaryHint,
                                Keywords = item.Keywords,
                                Description = item.Description,
                                Code = "",//Not needed
                                CodeImg = null,//Not needed
                                MinsToUnlock = item.MinsToUnlock,
                                IsActive = item.IsActive,
                                IsExclusive = item.IsExclusive,
                                IsSponsored = item.IsSponsored,
                                HasUniqueCodes = false,//Not needed
                                HasPreferences = false,//Not needed
                                AvailableQuantity = item.AvailableQuantity,
                                OneTimeRedemption = item.OneTimeRedemption,
                                MaxClaimsPerUser = item.MaxClaimsPerUser,
                                MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                PurchasesCountStartDate = item.PurchasesCountStartDate,
                                ClaimLocation = item.ClaimLocation,
                                Value = item.Value,
                                RegularValue = item.RegularValue,
                                ExtraBonus = -1,//Not needed
                                ExtraBonusType = -1,//Not needed
                                ExtraBonusTypeName = "",//Not needed
                                MinIncentive = -1,//Not needed
                                MaxIncentive = -1,//Not needed
                                IncentiveVariationType = -1,//Not needed
                                IncentiveVarationTypeName = "",//Not needed
                                IncentiveVariation = -1,//Not needed
                                SecondsIncentiveVariationFrame = -1,//Not needed
                                RedeemCount = item.RedeemCount,
                                ClaimCount = item.ClaimCount,
                                ReleaseDate = item.ReleaseDate,
                                ExpirationDate = item.ExpirationDate,
                                TargettingParams = "",//Not needed
                                DisplayImgId = item.DisplayImageId,
                                Rules = item.Rules ?? Resources.NoRulesAvailable,
                                Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                LastBroadcastingUsage = null,//Not needed
                                BroadcastingTimerType = -1,//Not needed
                                BroadcastingTimerTypeName = "",//Not needed
                                BroadcastingScheduleType = -1,//Not needed
                                BroadcastingScheduleTypeName = "",//Not needed
                                BroadcastingMinsToRedeem = -1,//Not needed
                                BroadcastingTitle = "",//Not needed
                                BroadcastingMsg = "",//Not needed
                                RelevanceRate = item.RelevanceRate,
                                CreatedDate = item.CreatedDate,
                                UpdatedDate = DateTime.UtcNow,//Not needed
                                SatisfactionScore = -1,//Not needed
                                RelevanceScore = -1//Not needed
                            },
                            Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                            {
                                Id = item.TenantId,
                                Name = item.TenantName,
                                Logo = item.TenantLogo,
                                LandingImg = item.TenantLandingImg,
                                CountryId = Guid.Empty,//USELESS
                                CurrencySymbol = Resources.LoyaltyPoints,
                                Type = item.TenantType,
                                CategoryId = item.TenantCategoryId,
                                CategoryName = item.TenantCategoryName,
                                RelevanceScore = null,//When its selector is category, there is no info about tenant relevance
                                NearestBranchId = Guid.Empty,
                                NearestBranchName = "",
                                NearestBranchLatitude = null,
                                NearestBranchLongitude = null,
                                MemberShipId = item.MembershipId != null ? (Guid)item.MembershipId : Guid.Empty,
                                IsMember = item.MembershipId != null,
                                PointsBalance = item.AvailablePoints ?? 0
                            },
                            Branch = new BasicBranchData
                            {
                                Id = item.BranchId,
                                Name = item.BranchName,
                                InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                DescriptiveAddress = item.BranchDescriptiveAddress,
                                Latitude = item.BranchLatitude,
                                Longitude = item.BranchLongitude,
                                Distance = -1,//USELESS
                                CityId = item.BranchCityId,
                                StateId = item.BranchStateId,
                                Enabled = false
                            },

                            MinMembershipLevel = item.RewardMinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.RewardMinMembershipLevel),
                            TimeOutDaysBetweenRedemption = item.RewardTimeOutDaysBetweenRedemption,
                            RedemptionAllowed = false,
                            UserWonIt = false,
                            RaffleType = item.RaffleType,
                            UsageType = item.RewardUsageType,
                            RequiredPurchasesToRedeem = item.MinPurchasesCountToRedeem,
                            UserPurchasesCount = 0 //This value needs to be calculated in other step
                        };

                        switch (item.RaffleType)
                        {
                            case RaffleTypes.Open:
                                reward.RaffleDate = item.ReleaseDate;


                                reward.UserWonIt = false; //Open rewards are never raffles

                                if (item.AvailablePoints >= item.Value)
                                    reward.RedemptionAllowed = true;

                                break;
                            case RaffleTypes.ByRaffle:
                                reward.RaffleDate = item.RaffleDate;

                                //If user has been chosen as winner, has enough points to redeem it and the raffle has happened already
                                if (item.Claimed != null && item.RaffleDate <= dateTime)
                                {
                                    if (item.Claimed == false)
                                    {
                                        reward.UserWonIt = true;
                                    }

                                    if (item.AvailablePoints >= item.Value)
                                    {
                                        reward.RedemptionAllowed = true;
                                    }
                                    else
                                    {
                                        reward.RedemptionAllowed = false;
                                    }

                                }


                                break;
                            case RaffleTypes.PerPurchases:
                                reward.RaffleDate = item.ReleaseDate;


                                reward.UserWonIt = false; //Open rewards are never raffles
                                reward.RedemptionAllowed = false;//This type of reward is only redeemable by proximity detection

                                break;
                        }

                        //If it's an open reward, or if it's a raffle that hasn't choose winer, or if it's a raffle with selected winner that hasn't reserve it
                        if ((item.RaffleType == RaffleTypes.Open && item.ExpirationDate > dateTime) ||
                            (item.RaffleType == RaffleTypes.ByRaffle && item.RaffleDate > dateTime) ||
                            (item.RaffleType == RaffleTypes.PerPurchases && item.ExpirationDate > dateTime) ||
                            (item.RaffleType == RaffleTypes.ByRaffle && item.RaffleDate <= dateTime && item.Claimed == false && reward.UserWonIt))
                        {
                            rewards.Add(reward);
                        }

                    }
                }

            }
            catch (Exception e)
            {
                rewards = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return rewards;
        }

        private List<FlattenedRewardData> GetsRewardsDataForUserByTenant(string userId, Guid stateId, Guid tenantId, DateTime dateTime, int offerPurpose, int raffleUsageType)
        {
            List<FlattenedRewardData> rewards = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetAvailableRewardsForUserByTenant(userId, stateId, tenantId, offerPurpose, raffleUsageType, dateTime)
                            where (x.AvailableQuantity == -1 || x.AvailableQuantity > 0)
                            select x;

                if (query != null)
                {
                    rewards = new List<FlattenedRewardData>();
                    FlattenedRewardData reward = null;

                    foreach (TemprewardDetails item in query)
                    {
                        reward = new FlattenedRewardData
                        {
                            Offer = new Offer
                            {
                                Id = item.Id,
                                TenantId = item.TenantId,
                                MainCategoryId = item.MainCategoryId,
                                MainCategoryName = "",//Not needed
                                OfferType = item.OfferType,
                                OfferTypeName = "",//Not needed
                                DealType = item.DealType,
                                DealTypeName = "",//Not needed
                                RewardType = item.RewardType,
                                RewardTypeName = "",//Not needed
                                PurposeType = item.PurposeType,
                                PurposeTypeName = "",//Not needed
                                GeoSegmentationType = item.GeoSegmentationType,
                                GeoSegmentationTypeName = "",//Not needed
                                DisplayType = item.DisplayType,
                                DisplayTypeName = "",//Not needed
                                Name = item.Name,
                                MainHint = item.MainHint,
                                ComplementaryHint = item.ComplementaryHint,
                                Keywords = item.Keywords,
                                Description = item.Description,
                                Code = "",//Not needed
                                CodeImg = null,//Not needed
                                MinsToUnlock = item.MinsToUnlock,
                                IsActive = item.IsActive,
                                IsExclusive = item.IsExclusive,
                                IsSponsored = item.IsSponsored,
                                HasUniqueCodes = false,//Not needed
                                HasPreferences = false,//Not needed
                                AvailableQuantity = item.AvailableQuantity,
                                OneTimeRedemption = item.OneTimeRedemption,
                                MaxClaimsPerUser = item.MaxClaimsPerUser,
                                MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                PurchasesCountStartDate = item.PurchasesCountStartDate,
                                ClaimLocation = item.ClaimLocation,
                                Value = item.Value,
                                RegularValue = item.RegularValue,
                                ExtraBonus = -1,//Not needed
                                ExtraBonusType = -1,//Not needed
                                ExtraBonusTypeName = "",//Not needed
                                MinIncentive = -1,//Not needed
                                MaxIncentive = -1,//Not needed
                                IncentiveVariationType = -1,//Not needed
                                IncentiveVarationTypeName = "",//Not needed
                                IncentiveVariation = -1,//Not needed
                                SecondsIncentiveVariationFrame = -1,//Not needed
                                RedeemCount = item.RedeemCount,
                                ClaimCount = item.ClaimCount,
                                ReleaseDate = item.ReleaseDate,
                                ExpirationDate = item.ExpirationDate,
                                TargettingParams = "",//Not needed
                                DisplayImgId = item.DisplayImageId,
                                Rules = item.Rules ?? Resources.NoRulesAvailable,
                                Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                LastBroadcastingUsage = null,//Not needed
                                BroadcastingTimerType = -1,//Not needed
                                BroadcastingTimerTypeName = "",//Not needed
                                BroadcastingScheduleType = -1,//Not needed
                                BroadcastingScheduleTypeName = "",//Not needed
                                BroadcastingMinsToRedeem = -1,//Not needed
                                BroadcastingTitle = "",//Not needed
                                BroadcastingMsg = "",//Not needed
                                RelevanceRate = item.RelevanceRate,
                                CreatedDate = item.CreatedDate,
                                UpdatedDate = DateTime.UtcNow,//Not needed
                                SatisfactionScore = -1,//Not needed
                                RelevanceScore = -1//Not needed
                            },
                            Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                            {
                                Id = item.TenantId,
                                Name = item.TenantName,
                                Logo = item.TenantLogo,
                                LandingImg = item.TenantLandingImg,
                                CountryId = Guid.Empty,//USELESS
                                CurrencySymbol = "",
                                Type = item.TenantType,
                                CategoryId = item.TenantCategoryId,
                                CategoryName = item.TenantCategoryName,
                                RelevanceScore = null,//When its selector is category, there is no info about tenant relevance
                                NearestBranchId = Guid.Empty,
                                NearestBranchName = "",
                                NearestBranchLatitude = null,
                                NearestBranchLongitude = null,
                                MemberShipId = item.MembershipId != null ? (Guid)item.MembershipId : Guid.Empty,
                                IsMember = item.MembershipId != null,
                                PointsBalance = item.AvailablePoints ?? 0
                            },
                            Branch = new BasicBranchData
                            {
                                Id = item.BranchId,
                                Name = item.BranchName,
                                InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                DescriptiveAddress = item.BranchDescriptiveAddress,
                                Latitude = item.BranchLatitude,
                                Longitude = item.BranchLongitude,
                                Distance = -1,//USELESS
                                CityId = item.BranchCityId,
                                StateId = item.BranchStateId,
                                Enabled = false
                            },

                            MinMembershipLevel = item.RewardMinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.RewardMinMembershipLevel),
                            TimeOutDaysBetweenRedemption = item.RewardTimeOutDaysBetweenRedemption,
                            RedemptionAllowed = false,
                            UserWonIt = false,
                            RaffleType = item.RaffleType,
                            UsageType = item.RewardUsageType,
                            RequiredPurchasesToRedeem = item.MinPurchasesCountToRedeem,
                            UserPurchasesCount = 0 //This value needs to be calculated in a further step
                        };

                        switch (item.RaffleType)
                        {
                            case RaffleTypes.Open:
                                reward.RaffleDate = item.ReleaseDate;

                                reward.UserWonIt = false; //Open rewards are never raffles

                                if (item.AvailablePoints >= item.Value)
                                    reward.RedemptionAllowed = true;

                                break;
                            case RaffleTypes.ByRaffle:
                                reward.RaffleDate = item.RaffleDate;

                                //If user has been chosen as winner, has enough points to redeem it and the raffle has happened already
                                if (item.Claimed != null && item.RaffleDate <= dateTime)
                                {
                                    if (item.Claimed == false)
                                    {
                                        reward.UserWonIt = true;
                                    }

                                    if (item.AvailablePoints >= item.Value)
                                    {
                                        reward.RedemptionAllowed = true;
                                    }
                                    else
                                    {
                                        reward.RedemptionAllowed = false;
                                    }

                                }


                                break;
                            case RaffleTypes.PerPurchases:
                                reward.RaffleDate = item.ReleaseDate;

                                reward.UserWonIt = false; //Open rewards are never raffles
                                reward.RedemptionAllowed = false;//This type of reward it's only available by proximity

                                break;
                        }

                        //If it's an open reward, or if it's a raffle that hasn't choose winer, or if it's a raffle with selected winner that hasn't reserve it
                        if ((item.RaffleType == RaffleTypes.Open && item.ExpirationDate > dateTime) ||
                            (item.RaffleType == RaffleTypes.PerPurchases && item.ExpirationDate > dateTime) ||
                            (item.RaffleType == RaffleTypes.ByRaffle && item.RaffleDate > dateTime) ||
                            (item.RaffleType == RaffleTypes.ByRaffle && item.RaffleDate <= dateTime && item.Claimed == false && reward.UserWonIt))
                        {
                            rewards.Add(reward);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                rewards = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return rewards;
        }

        private void BuildFullRewardList(ref List<FullRewardData> rewardsData, ref List<RewardDataWithBranches> enabledRewards, bool includeBranchList)
        {
            RewardDataWithBranches currentOffer;
            List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
            List<BasicBranchData> availableBranches;
            IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
            BasicBranchData nearestBranch;
            int? branchGroupsCount;

            if (!includeBranchList)
            {
                bool locationMatch = false;

                for (int i = 0; i < enabledRewards.Count; i++)
                {
                    currentOffer = enabledRewards[i];
                    locationMatch = false;

                    //This means it's enabled for the complete country, then it's enabled for all the potential branches
                    if (currentOffer.Offer.GeoSegmentationType != GeoSegmentationTypes.Country)
                    {
                        enabledLocations = this._businessObjects.ContentLocations.Gets(enabledRewards[i].Offer.Id);
                        availableBranches = currentOffer.Branches.GroupBy(x => x.Id)
                                                                           .Select(grp => grp.First())
                                                                           .ToList();

                        if (enabledLocations?.Count > 0)
                        {

                            //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                            switch (currentOffer.Offer.GeoSegmentationType)
                            {
                                case GeoSegmentationTypes.State:
                                    //Will group by state
                                    branchesGrouped = currentOffer.Branches.GroupBy(x => x.StateId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    locationMatch = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        locationMatch = false;
                                    }

                                    break;
                                case GeoSegmentationTypes.City:
                                    //Will group by state
                                    branchesGrouped = currentOffer.Branches.GroupBy(x => x.CityId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    locationMatch = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        locationMatch = false;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            //If no enabled locations needs to be removed
                            locationMatch = false;
                        }
                    }
                    else
                    {
                        //If the offer is enabled to be avaiable to the complete country, then it's valid
                        locationMatch = true;
                    }

                    if (locationMatch)
                    {

                        rewardsData.Add(new FullRewardData
                        {
                            RaffleDate = currentOffer.RaffleDate,
                            RaffleType = currentOffer.RaffleType,
                            MinMembershipLevel = currentOffer.MinMembershipLevel,
                            MinMembershipLevelName = currentOffer.MinMembershipLevelName,
                            TimeOutDaysBetweenRedemption = currentOffer.TimeOutDaysBetweenRedemption,
                            RedemptionAllowed = currentOffer.RedemptionAllowed,
                            UserWonIt = currentOffer.UserWonIt,
                            RequiredPurchasesToRedeem = currentOffer.RequiredPurchasesToRedeem,
                            UserPurchasesCount = currentOffer.UserPurchasesCount,
                            Offer = currentOffer.Offer,
                            Tenant = currentOffer.Tenant,
                            Branches = new List<BasicBranchData>()
                        }
                        );
                    }

                }
            }
            else
            {
                List<BasicBranchData> enabledBranches = null;

                for (int i = 0; i < enabledRewards.Count; i++)
                {
                    currentOffer = enabledRewards[i];

                    enabledLocations = this._businessObjects.ContentLocations.Gets(enabledRewards[i].Offer.Id);
                    availableBranches = currentOffer.Branches.GroupBy(x => x.Id)
                                                                           .Select(grp => grp.First())
                                                                           .ToList();
                    enabledBranches = new List<BasicBranchData>();

                    //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                    switch (currentOffer.Offer.GeoSegmentationType)
                    {
                        case GeoSegmentationTypes.State:
                            //Will group by state
                            branchesGrouped = currentOffer.Branches.GroupBy(x => x.StateId);
                            branchGroupsCount = branchesGrouped?.Count();


                            if (branchGroupsCount != null && branchGroupsCount > 0)
                            {
                                for (int j = 0; j < enabledLocations.Count; j++)
                                {
                                    for (int k = 0; k < branchGroupsCount; k++)
                                    {
                                        //If the location has the same geosegmentation and has the same location reference Id
                                        if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                        {
                                            enabledBranches.AddRange(availableBranches.Where(x => x.StateId == enabledLocations[j].Value).ToList());
                                        }
                                    }
                                }
                            }

                            break;
                        case GeoSegmentationTypes.City:
                            //Will group by state
                            branchesGrouped = currentOffer.Branches.GroupBy(x => x.CityId);
                            branchGroupsCount = branchesGrouped?.Count();


                            if (branchGroupsCount != null && branchGroupsCount > 0)
                            {
                                for (int j = 0; j < enabledLocations.Count; j++)
                                {
                                    for (int k = 0; k < branchGroupsCount; k++)
                                    {
                                        //If the location has the same geosegmentation and has the same location reference Id
                                        if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                        {
                                            enabledBranches.AddRange(availableBranches.Where(x => x.CityId == enabledLocations[j].Value).ToList());
                                        }
                                    }
                                }
                            }

                            break;
                        case GeoSegmentationTypes.Country:
                            //This means the offer is enabled to all the existing branches
                            enabledBranches = availableBranches;

                            break;
                    }

                    if (enabledBranches?.Count > 0)
                    {
                        enabledBranches = enabledBranches.OrderBy(x => x.Distance).ToList();

                        nearestBranch = enabledBranches.ElementAt(0);

                        if (nearestBranch != null)
                        {
                            currentOffer.Tenant.NearestBranchId = nearestBranch.Id;
                            currentOffer.Tenant.NearestBranchName = nearestBranch.Name;
                            currentOffer.Tenant.NearestBranchLatitude = nearestBranch.Latitude;
                            currentOffer.Tenant.NearestBranchLongitude = nearestBranch.Longitude;
                        }

                        rewardsData.Add(new FullRewardData
                        {
                            RaffleDate = currentOffer.RaffleDate,
                            RaffleType = currentOffer.RaffleType,
                            MinMembershipLevel = currentOffer.MinMembershipLevel,
                            MinMembershipLevelName = currentOffer.MinMembershipLevelName,
                            TimeOutDaysBetweenRedemption = currentOffer.TimeOutDaysBetweenRedemption,
                            RedemptionAllowed = currentOffer.RedemptionAllowed,
                            UserWonIt = currentOffer.UserWonIt,
                            RequiredPurchasesToRedeem = currentOffer.RequiredPurchasesToRedeem,
                            UserPurchasesCount = currentOffer.UserPurchasesCount,
                            Offer = currentOffer.Offer,
                            Tenant = currentOffer.Tenant,
                            Branches = enabledBranches
                        }
                        );
                    }

                }
            }
        }

        public List<FullRewardData> GetEnabledRewardsForUserByState(Guid stateId, string userId, DateTime dateTime, int offerPurpose, int raffleUsageType, bool includeBranchList)
        {
            List<FullRewardData> rewardsData = new List<FullRewardData>();

            try
            {
                List<FlattenedRewardData> flattenedRewards = this.GetsRewardsDataForUser(userId, stateId, dateTime, offerPurpose, raffleUsageType);

                if (flattenedRewards?.Count > 0)
                {
                    RewardDataWithBranches currentReward;
                    IEnumerable<IGrouping<Guid, FlattenedRewardData>> groupedByOfferId = flattenedRewards.GroupBy(x => x.Offer.Id);
                    List<RewardDataWithBranches> enabledRewards = new List<RewardDataWithBranches>();

                    FlattenedRewardData[] offersGroup = null;

                    foreach (IGrouping<Guid, FlattenedRewardData> offerDataGroup in groupedByOfferId)
                    {
                        offersGroup = offerDataGroup.ToArray();

                        currentReward = new RewardDataWithBranches
                        {
                            Offer = offersGroup[0].Offer,
                            Tenant = offersGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            MinMembershipLevel = offersGroup[0].MinMembershipLevel,
                            MinMembershipLevelName = offersGroup[0].MinMembershipLevelName,
                            TimeOutDaysBetweenRedemption = offersGroup[0].TimeOutDaysBetweenRedemption,
                            RedemptionAllowed = offersGroup[0].RedemptionAllowed,
                            UserWonIt = offersGroup[0].UserWonIt,
                            RaffleDate = offersGroup[0].RaffleDate,
                            RaffleType = offersGroup[0].RaffleType,
                            RequiredPurchasesToRedeem = offersGroup[0].RequiredPurchasesToRedeem,
                            UserPurchasesCount = offersGroup[0].UserPurchasesCount
                        };

                        for (int i = 0; i < offersGroup.Length; ++i)
                        {
                            currentReward.Branches.Add(offersGroup[i].Branch);
                        }

                        enabledRewards.Add(currentReward);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled

                    this.BuildFullRewardList(ref rewardsData, ref enabledRewards, includeBranchList);

                }

            }
            catch (Exception e)
            {
                rewardsData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return rewardsData;

        }

        public List<FullRewardData> GetEnabledRewardsForUserByTenant(Guid tenantId, Guid stateId, string userId, DateTime dateTime, int offerPurpose, int raffleUsageType, bool includeBranchList)
        {
            List<FullRewardData> rewardsData = new List<FullRewardData>();

            try
            {
                List<FlattenedRewardData> flattenedRewards = this.GetsRewardsDataForUserByTenant(userId, stateId, tenantId, dateTime, offerPurpose, raffleUsageType);

                if (flattenedRewards?.Count > 0)
                {
                    RewardDataWithBranches currentOffer;
                    IEnumerable<IGrouping<Guid, FlattenedRewardData>> groupedByOfferId = flattenedRewards.GroupBy(x => x.Offer.Id);
                    List<RewardDataWithBranches> enabledOffers = new List<RewardDataWithBranches>();
                    FlattenedRewardData[] offersGroup = null;

                    foreach (IGrouping<Guid, FlattenedRewardData> offerDataGroup in groupedByOfferId)
                    {
                        offersGroup = offerDataGroup.ToArray();

                        currentOffer = new RewardDataWithBranches
                        {
                            Offer = offersGroup[0].Offer,
                            Tenant = offersGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            MinMembershipLevel = offersGroup[0].MinMembershipLevel,
                            MinMembershipLevelName = offersGroup[0].MinMembershipLevelName,
                            TimeOutDaysBetweenRedemption = offersGroup[0].TimeOutDaysBetweenRedemption,
                            RedemptionAllowed = offersGroup[0].RedemptionAllowed,
                            UserWonIt = offersGroup[0].UserWonIt,
                            RaffleType = offersGroup[0].RaffleType,
                            RaffleDate = offersGroup[0].RaffleDate,
                            RequiredPurchasesToRedeem = offersGroup[0].RequiredPurchasesToRedeem,
                            UserPurchasesCount = offersGroup[0].UserPurchasesCount
                        };

                        for (int i = 0; i < offersGroup.Length; ++i)
                        {
                            currentOffer.Branches.Add(offersGroup[i].Branch);
                        }

                        enabledOffers.Add(currentOffer);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled

                    List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
                    BasicBranchData nearestBranch;
                    List<BasicBranchData> availableBranches;
                    IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
                    int? branchGroupsCount;

                    if (!includeBranchList)
                    {
                        bool locationMatch = false;

                        for (int i = 0; i < enabledOffers.Count; i++)
                        {
                            currentOffer = enabledOffers[i];
                            locationMatch = false;

                            //This means it's enabled for the complete country, then it's enabled for all the potential branches
                            if (currentOffer.Offer.GeoSegmentationType != GeoSegmentationTypes.Country)
                            {
                                if (currentOffer.Offer.GeoSegmentationType != GeoSegmentationTypes.Country)
                                {
                                    enabledLocations = this._businessObjects.ContentLocations.Gets(enabledOffers[i].Offer.Id);
                                }
                                else
                                {
                                    enabledLocations = new List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>>();
                                }

                                availableBranches = currentOffer.Branches.GroupBy(x => x.Id)
                                                                                   .Select(grp => grp.First())
                                                                                   .ToList();

                                if (enabledLocations?.Count > 0)
                                {

                                    //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                                    switch (currentOffer.Offer.GeoSegmentationType)
                                    {
                                        case GeoSegmentationTypes.State:
                                            //Will group by state
                                            branchesGrouped = currentOffer.Branches.GroupBy(x => x.StateId);
                                            branchGroupsCount = branchesGrouped?.Count();


                                            if (branchGroupsCount != null && branchGroupsCount > 0)
                                            {
                                                for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                                {
                                                    for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                                    {
                                                        //If the location has the same geosegmentation and has the same location reference Id
                                                        if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                        {
                                                            locationMatch = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                locationMatch = false;
                                            }

                                            break;
                                        case GeoSegmentationTypes.City:
                                            //Will group by state
                                            branchesGrouped = currentOffer.Branches.GroupBy(x => x.CityId);
                                            branchGroupsCount = branchesGrouped?.Count();


                                            if (branchGroupsCount != null && branchGroupsCount > 0)
                                            {
                                                for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                                {
                                                    for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                                    {
                                                        //If the location has the same geosegmentation and has the same location reference Id
                                                        if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                        {
                                                            locationMatch = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                locationMatch = false;
                                            }

                                            break;
                                    }
                                }
                                else
                                {
                                    //If no enabled locations needs to be removed
                                    locationMatch = false;
                                }
                            }
                            else
                            {
                                //If the offer is enabled to be avaiable to the complete country, then it's valid
                                locationMatch = true;
                            }

                            if (locationMatch)
                            {

                                rewardsData.Add(new FullRewardData
                                {
                                    RaffleDate = currentOffer.RaffleDate,
                                    RaffleType = currentOffer.RaffleType,
                                    RedemptionAllowed = currentOffer.RedemptionAllowed,
                                    MinMembershipLevel = currentOffer.MinMembershipLevel,
                                    MinMembershipLevelName = currentOffer.MinMembershipLevelName,
                                    TimeOutDaysBetweenRedemption = currentOffer.TimeOutDaysBetweenRedemption,
                                    UserWonIt = currentOffer.UserWonIt,
                                    RequiredPurchasesToRedeem = currentOffer.RequiredPurchasesToRedeem,
                                    UserPurchasesCount = currentOffer.UserPurchasesCount,
                                    Offer = currentOffer.Offer,
                                    Tenant = currentOffer.Tenant,
                                    Branches = new List<BasicBranchData>()
                                }
                                );
                            }

                        }
                    }
                    else
                    {
                        List<BasicBranchData> enabledBranches = null;

                        for (int i = 0; i < enabledOffers.Count; i++)
                        {
                            currentOffer = enabledOffers[i];

                            enabledLocations = this._businessObjects.ContentLocations.Gets(enabledOffers[i].Offer.Id);
                            availableBranches = currentOffer.Branches.GroupBy(x => x.Id)
                                                                                   .Select(grp => grp.First())
                                                                                   .ToList();
                            enabledBranches = new List<BasicBranchData>();

                            //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                            switch (currentOffer.Offer.GeoSegmentationType)
                            {
                                case GeoSegmentationTypes.State:
                                    //Will group by state
                                    branchesGrouped = currentOffer.Branches.GroupBy(x => x.StateId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    enabledBranches.AddRange(availableBranches.Where(x => x.StateId == enabledLocations[j].Value).ToList());
                                                }
                                            }
                                        }
                                    }

                                    break;
                                case GeoSegmentationTypes.City:
                                    //Will group by state
                                    branchesGrouped = currentOffer.Branches.GroupBy(x => x.CityId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    enabledBranches.AddRange(availableBranches.Where(x => x.CityId == enabledLocations[j].Value).ToList());
                                                }
                                            }
                                        }
                                    }

                                    break;
                                case GeoSegmentationTypes.Country:
                                    //This means the offer is enabled to all the existing branches
                                    enabledBranches = availableBranches;

                                    break;
                            }

                            if (enabledBranches?.Count > 0)
                            {
                                enabledBranches = enabledBranches.OrderBy(x => x.Distance).ToList();

                                nearestBranch = enabledBranches.ElementAt(0);

                                if (nearestBranch != null)
                                {
                                    currentOffer.Tenant.NearestBranchId = nearestBranch.Id;
                                    currentOffer.Tenant.NearestBranchName = nearestBranch.Name;
                                    currentOffer.Tenant.NearestBranchLatitude = nearestBranch.Latitude;
                                    currentOffer.Tenant.NearestBranchLongitude = nearestBranch.Longitude;
                                }

                                rewardsData.Add(new FullRewardData
                                {
                                    RaffleDate = currentOffer.RaffleDate,
                                    RaffleType = currentOffer.RaffleType,
                                    MinMembershipLevel = currentOffer.MinMembershipLevel,
                                    MinMembershipLevelName = currentOffer.MinMembershipLevelName,
                                    TimeOutDaysBetweenRedemption = currentOffer.TimeOutDaysBetweenRedemption,
                                    RedemptionAllowed = currentOffer.RedemptionAllowed,
                                    UserWonIt = currentOffer.UserWonIt,
                                    RequiredPurchasesToRedeem = currentOffer.RequiredPurchasesToRedeem,
                                    UserPurchasesCount = currentOffer.UserPurchasesCount,
                                    Offer = currentOffer.Offer,
                                    Tenant = currentOffer.Tenant,
                                    Branches = enabledBranches
                                }
                                );
                            }

                        }
                    }

                }

            }
            catch (Exception e)
            {
                rewardsData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return rewardsData;

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
        public ClubRewardManager(BusinessObjects businessObjects)
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
