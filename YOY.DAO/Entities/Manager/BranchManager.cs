using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.Structure.POCO;
using System.Xml.Linq;
using YOY.Security.Cryptography;
using System.Text;
using YOY.DTO.Entities.Misc.Branch;

namespace YOY.DAO.Entities.Manager
{
    public class BranchManager
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
        // METHODS                                                                                                                                  //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        #region METHODS

        private string GetTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case BranchTypes.Commerce:
                    typeName = Resources.Commerce;
                    break;
                case BranchTypes.Holder:
                    typeName = Resources.Holder;
                    break;
            }

            return typeName;
        }

        /// <summary>
        /// Retrieves all the branches that 
        /// </summary>
        /// <param name="branches"></param>
        /// <param name="openState"></param>
        /// <returns></returns>
        private List<DefbranchesView> GetBranchesByOpenState(List<DefbranchesView> branches, int openState, DateTime datetime)
        {
            List<DefbranchesView> filteredBranches = new List<DefbranchesView>();

            DateTime basedDateTime = datetime;
            int dayOfWeek = -1;
            int hour = -1;
            int min = -1;
            var schedules = (dynamic)null;
            int count = 0;

            int tempFromDay = -1;
            int tempToDay = -1;
            int tempCurrentDay = -1;

            foreach (DefbranchesView branch in branches)
            {
                //Get right reference date based on branch timezone
                basedDateTime = basedDateTime.AddHours(branch.UtcTimeZone);

                dayOfWeek = (int)basedDateTime.DayOfWeek;
                hour = basedDateTime.Hour;
                min = basedDateTime.Minute;

                //Get all schedules for the branch
                Guid branchId = branch.Id;
                schedules = from x in this._businessObjects.Context.DefbranchSchedules
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == branchId
                            select x;

                foreach (DefbranchSchedules item in schedules)
                {

                    //Move the schedule to the start of the week to normalize it
                    //Because of this the schedule can have only 2 cases, 1 day schedule 
                    tempFromDay = 0;
                    tempToDay = item.ToDay - item.FromDay;
                    tempCurrentDay = dayOfWeek = item.FromDay;

                    if (tempToDay < 0)
                    {
                        tempToDay += 7;
                    }

                    if (tempCurrentDay < 0)
                    {
                        tempCurrentDay += 7;
                    }

                    //if it's 1 day schedule
                    if (tempFromDay == tempToDay)
                    {
                        //if it's 1 hour schedule
                        if (item.FromHour == item.ToHour)
                        {
                            //if current branch hour is inside the minutes range
                            if (tempFromDay == tempCurrentDay && item.FromHour == hour && item.FromMinutes <= min && min <= item.ToMinutes)
                            {
                                ++count;
                            }
                        }
                        else  //If it's same day but different hour schedule
                        {
                            if (tempFromDay == tempCurrentDay)//if current day matches with the schedule day
                            {
                                //Inside the hour range not matching hour limits
                                if (item.FromHour < hour && hour < item.ToHour)
                                {
                                    ++count;
                                }
                                else
                                {//If schedule matches in its limit with current hour

                                    if (item.FromHour == hour)//If current hour matches with the beginning schedule hour
                                    {
                                        if (item.FromMinutes < min)
                                        {
                                            ++count;
                                        }
                                    }
                                    else
                                    {
                                        if (hour == item.ToHour)//if current hour matches with ending schedule hour
                                        {
                                            if (min < item.ToMinutes)
                                            {
                                                ++count;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                    else//More than 1 day schedule
                    {
                        if (tempFromDay < tempToDay)//if beginning day is earlier day then ending one
                        {
                            if (tempFromDay <= tempCurrentDay && tempCurrentDay <= tempToDay)//if current day is sinde the days range
                            {
                                //Inside the hour range not matching hour limits
                                if (item.FromHour < hour && hour < item.ToHour)
                                {
                                    ++count;
                                }
                                else
                                {//If schedule matches in its limit with current hour

                                    if (item.FromHour == hour)//If current hour matches with the beginning schedule hour
                                    {
                                        if (item.FromMinutes < min)
                                        {
                                            ++count;
                                        }
                                    }
                                    else
                                    {
                                        if (hour == item.ToHour)//if current hour matches with ending schedule hour
                                        {
                                            if (min < item.ToMinutes)
                                            {
                                                ++count;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                if (openState == OpenStates.Opened)
                {
                    if (count > 0)
                    {
                        filteredBranches.Add(branch);
                    }
                }
                else
                {
                    if (openState == OpenStates.Closed)
                    {
                        if (count == 0)
                        {
                            filteredBranches.Add(branch);
                        }
                    }
                }

                count = 0;

            }

            return filteredBranches;
        }//GETBRANCHESOPENSTATE ENDS --------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Get is a branch is open
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="openState"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private bool CheckOpenState(Guid branchId, int openState, DateTime datetime, int UTCTimeDiff)
        {
            bool open = false;

            DateTime basedDateTime = datetime.AddHours(UTCTimeDiff);
            int dayOfWeek = (int)basedDateTime.DayOfWeek;
            int hour = basedDateTime.Hour;
            int min = basedDateTime.Minute;
            var schedules = (dynamic)null;
            int count = 0;


            int tempFromDay = -1;
            int tempToDay = -1;
            int tempCurrentDay = -1;

            schedules = from x in this._businessObjects.Context.DefbranchSchedules
                        where x.TenantId == this._businessObjects.Tenant.TenantId && x.BranchId == branchId
                        select x;



            foreach (var item in schedules)
            {
                //Move the schedule to the start of the week to normalize it
                //Because of this the schedule can have only 2 cases, 1 day schedule 
                tempFromDay = 0;
                tempToDay = item.ToDay - item.FromDay;
                tempCurrentDay = dayOfWeek = item.FromDay;

                if (tempToDay < 0)
                {
                    tempToDay += 7;
                }

                if (tempCurrentDay < 0)
                {
                    tempCurrentDay += 7;
                }

                //if it's 1 day schedule
                if (tempFromDay == tempToDay)
                {
                    //if it's 1 hour schedule
                    if (item.FromHour == item.ToHour)
                    {
                        //if current branch hour is inside the minutes range
                        if (tempFromDay == tempCurrentDay && item.FromHour == hour && item.FromMinutes <= min && min <= item.ToMinutes)
                        {
                            ++count;
                        }
                    }
                    else  //If it's same day but different hour schedule
                    {
                        if (tempFromDay == tempCurrentDay)//if current day matches with the schedule day
                        {
                            //Inside the hour range not matching hour limits
                            if (item.FromHour < hour && hour < item.ToHour)
                            {
                                ++count;
                            }
                            else
                            {//If schedule matches in its limit with current hour

                                if (item.FromHour == hour)//If current hour matches with the beginning schedule hour
                                {
                                    if (item.FromMinutes < min)
                                    {
                                        ++count;
                                    }
                                }
                                else
                                {
                                    if (hour == item.ToHour)//if current hour matches with ending schedule hour
                                    {
                                        if (min < item.ToMinutes)
                                        {
                                            ++count;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                else//More than 1 day schedule
                {
                    if (tempFromDay < tempToDay)//if beginning day is earlier day then ending one
                    {
                        if (tempFromDay <= tempCurrentDay && tempCurrentDay <= tempToDay)//if current day is sinde the days range
                        {
                            //Inside the hour range not matching hour limits
                            if (item.FromHour < hour && hour < item.ToHour)
                            {
                                ++count;
                            }
                            else
                            {//If schedule matches in its limit with current hour

                                if (item.FromHour == hour)//If current hour matches with the beginning schedule hour
                                {
                                    if (item.FromMinutes < min)
                                    {
                                        ++count;
                                    }
                                }
                                else
                                {
                                    if (hour == item.ToHour)//if current hour matches with ending schedule hour
                                    {
                                        if (min < item.ToMinutes)
                                        {
                                            ++count;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (openState == OpenStates.Opened)
            {
                if (count > 0)
                {
                    open = true;
                }
            }
            else
            {
                if (openState == OpenStates.Closed)
                {
                    if (count == 0)
                    {
                        open = false;
                    }
                }
            }

            count = 0;

            return open;
        }//CHECKOPENSTATE ENDS --------------------------------------------------------------------------------------------------------------------- //



        /// <summary>
        /// Returns tenants for the given parameters
        /// </summary>
        /// <param name="activeState"></param>
        /// <param name="openState"></param>
        /// <param name="dateTime"></param>
        /// <param name="filterByTenant"></param>
        /// <returns></returns>
        public List<Branch> Gets(int activeState, int openState, DateTime dateTime, bool filterByTenant, int pageSize, int pageNumber)
        {
            List<Branch> businessBranches = new List<Branch>();

            try
            {

                List<DefbranchesView> branches = new List<DefbranchesView>();

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        if (filterByTenant)
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.TenantId == this._businessObjects.Tenant.TenantId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;
                    case ActiveStates.Active:

                        if (filterByTenant)
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.TenantId == this._businessObjects.Tenant.TenantId && x.IsActive
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.IsActive == true
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:

                        if (filterByTenant)
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.IsActive == false
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;

                }

                foreach (DefbranchesView item in branches)
                {
                    Branch currentBranch = new Branch
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Type = item.Type,
                        TypeName = this.GetTypeName(item.Type),
                        BranchHolderId = item.BranchHolderId,
                        BranchHolderName = item.BranchHolderName,
                        BranchHolderContactPhoneNumber = item.BranchHolderContactPhoneNumber,
                        BranchHolderEmail = item.BranchHolderEmail,
                        BranchHolderActiveState = item.BranchHolderActiveState,
                        BrachHolderDepartmentId = item.BranchHolderDepartmentId,
                        BranchHolderDepartmentName = item.BranchHolderDepartmentName,
                        BranchHolderDepartmentActiveState = item.BranchHolderDepartmentActiveState,
                        Name = item.Name,
                        PostCode = item.PostCode,
                        Email = item.Email,
                        ContactName = item.ContactName,
                        ContactPhoneNumber = item.ContactPhoneNumber,
                        ContactEmail = item.ContactEmail,
                        OrderInquiriesPhoneNumber = item.OrdersPhoneNumber,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        LocationAddress = XElement.Parse(item.LocationAddress),
                        DescriptiveAddress = item.DescriptiveAddress,
                        UtcTimeZone = item.UtcTimeZone,
                        StateId = item.StateId,
                        StateName = item.StateName,
                        CityId = item.CityId,
                        CityName = item.CityName,
                        OrderTakingType = item.OrderTakingType,
                        GeofenceId = item.GeofenceId,
                        HashedCode = item.HashedCode
                    };
                    currentBranch.IsOpen = this.CheckOpenState(currentBranch.Id, OpenStates.Opened, dateTime, currentBranch.UtcTimeZone);


                    businessBranches.Add(currentBranch);
                }
            }
            catch (Exception e)
            {
                businessBranches = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return businessBranches;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Returns tenants for the given parameters
        /// </summary>
        /// <param name="activeState"></param>
        /// <param name="openState"></param>
        /// <param name="dateTime"></param>
        /// <param name="filterByTenant"></param>
        /// <returns></returns>
        public List<Branch> Gets(Guid tenantId, int activeState, int openState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<Branch> businessBranches = new List<Branch>();

            try
            {

                List<DefbranchesView> branches = new List<DefbranchesView>();

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        query = (from x in this._businessObjects.Context.DefbranchesView
                                 where x.TenantId == tenantId
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;
                    case ActiveStates.Active:

                        query = (from x in this._businessObjects.Context.DefbranchesView
                                 where x.TenantId == tenantId && x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:

                        query = (from x in this._businessObjects.Context.DefbranchesView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId && !x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;

                }

                foreach (DefbranchesView item in branches)
                {
                    Branch currentBranch = new Branch
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Type = item.Type,
                        TypeName = this.GetTypeName(item.Type),
                        BranchHolderId = item.BranchHolderId,
                        BranchHolderName = item.BranchHolderName,
                        BranchHolderContactPhoneNumber = item.BranchHolderContactPhoneNumber,
                        BranchHolderEmail = item.BranchHolderEmail,
                        BranchHolderActiveState = item.BranchHolderActiveState,
                        BrachHolderDepartmentId = item.BranchHolderDepartmentId,
                        BranchHolderDepartmentName = item.BranchHolderDepartmentName,
                        BranchHolderDepartmentActiveState = item.BranchHolderDepartmentActiveState,
                        Name = item.Name,
                        PostCode = item.PostCode,
                        Email = item.Email,
                        ContactName = item.ContactName,
                        ContactPhoneNumber = item.ContactPhoneNumber,
                        ContactEmail = item.ContactEmail,
                        OrderInquiriesPhoneNumber = item.OrdersPhoneNumber,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        LocationAddress = XElement.Parse(item.LocationAddress),
                        DescriptiveAddress = item.DescriptiveAddress,
                        UtcTimeZone = item.UtcTimeZone,
                        StateId = item.StateId,
                        StateName = item.StateName,
                        CityId = item.CityId,
                        CityName = item.CityName,
                        OrderTakingType = item.OrderTakingType,
                        GeofenceId = item.GeofenceId,
                        HashedCode = item.HashedCode
                    };
                    currentBranch.IsOpen = this.CheckOpenState(currentBranch.Id, OpenStates.Opened, dateTime, currentBranch.UtcTimeZone);


                    businessBranches.Add(currentBranch);
                }
            }
            catch (Exception e)
            {
                businessBranches = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return businessBranches;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Returns all the branches inside a geofence that matches the given params
        /// </summary>
        /// <param name="activeState"></param>
        /// <param name="openState"></param>
        /// <param name="dateTime"></param>
        /// <param name="filterByTenant"></param>
        /// <param name="geofenceId"></param>
        /// <returns></returns>
        public List<Branch> Gets(int activeState, int openState, DateTime dateTime, Guid? tenantId, Guid geofenceId, int pageSize, int pageNumber)
        {
            List<Branch> businessBranches = new List<Branch>();

            try
            {

                List<DefbranchesView> branches = new List<DefbranchesView>();

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        if (tenantId != null)
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.TenantId == tenantId && x.GeofenceId == geofenceId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.GeofenceId == geofenceId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;
                    case ActiveStates.Active:

                        if (tenantId != null)
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.TenantId == tenantId && x.IsActive && x.GeofenceId == geofenceId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.IsActive && x.GeofenceId == geofenceId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:

                        if (tenantId != null)
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where x.TenantId == tenantId && !x.IsActive && x.GeofenceId == geofenceId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.DefbranchesView
                                     where !x.IsActive && x.GeofenceId == geofenceId
                                     orderby x.Name ascending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;

                }

                foreach (DefbranchesView item in branches)
                {
                    Branch currentBranch = new Branch
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Type = item.Type,
                        TypeName = this.GetTypeName(item.Type),
                        BranchHolderId = item.BranchHolderId,
                        BranchHolderName = item.BranchHolderName,
                        BranchHolderContactPhoneNumber = item.BranchHolderContactPhoneNumber,
                        BranchHolderEmail = item.BranchHolderEmail,
                        BranchHolderActiveState = item.BranchHolderActiveState,
                        BrachHolderDepartmentId = item.BranchHolderDepartmentId,
                        BranchHolderDepartmentName = item.BranchHolderDepartmentName,
                        BranchHolderDepartmentActiveState = item.BranchHolderDepartmentActiveState,
                        Name = item.Name,
                        PostCode = item.PostCode,
                        Email = item.Email,
                        ContactName = item.ContactName,
                        ContactPhoneNumber = item.ContactPhoneNumber,
                        ContactEmail = item.ContactEmail,
                        OrderInquiriesPhoneNumber = item.OrdersPhoneNumber,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        LocationAddress = XElement.Parse(item.LocationAddress),
                        DescriptiveAddress = item.DescriptiveAddress,
                        UtcTimeZone = item.UtcTimeZone,
                        StateId = item.StateId,
                        StateName = item.StateName,
                        CityId = item.CityId,
                        CityName = item.CityName,
                        OrderTakingType = item.OrderTakingType,
                        GeofenceId = item.GeofenceId,
                        HashedCode = item.HashedCode
                    };
                    currentBranch.IsOpen = this.CheckOpenState(currentBranch.Id, OpenStates.Opened, dateTime, currentBranch.UtcTimeZone);


                    businessBranches.Add(currentBranch);
                }
            }
            catch (Exception e)
            {
                businessBranches = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return businessBranches;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Returns all the branches for a given tenant
        /// </summary>
        /// <param name="activeState"></param>
        /// <param name="openState"></param>
        /// <param name="dateTime"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<Branch> Gets(int activeState, int openState, DateTime dateTime, Guid tenantId, int pageSize, int pageNumber)
        {
            List<Branch> businessBranches = new List<Branch>();

            try
            {

                List<DefbranchesView> branches = new List<DefbranchesView>();

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        query = (from x in this._businessObjects.Context.DefbranchesView
                                 where x.TenantId == tenantId
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;
                    case ActiveStates.Active:

                        query = (from x in this._businessObjects.Context.DefbranchesView
                                 where x.TenantId == tenantId && x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:

                        query = (from x in this._businessObjects.Context.DefbranchesView
                                 where x.TenantId == tenantId && !x.IsActive
                                 orderby x.Name ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);

                        foreach (var item in query)
                        {
                            branches.Add(item);
                        }

                        switch (openState)
                        {
                            case OpenStates.Opened:
                            case OpenStates.Closed:
                                branches = GetBranchesByOpenState(branches, openState, dateTime);
                                break;
                        }
                        break;

                }

                foreach (DefbranchesView item in branches)
                {
                    Branch currentBranch = new Branch
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Type = item.Type,
                        TypeName = this.GetTypeName(item.Type),
                        BranchHolderId = item.BranchHolderId,
                        BranchHolderName = item.BranchHolderName,
                        BranchHolderContactPhoneNumber = item.BranchHolderContactPhoneNumber,
                        BranchHolderEmail = item.BranchHolderEmail,
                        BranchHolderActiveState = item.BranchHolderActiveState,
                        BrachHolderDepartmentId = item.BranchHolderDepartmentId,
                        BranchHolderDepartmentName = item.BranchHolderDepartmentName,
                        BranchHolderDepartmentActiveState = item.BranchHolderDepartmentActiveState,
                        Name = item.Name,
                        PostCode = item.PostCode,
                        Email = item.Email,
                        ContactName = item.ContactName,
                        ContactPhoneNumber = item.ContactPhoneNumber,
                        ContactEmail = item.ContactEmail,
                        OrderInquiriesPhoneNumber = item.OrdersPhoneNumber,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        LocationAddress = XElement.Parse(item.LocationAddress),
                        DescriptiveAddress = item.DescriptiveAddress,
                        UtcTimeZone = item.UtcTimeZone,
                        StateId = item.StateId,
                        StateName = item.StateName,
                        CityId = item.CityId,
                        CityName = item.CityName,
                        OrderTakingType = item.OrderTakingType,
                        GeofenceId = item.GeofenceId,
                        HashedCode = item.HashedCode
                    };
                    currentBranch.IsOpen = this.CheckOpenState(currentBranch.Id, OpenStates.Opened, dateTime, currentBranch.UtcTimeZone);


                    businessBranches.Add(currentBranch);
                }
            }
            catch (Exception e)
            {
                businessBranches = null;
                // ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return businessBranches;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //

        public List<Branch> Gets(int type)
        {
            List<Branch> businessBranches = new List<Branch>();

            try
            {

                List<DefbranchesView> branches = new List<DefbranchesView>();

                var query = (dynamic)null;

                switch (type)
                {
                    case 1:

                        query = from x in this._businessObjects.Context.DefbranchesView
                                 where x.Type == BranchTypes.Holder
                                 select x;
                        break;
                    case 2:

                        query = from x in this._businessObjects.Context.DefbranchesView
                                 where x.BranchHolderId != null
                                 select x;
                        break;
                    

                }

                foreach (DefbranchesView item in query)
                {
                    Branch currentBranch = new Branch
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Type = item.Type,
                        TypeName = this.GetTypeName(item.Type),
                        BranchHolderId = item.BranchHolderId,
                        BranchHolderName = item.BranchHolderName,
                        BranchHolderContactPhoneNumber = item.BranchHolderContactPhoneNumber,
                        BranchHolderEmail = item.BranchHolderEmail,
                        BranchHolderActiveState = item.BranchHolderActiveState,
                        BrachHolderDepartmentId = item.BranchHolderDepartmentId,
                        BranchHolderDepartmentName = item.BranchHolderDepartmentName,
                        BranchHolderDepartmentActiveState = item.BranchHolderDepartmentActiveState,
                        Name = item.Name,
                        PostCode = item.PostCode,
                        Email = item.Email,
                        ContactName = item.ContactName,
                        ContactPhoneNumber = item.ContactPhoneNumber,
                        ContactEmail = item.ContactEmail,
                        OrderInquiriesPhoneNumber = item.OrdersPhoneNumber,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        LocationAddress = XElement.Parse(item.LocationAddress),
                        DescriptiveAddress = item.DescriptiveAddress,
                        UtcTimeZone = item.UtcTimeZone,
                        StateId = item.StateId,
                        StateName = item.StateName,
                        CityId = item.CityId,
                        CityName = item.CityName,
                        OrderTakingType = item.OrderTakingType,
                        GeofenceId = item.GeofenceId,
                        HashedCode = item.HashedCode
                    };


                    businessBranches.Add(currentBranch);
                }
            }
            catch (Exception e)
            {
                businessBranches = null;
                // ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return businessBranches;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        public List<Pair<Guid, string>> Gets(Guid tenantId, int activeState)
        {
            List<Pair<Guid, string>> branches = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = from x in this._businessObjects.Context.DefbranchesView
                                where x.TenantId == tenantId
                                orderby x.Name ascending
                                select x;
                        break;
                    case ActiveStates.Active:
                        query = from x in this._businessObjects.Context.DefbranchesView
                                where x.IsActive && x.TenantId == tenantId
                                orderby x.Name ascending
                                select x;
                        break;
                    case ActiveStates.Inactive:
                        query = from x in this._businessObjects.Context.DefbranchesView
                                where !x.IsActive && x.TenantId == tenantId
                                orderby x.Name ascending
                                select x;
                        break;
                }

                if (query != null)
                {
                    Pair<Guid, string> branchData = null;
                    branches = new List<Pair<Guid, string>>();

                    foreach (DefbranchesView item in query)
                    {
                        branchData = new Pair<Guid, string>
                        {
                            Key = item.Id,
                            Value = item.Name
                        };

                        branches.Add(branchData);
                    }
                }
            }
            catch (Exception e)
            {
                branches = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return branches;
        }

        /// <summary>
        /// Returns a business branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public Branch Get(Guid branchId, bool fileterByTenant, DateTime date)
        {
            Branch branch = null;

            try
            {
                var query = (dynamic)null;

                if (fileterByTenant)
                {
                    query = from x in this._businessObjects.Context.DefbranchesView
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == branchId
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.DefbranchesView
                            where x.Id == branchId
                            select x;
                }

                foreach (DefbranchesView item in query)
                {
                    branch = new Branch
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Type = item.Type,
                        TypeName = this.GetTypeName(item.Type),
                        BranchHolderId = item.BranchHolderId,
                        BranchHolderName = item.BranchHolderName,
                        BranchHolderContactPhoneNumber = item.BranchHolderContactPhoneNumber,
                        BranchHolderEmail = item.BranchHolderEmail,
                        BranchHolderActiveState = item.BranchHolderActiveState,
                        BrachHolderDepartmentId = item.BranchHolderDepartmentId,
                        BranchHolderDepartmentName = item.BranchHolderDepartmentName,
                        BranchHolderDepartmentActiveState = item.BranchHolderDepartmentActiveState,
                        Name = item.Name,
                        PostCode = item.PostCode,
                        Email = item.Email,
                        ContactName = item.ContactName,
                        ContactPhoneNumber = item.ContactPhoneNumber,
                        ContactEmail = item.ContactEmail,
                        OrderInquiriesPhoneNumber = item.OrdersPhoneNumber,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        LocationAddress = XElement.Parse(item.LocationAddress),
                        DescriptiveAddress = item.DescriptiveAddress,
                        UtcTimeZone = item.UtcTimeZone,
                        StateId = item.StateId,
                        StateName = item.StateName,
                        CityId = item.CityId,
                        CityName = item.CityName,
                        OrderTakingType = item.OrderTakingType,
                        GeofenceId = item.GeofenceId,
                        GeofenceName = item.GeofenceName,
                        HashedCode = item.HashedCode
                    };
                    branch.IsOpen = this.CheckOpenState(branch.Id, OpenStates.Opened, date, branch.UtcTimeZone);

                }
            }
            catch (Exception e)
            {
                branch = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return branch;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Returns the list of branches contained in the geofence
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public List<Branch> Gets(string geofenceId, Guid? tenantId, DateTime date, int pageSize, int pageNumber)
        {
            List<Branch> branches = new List<Branch>();
            Branch branch = null;

            try
            {
                var query = (dynamic)null;

                if (tenantId != null)
                {
                    query = (from x in this._businessObjects.Context.DefbranchesView
                             where x.TenantId == tenantId && x.GeofenceExternalId == geofenceId
                             orderby x.Name ascending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }
                else
                {
                    query = (from x in this._businessObjects.Context.DefbranchesView
                             where x.GeofenceExternalId == geofenceId
                             orderby x.Name ascending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                }

                foreach (DefbranchesView item in query)
                {
                    branch = new Branch
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Type = item.Type,
                        TypeName = this.GetTypeName(item.Type),
                        BranchHolderId = item.BranchHolderId,
                        BranchHolderName = item.BranchHolderName,
                        BranchHolderContactPhoneNumber = item.BranchHolderContactPhoneNumber,
                        BranchHolderEmail = item.BranchHolderEmail,
                        BranchHolderActiveState = item.BranchHolderActiveState,
                        BrachHolderDepartmentId = item.BranchHolderDepartmentId,
                        BranchHolderDepartmentName = item.BranchHolderDepartmentName,
                        BranchHolderDepartmentActiveState = item.BranchHolderDepartmentActiveState,
                        Name = item.Name,
                        PostCode = item.PostCode,
                        Email = item.Email,
                        ContactName = item.ContactName,
                        ContactPhoneNumber = item.ContactPhoneNumber,
                        ContactEmail = item.ContactEmail,
                        OrderInquiriesPhoneNumber = item.OrdersPhoneNumber,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        LocationAddress = XElement.Parse(item.LocationAddress),
                        DescriptiveAddress = item.DescriptiveAddress,
                        UtcTimeZone = item.UtcTimeZone,
                        StateId = item.StateId,
                        StateName = item.StateName,
                        CityId = item.CityId,
                        CityName = item.CityName,
                        OrderTakingType = item.OrderTakingType,
                        GeofenceId = item.GeofenceId,
                        HashedCode = item.HashedCode
                    };
                    branch.IsOpen = this.CheckOpenState(branch.Id, OpenStates.Opened, date, branch.UtcTimeZone);

                    branches.Add(branch);
                }
            }
            catch (Exception e)
            {
                branches = null;
                // ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return branches;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Creates a new branch
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="contactPhone"></param>
        /// <param name="ordersPhone"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="descriptiveAddress"></param>
        /// <param name="locationAddress"></param>
        /// <returns></returns>
        public Branch Post(Guid tenantId, Guid? franchiseeId, Guid stateId, Guid cityId, Guid? branchHolderId, Guid? branchHolderDepartmentId, int type, string name, string postCode, string email, string contactName, string contactPhone,
            string contactEmail, string ordersPhone, bool independantOwner, decimal latitude, decimal longitude, XElement locationAddress, string descriptiveAddress, int orderTakingType, Guid? geofenceId)//XElement locationAddress
        {
            Branch currentBranch = null;

            DefbranchesView branch = null;

            try
            {

                Guid newId = Guid.NewGuid();
                string hashedCode = SecurityHash.ComputeHash(newId.ToString(), "SHA512", Encoding.UTF8.GetBytes(Settings.Default.salt));


                bool? success = this._businessObjects.StoredProcsHandler.InsertBranch(
                                newId,
                                tenantId,
                                franchiseeId,
                                stateId,
                                cityId,
                                branchHolderId,
                                branchHolderDepartmentId,
                                type,
                                name,
                                postCode,
                                email,
                                contactName,
                                contactPhone,
                                contactEmail,
                                ordersPhone,
                                independantOwner,
                                latitude,
                                longitude,
                                locationAddress,
                                descriptiveAddress,
                                orderTakingType,
                                geofenceId,
                                hashedCode);


                //if (success == 0)
                //success = this._businessObjects.Context.SetBranchGeoPoint(newId, latitude, longitude);

                this._businessObjects.Context.SaveChanges();

                if (success == true)
                {
                    branch = (from x in this._businessObjects.Context.DefbranchesView
                              where x.Id == newId
                              select x).FirstOrDefault();

                    if (branch != null)
                    {
                        currentBranch = new Branch
                        {
                            Id = branch.Id,
                            TenantId = branch.TenantId,
                            Type = branch.Type,
                            TypeName = this.GetTypeName(branch.Type),
                            BranchHolderId = branch.BranchHolderId,
                            BranchHolderName = branch.BranchHolderName,
                            BranchHolderContactPhoneNumber = branch.BranchHolderContactPhoneNumber,
                            BranchHolderEmail = branch.BranchHolderEmail,
                            BranchHolderActiveState = branch.BranchHolderActiveState,
                            BrachHolderDepartmentId = branch.BranchHolderDepartmentId,
                            BranchHolderDepartmentName = branch.BranchHolderDepartmentName,
                            BranchHolderDepartmentActiveState = branch.BranchHolderDepartmentActiveState,
                            Name = branch.Name,
                            PostCode = branch.PostCode,
                            Email = branch.Email,
                            ContactName = branch.ContactName,
                            ContactPhoneNumber = branch.ContactPhoneNumber,
                            ContactEmail = branch.ContactEmail,
                            OrderInquiriesPhoneNumber = branch.OrdersPhoneNumber,
                            IsActive = branch.IsActive,
                            CreatedDate = branch.CreatedDate,
                            UpdatedDate = branch.UpdatedDate,
                            Latitude = branch.Latitude,
                            Longitude = branch.Longitude,
                            LocationAddress = XElement.Parse(branch.LocationAddress),
                            DescriptiveAddress = branch.DescriptiveAddress,
                            OrderTakingType = branch.OrderTakingType,
                            GeofenceId = branch.GeofenceId,
                            GeofenceName = branch.GeofenceName,
                            UtcTimeZone = branch.UtcTimeZone,
                            StateId = branch.StateId,
                            StateName = branch.StateName,
                            CityId = branch.CityId,
                            CityName = branch.CityName,
                            HashedCode = branch.HashedCode
                        };
                    }

                }


            }
            catch (Exception e)
            {

                currentBranch = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return currentBranch;
        }//POST ENDS ------------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Updates a branch
        /// </summary>
        /// <param name="branhchId"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="contactPhone"></param>
        /// <param name="ordersPhone"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="descriptiveAddress"></param>
        /// <param name="locationAddress"></param>
        /// <returns></returns>
        public Branch Put(Guid id, Guid? franchiseeId, Guid? branchHolderId, Guid? branchHolderDepartmentId, string name, string postCode, string email, string contactName, string contactPhone,
            string contactEmail, string ordersPhone, bool independantOwner, decimal latitude, decimal longitude, XElement locationAddress, string descriptiveAddress, int orderTakingType, Guid? geofenceId)
        {
            Branch currentBranch = new Branch();

            try
            {


             bool? success =   this._businessObjects.StoredProcsHandler.UpdateBranch(id,
                                                                      franchiseeId,
                                                                      branchHolderId,
                                                                      branchHolderDepartmentId,
                                                                      name,
                                                                      postCode,
                                                                      email,
                                                                      contactName,
                                                                      contactPhone,
                                                                      contactEmail,
                                                                      ordersPhone,
                                                                      independantOwner,
                                                                      latitude,
                                                                      longitude,
                                                                      locationAddress,
                                                                      descriptiveAddress,
                                                                      orderTakingType,
                                                                      geofenceId);

                this._businessObjects.Context.SaveChanges();

                DefbranchesView branch;

                if (success == true)
                {
                    branch = (from x in this._businessObjects.Context.DefbranchesView
                              where x.Id == id
                              select x).FirstOrDefault();

                    if (branch != null)
                    {
                        currentBranch = new Branch
                        {
                            Id = branch.Id,
                            TenantId = branch.TenantId,
                            Type = branch.Type,
                            TypeName = this.GetTypeName(branch.Type),
                            BranchHolderId = branch.BranchHolderId,
                            BranchHolderName = branch.BranchHolderName,
                            BranchHolderContactPhoneNumber = branch.BranchHolderContactPhoneNumber,
                            BranchHolderEmail = branch.BranchHolderEmail,
                            BranchHolderActiveState = branch.BranchHolderActiveState,
                            BrachHolderDepartmentId = branch.BranchHolderDepartmentId,
                            BranchHolderDepartmentName = branch.BranchHolderDepartmentName,
                            BranchHolderDepartmentActiveState = branch.BranchHolderDepartmentActiveState,
                            Name = branch.Name,
                            PostCode = branch.PostCode,
                            Email = branch.Email,
                            ContactName = branch.ContactName,
                            ContactPhoneNumber = branch.ContactPhoneNumber,
                            ContactEmail = branch.ContactEmail,
                            OrderInquiriesPhoneNumber = branch.OrdersPhoneNumber,
                            IsActive = branch.IsActive,
                            CreatedDate = branch.CreatedDate,
                            UpdatedDate = branch.UpdatedDate,
                            Latitude = branch.Latitude,
                            Longitude = branch.Longitude,
                            LocationAddress = XElement.Parse(branch.LocationAddress),
                            DescriptiveAddress = branch.DescriptiveAddress,
                            OrderTakingType = branch.OrderTakingType,
                            GeofenceId = branch.GeofenceId,
                            GeofenceName = branch.GeofenceName,
                            UtcTimeZone = branch.UtcTimeZone,
                            StateId = branch.StateId,
                            StateName = branch.StateName,
                            CityId = branch.CityId,
                            CityName = branch.CityName,
                            HashedCode = branch.HashedCode
                        };
                    }

                }

            }
            catch (Exception e)
            {
                currentBranch = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return currentBranch;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Changes branch active state
        /// </summary>
        /// <param name="branhchId"></param>
        /// <returns></returns>
        public bool Put(Guid branchId)
        {
            bool? sucess;
            try
            {
                sucess = this._businessObjects.StoredProcsHandler.UpdateBranchActiveState(branchId);

                if (sucess == null)
                    sucess = false;

                this._businessObjects.Context.SaveChanges();
            }
            catch (Exception e)
            {
                sucess = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return (bool)sucess;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Changes branch's geofence
        /// </summary>
        /// <param name="branhchId"></param>
        /// <returns></returns>
        public bool Put(Guid branhchId, Guid holderId)
        {
            bool sucess;


            try
            {
                Defbranches branch = (from x in this._businessObjects.Context.Defbranches
                                     where x.Id == branhchId
                                     select x).FirstOrDefault();


                if (branch != null)
                {
                    branch.BranchHolderId = holderId;
                    branch.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();
                    sucess = true;
                }
                else
                    sucess = false;

            }
            catch (Exception e)
            {
                sucess = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return sucess;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Deletes a branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public bool Delete(Guid branchId)
        {
            bool success;

            try
            {

                if (this._businessObjects.StoredProcsHandler.DeleteBranch(branchId) == true)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }

            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }//METHOD DELETE ENDS --------------------------------------------------------------------------------------------------------------------------- //

        #endregion

        #region BRANCHHOLDERDISPLAYDATE

        public List<BranchHolderDisplayData> GetBranchHoldersDisplayData(Guid countryId, Guid stateId, int contentSegmentationType, int pageSize, int pageNumber)
        {
            List<BranchHolderDisplayData> displayData = null;

            var query = (dynamic)null;

            switch (contentSegmentationType)
            {
                case GeoSegmentationTypes.Country:
                    query = from x in this._businessObjects.FuncsHandler.GetBranchHoldersByCountry(countryId, pageSize, pageNumber)
                            orderby x.Relevance descending, x.BranchName ascending
                            select x;
                    break;
                case GeoSegmentationTypes.State:
                    query = from x in this._businessObjects.FuncsHandler.GetBranchHoldersByState(countryId, stateId, pageSize, pageNumber)
                            orderby x.Relevance descending, x.BranchName ascending
                            select x;
                    break;
            }

            if (query != null)
            {
                displayData = new List<BranchHolderDisplayData>();
                BranchHolderDisplayData branchHolder = null;
                foreach (TempbranchHolderDisplayContents item in query)
                {
                    branchHolder = new BranchHolderDisplayData()
                    {
                        Id = item.BranchId,
                        TenantId = item.TenantId,
                        Name = item.BranchName,
                        TenantName = item.TenantName,
                        LogoUrl = item.LogoUrl,
                        CarrouselImgUrl = item.CarrouselImgUrl
                    };

                    displayData.Add(branchHolder);
                }
            }

            return displayData;
        }

        public List<BranchHolderDisplayData> GetBranchHoldersDisplayData(Guid countryId, Guid stateId, int contentSegmentationType, double latitude, double longitude, double radius, int pageSize, int pageNumber)
        {
            List<BranchHolderDisplayData> displayData = null;

            var query = (dynamic)null;

            switch (contentSegmentationType)
            {
                case GeoSegmentationTypes.Country:

                    query = from x in this._businessObjects.FuncsHandler.GetBranchHoldersByCountryAndLocation(latitude, longitude, radius, stateId, pageSize, pageNumber)
                            orderby x.Distance, x.Relevance descending, x.BranchName ascending
                            select x;
                    break;
                case GeoSegmentationTypes.State:

                    query = from x in this._businessObjects.FuncsHandler.GetBranchHoldersByStateAndLocation(latitude, longitude, radius, countryId, stateId, pageSize, pageNumber)
                            orderby x.Distance, x.Relevance descending, x.BranchName ascending
                            select x;
                    break;
            }

            if (query != null)
            {
                displayData = new List<BranchHolderDisplayData>();
                BranchHolderDisplayData branchHolder = null;
                foreach (TempbranchHolderDisplayContents item in query)
                {
                    branchHolder = new BranchHolderDisplayData()
                    {
                        Id = item.BranchId,
                        TenantId = item.TenantId,
                        Name = item.BranchName,
                        TenantName = item.TenantName,
                        CarrouselImgUrl = item.CarrouselImgUrl,
                        LogoUrl = item.LogoUrl
                    };

                    displayData.Add(branchHolder);
                }
            }

            return displayData;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new BranchManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public BranchManager(BusinessObjects businessObjects)
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
