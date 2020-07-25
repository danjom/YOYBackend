using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class HardwareIOTDeviceManager
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


        private string GetTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case HardwareIOTDeviceTypes.PaymentDevice:
                    typeName = Resources.PaymentDevice;
                    break;
            }

            return typeName;
        }
        private string GetStatusName(int status)
        {
            string statusName = "";

            switch (status)
            {
                case HardwareIOTDeviceStatuses.Unassigned:
                    statusName = Resources.Available;
                    break;
                case HardwareIOTDeviceStatuses.NotConfigured:
                    statusName = Resources.NotConfigured;
                    break;
                case HardwareIOTDeviceStatuses.InstalledOnTenant:
                    statusName = Resources.InstalledOnTenant;
                    break;
                case HardwareIOTDeviceStatuses.ActivelyWorking:
                    statusName = Resources.ActivelyWorking;
                    break;
                case HardwareIOTDeviceStatuses.IssuesReported:
                    statusName = Resources.IssuesReported;
                    break;
                case HardwareIOTDeviceStatuses.OnMaintenance:
                    statusName = Resources.OnMaintenance;
                    break;
                case HardwareIOTDeviceStatuses.Damaged:
                    statusName = Resources.Damaged;
                    break;
            }

            return statusName;
        }

        public List<HardwareIOTDevice> Gets(Guid? tenantId, Guid? branchId, int type, int status, int activeState, int pageSize, int pageNumber)
        {
            List<HardwareIOTDevice> hardwareIOTDevices = null;

            try
            {
                var query = (dynamic)null;

                if (tenantId != null)
                {
                    if (branchId != null)
                    {
                        if (type != HardwareIOTDeviceTypes.All)
                        {
                            if (status != HardwareIOTDeviceStatuses.All)
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.TenantId == tenantId && x.BranchId == branchId && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.TenantId == tenantId && x.BranchId == branchId && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.TenantId == tenantId && x.BranchId == branchId && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                            else
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.TenantId == tenantId && x.BranchId == branchId && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.TenantId == tenantId && x.BranchId == branchId && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.TenantId == tenantId && x.BranchId == branchId && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (status != HardwareIOTDeviceStatuses.All)
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.TenantId == tenantId && x.BranchId == branchId && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.TenantId == tenantId && x.BranchId == branchId && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.TenantId == tenantId && x.BranchId == branchId && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                            else
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.TenantId == tenantId && x.BranchId == branchId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.TenantId == tenantId && x.BranchId == branchId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.TenantId == tenantId && x.BranchId == branchId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (type != HardwareIOTDeviceTypes.All)
                        {
                            if (status != HardwareIOTDeviceStatuses.All)
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.TenantId == tenantId && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.TenantId == tenantId && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                            else
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.TenantId == tenantId && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.TenantId == tenantId && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.TenantId == tenantId && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (status != HardwareIOTDeviceStatuses.All)
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.TenantId == tenantId && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.TenantId == tenantId && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.TenantId == tenantId && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                            else
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.TenantId == tenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.TenantId == tenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.TenantId == tenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (branchId != null)
                    {
                        if (type != HardwareIOTDeviceTypes.All)
                        {
                            if (status != HardwareIOTDeviceStatuses.All)
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.BranchId == branchId && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.BranchId == branchId && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.BranchId == branchId && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                            else
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.BranchId == branchId && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.BranchId == branchId && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.BranchId == branchId && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (status != HardwareIOTDeviceStatuses.All)
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.BranchId == branchId && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.BranchId == branchId && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.BranchId == branchId && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                            else
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.BranchId == branchId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.BranchId == branchId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.BranchId == branchId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (type != HardwareIOTDeviceTypes.All)
                        {
                            if (status != HardwareIOTDeviceStatuses.All)
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                            else
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (status != HardwareIOTDeviceStatuses.All)
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                            else
                            {
                                switch (activeState)
                                {
                                    case ActiveStates.All:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Active:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && (bool)x.IsActive
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ActiveStates.Inactive:
                                        query = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                 where !x.Deleted && !(bool)x.IsActive
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                            }
                        }
                    }
                }

                if (query != null)
                {
                    hardwareIOTDevices = new List<HardwareIOTDevice>();
                    HardwareIOTDevice iOTDevice;

                    foreach (DefhardwareIotdevices item in query)
                    {
                        iOTDevice = new HardwareIOTDevice
                        {
                            Id = item.Id,
                            Alias = item.Alias,
                            UniqueKey = item.UniqueKey,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            FirmwareVersion = item.FirmwareVersion,
                            HardwareVersion = item.HardwareVersion,
                            IsActive = (bool)item.IsActive,
                            LastRequestDate = item.LastRequestDate,
                            LastMaintenanceDate = item.LastMaintenanceDate,
                            EffectiveRequestCount = item.EffectiveRequestsCount,
                            InstallationDate = item.InstallationDate,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow
                        };

                        hardwareIOTDevices.Add(iOTDevice);
                    }
                }
            }
            catch (Exception e)
            {
                hardwareIOTDevices = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return hardwareIOTDevices;
        }

        public HardwareIOTDevice Get(Guid id)
        {
            HardwareIOTDevice iOTDevice = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefhardwareIotdevices
                            where !x.Deleted && x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (DefhardwareIotdevices item in query)
                    {
                        iOTDevice = new HardwareIOTDevice
                        {
                            Id = item.Id,
                            Alias = item.Alias,
                            UniqueKey = item.UniqueKey,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            FirmwareVersion = item.FirmwareVersion,
                            HardwareVersion = item.HardwareVersion,
                            IsActive = (bool)item.IsActive,
                            LastRequestDate = item.LastRequestDate,
                            LastMaintenanceDate = item.LastMaintenanceDate,
                            EffectiveRequestCount = item.EffectiveRequestsCount,
                            InstallationDate = item.InstallationDate,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow
                        };
                    }
                }
            }
            catch (Exception e)
            {
                iOTDevice = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return iOTDevice;
        }

        public HardwareIOTDevice Get(string key)
        {
            HardwareIOTDevice iOTDevice = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefhardwareIotdevices
                            where !x.Deleted && x.UniqueKey == key
                            select x;

                if (query != null)
                {
                    foreach (DefhardwareIotdevices item in query)
                    {
                        iOTDevice = new HardwareIOTDevice
                        {
                            Id = item.Id,
                            Alias = item.Alias,
                            UniqueKey = item.UniqueKey,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            FirmwareVersion = item.FirmwareVersion,
                            HardwareVersion = item.HardwareVersion,
                            IsActive = (bool)item.IsActive,
                            LastRequestDate = item.LastRequestDate,
                            LastMaintenanceDate = item.LastMaintenanceDate,
                            EffectiveRequestCount = item.EffectiveRequestsCount,
                            InstallationDate = item.InstallationDate,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow
                        };
                    }
                }
            }
            catch (Exception e)
            {
                iOTDevice = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return iOTDevice;
        }

        public HardwareIOTDevice Post(Guid? tenantId, Guid? branchId, string alias, string uniqueKey, int type, int status, string firmwareVersion, string hardwareVersion)
        {
            HardwareIOTDevice iOTDevice;
            try
            {
                DefhardwareIotdevices newIOTDevice = new DefhardwareIotdevices
                {
                    Id = Guid.NewGuid(),
                    Alias = alias,
                    UniqueKey = uniqueKey,
                    TenantId = tenantId,
                    BranchId = branchId,
                    Type = type,
                    Status = status,
                    FirmwareVersion = firmwareVersion,
                    HardwareVersion = hardwareVersion,
                    IsActive = true,
                    LastRequestDate = null,
                    LastMaintenanceDate = null,
                    EffectiveRequestsCount = 0,
                    InstallationDate = null,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.DefhardwareIotdevices.Add(newIOTDevice);
                this._businessObjects.Context.SaveChanges();

                iOTDevice = new HardwareIOTDevice
                {
                    Id = newIOTDevice.Id,
                    Alias = newIOTDevice.Alias,
                    UniqueKey = newIOTDevice.UniqueKey,
                    TenantId = newIOTDevice.TenantId,
                    BranchId = newIOTDevice.BranchId,
                    Type = newIOTDevice.Type,
                    TypeName = GetTypeName(newIOTDevice.Type),
                    Status = newIOTDevice.Status,
                    StatusName = GetStatusName(newIOTDevice.Status),
                    FirmwareVersion = newIOTDevice.FirmwareVersion,
                    HardwareVersion = newIOTDevice.HardwareVersion,
                    IsActive = (bool)newIOTDevice.IsActive,
                    LastRequestDate = newIOTDevice.LastRequestDate,
                    LastMaintenanceDate = newIOTDevice.LastMaintenanceDate,
                    EffectiveRequestCount = newIOTDevice.EffectiveRequestsCount,
                    InstallationDate = newIOTDevice.InstallationDate,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                iOTDevice = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return iOTDevice;
        }

        public HardwareIOTDevice Put(Guid id, string alias, Guid? tenantId, Guid? branchId, string firmwareVersion)
        {
            HardwareIOTDevice iOTDevice = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefhardwareIotdevices
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefhardwareIotdevices currentIOTDevice = null;

                    foreach (DefhardwareIotdevices item in query)
                    {
                        currentIOTDevice = item;
                    }

                    if (currentIOTDevice != null)
                    {
                        currentIOTDevice.Alias = alias;
                        currentIOTDevice.TenantId = tenantId;
                        currentIOTDevice.BranchId = branchId;
                        currentIOTDevice.FirmwareVersion = firmwareVersion;
                        currentIOTDevice.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        iOTDevice = new HardwareIOTDevice
                        {
                            Id = currentIOTDevice.Id,
                            Alias = currentIOTDevice.Alias,
                            UniqueKey = currentIOTDevice.UniqueKey,
                            TenantId = currentIOTDevice.TenantId,
                            BranchId = currentIOTDevice.BranchId,
                            Type = currentIOTDevice.Type,
                            TypeName = GetTypeName(currentIOTDevice.Type),
                            Status = currentIOTDevice.Status,
                            StatusName = GetStatusName(currentIOTDevice.Status),
                            FirmwareVersion = currentIOTDevice.FirmwareVersion,
                            HardwareVersion = currentIOTDevice.HardwareVersion,
                            IsActive = (bool)currentIOTDevice.IsActive,
                            LastRequestDate = currentIOTDevice.LastRequestDate,
                            LastMaintenanceDate = currentIOTDevice.LastMaintenanceDate,
                            EffectiveRequestCount = currentIOTDevice.EffectiveRequestsCount,
                            InstallationDate = currentIOTDevice.InstallationDate,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow
                        };
                    }
                }
            }
            catch (Exception e)
            {
                iOTDevice = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return iOTDevice;
        }

        public bool Put(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefhardwareIotdevices
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefhardwareIotdevices currentIOTDevice = null;

                    foreach (DefhardwareIotdevices item in query)
                    {
                        currentIOTDevice = item;
                    }

                    if (currentIOTDevice != null)
                    {
                        currentIOTDevice.IsActive = !currentIOTDevice.IsActive;
                        currentIOTDevice.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }

        public bool Put(Guid id, int status)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefhardwareIotdevices
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefhardwareIotdevices currentIOTDevice = null;

                    foreach (DefhardwareIotdevices item in query)
                    {
                        currentIOTDevice = item;
                    }

                    if (currentIOTDevice != null)
                    {
                        currentIOTDevice.Status = status;
                        currentIOTDevice.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }
        public bool Put(Guid id, DateTime date, int dateType)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefhardwareIotdevices
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefhardwareIotdevices currentIOTDevice = null;

                    foreach (DefhardwareIotdevices item in query)
                    {
                        currentIOTDevice = item;
                    }

                    if (currentIOTDevice != null)
                    {
                        switch (dateType)
                        {
                            case HardwareIOTDateTypes.InstallationDate:
                                currentIOTDevice.InstallationDate = date;
                                currentIOTDevice.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();

                                success = true;

                                break;
                            case HardwareIOTDateTypes.LastRequestDate:
                                currentIOTDevice.LastRequestDate = date;
                                ++currentIOTDevice.EffectiveRequestsCount;
                                currentIOTDevice.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();

                                success = true;

                                break;
                            case HardwareIOTDateTypes.LastMaintenanceDate:
                                currentIOTDevice.LastMaintenanceDate = date;
                                currentIOTDevice.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();

                                success = true;

                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                DefhardwareIotdevices currentIOTDevice = (from x in this._businessObjects.Context.DefhardwareIotdevices
                                                            where x.Id == id
                                                            select x).FirstOrDefault();

                if (currentIOTDevice != null)
                {
                    currentIOTDevice.Deleted = true;
                    currentIOTDevice.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
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
        /// Creates a new HardwareIOTDeviceManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public HardwareIOTDeviceManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD HARDWARE IOT DEVICE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
