using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DAO.Entities.Manager.Misc.InteractionMetrics;
using YOY.Values;

namespace YOY.DAO.Entities.Manager
{
    public class UserInteractionMetricManager
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

        public bool Post(string userId, int referenceType,  Guid referenceId, string locationData, int timeInSeconds)
        {
            bool success;

            try
            {
                OltpuserInteractionMetrics interactionMetrics = new OltpuserInteractionMetrics
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ReferenceType = referenceType,
                    ReferenceId = referenceId,
                    LocationData = locationData,
                    TimeInSeconds = timeInSeconds,
                    CreatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpuserInteractionMetrics.Add(interactionMetrics);
                this._businessObjects.Context.SaveChanges();

                success = true;
            }
            catch(Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
             }

            return success;
        }

        public List<UserInteractionMetricsForReference> Gets(string userId, DateTime minDate, DateTime maxDate)
        {
            List<UserInteractionMetricsForReference> metricsForReferences = new List<UserInteractionMetricsForReference>();

            try
            {
                List<TempuserInteractionTimeMetrics> interactionTimeMetrics = this._businessObjects.FuncsHandler.GetUserInteractionMetricsTotalTimeByReferences(userId, minDate, maxDate);

                if (interactionTimeMetrics?.Count > 0)
                {
                    UserInteractionMetricsForReference metricForReference;

                    foreach(TempuserInteractionTimeMetrics item in interactionTimeMetrics)
                    {
                        metricForReference = new UserInteractionMetricsForReference
                        {
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            UserId = item.UserId,
                            MetricsCount = item.MetricsCount,
                            TotalTimeInSeconds = item.TotalTimeInSeconds
                        };

                        metricsForReferences.Add(metricForReference);
                    }
                }
            }
            catch(Exception e)
            {
                metricsForReferences = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return metricsForReferences;
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
        public UserInteractionMetricManager(BusinessObjects businessObjects)
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
