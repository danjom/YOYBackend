using YOY.DTO.Entities;
using YOY.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class GeozoneManager
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

        public List<Geozone> Gets(int activeState, int pageSize, int pageNumber)
        {
            List<Geozone> geozones = new List<Geozone>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.DefgeozonesView
                                 where x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.DefgeozonesView
                                 where !x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.DefgeozonesView
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }

                Geozone geozone = null;

                foreach (DefgeozonesView item in query)
                {
                    geozone = new Geozone
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ExternalId = item.ExternalId,
                        MinRetriggeredMins = item.MinRetriggeredMins,
                        IsActive = item.IsActive,
                        CountryId = item.CountryId,
                        CountryName = item.CountryName,
                        DistrictId = item.DistrictId,
                        DistrictName = item.DistrictName ?? "",
                        DescriptiveAddress = item.DescriptiveAddress,
                        LocationAddress = XElement.Parse(item.LocationAddress),
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    geozones.Add(geozone);
                }
            }
            catch (Exception e)
            {
                geozones = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return geozones;
        }

        public Geozone Get(Guid id)
        {
            Geozone geozone = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefgeozonesView
                            where x.Id == id
                            select x;


                foreach (DefgeozonesView item in query)
                {
                    geozone = new Geozone
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ExternalId = item.ExternalId,
                        MinRetriggeredMins = item.MinRetriggeredMins,
                        IsActive = item.IsActive,
                        CountryId = item.CountryId,
                        CountryName = item.CountryName,
                        DistrictId = item.DistrictId,
                        DistrictName = item.DistrictName ?? "",
                        DescriptiveAddress = item.DescriptiveAddress,
                        LocationAddress = XElement.Parse(item.LocationAddress),
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
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

        public Geozone Post(string externalId, string name, int minRetriggeredMins, Guid countryId, Guid districtId, string descriptiveAddress, XElement locationAddress)
        {
            Geozone geozone = null;
            try
            {
                //XElements needs to be checked when set to database object!!!!!!!!!!!!
                Defgeozones newGeozone = new Defgeozones
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    ExternalId = externalId,
                    MinRetriggeredMins = minRetriggeredMins,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,
                    CountryId = countryId,
                    DistrictId = districtId,
                    DescriptiveAddress = descriptiveAddress,
                    LocationAddress = locationAddress.ToString()
                };

                this._businessObjects.Context.Defgeozones.Add(newGeozone);
                this._businessObjects.Context.SaveChanges();

                DefgeozonesView newGeozoneView = (from x in this._businessObjects.Context.DefgeozonesView
                                                  where x.Id == newGeozone.Id
                                                  select x).FirstOrDefault();

                if(newGeozoneView != null)
                {

                    geozone = new Geozone
                    {
                        Id = newGeozoneView.Id,
                        Name = newGeozoneView.Name,
                        ExternalId = newGeozoneView.ExternalId,
                        MinRetriggeredMins = newGeozoneView.MinRetriggeredMins,
                        IsActive = newGeozoneView.IsActive,
                        CountryId = newGeozoneView.CountryId,
                        CountryName = newGeozoneView.CountryName,
                        DistrictId = newGeozoneView.DistrictId,
                        DistrictName = newGeozoneView.DistrictName ?? "",
                        DescriptiveAddress = newGeozoneView.DescriptiveAddress,
                        LocationAddress = XElement.Parse(newGeozoneView.LocationAddress),
                        CreatedDate = newGeozoneView.CreatedDate,
                        UpdatedDate = newGeozoneView.UpdatedDate
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

        public Geozone Put(Guid id, string externalId, string name, int minRetriggeredMins, Guid district, string descriptiveAddress, XElement locationAddress)
        {
            Geozone geozone = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defgeozones
                            where x.Id == id
                            select x;

                foreach (Defgeozones item in query)
                {
                    item.Name = name;
                    item.ExternalId = externalId;
                    item.MinRetriggeredMins = minRetriggeredMins;
                    item.DistrictId = district;
                    item.DescriptiveAddress = descriptiveAddress;
                    item.LocationAddress = locationAddress.ToString();
                    item.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    DefgeozonesView itemView = (from x in this._businessObjects.Context.DefgeozonesView
                                                      where x.Id == item.Id
                                                      select x).FirstOrDefault();

                    if (itemView != null)
                    {

                        geozone = new Geozone
                        {
                            Id = itemView.Id,
                            Name = itemView.Name,
                            ExternalId = itemView.ExternalId,
                            MinRetriggeredMins = itemView.MinRetriggeredMins,
                            IsActive = itemView.IsActive,
                            CountryId = itemView.CountryId,
                            CountryName = itemView.CountryName,
                            DistrictId = itemView.DistrictId,
                            DistrictName = itemView.DistrictName ?? "",
                            DescriptiveAddress = itemView.DescriptiveAddress,
                            LocationAddress = XElement.Parse(itemView.LocationAddress),
                            CreatedDate = itemView.CreatedDate,
                            UpdatedDate = itemView.UpdatedDate
                        };
                    }
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

        public bool Put(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defgeozones
                            where x.Id == id
                            select x;

                foreach (Defgeozones item in query)
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
                var query = from x in this._businessObjects.Context.Defgeozones
                            where x.Id == id
                            select x;

                Defgeozones geozone = null;

                foreach (Defgeozones item in query)
                {
                    geozone = item;
                }

                if (geozone != null)
                {
                    this._businessObjects.Context.Defgeozones.Remove(geozone);
                    this._businessObjects.Context.SaveChanges();
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
        /// Creates a new GeozoneManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public GeozoneManager(BusinessObjects businessObjects)
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
