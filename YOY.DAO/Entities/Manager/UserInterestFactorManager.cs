using YOY.DTO.Entities;
using YOY.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class UserInterestFactorManager
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

        public List<UserInterestFactor> Gets(Guid interestId, int interestType, int activeState, int expireState)
        {
            List<UserInterestFactor> interestRelevances = null;

            try
            {

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (expireState)
                        {
                            case ExpiredStates.All:
                                query = from x in this._businessObjects.Context.DefuserInterestFactors
                                        where x.InterestId == interestId && x.InterestType == interestType
                                        select x;
                                break;
                            case ExpiredStates.Valid:
                                query = from x in this._businessObjects.Context.DefuserInterestFactors
                                        where x.InterestId == interestId && x.InterestType == interestType && x.ExpirationDate > DateTime.UtcNow
                                        select x;
                                break;
                            case ExpiredStates.Expired:
                                query = from x in this._businessObjects.Context.DefuserInterestFactors
                                        where x.InterestId == interestId && x.InterestType == interestType && x.ExpirationDate <= DateTime.UtcNow
                                        select x;
                                break;
                        }

                        break;
                    case ActiveStates.Active:
                        switch (expireState)
                        {
                            case ExpiredStates.All:
                                query = from x in this._businessObjects.Context.DefuserInterestFactors
                                        where x.InterestId == interestId && x.InterestType == interestType && (bool)x.IsActive
                                        select x;
                                break;
                            case ExpiredStates.Valid:
                                query = from x in this._businessObjects.Context.DefuserInterestFactors
                                        where x.InterestId == interestId && x.InterestType == interestType && (bool)x.IsActive && x.ExpirationDate > DateTime.UtcNow
                                        select x;
                                break;
                            case ExpiredStates.Expired:
                                query = from x in this._businessObjects.Context.DefuserInterestFactors
                                        where x.InterestId == interestId && x.InterestType == interestType && (bool)x.IsActive && x.ExpirationDate <= DateTime.UtcNow
                                        select x;
                                break;
                        }

                        break;
                    case ActiveStates.Inactive:
                        switch (expireState)
                        {
                            case ExpiredStates.All:
                                query = from x in this._businessObjects.Context.DefuserInterestFactors
                                        where x.InterestId == interestId && x.InterestType == interestType && !(bool)x.IsActive
                                        select x;
                                break;
                            case ExpiredStates.Valid:
                                query = from x in this._businessObjects.Context.DefuserInterestFactors
                                        where x.InterestId == interestId && x.InterestType == interestType && !(bool)x.IsActive && x.ExpirationDate > DateTime.UtcNow
                                        select x;
                                break;
                            case ExpiredStates.Expired:
                                query = from x in this._businessObjects.Context.DefuserInterestFactors
                                        where x.InterestId == interestId && x.InterestType == interestType && !(bool)x.IsActive && x.ExpirationDate <= DateTime.UtcNow
                                        select x;
                                break;
                        }

                        break;
                }

                UserInterestFactor interestRelevance = null;

                foreach (DefuserInterestFactors item in query)
                {
                    interestRelevance = new UserInterestFactor
                    {
                        Id = item.Id,
                        InterestId = item.InterestId,
                        InterestType = item.InterestType,
                        Factor = item.Factor,
                        MonthsRange = item.MonthsRange,
                        DatesRange = item.DatesRange,
                        DaysOfWeekRange = item.DaysOfWeekRange,
                        HoursInterval = item.HoursRange,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = (bool)item.IsActive,
                        ExpirationDate = item.ExpirationDate
                    };

                    interestRelevances.Add(interestRelevance);
                }
            }
            catch (Exception e)
            {
                interestRelevances = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return interestRelevances;
        }

        public UserInterestFactor Get(Guid id)
        {
            UserInterestFactor interestRelevance = null;

            try
            {

                var query = from x in this._businessObjects.Context.DefuserInterestFactors
                            where x.Id == id
                            select x;


                foreach (DefuserInterestFactors item in query)
                {
                    interestRelevance = new UserInterestFactor
                    {
                        Id = item.Id,
                        InterestId = item.InterestId,
                        InterestType = item.InterestType,
                        Factor = item.Factor,
                        MonthsRange = item.MonthsRange,
                        DatesRange = item.DatesRange,
                        DaysOfWeekRange = item.DaysOfWeekRange,
                        HoursInterval = item.HoursRange,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = (bool)item.IsActive,
                        ExpirationDate = item.ExpirationDate
                    };
                }
            }
            catch (Exception e)
            {
                interestRelevance = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return interestRelevance;
        }

        public UserInterestFactor Post(Guid interestId, int interestType, double factor, string monthsRange, string datesRange, string daysOfWeekRange, string hoursRange, DateTime? expirationDate)
        {
            UserInterestFactor interestRelevance;
            try
            {
                DefuserInterestFactors newInterestRelevance = new DefuserInterestFactors
                {
                    Id = Guid.NewGuid(),
                    InterestId = interestId,
                    InterestType = interestType,
                    MonthsRange = monthsRange,
                    DatesRange = datesRange,
                    DaysOfWeekRange = daysOfWeekRange,
                    HoursRange = hoursRange,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    ExpirationDate = expirationDate,
                    Factor = factor
                };

                this._businessObjects.Context.DefuserInterestFactors.Add(newInterestRelevance);
                this._businessObjects.Context.SaveChanges();

                interestRelevance = new UserInterestFactor
                {
                    Id = newInterestRelevance.Id,
                    InterestId = newInterestRelevance.InterestId,
                    InterestType = newInterestRelevance.InterestType,
                    Factor = newInterestRelevance.Factor,
                    MonthsRange = newInterestRelevance.MonthsRange,
                    DatesRange = newInterestRelevance.DatesRange,
                    DaysOfWeekRange = newInterestRelevance.DaysOfWeekRange,
                    HoursInterval = newInterestRelevance.HoursRange,
                    CreatedDate = newInterestRelevance.CreatedDate,
                    UpdatedDate = newInterestRelevance.UpdatedDate,
                    IsActive = (bool)newInterestRelevance.IsActive,
                    ExpirationDate = newInterestRelevance.ExpirationDate
                };
            }
            catch (Exception e)
            {
                interestRelevance = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return interestRelevance;
        }

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefuserInterestFactors
                            where x.Id == id
                            select x;

                DefuserInterestFactors relevanceToDelete = null;

                foreach (DefuserInterestFactors item in query)
                {
                    relevanceToDelete = item;
                }

                if (relevanceToDelete != null)
                {
                    this._businessObjects.Context.DefuserInterestFactors.Remove(relevanceToDelete);
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
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new TableManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public UserInterestFactorManager(BusinessObjects businessObjects)
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
