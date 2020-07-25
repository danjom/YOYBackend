using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class BroadcastingPlayerLogManager
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

        public List<BroadcastingPlayerLog> Gets(Guid branchId, DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            List<BroadcastingPlayerLog> logs = null;

            try
            {
                var query = (from x in this._businessObjects.Context.OltpbroadcastingPlayerLogsView
                             where x.BranchId == branchId && x.EventDate >= start && x.EventDate <= end
                             orderby x.EventDate descending
                             select x).Skip(pageNumber * pageSize).Take(pageSize);

                if (query != null)
                {
                    BroadcastingPlayerLog log;
                    logs = new List<BroadcastingPlayerLog>();

                    foreach (OltpbroadcastingPlayerLogsView item in query)
                    {
                        log = new BroadcastingPlayerLog
                        {
                            Id = item.Id,
                            BeaconId = item.BroadcasterId,
                            BeaconName = item.BroadcasterName,
                            BranchId = item.BranchId,
                            BranchName = item.BranchName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            EmployeeId = item.EmployeeId,
                            EmployeeName = item.EmployeeName,
                            EmployeeEmail = item.EmployeeEmail,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            EventDate = item.EventDate,
                            EventType = item.EventType,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        logs.Add(log);
                    }
                }
            }
            catch (Exception e)
            {
                logs = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return logs;
        }

        public List<BroadcastingPlayerLog> Gets(Guid branchId, Guid departmentId, DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            List<BroadcastingPlayerLog> logs = null;

            try
            {
                var query = (from x in this._businessObjects.Context.OltpbroadcastingPlayerLogsView
                             where x.BranchId == branchId && x.DepartmentId == departmentId && x.EventDate >= start && x.EventDate <= end
                             orderby x.EventDate descending
                             select x).Skip(pageNumber * pageSize).Take(pageSize);

                if (query != null)
                {
                    BroadcastingPlayerLog log;
                    logs = new List<BroadcastingPlayerLog>();

                    foreach (OltpbroadcastingPlayerLogsView item in query)
                    {
                        log = new BroadcastingPlayerLog
                        {
                            Id = item.Id,
                            BeaconId = item.BroadcasterId,
                            BeaconName = item.BroadcasterName,
                            BranchId = item.BranchId,
                            BranchName = item.BranchName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            EmployeeId = item.EmployeeId,
                            EmployeeName = item.EmployeeName,
                            EmployeeEmail = item.EmployeeEmail,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            EventDate = item.EventDate,
                            EventType = item.EventType,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        logs.Add(log);
                    }
                }
            }
            catch (Exception e)
            {
                logs = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return logs;
        }

        public List<BroadcastingPlayerLog> Gets(Guid branchId, Guid departmentId, Guid beaconId, DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            List<BroadcastingPlayerLog> logs = null;

            try
            {
                var query = (from x in this._businessObjects.Context.OltpbroadcastingPlayerLogsView
                             where x.BranchId == branchId && x.DepartmentId == departmentId && x.BroadcasterId == beaconId && x.EventDate >= start && x.EventDate <= end
                             orderby x.EventDate descending
                             select x).Skip(pageNumber * pageSize).Take(pageSize);

                if (query != null)
                {
                    BroadcastingPlayerLog log;
                    logs = new List<BroadcastingPlayerLog>();

                    foreach (OltpbroadcastingPlayerLogsView item in query)
                    {
                        log = new BroadcastingPlayerLog
                        {
                            Id = item.Id,
                            BeaconId = item.BroadcasterId,
                            BeaconName = item.BroadcasterName,
                            BranchId = item.BranchId,
                            BranchName = item.BranchName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            EmployeeId = item.EmployeeId,
                            EmployeeName = item.EmployeeName,
                            EmployeeEmail = item.EmployeeEmail,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            EventDate = item.EventDate,
                            EventType = item.EventType,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        logs.Add(log);
                    }
                }
            }
            catch (Exception e)
            {
                logs = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return logs;
        }

        public BroadcastingPlayerLog Get(Guid id)
        {
            BroadcastingPlayerLog log = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbroadcastingPlayerLogsView
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (OltpbroadcastingPlayerLogsView item in query)
                    {
                        log = new BroadcastingPlayerLog
                        {
                            Id = item.Id,
                            BeaconId = item.BroadcasterId,
                            BeaconName = item.BroadcasterName,
                            BranchId = item.BranchId,
                            BranchName = item.BranchName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            EmployeeId = item.EmployeeId,
                            EmployeeName = item.EmployeeName,
                            EmployeeEmail = item.EmployeeEmail,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            EventDate = item.EventDate,
                            EventType = item.EventType,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                        };

                    }
                }
            }
            catch (Exception e)
            {
                log = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return log;
        }

        public bool Post(Guid branchId, Guid departmentId, Guid broadcasterId, Guid? employeeId, DateTime eventDate, int eventType)
        {
            bool success;
            try
            {
                OltpbroadcastingPlayerLogs log = new OltpbroadcastingPlayerLogs
                {
                    Id = Guid.NewGuid(),
                    BroadcasterId = broadcasterId,
                    BranchId = branchId,
                    DepartmentId = departmentId,
                    EmployeeId = employeeId,
                    EventDate = eventDate,
                    EventType = eventType,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpbroadcastingPlayerLogs.Add(log);
                this._businessObjects.Context.SaveChanges();

                success = true;
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Delete(DateTime dateTime)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbroadcastingPlayerLogs
                            where x.EventDate <= dateTime
                            select x;

                if (query != null)
                {
                    foreach (OltpbroadcastingPlayerLogs item in query)
                    {
                        this._businessObjects.Context.OltpbroadcastingPlayerLogs.Remove(item);
                    }

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
        /// Creates a new BroadcasterPlayerLogManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public BroadcastingPlayerLogManager(BusinessObjects businessObjects)
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
