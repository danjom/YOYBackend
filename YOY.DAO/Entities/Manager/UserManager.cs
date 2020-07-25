using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.User;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class UserManager
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

        private string GetGenderName(string gender)
        {
            string genderName = (gender.ElementAt(0)) switch
            {
                'M' => Resources.Male,
                'F' => Resources.Female,
                '-' => Resources.NotSpecified,
                _ => Resources.Other,
            };
            return genderName;
        }

        /// <summary>
        /// Retrieve all Users
        /// </summary>
        /// <returns></returns>
        public List<UserData> Gets(int activeState)
        {
            List<UserData> users = new List<UserData>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = from x in this._businessObjects.Context.UserDataView
                                select x;
                        break;
                    case ActiveStates.Inactive:
                        query = from x in this._businessObjects.Context.UserDataView
                                where x.LockoutEnabled
                                select x;
                        break;
                    case ActiveStates.Active:
                        query = from x in this._businessObjects.Context.UserDataView
                                where !x.LockoutEnabled
                                select x;
                        break;
                }

                if (query != null)
                {
                    UserData user;
                    foreach (UserDataView item in query)
                    {
                        user = new UserData
                        {
                            Id = item.Id,
                            AccountNumber = item.AccountNumber,
                            UserName = item.UserName,
                            NormalizedUserName = item.NormalizedUserName,
                            Name = item.Name,
                            DateOfBirth = item.DateOfBirth,
                            Email = item.Email,
                            NormalizedEmail = item.NormalizedEmail,
                            EmailConfirmed = item.EmailConfirmed,
                            CountryPhonePrefix = item.CountryPhonePrefix,
                            PhoneNumber = item.PhoneNumber,
                            PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                            Gender = item.Gender,
                            CreatedDate = item.CreatedDate,
                            MaxDailyAlerts = item.MaxDailyNotifications,
                            LastAppOpen = item.LastAppOpen,
                            LastRedemption = item.LastOfferRedemption,
                            AccountCode = item.AccountCode,
                            ReferenceCode = item.ReferenceCode,
                            StateId = item.StateId,
                            StateName = "",
                            CountryId = null,
                            CountryName = "",
                            UtcTimeDiff = item.StateId != null ? (int)item.StateUtcTimeZone : int.MinValue
                        };

                        if(user.StateId != null)
                        {
                            user.StateName = item.StateName;
                            user.CountryId = item.CountryId;
                            user.CountryName = item.CountryName;
                        }

                        if (!string.IsNullOrWhiteSpace(user.Gender))
                        {
                            user.GenderName = GetGenderName(user.Gender);
                        }

                        users.Add(user);
                    }
                }
                
            }
            catch (Exception e)
            {
                users = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return users;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        public UserData Get(string userKey, int type)
        {
            UserData user = null;

            try
            {
                var query = (dynamic) null;

                switch (type)
                {
                    case UserKeys.Username:
                        query = from x in this._businessObjects.Context.UserDataView
                                where x.UserName == userKey
                                select x;
                        break;
                    case UserKeys.UserId:
                        query = from x in this._businessObjects.Context.UserDataView
                                where x.Id == userKey
                                select x;
                        break;
                    case UserKeys.AccountCode:
                        query = from x in this._businessObjects.Context.UserDataView
                                where x.AccountCode == userKey
                                select x;
                        break;
                    case UserKeys.AppleUserId:
                        query = from x in this._businessObjects.Context.UserDataView
                                where x.AppleId == userKey
                                select x;
                        break;
                }


                
                foreach (UserDataView item in query)
                {
                    user = new UserData
                    {
                        Id = item.Id,
                        AccountNumber = item.AccountNumber,
                        UserName = item.UserName,
                        NormalizedUserName = item.UserName,
                        Name = item.Name,
                        DateOfBirth = item.DateOfBirth,
                        Email = item.Email,
                        NormalizedEmail = item.Email,
                        EmailConfirmed = item.EmailConfirmed,
                        CountryPhonePrefix = item.CountryPhonePrefix,
                        PhoneNumber = item.PhoneNumber,
                        PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                        Gender = item.Gender,
                        CreatedDate = DateTime.UtcNow,
                        MaxDailyAlerts = -1,
                        LastAppOpen = item.LastAppOpen,
                        LastRedemption = item.LastOfferRedemption,
                        AccountCode = item.AccountCode,
                        ReferenceCode = item.ReferenceCode,
                        StateId = item.StateId,
                        StateName = "",
                        CountryId = null,
                        CountryName = "",
                        UtcTimeDiff = item.StateId != null ? (int)item.StateUtcTimeZone : int.MinValue
                    };

                    if (user.StateId != null)
                    {
                        user.StateName = item.StateName;
                        user.CountryId = item.CountryId;
                        user.CountryName = item.CountryName;
                    }

                    if (!string.IsNullOrWhiteSpace(user.Gender))
                    {
                        user.GenderName = GetGenderName(user.Gender);
                    }
                    
                }
            }
            catch (Exception e)
            {
                user = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }


            return user;
        }


        public UserData Get(long accountNumber)
        {
            UserData user = null;

            try
            {
                var query = from x in this._businessObjects.Context.UserDataView
                            where x.AccountNumber == accountNumber
                            select x;


                foreach (UserDataView item in query)
                {

                    user = new UserData
                    {
                        Id = item.Id,
                        AccountNumber = item.AccountNumber,
                        UserName = item.UserName,
                        NormalizedUserName = item.NormalizedUserName,
                        Name = item.Name,
                        DateOfBirth = item.DateOfBirth,
                        Email = item.Email,
                        NormalizedEmail = item.NormalizedEmail,
                        EmailConfirmed = item.EmailConfirmed,
                        CountryPhonePrefix = item.CountryPhonePrefix,
                        PhoneNumber = item.PhoneNumber,
                        PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                        Gender = item.Gender,
                        CreatedDate = item.CreatedDate,
                        MaxDailyAlerts = item.MaxDailyNotifications,
                        LastAppOpen = item.LastAppOpen,
                        LastRedemption = item.LastOfferRedemption,
                        AccountCode = item.AccountCode,
                        ReferenceCode = item.ReferenceCode,
                        StateId = item.StateId,
                        StateName = "",
                        CountryId = null,
                        CountryName = "",
                        UtcTimeDiff = item.StateId != null ? (int)item.StateUtcTimeZone : int.MinValue
                    };

                    if (user.StateId != null)
                    {
                        user.StateName = item.StateName;
                        user.CountryId = item.CountryId;
                        user.CountryName = item.CountryName;
                    }

                    if (!string.IsNullOrWhiteSpace(user.Gender))
                    {
                        user.GenderName = GetGenderName(user.Gender);
                    }

                }
            }
            catch (Exception e)
            {
                user = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return user;
        }

        public bool Put(string userId,bool confirmed, int type)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.AspNetUsers
                    where x.Id == userId
                    select x;

                AspNetUsers user = null;
                foreach (AspNetUsers item in query)
                {
                    user = item;
                }

                if (user != null)
                {
                    switch (type)
                    {
                        case ConfirmationTypes.Email:
                            user.EmailConfirmed = confirmed;
                            break;
                        case ConfirmationTypes.MobilePhone:
                            user.PhoneNumberConfirmed = confirmed;
                            break;
                    }

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
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        public UserData Put(string userId, string name, string lastname, DateTime? dateOfBirth, string phoneNumber, string gender, int maxDailyAlerts)
        {
            UserData user = null;

            try
            {
                var query = from x in this._businessObjects.Context.AspNetUsers
                    where x.Id == userId
                    select x;

                AspNetUsers currentUser = null;
                foreach (AspNetUsers item in query)
                {
                    currentUser = item;
                }

                if (currentUser != null)
                {
                    currentUser.Name = name;
                    currentUser.DateOfBirth = dateOfBirth;
                    currentUser.Gender = gender;
                    currentUser.MaxDailyNotifications = maxDailyAlerts;
                    
                    if (currentUser.PhoneNumber != phoneNumber)
                    {
                        currentUser.PhoneNumberConfirmed = false;
                    }
                    currentUser.PhoneNumber = phoneNumber;

                    this._businessObjects.Context.SaveChanges();

                    user = new UserData
                    {
                        Id = currentUser.Id,
                        AccountNumber = currentUser.AccountNumber,
                        UserName = currentUser.UserName,
                        NormalizedUserName = currentUser.NormalizedUserName,
                        Name = currentUser.Name,
                        DateOfBirth = currentUser.DateOfBirth,
                        Email = currentUser.Email,
                        NormalizedEmail = currentUser.NormalizedEmail,
                        EmailConfirmed = currentUser.EmailConfirmed,
                        CountryPhonePrefix = currentUser.CountryPhonePrefix,
                        PhoneNumber = currentUser.PhoneNumber,
                        PhoneNumberConfirmed = currentUser.PhoneNumberConfirmed,
                        Gender = currentUser.Gender,
                        CreatedDate = currentUser.CreatedDate,
                        MaxDailyAlerts = currentUser.MaxDailyNotifications,
                        LastAppOpen = currentUser.LastAppOpen,
                        LastRedemption = currentUser.LastOfferRedemption,
                        AccountCode = currentUser.AccountCode,
                        ReferenceCode = currentUser.ReferenceCode,
                        StateId = currentUser.StateId,
                        StateName = "",
                        CountryId = null,
                        CountryName = "",
                        UtcTimeDiff = currentUser.StateId != null ? currentUser.State.UtcTimeZone : int.MinValue
                    };

                    if (user.StateId != null)
                    {
                        user.StateName = currentUser.State.Name;
                        user.CountryId = currentUser.State.CountryId;
                        user.CountryName = currentUser.State.Country.Name;
                    }

                    if (!string.IsNullOrWhiteSpace(user.Gender))
                    {
                        user.GenderName = GetGenderName(user.Gender);
                    }
                }
            }
            catch (Exception e)
            {
                user = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return user;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Updates the user name, this also updates the email and sets that the email is not confirmed
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool Put(string userId, string email)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.AspNetUsers
                            where x.Id == userId
                            select x;

                AspNetUsers currentUser = null;

                foreach (AspNetUsers item in query)
                {
                    currentUser = item;
                }

                if (currentUser != null)
                {
                    currentUser.Email = email;
                    currentUser.UserName = email;
                    currentUser.EmailConfirmed = false;

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
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Updates the user name, this also updates the email and sets that the email is not confirmed
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="refCode"></param>
        /// <returns></returns>
        public bool Put(long accountNumber, int fieldType, string newValue)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.AspNetUsers
                            where x.AccountNumber == accountNumber
                            select x;

                AspNetUsers currentUser = null;

                foreach (var item in query)
                {
                    currentUser = item;
                }

                if (currentUser != null)
                {
                    switch (fieldType)
                    {
                        case UserProfileFieldTypes.InvitorReferenceCode:

                            if (string.IsNullOrWhiteSpace(currentUser.ReferenceCode))
                            {
                                currentUser.ReferenceCode = newValue;
                            }

                            break;
                        case UserProfileFieldTypes.PhoneNumber:

                            currentUser.PhoneNumber = newValue;
                            currentUser.PhoneNumberConfirmed = true;

                            break;
                        case UserProfileFieldTypes.State:

                            currentUser.StateId = new Guid(newValue);

                            break;
                        case UserProfileFieldTypes.ProfilePic:

                            currentUser.ProfilePicUrl = newValue;

                            break;
                        case UserProfileFieldTypes.Language:

                            currentUser.Language = newValue;

                            break;
                    }

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
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Updates user data of birth
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public bool Put(string userId, DateTime dateOfBirth)
        {
            bool success = false;

            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = DateTime.Now - dateOfBirth;

            // Because we start at year 1 for the Gregorian
            // calendar, we must subtract a year here.
            int years = (zeroTime + span).Year - 1;

            //No user will have more than 100 years
            if (years <= 100)
            {
                try
                {
                    var query = from x in this._businessObjects.Context.AspNetUsers
                                where x.Id == userId
                                select x;

                    AspNetUsers currentUser = null;

                    foreach (var item in query)
                    {
                        currentUser = item;
                    }

                    if (currentUser != null)
                    {
                        currentUser.DateOfBirth = dateOfBirth;

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
            }

            return success;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Updates user activity dates
        /// </summary>
        /// <param name="userSk"></param>
        /// <param name="userSkType"></param>
        /// <param name="newDate"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public bool Put(string userSk, int userSkType, DateTime newDate, int eventType)
        {
            bool success = false;

            try
            {
                var query = (dynamic) null;

                switch (userSkType)
                {
                    case UserKeys.UserId:
                        query = from x in this._businessObjects.Context.AspNetUsers
                                where x.Id == userSk
                                select x;
                        break;
                    case UserKeys.Username:
                        query = from x in this._businessObjects.Context.AspNetUsers
                                where x.UserName == userSk
                                select x;
                        break;
                }
                

                AspNetUsers currentUser = null;

                foreach (var item in query)
                {
                    currentUser = item;
                }

                if (currentUser != null)
                {
                    switch (eventType)
                    {
                        case UserEventTypes.AppOpen:
                            currentUser.LastAppOpen = newDate;
                            break;
                        case UserEventTypes.OfferRedemption:
                            currentUser.LastOfferRedemption = newDate;
                            break;
                    }

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
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Updates the user's surrogate key
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newUserSk"></param>
        /// <param name="userSkType"></param>
        /// <returns></returns>
        public bool Put(string email, string newUserSk, int userSkType)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.AspNetUsers
                            where x.Email == email
                            select x;

                AspNetUsers currentUser = null;

                foreach (var item in query)
                {
                    currentUser = item;
                }

                if (currentUser != null)
                {
                    switch (userSkType)
                    {
                        case UserIdTypes.FBId:
                            currentUser.Fbid = newUserSk;
                            break;
                    }
                    

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
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        public bool AddAccountCode(string userId, string code)
        {
            bool success = false;

            try
            {
                bool? added = this._businessObjects.StoredProcsHandler.AddAccountCode(userId, code);

                if(added == true)
                {
                    success = true;
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

        public bool CheckPhoneNumberUniqueness(string phoneNumber, string countryPhonePrefix)
        {
            bool success = false;

            try
            {
                int phoneUniqueness = this._businessObjects.StoredProcsHandler.CheckPhoneNumberUniqueness(phoneNumber, countryPhonePrefix);

                if (phoneUniqueness == 0)
                {
                    success = true;
                }
                success = true;//NEEDS TO BE REMOVED!!!
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

        #region MISCELLANEOUS

        public UserDataForToken Get(string username)
        {
            UserDataForToken dataForTokenView = null;

            try
            {
                var query = from x in this._businessObjects.Context.UserDataForTokenView
                            where x.UserName == username
                            select x;

                if(query != null)
                {
                    foreach(UserDataForTokenView item in query)
                    {
                        dataForTokenView = new UserDataForToken
                        {
                            UserId = item.Id,
                            ProfilePic = item.ProfilePicUrl,
                            AccountNumber = item.AccountNumber,
                            AccountCode = item.AccountCode,
                            UserName = item.UserName,
                            Email = item.Email,
                            EmailConfirmed = item.EmailConfirmed,
                            Name = item.Name,
                            Gender = item.Gender,
                            DateOfBirth = item.DateOfBirth,
                            Language = item.Language,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            StateUtcTimeZone = item.StateUtcTimeZone,
                            CountryId = item.CountryId,
                            CountryCode = item.CountryCode,
                            CountryFlag = item.CountryFlag,
                            CountryName = item.CountryName,
                            CurrencySymbol = item.CurrencySymbol,
                            CurrencyType = item.CurrencyType,
                            MembershipId = item.MembershipId,
                            MembershipLevel = item.MembershipLevel,
                            UserPoints = item.UsedPoints,
                            AvailablePoints = item.AvailablePoints ?? 0,
                            LastestAndroidVersion = item.LastestAndroidVersion,
                            LastestiOSVersion = item.LastestiOsversion,
                            LastestFBVersion = item.LastestFbversion
                        };
                    }
                }
            }
            catch(Exception e)
            {
                dataForTokenView = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return dataForTokenView;
        }

        public UserWithLocationAndMembershipData Get(string id, bool nothing)
        {
            UserWithLocationAndMembershipData userWithLocation = null;

            try
            {
                var query = from x in this._businessObjects.Context.UserWithLocationAndMembershipDataView
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (UserWithLocationAndMembershipDataView item in query)
                    {
                        userWithLocation = new UserWithLocationAndMembershipData
                        {
                            Id = item.Id,
                            ProfilePic = item.ProfilePicUrl,
                            AccountNumber = item.AccountNumber,
                            AccountCode = item.AccountCode,
                            Username = item.UserName,
                            Email = item.Email,
                            EmailConfirmed = item.EmailConfirmed,
                            CountryPhonePrefix = item.CountryPhonePrefix,
                            PhoneNumber = item.PhoneNumber,
                            PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                            Name = item.Name,
                            Gender = item.Gender,
                            DateOfBirth = item.DateOfBirth,
                            Language = item.Language,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            StateUtcTimeZone = item.StateUtcTimeZone,
                            StateActiveState = item.StateActiveState,
                            StateOperationState = item.StateOperationState,
                            StateNearestNeighbor = item.StateNearestNeighbor,
                            CountryId = item.CountryId,
                            CountryFlag = item.CountryFlag,
                            CountryName = item.CountryName,
                            CountryCode = item.CountryCode,
                            CurrencySymbol = item.CountryCurrencySymbol,
                            CurrencyType = item.CountryCurrencyType,
                            ContentSegmentationType = item.ContentSegmentationType,
                            MembershipId = item.MembershipId,
                            MembershipLevel = item.MembershipLevel,
                            UsedPoints = item.UsedPoints,
                            AvailablePoints = item.AvailablePoints
                        };
                    }
                }
            }
            catch (Exception e)
            {
                userWithLocation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return userWithLocation;
        }

        public UserWithLocationAndMembershipData Get(string userKey, int keyType, int nothing)
        {
            UserWithLocationAndMembershipData userWithLocation = null;

            try
            {
                var query = (dynamic)null;

                switch (keyType)
                {
                    case UserKeys.UserId:

                        query = from x in this._businessObjects.Context.UserWithLocationAndMembershipDataView
                                where x.Id == userKey
                                select x;
                        break;
                    case UserKeys.Username:

                        query = from x in this._businessObjects.Context.UserWithLocationAndMembershipDataView
                                where x.Email == userKey
                                select x;
                        break;
                }

                if (query != null)
                {
                    foreach (UserWithLocationAndMembershipDataView item in query)
                    {
                        userWithLocation = new UserWithLocationAndMembershipData
                        {
                            Id = item.Id,
                            AccountNumber = item.AccountNumber,
                            AccountCode = item.AccountCode,
                            Username = item.UserName,
                            CountryPhonePrefix = item.CountryPhonePrefix,
                            PhoneNumber = item.PhoneNumber,
                            PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                            Email = item.Email,
                            EmailConfirmed = item.EmailConfirmed,
                            Name = item.Name,
                            Gender = item.Gender,
                            DateOfBirth = item.DateOfBirth,
                            Language = item.Language,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            StateUtcTimeZone = item.StateUtcTimeZone,
                            StateActiveState = item.StateActiveState,
                            StateOperationState = item.StateOperationState,
                            StateNearestNeighbor = item.StateNearestNeighbor,
                            CountryId = item.CountryId,
                            CountryFlag = item.CountryFlag,
                            CountryName = item.CountryName,
                            CountryCode = item.CountryCode,
                            CurrencySymbol = item.CountryCurrencySymbol,
                            CurrencyType = item.CountryCurrencyType,
                            ContentSegmentationType = item.ContentSegmentationType,
                            MembershipId = item.MembershipId,
                            MembershipLevel = item.MembershipLevel,
                            UsedPoints = item.UsedPoints,
                            AvailablePoints = item.AvailablePoints
                        };

                        if (!string.IsNullOrWhiteSpace(item.Gender))
                        {
                            userWithLocation.GenderName = GetGenderName(item.Gender);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                userWithLocation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return userWithLocation;
        }

        #endregion


        #region REWARDEDUSERS



        public int Get()
        {
            int count = 0;

            try
            {
                count = (from x in this._businessObjects.Context.OltprewardedUsers
                         where x.GotDonut == true
                         select x).Count();
            }
            catch (Exception e)
            {
                count = -1;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");


            }

            return count;
        }

        public bool Get(long acc, string userId)
        {
            bool valid = true;

            try
            {

                var query = from x in this._businessObjects.Context.OltprewardedUsers
                            where x.AccountNumber == acc && x.UserId == userId && x.GotDonut == true
                            select x;

                if(query != null)
                {
                    foreach(OltprewardedUsers item in query)
                    {
                        valid = false;
                    }
                }
            }
            catch (Exception e)
            {
                valid = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return valid;
        }

        public bool Post(long acc, string userId, bool gotBonus, bool gotDonut, string code)
        {
            bool success;
            try
            {
                OltprewardedUsers rewardedUser = new OltprewardedUsers
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = acc,
                    UserId = userId,
                    GotBonus = gotBonus,
                    GotDonut = gotDonut,
                    Code = code,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltprewardedUsers.Add(rewardedUser);
                this._businessObjects.Context.SaveChanges();

                success = true;
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
        /// 
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public UserManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD PRODUCT PREFERENCE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
