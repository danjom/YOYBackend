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
    public class SearchLogManager
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

        public List<SearchLog> Gets(string userId, DateTime start, DateTime end, Guid indexOwner)
        {
            List<SearchLog> logs = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetSearchesByUser(start, end, userId, indexOwner)
                            select x;

                SearchLog log;
                foreach (TempsearchableLogs item in query)
                {
                    log = new SearchLog
                    {
                        UserId = userId,
                        Date = DateTime.MaxValue,
                        Details = item.Details,
                        Icon = item.Icon,
                        Name = item.Name,
                        Reference = (Guid)item.ReferenceId,
                        ReferenceType = (int)item.ReferenceType,
                        SearchCount = (int)item.SearchCount
                    };
                }
            }
            catch (Exception e)
            {
                logs = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return logs;
        }

        public List<SearchLog> Gets(DateTime start, DateTime end, Guid indexOwner, Guid countryId)
        {
            List<SearchLog> logs;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetSearchesCount(start, end, indexOwner, countryId)
                            orderby x.SearchCount descending
                            select x;

                SearchLog log;
                logs = new List<SearchLog>();

                foreach (TempsearchableLogs item in query)
                {
                    log = new SearchLog
                    {
                        UserId = "",
                        Date = DateTime.MaxValue,
                        Details = item.Details,
                        Icon = item.Icon,
                        Name = item.Name,
                        Reference = (Guid)item.ReferenceId,
                        ReferenceType = (int)item.ReferenceType,
                        ContentType = (int)item.ContentType,
                        SearchCount = (int)item.SearchCount,
                        ExpirationDate = item.ExpirationDate,
                        ReleaseDate = item.ReleaseDate
                    };

                    logs.Add(log);
                }
            }
            catch (Exception e)
            {
                logs = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return logs;
        }

        public bool Post(Guid referenceId, int referenceType, Guid indexId, string userId, DateTime dateTime, int sourceType)
        {
            bool success;
            try
            {

                OltpsearchLogs log = new OltpsearchLogs
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Date = dateTime,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    IndexId = indexId,
                    SourceType = sourceType,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpsearchLogs.Add(log);
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
        /// Creates a new PaymentMethodManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public SearchLogManager(BusinessObjects businessObjects)
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
