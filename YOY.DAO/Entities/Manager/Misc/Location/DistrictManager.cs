using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.Location;
using YOY.Values;

namespace YOY.DAO.Entities.Manager.Misc
{
    public class DistrictManager
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
        /// Retrieves all districts of a city
        /// </summary>
        /// <returns></returns>
        public List<District> Gets(Guid cityId)
        {
            List<District> districts = new List<District>();

            try
            {
                var query = from x in this._businessObjects.Context.Defdistricts
                    where (bool)x.IsActive && x.CityId == cityId
                    orderby x.Name ascending
                    select x;

                District district = null;
                foreach (var item in query)
                {
                    district = new District()
                    {
                        Id = item.Id,
                        CityId = item.CityId,
                        Name = item.Name,
                        IsActive = (bool)item.IsActive
                    };
                    

                    districts.Add(district);
                }
            }
            catch (Exception e)
            {
                districts = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return districts;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieve a city
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public District Get(Guid districtId)
        {
            District district = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defdistricts
                            where x.Id == districtId
                            select x;

                foreach (var item in query)
                {
                    district = new District()
                    {
                        Id = item.Id,
                        CityId = item.CityId,
                        Name = item.Name,
                        IsActive = (bool)item.IsActive
                    };
                }
            }
            catch (Exception e)
            {
                district = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return district;
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
        public DistrictManager(BusinessObjects businessObjects)
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
