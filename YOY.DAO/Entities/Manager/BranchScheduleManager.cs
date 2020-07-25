using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class BranchScheduleManager
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
        /// Retrieves all the branch's schedules
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public List<BranchSchedule> Gets(Guid branchId)
        {
            List<BranchSchedule> schedules = new List<BranchSchedule>();

            try
            {
                var query = from x in this._businessObjects.Context.DefbranchSchedules
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == branchId
                            orderby x.FromDay ascending
                            select x;

                BranchSchedule currentSchedule = null;

                foreach (DefbranchSchedules item in query)
                {
                    currentSchedule = new BranchSchedule
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        FromDay = item.FromDay,
                        ToDay = item.ToDay,
                        FromHour = item.FromHour,
                        ToHour = item.ToHour,
                        FromMinutes = item.FromMinutes,
                        ToMinutes = item.ToMinutes,
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
        /// Retrieve the branch's schedule for a specific date
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<BranchSchedule> Gets(Guid branchId, DateTime dateTime)
        {
            List<BranchSchedule> schedules = new List<BranchSchedule>();

            int dayOfWeek = (int)dateTime.DayOfWeek;

            try
            {
                var query = from x in this._businessObjects.Context.DefbranchSchedules
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == branchId && ((x.FromDay <= dayOfWeek && x.ToDay >= dayOfWeek) || (x.FromDay >= dayOfWeek && x.ToDay <= dayOfWeek))
                            orderby x.FromDay ascending
                            select x;

                foreach (var item in query)
                {
                    BranchSchedule currentSchedule = new BranchSchedule
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        FromDay = item.FromDay,
                        ToDay = item.ToDay,
                        FromHour = item.FromHour,
                        ToHour = item.ToHour,
                        FromMinutes = item.FromMinutes,
                        ToMinutes = item.ToMinutes,
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
        /// Retrieve all the schedules for a branch
        /// on a days interval
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="fromDay"></param>
        /// <param name="toDay"></param>
        /// <returns></returns>
        public List<BranchSchedule> Gets(Guid branchId, int fromDay, int toDay)
        {
            List<BranchSchedule> schedules = new List<BranchSchedule>();

            try
            {
                var query = from x in this._businessObjects.Context.DefbranchSchedules
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == branchId && x.FromDay == fromDay && x.ToDay == toDay
                            orderby x.FromDay ascending
                            select x;

                foreach (var item in query)
                {
                    BranchSchedule currentSchedule = new BranchSchedule
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        FromDay = item.FromDay,
                        ToDay = item.ToDay,
                        FromHour = item.FromHour,
                        FromMinutes = item.FromMinutes,
                        ToHour = item.ToHour,
                        ToMinutes = item.ToMinutes,
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
        /// Retrieves a schedule
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public BranchSchedule Get(Guid scheduleId)
        {
            BranchSchedule schedule = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefbranchSchedules
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == scheduleId
                            select x;

                foreach (var item in query)
                {
                    schedule = new BranchSchedule
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        FromDay = item.FromDay,
                        ToDay = item.ToDay,
                        FromHour = item.FromHour,
                        ToHour = item.ToHour,
                        FromMinutes = item.FromMinutes,
                        ToMinutes = item.ToMinutes,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
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
        /// <param name="branchId"></param>
        /// <param name="fromHour"></param>
        /// <param name="toHour"></param>
        /// <param name="fromDay"></param>
        /// <param name="toDay"></param>
        /// <returns></returns>
        public BranchSchedule Post(Guid branchId, int fromDay, int toDay, int fromHour, int fromMinutes, int toHour, int toMinutes)
        {
            BranchSchedule newSchedule = null;

            DefbranchSchedules schedule = null;
            bool insertAttempt = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefbranchSchedules
                            where
                                x.BranchId == branchId &&
                                ((x.FromDay <= fromDay && x.ToDay >= fromDay && x.FromHour <= fromHour && x.ToHour >= fromHour && x.FromMinutes <= fromMinutes && x.ToMinutes >= fromMinutes) ||
                                 (x.FromDay <= toDay && x.ToDay >= toDay && x.FromHour <= toHour && x.ToHour >= toHour && x.FromMinutes <= toMinutes && x.ToMinutes >= toMinutes) ||
                                 (fromDay <= x.FromDay && toDay >= x.FromDay && fromHour <= x.FromHour && toHour >= x.FromHour && fromMinutes <= x.FromMinutes && toMinutes >= x.FromMinutes) ||
                                 (fromDay <= x.ToDay && toDay >= x.ToDay && fromHour <= x.ToHour && toHour >= x.ToHour && fromMinutes <= x.ToMinutes && toMinutes >= x.ToMinutes))
                            select x;

                if (!query.Any())
                {

                    schedule = new DefbranchSchedules
                    {
                        Id = Guid.NewGuid(),
                        TenantId = this._businessObjects.Tenant.TenantId,
                        BranchId = branchId,
                        FromDay = fromDay,
                        ToDay = toDay,
                        FromHour = fromHour,
                        ToHour = toHour,
                        FromMinutes = fromMinutes,
                        ToMinutes = toMinutes,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };

                    this._businessObjects.Context.DefbranchSchedules.Add(schedule);
                    this._businessObjects.Context.SaveChanges();

                    insertAttempt = true;

                    newSchedule = new BranchSchedule
                    {
                        Id = schedule.Id,
                        BranchId = schedule.BranchId,
                        TenantId = schedule.TenantId,
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
                if (insertAttempt)
                {
                    this._businessObjects.Context.DefbranchSchedules.Remove(schedule);
                    this._businessObjects.Context.SaveChanges();
                }

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
        public BranchSchedule Put(Guid scheduleId, int fromMinutes, int toMinutes, int fromHour, int toHour, int fromDay, int toDay)
        {
            BranchSchedule currentSchedule = null;

            try
            {
                var queryEv = from x in this._businessObjects.Context.DefbranchSchedules
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
                    DefbranchSchedules schedule = null;

                    var query = from x in this._businessObjects.Context.DefbranchSchedules
                                where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == scheduleId
                                select x;

                    foreach (var item in query)
                    {
                        schedule = item;
                    }

                    if (schedule != null)
                    {

                        schedule.FromDay = fromDay;
                        schedule.ToDay = toDay;
                        schedule.FromHour = fromHour;
                        schedule.ToHour = toHour;
                        schedule.FromMinutes = fromMinutes;
                        schedule.ToMinutes = toMinutes;
                        schedule.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        currentSchedule = new BranchSchedule
                        {
                            Id = schedule.Id,
                            BranchId = schedule.BranchId,
                            TenantId = schedule.TenantId,
                            FromDay = schedule.FromDay,
                            ToDay = schedule.ToDay,
                            FromHour = schedule.FromHour,
                            ToHour = schedule.ToHour,
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
                DefbranchSchedules schedule = null;

                var query = from x in this._businessObjects.Context.DefbranchSchedules
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == scheduleId
                            select x;


                foreach (var item in query)
                {
                    schedule = item;
                }

                if (schedule != null)
                {
                    this._businessObjects.Context.DefbranchSchedules.Remove(schedule);
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
        /// Creates a new FileManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public BranchScheduleManager(BusinessObjects businessObjects)
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
