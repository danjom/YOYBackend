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
    public class GeotriggerManager
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

        public List<Geotrigger> Gets(int activeState, int pageSize, int pageNumber)
        {
            List<Geotrigger> geotriggers = new List<Geotrigger>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.DefgeotriggersView
                                 where x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.DefgeotriggersView
                                 where !x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.DefgeotriggersView
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }

                Geotrigger geotrigger = null;

                foreach (DefgeotriggersView item in query)
                {
                    geotrigger = new Geotrigger
                    {
                        Id = item.Id,
                        GeofenceId = item.GeofenceId,
                        ExternalId = item.ExternalId,
                        Name = item.Name,
                        ExternalGeofenceId = item.GeofenceExternalId,
                        GeofenceName = item.GeofenceName,
                        GeofenceCenterLatitude = item.GeofenceCenterLatitude,
                        GeofenceCenterLongitude = item.GeofenceCenterLongitude,
                        GeofenceRadius = item.GeofenceRadius,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        TriggerType = item.TriggerType
                    };

                    geotriggers.Add(geotrigger);
                }
            }
            catch (Exception e)
            {
                geotriggers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geotriggers;
        }

        public List<Geotrigger> Gets(Guid fenceId, int activeState, int pageSize, int pageNumber)
        {
            List<Geotrigger> geotriggers = new List<Geotrigger>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.DefgeotriggersView
                                 where x.GeofenceId == fenceId && x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.DefgeotriggersView
                                 where x.GeofenceId == fenceId && !x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.DefgeotriggersView
                                 where x.GeofenceId == fenceId
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                Geotrigger geotrigger = null;

                foreach (DefgeotriggersView item in query)
                {
                    geotrigger = new Geotrigger
                    {
                        Id = item.Id,
                        GeofenceId = item.GeofenceId,
                        ExternalId = item.ExternalId,
                        Name = item.Name,
                        ExternalGeofenceId = item.GeofenceExternalId,
                        GeofenceName = item.GeofenceName,
                        GeofenceCenterLatitude = item.GeofenceCenterLatitude,
                        GeofenceCenterLongitude = item.GeofenceCenterLongitude,
                        GeofenceRadius = item.GeofenceRadius,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        TriggerType = item.TriggerType
                    };

                    geotriggers.Add(geotrigger);
                }
            }
            catch (Exception e)
            {
                geotriggers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geotriggers;
        }

        public Geotrigger Get(Guid id)
        {
            Geotrigger geozone = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefgeotriggersView
                            where x.Id == id
                            select x;


                foreach (DefgeotriggersView item in query)
                {
                    geozone = new Geotrigger
                    {
                        Id = item.Id,
                        GeofenceId = item.GeofenceId,
                        ExternalId = item.ExternalId,
                        Name = item.Name,
                        ExternalGeofenceId = item.GeofenceExternalId,
                        GeofenceName = item.GeofenceName,
                        GeofenceCenterLatitude = item.GeofenceCenterLatitude,
                        GeofenceCenterLongitude = item.GeofenceCenterLongitude,
                        GeofenceRadius = item.GeofenceRadius,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        TriggerType = item.TriggerType
                    };
                }
            }
            catch (Exception e)
            {
                geozone = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geozone;
        }

        public Geotrigger Get(string externalId)
        {
            Geotrigger geozone = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefgeotriggersView
                            where x.ExternalId == externalId
                            select x;


                foreach (DefgeotriggersView item in query)
                {
                    geozone = new Geotrigger
                    {
                        Id = item.Id,
                        GeofenceId = item.GeofenceId,
                        ExternalId = item.ExternalId,
                        Name = item.Name,
                        ExternalGeofenceId = item.GeofenceExternalId,
                        GeofenceName = item.GeofenceName,
                        GeofenceCenterLatitude = item.GeofenceCenterLatitude,
                        GeofenceCenterLongitude = item.GeofenceCenterLongitude,
                        GeofenceRadius = item.GeofenceRadius,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        TriggerType = item.TriggerType
                    };
                }
            }
            catch (Exception e)
            {
                geozone = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geozone;
        }

        public Geotrigger Post(Guid geofenceId, string externalId, string name, int triggerType)
        {
            Geotrigger geotrigger = null;
            try
            {
                Defgeotriggers newGeotrigger = new Defgeotriggers
                {
                    Id = Guid.NewGuid(),
                    GeofenceId = geofenceId,
                    Name = name,
                    ExternalId = externalId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    TriggerType = triggerType
                };

                this._businessObjects.Context.Defgeotriggers.Add(newGeotrigger);
                this._businessObjects.Context.SaveChanges();

                DefgeotriggersView newGeoTriggerView = (from x in this._businessObjects.Context.DefgeotriggersView
                                                        where x.Id == newGeotrigger.Id
                                                        select x).FirstOrDefault();

                if (newGeoTriggerView != null)
                {
                    geotrigger = new Geotrigger
                    {
                        Id = newGeoTriggerView.Id,
                        GeofenceId = newGeoTriggerView.GeofenceId,
                        ExternalId = newGeoTriggerView.ExternalId,
                        Name = newGeoTriggerView.Name,
                        ExternalGeofenceId = newGeoTriggerView.GeofenceExternalId,
                        GeofenceName = newGeoTriggerView.GeofenceName,
                        GeofenceCenterLatitude = newGeoTriggerView.GeofenceCenterLatitude,
                        GeofenceCenterLongitude = newGeoTriggerView.GeofenceCenterLongitude,
                        GeofenceRadius = newGeoTriggerView.GeofenceRadius,
                        IsActive = newGeoTriggerView.IsActive,
                        CreatedDate = newGeoTriggerView.CreatedDate,
                        UpdatedDate = newGeoTriggerView.UpdatedDate,
                        TriggerType = newGeoTriggerView.TriggerType
                    };
                }
                
            }
            catch (Exception e)
            {
                geotrigger = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geotrigger;
        }

        public Geotrigger Put(Guid id, string externalId, string name, bool enabled, int triggerType)
        {
            Geotrigger geotrigger = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defgeotriggers
                            where x.Id == id
                            select x;

                foreach (Defgeotriggers item in query)
                {
                    item.Name = name;
                    item.ExternalId = externalId;
                    item.TriggerType = triggerType;
                    item.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    DefgeotriggersView itemView = (from x in this._businessObjects.Context.DefgeotriggersView
                                                            where x.Id == item.Id
                                                            select x).FirstOrDefault();

                    if (itemView != null)
                    {
                        geotrigger = new Geotrigger
                        {
                            Id = itemView.Id,
                            GeofenceId = itemView.GeofenceId,
                            ExternalId = itemView.ExternalId,
                            Name = itemView.Name,
                            ExternalGeofenceId = itemView.GeofenceExternalId,
                            GeofenceName = itemView.GeofenceName,
                            GeofenceCenterLatitude = itemView.GeofenceCenterLatitude,
                            GeofenceCenterLongitude = itemView.GeofenceCenterLongitude,
                            GeofenceRadius = itemView.GeofenceRadius,
                            IsActive = itemView.IsActive,
                            CreatedDate = itemView.CreatedDate,
                            UpdatedDate = itemView.UpdatedDate,
                            TriggerType = itemView.TriggerType
                        };
                    }
                }


            }
            catch (Exception e)
            {
                geotrigger = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geotrigger;
        }

        public bool Put(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defgeotriggers
                            where x.Id == id
                            select x;

                foreach (Defgeotriggers item in query)
                {
                    item.IsActive = !item.IsActive;
                    item.UpdatedDate = DateTime.UtcNow;

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

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defgeotriggers
                            where x.Id == id
                            select x;

                Defgeotriggers geotrigger = null;

                foreach (Defgeotriggers item in query)
                {
                    geotrigger = item;
                }

                if (geotrigger != null)
                {
                    this._businessObjects.Context.Defgeotriggers.Remove(geotrigger);
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
        /// Creates a new GeotriggerManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public GeotriggerManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD GEOZONE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
