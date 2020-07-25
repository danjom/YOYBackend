using YOY.DTO.Entities;
using YOY.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class HttpcallInvokationLogManager
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
        /// Creates a log
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="controller"></param>
        /// <param name="callId"></param>
        /// <param name="version"></param>
        /// <param name="statusCode"></param>
        /// <param name="operatingSystem"></param>
        /// <param name="parameters"></param>
        /// <param name="remainingMinsToTrigger"></param>
        /// <param name="minRetriggeredMins"></param>
        /// <param name="retrievedContent"></param>
        /// <param name="lastValidCallId"></param>
        /// <param name="callType"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public HttpcallInvokationLog Post(string requesterId, string controller, int callId, int version, int statusCode,
            int operatingSystem, string parameters, int remainingMinsToTrigger, int minRetriggeredMins, bool retrievedContent,
            Guid? lastValidCallId, int callType, string message)
        {
            HttpcallInvokationLog invokation;
            try
            {
                OltphttpcallInvokationLogs newInvokation = new OltphttpcallInvokationLogs
                {
                    Id = Guid.NewGuid(),
                    RequesterId = requesterId,
                    Controller = controller,
                    Call = callId,
                    Version = version,
                    StatusCode = statusCode,
                    OperationSystem = operatingSystem,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Params = parameters,
                    MinRetriggeredMins = minRetriggeredMins,
                    RemainingMinToTrigger = remainingMinsToTrigger,
                    LastValidCallId = lastValidCallId,
                    RetrievedContent = retrievedContent,
                    CallType = callType,
                    Message = message,
                };

                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();//New context is created because this call is part of an async logic


                context.OltphttpcallInvokationLogs.Add(newInvokation);
                context.SaveChanges();

                invokation = new HttpcallInvokationLog
                {
                    Id = newInvokation.Id,
                    RequesterId = newInvokation.RequesterId,
                    Controller = newInvokation.Controller,
                    Call = newInvokation.Call,
                    Version = newInvokation.Version,
                    StatusCode = newInvokation.StatusCode,
                    OperationSystem = newInvokation.OperationSystem,
                    CreatedDate = newInvokation.CreatedDate,
                    UpdatedDate = newInvokation.UpdatedDate,
                    Params = newInvokation.Params,
                    MinRetriggeredMins = newInvokation.MinRetriggeredMins,
                    RemainingMinsToTrigger = newInvokation.RemainingMinToTrigger,
                    LastValidCall = newInvokation.LastValidCallId,
                    RetrievedContent = newInvokation.RetrievedContent,
                    CallType = newInvokation.Call,
                    Message = newInvokation.Message,
                };

                /*
                if (newInvokation.LastValidCallId != null)
                {

                    invokation.LastValidCallDate = newInvokation.OLTPHttpcallInvokationLog1.CreatedDate;
                }*/
            }
            catch (Exception e)
            {
                invokation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return invokation;
        }

        public List<HttpcallInvokationLog> Gets(string controller, int version, int call)
        {
            List<HttpcallInvokationLog> logs = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltphttpcallInvokationLogs
                            where x.Controller == controller && x.Version == version && x.Call == call
                            select x;

                if (query != null)
                {
                    HttpcallInvokationLog log;
                    logs = new List<HttpcallInvokationLog>();

                    foreach (OltphttpcallInvokationLogs item in query)
                    {
                        log = new HttpcallInvokationLog
                        {
                            Id = item.Id,
                            RequesterId = item.RequesterId,
                            Controller = item.Controller,
                            Call = item.Call,
                            Version = item.Version,
                            StatusCode = item.StatusCode,
                            OperationSystem = item.OperationSystem,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            Params = item.Params,
                            MinRetriggeredMins = item.MinRetriggeredMins,
                            RemainingMinsToTrigger = item.RemainingMinToTrigger,
                            LastValidCall = item.LastValidCallId,
                            RetrievedContent = item.RetrievedContent,
                            CallType = item.CallType,
                            Message = item.Message,
                        };

                        /*
                        if (item.LastValidCallId != null)
                        {
                            log.LastValidCallDate = item.OLTPHttpcallInvokationLog1.CreatedDate;
                        }*/

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

        public List<HttpcallInvokationLog> Gets(string requesterId, string controller, int version, int call, int pageSize, int pageNumber)
        {
            List<HttpcallInvokationLog> logs = null;

            try
            {
                var query = (from x in this._businessObjects.Context.OltphttpcallInvokationLogs
                             where x.RequesterId == requesterId && x.Controller == controller && x.Version == version && x.Call == call
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                if (query != null)
                {
                    HttpcallInvokationLog log;
                    logs = new List<HttpcallInvokationLog>();

                    foreach (OltphttpcallInvokationLogs item in query)
                    {
                        log = new HttpcallInvokationLog
                        {
                            Id = item.Id,
                            RequesterId = item.RequesterId,
                            Controller = item.Controller,
                            Call = item.Call,
                            Version = item.Version,
                            StatusCode = item.StatusCode,
                            OperationSystem = item.OperationSystem,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            Params = item.Params,
                            MinRetriggeredMins = item.MinRetriggeredMins,
                            RemainingMinsToTrigger = item.RemainingMinToTrigger,
                            LastValidCall = item.LastValidCallId,
                            RetrievedContent = item.RetrievedContent,
                            CallType = item.CallType,
                            Message = item.Message,
                        };

                        /*
                        if (item.LastValidCallId != null)
                        {
                            log.LastValidCallDate = item.OLTPHttpcallInvokationLog1.CreatedDate;
                        }*/

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

        public HttpcallInvokationLog Get(string requesterId, string controller, int version, int call)
        {
            HttpcallInvokationLog log = null;

            try
            {
                OltphttpcallInvokationLogs item = (from x in this._businessObjects.Context.OltphttpcallInvokationLogs
                                                    where x.RequesterId == requesterId && x.Controller == controller && x.Version == version && x.Call == call
                                                            && x.StatusCode == StatusCodes.Ok && x.RetrievedContent
                                                    orderby x.CreatedDate descending
                                                    select x).FirstOrDefault();

                if (item != null)
                {
                    log = new HttpcallInvokationLog
                    {
                        Id = item.Id,
                        RequesterId = item.RequesterId,
                        Controller = item.Controller,
                        Call = item.Call,
                        Version = item.Version,
                        StatusCode = item.StatusCode,
                        OperationSystem = item.OperationSystem,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Params = item.Params,
                        MinRetriggeredMins = item.MinRetriggeredMins,
                        RemainingMinsToTrigger = item.RemainingMinToTrigger,
                        LastValidCall = item.LastValidCallId,
                        RetrievedContent = item.RetrievedContent,
                        CallType = item.CallType,
                        Message = item.Message,
                    };
                    
                    /*
                    if (item.LastValidCallId != null)
                    {
                        log.LastValidCallDate = item.OLTPHttpcallInvokationLog1.CreatedDate;
                    }*/

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

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new HttpcallInvokation with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public HttpcallInvokationLogManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD TABLE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion

        #endregion
    }
}
