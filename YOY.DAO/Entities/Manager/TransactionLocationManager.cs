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
    public class TransactionLocationManager
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
        /// Gets all the records based on the given reference id and type
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="referenceType"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public List<TransactionLocation> Gets(Guid referenceId, int referenceType, int? actionType, int pageSize, int pageNumber)
        {
            List<TransactionLocation> locationRecords = new List<TransactionLocation>();

            try
            {
                var query = (dynamic)null;

                switch (referenceType)
                {
                    case TransactionLocationReferenceTypes.Broadcaster:
                        if (actionType != null)
                        {
                            query = (from x in this._businessObjects.Context.OltptransactionLocations
                                     where x.BroadcasterId == referenceId && x.ActionType == actionType
                                     orderby x.CreatedDate ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.OltptransactionLocations
                                     where x.BroadcasterId == referenceId
                                     orderby x.CreatedDate ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }

                        break;
                    case TransactionLocationReferenceTypes.Transaction:
                        if (actionType != null)
                        {
                            query = (from x in this._businessObjects.Context.OltptransactionLocations
                                     where x.TransactionId == referenceId && x.ActionType == actionType
                                     orderby x.CreatedDate ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.OltptransactionLocations
                                     where x.TransactionId == referenceId
                                     orderby x.CreatedDate ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }

                        break;
                }

                TransactionLocation locationRecord = null;

                foreach (OltptransactionLocations item in query)
                {
                    locationRecord = new TransactionLocation
                    {
                        Id = item.Id,
                        TransactionId = item.TransactionId,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        BranchName = item.BranchName,
                        BroadcasterId = item.BroadcasterId,
                        BroadcasterName = item.BroadcasterName,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ActionType = item.ActionType,
                        Valid = item.Valid
                    };

                    locationRecords.Add(locationRecord);
                }
            }
            catch (Exception e)
            {
                locationRecords = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return locationRecords;
        }

        /// <summary>
        /// Retrieve all the records for a given tenant in a date range
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public List<TransactionLocation> Gets(Guid tenantId, DateTime start, DateTime end, int? actionType, int pageSize, int pageNumber)
        {
            List<TransactionLocation> locationRecords = new List<TransactionLocation>();

            try
            {
                var query = (dynamic)null;

                if (actionType != null)
                {
                    query = (from x in this._businessObjects.Context.OltptransactionLocations
                             where x.TenantId == tenantId && x.ActionType == actionType && x.CreatedDate >= start && x.CreatedDate <= end
                             orderby x.CreatedDate ascending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }
                else
                {
                    query = (from x in this._businessObjects.Context.OltptransactionLocations
                             where x.TenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end
                             orderby x.CreatedDate ascending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }


                TransactionLocation locationRecord = null;

                foreach (OltptransactionLocations item in query)
                {
                    locationRecord = new TransactionLocation
                    {
                        Id = item.Id,
                        TransactionId = item.TransactionId,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        BranchName = item.BranchName,
                        BroadcasterId = item.BroadcasterId,
                        BroadcasterName = item.BroadcasterName,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ActionType = item.ActionType,
                        Valid = item.Valid
                    };

                    locationRecords.Add(locationRecord);
                }
            }
            catch (Exception e)
            {
                locationRecords = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return locationRecords;
        }


        /// <summary>
        /// Gets all the records for a given tenant and a beacon on a date range
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="beaconId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<TransactionLocation> Gets(Guid tenantId, Guid beaconId, DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            List<TransactionLocation> locationRecords = new List<TransactionLocation>();

            try
            {
                var query = (from x in this._businessObjects.Context.OltptransactionLocations
                             where x.TenantId == tenantId && x.BroadcasterId == beaconId && x.CreatedDate >= start && x.CreatedDate <= end
                             orderby x.CreatedDate ascending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);


                TransactionLocation locationRecord = null;

                foreach (OltptransactionLocations item in query)
                {
                    locationRecord = new TransactionLocation
                    {
                        Id = item.Id,
                        TransactionId = item.TransactionId,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        BranchName = item.BranchName,
                        BroadcasterId = item.BroadcasterId,
                        BroadcasterName = item.BroadcasterName,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ActionType = item.ActionType,
                        Valid = item.Valid
                    };

                    locationRecords.Add(locationRecord);
                }
            }
            catch (Exception e)
            {
                locationRecords = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return locationRecords;
        }

        /// <summary>
        /// Gets and specific record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionLocation Get(Guid id)
        {
            TransactionLocation locationRecord = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltptransactionLocations
                            where x.Id == id
                            select x;

                foreach (OltptransactionLocations item in query)
                {
                    locationRecord = new TransactionLocation
                    {
                        Id = item.Id,
                        TransactionId = item.TransactionId,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        BranchName = item.BranchName,
                        BroadcasterId = item.BroadcasterId,
                        BroadcasterName = item.BroadcasterName,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ActionType = item.ActionType,
                        Valid = item.Valid
                    };

                }
            }
            catch (Exception e)
            {
                locationRecord = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }
            return locationRecord;
        }

        public TransactionLocation Post(Guid transactionId, Guid tenantId, Guid? branchId, string branchName, Guid? broadcasterId, string broadcasterName, double? latitude, double? longitude, int actionType, bool valid)
        {
            TransactionLocation locationRecord;
            try
            {
                OltptransactionLocations newLocationRecord = new OltptransactionLocations
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transactionId,
                    TenantId = tenantId,
                    BranchId = branchId,
                    BranchName = branchName,
                    BroadcasterId = broadcasterId,
                    BroadcasterName = broadcasterName,
                    Latitude = latitude,
                    Longitude = longitude,
                    ActionType = actionType,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Valid = valid
                };

                this._businessObjects.Context.OltptransactionLocations.Add(newLocationRecord);
                this._businessObjects.Context.SaveChanges();

                locationRecord = new TransactionLocation
                {
                    Id = newLocationRecord.Id,
                    TransactionId = newLocationRecord.TransactionId,
                    TenantId = newLocationRecord.TenantId,
                    BranchId = newLocationRecord.BranchId,
                    BranchName = newLocationRecord.BranchName,
                    BroadcasterId = newLocationRecord.BroadcasterId,
                    BroadcasterName = newLocationRecord.BroadcasterName,
                    Latitude = newLocationRecord.Latitude,
                    Longitude = newLocationRecord.Longitude,
                    ActionType = newLocationRecord.ActionType,
                    CreatedDate = newLocationRecord.CreatedDate,
                    UpdatedDate = newLocationRecord.UpdatedDate,
                    Valid = newLocationRecord.Valid
                };

            }
            catch (Exception e)
            {
                locationRecord = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return locationRecord;
        }

        #endregion
        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new TransactionLocation with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public TransactionLocationManager(BusinessObjects businessObjects)
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
