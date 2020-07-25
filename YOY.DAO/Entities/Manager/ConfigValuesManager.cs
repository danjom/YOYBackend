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
    public class ConfigValuesManager
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
        /// Gets the most recent enabled tuple
        /// </summary>
        /// <returns></returns>
        public ConfigValues Get()
        {
            ConfigValues tuple = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefconfigValues
                            where (bool)x.Enabled
                            orderby x.CreatedDate ascending
                            select x;

                foreach (DefconfigValues item in query)
                {
                    tuple = new ConfigValues
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Enabled = (bool)item.Enabled,
                        LastestAndroidVersion = item.LastestAndroidVersion,
                        LastestiOSVersion = item.LastestiOsversion,
                        LastestWebVersion = item.LastestWebVersion,
                        LastestFBVersion = item.LastestFbversion,
                        SupportEmail = item.SupportEmail,
                        SupportNumber = item.SupportNumber,
                        CompanyName = item.CompanyName,
                        AppName = item.AppName
                    };
                }
            }
            catch (Exception e)
            {
                tuple = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tuple;
        }//ENDS GET METHOD

        #endregion


        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new ConfigValueManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public ConfigValuesManager(BusinessObjects businessObjects)
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
