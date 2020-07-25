using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;

namespace YOY.DAO.Entities.Manager.Misc.PushNotifications
{
    public class AppInstallationManager
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

        public List<AppInstallation> Gets()
        {
            List<AppInstallation> installations = new List<AppInstallation>();

            try
            {
                var query = from x in this._businessObjects.Context.DefappInstallationsView
                            select x;

                AppInstallation installation = null;

                foreach(DefappInstallationsView item in query)
                {
                    installation = new AppInstallation()
                    {
                        Id = item.Id,
                        InstallationId = item.InstallationId,
                        Username = item.Username,
                        DeviceType = item.DeviceType,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        LastLoginDate = item.LastDate,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AccountCode = item.AccountCode,
                        Name = item.Name,
                    };
                    installations.Add(installation);
                }
            }
            catch(Exception e)
            {
                installations = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return installations;
        }

        public List<AppInstallation> Gets(string username)
        {
            List<AppInstallation> installations = new List<AppInstallation>();

            try
            {
                var query = from x in this._businessObjects.Context.DefappInstallationsView
                            where x.Username == username
                            select x;

                AppInstallation installation = null;

                foreach (DefappInstallationsView item in query)
                {
                    installation = new AppInstallation()
                    {
                        Id = item.Id,
                        InstallationId = item.InstallationId,
                        Username = item.Username,
                        DeviceType = item.DeviceType,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        LastLoginDate = item.LastDate,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AccountCode = item.AccountCode,
                        Name = item.Name,
                    };
                    installations.Add(installation);
                }
            }
            catch (Exception e)
            {
                installations = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return installations;
        }

        public List<AppInstallation> Gets(string username, int deviceType)
        {
            List<AppInstallation> installations = new List<AppInstallation>();

            try
            {
                var query = from x in this._businessObjects.Context.DefappInstallationsView
                            where x.Username == username && x.DeviceType == deviceType
                            select x;

                AppInstallation installation = null;

                foreach (DefappInstallationsView item in query)
                {
                    installation = new AppInstallation()
                    {
                        Id = item.Id,
                        InstallationId = item.InstallationId,
                        Username = item.Username,
                        DeviceType = item.DeviceType,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        LastLoginDate = item.LastDate,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AccountCode = item.AccountCode,
                        Name = item.Name
                    };
                    installations.Add(installation);
                }
            }
            catch (Exception e)
            {
                installations = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return installations;
        }

        public AppInstallation Get(Guid id)
        {
            AppInstallation installation = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefappInstallationsView
                            where x.Id == id
                            select x;



                foreach (DefappInstallationsView item in query)
                {
                    installation = new AppInstallation()
                    {
                        Id = item.Id,
                        InstallationId = item.InstallationId,
                        Username = item.Username,
                        DeviceType = item.DeviceType,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        LastLoginDate = item.LastDate,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AccountCode = item.AccountCode,
                        Name = item.Name
                    };
                }
            }
            catch (Exception e)
            {
                installation = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return installation;
        }

        public AppInstallation Get(string installationId)
        {
            AppInstallation installation = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefappInstallationsView
                            where x.InstallationId == installationId
                            select x;



                foreach (DefappInstallationsView item in query)
                {
                    installation = new AppInstallation()
                    {
                        Id = item.Id,
                        InstallationId = item.InstallationId,
                        Username = item.Username,
                        DeviceType = item.DeviceType,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        LastLoginDate = item.LastDate,
                        UserId = item.UserId,
                        AccountNumber = item.AccountNumber,
                        AccountCode = item.AccountCode,
                        Name = item.Name
                    };
                }
            }
            catch (Exception e)
            {
                installation = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return installation;
        }

        public AppInstallation Post(string installationId, string username, int deviceType)
        {
            AppInstallation installation = null;
            bool existInstallations = false;

            try
            {

                var query = from x in this._businessObjects.Context.DefappInstallations
                            where x.Username == username && x.DeviceType == deviceType
                            select x;

                foreach(DefappInstallations item in query)
                {
                    this._businessObjects.Context.DefappInstallations.Remove(item);
                    existInstallations = true;
                }

                if (existInstallations)
                {
                    this._businessObjects.Context.SaveChanges();
                }

                DefappInstallations newInstallation = new DefappInstallations()
                {
                    Id = Guid.NewGuid(),
                    InstallationId = installationId,
                    Username = username,
                    DeviceType = deviceType,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    LastDate = DateTime.UtcNow
                };
                this._businessObjects.Context.DefappInstallations.Add(newInstallation);
                this._businessObjects.Context.SaveChanges();

                installation = new AppInstallation()
                {
                    Id = newInstallation.Id,
                    InstallationId = newInstallation.InstallationId,
                    Username = newInstallation.Username,
                    DeviceType = newInstallation.DeviceType,
                    IsActive = (bool)newInstallation.IsActive,
                    CreatedDate = newInstallation.CreatedDate,
                    LastLoginDate = newInstallation.LastDate
                };
            }
            catch (Exception e)
            {
                installation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return installation;
        }

        public AppInstallation Put(Guid id)
        {
            AppInstallation installation = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefappInstallations
                            where x.Id == id
                            select x;

                DefappInstallations currentInstallation = null;
                foreach(DefappInstallations item in query)
                {
                    currentInstallation = item;
                }
                
                if(currentInstallation != null)
                {

                    currentInstallation.LastDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    installation = new AppInstallation()
                    {
                        Id = currentInstallation.Id,
                        InstallationId = currentInstallation.InstallationId,
                        Username = currentInstallation.Username,
                        DeviceType = currentInstallation.DeviceType,
                        IsActive = (bool)currentInstallation.IsActive,
                        CreatedDate = currentInstallation.CreatedDate,
                        LastLoginDate = currentInstallation.LastDate
                    };
                }

            }
            catch (Exception e)
            {
                installation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return installation;
        }

        /// <summary>
        /// In case the installation already exists but for a different user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public AppInstallation Put(Guid id, string newValue, int valueType)
        {
            AppInstallation installation = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefappInstallations
                            where x.Id == id
                            select x;

                DefappInstallations currentInstallation = null;
                foreach (DefappInstallations item in query)
                {
                    currentInstallation = item;
                }

                if (currentInstallation != null)
                {
                    switch (valueType)
                    {
                        case InstallationRefTypes.Username:
                            currentInstallation.Username = newValue;
                            break;
                        case InstallationRefTypes.InstallationId:
                            currentInstallation.InstallationId = newValue;
                            break;
                    }

                    currentInstallation.CreatedDate = DateTime.UtcNow;
                    currentInstallation.LastDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    installation = new AppInstallation()
                    {
                        Id = currentInstallation.Id,
                        InstallationId = currentInstallation.InstallationId,
                        Username = currentInstallation.Username,
                        DeviceType = currentInstallation.DeviceType,
                        IsActive = (bool)currentInstallation.IsActive,
                        CreatedDate = currentInstallation.CreatedDate,
                        LastLoginDate = currentInstallation.LastDate
                    };
                }

            }
            catch (Exception e)
            {
                installation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return installation;
        }

        public bool Delete(string refId, int refType)
        {
            bool result = true;

            try
            {
                var query = (dynamic)null;

                switch (refType)
                {
                    case InstallationRefTypes.Id:
                        query = from x in this._businessObjects.Context.DefappInstallations
                                where x.Id == new Guid(refId)
                                select x;
                        break;
                    case InstallationRefTypes.Username:
                        query = from x in this._businessObjects.Context.DefappInstallations
                                where x.Username == refId
                                select x;
                        break;
                    case InstallationRefTypes.InstallationId:
                        query = from x in this._businessObjects.Context.DefappInstallations
                                where x.InstallationId == refId
                                select x;
                        break;
                }

                foreach(DefappInstallations item in query)
                {
                    this._businessObjects.Context.DefappInstallations.Remove(item);
                    
                }

                this._businessObjects.Context.SaveChanges();
            }
            catch(Exception e)
            {
                result = false;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return result;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new AppInstallationManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public AppInstallationManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException("businessObjects");
            } // ELSE ENDS
        } // METHOD TABLE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
