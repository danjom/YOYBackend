using YOY.DTO.Entities;
using YOY.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class GeofenceManager
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

        public List<Geofence> Gets(int activeState, int pageSize, int pageNumber)
        {
            List<Geofence> geofences = new List<Geofence>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.DefgeofencesView
                                 where x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.DefgeofencesView
                                 where !x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.DefgeofencesView
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }

                Geofence geofence = null;

                foreach (DefgeofencesView item in query)
                {
                    geofence = new Geofence
                    {
                        Id = item.Id,
                        ZoneId = item.GeozoneId,
                        ZoneName = item.ZoneName,
                        ExternalZoneId = item.ZoneExternalId,
                        DistrictId = item.DistrictId,
                        DistrictName = item.DistrictName,
                        Name = item.Name,
                        CenterLatitude = item.CenterLatitude,
                        CenterLongitude = item.CenterLongitude,
                        Radius = item.Radius,
                        ExternalId = item.ExternalId,
                        Label = item.Label,
                        ActionType = item.ActionType,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    geofences.Add(geofence);
                }
            }
            catch (Exception e)
            {
                geofences = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geofences;
        }

        /// <summary>
        /// Retrieves all the geofences for a tenant or a zone, depending in referenceType
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="referenceType"></param>
        /// <returns></returns>
        public List<Geofence> Gets(Guid referenceId, int referenceType, int pageSize, int pageNumber)
        {
            List<Geofence> geofences = new List<Geofence>();

            try
            {
                var query = (dynamic)null;

                switch (referenceType)
                {
                    case GeofenceReferenceTypes.TenantId:
                        query = (from x in this._businessObjects.Context.DefgeofencesView
                                 where x.BranchTenantId == referenceId
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);

                        Geofence geofence = null;

                        foreach (DefgeofencesView item in query)
                        {
                            if (item.Id != null)
                            {
                                geofence = new Geofence
                                {
                                    Id = item.Id,
                                    ZoneId = item.GeozoneId,
                                    ZoneName = item.ZoneName,
                                    ExternalZoneId = item.ZoneExternalId,
                                    DistrictId = item.DistrictId,
                                    DistrictName = item.DistrictName,
                                    Name = item.Name,
                                    CenterLatitude = item.CenterLatitude,
                                    CenterLongitude = item.CenterLongitude,
                                    Radius = item.Radius,
                                    ExternalId = item.ExternalId,
                                    Label = item.Label,
                                    ActionType = item.ActionType,
                                    IsActive = item.IsActive,
                                    CreatedDate = item.CreatedDate,
                                    UpdatedDate = item.UpdatedDate
                                };


                                geofences.Add(geofence);
                            }

                        }
                        break;
                    case GeofenceReferenceTypes.ZoneId:
                        query = (from x in this._businessObjects.Context.DefgeofencesView
                                 where x.GeozoneId == referenceId
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);

                        geofence = null;

                        foreach (DefgeofencesView item in query)
                        {
                            geofence = new Geofence
                            {
                                Id = item.Id,
                                ZoneId = item.GeozoneId,
                                ZoneName = item.ZoneName,
                                ExternalZoneId = item.ZoneExternalId,
                                DistrictId = item.DistrictId,
                                DistrictName = item.DistrictName,
                                Name = item.Name,
                                CenterLatitude = item.CenterLatitude,
                                CenterLongitude = item.CenterLongitude,
                                Radius = item.Radius,
                                ExternalId = item.ExternalId,
                                Label = item.Label,
                                ActionType = item.ActionType,
                                IsActive = item.IsActive,
                                CreatedDate = item.CreatedDate,
                                UpdatedDate = item.UpdatedDate
                            };

                            geofences.Add(geofence);
                        }
                        break;
                }

            }
            catch (Exception e)
            {
                geofences = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geofences;
        }

        /// <summary>
        /// Retrieves all geofences of a district
        /// </summary>
        /// <returns></returns>
        public List<Geofence> Gets(Guid districtId, int pageSize, int pageNumber)
        {
            List<Geofence> geofences = new List<Geofence>();

            try
            {
                var query = (from x in this._businessObjects.Context.DefgeofencesView
                             where x.IsActive && x.DistrictId == districtId
                             orderby x.Name ascending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                Geofence geofence = null;
                foreach (DefgeofencesView item in query)
                {
                    geofence = new Geofence()
                    {
                        Id = item.Id,
                        ZoneId = item.GeozoneId,
                        ZoneName = item.ZoneName,
                        ExternalZoneId = item.ZoneExternalId,
                        DistrictId = item.DistrictId,
                        DistrictName = item.DistrictName,
                        Name = item.Name,
                        CenterLatitude = item.CenterLatitude,
                        CenterLongitude = item.CenterLongitude,
                        Radius = item.Radius,
                        ExternalId = item.ExternalId,
                        Label = item.Label,
                        ActionType = item.ActionType,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };


                    geofences.Add(geofence);
                }
            }
            catch (Exception e)
            {
                geofences = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geofences;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves a geofence based on its Id or based in a geotrigger's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idType"></param>
        /// <returns></returns>
        public Geofence Get(Guid referenceId, int referenceType)
        {
            Geofence geofence = null;

            try
            {
                var query = (dynamic)null;

                switch (referenceType)
                {
                    case GeofenceFilters.Geofence:
                        query = from x in this._businessObjects.Context.DefgeofencesView
                                where x.Id == referenceId
                                select x;

                        foreach (DefgeofencesView item in query)
                        {
                            geofence = new Geofence
                            {
                                Id = item.Id,
                                ZoneId = item.GeozoneId,
                                ZoneName = item.ZoneName,
                                ExternalZoneId = item.ZoneExternalId,
                                DistrictId = item.DistrictId,
                                DistrictName = item.DistrictName,
                                Name = item.Name,
                                CenterLatitude = item.CenterLatitude,
                                CenterLongitude = item.CenterLongitude,
                                Radius = item.Radius,
                                ExternalId = item.ExternalId,
                                Label = item.Label,
                                ActionType = item.ActionType,
                                IsActive = item.IsActive,
                                CreatedDate = item.CreatedDate,
                                UpdatedDate = item.UpdatedDate
                            };
                        }
                        break;
                    case GeofenceFilters.Geotrigger:
                        query = from x in this._businessObjects.Context.DefgeofencesFromGeoTriggerView
                                where x.Id == referenceId
                                select x;

                        foreach (DefgeofencesFromGeoTriggerView item in query)
                        {
                            geofence = new Geofence
                            {
                                Id = item.Id,
                                ZoneId = item.GeozoneId,
                                ZoneName = item.ZoneName,
                                ExternalZoneId = item.ZoneExternalId,
                                DistrictId = item.DistrictId,
                                DistrictName = item.DistrictName,
                                Name = item.Name,
                                CenterLatitude = item.CenterLatitude,
                                CenterLongitude = item.CenterLongitude,
                                Radius = item.Radius,
                                ExternalId = item.ExternalId,
                                Label = item.Label,
                                ActionType = item.ActionType,
                                IsActive = item.IsActive,
                                CreatedDate = item.CreatedDate,
                                UpdatedDate = item.UpdatedDate
                            };
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                geofence = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geofence;
        }

        public Geofence Get(string externalId)
        {
            Geofence geofence = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefgeofencesView
                            where x.ExternalId == externalId
                            select x;


                foreach (DefgeofencesView item in query)
                {
                    geofence = new Geofence
                    {
                        Id = item.Id,
                        ZoneId = item.GeozoneId,
                        ZoneName = item.ZoneName,
                        ExternalZoneId = item.ZoneExternalId,
                        DistrictId = item.DistrictId,
                        DistrictName = item.DistrictName,
                        Name = item.Name,
                        CenterLatitude = item.CenterLatitude,
                        CenterLongitude = item.CenterLongitude,
                        Radius = item.Radius,
                        ExternalId = item.ExternalId,
                        Label = item.Label,
                        ActionType = item.ActionType,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };
                }
            }
            catch (Exception e)
            {
                geofence = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geofence;
        }

        public Geofence Post(string name, Guid zoneId, Guid districtId, int actionType, decimal centerLatitude, decimal centerLongitude, decimal radius, string externalId, string label)
        {
            Geofence geofence = null;
            try
            {
                Defgeofences newGeofence = new Defgeofences
                {
                    Id = Guid.NewGuid(),
                    GeozoneId = zoneId,
                    DistrictId = districtId,
                    Name = name,
                    CenterLatitude = centerLatitude,
                    CenterLongitude = centerLongitude,
                    Radius = radius,
                    ExternalId = externalId,
                    Label = label,
                    ActionType = actionType,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.Defgeofences.Add(newGeofence);
                this._businessObjects.Context.SaveChanges();

                DefgeofencesView newGeofenceView = (from x in this._businessObjects.Context.DefgeofencesView
                                                    where x.Id == newGeofence.Id
                                                    select x).FirstOrDefault();

                if(newGeofenceView != null)
                {

                    geofence = new Geofence
                    {
                        Id = newGeofenceView.Id,
                        ZoneId = newGeofenceView.GeozoneId,
                        ZoneName = newGeofenceView.ZoneName,
                        ExternalZoneId = newGeofenceView.ZoneExternalId,
                        DistrictId = newGeofenceView.DistrictId,
                        DistrictName = newGeofenceView.DistrictName,
                        Name = newGeofenceView.Name,
                        CenterLatitude = newGeofenceView.CenterLatitude,
                        CenterLongitude = newGeofenceView.CenterLongitude,
                        Radius = newGeofenceView.Radius,
                        ExternalId = newGeofenceView.ExternalId,
                        Label = newGeofenceView.Label,
                        ActionType = newGeofenceView.ActionType,
                        IsActive = newGeofenceView.IsActive,
                        CreatedDate = newGeofenceView.CreatedDate,
                        UpdatedDate = newGeofenceView.UpdatedDate
                    };
                }


            }
            catch (Exception e)
            {
                geofence = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geofence;
        }

        public Geofence Put(Guid id, string externalId, string name, Guid districtId, int actionType, decimal centerLatitude, decimal centerLongitude, decimal radius, string label)
        {
            Geofence geofence = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defgeofences
                            where x.Id == id
                            select x;

                foreach (Defgeofences item in query)
                {
                    item.Name = name;
                    item.ExternalId = externalId;
                    item.DistrictId = districtId;
                    item.CenterLatitude = centerLatitude;
                    item.CenterLongitude = centerLongitude;
                    item.Radius = radius;
                    item.Label = label;
                    item.ActionType = actionType;
                    item.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    DefgeofencesView itemView = (from x in this._businessObjects.Context.DefgeofencesView
                                                 where x.Id == item.Id
                                                 select x).FirstOrDefault();

                    if(itemView != null)
                    {
                        geofence = new Geofence
                        {
                            Id = itemView.Id,
                            ZoneId = itemView.GeozoneId,
                            ZoneName = itemView.ZoneName,
                            ExternalZoneId = itemView.ZoneExternalId,
                            DistrictId = itemView.DistrictId,
                            DistrictName = itemView.DistrictName,
                            Name = itemView.Name,
                            CenterLatitude = itemView.CenterLatitude,
                            CenterLongitude = itemView.CenterLongitude,
                            Radius = itemView.Radius,
                            ExternalId = itemView.ExternalId,
                            Label = itemView.Label,
                            ActionType = itemView.ActionType,
                            IsActive = itemView.IsActive,
                            CreatedDate = itemView.CreatedDate,
                            UpdatedDate = itemView.UpdatedDate
                        };
                    }

                }


            }
            catch (Exception e)
            {
                geofence = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geofence;
        }

        public bool Put(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defgeofences
                            where x.Id == id
                            select x;

                foreach (Defgeofences item in query)
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
                Defgeofences geofence = (from x in this._businessObjects.Context.Defgeofences
                                        where x.Id == id
                                        select x).FirstOrDefault();

                if (geofence != null)
                {
                    geofence.Deleted = true;
                    geofence.UpdatedDate = DateTime.UtcNow;

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
        /// Creates a new GeofenceManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public GeofenceManager(BusinessObjects businessObjects)
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
