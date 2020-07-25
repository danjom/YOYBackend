using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.Location;
using YOY.Values;

namespace YOY.DAO.Entities.Manager.Misc
{
    public class CountryManager
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
        /// Retrieves all countries
        /// </summary>
        /// <returns></returns>
        public List<Country> Gets()
        {
            List<Country> countries = new List<Country>();

            try
            {
                var query = from x in this._businessObjects.Context.Defcountries
                            where (bool)x.IsActive
                            orderby x.Name ascending
                            select x;

                Country country = null;
                foreach (var item in query)
                {
                    country = new Country()
                    {
                        Id = item.Id,
                        Code = item.Code,
                        PhoneNumberPrefix = item.PhoneNumberPrefix,
                        CurrencySymbol = item.CurrencySymbol,
                        CurrencyType = item.CurrencyType,
                        Name = item.Name,
                        IsActive = (bool)item.IsActive,
                        Flag = item.Flag
                    };
                    

                    countries.Add(country);
                }
            }
            catch (Exception e)
            {
                countries = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return countries;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves a country by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Country Get(Guid id)
        {
            Country country = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defcountries
                            where x.Id == id
                            select x;

                foreach (var item in query)
                {
                    country = new Country()
                    {
                        Id = item.Id,
                        Code = item.Code,
                        PhoneNumberPrefix = item.PhoneNumberPrefix,
                        CurrencySymbol = item.CurrencySymbol,
                        CurrencyType = item.CurrencyType,
                        Name = item.Name,
                        IsActive = (bool)item.IsActive,
                        Flag = item.Flag
                    };
                }
            }
            catch (Exception e)
            {
                country = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return country;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Retrieves a country by its code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Country Get(string code)
        {
            Country country = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defcountries
                            where x.Code == code
                            select x;

                foreach (var item in query)
                {
                    country = new Country()
                    {
                        Id = item.Id,
                        Code = item.Code,
                        PhoneNumberPrefix = item.PhoneNumberPrefix,
                        CurrencySymbol = item.CurrencySymbol,
                        CurrencyType = item.CurrencyType,
                        Name = item.Name,
                        IsActive = (bool)item.IsActive,
                        Flag = item.Flag
                    };
                }
            }
            catch (Exception e)
            {
                country = null;
                // ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return country;
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
        public CountryManager(BusinessObjects businessObjects)
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
