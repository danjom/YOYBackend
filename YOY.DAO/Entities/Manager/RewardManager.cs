using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class RewardManager
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

        #region METHOD

        private string GetMembershipLevelName(int membershipLevel)
        {
            string membershipLevelName = membershipLevel switch
            {
                MembershipLevels.Bronze => Resources.Bronze,
                MembershipLevels.Silver => Resources.Silver,
                MembershipLevels.Gold => Resources.Gold,
                MembershipLevels.Platinum => Resources.Platinum,
                MembershipLevels.Diamond => Resources.Diamond,
                _ => "--",
            };
            return membershipLevelName;
        }

        public List<Raffle> Gets(Guid tenantId, bool filterByTenant, DateTime datetime, int activeState, int expiredState, int pageSize, int pageNumber)
        {
            List<Raffle> raffles = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        switch (expiredState)
                        {
                            case ExpiredStates.All:

                                if (filterByTenant)
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.TenantId == tenantId
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ExpiredStates.Valid:
                                if (filterByTenant)
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.TenantId == tenantId && x.RaffleDate <= datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.RaffleDate <= datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (filterByTenant)
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.TenantId == tenantId && x.RaffleDate > datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.RaffleDate > datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }

                        break;
                    case ActiveStates.Active:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:

                                if (filterByTenant)
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.IsActive && x.TenantId == tenantId
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.IsActive
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ExpiredStates.Valid:
                                if (filterByTenant)
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.IsActive && x.TenantId == tenantId && x.RaffleDate <= datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.IsActive && x.RaffleDate <= datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (filterByTenant)
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.IsActive && x.TenantId == tenantId && x.RaffleDate > datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where x.IsActive && x.RaffleDate > datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:

                                if (filterByTenant)
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where !x.IsActive && x.TenantId == tenantId
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where !x.IsActive
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ExpiredStates.Valid:
                                if (filterByTenant)
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where !x.IsActive && x.TenantId == tenantId && x.RaffleDate <= datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where !x.IsActive && x.RaffleDate <= datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (filterByTenant)
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where !x.IsActive && x.TenantId == tenantId && x.RaffleDate > datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltprewardsView
                                             where !x.IsActive && x.RaffleDate > datetime
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }
                        break;
                }

                if (query != null)
                {
                    Raffle currentRaffle = null;
                    foreach (OltprewardsView item in query)
                    {
                        currentRaffle = new Raffle
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            RewardId = item.RewardId,
                            RewardName = item.RewardName,
                            RaffleDate = item.RaffleDate,
                            IsActive = item.IsActive,
                            Notes = item.Notes,
                            Type = item.Type,
                            UsageType = item.UsageType,
                            EarningsIncreaserId = item.EarningsIncreaserId,
                            IncreaserFactor = item.EarningsIncreaserId != null ? (decimal)item.IncreaserFactor : -1,
                            MinMembershipLevel = item.MinMembershipLevel,
                            TimeOutDaysBetweenRedemption = item.TimeOutDaysBetweenRedemption,
                            MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        raffles.Add(currentRaffle);
                    }
                }
            }
            catch (Exception e)
            {
                raffles = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return raffles;
        }

        public List<Raffle> Gets(Guid tenantId, Guid rewardId, DateTime datetime, int activeState, int expiredState, int pageSize, int pageNumber)
        {
            List<Raffle> raffles = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        switch (expiredState)
                        {
                            case ExpiredStates.All:

                                query = (from x in this._businessObjects.Context.OltprewardsView
                                         where x.TenantId == tenantId && x.RewardId == rewardId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ExpiredStates.Valid:
                                query = (from x in this._businessObjects.Context.OltprewardsView
                                         where x.TenantId == tenantId && x.RewardId == rewardId && x.RaffleDate <= datetime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ExpiredStates.Expired:
                                query = (from x in this._businessObjects.Context.OltprewardsView
                                         where x.TenantId == tenantId && x.RewardId == rewardId && x.RaffleDate > datetime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }

                        break;
                    case ActiveStates.Active:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:

                                query = (from x in this._businessObjects.Context.OltprewardsView
                                         where x.IsActive && x.TenantId == tenantId && x.RewardId == rewardId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ExpiredStates.Valid:
                                query = (from x in this._businessObjects.Context.OltprewardsView
                                         where x.IsActive && x.TenantId == tenantId && x.RewardId == rewardId && x.RaffleDate <= datetime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ExpiredStates.Expired:
                                query = (from x in this._businessObjects.Context.OltprewardsView
                                         where x.IsActive && x.TenantId == tenantId && x.RewardId == rewardId && x.RaffleDate > datetime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:

                                query = (from x in this._businessObjects.Context.OltprewardsView
                                         where !x.IsActive && x.TenantId == tenantId && x.RewardId == rewardId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ExpiredStates.Valid:
                                query = (from x in this._businessObjects.Context.OltprewardsView
                                         where !x.IsActive && x.TenantId == tenantId && x.RewardId == rewardId && x.RaffleDate <= datetime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ExpiredStates.Expired:
                                query = (from x in this._businessObjects.Context.OltprewardsView
                                         where !x.IsActive && x.TenantId == tenantId && x.RewardId == rewardId && x.RaffleDate > datetime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                }

                if (query != null)
                {
                    Raffle currentRaffle = null;
                    foreach (OltprewardsView item in query)
                    {
                        currentRaffle = new Raffle
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            RewardId = item.RewardId,
                            RewardName = item.RewardName,
                            RaffleDate = item.RaffleDate,
                            IsActive = item.IsActive,
                            Notes = item.Notes,
                            Type = item.Type,
                            UsageType = item.UsageType,
                            EarningsIncreaserId = item.EarningsIncreaserId,
                            IncreaserFactor = item.EarningsIncreaserId != null ? (decimal)item.IncreaserFactor : -1,
                            MinMembershipLevel = item.MinMembershipLevel,
                            TimeOutDaysBetweenRedemption = item.TimeOutDaysBetweenRedemption,
                            MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        raffles.Add(currentRaffle);
                    }
                }
            }
            catch (Exception e)
            {
                raffles = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return raffles;
        }

        public Raffle Get(Guid id, int idType, bool filterByTenant)
        {
            Raffle raffle = null;

            try
            {
                var query = (dynamic)null;

                switch (idType)
                {
                    case RaffleIdTypes.Raffle:
                        if (filterByTenant)
                        {
                            query = from x in this._businessObjects.Context.OltprewardsView
                                    where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltprewardsView
                                    where x.Id == id
                                    select x;
                        }
                        break;
                    case RaffleIdTypes.Reward:
                        if (filterByTenant)
                        {
                            query = from x in this._businessObjects.Context.OltprewardsView
                                    where x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardId == id
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltprewardsView
                                    where x.RewardId == id
                                    select x;
                        }
                        break;
                }


                if (query != null)
                {
                    foreach (OltprewardsView item in query)
                    {
                        raffle = new Raffle
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            RewardId = item.RewardId,
                            RaffleDate = item.RaffleDate,
                            RewardName = item.RewardName,
                            Type = item.Type,
                            IsActive = item.IsActive,
                            Notes = item.Notes,
                            UsageType = item.UsageType,
                            EarningsIncreaserId = item.EarningsIncreaserId,
                            IncreaserFactor = item.EarningsIncreaserId != null ? (decimal)item.IncreaserFactor : -1,
                            MinMembershipLevel = item.MinMembershipLevel,
                            TimeOutDaysBetweenRedemption = item.TimeOutDaysBetweenRedemption,
                            MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                raffle = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return raffle;
        }

        public bool Post(Guid rewardId, DateTime raffleDate, string notes, int type, int usageType, int minMembershipLevel, int timeOutDaysBetweenRedemption, Guid? EarningsIncreaserId)
        {
            Oltprewards newRaffle = null;

            bool success;
            try
            {
                newRaffle = new Oltprewards
                {
                    Id = Guid.NewGuid(),
                    RewardId = rewardId,
                    TenantId = this._businessObjects.Tenant.TenantId,
                    RaffleDate = raffleDate,
                    Notes = notes,
                    Type = type,
                    UsageType = usageType,
                    MinMembershipLevel = minMembershipLevel,
                    TimeOutDaysBetweenRedemption = timeOutDaysBetweenRedemption,
                    EarningsIncreaserId = EarningsIncreaserId,
                    IsActive = true,//By default
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.Oltprewards.Add(newRaffle);
                this._businessObjects.Context.SaveChanges();

                success = true;
            }
            catch (Exception e)
            {
                this._businessObjects.Context.Oltprewards.Remove(newRaffle);
                this._businessObjects.Context.SaveChanges();

                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Put(Guid id, DateTime raffleDate, string notes, int minMembershipLevel, int timeOutDaysBetweenRedemption, Guid? earningsIncreaserId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltprewards
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Oltprewards raffle = null;

                    foreach (Oltprewards item in query)
                    {
                        raffle = item;
                    }

                    if (raffle != null)
                    {
                        raffle.RaffleDate = raffleDate;
                        raffle.Notes = notes;
                        raffle.MinMembershipLevel = minMembershipLevel;
                        raffle.TimeOutDaysBetweenRedemption = timeOutDaysBetweenRedemption;
                        raffle.EarningsIncreaserId = earningsIncreaserId;
                        raffle.UpdatedDate = DateTime.UtcNow;

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


        public bool Put(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltprewards
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Oltprewards raffle = null;

                    foreach (Oltprewards item in query)
                    {
                        raffle = item;
                    }

                    if (raffle != null)
                    {
                        raffle.IsActive = !raffle.IsActive;
                        raffle.UpdatedDate = DateTime.UtcNow;

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

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltprewards
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Oltprewards raffle = null;

                    foreach (Oltprewards item in query)
                    {
                        raffle = item;
                    }

                    if (raffle != null)
                    {
                        raffle.Deleted = true;

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

        #endregion

        #region RAFFLEWINNER

        public bool Post(Guid rewardId, string userId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltprewards
                            where x.RewardId == rewardId
                            select x;

                if (query != null)
                {
                    Oltprewards raffle = null;

                    foreach (Oltprewards item in query)
                    {
                        raffle = item;
                    }

                    if (raffle != null)
                    {
                        switch (raffle.Type)
                        {
                            case RaffleTypes.ByRaffle:
                                //This is just the creation of the record that indicates user has win the reward
                                OltpraffleWinners winner = new OltpraffleWinners
                                {
                                    UserId = userId,
                                    RaffleId = raffle.Id,
                                    Claimed = false,
                                    ClaimDate = null,
                                    ClaimBranchId = null,
                                    CreatedDate = DateTime.UtcNow,
                                    UpdatedDate = DateTime.UtcNow
                                };

                                this._businessObjects.Context.OltpraffleWinners.Add(winner);
                                this._businessObjects.Context.SaveChanges();

                                success = true;
                                break;
                            case RaffleTypes.Open:
                            case RaffleTypes.PerPurchases:
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

        public bool Put(Guid rewardId, long accountNumber, Guid? branchId, DateTime dateTime)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltprewards
                            where x.RewardId == rewardId
                            select x;

                if (query != null)
                {
                    Oltprewards raffle = null;

                    foreach (Oltprewards item in query)
                    {
                        raffle = item;
                    }

                    //If it's a raffle that requires that winners are selected
                    if (raffle != null && raffle.Type == RaffleTypes.ByRaffle)
                    {
                        AspNetUsers user = (from x in this._businessObjects.Context.AspNetUsers
                                            where x.AccountNumber == accountNumber
                                             select x).FirstOrDefault();

                        if(user != null)
                        {
                            OltpraffleWinners winner = (from x in this._businessObjects.Context.OltpraffleWinners
                                                             where x.RaffleId == rewardId && x.UserId == user.Id
                                                             select x).FirstOrDefault();

                            if (winner != null)
                            {
                                winner.ClaimBranchId = branchId;
                                winner.Claimed = true;//Finally user has claim it
                                winner.ClaimDate = dateTime;
                                winner.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();

                                success = true;
                            }
                        }
                        
                    }
                    else
                    {
                        //If open raffle, any user with enough points can redeem
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
        /// Creates a new ContestManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public RewardManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD TABLE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
