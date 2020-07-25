using System;
using System.Collections.Generic;
using System.Text;
using YOY.DTO.Entities;
using YOY.Values;
using System.Linq;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class OperationFlowStepLogManager
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
        /// This method has the purpose to retrieve all the steps from the most recent incomplete flow
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="ownerType"></param>
        /// <param name="discriminator"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<OperationFlowStepLog> Gets(string ownerId, int ownerType, string discriminator, DateTime date)
        {
            List<OperationFlowStepLog> stepLogs = new List<OperationFlowStepLog>();

            try
            {
                var query = (dynamic)null;


                if(ownerType != OperationFlowOwners.All)
                {
                    query = from x in this._businessObjects.Context.OltpoperationFlowStepLogs
                            where x.IsValid && x.Allowed && x.ReferenceId == null && x.OwnerType == ownerType && x.OwnerId == ownerId && x.Discriminator == discriminator && x.CreatedDate >= date
                            orderby x.Step descending, x.CreatedDate descending
                            select x;

                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpoperationFlowStepLogs
                            where x.IsValid && x.Allowed && x.ReferenceId == null && x.Discriminator == discriminator && x.CreatedDate >= date
                            orderby x.Step descending, x.CreatedDate descending
                            select x;
                }

                if(query != null)
                {
                    stepLogs = new List<OperationFlowStepLog>();
                    OperationFlowStepLog stepLog;
                    string operationCode = "";

                    foreach(OltpoperationFlowStepLogs item in query)
                    {
                        if (string.IsNullOrEmpty(operationCode))
                            operationCode = item.OperationFlowCode;

                        if(item.OperationFlowCode.CompareTo(operationCode) == 0)
                        {

                            stepLog = new OperationFlowStepLog
                            {
                                Id = item.Id,
                                Tenant = item.TenantId,
                                BranchId = item.BranchId,
                                OwnerId = item.OwnerId,
                                OwnerType = item.OwnerType,
                                OwnerTypeName = "",//Pending
                                OperationType = item.OperationType,
                                OperationTypeName = "",//Pending,
                                OriginOpStepLogId = item.OriginOpStepLogId,
                                OperationFlowCode = item.OperationFlowCode,
                                Discriminator = item.Discriminator,
                                ReferenceId = item.ReferenceId,
                                ReferenceType = item.ReferenceType,
                                ReferenceTypeName = "",//Pending
                                SourceId = item.SourceId,
                                SourceType = item.SourceType,
                                SourceTypeName = "",//Pending
                                Step = item.Step,
                                IsValid = item.IsValid,
                                Allowed = item.Allowed,
                                FlowCompleter = item.FlowCompleter,
                                CreatedDate = item.CreatedDate,
                                UpdatedDate = item.UpdatedDate
                            };

                            stepLogs.Add(stepLog);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                stepLogs = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return stepLogs;
        }

        public Guid? Post(Guid tenantId, Guid? branchId, string ownerId, int ownerType, int operationType, Guid? originOpStepLogId, string operationFlowCode, string discriminator, Guid? referenceId,
            int referenceType, Guid? sourceId, int sourceType, int step, bool isValid, bool allowed, bool flowCompleter)
        {
            Guid? newStepFlowId;

            try
            {
                OltpoperationFlowStepLogs newLog = new OltpoperationFlowStepLogs
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    BranchId = branchId,
                    OwnerId = ownerId,
                    OwnerType = ownerType,
                    OperationType = operationType,
                    OriginOpStepLogId = originOpStepLogId,
                    OperationFlowCode = operationFlowCode,
                    Discriminator = discriminator,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    SourceId = sourceId,
                    SourceType = sourceType,
                    Step = step,
                    IsValid = isValid,
                    Allowed = allowed,
                    FlowCompleter = flowCompleter,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpoperationFlowStepLogs.Add(newLog);
                this._businessObjects.Context.SaveChanges();

                newStepFlowId = newLog.Id;
            }
            catch(Exception e)
            {
                newStepFlowId = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return newStepFlowId;
        }

        public bool Put(Guid id, int changeState)
        {
            bool success = false;

            try
            {
                OltpoperationFlowStepLogs flowStep = (from x in this._businessObjects.Context.OltpoperationFlowStepLogs
                                                     where x.Id == id
                                                     select x).FirstOrDefault();


                if(flowStep != null)
                {
                    switch (changeState)
                    {
                        case ChangeTypes.IsValidState:

                            flowStep.IsValid = !flowStep.IsValid;
                            flowStep.UpdatedDate = DateTime.UtcNow;

                            this._businessObjects.Context.SaveChanges();
                            
                            success = true;

                            break;
                        case ChangeTypes.AllowedState:

                            flowStep.Allowed = !flowStep.Allowed;
                            flowStep.UpdatedDate = DateTime.UtcNow;

                            this._businessObjects.Context.SaveChanges();

                            success = true;

                            break;
                        case ChangeTypes.CompleterState:

                            flowStep.FlowCompleter = !flowStep.FlowCompleter;
                            flowStep.UpdatedDate = DateTime.UtcNow;

                            this._businessObjects.Context.SaveChanges();

                            success = true;

                            break;

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

        public bool Put(int operationType, Guid id)
        {
            bool success = false;

            try
            {
                OltpoperationFlowStepLogs flowStep = (from x in this._businessObjects.Context.OltpoperationFlowStepLogs
                                                      where x.Id == id
                                                      select x).FirstOrDefault();


                if (flowStep != null)
                {

                    flowStep.OperationType = operationType;
                    flowStep.UpdatedDate = DateTime.UtcNow;

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

        /// <summary>
        /// This method sets a reference for a complete flow, and sets the most recent one as the completer
        /// </summary>
        /// <param name="flowCode"></param>
        /// <param name="referenceId"></param>
        /// <param name="referenceType"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool Put(string flowCode, Guid referenceId, int referenceType, DateTime date)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpoperationFlowStepLogs
                            where x.OperationFlowCode == flowCode && x.CreatedDate >= date
                            orderby x.CreatedDate descending
                            select x;


                if (query != null)
                {

                    foreach(OltpoperationFlowStepLogs item in query)
                    {
                        item.ReferenceType = referenceType;
                        item.ReferenceId = referenceId;
                        item.UpdatedDate = DateTime.UtcNow;

                    }


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

        #region CONSTRUCTOR

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new SmsLogManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public OperationFlowStepLogManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD OPERATION FLOW STEP LOG MANAGER ------------------------------------------------------------------------------------------------------------------------ //


        #endregion
    }
}
