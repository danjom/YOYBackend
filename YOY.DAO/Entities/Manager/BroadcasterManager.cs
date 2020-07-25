using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class BroadcasterManager
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

        private void GetLocationData(ref Broadcaster beacon, string tenantName, string branchName, string departmentName)
        {
            if (beacon.BranchId != Guid.Empty && beacon.BranchId != null)
            {
                beacon.BranchName = branchName;
            }
            else
            {
                beacon.BranchName = Resources.NotAssigned;
            }

            if (beacon.DepartmentId != Guid.Empty && beacon.DepartmentId != null)
            {
                beacon.DepartmentName = departmentName;
            }
            else
            {
                beacon.DepartmentName = Resources.NotAssigned;
            }

            if (beacon.TenantId == Guid.Empty)
            {
                beacon.TenantName = Resources.NotAssigned;
            }
            else
            {
                beacon.TenantName = tenantName;
            }

            if (string.IsNullOrWhiteSpace(beacon.InStoreLocation))
            {
                beacon.InStoreLocation = Resources.NotAssigned;
            }
        }

        public string GetBeaconTypeName(int beaconType)
        {
            string typeName = beaconType switch
            {
                BroadcasterTypes.Signal => Resources.Signal,
                BroadcasterTypes.Audio => Resources.Audio,
                BroadcasterTypes.Beacon => Resources.Beacon,
                BroadcasterTypes.Image => Resources.Image,
                _ => "--",
            };
            return typeName;
        }

        public string GetUsageTypeName(int usageType)
        {
            string typeName = usageType switch
            {
                BroadcasterUsageTypes.InPlace => Resources.InPlace,
                BroadcasterUsageTypes.OnTheFence => Resources.OnTheFence,
                BroadcasterUsageTypes.MediaInteraction => Resources.MediaInteraction,
                _ => "--",
            };
            return typeName;
        }

        public string GetPurposeTypeName(int purposeType)
        {
            string typeName = purposeType switch
            {
                BroadcasterPurposeTypes.BroadcastingExclusive => Resources.BroadcastingExclusive,
                BroadcasterPurposeTypes.WalkInCheckInExclusive => Resources.WalkInCheckIn,
                BroadcasterPurposeTypes.BroadcastingAndWalkInCheckIn => Resources.BroadcastingAndWalkInCheckIn,
                _ => "--",
            };
            return typeName;
        }

        public string GetProtocolTypeName(int protocolType)
        {
            string typeName = protocolType switch
            {
                BroadcastingProtocols.SignalRecognition => Resources.SignalRecognition,
                BroadcastingProtocols.AudioRecognition => Resources.AudioRecognition,
                BroadcastingProtocols.iBeacon => Resources.iBeacon,
                BroadcastingProtocols.Eddystone => Resources.Eddystone,
                _ => "--",
            };
            return typeName;
        }

        public List<Broadcaster> Gets(Guid? stateId, Guid? tenantId, Guid referenceId, int referenceType, int activeState, int usageType, int pageSize, int pageNumber)
        {
            List<Broadcaster> broadcasters = null;

            try
            {
                var query = (dynamic)null;

                switch (referenceType)
                {
                    case BroadcastingDevicesReferenceTypes.Department:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.DepartmentId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.DepartmentId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.DepartmentId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.DepartmentId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.DepartmentId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.DepartmentId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.DepartmentId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.DepartmentId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case ActiveStates.Active:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.DepartmentId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.DepartmentId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.DepartmentId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.DepartmentId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.DepartmentId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.DepartmentId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.DepartmentId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.DepartmentId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case ActiveStates.Inactive:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.DepartmentId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.DepartmentId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.DepartmentId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.DepartmentId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.DepartmentId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.DepartmentId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.DepartmentId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.DepartmentId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                        }
                        break;
                    case BroadcastingDevicesReferenceTypes.Branch:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.BranchId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.BranchId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.BranchId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.BranchId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.BranchId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.BranchId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.BranchId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.BranchId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                break;
                            case ActiveStates.Active:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.BranchId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.BranchId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.BranchId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.BranchId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.BranchId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.BranchId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.BranchId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.BranchId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                break;
                            case ActiveStates.Inactive:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.BranchId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.BranchId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.BranchId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.BranchId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.BranchId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.BranchId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.BranchId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.BranchId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case BroadcastingDevicesReferenceTypes.Tenant://For this case makes no sense to evaluate tenantId
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.TenantId == referenceId
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.UsageType == usageType
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.TenantId == referenceId && x.UsageType == usageType
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                break;
                            case ActiveStates.Active:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.IsActive
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.TenantId == referenceId && x.IsActive
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.IsActive && x.UsageType == usageType
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.TenantId == referenceId && x.IsActive && x.UsageType == usageType
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                break;
                            case ActiveStates.Inactive:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && !x.IsActive
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.TenantId == referenceId && !x.IsActive
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && !x.IsActive && x.UsageType == usageType
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                 where x.TenantId == referenceId && !x.IsActive && x.UsageType == usageType
                                                 orderby x.Name ascending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                break;
                        }
                        break;
                    case BroadcastingDevicesReferenceTypes.All:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                break;
                            case ActiveStates.Active:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                break;
                            case ActiveStates.Inactive:
                                if (usageType == BroadcasterUsageTypes.All)
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (stateId != null)
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && x.StateId == (Guid)stateId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (tenantId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == tenantId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                }



                if (query != null)
                {
                    broadcasters = new List<Broadcaster>();
                    Broadcaster broadcaster = null;

                    foreach (DefbroadcastersView item in query)
                    {
                        broadcaster = new Broadcaster
                        {
                            Id = item.Id,
                            ExternalId = item.ExternalId,
                            Name = item.Name,
                            FriendlyName = item.FriendlyName,
                            UUID = item.Uuid,
                            Minor = item.Minor,
                            Major = item.Mayor,
                            NamespaceId = item.NamespaceId,
                            InstanceId = item.InstanceId,
                            URL = item.Url,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            MultiBranchEnabled = item.MultiBranchEnabled,
                            BranchId = item.BranchId,
                            BranchName = item.BrachName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            BeaconType = item.BeaconType,
                            BeaconTypeName = GetBeaconTypeName(item.BeaconType),
                            BroadcastingProtocol = item.BroadcastingProtocol,
                            BroadcastingProtocolName = GetProtocolTypeName(item.BroadcastingProtocol),
                            UsageType = item.UsageType,
                            UsageTypeName = GetUsageTypeName(item.UsageType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            InStoreLocation = item.InStoreLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LastCheckDate = item.LastCheckDate,
                            IsActive = item.IsActive,
                            Default = item.Default,
                            FileId = item.FileId,
                            FileMimeType = item.FileMimeType
                        };

                        this.GetLocationData(ref broadcaster, item.TenantName, item.BrachName, item.DepartmentName);

                        broadcasters.Add(broadcaster);
                    }
                }

            }
            catch (Exception e)
            {
                broadcasters = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return broadcasters;
        }

        /// <summary>
        /// Retrieve all beacons for specific type
        /// from a branchId or departmentId
        /// to get beacons from every tenant's branch or branch's department
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Broadcaster> Gets(Guid? stateId, Guid referenceId, int referenceType, int deviceType, int deviceTypeFilter, int activeState, int usageType, int pageSize, int pageNumber)
        {
            List<Broadcaster> broadcasters = null;

            try
            {
                var query = (dynamic)null;


                switch (referenceType)
                {
                    case BroadcastingDevicesReferenceTypes.Branch:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (deviceType == BroadcasterTypes.All)
                                {
                                    if (referenceId != Guid.Empty)
                                    {
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }

                                        }
                                    }
                                    else
                                    {//If wants all the beacons for the tenant
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.UsageType == usageType
                                                         orderby x.Name
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:
                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:

                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                            case ActiveStates.Active:
                                if (deviceType == BroadcasterTypes.All)
                                {
                                    if (referenceId != Guid.Empty)
                                    {
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {//If wants all the beacons for the tenant
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:
                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:

                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                            case ActiveStates.Inactive:
                                if (deviceType != BroadcasterTypes.All)
                                {
                                    if (referenceId != Guid.Empty)
                                    {
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && !x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && !x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && !x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && !x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {//If wants all the beacons for the tenant
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:
                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:

                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                        }
                        break;
                    case BroadcastingDevicesReferenceTypes.Department:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (deviceType == BroadcasterTypes.All)
                                {
                                    if (referenceId != Guid.Empty)
                                    {
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {//If wants all the beacons for the tenant
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:
                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:
                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = from x in this._businessObjects.Context.DefbroadcastersView
                                                                where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                select x;
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                            case ActiveStates.Active:
                                if (deviceType == BroadcasterTypes.All)
                                {
                                    if (referenceId != Guid.Empty)
                                    {
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {//If wants all the beacons for the tenant
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:
                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:
                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                            case ActiveStates.Inactive:
                                if (deviceType != BroadcasterTypes.All)
                                {
                                    if (referenceId != Guid.Empty)
                                    {
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && !x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && !x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && !x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && !x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {//If wants all the beacons for the tenant
                                        if (usageType == BroadcasterUsageTypes.All)
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (stateId != null)
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive && x.UsageType == usageType
                                                         orderby x.Name ascending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:
                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:
                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId != (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DepartmentId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                        }
                        break;
                    case BroadcastingDevicesReferenceTypes.Tenant:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (deviceType == BroadcasterTypes.All)
                                {
                                    if (usageType == BroadcasterUsageTypes.All)
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.TenantId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == referenceId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == referenceId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:

                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BeaconType == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BeaconType == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:

                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BroadcastingProtocol == deviceType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                            case ActiveStates.Active:
                                if (deviceType == BroadcasterTypes.All)
                                {
                                    if (usageType == BroadcasterUsageTypes.All)
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == referenceId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == referenceId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:

                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BeaconType == deviceType && x.IsActive
                                                                 orderby x.Name
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:


                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BroadcastingProtocol == deviceType && x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                            case ActiveStates.Inactive:
                                if (deviceType != BroadcasterTypes.All)
                                {
                                    if (usageType == BroadcasterUsageTypes.All)
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.TenantId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == referenceId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.TenantId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.TenantId == referenceId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:

                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == stateId && x.TenantId == referenceId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BeaconType == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:

                                            if (referenceId != Guid.Empty)
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.TenantId == referenceId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (usageType == BroadcasterUsageTypes.All)
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BroadcastingProtocol == deviceType && !x.IsActive
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                                else
                                                {
                                                    if (stateId != null)
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                    else
                                                    {
                                                        query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                                 where x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                                 orderby x.Name ascending
                                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                    }
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                        }
                        break;
                    case BroadcastingDevicesReferenceTypes.All:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (deviceType == BroadcasterTypes.All)
                                {
                                    if (usageType == BroadcasterUsageTypes.All)
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:
                                            if (usageType == BroadcasterUsageTypes.All)
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BeaconType == deviceType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BeaconType == deviceType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }
                                            else
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BeaconType == deviceType && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BeaconType == deviceType && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:
                                            if (usageType == BroadcasterUsageTypes.All)
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BroadcastingProtocol == deviceType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }
                                            else
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BroadcastingProtocol == deviceType && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                            case ActiveStates.Active:
                                if (deviceType == BroadcasterTypes.All)
                                {
                                    if (usageType == BroadcasterUsageTypes.All)
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:
                                            if (usageType == BroadcasterUsageTypes.All)
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BeaconType == deviceType && x.IsActive
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BeaconType == deviceType && x.IsActive
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }
                                            else
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BeaconType == deviceType && x.IsActive && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:
                                            if (usageType == BroadcasterUsageTypes.All)
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && x.IsActive
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BroadcastingProtocol == deviceType && x.IsActive
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }
                                            else
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BroadcastingProtocol == deviceType && x.IsActive && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                            case ActiveStates.Inactive:
                                if (deviceType != BroadcasterTypes.All)
                                {
                                    if (usageType == BroadcasterUsageTypes.All)
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where !x.IsActive
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (stateId != null)
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where x.StateId == (Guid)stateId && !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                     where !x.IsActive && x.UsageType == usageType
                                                     orderby x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }

                                }
                                else
                                {

                                    switch (deviceTypeFilter)
                                    {
                                        case BroadcasterTypeFilters.BeaconType:
                                            if (usageType == BroadcasterUsageTypes.All)
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BeaconType == deviceType && !x.IsActive
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BeaconType == deviceType && !x.IsActive
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }
                                            else
                                            {
                                                if (stateId != null)
                                                {
                                                    query = from x in this._businessObjects.Context.DefbroadcastersView
                                                            where x.StateId == (Guid)stateId && x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                            select x;
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BeaconType == deviceType && !x.IsActive && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }

                                            break;
                                        case BroadcasterTypeFilters.ProtocolType:
                                            if (usageType == BroadcasterUsageTypes.All)
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && !x.IsActive
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BroadcastingProtocol == deviceType && !x.IsActive
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }
                                            else
                                            {
                                                if (stateId != null)
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.StateId == (Guid)stateId && x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                                else
                                                {
                                                    query = (from x in this._businessObjects.Context.DefbroadcastersView
                                                             where x.BroadcastingProtocol == deviceType && !x.IsActive && x.UsageType == usageType
                                                             orderby x.Name ascending
                                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                                }
                                            }

                                            break;
                                    }


                                }
                                break;
                        }
                        break;
                }


                if (query != null)
                {
                    broadcasters = new List<Broadcaster>();
                    Broadcaster broadcaster = null;

                    foreach (DefbroadcastersView item in query)
                    {
                        broadcaster = new Broadcaster
                        {
                            Id = item.Id,
                            ExternalId = item.ExternalId,
                            Name = item.Name,
                            FriendlyName = item.FriendlyName,
                            UUID = item.Uuid,
                            Minor = item.Minor,
                            Major = item.Mayor,
                            NamespaceId = item.NamespaceId,
                            InstanceId = item.InstanceId,
                            URL = item.Url,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            MultiBranchEnabled = item.MultiBranchEnabled,
                            BranchId = item.BranchId,
                            BranchName = item.BrachName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            BeaconType = item.BeaconType,
                            BeaconTypeName = GetBeaconTypeName(item.BeaconType),
                            BroadcastingProtocol = item.BroadcastingProtocol,
                            BroadcastingProtocolName = GetProtocolTypeName(item.BroadcastingProtocol),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            UsageType = item.UsageType,
                            UsageTypeName = GetUsageTypeName(item.UsageType),
                            InStoreLocation = item.InStoreLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LastCheckDate = item.LastCheckDate,
                            IsActive = item.IsActive,
                            Default = item.Default,
                            FileId = item.FileId,
                            FileMimeType = item.FileMimeType
                        };

                        GetLocationData(ref broadcaster, item.TenantName, item.BrachName, item.DepartmentName);

                        broadcasters.Add(broadcaster);
                    }
                }

            }
            catch (Exception e)
            {
                broadcasters = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return broadcasters;
        }

        /// <summary>
        /// Retrieve broadcasters from a specific branch and department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Broadcaster> Gets(Guid branchId, Guid departmentId, int activeState, int usageType, int pageSize, int pageNumber)
        {
            List<Broadcaster> broadcasters = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        if (usageType != BroadcasterUsageTypes.All)
                        {
                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                     where x.UsageType == usageType && x.BranchId == branchId && x.DepartmentId == departmentId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                     where x.BranchId == branchId && x.DepartmentId == departmentId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        break;
                    case ActiveStates.Active:
                        if (usageType != BroadcasterUsageTypes.All)
                        {
                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                     where x.IsActive && x.UsageType == usageType && x.BranchId == branchId && x.DepartmentId == departmentId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                     where x.IsActive && x.BranchId == branchId && x.DepartmentId == departmentId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        break;
                    case ActiveStates.Inactive:
                        if (usageType != BroadcasterUsageTypes.All)
                        {
                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                     where !x.IsActive && x.UsageType == usageType && x.BranchId == branchId && x.DepartmentId == departmentId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.DefbroadcastersView
                                     where !x.IsActive && x.BranchId == branchId && x.DepartmentId == departmentId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        break;
                }

                if (query != null)
                {

                    Broadcaster broadcaster = null;
                    broadcasters = new List<Broadcaster>();
                    foreach (DefbroadcastersView item in query)
                    {
                        broadcaster = new Broadcaster
                        {
                            Id = item.Id,
                            ExternalId = item.ExternalId,
                            Name = item.Name,
                            FriendlyName = item.FriendlyName,
                            UUID = item.Uuid,
                            Minor = item.Minor,
                            Major = item.Mayor,
                            NamespaceId = item.NamespaceId,
                            InstanceId = item.InstanceId,
                            URL = item.Url,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            MultiBranchEnabled = item.MultiBranchEnabled,
                            BranchId = item.BranchId,
                            BranchName = item.BrachName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            BeaconType = item.BeaconType,
                            BeaconTypeName = GetBeaconTypeName(item.BeaconType),
                            BroadcastingProtocol = item.BroadcastingProtocol,
                            BroadcastingProtocolName = GetProtocolTypeName(item.BroadcastingProtocol),
                            UsageType = item.UsageType,
                            UsageTypeName = GetUsageTypeName(item.UsageType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            InStoreLocation = item.InStoreLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LastCheckDate = item.LastCheckDate,
                            IsActive = item.IsActive,
                            Default = item.Default,
                            FileId = item.FileId,
                            FileMimeType = item.FileMimeType
                        };


                        GetLocationData(ref broadcaster, item.TenantName, item.BrachName, item.DepartmentName);

                        broadcasters.Add(broadcaster);

                    }
                }

            }
            catch (Exception e)
            {
                broadcasters = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return broadcasters;
        }

        /// <summary>
        /// Retrieve specific beacon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Broadcaster Get(Guid id, bool filterByTenant)
        {
            Broadcaster broadcaster = null;

            try
            {
                var query = (dynamic)null;

                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.DefbroadcastersView
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.DefbroadcastersView
                            where x.Id == id
                            select x;
                }


                if (query != null)
                {

                    foreach (DefbroadcastersView item in query)
                    {
                        broadcaster = new Broadcaster
                        {
                            Id = item.Id,
                            ExternalId = item.ExternalId,
                            Name = item.Name,
                            FriendlyName = item.FriendlyName,
                            UUID = item.Uuid,
                            Minor = item.Minor,
                            Major = item.Mayor,
                            NamespaceId = item.NamespaceId,
                            InstanceId = item.InstanceId,
                            URL = item.Url,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            MultiBranchEnabled = item.MultiBranchEnabled,
                            BranchId = item.BranchId,
                            BranchName = item.BrachName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            BeaconType = item.BeaconType,
                            BeaconTypeName = GetBeaconTypeName(item.BeaconType),
                            BroadcastingProtocol = item.BroadcastingProtocol,
                            BroadcastingProtocolName = GetProtocolTypeName(item.BroadcastingProtocol),
                            UsageType = item.UsageType,
                            UsageTypeName = GetUsageTypeName(item.UsageType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            InStoreLocation = item.InStoreLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LastCheckDate = item.LastCheckDate,
                            IsActive = item.IsActive,
                            Default = item.Default,
                            FileId = item.FileId,
                            FileMimeType = item.FileMimeType
                        };


                        GetLocationData(ref broadcaster, item.TenantName, item.BrachName, item.DepartmentName);

                    }
                }

            }
            catch (Exception e)
            {
                broadcaster = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return broadcaster;
        }


        /// <summary>
        /// Retrieve specific beacon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Broadcaster Get(string externalId)
        {
            Broadcaster broadcaster = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefbroadcastersView
                            where x.ExternalId == externalId
                            select x;

                if (query != null)
                {

                    foreach (DefbroadcastersView item in query)
                    {
                        broadcaster = new Broadcaster
                        {
                            Id = item.Id,
                            ExternalId = item.ExternalId,
                            Name = item.Name,
                            FriendlyName = item.FriendlyName,
                            UUID = item.Uuid,
                            Minor = item.Minor,
                            Major = item.Mayor,
                            NamespaceId = item.NamespaceId,
                            InstanceId = item.InstanceId,
                            URL = item.Url,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            BranchId = item.BranchId,
                            BranchName = item.BrachName,
                            MultiBranchEnabled = item.MultiBranchEnabled,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            BeaconType = item.BeaconType,
                            BeaconTypeName = GetBeaconTypeName(item.BeaconType),
                            BroadcastingProtocol = item.BroadcastingProtocol,
                            BroadcastingProtocolName = GetProtocolTypeName(item.BroadcastingProtocol),
                            UsageType = item.UsageType,
                            UsageTypeName = GetUsageTypeName(item.UsageType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetProtocolTypeName(item.PurposeType),
                            InStoreLocation = item.InStoreLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LastCheckDate = item.LastCheckDate,
                            IsActive = item.IsActive,
                            Default = item.Default,
                            FileId = item.FileId,
                            FileMimeType = item.FileMimeType
                        };


                        GetLocationData(ref broadcaster, item.TenantName, item.BrachName, item.DepartmentName);

                    }
                }

            }
            catch (Exception e)
            {
                broadcaster = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return broadcaster;
        }

        /// <summary>
        /// Retrieve specific beacon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Broadcaster Get(Guid branchId, Guid departmentId)
        {
            Broadcaster broadcaster = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefbroadcastersView
                            where x.BranchId == branchId && x.DepartmentId == departmentId
                            select x;

                if (query != null)
                {

                    foreach (DefbroadcastersView item in query)
                    {
                        broadcaster = new Broadcaster
                        {
                            Id = item.Id,
                            ExternalId = item.ExternalId,
                            Name = item.Name,
                            FriendlyName = item.FriendlyName,
                            UUID = item.Uuid,
                            Minor = item.Minor,
                            Major = item.Mayor,
                            NamespaceId = item.NamespaceId,
                            InstanceId = item.InstanceId,
                            URL = item.Url,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            MultiBranchEnabled = item.MultiBranchEnabled,
                            BranchId = item.BranchId,
                            BranchName = item.BrachName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            BeaconType = item.BeaconType,
                            BeaconTypeName = GetBeaconTypeName(item.BeaconType),
                            BroadcastingProtocol = item.BroadcastingProtocol,
                            BroadcastingProtocolName = GetProtocolTypeName(item.BroadcastingProtocol),
                            UsageType = item.UsageType,
                            UsageTypeName = GetUsageTypeName(item.UsageType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            InStoreLocation = item.InStoreLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LastCheckDate = item.LastCheckDate,
                            IsActive = item.IsActive,
                            Default = item.Default,
                            FileId = item.FileId,
                            FileMimeType = item.FileMimeType
                        };


                        GetLocationData(ref broadcaster, item.TenantName, item.BrachName, item.DepartmentName);

                    }
                }

            }
            catch (Exception e)
            {
                broadcaster = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return broadcaster;
        }

        /// <summary>
        /// Retrieve specific beacon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Broadcaster Get(string uuid, int minor, int major)
        {
            Broadcaster broadcaster = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefbroadcastersView
                            where x.Uuid == uuid && x.Minor == minor && x.Mayor == major
                            select x;

                if (query != null)
                {

                    foreach (DefbroadcastersView item in query)
                    {
                        broadcaster = new Broadcaster
                        {
                            Id = item.Id,
                            ExternalId = item.ExternalId,
                            Name = item.Name,
                            FriendlyName = item.FriendlyName,
                            UUID = item.Uuid,
                            Minor = item.Minor,
                            Major = item.Mayor,
                            NamespaceId = item.NamespaceId,
                            InstanceId = item.InstanceId,
                            URL = item.Url,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            MultiBranchEnabled = item.MultiBranchEnabled,
                            BranchId = item.BranchId,
                            BranchName = item.BrachName,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = item.DepartmentName,
                            BeaconType = item.BeaconType,
                            BeaconTypeName = GetBeaconTypeName(item.BeaconType),
                            BroadcastingProtocol = item.BroadcastingProtocol,
                            BroadcastingProtocolName = GetProtocolTypeName(item.BroadcastingProtocol),
                            UsageType = item.UsageType,
                            UsageTypeName = GetUsageTypeName(item.UsageType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            InStoreLocation = item.InStoreLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LastCheckDate = item.LastCheckDate,
                            IsActive = item.IsActive,
                            Default = item.Default,
                            FileId = item.FileId,
                            FileMimeType = item.FileMimeType
                        };


                        GetLocationData(ref broadcaster, item.TenantName, item.BrachName, item.DepartmentName);

                    }
                }

            }
            catch (Exception e)
            {
                broadcaster = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return broadcaster;
        }

        /// <summary>
        /// Creates a new beacon
        /// </summary>
        /// <param name="proximityUUID"></param>
        /// <param name="externalId"></param>
        /// <param name="name"></param>
        /// <param name="friendlyName"></param>
        /// <param name="inStoreLocation"></param>
        /// <param name="branchId"></param>
        /// <param name="tenantId"></param>
        /// <param name="departmentId"></param>
        /// <param name="minor"></param>
        /// <param name="mayor"></param>
        /// <param name="namespaceId"></param>
        /// <param name="instanceId"></param>
        /// <param name="url"></param>
        /// <param name="beaconType"></param>
        /// <param name="broadcastType"></param>
        /// <param name="broadcastProtocol"></param>
        /// <param name="isPivot"></param>
        /// <param name="defaultBeacon"></param>
        /// <returns></returns>
        public Broadcaster Post(string proximityUUID, string externalId, string name, string friendlyName, Guid countryId, Guid stateId,
            Guid tenantId, bool multiBranchEnabled, int? minor, int? mayor, string namespaceId, string instanceId, string url,
            int beaconType, int broadcastProtocol, int usageType, int purposeType, bool defaultBeacon, string fileId, string fileMimeType)
        {
            Broadcaster broadcaster = null;

            try
            {
                Defbroadcasters newBroadcaster = new Defbroadcasters
                {
                    Id = Guid.NewGuid(),
                    ExternalId = externalId,
                    CountryId = countryId,
                    StateId = stateId,
                    TenantId = tenantId,
                    Uuid = proximityUUID,
                    Name = name,
                    FriendlyName = friendlyName,
                    MultiBranchEnabled = multiBranchEnabled,
                    BranchId = null,
                    DepartmentId = null,
                    IsActive = true,
                    Mayor = mayor,
                    Minor = minor,
                    NamespaceId = namespaceId,
                    InstanceId = instanceId,
                    Url = url,
                    BeaconType = beaconType,
                    BroadcastingProtocol = broadcastProtocol,
                    UsageType = usageType,
                    PurposeType = purposeType,
                    InStoreLocation = "",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    LastCheckDate = DateTime.UtcNow,
                    Default = defaultBeacon,
                    FileId = fileId,
                    FileMimeType = fileMimeType
                };

                this._businessObjects.Context.Defbroadcasters.Add(newBroadcaster);
                this._businessObjects.Context.SaveChanges();

                //Now retrieve the new beacon data

                var query = from x in this._businessObjects.Context.DefbroadcastersView
                            where x.Id == newBroadcaster.Id
                            select x;

                if (query != null)
                {

                    foreach (DefbroadcastersView item in query)
                    {
                        broadcaster = new Broadcaster
                        {
                            Id = item.Id,
                            ExternalId = item.ExternalId,
                            Name = item.Name,
                            FriendlyName = item.FriendlyName,
                            UUID = item.Uuid,
                            Minor = item.Minor,
                            Major = item.Mayor,
                            NamespaceId = item.NamespaceId,
                            InstanceId = item.InstanceId,
                            URL = item.Url,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            TenantId = item.TenantId,
                            MultiBranchEnabled = item.MultiBranchEnabled,
                            BranchId = item.BranchId,
                            DepartmentId = item.DepartmentId,
                            BeaconType = item.BeaconType,
                            BeaconTypeName = GetBeaconTypeName(item.BeaconType),
                            BroadcastingProtocol = item.BroadcastingProtocol,
                            BroadcastingProtocolName = GetProtocolTypeName(item.BroadcastingProtocol),
                            UsageType = item.UsageType,
                            UsageTypeName = GetUsageTypeName(item.UsageType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            InStoreLocation = item.InStoreLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LastCheckDate = item.LastCheckDate,
                            IsActive = item.IsActive,
                            Default = item.Default,
                            FileId = item.FileId,
                            FileMimeType = item.FileMimeType
                        };


                        GetLocationData(ref broadcaster, item.TenantName, item.BrachName, item.DepartmentName);

                    }
                }

            }
            catch (Exception e)
            {
                broadcaster = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return broadcaster;
        }

        /// <summary>
        /// Updates a beacon
        /// </summary>
        /// <param name="id"></param>
        /// <param name="proximityUUID"></param>
        /// <param name="externalId"></param>
        /// <param name="name"></param>
        /// <param name="friendlyName"></param>
        /// <param name="inStoreLocation"></param>
        /// <param name="branchId"></param>
        /// <param name="departmentId"></param>
        /// <param name="minor"></param>
        /// <param name="mayor"></param>
        /// <param name="namespaceId"></param>
        /// <param name="instanceId"></param>
        /// <param name="url"></param>
        /// <param name="beaconType"></param>
        /// <param name="broadcastType"></param>
        /// <param name="broadcastProtocol"></param>
        /// <param name="isPivot"></param>
        /// <param name="defaultBeacon"></param>
        /// <returns></returns>
        public Broadcaster Put(Guid id, string proximityUUID, string externalId, string name, string friendlyName, Guid tenantId,
            Guid countryId, Guid stateId, bool multiBranchEnabled, int minor, int mayor, string namespaceId, string instanceId,
            string url, int beaconType, int broadcastProtocol, int usageType, int purposeType, bool defaultBeacon, string fileId,
            string fileMimeType)
        {
            Broadcaster broadcaster = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defbroadcasters
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Defbroadcasters currentBroadcaster = null;

                    foreach (Defbroadcasters item in query)
                    {
                        currentBroadcaster = item;
                    }

                    if (currentBroadcaster != null)
                    {

                        currentBroadcaster.Uuid = proximityUUID;
                        currentBroadcaster.ExternalId = externalId;
                        currentBroadcaster.Name = name;
                        currentBroadcaster.FriendlyName = friendlyName;
                        currentBroadcaster.CountryId = countryId;
                        currentBroadcaster.StateId = stateId;
                        currentBroadcaster.TenantId = tenantId;
                        currentBroadcaster.MultiBranchEnabled = multiBranchEnabled;
                        currentBroadcaster.Mayor = mayor;
                        currentBroadcaster.Minor = minor;
                        currentBroadcaster.NamespaceId = namespaceId;
                        currentBroadcaster.InstanceId = instanceId;
                        currentBroadcaster.Url = url;
                        currentBroadcaster.BeaconType = beaconType;
                        currentBroadcaster.BroadcastingProtocol = broadcastProtocol;
                        currentBroadcaster.UsageType = usageType;
                        currentBroadcaster.PurposeType = purposeType;
                        currentBroadcaster.Default = defaultBeacon;
                        currentBroadcaster.FileId = fileId;
                        currentBroadcaster.FileMimeType = fileMimeType;
                        currentBroadcaster.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        //Now retrieve the beacon data

                        var queryData = from x in this._businessObjects.Context.DefbroadcastersView
                                        where x.Id == currentBroadcaster.Id
                                        select x;

                        if (queryData != null)
                        {

                            foreach (DefbroadcastersView item in queryData)
                            {
                                broadcaster = new Broadcaster
                                {
                                    Id = item.Id,
                                    ExternalId = item.ExternalId,
                                    Name = item.Name,
                                    FriendlyName = item.FriendlyName,
                                    UUID = item.Uuid,
                                    Minor = item.Minor,
                                    Major = item.Mayor,
                                    NamespaceId = item.NamespaceId,
                                    InstanceId = item.InstanceId,
                                    URL = item.Url,
                                    CountryId = item.CountryId,
                                    CountryName = item.CountryName,
                                    StateId = item.StateId,
                                    StateName = item.StateName,
                                    TenantId = item.TenantId,
                                    TenantName = item.TenantName,
                                    MultiBranchEnabled = item.MultiBranchEnabled,
                                    BranchId = item.BranchId,
                                    BranchName = item.BrachName,
                                    DepartmentId = item.DepartmentId,
                                    DepartmentName = item.DepartmentName,
                                    BeaconType = item.BeaconType,
                                    BeaconTypeName = GetBeaconTypeName(item.BeaconType),
                                    BroadcastingProtocol = item.BroadcastingProtocol,
                                    BroadcastingProtocolName = GetProtocolTypeName(item.BroadcastingProtocol),
                                    UsageType = item.UsageType,
                                    UsageTypeName = GetUsageTypeName(item.UsageType),
                                    PurposeType = item.PurposeType,
                                    PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                                    InStoreLocation = item.InStoreLocation,
                                    CreatedDate = item.CreatedDate,
                                    UpdatedDate = item.UpdatedDate,
                                    LastCheckDate = item.LastCheckDate,
                                    IsActive = item.IsActive,
                                    Default = item.Default,
                                    FileId = item.FileId,
                                    FileMimeType = item.FileMimeType
                                };


                                GetLocationData(ref broadcaster, item.TenantName, item.BrachName, item.DepartmentName);

                            }
                        }

                    }

                }

            }
            catch (Exception e)
            {
                broadcaster = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return broadcaster;
        }

        /// <summary>
        /// Updates beacon storage file Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Put(Guid id, string fileId, string fileMimeType)
        {
            string oldFileId = "";

            try
            {
                var query = from x in this._businessObjects.Context.Defbroadcasters
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Defbroadcasters currentBroadcaster = null;

                    foreach (Defbroadcasters item in query)
                    {
                        currentBroadcaster = item;
                    }

                    if (currentBroadcaster != null)
                    {

                        oldFileId = currentBroadcaster.FileId;

                        currentBroadcaster.FileId = fileId;
                        currentBroadcaster.FileMimeType = fileMimeType;
                        currentBroadcaster.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();
                    }

                }

            }
            catch (Exception e)
            {
                oldFileId = "";
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return oldFileId;
        }

        /// <summary>
        /// Updates beacon last status check date
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Put(Guid id, DateTime lastCheckDate)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defbroadcasters
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Defbroadcasters currentBroadcaster = null;

                    foreach (Defbroadcasters item in query)
                    {
                        currentBroadcaster = item;
                    }

                    if (currentBroadcaster != null)
                    {
                        currentBroadcaster.LastCheckDate = lastCheckDate;
                        currentBroadcaster.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }

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

        /// <summary>
        /// Updates beacon location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Put(Guid id, Guid? departmentId, Guid? branchId, string inStoreLocation)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defbroadcasters
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Defbroadcasters currentBeacon = null;

                    foreach (Defbroadcasters item in query)
                    {
                        currentBeacon = item;
                    }

                    if (currentBeacon != null)
                    {
                        currentBeacon.BranchId = branchId;
                        currentBeacon.DepartmentId = departmentId;
                        currentBeacon.InStoreLocation = inStoreLocation;
                        currentBeacon.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }

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

        /// <summary>
        /// Updates beacon active state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Put(Guid id, int type)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defbroadcasters
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Defbroadcasters currentBroadcaster = null;

                    foreach (Defbroadcasters item in query)
                    {
                        currentBroadcaster = item;
                    }

                    if (currentBroadcaster != null)
                    {
                        switch (type)
                        {
                            case BroadcasterStateUpdateTypes.ActiveState:
                                currentBroadcaster.IsActive = !currentBroadcaster.IsActive;
                                currentBroadcaster.UpdatedDate = DateTime.UtcNow;
                                break;
                            case BroadcasterStateUpdateTypes.DefaultState:
                                currentBroadcaster.Default = !currentBroadcaster.Default;
                                currentBroadcaster.UpdatedDate = DateTime.UtcNow;
                                break;
                        }


                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }

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

        /// <summary>
        /// Deletes a beacon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                Defbroadcasters broadcaster = (from x in this._businessObjects.Context.Defbroadcasters
                                                where x.Id == id
                                                select x).FirstOrDefault();

                if (broadcaster != null)
                {
                    broadcaster.Deleted = true;
                    broadcaster.UpdatedDate = DateTime.UtcNow;

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
        /// Creates a new TableManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public BroadcasterManager(BusinessObjects businessObjects)
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
