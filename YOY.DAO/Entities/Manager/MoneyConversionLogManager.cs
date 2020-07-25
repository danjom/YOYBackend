using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.MoneyConversionLog;
using YOY.Values;
using YOY.Values.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class MoneyConversionLogManager
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

        private string GetStatusName(int status)
        {
            string statusName = status switch
            {
                MoneyConversionLogInternalStatuses.Created => Resources.Created,
                MoneyConversionLogInternalStatuses.ValidationPending => Resources.ValidationPending,
                MoneyConversionLogInternalStatuses.PaymentPending => Resources.PaymentPending,
                MoneyConversionLogInternalStatuses.Payed => Resources.Payed,
                MoneyConversionLogInternalStatuses.Alert => Resources.Alert,
                _ => "--",
            };
            return statusName;
        }

        private string GetStateName(int state)
        {
            string stateName = state switch
            {
                MoneyConversionLogStates.PointsConverted => Resources.PointsConverted,
                MoneyConversionLogStates.Registered => Resources.RegisteredOnBusinessApp,
                MoneyConversionLogStates.AlreadyUsed => Resources.AlreadyUsed,
                MoneyConversionLogStates.Expired => Resources.Expired,
                MoneyConversionLogStates.FailedRegistration => Resources.FailedRegistrationOnBusinessApp,
                MoneyConversionLogStates.CommerceMismatch => Resources.CommerceMismatch,
                MoneyConversionLogStates.UserMismatch => Resources.UserMismatch,
                _ => "--",
            };
            return stateName;
        }

        private string GetMembershipLevelName(int membershipLevel)
        {
            string  membershipLevelName = membershipLevel switch
            {
                MembershipLevels.Bronze => Resources.Bronze,
                MembershipLevels.Silver => Resources.Silver,
                MembershipLevels.Gold => Resources.Gold,
                MembershipLevels.Platinum => Resources.Platinum,
                MembershipLevels.Diamond => Resources.Diamond,
                _ => "--",
            };
            return membershipLevelName;
        }

        public List<ConversionLogCountByCode> Gets(Guid tenantId, DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            List<ConversionLogCountByCode> logCountByCodes = null;

            try
            {

                //Gets the codes in range
                var query = (from x in this._businessObjects.Context.OltpmoneyConversionLogsView
                             where x.TenantId == tenantId && x.CreatedDate >= start && x.CreatedDate < end
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                if (query != null)
                {
                    List<MoneyConversionLog> moneyConversionLogs = new List<MoneyConversionLog>();
                    MoneyConversionLog moneyConversionLog = null;

                    foreach (OltpmoneyConversionLogsView item in query)
                    {
                        moneyConversionLog = new MoneyConversionLog
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            OwnerId = item.OwnerId,
                            ConversionCode = item.ConversionCode,
                            RequiredPoints = item.RequiredPoints,
                            MoneyAmount = item.MoneyAmount,
                            InternalStatus = item.InternalStatus,
                            State = item.State,
                            PointsOpUsedPoints = item.PointsOpUsedPoints,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        moneyConversionLogs.Add(moneyConversionLog);
                    }

                    if (moneyConversionLogs?.Count > 0)
                    {

                        ConversionLogCountByCode conversionLogCountByCode = null;
                        int logsCount = 0;
                        int latestStatus = -1;
                        int latestState = -1;

                        MoneyConversionLog[] logsGroup = null;
                        IEnumerable<IGrouping<string, MoneyConversionLog>> groupedByConversionCode = moneyConversionLogs.GroupBy(x => x.ConversionCode);

                        logCountByCodes = new List<ConversionLogCountByCode>();

                        foreach (IGrouping<string, MoneyConversionLog> conversionLogGroup in groupedByConversionCode)
                        {
                            logsGroup = conversionLogGroup.ToArray();

                            if (logsGroup?.Length > 0)
                            {
                                //Clear variables
                                logsCount = 0;
                                latestStatus = -1;
                                latestState = -1;

                                for (int i = 0; i < logsGroup.Length; ++i)
                                {
                                    ++logsCount;

                                    if (logsGroup[i].InternalStatus > latestStatus)
                                        latestStatus = logsGroup[i].InternalStatus;

                                    if (logsGroup[i].State > latestState)
                                        latestState = logsGroup[i].State;
                                }

                                conversionLogCountByCode = new ConversionLogCountByCode
                                {
                                    TenantId = logsGroup[0].TenantId,
                                    OwnerId = logsGroup[0].OwnerId,
                                    ConversionCode = logsGroup[0].ConversionCode,
                                    RequiredPoints = logsGroup[0].RequiredPoints,
                                    MoneyAmount = logsGroup[0].MoneyAmount,
                                    Count = logsCount,
                                    Status = latestStatus,
                                    StatusName = GetStatusName(latestStatus),
                                    State = latestState,
                                    StateName = GetStateName(latestState)
                                };

                                logCountByCodes.Add(conversionLogCountByCode);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logCountByCodes = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return logCountByCodes;
        }

        public List<ConversionLogCountByCode> Gets(DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            List<ConversionLogCountByCode> logCountByCodes = null;

            try
            {

                //Gets the codes in range
                var query = (from x in this._businessObjects.Context.OltpmoneyConversionLogsWithTenantView
                             where x.CreatedDate >= start && x.CreatedDate < end
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                if (query != null)
                {
                    List<MoneyConversionLog> moneyConversionLogs = new List<MoneyConversionLog>();
                    MoneyConversionLog moneyConversionLog = null;

                    foreach (OltpmoneyConversionLogsWithTenantView item in query)
                    {
                        moneyConversionLog = new MoneyConversionLog
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            OwnerId = item.OwnerId,
                            ConversionCode = item.ConversionCode,
                            RequiredPoints = item.RequiredPoints,
                            MoneyAmount = item.MoneyAmount,
                            InternalStatus = item.InternalStatus,
                            State = item.State,
                            PointsOpUsedPoints = item.PointsOpUsedPoints,
                            TenantName = item.TenantName,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        moneyConversionLogs.Add(moneyConversionLog);
                    }

                    if (moneyConversionLogs?.Count > 0)
                    {

                        ConversionLogCountByCode conversionLogCountByCode = null;
                        int logsCount = 0;
                        int latestStatus = -1;
                        int latestState = -1;

                        MoneyConversionLog[] logsGroup = null;
                        IEnumerable<IGrouping<string, MoneyConversionLog>> groupedByConversionCode = moneyConversionLogs.GroupBy(x => x.ConversionCode);

                        logCountByCodes = new List<ConversionLogCountByCode>();

                        foreach (IGrouping<string, MoneyConversionLog> conversionLogGroup in groupedByConversionCode)
                        {
                            logsGroup = conversionLogGroup.ToArray();

                            if (logsGroup?.Length > 0)
                            {
                                //Clear variables
                                logsCount = 0;
                                latestStatus = -1;
                                latestState = -1;

                                for (int i = 0; i < logsGroup.Length; ++i)
                                {
                                    ++logsCount;

                                    if (logsGroup[i].InternalStatus > latestStatus)
                                        latestStatus = logsGroup[i].InternalStatus;

                                    if (logsGroup[i].State > latestState)
                                        latestState = logsGroup[i].State;
                                }

                                conversionLogCountByCode = new ConversionLogCountByCode
                                {
                                    TenantId = logsGroup[0].TenantId,
                                    OwnerId = logsGroup[0].OwnerId,
                                    ConversionCode = logsGroup[0].ConversionCode,
                                    RequiredPoints = logsGroup[0].RequiredPoints,
                                    MoneyAmount = logsGroup[0].MoneyAmount,
                                    Count = logsCount,
                                    Status = latestStatus,
                                    StatusName = GetStatusName(latestStatus),
                                    State = latestState,
                                    StateName = GetStateName(latestState),
                                    TenantName = logsGroup[0].TenantName
                                };

                                logCountByCodes.Add(conversionLogCountByCode);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logCountByCodes = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return logCountByCodes;
        }

        public List<MoneyConversionLog> Gets(Guid tenantId, Guid? branchId, string conversionCode, DateTime startDate, DateTime endDate, int pageSize, int pageNumber)
        {
            List<MoneyConversionLog> moneyConversionLogs = null;

            try
            {
                var query = (dynamic)null;

                if (branchId != null)
                {
                    query = (from x in this._businessObjects.Context.OltpmoneyConversionLogsView
                             where x.TenantId == tenantId && x.BranchId == (Guid)branchId && x.ConversionCode == conversionCode && x.CreatedDate >= startDate && x.CreatedDate <= endDate
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }
                else
                {
                    query = (from x in this._businessObjects.Context.OltpmoneyConversionLogsView
                             where x.TenantId == tenantId && x.ConversionCode == conversionCode && x.CreatedDate >= startDate && x.CreatedDate <= endDate
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }

                if (query != null)
                {
                    MoneyConversionLog moneyConversionLog = null;
                    moneyConversionLogs = new List<MoneyConversionLog>();

                    foreach (OltpmoneyConversionLogsView item in query)
                    {
                        moneyConversionLog = new MoneyConversionLog
                        {
                            Id = item.Id,
                            OperationId = item.OperationId,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            BranchName = item.BranchName,
                            EmployeeId = item.EmployeeId,
                            EmployeeUserName = item.EmployeeUserName,
                            OwnerId = item.OwnerId,
                            ClaimerId = item.ClaimerId,
                            ConversionCode = item.ConversionCode,
                            RequiredPoints = item.RequiredPoints,
                            MoneyAmount = item.MoneyAmount,
                            PointsOpCode = item.PointsOpCode,
                            PointsOpUsedPoints = item.PointsOpUsedPoints,
                            PointsOpProviderMembershipId = item.PointsOpProviderMembershipId,
                            InternalStatus = item.InternalStatus,
                            InternalStatusName = this.GetStatusName(item.InternalStatus),
                            State = item.State,
                            StateName = this.GetStateName(item.State),
                            UserEmail = item.UserEmail,
                            UserName = item.UserName,
                            ClaimedPromos = item.ClaimedPromos,
                            LastPromoClaimedDate = item.LastPromoClaimed,
                            LastPromoReservedDate = item.LastPromoReserved,
                            MembershipLevel = item.MembershipLevel,
                            MembershipLevelName = this.GetMembershipLevelName(item.MembershipLevel),
                            CreatedDate = item.CreatedDate,
                            CreatedDateLiteral = item.CreatedDate.Date.ToShortDateString(),
                            UpdatedDate = item.UpdatedDate,
                            LastStatusUpdate = item.LastStatusUpdate
                        };

                        moneyConversionLogs.Add(moneyConversionLog);
                    }
                }

            }
            catch (Exception e)
            {
                moneyConversionLogs = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return moneyConversionLogs;
        }

        public List<MoneyConversionLog> Gets(Guid tenantId, string conversionCode, DateTime startDate, DateTime endDate, int pageSize, int pageNumber)
        {
            List<MoneyConversionLog> moneyConversionLogs = null;

            try
            {
                var query = (from x in this._businessObjects.Context.OltpmoneyConversionLogsWithTenantView
                             where x.TenantId == tenantId && x.ConversionCode == conversionCode && x.CreatedDate >= startDate && x.CreatedDate <= endDate
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                if (query != null)
                {
                    MoneyConversionLog moneyConversionLog = null;
                    moneyConversionLogs = new List<MoneyConversionLog>();

                    foreach (OltpmoneyConversionLogsWithTenantView item in query)
                    {
                        moneyConversionLog = new MoneyConversionLog
                        {
                            Id = item.Id,
                            OperationId = item.OperationId,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            BranchName = item.BranchName,
                            EmployeeId = item.EmployeeId,
                            EmployeeUserName = item.EmployeeUserName,
                            OwnerId = item.OwnerId,
                            ClaimerId = item.ClaimerId,
                            ConversionCode = item.ConversionCode,
                            RequiredPoints = item.RequiredPoints,
                            MoneyAmount = item.MoneyAmount,
                            PointsOpCode = item.PointsOpCode,
                            PointsOpUsedPoints = item.PointsOpUsedPoints,
                            PointsOpProviderMembershipId = item.PointsOpProviderMembershipId,
                            InternalStatus = item.InternalStatus,
                            InternalStatusName = this.GetStatusName(item.InternalStatus),
                            State = item.State,
                            StateName = this.GetStateName(item.State),
                            UserEmail = item.UserEmail,
                            UserName = item.UserName,
                            ClaimedPromos = item.ClaimedPromos,
                            LastPromoClaimedDate = item.LastPromoClaimed,
                            LastPromoReservedDate = item.LastPromoReserved,
                            MembershipLevel = item.MembershipLevel,
                            MembershipLevelName = this.GetMembershipLevelName(item.MembershipLevel),
                            CreatedDate = item.CreatedDate,
                            CreatedDateLiteral = item.CreatedDate.Date.ToShortDateString(),
                            UpdatedDate = item.UpdatedDate,
                            LastStatusUpdate = item.LastStatusUpdate,
                            OwnerName = item.OwnerName,
                            OwnerEmail = item.OwnerEmail,
                            TenantName = item.TenantName,
                            TenantContactName = item.TenantContactName,
                            TenantContactEmail = item.TenantContactEmail,
                            TenantContactPhone = item.TenantContactPhone
                        };

                        moneyConversionLogs.Add(moneyConversionLog);
                    }
                }

            }
            catch (Exception e)
            {
                moneyConversionLogs = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return moneyConversionLogs;
        }

        public MoneyConversionLog Get(Guid id)
        {
            MoneyConversionLog moneyConversionLog = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpmoneyConversionLogsView
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (OltpmoneyConversionLogsView item in query)
                    {
                        moneyConversionLog = new MoneyConversionLog
                        {
                            Id = item.Id,
                            OperationId = item.OperationId,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            BranchName = item.BranchName,
                            EmployeeId = item.EmployeeId,
                            EmployeeUserName = item.EmployeeUserName,
                            OwnerId = item.OwnerId,
                            ClaimerId = item.ClaimerId,
                            ConversionCode = item.ConversionCode,
                            RequiredPoints = item.RequiredPoints,
                            MoneyAmount = item.MoneyAmount,
                            PointsOpCode = item.PointsOpCode,
                            PointsOpUsedPoints = item.PointsOpUsedPoints,
                            PointsOpProviderMembershipId = item.PointsOpProviderMembershipId,
                            InternalStatus = item.InternalStatus,
                            InternalStatusName = this.GetStatusName(item.InternalStatus),
                            State = item.State,
                            StateName = this.GetStateName(item.State),
                            UserEmail = item.UserEmail,
                            UserName = item.UserName,
                            ClaimedPromos = item.ClaimedPromos,
                            LastPromoClaimedDate = item.LastPromoClaimed,
                            LastPromoReservedDate = item.LastPromoReserved,
                            MembershipLevel = item.MembershipLevel,
                            MembershipLevelName = this.GetMembershipLevelName(item.MembershipLevel),
                            CreatedDate = item.CreatedDate,
                            CreatedDateLiteral = item.CreatedDate.Date.ToShortDateString(),
                            UpdatedDate = item.UpdatedDate,
                            LastStatusUpdate = item.LastStatusUpdate
                        };

                    }
                }

            }
            catch (Exception e)
            {
                moneyConversionLog = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return moneyConversionLog;
        }

        public MoneyConversionLog Get(Guid id, Guid tenantId)
        {
            MoneyConversionLog moneyConversionLog = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpmoneyConversionLogsWithTenantView
                            where x.TenantId == tenantId && x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (OltpmoneyConversionLogsWithTenantView item in query)
                    {
                        moneyConversionLog = new MoneyConversionLog
                        {
                            Id = item.Id,
                            OperationId = item.OperationId,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            BranchName = item.BranchName,
                            EmployeeId = item.EmployeeId,
                            EmployeeUserName = item.EmployeeUserName,
                            OwnerId = item.OwnerId,
                            ClaimerId = item.ClaimerId,
                            ConversionCode = item.ConversionCode,
                            RequiredPoints = item.RequiredPoints,
                            MoneyAmount = item.MoneyAmount,
                            PointsOpCode = item.PointsOpCode,
                            PointsOpUsedPoints = item.PointsOpUsedPoints,
                            PointsOpProviderMembershipId = item.PointsOpProviderMembershipId,
                            InternalStatus = item.InternalStatus,
                            InternalStatusName = this.GetStatusName(item.InternalStatus),
                            State = item.State,
                            StateName = this.GetStateName(item.State),
                            UserEmail = item.UserEmail,
                            UserName = item.UserName,
                            ClaimedPromos = item.ClaimedPromos,
                            LastPromoClaimedDate = item.LastPromoClaimed,
                            LastPromoReservedDate = item.LastPromoReserved,
                            MembershipLevel = item.MembershipLevel,
                            MembershipLevelName = this.GetMembershipLevelName(item.MembershipLevel),
                            CreatedDate = item.CreatedDate,
                            CreatedDateLiteral = item.CreatedDate.Date.ToShortDateString(),
                            LastStatusUpdate = item.LastStatusUpdate,
                            OwnerName = item.OwnerName,
                            OwnerEmail = item.OwnerEmail,
                            TenantName = item.TenantName,
                            TenantContactName = item.TenantContactName,
                            TenantContactEmail = item.TenantContactEmail,
                            TenantContactPhone = item.TenantContactPhone
                        };

                    }
                }

            }
            catch (Exception e)
            {
                moneyConversionLog = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return moneyConversionLog;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new MoneyConversionLog with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public MoneyConversionLogManager(BusinessObjects businessObjects)
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
