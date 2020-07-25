using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.Structure.POCO;

namespace YOY.DAO.Entities.Manager
{
    public class ContentLocationManager
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
        /// Retrieve all the geolocations where an offer is available
        /// </summary>
        /// <param name="dealId"></param>
        /// <returns></returns>
        public List<Pair<int, Guid>> Gets(Guid dealId)
        {
            List<Pair<int, Guid>> locations = null;

            try
            {
                var queryLocation = from x in this._businessObjects.Context.OltpcontentLocations
                                    where x.ReferenceId == dealId && (bool)x.IsActive
                                    select x;


                Pair<int, Guid> currentLocation = null;
                locations = new List<Pair<int, Guid>>();

                foreach (OltpcontentLocations location in queryLocation)
                {
                    currentLocation = new Pair<int, Guid>
                    {
                        Key = location.LocationType,
                        Value = location.LocationId
                    };

                    locations.Add(currentLocation);
                }
            }
            catch (Exception e)
            {
                locations = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return locations;
        }

        /// <summary>
        /// All the location Ids have the same locationType, and referenceType
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="locationIds"></param>
        /// <param name="referenceType"></param>
        /// <param name="locationType"></param>
        /// <returns></returns>
        public bool Post(Guid referenceId, Guid locationId, int referenceType, int locationType)
        {
            bool success;
            try
            {
                OltpcontentLocations newLocation = new OltpcontentLocations
                {
                    LocationId = locationId,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    LocationType = locationType,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };


                this._businessObjects.Context.OltpcontentLocations.Add(newLocation);
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

        public bool Put(Guid locationId, Guid referenceId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpcontentLocations
                            where x.LocationId == locationId && x.ReferenceId == referenceId
                            select x;

                if (query != null)
                {
                    OltpcontentLocations location = null;
                    foreach (OltpcontentLocations item in query)
                    {
                        location = item;
                    }

                    if (location != null)
                    {
                        location.IsActive = !location.IsActive;
                        location.UpdatedDate = DateTime.UtcNow;

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

        public bool Delete(Guid referenceId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpcontentLocations
                            where x.ReferenceId == referenceId
                            select x;

                OltpcontentLocations contentLocation = null;

                foreach (OltpcontentLocations item in query)
                {
                    contentLocation = item;
                }

                if (contentLocation != null)
                {
                    this._businessObjects.Context.OltpcontentLocations.Remove(contentLocation);
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
        /// Creates a new ContentLocationManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public ContentLocationManager(BusinessObjects businessObjects)
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
