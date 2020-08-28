using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;

namespace YOY.DAO.Entities.Manager
{
    public class UserDisplayedContentManager
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

        public List<UserDisplayedContent> Gets(string userId, Guid? ownerId, int targetScreen, DateTime dateTime)
        {
            List<UserDisplayedContent> displayedContents = null;

            try
            {
                var query = (dynamic)null;

                if(ownerId != null)
                {
                    query = from x in this._businessObjects.Context.OltpuserDisplayedContents
                            where x.UserId == userId && x.TargetScreen == targetScreen && x.OwnerId == ownerId && x.CreatedDate > dateTime
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpuserDisplayedContents
                            where x.UserId == userId && x.TargetScreen == targetScreen && x.CreatedDate > dateTime
                            select x;
                }

                if(query != null)
                {
                    UserDisplayedContent displayedContent;
                    displayedContents = new List<UserDisplayedContent>();

                    foreach(OltpuserDisplayedContents item in query)
                    {
                        displayedContent = new UserDisplayedContent
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            OwnerId = item.OwnerId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            TargetScreen = item.TargetScreen,
                            CreatedDate = item.CreatedDate
                        };

                        displayedContents.Add(displayedContent);
                    }
                }

            }
            catch(Exception e)
            {
                displayedContents = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return displayedContents;
        }

        public bool Post(List<UserDisplayedContent> userDisplayedContents)
        {
            bool success;

            try
            {
                OltpuserDisplayedContents displayedContent;
                List<OltpuserDisplayedContents> displayedContents = new List<OltpuserDisplayedContents>();

                foreach(UserDisplayedContent item in userDisplayedContents)
                {
                    displayedContent = new OltpuserDisplayedContents
                    {
                        Id = Guid.NewGuid(),
                        UserId = item.UserId,
                        OwnerId = item.OwnerId,
                        ReferenceId = item.ReferenceId,
                        ReferenceType = item.ReferenceType,
                        TargetScreen = item.TargetScreen,
                        CreatedDate = DateTime.UtcNow
                    };

                    displayedContents.Add(displayedContent);
                }

                this._businessObjects.Context.OltpuserDisplayedContents.AddRange(displayedContents);
                this._businessObjects.Context.SaveChanges();

                success = true;
            }
            catch(Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Delete(DateTime dateTime)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpuserDisplayedContents
                            where x.CreatedDate < dateTime
                            select x;

                if(query != null)
                {
                    this._businessObjects.Context.OltpuserDisplayedContents.RemoveRange(query);
                    this._businessObjects.Context.SaveChanges();

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

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new OfferPreferenceManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public UserDisplayedContentManager(BusinessObjects businessObjects)
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
