using System;
using System.Collections.Generic;
using System.Text;
using YOY.DAO.Entities.DB;
using YOY.Values;

namespace YOY.DAO.Entities.Manager
{
    public class VisitorLogManager
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

        public bool Post(string IpAddress, string hostname, string type, string continentName, string countryCode, string countryName, string regionCode, string regionName, 
            string city, string zipCode, double? ipLatitude, double? ipLongitude, double? deviceLatitude, double? deviceLongitude, int deviceType, string deviceModel, string osVersion, bool allowedAccess)
        {
            bool success;

            try
            {
                OltpvisitorsLog newLog = new OltpvisitorsLog
                {
                    Id = Guid.NewGuid(),
                    IpAddress = IpAddress,
                    Hostname = hostname,
                    Type = type,
                    ContinentName = continentName,
                    CountryCode = countryCode,
                    CountryName = countryName,
                    RegionCode = regionCode,
                    RegionName = regionName,
                    City = city,
                    ZipCode = zipCode,
                    IpLatitude = ipLatitude,
                    IpLongitude = ipLongitude,
                    VisitorLatitude = deviceLatitude,
                    VisitorLongitude = deviceLongitude,
                    DeviceType = deviceType,
                    DeviceModel = deviceModel,
                    OsVersion = osVersion,
                    AllowedAccess = allowedAccess,
                    CreatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpvisitorsLog.Add(newLog);
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
            
        #endregion


        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new OfferPreferenceManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public VisitorLogManager(BusinessObjects businessObjects)
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
