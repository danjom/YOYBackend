using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.BroadcastingLog;

namespace YOY.DAO.Entities.Manager
{
    public class BroadcastingLogManager
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
        /// Retrieve all the logs for a campaign
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public List<BroadcastingLog> Gets(int referenceType, Guid referenceId, int broadcasterType, int expiredState, int viewedState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<BroadcastingLog> logs = new List<BroadcastingLog>();

            try
            {
                var query = (dynamic)null;

                if (broadcasterType == BroadcasterTypes.All)
                {

                    switch (expiredState)
                    {
                        case ExpiredStates.All:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.ContainedContentIds.Contains(referenceType + ":" + referenceId)
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId)
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where !(x.OpenedDateTime.HasValue) && x.ContainedContentIds.Contains(referenceType + ":" + referenceId)
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Valid:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where !x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Expired:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where !(x.OpenedDateTime.HasValue) && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                    }
                }
                else
                {

                    switch (expiredState)
                    {
                        case ExpiredStates.All:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where !x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Valid:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where !x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Expired:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where !x.OpenedDateTime.HasValue && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                    }
                }


                BroadcastingLog log = null;
                foreach (OltpbroadcastingLogsDataView item in query)
                {
                    log = new BroadcastingLog
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        Username = item.UserName,
                        UserAccNumber = item.UserAccNumber,
                        UserEmail = item.UserEmail,
                        UserPhoneNumber = item.UserPhoneNumber,
                        SentDate = item.SentDate,
                        ExpirationDate = item.ExpirationDate,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterId = item.BroadcasterId,
                        MsgTitle = item.MsgTitle,
                        MsgContent = item.MsgContent,
                        OpenedDate = item.OpenedDateTime,
                        TriggeredLatitude = item.TriggeredLatitude,
                        TriggeredLongitude = item.TriggeredLongitude,
                        ContainedContentCount = item.ContainedContentCount,
                        ContainedContentIds = item.ContainedContentIds,
                        ContentRedeemed = item.ContentRedeemed,
                        RedeemedContentIds = item.RedeemContentIds,
                        ViewCount = item.ViewedCount,
                    };

                    logs.Add(log);
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


        /// <summary>
        /// Retrieve all the logs for a campaign in a time interval
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="broadcasterType"></param>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        public List<BroadcastingLog> Gets(int referenceType, Guid referenceId, int broadcasterType, DateTime minDate, DateTime maxDate, int viewedState, int pageSize, int pageNumber)
        {
            List<BroadcastingLog> logs = new List<BroadcastingLog>();

            try
            {

                var query = (dynamic)null;

                if (broadcasterType == BroadcastingChannelTypes.All)
                {
                    switch (viewedState)
                    {
                        case ViewedStates.All:
                            query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                     where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.SentDate >= minDate && x.SentDate <= maxDate
                                     orderby x.SentDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                            break;
                        case ViewedStates.Seen:
                            query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                     where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.SentDate >= minDate && x.SentDate <= maxDate && x.OpenedDateTime.HasValue
                                     orderby x.SentDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                            break;
                        case ViewedStates.Unseen:
                            query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                     where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.SentDate >= minDate && x.SentDate <= maxDate && !x.OpenedDateTime.HasValue
                                     orderby x.SentDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                            break;
                    }

                }
                else
                {

                    switch (viewedState)
                    {
                        case ViewedStates.All:
                            query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                     where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate
                                     orderby x.SentDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                            break;
                        case ViewedStates.Seen:
                            query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                     where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && x.OpenedDateTime.HasValue
                                     orderby x.SentDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                            break;
                        case ViewedStates.Unseen:
                            query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                     where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && !x.OpenedDateTime.HasValue
                                     orderby x.SentDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                            break;
                    }
                }

                BroadcastingLog log = null;
                foreach (OltpbroadcastingLogsDataView item in query)
                {
                    log = new BroadcastingLog
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        Username = item.UserName,
                        UserAccNumber = item.UserAccNumber,
                        UserEmail = item.UserEmail,
                        UserPhoneNumber = item.UserPhoneNumber,
                        SentDate = item.SentDate,
                        ExpirationDate = item.ExpirationDate,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterId = item.BroadcasterId,
                        MsgTitle = item.MsgTitle,
                        MsgContent = item.MsgContent,
                        OpenedDate = item.OpenedDateTime,
                        TriggeredLatitude = item.TriggeredLatitude,
                        TriggeredLongitude = item.TriggeredLongitude,
                        ContainedContentCount = item.ContainedContentCount,
                        ContainedContentIds = item.ContainedContentIds,
                        ContentRedeemed = item.ContentRedeemed,
                        RedeemedContentIds = item.RedeemContentIds,
                        ViewCount = item.ViewedCount,
                    };

                    logs.Add(log);
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

        /// <summary>
        /// Retrieve all the logs for a user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<BroadcastingLog> Gets(string userId, int broadcasterType, int expiredState, int viewedState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<BroadcastingLog> logs = new List<BroadcastingLog>();

            try
            {
                var query = (dynamic)null;

                if (broadcasterType == BroadcastingChannelTypes.All)
                {
                    switch (expiredState)
                    {
                        case ExpiredStates.All:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && !x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Valid:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.ExpirationDate > dateTime && x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.ExpirationDate > dateTime && !x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Expired:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.ExpirationDate <= dateTime && x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.ExpirationDate <= dateTime && !x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                    }

                }
                else
                {

                    switch (expiredState)
                    {
                        case ExpiredStates.All:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && !x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Valid:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.ExpirationDate > dateTime && x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.ExpirationDate > dateTime && !x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Expired:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.ExpirationDate <= dateTime && x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.ExpirationDate <= dateTime && !x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                    }
                }


                BroadcastingLog log = null;
                foreach (OltpbroadcastingLogsDataView item in query)
                {
                    log = new BroadcastingLog
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        Username = item.UserName,
                        UserAccNumber = item.UserAccNumber,
                        UserEmail = item.UserEmail,
                        UserPhoneNumber = item.UserPhoneNumber,
                        SentDate = item.SentDate,
                        ExpirationDate = item.ExpirationDate,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterId = item.BroadcasterId,
                        MsgTitle = item.MsgTitle,
                        MsgContent = item.MsgContent,
                        OpenedDate = item.OpenedDateTime,
                        TriggeredLatitude = item.TriggeredLatitude,
                        TriggeredLongitude = item.TriggeredLongitude,
                        ContainedContentCount = item.ContainedContentCount,
                        ContainedContentIds = item.ContainedContentIds,
                        ContentRedeemed = item.ContentRedeemed,
                        RedeemedContentIds = item.RedeemContentIds,
                        ViewCount = item.ViewedCount,
                    };

                    logs.Add(log);
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

        /// <summary>
        /// Retrieve all the logs for a user in a time interval
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        public List<BroadcastingLog> Gets(string userId, int broadcasterType, DateTime minDate, DateTime maxDate, int viewedState, int expiredState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<BroadcastingLog> logs = new List<BroadcastingLog>();

            try
            {
                var query = (dynamic)null;

                if (broadcasterType == BroadcastingChannelTypes.All)
                {

                    switch (expiredState)
                    {
                        case ExpiredStates.All:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && !x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Valid:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.OpenedDateTime.HasValue && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && !x.OpenedDateTime.HasValue && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Expired:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.OpenedDateTime.HasValue && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && !x.OpenedDateTime.HasValue && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                    }

                }
                else
                {
                    switch (expiredState)
                    {
                        case ExpiredStates.All:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && !x.OpenedDateTime.HasValue
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Valid:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && x.OpenedDateTime.HasValue && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && !x.OpenedDateTime.HasValue && x.ExpirationDate > dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                        case ExpiredStates.Expired:
                            switch (viewedState)
                            {
                                case ViewedStates.All:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Seen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && x.OpenedDateTime.HasValue && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ViewedStates.Unseen:
                                    query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.SentDate >= minDate && x.SentDate <= maxDate && !x.OpenedDateTime.HasValue && x.ExpirationDate <= dateTime
                                             orderby x.SentDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                            break;
                    }

                }

                BroadcastingLog log = null;
                foreach (OltpbroadcastingLogsDataView item in query)
                {
                    log = new BroadcastingLog
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        Username = item.UserName,
                        UserAccNumber = item.UserAccNumber,
                        UserEmail = item.UserEmail,
                        UserPhoneNumber = item.UserPhoneNumber,
                        SentDate = item.SentDate,
                        ExpirationDate = item.ExpirationDate,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterId = item.BroadcasterId,
                        MsgTitle = item.MsgTitle,
                        MsgContent = item.MsgContent,
                        OpenedDate = item.OpenedDateTime,
                        TriggeredLatitude = item.TriggeredLatitude,
                        TriggeredLongitude = item.TriggeredLongitude,
                        ContainedContentCount = item.ContainedContentCount,
                        ContainedContentIds = item.ContainedContentIds,
                        ContentRedeemed = item.ContentRedeemed,
                        RedeemedContentIds = item.RedeemContentIds,
                        ViewCount = item.ViewedCount,
                    };

                    logs.Add(log);
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

        /// <summary>
        /// Retrieve all the logs for a user in a campaign ordered chronogically descending
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public List<BroadcastingLog> Gets(int referenceType, Guid referenceId, string userId, int expiredState, int viewedState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<BroadcastingLog> logs = new List<BroadcastingLog>();

            try
            {
                var query = (dynamic)null;
                switch (expiredState)
                {
                    case ExpiredStates.All:
                        switch (viewedState)
                        {
                            case ViewedStates.All:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Seen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Unseen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && !x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                    case ExpiredStates.Valid:
                        switch (viewedState)
                        {
                            case ViewedStates.All:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.ExpirationDate > dateTime
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Seen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.ExpirationDate > dateTime && x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Unseen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.ExpirationDate > dateTime && !x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                    case ExpiredStates.Expired:
                        switch (viewedState)
                        {
                            case ViewedStates.All:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.ExpirationDate <= dateTime
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Seen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.ExpirationDate <= dateTime && x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Unseen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.ExpirationDate <= dateTime && !x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;

                }

                BroadcastingLog log = null;
                foreach (OltpbroadcastingLogsDataView item in query)
                {
                    log = new BroadcastingLog
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        Username = item.UserName,
                        UserAccNumber = item.UserAccNumber,
                        UserEmail = item.UserEmail,
                        UserPhoneNumber = item.UserPhoneNumber,
                        SentDate = item.SentDate,
                        ExpirationDate = item.ExpirationDate,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterId = item.BroadcasterId,
                        MsgTitle = item.MsgTitle,
                        MsgContent = item.MsgContent,
                        OpenedDate = item.OpenedDateTime,
                        TriggeredLatitude = item.TriggeredLatitude,
                        TriggeredLongitude = item.TriggeredLongitude,
                        ContainedContentCount = item.ContainedContentCount,
                        ContainedContentIds = item.ContainedContentIds,
                        ContentRedeemed = item.ContentRedeemed,
                        RedeemedContentIds = item.RedeemContentIds,
                        ViewCount = item.ViewedCount,
                    };

                    logs.Add(log);
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

        /// <summary>
        /// Retrieve all the logs for a user in a campaign
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public List<BroadcastingLog> Gets(int referenceType, Guid referenceId, string userId, DateTime minDate, DateTime maxDate, int viewedState, int expiredState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<BroadcastingLog> logs = new List<BroadcastingLog>();

            try
            {
                var query = (dynamic)null;

                switch (expiredState)
                {
                    case ExpiredStates.All:
                        switch (viewedState)
                        {
                            case ViewedStates.All:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Seen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Unseen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && !x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                    case ExpiredStates.Valid:
                        switch (viewedState)
                        {
                            case ViewedStates.All:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate > dateTime
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Seen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate > dateTime && x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Unseen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate > dateTime && !x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                    case ExpiredStates.Expired:
                        switch (viewedState)
                        {
                            case ViewedStates.All:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate <= dateTime
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Seen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate <= dateTime && x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ViewedStates.Unseen:
                                query = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                         where x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.UserId == userId && x.SentDate >= minDate && x.SentDate <= maxDate && x.ExpirationDate <= dateTime && !x.OpenedDateTime.HasValue
                                         orderby x.SentDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                }

                BroadcastingLog log = null;
                foreach (OltpbroadcastingLogsDataView item in query)
                {
                    log = new BroadcastingLog
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        Username = item.UserName,
                        UserAccNumber = item.UserAccNumber,
                        UserEmail = item.UserEmail,
                        UserPhoneNumber = item.UserPhoneNumber,
                        SentDate = item.SentDate,
                        ExpirationDate = item.ExpirationDate,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterId = item.BroadcasterId,
                        MsgTitle = item.MsgTitle,
                        MsgContent = item.MsgContent,
                        OpenedDate = item.OpenedDateTime,
                        TriggeredLatitude = item.TriggeredLatitude,
                        TriggeredLongitude = item.TriggeredLongitude,
                        ContainedContentCount = item.ContainedContentCount,
                        ContainedContentIds = item.ContainedContentIds,
                        ContentRedeemed = item.ContentRedeemed,
                        RedeemedContentIds = item.RedeemContentIds,
                        ViewCount = item.ViewedCount,
                    };

                    logs.Add(log);
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

        /// <summary>
        /// Retrieve the lastest or the oldest broadcasting message for each broadcaster type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        public BroadcastingLog Get(string userId, Guid? referenceId, int? referenceType, int timeLimitType, int broadcasterType)
        {
            BroadcastingLog log = null;

            try
            {

                var query = (dynamic)null;

                switch (timeLimitType)
                {
                    case ChronologicalOrders.Descending:

                        if (broadcasterType != BroadcastingChannelTypes.All)
                        {
                            if (referenceId != null)
                            {
                                query = from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                        where x.UserId == userId && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType
                                        orderby x.SentDate descending
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                        where x.UserId == userId && x.BroadcasterType == broadcasterType
                                        orderby x.SentDate descending
                                        select x;
                            }

                        }
                        else
                        {
                            if (referenceType != null)
                            {
                                query = from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                        where x.UserId == userId && x.ContainedContentIds.Contains(referenceType + ":" + referenceId)
                                        orderby x.SentDate descending
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                        where x.UserId == userId
                                        orderby x.SentDate descending
                                        select x;
                            }

                        }

                        break;
                    case ChronologicalOrders.Ascending:

                        if (broadcasterType != BroadcastingChannelTypes.All)
                        {
                            if (referenceId != null)
                            {
                                query = from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                        where x.UserId == userId && x.ContainedContentIds.Contains(referenceType + ":" + referenceId) && x.BroadcasterType == broadcasterType
                                        orderby x.SentDate ascending
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                        where x.UserId == userId && x.BroadcasterType == broadcasterType
                                        orderby x.SentDate ascending
                                        select x;
                            }

                        }
                        else
                        {
                            if (referenceId != null)
                            {
                                query = from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                        where x.UserId == userId && x.ContainedContentIds.Contains(referenceType + ":" + referenceId)
                                        orderby x.SentDate ascending
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                        where x.UserId == userId
                                        orderby x.SentDate ascending
                                        select x;
                            }

                        }


                        break;
                }

                OltpbroadcastingLogsData item = null;

                if (query.Any())
                {
                    item = query.FirstOrDefault();

                    log = new BroadcastingLog
                    {
                        
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        Username = item.UserName,
                        UserAccNumber = item.UserAccNumber,
                        UserEmail = item.UserEmail,
                        UserPhoneNumber = item.UserPhoneNumber,
                        SentDate = item.SentDate,
                        ExpirationDate = item.ExpirationDate,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterId = item.BroadcasterId,
                        MsgTitle = item.MsgTitle,
                        MsgContent = item.MsgContent,
                        OpenedDate = item.OpenedDateTime,
                        TriggeredLatitude = item.TriggeredLatitude,
                        TriggeredLongitude = item.TriggeredLongitude,
                        ContainedContentCount = item.ContainedContentCount,
                        ContainedContentIds = item.ContainedContentIds,
                        ContentRedeemed = item.ContentRedeemed,
                        RedeemedContentIds = item.RedeemContentIds,
                        ViewCount = item.ViewedCount,
                    };

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

        /// <summary>
        /// Retrieve a specific log
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public BroadcastingLog Get(Guid id)
        {
            BroadcastingLog log = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                            where x.Id == id
                            select x;

                foreach (OltpbroadcastingLogsDataView item in query)
                {
                    log = new BroadcastingLog
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        Username = item.UserName,
                        UserAccNumber = item.UserAccNumber,
                        UserEmail = item.UserEmail,
                        UserPhoneNumber = item.UserPhoneNumber,
                        SentDate = item.SentDate,
                        ExpirationDate = item.ExpirationDate,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterId = item.BroadcasterId,
                        MsgTitle = item.MsgTitle,
                        MsgContent = item.MsgContent,
                        OpenedDate = item.OpenedDateTime,
                        TriggeredLatitude = item.TriggeredLatitude,
                        TriggeredLongitude = item.TriggeredLongitude,
                        ContainedContentCount = item.ContainedContentCount,
                        ContainedContentIds = item.ContainedContentIds,
                        ContentRedeemed = item.ContentRedeemed,
                        RedeemedContentIds = item.RedeemContentIds,
                        ViewCount = item.ViewedCount,
                    };

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

        /// <summary>
        /// Creates a new message log
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BroadcastingLog Post(Guid tenantId, string userId, DateTime sentDate, DateTime expirationDate, int broadcasterType, Guid broadcasterId, string msgTitle, string msgContent, decimal? triggeredLatitude, decimal? triggeredLongitude, int contentCount, string containedContentIds, int referenceType, string referenceName)
        {
            BroadcastingLog log;
            try
            {
                OltpbroadcastingLogs newLog = new OltpbroadcastingLogs
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    UserId = userId,
                    SentDate = sentDate,
                    ExpirationDate = expirationDate,
                    BroadcasterType = broadcasterType,
                    BroadcasterId = broadcasterId,
                    MsgTitle = msgTitle,
                    MsgContent = msgContent,
                    OpenedDateTime = null,
                    TriggeredLatitude = triggeredLatitude,
                    TriggeredLongitude = triggeredLongitude,
                    ContainedContentCount = contentCount,
                    ContainedContentIds = containedContentIds,
                    ContentRedeemed = null,
                    RedeemContentIds = "",
                    ViewedCount = 0,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpbroadcastingLogs.Add(newLog);
                this._businessObjects.Context.SaveChanges();

                OltpbroadcastingLogsDataView newLogData = (from x in this._businessObjects.Context.OltpbroadcastingLogsDataView
                                                           where x.Id == newLog.Id
                                                           select x).FirstOrDefault();

                if (newLogData != null)
                {
                    log = new BroadcastingLog
                    {
                        Id = newLogData.Id,
                        TenantId = newLogData.TenantId,
                        UserId = newLogData.UserId,
                        Username = newLogData.UserName,
                        UserAccNumber = newLogData.UserAccNumber,
                        UserEmail = newLogData.UserEmail,
                        UserPhoneNumber = newLogData.UserPhoneNumber,
                        SentDate = newLogData.SentDate,
                        ExpirationDate = newLogData.ExpirationDate,
                        BroadcasterType = newLogData.BroadcasterType,
                        BroadcasterId = newLogData.BroadcasterId,
                        MsgTitle = newLogData.MsgTitle,
                        MsgContent = newLogData.MsgContent,
                        OpenedDate = newLogData.OpenedDateTime,
                        TriggeredLatitude = newLogData.TriggeredLatitude,
                        TriggeredLongitude = newLogData.TriggeredLongitude,
                        ContainedContentCount = newLogData.ContainedContentCount,
                        ContainedContentIds = newLogData.ContainedContentIds,
                        ContentRedeemed = newLogData.ContentRedeemed,
                        RedeemedContentIds = newLogData.RedeemContentIds,
                        ViewCount = newLogData.ViewedCount,
                    };
                }
                else
                    log = null;
            }
            catch (Exception e)
            {
                log = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return log;
        }

        #endregion

        #region BROADCASTINGLOGRECORDS

        public List<BroadcastingLogRecord> Gets(Guid logId, int expiredState, DateTime dateTime)
        {
            List<BroadcastingLogRecord> records = null;


            try
            {
                var query = (dynamic)null;

                switch (expiredState)
                {
                    case ExpiredStates.All:
                        query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                                where x.BroadcastingLogId == logId
                                select x;
                        break;
                    case ExpiredStates.Valid:
                        query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                                where x.BroadcastingLogId == logId && x.ExpirationDate > dateTime
                                select x;
                        break;
                    case ExpiredStates.Expired:
                        query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                                where x.BroadcastingLogId == logId && x.ExpirationDate <= dateTime
                                select x;
                        break;
                }

                if (query != null)
                {
                    BroadcastingLogRecord record = null;
                    records = new List<BroadcastingLogRecord>();

                    foreach (OltpbroadcastingLogRecords item in query)
                    {
                        record = new BroadcastingLogRecord
                        {
                            Id = item.Id,
                            BroadcastingLogId = item.BroadcastingLogId,
                            TenantId = item.TenantId,
                            UserId = item.UserId,
                            ReferenceType = item.ReferenceType,
                            ReferenceId = item.ReferenceId,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate,
                            ViewCount = item.ViewCount,
                            RedemptionCount = item.RedemptionCount
                        };

                        records.Add(record);
                    }
                }
            }
            catch (Exception e)
            {
                records = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return records;
        }

        public List<BroadcastingLogRecord> Gets(Guid? tenantId, string userId, int expiredState, DateTime dateTime)
        {
            List<BroadcastingLogRecord> records = null;


            try
            {
                var query = (dynamic)null;

                switch (expiredState)
                {
                    case ExpiredStates.All:
                        if (tenantId != null)
                        {
                            query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                                    where x.UserId == userId && x.TenantId == (Guid)tenantId
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                                    where x.UserId == userId
                                    select x;
                        }
                        break;
                    case ExpiredStates.Valid:
                        if (tenantId != null)
                        {
                            query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                                    where x.UserId == userId && x.TenantId == (Guid)tenantId && x.ExpirationDate > dateTime
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                                    where x.UserId == userId && x.ExpirationDate > dateTime
                                    select x;
                        }
                        break;
                    case ExpiredStates.Expired:
                        if (tenantId != null)
                        {
                            query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                                    where x.UserId == userId && x.TenantId == (Guid)tenantId && x.ExpirationDate <= dateTime
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                                    where x.UserId == userId && x.ExpirationDate <= dateTime
                                    select x;
                        }
                        break;
                }

                if (query != null)
                {
                    BroadcastingLogRecord record = null;
                    records = new List<BroadcastingLogRecord>();

                    foreach (OltpbroadcastingLogRecords item in query)
                    {
                        record = new BroadcastingLogRecord
                        {
                            Id = item.Id,
                            BroadcastingLogId = item.BroadcastingLogId,
                            TenantId = item.TenantId,
                            UserId = item.UserId,
                            ReferenceType = item.ReferenceType,
                            ReferenceId = item.ReferenceId,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate,
                            ViewCount = item.ViewCount,
                            RedemptionCount = item.RedemptionCount
                        };

                        records.Add(record);
                    }
                }
            }
            catch (Exception e)
            {
                records = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return records;
        }

        public BroadcastingLogRecord Post(Guid broadcastingLogId, Guid tenantId, string userId, Guid referenceId, int referenceType, DateTime expirationDate)
        {
            BroadcastingLogRecord record;

            try
            {
                OltpbroadcastingLogRecords newRecord = new OltpbroadcastingLogRecords
                {
                    Id = Guid.NewGuid(),
                    BroadcastingLogId = broadcastingLogId,
                    TenantId = tenantId,
                    UserId = userId,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    ExpirationDate = expirationDate,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    ViewCount = 0,
                    RedemptionCount = 0
                };

                this._businessObjects.Context.OltpbroadcastingLogRecords.Add(newRecord);
                this._businessObjects.Context.SaveChanges();

                record = new BroadcastingLogRecord
                {
                    Id = newRecord.Id,
                    BroadcastingLogId = newRecord.BroadcastingLogId,
                    TenantId = newRecord.TenantId,
                    UserId = newRecord.UserId,
                    ReferenceType = newRecord.ReferenceType,
                    ReferenceId = newRecord.ReferenceId,
                    CreatedDate = newRecord.CreatedDate,
                    UpdatedDate = newRecord.UpdatedDate,
                    ExpirationDate = newRecord.ExpirationDate,
                    ViewCount = newRecord.ViewCount,
                    RedemptionCount = newRecord.RedemptionCount
                };


            }
            catch(Exception e)
            {
                record = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return record;
        }

        public bool Put(Guid id, Guid? broadcastingLogId, int changeType, int quantity)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbroadcastingLogRecords
                            where x.Id == id
                            select x;

                if(query != null)
                {
                    OltpbroadcastingLogRecords record = null;

                    foreach(OltpbroadcastingLogRecords item in query)
                    {
                        record = item;
                    }

                    if(record != null)
                    {
                        OltpbroadcastingLogs log = (from x in this._businessObjects.Context.OltpbroadcastingLogs
                                                    where x.Id == record.BroadcastingLogId
                                                    select x).FirstOrDefault();

                        switch (changeType)
                        {
                            case BroadcastingLogRecordChangeTypes.ViewCount:
                                record.ViewCount += quantity;

                                if(log != null)
                                {
                                    log.ViewedCount += quantity;

                                    this._businessObjects.Context.SaveChanges();

                                    success = true;
                                }

                                break;
                            case BroadcastingLogRecordChangeTypes.RedemptionCount:

                                record.RedemptionCount += quantity;

                                if(log != null)
                                {
                                    log.ContentRedeemed = true;

                                    if (!log.RedeemContentIds.Contains(record.Id + ""))
                                    {
                                        log.RedeemContentIds += record.Id + "*";
                                    }

                                    this._businessObjects.Context.SaveChanges();

                                    success = true;
                                    
                                }

                                break;
                        }
                    }
                }
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

        #region BROADCASTING

        private string GetDealTypeName(int dealType)
        {
            string typeName = "";

            switch (dealType)
            {
                case DealTypes.InStore:
                    typeName = Resources.Instore;
                    break;
                case DealTypes.Online:
                    typeName = Resources.Online;
                    break;
                case DealTypes.Phone:
                    typeName = Resources.PhoneCall;
                    break;
            }

            return typeName;
        }

        private string GetExtraBonusAdditionTypeName(int extraBonusAdditionType)
        {
            string extraBonusAdditionTypeName = "";

            switch (extraBonusAdditionType)
            {
                case ExtraBonusAdditionTypes.None:
                    extraBonusAdditionTypeName = Resources.None;
                    break;
                case ExtraBonusAdditionTypes.Wallet:
                    extraBonusAdditionTypeName = Resources.YOYWallet;
                    break;
                case ExtraBonusAdditionTypes.Club:
                    extraBonusAdditionTypeName = Resources.Club;
                    break;
            }

            return extraBonusAdditionTypeName;
        }

        private string GetExtraBonusTypeName(int extraBonusType)
        {
            string extraBonusTypeName = "";

            switch (extraBonusType)
            {
                case ExtraBonusTypes.None:
                    extraBonusTypeName = Resources.None;
                    break;
                case ExtraBonusTypes.Percentage:
                    extraBonusTypeName = Resources.Percentage;
                    break;
                case ExtraBonusTypes.FixedAmount:
                    extraBonusTypeName = Resources.FixedAmount;
                    break;
            }

            return extraBonusTypeName;
        }

        private void BuildFullContentList(ref List<BroadcastingLogContentFullData> contentsData, ref List<BroadcastingLogDataWithBranches> enabledContents, bool includeBranchList)
        {
            BroadcastingLogDataWithBranches currentContent;
            List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
            List<BasicBranchData> availableBranches;
            IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
            BasicBranchData nearestBranch;
            int? branchGroupsCount;

            if (!includeBranchList)
            {
                bool locationMatch = false;

                for (int i = 0; i < enabledContents.Count; i++)
                {
                    currentContent = enabledContents[i];
                    locationMatch = false;

                    //This means it's enabled for the complete country, then it's enabled for all the potential branches
                    if (currentContent.Offer.GeoSegmentationType != GeoSegmentationTypes.Country)
                    {
                        enabledLocations = this._businessObjects.ContentLocations.Gets(enabledContents[i].Offer.Id);
                        availableBranches = currentContent.Branches.GroupBy(x => x.Id)
                                                                           .Select(grp => grp.First())
                                                                           .ToList();

                        if (enabledLocations?.Count > 0)
                        {

                            //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                            switch (currentContent.Offer.GeoSegmentationType)
                            {
                                case GeoSegmentationTypes.State:
                                    //Will group by state
                                    branchesGrouped = currentContent.Branches.GroupBy(x => x.StateId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    locationMatch = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        locationMatch = false;
                                    }

                                    break;
                                case GeoSegmentationTypes.City:
                                    //Will group by state
                                    branchesGrouped = currentContent.Branches.GroupBy(x => x.CityId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    locationMatch = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        locationMatch = false;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            //If no enabled locations needs to be removed
                            locationMatch = false;
                        }
                    }
                    else
                    {
                        //If the offer is enabled to be avaiable to the complete country, then it's valid
                        locationMatch = true;
                    }

                    if (locationMatch)
                    {

                        contentsData.Add(new BroadcastingLogContentFullData
                        {
                            BroadcastingMinsToRedeem = currentContent.BroadcastingMinsToRedeem,
                            BroadcastingTimerType = currentContent.BroadcastingTimerType,
                            LogRecordReferenceId = currentContent.LogRecordReferenceId,
                            LogRecordReferenceType = currentContent.LogRecordReferenceType,
                            LogRecordExpirationDate = currentContent.LogRecordExpirationDate,
                            Offer = currentContent.Offer,
                            Tenant = currentContent.Tenant,
                            Branches = new List<BasicBranchData>()
                        }
                        );
                    }

                }
            }
            else
            {
                List<BasicBranchData> enabledBranches = null;

                for (int i = 0; i < enabledContents.Count; i++)
                {
                    currentContent = enabledContents[i];

                    enabledLocations = this._businessObjects.ContentLocations.Gets(enabledContents[i].Offer.Id);
                    availableBranches = currentContent.Branches.GroupBy(x => x.Id)
                                                                    .Select(grp => grp.First())
                                                                    .ToList();
                    enabledBranches = new List<BasicBranchData>();

                    //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                    switch (currentContent.Offer.GeoSegmentationType)
                    {
                        case GeoSegmentationTypes.State:
                            //Will group by state
                            branchesGrouped = currentContent.Branches.GroupBy(x => x.StateId);
                            branchGroupsCount = branchesGrouped?.Count();


                            if (branchGroupsCount != null && branchGroupsCount > 0)
                            {
                                for (int j = 0; j < enabledLocations.Count; j++)
                                {
                                    for (int k = 0; k < branchGroupsCount; k++)
                                    {
                                        //If the location has the same geosegmentation and has the same location reference Id
                                        if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                        {
                                            enabledBranches.AddRange(availableBranches.Where(x => x.StateId == enabledLocations[j].Value).ToList());
                                        }
                                    }
                                }
                            }

                            break;
                        case GeoSegmentationTypes.City:
                            //Will group by state
                            branchesGrouped = currentContent.Branches.GroupBy(x => x.CityId);
                            branchGroupsCount = branchesGrouped?.Count();


                            if (branchGroupsCount != null && branchGroupsCount > 0)
                            {
                                for (int j = 0; j < enabledLocations.Count; j++)
                                {
                                    for (int k = 0; k < branchGroupsCount; k++)
                                    {
                                        //If the location has the same geosegmentation and has the same location reference Id
                                        if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                        {
                                            enabledBranches.AddRange(availableBranches.Where(x => x.CityId == enabledLocations[j].Value).ToList());
                                        }
                                    }
                                }
                            }

                            break;
                        case GeoSegmentationTypes.Country:
                            //This means the offer is enabled to all the existing branches
                            enabledBranches = availableBranches;

                            break;
                    }

                    if (enabledBranches?.Count > 0)
                    {
                        enabledBranches = enabledBranches.OrderBy(x => x.Distance).ToList();

                        nearestBranch = enabledBranches.ElementAt(0);

                        if (nearestBranch != null)
                        {
                            currentContent.Tenant.NearestBranchId = nearestBranch.Id;
                            currentContent.Tenant.NearestBranchName = nearestBranch.Name;
                            currentContent.Tenant.NearestBranchLatitude = nearestBranch.Latitude;
                            currentContent.Tenant.NearestBranchLongitude = nearestBranch.Longitude;
                        }

                        contentsData.Add(new BroadcastingLogContentFullData
                        {
                            BroadcastingMinsToRedeem = currentContent.BroadcastingMinsToRedeem,
                            BroadcastingTimerType = currentContent.BroadcastingTimerType,
                            LogRecordReferenceId = currentContent.LogRecordReferenceId,
                            LogRecordReferenceType = currentContent.LogRecordReferenceType,
                            LogRecordExpirationDate = currentContent.LogRecordExpirationDate,
                            Offer = currentContent.Offer,
                            Tenant = currentContent.Tenant,
                            Branches = enabledBranches
                        }
                        );
                    }

                }
            }
        }

        private void BuildFullContentList(ref List<BroadcastingLogContentFullData> contentsData, ref List<BroadcastingLogDataWithBranches> enabledContents, bool includeBranchList, bool includeNearestBranch)
        {
            BroadcastingLogDataWithBranches currentContent;
            List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
            List<BasicBranchData> availableBranches;
            IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
            BasicBranchData nearestBranch;
            int? branchGroupsCount;

            List<BasicBranchData> enabledBranches = null;

            for (int i = 0; i < enabledContents.Count; i++)
            {
                currentContent = enabledContents[i];

                if (currentContent.Offer.GeoSegmentationType != GeoSegmentationTypes.Country)
                {
                    enabledLocations = this._businessObjects.ContentLocations.Gets(enabledContents[i].Offer.Id);
                }
                else
                {
                    enabledLocations = new List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>>();
                }

                availableBranches = currentContent.Branches.GroupBy(x => x.Id)
                                                                       .Select(grp => grp.First())
                                                                       .ToList();
                enabledBranches = new List<BasicBranchData>();

                if (currentContent.Offer.DealType == DealTypes.InStore || currentContent.Offer.DealType == DealTypes.Catalog)
                {
                    //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                    switch (currentContent.Offer.GeoSegmentationType)
                    {
                        case GeoSegmentationTypes.State:
                            //Will group by state
                            branchesGrouped = currentContent.Branches.GroupBy(x => x.StateId);
                            branchGroupsCount = branchesGrouped?.Count();


                            if (branchGroupsCount != null && branchGroupsCount > 0)
                            {
                                for (int j = 0; j < enabledLocations.Count; j++)
                                {
                                    for (int k = 0; k < branchGroupsCount; k++)
                                    {
                                        //If the location has the same geosegmentation and has the same location reference Id
                                        if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                        {
                                            enabledBranches.AddRange(availableBranches.Where(x => x.StateId == enabledLocations[j].Value).ToList());
                                        }
                                    }
                                }
                            }

                            break;
                        case GeoSegmentationTypes.City:
                            //Will group by state
                            branchesGrouped = currentContent.Branches.GroupBy(x => x.CityId);
                            branchGroupsCount = branchesGrouped?.Count();


                            if (branchGroupsCount != null && branchGroupsCount > 0)
                            {
                                for (int j = 0; j < enabledLocations.Count; j++)
                                {
                                    for (int k = 0; k < branchGroupsCount; k++)
                                    {
                                        //If the location has the same geosegmentation and has the same location reference Id
                                        if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                        {
                                            enabledBranches.AddRange(availableBranches.Where(x => x.CityId == enabledLocations[j].Value).ToList());
                                        }
                                    }
                                }
                            }

                            break;
                        case GeoSegmentationTypes.Country:
                            //This means the offer is available to all the branches in the country
                            enabledBranches = availableBranches;

                            break;
                    }
                }


                if (enabledBranches?.Count > 0)
                {

                    enabledBranches = enabledBranches.OrderBy(x => x.Distance).ToList();

                    nearestBranch = enabledBranches.ElementAt(0);

                    if (includeNearestBranch && nearestBranch != null)
                    {
                        currentContent.Tenant.NearestBranchId = nearestBranch.Id;
                        currentContent.Tenant.NearestBranchName = nearestBranch.Name;
                        currentContent.Tenant.NearestBranchLatitude = nearestBranch.Latitude;
                        currentContent.Tenant.NearestBranchLongitude = nearestBranch.Longitude;
                        currentContent.Tenant.NearesBranchDistance = nearestBranch.Distance;
                    }

                    if (includeBranchList)
                    {
                        contentsData.Add(new BroadcastingLogContentFullData
                        {
                            BroadcastingMinsToRedeem = currentContent.BroadcastingMinsToRedeem,
                            BroadcastingTimerType = currentContent.BroadcastingTimerType,
                            LogRecordReferenceId = currentContent.LogRecordReferenceId,
                            LogRecordReferenceType = currentContent.LogRecordReferenceType,
                            LogRecordExpirationDate = currentContent.LogRecordExpirationDate,
                            Offer = currentContent.Offer,
                            Tenant = currentContent.Tenant,
                            Branches = enabledBranches
                        }
                        );
                    }
                    else
                    {
                        contentsData.Add(new BroadcastingLogContentFullData
                        {
                            BroadcastingMinsToRedeem = currentContent.BroadcastingMinsToRedeem,
                            BroadcastingTimerType = currentContent.BroadcastingTimerType,
                            LogRecordReferenceId = currentContent.LogRecordReferenceId,
                            LogRecordReferenceType = currentContent.LogRecordReferenceType,
                            LogRecordExpirationDate = currentContent.LogRecordExpirationDate,
                            Offer = currentContent.Offer,
                            Tenant = currentContent.Tenant,
                            Branches = new List<BasicBranchData>()
                        }
                        );
                    }

                }

            }
        }

        private List<FlattenedBroadcastingLogData> GetBroadcastingOffersDataWithLocation(string userId, decimal latitude, decimal longitude, double radius, DateTime dateTime)
        {
            List<FlattenedBroadcastingLogData> broadcastingLogOffers = null;

            try
            {
                var query = this._businessObjects.FuncsHandler.GetBroadcastingOfferLogsWithLocation(latitude, longitude, radius, userId, dateTime);

                if (query != null)
                {
                    FlattenedBroadcastingLogData broadcastingLogData = null;
                    broadcastingLogOffers = new List<FlattenedBroadcastingLogData>();

                    foreach (TempbroadcastingOffersLogs item in query)
                    {
                        broadcastingLogData = new FlattenedBroadcastingLogData
                        {
                            Id = item.Id,
                            BroadcasterId = item.BroadcasterId,
                            BroadcasterType = item.BroadcasterType,
                            UserId = item.UserId,
                            ContainedContentCount = item.ContainedContentCount,
                            MsgTitle = item.MsgTitle,
                            MsgContent = item.MsgContent,
                            SentDate = item.SentDate,
                            ExpirationDate = item.ExpirationDate,
                            LogRecordReferenceId = item.LogRecordReferenceId,
                            LogRecordReferenceType = item.LogRecordReferenceType,
                            LogRecordExpirationDate = item.LogRecordExpirationDate,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            Offer = new Offer
                            {
                                Id = item.Id,
                                TenantId = item.TenantId,
                                MainCategoryId = item.MainCategoryId,
                                MainCategoryName = "",//Not needed
                                OfferType = item.OfferType,
                                OfferTypeName = "",//Not needed
                                DealType = item.DealType,
                                DealTypeName = "",//Not needed
                                RewardType = item.RewardType,
                                RewardTypeName = "",//Not needed
                                PurposeType = item.PurposeType,
                                PurposeTypeName = "",//Not needed
                                GeoSegmentationType = item.GeoSegmentationType,
                                GeoSegmentationTypeName = "",//Not needed
                                DisplayType = item.DisplayType,
                                DisplayTypeName = "",//Not needed
                                Name = item.Name,
                                MainHint = item.MainHint,
                                ComplementaryHint = item.ComplementaryHint,
                                Keywords = item.Keywords,
                                Description = item.Description,
                                Code = "",//Not needed
                                CodeImg = null,//Not needed
                                MinsToUnlock = item.MinsToUnlock,
                                IsActive = item.IsActive,
                                IsExclusive = item.IsExclusive,
                                IsSponsored = item.IsSponsored,
                                HasUniqueCodes = false,//Not needed
                                HasPreferences = item.HasPreferences,
                                AvailableQuantity = item.AvailableQuantity,
                                OneTimeRedemption = item.OneTimeRedemption,
                                MaxClaimsPerUser = item.MaxClaimsPerUser,
                                MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                PurchasesCountStartDate = item.PurchasesCountStartDate,
                                ClaimLocation = item.ClaimLocation,
                                Value = item.Value,
                                RegularValue = item.RegularValue,
                                ExtraBonus = item.ExtraBonus,
                                ExtraBonusType = item.ExtraBonusType,
                                ExtraBonusTypeName = "",//Not needed
                                MinIncentive = -1,//Not needed
                                MaxIncentive = -1,//Not needed
                                IncentiveVariationType = -1,//Not needed
                                IncentiveVarationTypeName = "",//Not needed
                                IncentiveVariation = -1,//Not needed
                                SecondsIncentiveVariationFrame = -1,//Not needed
                                RedeemCount = item.RedeemCount,
                                ClaimCount = item.ClaimCount,
                                ReleaseDate = item.ReleaseDate,
                                ExpirationDate = item.ExpirationDate,
                                TargettingParams = item.TargettingParams,
                                DisplayImgId = item.DisplayImageId,
                                Rules = item.Rules ?? Resources.NoRulesAvailable,
                                Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                LastBroadcastingUsage = null,//Not needed
                                BroadcastingTimerType = -1,//Not needed
                                BroadcastingTimerTypeName = "",//Not needed
                                BroadcastingScheduleType = -1,//Not needed
                                BroadcastingScheduleTypeName = "",//Not needed
                                BroadcastingMinsToRedeem = -1,//Not needed
                                BroadcastingTitle = "",//Not needed
                                BroadcastingMsg = "",//Not needed
                                RelevanceRate = item.RelevanceRate,
                                CreatedDate = item.CreatedDate,
                                UpdatedDate = DateTime.UtcNow,//Not needed
                                SatisfactionScore = item.SatisfactionScore,
                                RelevanceScore = item.RelevanceScore
                            },
                            Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                            {
                                Id = item.TenantId,
                                Name = item.TenantName,
                                Logo = item.TenantLogo,
                                CountryId = item.TenantCountryId,
                                CurrencySymbol = item.CurrencySymbol,
                                Type = item.TenantType,
                                CategoryId = item.TenantCategoryId,
                                CategoryName = "",
                                RelevanceScore = null,//When its selector is category, there is no info about tenant relevance
                                NearestBranchId = Guid.Empty,
                                NearestBranchName = "",
                                NearestBranchLatitude = null,
                                NearestBranchLongitude = null,
                            },
                            Branch = new BasicBranchData
                            {
                                Id = item.BranchId,
                                Name = item.BranchName,
                                DescriptiveAddress = item.BranchDescriptiveAddress,
                                InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                Latitude = item.BranchLatitude,
                                Longitude = item.BranchLongitude,
                                Distance = Math.Round(((double)item.Distance / 1000), 2, MidpointRounding.AwayFromZero),//Is originally in meters, it's passed to kilometers
                                CityId = item.BranchCityId,
                                StateId = item.BranchStateId,
                                Enabled = false
                            },
                            Preference = new DTO.Entities.Misc.InterestPreference.BasicUserPreferenceData
                            {
                                Id = Guid.Empty,
                                Name = "",
                                Icon = "",
                                RelevanceScore = -1
                            },
                            ExactLocationBased = true
                        };
                    }
                }

            }
            catch (Exception e)
            {
                broadcastingLogOffers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return broadcastingLogOffers;
        }

        private List<FlattenedBroadcastingLogData> GetBroadcastingOffers(string userId, DateTime dateTime)
        {
            List<FlattenedBroadcastingLogData> broadcastingLogOffers = null;

            try
            {
                var query = this._businessObjects.FuncsHandler.GetBroadcastingOfferLogs(userId, dateTime);

                if (query != null)
                {
                    FlattenedBroadcastingLogData broadcastingLogData = null;
                    broadcastingLogOffers = new List<FlattenedBroadcastingLogData>();

                    foreach (TempbroadcastingOffersLogs item in query)
                    {
                        broadcastingLogData = new FlattenedBroadcastingLogData
                        {
                            Id = item.Id,
                            BroadcasterId = item.BroadcasterId,
                            BroadcasterType = item.BroadcasterType,
                            UserId = item.UserId,
                            ContainedContentCount = item.ContainedContentCount,
                            MsgTitle = item.MsgTitle,
                            MsgContent = item.MsgContent,
                            SentDate = item.SentDate,
                            ExpirationDate = item.ExpirationDate,
                            LogRecordReferenceId = item.LogRecordReferenceId,
                            LogRecordReferenceType = item.LogRecordReferenceType,
                            LogRecordExpirationDate = item.LogRecordExpirationDate,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            Offer = new Offer
                            {
                                Id = item.Id,
                                TenantId = item.TenantId,
                                MainCategoryId = item.MainCategoryId,
                                MainCategoryName = "",//Not needed
                                OfferType = item.OfferType,
                                OfferTypeName = "",//Not needed
                                DealType = item.DealType,
                                DealTypeName = "",//Not needed
                                RewardType = item.RewardType,
                                RewardTypeName = "",//Not needed
                                PurposeType = item.PurposeType,
                                PurposeTypeName = "",//Not needed
                                GeoSegmentationType = item.GeoSegmentationType,
                                GeoSegmentationTypeName = "",//Not needed
                                DisplayType = item.DisplayType,
                                DisplayTypeName = "",//Not needed
                                Name = item.Name,
                                MainHint = item.MainHint,
                                ComplementaryHint = item.ComplementaryHint,
                                Keywords = item.Keywords,
                                Description = item.Description,
                                Code = "",//Not needed
                                CodeImg = null,//Not needed
                                MinsToUnlock = item.MinsToUnlock,
                                IsActive = item.IsActive,
                                IsExclusive = item.IsExclusive,
                                IsSponsored = item.IsSponsored,
                                HasUniqueCodes = false,//Not needed
                                HasPreferences = item.HasPreferences,
                                AvailableQuantity = item.AvailableQuantity,
                                OneTimeRedemption = item.OneTimeRedemption,
                                MaxClaimsPerUser = item.MaxClaimsPerUser,
                                MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                PurchasesCountStartDate = item.PurchasesCountStartDate,
                                ClaimLocation = item.ClaimLocation,
                                Value = item.Value,
                                RegularValue = item.RegularValue,
                                ExtraBonus = item.ExtraBonus,
                                ExtraBonusType = item.ExtraBonusType,
                                ExtraBonusTypeName = "",//Not needed
                                MinIncentive = -1,//Not needed
                                MaxIncentive = -1,//Not needed
                                IncentiveVariationType = -1,//Not needed
                                IncentiveVarationTypeName = "",//Not needed
                                IncentiveVariation = -1,//Not needed
                                SecondsIncentiveVariationFrame = -1,//Not needed
                                RedeemCount = item.RedeemCount,
                                ClaimCount = item.ClaimCount,
                                ReleaseDate = item.ReleaseDate,
                                ExpirationDate = item.ExpirationDate,
                                TargettingParams = item.TargettingParams,
                                DisplayImgId = item.DisplayImageId,
                                Rules = item.Rules ?? Resources.NoRulesAvailable,
                                Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                LastBroadcastingUsage = null,//Not needed
                                BroadcastingTimerType = -1,//Not needed
                                BroadcastingTimerTypeName = "",//Not needed
                                BroadcastingScheduleType = -1,//Not needed
                                BroadcastingScheduleTypeName = "",//Not needed
                                BroadcastingMinsToRedeem = -1,//Not needed
                                BroadcastingTitle = "",//Not needed
                                BroadcastingMsg = "",//Not needed
                                RelevanceRate = item.RelevanceRate,
                                CreatedDate = item.CreatedDate,
                                UpdatedDate = DateTime.UtcNow,//Not needed
                                SatisfactionScore = item.SatisfactionScore,
                                RelevanceScore = item.RelevanceScore
                            },
                            Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                            {
                                Id = item.TenantId,
                                Name = item.TenantName,
                                Logo = item.TenantLogo,
                                CountryId = item.TenantCountryId,
                                CurrencySymbol = item.CurrencySymbol,
                                CategoryId = item.TenantCategoryId,
                                CategoryName = "",
                                Type = item.TenantType,
                                RelevanceScore = null,
                                NearestBranchId = Guid.Empty,
                                NearestBranchName = "",
                                NearestBranchLatitude = null,
                                NearestBranchLongitude = null,
                            },
                            Branch = new BasicBranchData
                            {
                                Id = item.BranchId,
                                Name = item.BranchName,
                                DescriptiveAddress = item.BranchDescriptiveAddress,
                                InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                Latitude = item.BranchLatitude,
                                Longitude = item.BranchLongitude,
                                Distance = null,//In this case there is no way to define distance
                                CityId = item.BranchCityId,
                                StateId = item.BranchStateId,
                                Enabled = false
                            },
                            Preference = new DTO.Entities.Misc.InterestPreference.BasicUserPreferenceData
                            {
                                Id = Guid.Empty,
                                Name = "",
                                Icon = "",
                                RelevanceScore = -1
                            },
                            ExactLocationBased = false
                        };
                    }
                }

            }
            catch (Exception e)
            {
                broadcastingLogOffers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return broadcastingLogOffers;
        }

        public List<FullBroadcastingLogData> GetBroadcastingLogDataWithLocation(string userId, decimal latitude, decimal longitude, double radius, DateTime dateTime, bool includeBranchList, bool includeNearestBranch)
        {
            List<FullBroadcastingLogData> broadcastingLogsData = new List<FullBroadcastingLogData>();

            try
            {
                List<FlattenedBroadcastingLogData> flattenedBroadcastingOfferLogs = this.GetBroadcastingOffersDataWithLocation(userId, latitude, longitude, radius, dateTime);

                if (flattenedBroadcastingOfferLogs?.Count > 0)
                {
                    //1st will create groups per broadcasting log
                    IEnumerable<IGrouping<Guid, FlattenedBroadcastingLogData>> groupedByBroadcastingLogId = flattenedBroadcastingOfferLogs.GroupBy(x => x.Id);

                    FlattenedBroadcastingLogData[] broadcastingLogsGroup = null;
                    FullBroadcastingLogData currentFullBroadcastingLog;

                    List<FlattenedBroadcastingLogData> flattenedOffersByBroadcastingLogId;
                    IEnumerable<IGrouping<Guid, FlattenedBroadcastingLogData>> groupedByOfferId;
                    FlattenedBroadcastingLogData[] broadcastingOffersGroup = null;

                    BroadcastingLogDataWithBranches currentBroadcastingLogData;
                    List<BroadcastingLogDataWithBranches> enabledContents = new List<BroadcastingLogDataWithBranches>();

                    List<BroadcastingLogContentFullData> contentsData;

                    foreach (IGrouping<Guid, FlattenedBroadcastingLogData> broadcastingLogDataGroup in groupedByBroadcastingLogId)
                    {
                        broadcastingLogsGroup = broadcastingLogDataGroup.ToArray();

                        currentFullBroadcastingLog = new FullBroadcastingLogData
                        {
                            Id = broadcastingLogsGroup[0].Id,
                            BroadcasterId = broadcastingLogsGroup[0].BroadcasterId,
                            BroadcasterType = broadcastingLogsGroup[0].BroadcasterType,
                            UserId = broadcastingLogsGroup[0].UserId,
                            MsgTitle = broadcastingLogsGroup[0].MsgTitle,
                            MsgContent = broadcastingLogsGroup[0].MsgContent,
                            ContainedContentCount = 0,
                            ContentList = new List<BroadcastingLogContentFullData>(),
                            SentDate = broadcastingLogsGroup[0].SentDate,
                            ExpirationDate = broadcastingLogsGroup[0].ExpirationDate
                        };

                        //Now needs to build the content list
                        flattenedOffersByBroadcastingLogId = broadcastingLogsGroup.ToList();

                        if (flattenedOffersByBroadcastingLogId?.Count > 0)
                        {
                            groupedByOfferId = flattenedOffersByBroadcastingLogId.GroupBy(x => x.Offer.Id);

                            foreach (IGrouping<Guid, FlattenedBroadcastingLogData> offerDataGroup in groupedByOfferId)
                            {
                                broadcastingOffersGroup = offerDataGroup.ToArray();

                                currentBroadcastingLogData = new BroadcastingLogDataWithBranches
                                {
                                    Offer = broadcastingOffersGroup[0].Offer,
                                    Tenant = broadcastingOffersGroup[0].Tenant,
                                    Branches = new List<BasicBranchData>(),
                                    ExactLocationBased = broadcastingOffersGroup[0].ExactLocationBased,
                                    BroadcastingMinsToRedeem = broadcastingOffersGroup[0].BroadcastingMinsToRedeem,
                                    BroadcastingTimerType = broadcastingOffersGroup[0].BroadcastingTimerType,
                                    LogRecordReferenceId = broadcastingOffersGroup[0].LogRecordReferenceId,
                                    LogRecordReferenceType = broadcastingOffersGroup[0].LogRecordReferenceType,
                                    LogRecordExpirationDate = broadcastingOffersGroup[0].LogRecordExpirationDate
                                };

                                for (int i = 0; i < broadcastingOffersGroup.Length; ++i)
                                {
                                    currentBroadcastingLogData.Branches.Add(broadcastingOffersGroup[i].Branch);
                                }

                                enabledContents.Add(currentBroadcastingLogData);
                            }

                            //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                            //each offer can be actually enabled

                            contentsData = new List<BroadcastingLogContentFullData>();

                            this.BuildFullContentList(ref contentsData, ref enabledContents, includeBranchList, includeNearestBranch);

                            if (contentsData?.Count > 0)
                            {
                                currentFullBroadcastingLog.ContentList = contentsData;
                                currentFullBroadcastingLog.ContainedContentCount = contentsData.Count;
                            }
                        }

                        broadcastingLogsData.Add(currentFullBroadcastingLog);
                    }


                }

            }
            catch (Exception e)
            {
                broadcastingLogsData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return broadcastingLogsData;

        }


        public List<FullBroadcastingLogData> GetBroadcastingLogData(string userId, DateTime dateTime, bool includeBranchList)
        {
            List<FullBroadcastingLogData> broadcastingLogsData = new List<FullBroadcastingLogData>();

            try
            {
                List<FlattenedBroadcastingLogData> flattenedBroadcastingOfferLogs = this.GetBroadcastingOffers(userId, dateTime);

                if (flattenedBroadcastingOfferLogs?.Count > 0)
                {
                    //1st will create groups per broadcasting log
                    IEnumerable<IGrouping<Guid, FlattenedBroadcastingLogData>> groupedByBroadcastingLogId = flattenedBroadcastingOfferLogs.GroupBy(x => x.Id);

                    FlattenedBroadcastingLogData[] broadcastingLogsGroup = null;
                    FullBroadcastingLogData currentFullBroadcastingLog;

                    List<FlattenedBroadcastingLogData> flattenedOffersByBroadcastingLogId;
                    IEnumerable<IGrouping<Guid, FlattenedBroadcastingLogData>> groupedByOfferId;
                    FlattenedBroadcastingLogData[] broadcastingOffersGroup = null;

                    BroadcastingLogDataWithBranches currentBroadcastingLogData;
                    List<BroadcastingLogDataWithBranches> enabledContents = new List<BroadcastingLogDataWithBranches>();

                    List<BroadcastingLogContentFullData> contentsData;

                    foreach (IGrouping<Guid, FlattenedBroadcastingLogData> broadcastingLogDataGroup in groupedByBroadcastingLogId)
                    {
                        broadcastingLogsGroup = broadcastingLogDataGroup.ToArray();

                        currentFullBroadcastingLog = new FullBroadcastingLogData
                        {
                            Id = broadcastingLogsGroup[0].Id,
                            BroadcasterId = broadcastingLogsGroup[0].BroadcasterId,
                            BroadcasterType = broadcastingLogsGroup[0].BroadcasterType,
                            UserId = broadcastingLogsGroup[0].UserId,
                            MsgTitle = broadcastingLogsGroup[0].MsgTitle,
                            MsgContent = broadcastingLogsGroup[0].MsgContent,
                            ContainedContentCount = 0,
                            ContentList = new List<BroadcastingLogContentFullData>(),
                            SentDate = broadcastingLogsGroup[0].SentDate,
                            ExpirationDate = broadcastingLogsGroup[0].ExpirationDate
                        };

                        //Now needs to build the content list
                        flattenedOffersByBroadcastingLogId = broadcastingLogsGroup.ToList();

                        if (flattenedOffersByBroadcastingLogId?.Count > 0)
                        {
                            groupedByOfferId = flattenedOffersByBroadcastingLogId.GroupBy(x => x.Offer.Id);

                            foreach (IGrouping<Guid, FlattenedBroadcastingLogData> offerDataGroup in groupedByOfferId)
                            {
                                broadcastingOffersGroup = offerDataGroup.ToArray();

                                currentBroadcastingLogData = new BroadcastingLogDataWithBranches
                                {
                                    Offer = broadcastingOffersGroup[0].Offer,
                                    Tenant = broadcastingOffersGroup[0].Tenant,
                                    Branches = new List<BasicBranchData>(),
                                    ExactLocationBased = broadcastingOffersGroup[0].ExactLocationBased,
                                    BroadcastingMinsToRedeem = broadcastingOffersGroup[0].BroadcastingMinsToRedeem,
                                    BroadcastingTimerType = broadcastingOffersGroup[0].BroadcastingTimerType,
                                    LogRecordReferenceId = broadcastingOffersGroup[0].LogRecordReferenceId,
                                    LogRecordReferenceType = broadcastingOffersGroup[0].LogRecordReferenceType,
                                    LogRecordExpirationDate = broadcastingOffersGroup[0].LogRecordExpirationDate
                                };

                                for (int i = 0; i < broadcastingOffersGroup.Length; ++i)
                                {
                                    currentBroadcastingLogData.Branches.Add(broadcastingOffersGroup[i].Branch);
                                }

                                enabledContents.Add(currentBroadcastingLogData);
                            }

                            //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                            //each offer can be actually enabled

                            contentsData = new List<BroadcastingLogContentFullData>();

                            this.BuildFullContentList(ref contentsData, ref enabledContents, includeBranchList);

                            if (contentsData?.Count > 0)
                            {
                                currentFullBroadcastingLog.ContentList = contentsData;
                                currentFullBroadcastingLog.ContainedContentCount = contentsData.Count;
                            }
                        }

                        broadcastingLogsData.Add(currentFullBroadcastingLog);
                    }

                }

            }
            catch (Exception e)
            {
                broadcastingLogsData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return broadcastingLogsData;

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
        public BroadcastingLogManager(BusinessObjects businessObjects)
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

