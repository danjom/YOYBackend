using YOY.DTO.Entities.Manager.Misc.InterestPreference;
using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.Values;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class UserInterestManager
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

        /// <summary>
        /// Retrieves all the interests count from a specific user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public int Gets(long accountNumber, int activeState)
        {
            int count = 0;

            try
            {

                switch (activeState)
                {
                    case ActiveStates.All:

                        count = (from x in this._businessObjects.Context.OltpuserInterestsView
                                 where x.AccountNumber == accountNumber
                                 orderby x.Score descending
                                 select x).Count();

                        break;
                    case ActiveStates.Active:

                        count = (from x in this._businessObjects.Context.OltpuserInterestsView
                                 where x.AccountNumber == accountNumber && x.IsActive
                                 orderby x.Score descending
                                 select x).Count();

                        break;
                    case ActiveStates.Inactive:

                        count = (from x in this._businessObjects.Context.OltpuserInterestsView
                                 where x.AccountNumber == accountNumber && !x.IsActive
                                 orderby x.Score descending
                                 select x).Count();

                        break;

                }


            }
            catch (Exception e)
            {
                count = -1;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return count;
        }


        /// <summary>
        /// Retrieves all the interests from a specific user for a specific type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public List<UserInterest> Gets(string userId, int type, int activeState)
        {
            List<UserInterest> interests = new List<UserInterest>();

            try
            {
                var queryTenants = (dynamic)null;
                var queryCategories = (dynamic)null;

                if (type != InterestTypes.All)
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:

                            switch (type)
                            {
                                case InterestTypes.Category:
                                    queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryInterestsForUser(userId)
                                                      orderby x.Score descending, x.Relevance descending
                                                      select x;
                                    break;
                                case InterestTypes.Tenant:
                                    queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                                   orderby x.Score descending, x.Relevance descending
                                                   select x;
                                    break;
                            }


                            break;
                        case ActiveStates.Active:

                            switch (type)
                            {
                                case InterestTypes.Category:
                                    queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryInterestsForUser(userId)
                                                      where x.IsActive == true
                                                      orderby x.Score descending, x.Relevance descending
                                                      select x;
                                    break;
                                case InterestTypes.Tenant:
                                    queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                                   where x.IsActive == true
                                                   orderby x.Score descending, x.Relevance descending
                                                   select x;
                                    break;
                            }


                            break;
                        case ActiveStates.Inactive:

                            switch (type)
                            {
                                case InterestTypes.Category:
                                    queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryInterestsForUser(userId)
                                                      where x.IsActive == false
                                                      orderby x.Score descending, x.Relevance descending
                                                      select x;
                                    break;
                                case InterestTypes.Tenant:
                                    queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                                   where x.IsActive == false
                                                   orderby x.Score descending, x.Relevance descending
                                                   select x;
                                    break;
                            }

                            break;
                    }
                }
                else
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:

                            queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryInterestsForUser(userId)
                                              orderby x.Score descending, x.Relevance descending
                                              select x;

                            queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                           orderby x.Score descending, x.Relevance descending
                                           select x;

                            break;
                        case ActiveStates.Active:

                            queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryInterestsForUser(userId)
                                              where x.IsActive == true
                                              orderby x.Score descending, x.Relevance descending
                                              select x;

                            queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                           where x.IsActive == true
                                           orderby x.Score descending, x.Relevance descending
                                           select x;

                            break;
                        case ActiveStates.Inactive:

                            queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryInterestsForUser(userId)
                                              where x.IsActive == false
                                              orderby x.Score descending, x.Relevance descending
                                              select x;

                            queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                           where x.IsActive == false
                                           orderby x.Score descending, x.Relevance descending
                                           select x;

                            break;

                    }
                }


                UserInterest currentInterest = null;

                if (queryCategories != null)
                {

                    foreach (Temppreferences item in queryCategories)
                    {
                        currentInterest = new UserInterest
                        {
                            InterestId = item.InterestId,
                            UserId = item.UserId,
                            CreatedDate = DateTime.Now,//THIS IS BY DEFAULT
                            UpdatedDate = DateTime.Now,//THIS IS BY DEFAULT
                            InterestType = (int)item.InterestType,
                            IsActive = (bool)item.IsActive,
                            Score = item.Score != null ? (decimal)item.Score : -1,
                            OriginType = (int)item.OriginType,
                            HerarchyLevel = (int)item.HerarchyLevel,
                            InterestName = item.Name,
                            Icon = item.Icon
                        };

                        interests.Add(currentInterest);

                    }
                }


            }
            catch (Exception e)
            {
                interests = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return interests;
        }

        /// <summary>
        /// Retrieves all the interest records related to an specific interestId
        /// </summary>
        /// <param name="interestId"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public List<UserInterest> Gets(Guid interestId, int activeState)
        {
            List<UserInterest> interests = new List<UserInterest>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        query = from x in this._businessObjects.Context.OltpuserInterestsView
                                where x.InterestId == interestId
                                orderby x.Score descending
                                select x;

                        break;
                    case ActiveStates.Active:

                        query = from x in this._businessObjects.Context.OltpuserInterestsView
                                where x.InterestId == interestId && x.IsActive
                                orderby x.Score descending
                                select x;

                        break;
                    case ActiveStates.Inactive:

                        query = from x in this._businessObjects.Context.OltpuserInterestsView
                                where x.InterestId == interestId && !x.IsActive
                                orderby x.Score descending
                                select x;

                        break;
                }


                if (query != null)
                {
                    UserInterest currentInterest = null;
                    var queryInterest = (dynamic)null;

                    foreach (OltpuserInterestsView item in query)
                    {
                        currentInterest = new UserInterest
                        {
                            InterestId = item.InterestId,
                            UserId = item.UserId,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            InterestType = item.InterestType,
                            IsActive = item.IsActive,
                            Score = item.Score,
                            OriginType = item.OriginType,
                            HerarchyLevel = item.HerarchyLevel
                        };

                        switch (item.InterestType)
                        {
                            case InterestTypes.Category:
                                queryInterest = from x in this._businessObjects.Context.Oltpcategories
                                                where x.Id == item.InterestId
                                                select x;

                                if (queryInterest != null)
                                {
                                    foreach (Oltpcategories interestItem in queryInterest)
                                    {
                                        currentInterest.InterestName = interestItem.Name;
                                        currentInterest.Icon = interestItem.Icon;
                                    }
                                }

                                break;
                            case InterestTypes.Tenant:
                                queryInterest = from x in this._businessObjects.Context.DeftenantInformations
                                                where x.TenantId == item.InterestId
                                                select x;

                                if (queryInterest != null)
                                {
                                    foreach (DeftenantInformations interestItem in queryInterest)
                                    {
                                        currentInterest.InterestName = interestItem.Name;
                                        currentInterest.Icon = interestItem.Logo + "";
                                    }
                                }

                                break;
                        }

                    }
                }


            }
            catch (Exception e)
            {
                interests = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return interests;
        }


        /// <summary>
        /// Retrieves all the interest records related to an specific interestId
        /// </summary>
        /// <param name="interestId"></param>
        /// <returns></returns>
        public UserInterest Get(string userId, Guid interestId, int interestType)
        {
            UserInterest interest = null;

            try
            {
                var query = (dynamic)null;

                switch (interestType)
                {
                    case InterestTypes.Category:
                        query = from x in this._businessObjects.FuncsHandler.GetCategoryInterestForUser(userId, interestId)
                                select x;
                        break;
                    case InterestTypes.Tenant:
                        query = from x in this._businessObjects.FuncsHandler.GetTenantInterestForUser(userId, interestId)
                                select x;
                        break;
                }


                if (query != null)
                {

                    foreach (Temppreferences item in query)
                    {
                        interest = new UserInterest
                        {
                            InterestId = item.InterestId,
                            UserId = item.UserId,
                            CreatedDate = DateTime.UtcNow,//THIS IS BY DEFAULT
                            UpdatedDate = DateTime.UtcNow,//THIS IS BY DEFAULT
                            InterestType = (int)item.InterestType,
                            IsActive = (bool)item.IsActive,
                            Score = item.Score != null ? (decimal)item.Score : -1,
                            OriginType = (int)item.OriginType,
                            HerarchyLevel = (int)item.HerarchyLevel,
                            InterestName = item.Name,
                            Icon = item.Icon
                        };

                    }
                }
                else
                {
                    interest = null;
                }


            }
            catch (Exception e)
            {
                interest = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return interest;
        }

        /// <summary>
        /// Creates a new match for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="interestId"></param>
        /// <param name="interestType"></param>
        /// <returns></returns>
        public UserInterest Post(string userId, Guid interestId, int interestType, int originType, int herarchyLevel)
        {
            UserInterest interest = null;

            try
            {

                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();//New context is created because this call is part of an async logic

                //Before creating the user interest, 1st needs to verify it hasn't been created already by computational processing
                //because user can modify his interests at anytime, it's possible that eventually he selects an interest that has been
                //already created from his behaviour

                var query = from x in context.OltpuserInterests
                            where x.InterestType == interestType && x.InterestId == interestId && x.UserId == userId
                            select x;

                OltpuserInterests newInterest = null;
                OltpuserInterestRecords newInterestRecord = null;

                foreach (OltpuserInterests item in query)
                {
                    newInterest = item;
                }

                int score = 0;

                switch (interestType)
                {
                    case InterestTypes.Tenant:
                        score = InterestScoreBasePoints.TenantInterestCreation;
                        break;
                    case InterestTypes.Category:
                        score = InterestScoreBasePoints.ProductCatgoryInterestCreation;
                        break;
                }

                //If the interest hasn't been created by user or by computational processing
                if (newInterest == null)
                {
                    newInterest = new OltpuserInterests
                    {
                        UserId = userId,
                        InterestId = interestId,
                        InterestType = interestType,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        AdditionRecordsCount = 1,
                        IsActive = true,
                        Score = score,
                        OriginType = originType,
                        HerarchyLevel = herarchyLevel
                    };
                    //Active by default


                    this._businessObjects.Context.OltpuserInterests.Add(newInterest);
                }
                else
                {
                    //If the interest was created by computational processing
                    if (newInterest.OriginType == InterestOrigintTypes.MachineLearning)
                    {
                        newInterest.OriginType = InterestOrigintTypes.UserSelection;
                        newInterest.Score += score;
                        ++newInterest.AdditionRecordsCount;
                    }
                }

                DateTime currentUtcDate = DateTime.UtcNow;

                //Now creates the interest record
                newInterestRecord = new OltpuserInterestRecords
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    InterestId = interestId,
                    InterestType = interestType,
                    HerarchyLevel = herarchyLevel,
                    Score = score,
                    CreatedDate = currentUtcDate,
                    UpdatedDate = currentUtcDate,
                    Hour = currentUtcDate.Hour,
                    Day = currentUtcDate.Day,
                    DayOfWeek = (int)currentUtcDate.DayOfWeek,
                    Month = currentUtcDate.Month,
                    Year = currentUtcDate.Year
                };

                this._businessObjects.Context.OltpuserInterestRecords.Add(newInterestRecord);

                this._businessObjects.Context.SaveChanges();

                var queryInterest = (dynamic)null;

                switch (newInterest.InterestType)
                {
                    case InterestTypes.Category:
                        queryInterest = from x in this._businessObjects.FuncsHandler.GetCategoryInterestForUser(userId, interestId)
                                        where x.InterestId == interestId
                                        select x;
                        break;
                    case InterestTypes.Tenant:
                        queryInterest = from x in this._businessObjects.FuncsHandler.GetTenantInterestForUser(userId, interestId)
                                        where x.InterestId == interestId
                                        select x;
                        break;
                }

                if (query != null)
                {

                    foreach (Temppreferences item in queryInterest)
                    {
                        interest = new UserInterest
                        {
                            InterestId = item.InterestId,
                            UserId = item.UserId,
                            CreatedDate = DateTime.UtcNow,//THIS IS BY DEFAULT
                            UpdatedDate = DateTime.UtcNow,
                            InterestType = (int)item.InterestType,
                            IsActive = (bool)item.IsActive,
                            Score = item.Score != null ? (decimal)item.Score : -1,
                            OriginType = (int)item.OriginType,
                            HerarchyLevel = (int)item.HerarchyLevel,
                            InterestName = item.Name,
                            Icon = item.Icon
                        };

                    }
                }
                else
                {
                    interest = null;
                }
            }
            catch (Exception e)
            {
                interest = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return interest;
        }

        /// <summary>
        /// Modifies the interest score based on actions user does
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="interestId"></param>
        /// <param name="quantity"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public bool Put(string userId, Guid interestId, int score, int operation)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpuserInterests
                            where x.UserId == userId && x.InterestId == interestId
                            select x;

                OltpuserInterests interest = null;
                if (query != null)
                {
                    foreach (OltpuserInterests item in query)
                    {
                        interest = item;
                    }
                }

                if (interest != null)
                {

                    DateTime currentUtcDate = DateTime.UtcNow;
                    OltpuserInterestRecords newInterestRecord = null;

                    switch (operation)
                    {
                        case ArithmeticOps.Add:
                            interest.Score += score;
                            ++interest.AdditionRecordsCount;
                            interest.UpdatedDate = DateTime.UtcNow;

                            //Now creates the interest record
                            newInterestRecord = new OltpuserInterestRecords
                            {
                                Id = Guid.NewGuid(),
                                UserId = interest.UserId,
                                InterestId = interest.InterestId,
                                InterestType = interest.InterestType,
                                HerarchyLevel = interest.HerarchyLevel,
                                Score = score,
                                CreatedDate = currentUtcDate,
                                UpdatedDate = currentUtcDate,
                                Hour = currentUtcDate.Hour,
                                Day = currentUtcDate.Day,
                                DayOfWeek = (int)currentUtcDate.DayOfWeek,
                                Month = currentUtcDate.Month,
                                Year = currentUtcDate.Year
                            };

                            this._businessObjects.Context.OltpuserInterestRecords.Add(newInterestRecord);

                            this._businessObjects.Context.SaveChanges();
                            break;
                        case ArithmeticOps.Minus:
                            interest.Score -= score;
                            interest.UpdatedDate = DateTime.UtcNow;
                            ++interest.SubstractionRecordsCount;

                            //Now creates the interest record
                            newInterestRecord = new OltpuserInterestRecords
                            {
                                Id = Guid.NewGuid(),
                                UserId = interest.UserId,
                                InterestId = interest.InterestId,
                                InterestType = interest.InterestType,
                                HerarchyLevel = interest.HerarchyLevel,
                                Score = score * -1,
                                CreatedDate = currentUtcDate,
                                UpdatedDate = currentUtcDate,
                                Hour = currentUtcDate.Hour,
                                Day = currentUtcDate.Day,
                                DayOfWeek = (int)currentUtcDate.DayOfWeek,
                                Month = currentUtcDate.Month,
                                Year = currentUtcDate.Year
                            };

                            this._businessObjects.Context.OltpuserInterestRecords.Add(newInterestRecord);

                            this._businessObjects.Context.SaveChanges();
                            break;
                    }

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

        /// <summary>
        /// Updates interest active state
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="interestId"></param>
        /// <returns></returns>
        public bool Put(string userId, Guid interestId)
        {
            bool success = false;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();//New context is created because this call is part of an async logic


                var query = from x in context.OltpuserInterests
                            where x.UserId == userId && x.InterestId == interestId
                            select x;

                OltpuserInterests interest = null;
                if (query != null)
                {
                    foreach (OltpuserInterests item in query)
                    {
                        interest = item;
                    }
                }

                if (interest != null)
                {
                    interest.IsActive = !interest.IsActive;
                    interest.UpdatedDate = DateTime.UtcNow;
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

        #region PREFERENCES

        public List<UserInterest> GetPreferencesForUser(string userId, int type, int activeState)
        {
            List<UserInterest> interests = new List<UserInterest>();

            try
            {
                var queryTenants = (dynamic)null;
                var queryCategories = (dynamic)null;

                if (type != InterestTypes.All)
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:

                            switch (type)
                            {
                                case InterestTypes.Category:
                                    queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryPreferencesForUser(userId)
                                                      orderby x.Score descending, x.Relevance descending
                                                      select x;
                                    break;
                                case InterestTypes.Tenant:
                                    queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                                   orderby x.Score descending, x.Relevance descending
                                                   select x;
                                    break;
                            }


                            break;
                        case ActiveStates.Active:

                            switch (type)
                            {
                                case InterestTypes.Category:
                                    queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryPreferencesForUser(userId)
                                                      where x.IsActive == true
                                                      orderby x.Score descending, x.Relevance descending
                                                      select x;
                                    break;
                                case InterestTypes.Tenant:
                                    queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                                   where x.IsActive == true
                                                   orderby x.Score descending, x.Relevance descending
                                                   select x;
                                    break;
                            }


                            break;
                        case ActiveStates.Inactive:

                            switch (type)
                            {
                                case InterestTypes.Category:
                                    queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryPreferencesForUser(userId)
                                                      where x.IsActive == false
                                                      orderby x.Score descending, x.Relevance descending
                                                      select x;
                                    break;
                                case InterestTypes.Tenant:
                                    queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                                   where x.IsActive == false
                                                   orderby x.Score descending, x.Relevance descending
                                                   select x;
                                    break;
                            }

                            break;
                    }
                }
                else
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:

                            queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryPreferencesForUser(userId)
                                              orderby x.Score descending, x.Relevance descending
                                              select x;

                            queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                           orderby x.Score descending, x.Relevance descending
                                           select x;

                            break;
                        case ActiveStates.Active:

                            queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryPreferencesForUser(userId)
                                              where x.IsActive == true
                                              orderby x.Score descending, x.Relevance descending
                                              select x;

                            queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                           where x.IsActive == true
                                           orderby x.Score descending, x.Relevance descending
                                           select x;

                            break;
                        case ActiveStates.Inactive:

                            queryCategories = from x in this._businessObjects.FuncsHandler.GetCategoryPreferencesForUser(userId)
                                              where x.IsActive == false
                                              orderby x.Score descending, x.Relevance descending
                                              select x;

                            queryTenants = from x in this._businessObjects.FuncsHandler.GetTenantInterestsForUser(userId)
                                           where x.IsActive == false
                                           orderby x.Score descending, x.Relevance descending
                                           select x;

                            break;

                    }
                }


                UserInterest currentInterest = null;

                if (queryCategories != null)
                {

                    foreach (Temppreferences item in queryCategories)
                    {
                        currentInterest = new UserInterest
                        {
                            InterestId = item.InterestId,
                            UserId = item.UserId,
                            CreatedDate = DateTime.UtcNow,//THIS IS BY DEFAULT
                            UpdatedDate = DateTime.UtcNow,
                            InterestType = (int)item.InterestType,
                            IsActive = (bool)item.IsActive,
                            Score = item.Score != null ? (decimal)item.Score : -1,
                            OriginType = (int)item.OriginType,
                            HerarchyLevel = (int)item.HerarchyLevel,
                            InterestName = item.Name,
                            Icon = item.Icon
                        };

                        interests.Add(currentInterest);

                    }
                }

                if (queryTenants != null)
                {

                    foreach (Temppreferences item in queryTenants)
                    {
                        currentInterest = new UserInterest
                        {
                            InterestId = item.InterestId,
                            UserId = item.UserId,
                            CreatedDate = DateTime.UtcNow,//THIS IS BY DEFAULT
                            UpdatedDate = DateTime.UtcNow,
                            InterestType = (int)item.InterestType,
                            IsActive = (bool)item.IsActive,
                            Score = item.Score != null ? (decimal)item.Score : -1,
                            OriginType = (int)item.OriginType,
                            HerarchyLevel = (int)item.HerarchyLevel,
                            InterestName = item.Name,
                            Icon = item.Icon
                        };

                        interests.Add(currentInterest);

                    }
                }


            }
            catch (Exception e)
            {
                interests = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return interests;
        }

        public List<UserPreferenceData> GetPreferences(string userId)
        {
            List<UserPreferenceData> preferences = null;

            var query = from x in this._businessObjects.FuncsHandler.GetCategoryPreferences(userId)
                        orderby x.Relevance descending, x.Name ascending
                        select x;

            if (query != null)
            {
                preferences = new List<UserPreferenceData>();
                UserPreferenceData preference = null;
                foreach (Temppreferences item in query)
                {
                    if (!string.IsNullOrWhiteSpace(item.Icon))
                    {
                        preference = new UserPreferenceData()
                        {
                            Id = item.InterestId,
                            Name = item.Name,
                            BaseImgUrl = item.Icon,
                            Type = InterestTypes.Category,
                        };

                        if (item.UserId != null)
                        {
                            preference.IsSelected = item.IsActive ?? false;
                        }
                        else
                        {
                            preference.IsSelected = false;
                        }

                        preferences.Add(preference);
                    }

                }
            }

            return preferences;
        }

        public List<UserPreferenceData> GetPreferences(string userId, Guid countryId, double latitude, double longitude, double radius, int pageSize, int pageNumber)
        {
            List<UserPreferenceData> preferences = null;

            var query = from x in this._businessObjects.FuncsHandler.GetTenantPreferencesByGeoLocation(latitude, longitude, radius, countryId, userId, pageSize, pageNumber)
                         orderby x.Relevance descending, x.Name ascending
                         select x;

            if (query != null)
            {
                preferences = new List<UserPreferenceData>();
                UserPreferenceData preference = null;
                foreach (Temppreferences item in query)
                {
                    preference = new UserPreferenceData()
                    {
                        Id = item.InterestId,
                        Name = item.Name,
                        BaseImgUrl = item.Icon,
                        Type = InterestTypes.Tenant,
                    };

                    if (item.UserId != null)
                    {
                        preference.IsSelected = item.IsActive ?? false;
                    }
                    else
                    {
                        preference.IsSelected = false;
                    }

                    preferences.Add(preference);
                }
            }

            return preferences;
        }

        public List<UserPreferenceData> GetPreferences(string userId, Guid regionId, int contentSegmentationType, int pageSize, int pageNumber)
        {
            List<UserPreferenceData> preferences = null;

            var query = (dynamic)null;

            switch (contentSegmentationType)
            {
                case GeoSegmentationTypes.Country:
                    query = from x in this._businessObjects.FuncsHandler.GetTenantPreferencesByCountry(userId, regionId, pageSize, pageNumber)
                             orderby x.Relevance descending, x.Name ascending
                             select x;
                    break;
                case GeoSegmentationTypes.State:
                    query = from x in this._businessObjects.FuncsHandler.GetTenantPreferencesByState(userId, regionId, pageSize, pageNumber)
                             orderby x.Relevance descending, x.Name ascending
                             select x;
                    break;
            }

            if (query != null)
            {
                preferences = new List<UserPreferenceData>();
                UserPreferenceData preference = null;
                foreach (Temppreferences item in query)
                {
                    preference = new UserPreferenceData()
                    {
                        Id = item.InterestId,
                        Name = item.Name,
                        BaseImgUrl = item.Icon,
                        Type = InterestTypes.Tenant,
                    };

                    if (item.UserId != null)
                    {
                        preference.IsSelected = item.IsActive ?? false;
                    }
                    else
                    {
                        preference.IsSelected = false;
                    }

                    preferences.Add(preference);
                }
            }

            return preferences;
        }

        public List<UserPreferenceData> GetPreferences(string userId, Guid regionId, int contentSegmentationType, double latitude, double longitude, double radius, int pageSize, int pageNumber)
        {
            List<UserPreferenceData> preferences = null;

            var query = (dynamic)null;

            switch (contentSegmentationType)
            {
                case GeoSegmentationTypes.Country:

                    query = from x in this._businessObjects.FuncsHandler.GetTenantPreferencesByCountryAndLocation(latitude, longitude, radius, userId, regionId, pageSize, pageNumber)
                             orderby x.Relevance descending, x.Name ascending
                             select x;
                    break;
                case GeoSegmentationTypes.State:

                    query = from x in this._businessObjects.FuncsHandler.GetTenantPreferencesByStateAndLocation(latitude, longitude, radius, userId, regionId, pageSize, pageNumber)
                             orderby x.Relevance descending, x.Name ascending
                             select x;
                    break;
            }

            if (query != null)
            {
                preferences = new List<UserPreferenceData>();
                UserPreferenceData preference = null;
                foreach (Temppreferences item in query)
                {
                    preference = new UserPreferenceData()
                    {
                        Id = item.InterestId,
                        Name = item.Name,
                        BaseImgUrl = item.Icon,
                        Type = InterestTypes.Tenant,
                    };

                    if (item.UserId != null)
                    {
                        preference.IsSelected = item.IsActive ?? false;
                    }
                    else
                    {
                        preference.IsSelected = false;
                    }

                    preferences.Add(preference);
                }
            }

            return preferences;
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
        public UserInterestManager(BusinessObjects businessObjects)
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