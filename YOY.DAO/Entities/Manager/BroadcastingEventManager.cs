using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class BroadcastingEventManager
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

        private string GetBroadcasterTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case BroadcasterTypes.Geofence:
                    typeName = Resources.Geofence;
                    break;
                case BroadcasterTypes.Signal:
                    typeName = Resources.Signal;
                    break;
                case BroadcasterTypes.Audio:
                    typeName = Resources.Audio;
                    break;
                case BroadcasterTypes.Beacon:
                    typeName = Resources.Beacon;
                    break;
                case BroadcasterTypes.Image:
                    typeName = Resources.Image;
                    break;
            }
            return typeName;
        }

        private string GetEventTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case BroadcastingEventTypes.Detection:
                    typeName = Resources.Detection;
                    break;
                case BroadcastingEventTypes.Entering:
                    typeName = Resources.GeofenceEnter;
                    break;
                case BroadcastingEventTypes.Exiting:
                    typeName = Resources.GeofenceExit;
                    break;
                case BroadcastingEventTypes.Dwelling:
                    typeName = Resources.GeofenceDwell;
                    break;
            }
            return typeName;
        }

        private string GetConfidenceTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case BroadcastingEventConfidences.Low:
                    typeName = Resources.LowAccuracy;
                    break;
                case BroadcastingEventConfidences.Medium:
                    typeName = Resources.MediumAccuracy;
                    break;
                case BroadcastingEventConfidences.High:
                    typeName = Resources.HighAccuracy;
                    break;
            }
            return typeName;
        }

        /// <summary>
        /// Gets all the location events for a user based on eventType
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public List<BroadcastingEvent> Gets(string userId, int? broadcasterType, int? eventType, bool addBroadcasterName, int pageSize, int pageNumber)
        {
            List<BroadcastingEvent> events = new List<BroadcastingEvent>();

            try
            {
                var query = (dynamic)null;

                if (eventType != null)
                {
                    query = (from x in this._businessObjects.Context.OltpbroadcastingEvents
                             where x.UserId == userId && x.BroadcasterType == broadcasterType && x.EventType == eventType
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }
                else
                {
                    query = (from x in this._businessObjects.Context.OltpbroadcastingEvents
                             where x.UserId == userId
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }

                BroadcastingEvent broadcastingEvent = null;

                foreach (OltpbroadcastingEvents item in query)
                {
                    broadcastingEvent = new BroadcastingEvent
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterTypeName = GetBroadcasterTypeName(item.BroadcasterType),
                        BroadcasterId = item.BroadcasterId,
                        EventType = item.EventType,
                        EventTypeName = GetEventTypeName(item.EventType),
                        ConfidenceType = item.ConfidenceType,
                        ConfidenceTypeName = GetConfidenceTypeName(item.ConfidenceType),
                        Accuracy = item.Accuracy,
                        ContentDelivered = item.ContentDelivered,
                        BroadcastingLogId = item.BroadcastingLogId,
                        SequenceStart = item.SequenceStart,
                        SequenceEnd = item.SequenceEnd,
                        EventSequencePos = item.EventSequencePos,
                        PreviousEventId = item.PreviousEventId,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    if (addBroadcasterName)
                    {
                        var queryDetector = (dynamic)null;

                        switch (broadcastingEvent.BroadcasterType)
                        {
                            case BroadcasterTypes.Signal:
                            case BroadcasterTypes.Audio:
                            case BroadcasterTypes.Beacon:
                                queryDetector = from x in this._businessObjects.Context.Defbroadcasters
                                                where x.Id == broadcastingEvent.BroadcasterId
                                                select x;
                                break;
                            case BroadcasterTypes.Geofence:
                                queryDetector = from x in this._businessObjects.Context.Defgeofences
                                                where x.Id == broadcastingEvent.BroadcasterId
                                                select x;
                                break;
                        }

                        foreach (var itemDetector in queryDetector)
                        {
                            broadcastingEvent.BroadcasterName = itemDetector.Name;
                        }
                    }



                    events.Add(broadcastingEvent);
                }
            }
            catch (Exception e)
            {
                events = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return events;
        }

        /// <summary>
        /// Gets all the location events for a campaign
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public List<BroadcastingEvent> Gets(Guid broadcastingLogId, string userId, bool addBroadcasterName, int pageSize, int pageNumber)
        {
            List<BroadcastingEvent> events = new List<BroadcastingEvent>();

            try
            {
                var query = (dynamic)null;

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    query = (from x in this._businessObjects.Context.OltpbroadcastingEvents
                             where x.UserId == userId && x.BroadcastingLogId == broadcastingLogId
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }
                else
                {
                    query = (from x in this._businessObjects.Context.OltpbroadcastingEvents
                             where x.BroadcastingLogId == broadcastingLogId
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }

                BroadcastingEvent broadcastingEvent = null;

                foreach (OltpbroadcastingEvents item in query)
                {
                    broadcastingEvent = new BroadcastingEvent
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterTypeName = GetBroadcasterTypeName(item.BroadcasterType),
                        BroadcasterId = item.BroadcasterId,
                        EventType = item.EventType,
                        EventTypeName = GetEventTypeName(item.EventType),
                        ConfidenceType = item.ConfidenceType,
                        ConfidenceTypeName = GetConfidenceTypeName(item.ConfidenceType),
                        Accuracy = item.Accuracy,
                        ContentDelivered = item.ContentDelivered,
                        BroadcastingLogId = item.BroadcastingLogId,
                        SequenceStart = item.SequenceStart,
                        SequenceEnd = item.SequenceEnd,
                        EventSequencePos = item.EventSequencePos,
                        PreviousEventId = item.PreviousEventId,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    if (addBroadcasterName)
                    {
                        var queryDetector = (dynamic)null;

                        switch (broadcastingEvent.BroadcasterType)
                        {
                            case BroadcasterTypes.Signal:
                            case BroadcasterTypes.Audio:
                            case BroadcasterTypes.Beacon:
                                queryDetector = from x in this._businessObjects.Context.Defbroadcasters
                                                where x.Id == broadcastingEvent.BroadcasterId
                                                select x;
                                break;
                            case BroadcasterTypes.Geofence:
                                queryDetector = from x in this._businessObjects.Context.Defgeofences
                                                where x.Id == broadcastingEvent.BroadcasterId
                                                select x;
                                break;
                        }

                        foreach (var itemDetector in queryDetector)
                        {
                            broadcastingEvent.BroadcasterName = itemDetector.Name;
                        }
                    }


                    events.Add(broadcastingEvent);
                }
            }
            catch (Exception e)
            {
                events = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return events;
        }

        /// <summary>
        /// Retrieves all the location events for a detectorId for specific type, depending in eventType
        /// </summary>
        /// <param name="detertorId"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public List<BroadcastingEvent> Gets(Guid broadcasterId, int? eventType, bool addBroadcasterName, int pageSize, int pageNumber)
        {
            List<BroadcastingEvent> events = new List<BroadcastingEvent>();

            try
            {
                var query = (dynamic)null;

                if (eventType != null)
                {
                    query = (from x in this._businessObjects.Context.OltpbroadcastingEvents
                             where x.BroadcasterId == broadcasterId && x.EventType == eventType
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }
                else
                {
                    query = (from x in this._businessObjects.Context.OltpbroadcastingEvents
                             where x.BroadcasterId == broadcasterId
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }

                BroadcastingEvent broadcastingEvent = null;

                foreach (OltpbroadcastingEvents item in query)
                {
                    broadcastingEvent = new BroadcastingEvent
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterTypeName = GetBroadcasterTypeName(item.BroadcasterType),
                        BroadcasterId = item.BroadcasterId,
                        EventType = item.EventType,
                        EventTypeName = GetEventTypeName(item.EventType),
                        ConfidenceType = item.ConfidenceType,
                        ConfidenceTypeName = GetConfidenceTypeName(item.ConfidenceType),
                        Accuracy = item.Accuracy,
                        ContentDelivered = item.ContentDelivered,
                        BroadcastingLogId = item.BroadcastingLogId,
                        SequenceStart = item.SequenceStart,
                        SequenceEnd = item.SequenceEnd,
                        EventSequencePos = item.EventSequencePos,
                        PreviousEventId = item.PreviousEventId,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    if (addBroadcasterName)
                    {
                        var queryDetector = (dynamic)null;

                        switch (broadcastingEvent.BroadcasterType)
                        {
                            case BroadcasterTypes.Signal:
                            case BroadcasterTypes.Audio:
                            case BroadcasterTypes.Beacon:
                                queryDetector = from x in this._businessObjects.Context.Defbroadcasters
                                                where x.Id == broadcastingEvent.BroadcasterId
                                                select x;
                                break;
                            case BroadcasterTypes.Geofence:
                                queryDetector = from x in this._businessObjects.Context.Defgeofences
                                                where x.Id == broadcastingEvent.BroadcasterId
                                                select x;
                                break;
                        }

                        foreach (var itemDetector in queryDetector)
                        {
                            broadcastingEvent.BroadcasterName = itemDetector.Name;
                        }
                    }

                    events.Add(broadcastingEvent);
                }

            }
            catch (Exception e)
            {
                events = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return events;
        }

        /// <summary>
        /// Gets all the location events for a user and a detector based on eventType
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public List<BroadcastingEvent> Gets(string userId, Guid broadcasterId, int? eventType, bool addBroadcasterName, int pageSize, int pageNumber)
        {
            List<BroadcastingEvent> events = new List<BroadcastingEvent>();

            try
            {
                var query = (dynamic)null;

                if (eventType != null)
                {
                    query = (from x in this._businessObjects.Context.OltpbroadcastingEvents
                             where x.UserId == userId && x.BroadcasterId == broadcasterId && x.EventType == eventType
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }
                else
                {
                    query = (from x in this._businessObjects.Context.OltpbroadcastingEvents
                             where x.UserId == userId && x.BroadcasterId == broadcasterId
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }

                BroadcastingEvent broadcastingEvent = null;

                foreach (OltpbroadcastingEvents item in query)
                {
                    broadcastingEvent = new BroadcastingEvent
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterTypeName = GetBroadcasterTypeName(item.BroadcasterType),
                        BroadcasterId = item.BroadcasterId,
                        EventType = item.EventType,
                        EventTypeName = GetEventTypeName(item.EventType),
                        ConfidenceType = item.ConfidenceType,
                        ConfidenceTypeName = GetConfidenceTypeName(item.ConfidenceType),
                        Accuracy = item.Accuracy,
                        ContentDelivered = item.ContentDelivered,
                        BroadcastingLogId = item.BroadcastingLogId,
                        SequenceStart = item.SequenceStart,
                        SequenceEnd = item.SequenceEnd,
                        EventSequencePos = item.EventSequencePos,
                        PreviousEventId = item.PreviousEventId,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    if (addBroadcasterName)
                    {
                        var queryDetector = (dynamic)null;

                        switch (broadcastingEvent.BroadcasterType)
                        {
                            case BroadcasterTypes.Signal:
                            case BroadcasterTypes.Audio:
                            case BroadcasterTypes.Beacon:
                                queryDetector = from x in this._businessObjects.Context.Defbroadcasters
                                                where x.Id == broadcastingEvent.BroadcasterId
                                                select x;
                                break;
                            case BroadcasterTypes.Geofence:
                                queryDetector = from x in this._businessObjects.Context.Defgeofences
                                                where x.Id == broadcastingEvent.BroadcasterId
                                                select x;
                                break;
                        }

                        foreach (var itemDetector in queryDetector)
                        {
                            broadcastingEvent.BroadcasterName = itemDetector.Name;
                        }
                    }


                    events.Add(broadcastingEvent);
                }
            }
            catch (Exception e)
            {
                events = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return events;
        }

        public BroadcastingEvent Get(Guid id)
        {
            BroadcastingEvent broadcastingEvent = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbroadcastingEvents
                            where x.Id == id
                            select x;


                foreach (OltpbroadcastingEvents item in query)
                {
                    broadcastingEvent = new BroadcastingEvent
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        BroadcasterType = item.BroadcasterType,
                        BroadcasterTypeName = GetBroadcasterTypeName(item.BroadcasterType),
                        BroadcasterId = item.BroadcasterId,
                        EventType = item.EventType,
                        EventTypeName = GetEventTypeName(item.EventType),
                        ConfidenceType = item.ConfidenceType,
                        ConfidenceTypeName = GetConfidenceTypeName(item.ConfidenceType),
                        Accuracy = item.Accuracy,
                        ContentDelivered = item.ContentDelivered,
                        BroadcastingLogId = item.BroadcastingLogId,
                        SequenceStart = item.SequenceStart,
                        SequenceEnd = item.SequenceEnd,
                        EventSequencePos = item.EventSequencePos,
                        PreviousEventId = item.PreviousEventId,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    var queryDetector = (dynamic)null;

                    switch (broadcastingEvent.BroadcasterType)
                    {
                        case BroadcasterTypes.Signal:
                        case BroadcasterTypes.Audio:
                        case BroadcasterTypes.Beacon:
                            queryDetector = from x in this._businessObjects.Context.Defbroadcasters
                                            where x.Id == broadcastingEvent.BroadcasterId
                                            select x;
                            break;
                        case BroadcasterTypes.Geofence:
                            queryDetector = from x in this._businessObjects.Context.Defgeofences
                                            where x.Id == broadcastingEvent.BroadcasterId
                                            select x;
                            break;
                    }

                    foreach (var itemDetector in queryDetector)
                    {
                        broadcastingEvent.BroadcasterName = itemDetector.Name;
                    }


                }
            }
            catch (Exception e)
            {
                broadcastingEvent = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return broadcastingEvent;
        }

        public BroadcastingEvent Post(string userId, int broadcasterType, Guid broadcasterId, int eventType, int confidenceType, string accuracy, bool contentDelivered, Guid? broadcastingLogId, decimal? latitude, decimal? longitude, DateTime createdDate)
        {
            BroadcastingEvent locationEvent = null;

            try
            {
                OltpbroadcastingEvents newEvent = new OltpbroadcastingEvents
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    BroadcasterType = broadcasterType,
                    BroadcasterId = broadcasterId,
                    EventType = eventType,
                    ConfidenceType = confidenceType,
                    Accuracy = accuracy,
                    ContentDelivered = contentDelivered,
                    BroadcastingLogId = broadcastingLogId,
                    Latitude = latitude,
                    Longitude = longitude,
                    CreatedDate = createdDate,
                    UpdatedDate = DateTime.UtcNow
                };

                //Gets all the events for user with that detectorId
                OltpbroadcastingEvents currentEvent = (from x in this._businessObjects.Context.OltpbroadcastingEvents
                                                      where x.UserId == userId && x.BroadcasterId == broadcasterId
                                                      orderby x.CreatedDate descending
                                                      select x).FirstOrDefault();


                //If there an event for user and that detectorId
                if (currentEvent != null)
                {

                    switch (currentEvent.BroadcasterType)
                    {
                        case BroadcasterTypes.Signal:
                        case BroadcasterTypes.Audio:
                        case BroadcasterTypes.Beacon:
                            //If the last time different between both is more than 10min
                            if ((createdDate - currentEvent.CreatedDate).TotalMinutes > 10)
                            {
                                newEvent.PreviousEventId = null;
                                newEvent.EventSequencePos = 1;
                                newEvent.SequenceStart = true;
                                newEvent.SequenceEnd = false;
                            }
                            else
                            {
                                newEvent.PreviousEventId = currentEvent.Id;
                                newEvent.EventSequencePos = currentEvent.EventSequencePos + 1;
                                newEvent.SequenceStart = false;
                                newEvent.SequenceEnd = false;
                            }
                            break;
                        case BroadcasterTypes.Geofence:
                            //If the time difference is so big it most be a separated event
                            if ((createdDate - currentEvent.CreatedDate).TotalHours > 6)
                            {
                                newEvent.PreviousEventId = null;
                                newEvent.EventSequencePos = 1;
                                newEvent.SequenceStart = true;
                                newEvent.SequenceEnd = false;
                            }
                            else
                            {
                                //If it's the same event type, for example entering
                                if (currentEvent.EventType == eventType)
                                {
                                    newEvent.PreviousEventId = null;
                                    newEvent.EventSequencePos = 1;
                                    newEvent.SequenceStart = true;
                                    newEvent.SequenceEnd = false;
                                }
                                else
                                {
                                    //If the last event for this geofence for the user was exit, this new event is a complete new event
                                    if (currentEvent.EventType == BroadcastingEventTypes.Exiting)
                                    {
                                        newEvent.PreviousEventId = null;
                                        newEvent.EventSequencePos = 1;
                                        newEvent.SequenceStart = true;
                                        newEvent.SequenceEnd = false;
                                    }
                                    else
                                    {
                                        newEvent.PreviousEventId = currentEvent.Id;
                                        newEvent.EventSequencePos = currentEvent.EventSequencePos + 1;
                                        newEvent.SequenceStart = false;
                                        newEvent.SequenceEnd = false;

                                        //If the geofencing event is exit
                                        if (eventType == BroadcastingEventTypes.Exiting)
                                        {
                                            newEvent.SequenceEnd = true;
                                        }
                                    }

                                }
                            }

                            break;
                    }

                }
                else//if the 1st event for that tuple
                {
                    newEvent.PreviousEventId = null;
                    newEvent.EventSequencePos = 1;
                    newEvent.SequenceStart = true;
                    newEvent.SequenceEnd = false;
                }


                this._businessObjects.Context.OltpbroadcastingEvents.Add(newEvent);
                this._businessObjects.Context.SaveChanges();

                locationEvent = new BroadcastingEvent
                {
                    Id = newEvent.Id,
                    UserId = newEvent.UserId,
                    BroadcasterType = newEvent.BroadcasterType,
                    BroadcasterTypeName = GetBroadcasterTypeName(newEvent.BroadcasterType),
                    BroadcasterId = newEvent.BroadcasterId,
                    EventType = newEvent.EventType,
                    EventTypeName = GetEventTypeName(newEvent.EventType),
                    ConfidenceType = newEvent.ConfidenceType,
                    ConfidenceTypeName = GetConfidenceTypeName(newEvent.ConfidenceType),
                    Accuracy = newEvent.Accuracy,
                    ContentDelivered = newEvent.ContentDelivered,
                    BroadcastingLogId = newEvent.BroadcastingLogId,
                    SequenceStart = newEvent.SequenceStart,
                    SequenceEnd = newEvent.SequenceEnd,
                    EventSequencePos = newEvent.EventSequencePos,
                    PreviousEventId = newEvent.PreviousEventId,
                    Latitude = newEvent.Latitude,
                    Longitude = newEvent.Longitude,
                    CreatedDate = newEvent.CreatedDate,
                    UpdatedDate = newEvent.UpdatedDate
                };

            }
            catch (Exception e)
            {
                locationEvent = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return locationEvent;
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
        public BroadcastingEventManager(BusinessObjects businessObjects)
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
