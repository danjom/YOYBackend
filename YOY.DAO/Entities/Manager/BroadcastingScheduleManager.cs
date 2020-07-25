using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.DAO.Entities.DB;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class BroadcastingScheduleManager
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

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS METHODS                                                                                                                                  //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        #region METHODS

        /// <summary>
        /// Retrieves all the content broadcasting's schedules
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public List<BroadcastingSchedule> Gets(Guid contentId, int contentType)
        {
            List<BroadcastingSchedule> schedules = new List<BroadcastingSchedule>();

            try
            {
                var query = from x in this._businessObjects.Context.DefbroadcastingSchedules
                            where x.ContentId == contentId && x.ContentType == contentType
                            select x;

                BroadcastingSchedule currentSchedule;

                foreach (DefbroadcastingSchedules item in query)
                {
                    currentSchedule = new BroadcastingSchedule
                    {
                        Id = item.Id,
                        ContentId = item.ContentId,
                        ContentType = item.ContentType,
                        FromDay = item.FromDay,
                        ToDay = item.ToDay,
                        FromHour = item.FromHour,
                        ToHour = item.ToHour,
                        FromMinutes = item.FromMinute,
                        ToMinutes = item.ToMinute,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    schedules.Add(currentSchedule);
                }
            }
            catch (Exception e)
            {
                schedules = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return schedules;
        }// GETS ENDS ----------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Retrieve if there are broadcasting rules for a content in a given date
        /// if it was means the offer is enabled
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool Gets(Guid contentId, int contentType, DateTime dateTime)
        {
            bool isEnabled = false;

            int dayOfWeek = (int)dateTime.DayOfWeek;
            int hour = dateTime.Hour;
            int min = dateTime.Minute;

            try
            {
                var query = from x in this._businessObjects.Context.DefbroadcastingSchedules
                            where x.ContentId == contentId && x.ContentType == contentType && ((x.FromDay <= dayOfWeek && x.ToDay >= dayOfWeek) || (x.FromDay >= dayOfWeek && x.ToDay <= dayOfWeek))
                                    && ((x.FromHour <= hour && x.ToHour >= hour) || (x.FromHour >= hour && x.ToHour <= hour))
                                    && ((x.FromMinute <= min && x.ToMinute >= min) || (x.FromMinute >= min && x.ToMinute <= min))
                            select x;

                //If returned any element means the campaign has broadcasting rules for parametered date
                isEnabled = query.Any();

            }
            catch (Exception e)
            {
                isEnabled = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return isEnabled;
        }// GETS ENDS ----------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Retrieve all the schedules for a content broadcasting
        /// on a days interval
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="fromDay"></param>
        /// <param name="toDay"></param>
        /// <returns></returns>
        public List<BroadcastingSchedule> Gets(Guid contentId, int contentType, int fromDay, int toDay)
        {
            List<BroadcastingSchedule> schedules = new List<BroadcastingSchedule>();

            try
            {
                var query = from x in this._businessObjects.Context.DefbroadcastingSchedules
                            where x.ContentId == contentId && x.ContentType == contentType && x.FromDay == fromDay && x.ToDay == toDay
                            select x;

                BroadcastingSchedule currentSchedule;

                foreach (var item in query)
                {
                    currentSchedule = new BroadcastingSchedule
                    {
                        Id = item.Id,
                        ContentId = item.ContentId,
                        ContentType = item.ContentType,
                        FromDay = item.FromDay,
                        ToDay = item.ToDay,
                        FromHour = item.FromHour,
                        FromMinutes = item.FromMinute,
                        ToHour = item.ToHour,
                        ToMinutes = item.ToMinute,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                    };

                    schedules.Add(currentSchedule);
                }
            }
            catch (Exception e)
            {
                schedules = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return schedules;
        }// GETS ENDS ----------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Retrieves a schedule
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public BroadcastingSchedule Get(Guid scheduleId)
        {
            BroadcastingSchedule schedule = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefbroadcastingSchedules
                            where x.Id == scheduleId
                            select x;

                foreach (var item in query)
                {
                    schedule = new BroadcastingSchedule
                    {
                        Id = item.Id,
                        ContentId = item.ContentId,
                        ContentType = item.ContentType,
                        FromDay = item.FromDay,
                        ToDay = item.ToDay,
                        FromHour = item.FromHour,
                        ToHour = item.ToHour,
                        FromMinutes = item.FromMinute,
                        ToMinutes = item.ToMinute,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                    };
                }
            }
            catch (Exception e)
            {
                schedule = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return schedule;
        }


        /// <summary>
        /// Creates a new schedule
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="fromHour"></param>
        /// <param name="fromMinutes"></param>
        /// <param name="toHour"></param>
        /// <param name="fromDay"></param>
        /// <param name="toDay"></param>
        /// <param name="toMinutes"></param>
        /// <returns></returns>
        public BroadcastingSchedule Post(Guid contentId, int contentType, int fromHour, int fromMinutes, int toHour, int fromDay, int toDay, int toMinutes)
        {
            BroadcastingSchedule newSchedule;

            DefbroadcastingSchedules schedule = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefbroadcastingSchedules
                            where
                                x.ContentId == contentId && x.ContentType == contentType &&
                                ((x.FromDay <= fromDay && x.ToDay >= fromDay) || (x.FromDay <= toDay && x.ToDay >= toDay) ||
                                    (fromDay <= x.FromDay && toDay >= x.FromDay) || (fromDay <= x.ToDay && toDay >= x.ToDay)) &&
                                    ((x.FromHour <= fromHour && x.ToHour >= fromHour || x.FromHour <= toHour && x.ToHour >= toHour) ||
                                    (fromHour <= x.FromHour && toHour >= x.FromHour || fromHour <= x.ToHour && toHour >= x.ToHour))
                            select x;

                if (!query.Any())
                {

                    schedule = new DefbroadcastingSchedules
                    {
                        Id = Guid.NewGuid(),
                        ContentId = contentId,
                        ContentType = contentType,
                        FromDay = fromDay,
                        ToDay = toDay,
                        FromHour = fromHour,
                        ToHour = toHour,
                        FromMinute = fromMinutes,
                        ToMinute = toMinutes,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };


                    this._businessObjects.Context.DefbroadcastingSchedules.Add(schedule);
                    this._businessObjects.Context.SaveChanges();

                    newSchedule = new BroadcastingSchedule
                    {
                        Id = schedule.Id,
                        ContentId = schedule.ContentId,
                        ContentType = schedule.ContentType,
                        FromDay = schedule.FromDay,
                        ToDay = schedule.ToDay,
                        FromHour = schedule.FromHour,
                        ToHour = schedule.ToHour,
                        CreatedDate = schedule.CreatedDate,
                        UpdatedDate = schedule.UpdatedDate
                    };
                }
                else
                {
                    throw new ArgumentException(Resources.ScheduleOverlaps);
                }

            }
            catch (Exception e)
            {
                this._businessObjects.Context.DefbroadcastingSchedules.Remove(schedule);
                this._businessObjects.Context.SaveChanges();

                newSchedule = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }


            return newSchedule;
        }//END POST ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Updates a schedule
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="fromHour"></param>
        /// <param name="toHour"></param>
        /// <param name="fromDay"></param>
        /// <param name="toDay"></param>
        /// <returns></returns>
        public BroadcastingSchedule Put(Guid scheduleId, int fromHour, int toHour, int fromDay, int toDay, int fromMin, int toMin)
        {
            BroadcastingSchedule currentSchedule = null;

            try
            {
                var queryEv = from x in this._businessObjects.Context.DefbroadcastingSchedules
                              where
                                  x.Id != scheduleId &&
                                  (((x.FromDay <= fromDay && x.ToDay >= fromDay) || (x.FromDay <= toDay && x.ToDay >= toDay) ||
                                      (fromDay <= x.FromDay && toDay >= x.FromDay) || (fromDay <= x.ToDay && toDay >= x.ToDay)) &&
                                      ((x.FromHour <= fromHour && x.ToHour >= fromHour ||
                                      x.FromHour <= toHour && x.ToHour >= toHour) ||
                                      (fromHour <= x.FromHour && toHour >= x.FromHour ||
                                      fromHour <= x.ToHour && toHour >= x.ToHour)))
                              select x;

                if (!queryEv.Any())
                {
                    DefbroadcastingSchedules schedule = null;

                    var query = from x in this._businessObjects.Context.DefbroadcastingSchedules
                                where x.Id == scheduleId
                                select x;

                    foreach (DefbroadcastingSchedules item in query)
                    {
                        schedule = item;
                    }

                    if (schedule != null)
                    {

                        schedule.FromDay = fromDay;
                        schedule.ToDay = toDay;
                        schedule.FromHour = fromHour;
                        schedule.ToHour = toHour;
                        schedule.FromMinute = fromMin;
                        schedule.ToMinute = toMin;
                        schedule.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        currentSchedule = new BroadcastingSchedule
                        {
                            Id = schedule.Id,
                            ContentId = schedule.ContentId,
                            ContentType = schedule.ContentType,
                            FromDay = schedule.FromDay,
                            ToDay = schedule.ToDay,
                            FromHour = schedule.FromHour,
                            ToHour = schedule.ToHour,
                            FromMinutes = schedule.FromMinute,
                            ToMinutes = schedule.ToMinute,
                            CreatedDate = schedule.CreatedDate,
                            UpdatedDate = schedule.UpdatedDate
                        };
                    }
                }
                else
                {
                    throw new ArgumentException(Resources.ScheduleOverlaps);
                }

            }
            catch (Exception e)
            {
                currentSchedule = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return currentSchedule;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Deletes a schedule
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public bool Delete(Guid scheduleId)
        {
            bool success = false;

            try
            {
                DefbroadcastingSchedules schedule = null;

                var query = from x in this._businessObjects.Context.DefbroadcastingSchedules
                            where x.Id == scheduleId
                            select x;


                foreach (var item in query)
                {
                    schedule = item;
                }

                if (schedule != null)
                {
                    this._businessObjects.Context.DefbroadcastingSchedules.Remove(schedule);
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
        }//DELETE ENDS ---------------------------------------------------------------------------------------------------------------------------------- //

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new BroadcastingScheduleManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public BroadcastingScheduleManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD BROADCASTING SCHEDULE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
