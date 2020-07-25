using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.Location;
using YOY.Values;

namespace YOY.DAO.Entities.Manager.Misc
{
    public class CityManager
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

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS METHODS                                                                                                                                  //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        #region METHODS

        /// <summary>
        /// Retrieves all cities of a state
        /// </summary>
        /// <returns></returns>
        public List<City> Gets(Guid stateId)
        {
            List<City> cities = new List<City>();

            try
            {
                var query = from x in this._businessObjects.Context.Defcities
                    where (bool)x.IsActive && x.StateId == stateId
                    orderby x.Name ascending
                    select x;

                City city = null;
                foreach (Defcities item in query)
                {
                    city = new City()
                    {
                        Id = item.Id,
                        StateId = item.StateId,
                        Name = item.Name,
                        UtcDiffTime = item.UtctimeDifference,
                        IsActive = (bool)item.IsActive
                    };
                    
                    cities.Add(city);
                }
            }
            catch (Exception e)
            {
                cities = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return cities;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieve a city
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public City Get(Guid cityId)
        {
            City city = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defcities
                            where x.Id == cityId
                            select x;

                foreach (var item in query)
                {
                    city = new City()
                    {
                        Id = item.Id,
                        StateId = item.StateId,
                        Name = item.Name,
                        UtcDiffTime = item.UtctimeDifference,
                        IsActive = (bool)item.IsActive
                    };
                }
            }
            catch (Exception e)
            {
                city = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return city;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Retrieve a city
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public City Get(Guid stateId, string name)
        {
            City city = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defcities
                            where x.StateId == stateId && x.Name == name
                            select x;

                foreach (var item in query)
                {
                    city = new City()
                    {
                        Id = item.Id,
                        StateId = item.StateId,
                        Name = item.Name,
                        UtcDiffTime = item.UtctimeDifference,
                        IsActive = (bool)item.IsActive
                    };
                }
            }
            catch (Exception e)
            {
                city = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return city;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new FileManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public CityManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException("businessObjects");
            } // ELSE ENDS
        } // METHOD FILE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}