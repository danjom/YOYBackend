using YOY.DTO.Entities;
using YOY.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class UserInviteRelationManager
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

        public List<UserInviteRelation> Gets(string joinedUserId, int joiningBonusExpiredState, int ancestorBonusExpiredState, DateTime dateTime)
        {
            List<UserInviteRelation> inviteRelations = null;

            try
            {
                var query = (dynamic)null;

                switch (joiningBonusExpiredState)
                {
                    case ExpiredStates.All:
                        switch (ancestorBonusExpiredState)
                        {
                            case ExpiredStates.All:
                                query = from x in this._businessObjects.Context.OltpuserInviteRelations
                                        where x.JoinedUserId == joinedUserId
                                        select x;
                                break;
                            case ExpiredStates.Valid:
                                query = from x in this._businessObjects.Context.OltpuserInviteRelations
                                        where x.JoinedUserId == joinedUserId && x.AncestorBonusExpirationDate > dateTime
                                        select x;
                                break;
                            case ExpiredStates.Expired:
                                query = from x in this._businessObjects.Context.OltpuserInviteRelations
                                        where x.JoinedUserId == joinedUserId && x.AncestorBonusExpirationDate <= dateTime
                                        select x;
                                break;
                        }

                        break;
                    case ExpiredStates.Valid:
                        switch (ancestorBonusExpiredState)
                        {
                            case ExpiredStates.All:
                                query = from x in this._businessObjects.Context.OltpuserInviteRelations
                                        where x.JoinedUserId == joinedUserId && x.JoiningBonusExpirationDate > dateTime
                                        select x;
                                break;
                            case ExpiredStates.Valid:
                                query = from x in this._businessObjects.Context.OltpuserInviteRelations
                                        where x.JoinedUserId == joinedUserId && x.JoiningBonusExpirationDate > dateTime && x.AncestorBonusExpirationDate > dateTime
                                        select x;
                                break;
                            case ExpiredStates.Expired:
                                query = from x in this._businessObjects.Context.OltpuserInviteRelations
                                        where x.JoinedUserId == joinedUserId && x.JoiningBonusExpirationDate > dateTime && x.AncestorBonusExpirationDate <= dateTime
                                        select x;
                                break;
                        }
                        break;
                    case ExpiredStates.Expired:
                        switch (ancestorBonusExpiredState)
                        {
                            case ExpiredStates.All:
                                query = from x in this._businessObjects.Context.OltpuserInviteRelations
                                        where x.JoinedUserId == joinedUserId && x.JoiningBonusExpirationDate <= dateTime
                                        select x;
                                break;
                            case ExpiredStates.Valid:
                                query = from x in this._businessObjects.Context.OltpuserInviteRelations
                                        where x.JoinedUserId == joinedUserId && x.JoiningBonusExpirationDate <= dateTime && x.AncestorBonusExpirationDate > dateTime
                                        select x;
                                break;
                            case ExpiredStates.Expired:
                                query = from x in this._businessObjects.Context.OltpuserInviteRelations
                                        where x.JoinedUserId == joinedUserId && x.JoiningBonusExpirationDate <= dateTime && x.AncestorBonusExpirationDate <= dateTime
                                        select x;
                                break;
                        }
                        break;
                }

                if (query != null)
                {
                    inviteRelations = new List<UserInviteRelation>();
                    UserInviteRelation inviteRelation = null;

                    foreach (OltpuserInviteRelations item in query)
                    {
                        inviteRelation = new UserInviteRelation
                        {
                            Id = item.Id,
                            AncestorUserId = item.AncestorUserId,
                            JoinedUserId = item.JoinedUserId,
                            ParentRelationId = item.ParentRelationId,
                            HerarchyLevel = item.HerarchyLevel,
                            JoiningFirstPurchaseMoney = item.JoiningFirstPurchaseMoney,
                            JoiningFirstPurchaseMoneyGranted = item.JoiningFirstPurchaseMoneyGranted,
                            JoiningBonusCommissionMoney = item.JoiningBonusCommissionMoney,
                            RemainingBonusCommissionMoney = item.RemainingBonusCommissionMoney,
                            JoiningBonusCommissionPercentage = item.JoiningBonusCommissionPercentage,
                            AncestorFirstPurchaseMoney = item.AncestorFirstPurchaseMoney,
                            AncestorFirstPurchaseMoneyGranted = item.AncestorFirstPurchaseMoneyGranted,
                            AncestorBonusCommisionPercentage = item.AncestorBonusCommisionPercentage,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            JoiningBonusExpirationDate = item.JoiningBonusExpirationDate,
                            AncestorBonusExpirationDate = item.AncestorBonusExpirationDate
                        };
                        inviteRelations.Add(inviteRelation);
                    }
                }
            }
            catch (Exception e)
            {
                inviteRelations = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return inviteRelations;
        }

        public List<UserInviteRelation> Gets(string joinedUserId, int herarchyLevel)
        {
            List<UserInviteRelation> inviteRelations = null;

            try
            {
                var query = (dynamic)null;

                if (herarchyLevel > -1)
                {
                    query = from x in this._businessObjects.Context.OltpuserInviteRelations
                            where x.JoinedUserId == joinedUserId && x.HerarchyLevel == herarchyLevel
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpuserInviteRelations
                            where x.JoinedUserId == joinedUserId
                            select x;
                }


                if (query != null)
                {
                    inviteRelations = new List<UserInviteRelation>();
                    UserInviteRelation inviteRelation = null;

                    foreach (OltpuserInviteRelations item in query)
                    {
                        inviteRelation = new UserInviteRelation
                        {
                            Id = item.Id,
                            AncestorUserId = item.AncestorUserId,
                            JoinedUserId = item.JoinedUserId,
                            ParentRelationId = item.ParentRelationId,
                            HerarchyLevel = item.HerarchyLevel,
                            JoiningFirstPurchaseMoney = item.JoiningFirstPurchaseMoney,
                            JoiningFirstPurchaseMoneyGranted = item.JoiningFirstPurchaseMoneyGranted,
                            JoiningBonusCommissionMoney = item.JoiningBonusCommissionMoney,
                            RemainingBonusCommissionMoney = item.RemainingBonusCommissionMoney,
                            JoiningBonusCommissionPercentage = item.JoiningBonusCommissionPercentage,
                            AncestorFirstPurchaseMoney = item.AncestorFirstPurchaseMoney,
                            AncestorFirstPurchaseMoneyGranted = item.AncestorFirstPurchaseMoneyGranted,
                            AncestorBonusCommisionPercentage = item.AncestorBonusCommisionPercentage,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            JoiningBonusExpirationDate = item.JoiningBonusExpirationDate,
                            AncestorBonusExpirationDate = item.AncestorBonusExpirationDate
                        };
                        inviteRelations.Add(inviteRelation);
                    }
                }
            }
            catch (Exception e)
            {
                inviteRelations = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return inviteRelations;
        }

        public UserInviteRelation Get(Guid id)
        {
            UserInviteRelation inviteRelation = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpuserInviteRelations
                            where x.Id == id
                            select x;


                if (query != null)
                {

                    foreach (OltpuserInviteRelations item in query)
                    {
                        inviteRelation = new UserInviteRelation
                        {
                            Id = item.Id,
                            AncestorUserId = item.AncestorUserId,
                            JoinedUserId = item.JoinedUserId,
                            ParentRelationId = item.ParentRelationId,
                            HerarchyLevel = item.HerarchyLevel,
                            JoiningFirstPurchaseMoney = item.JoiningFirstPurchaseMoney,
                            JoiningFirstPurchaseMoneyGranted = item.JoiningFirstPurchaseMoneyGranted,
                            JoiningBonusCommissionMoney = item.JoiningBonusCommissionMoney,
                            RemainingBonusCommissionMoney = item.RemainingBonusCommissionMoney,
                            JoiningBonusCommissionPercentage = item.JoiningBonusCommissionPercentage,
                            AncestorFirstPurchaseMoney = item.AncestorFirstPurchaseMoney,
                            AncestorFirstPurchaseMoneyGranted = item.AncestorFirstPurchaseMoneyGranted,
                            AncestorBonusCommisionPercentage = item.AncestorBonusCommisionPercentage,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            JoiningBonusExpirationDate = item.JoiningBonusExpirationDate,
                            AncestorBonusExpirationDate = item.AncestorBonusExpirationDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                inviteRelation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return inviteRelation;
        }

        public UserInviteRelation Get(string ancestorUserId, string joinedUserId)
        {
            UserInviteRelation inviteRelation = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpuserInviteRelations
                            where x.AncestorUserId == ancestorUserId && x.JoinedUserId == joinedUserId
                            select x;


                if (query != null)
                {

                    foreach (OltpuserInviteRelations item in query)
                    {
                        inviteRelation = new UserInviteRelation
                        {
                            Id = item.Id,
                            AncestorUserId = item.AncestorUserId,
                            JoinedUserId = item.JoinedUserId,
                            ParentRelationId = item.ParentRelationId,
                            HerarchyLevel = item.HerarchyLevel,
                            JoiningFirstPurchaseMoney = item.JoiningFirstPurchaseMoney,
                            JoiningFirstPurchaseMoneyGranted = item.JoiningFirstPurchaseMoneyGranted,
                            JoiningBonusCommissionMoney = item.JoiningBonusCommissionMoney,
                            RemainingBonusCommissionMoney = item.RemainingBonusCommissionMoney,
                            JoiningBonusCommissionPercentage = item.JoiningBonusCommissionPercentage,
                            AncestorFirstPurchaseMoney = item.AncestorFirstPurchaseMoney,
                            AncestorFirstPurchaseMoneyGranted = item.AncestorFirstPurchaseMoneyGranted,
                            AncestorBonusCommisionPercentage = item.AncestorBonusCommisionPercentage,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            JoiningBonusExpirationDate = item.JoiningBonusExpirationDate,
                            AncestorBonusExpirationDate = item.AncestorBonusExpirationDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                inviteRelation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return inviteRelation;
        }

        public UserInviteRelation Post(string ancestorUserId, string joinedUserId, Guid? parentRelationId, int herarchyLevel, decimal joiningFirstPurchaseMoney,
            decimal joiningBonusCommissionMoney, double joiningBonusCommissionPercentage, decimal ancestorFirstPurchaseMoney, double ancestorBonusCommisionPercentage, int joiningBonusMonths, int? ancestorBonusMonths)
        {
            UserInviteRelation inviteRelation;
            try
            {
                OltpuserInviteRelations newInviteRelation = new OltpuserInviteRelations
                {
                    Id = Guid.NewGuid(),
                    AncestorUserId = ancestorUserId,
                    JoinedUserId = joinedUserId,
                    ParentRelationId = parentRelationId,
                    HerarchyLevel = herarchyLevel,
                    JoiningFirstPurchaseMoney = joiningFirstPurchaseMoney,
                    JoiningFirstPurchaseMoneyGranted = false,//At this point user hasn't made a purchase
                    JoiningBonusCommissionMoney = joiningBonusCommissionMoney,
                    RemainingBonusCommissionMoney = joiningBonusCommissionMoney,
                    JoiningBonusCommissionPercentage = joiningBonusCommissionPercentage,
                    AncestorFirstPurchaseMoney = ancestorFirstPurchaseMoney,
                    AncestorFirstPurchaseMoneyGranted = false,//At this point user hasn't made a purchase
                    AncestorBonusCommisionPercentage = ancestorBonusCommisionPercentage,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    JoiningBonusExpirationDate = DateTime.UtcNow.AddMonths(joiningBonusMonths),
                    AncestorBonusExpirationDate = null
                };

                if (ancestorBonusMonths != null && ancestorBonusMonths > -1)
                {
                    newInviteRelation.AncestorBonusExpirationDate = DateTime.UtcNow.AddMonths((int)ancestorBonusMonths);
                }

                this._businessObjects.Context.OltpuserInviteRelations.Add(newInviteRelation);
                this._businessObjects.Context.SaveChanges();

                inviteRelation = new UserInviteRelation
                {
                    Id = newInviteRelation.Id,
                    AncestorUserId = newInviteRelation.AncestorUserId,
                    JoinedUserId = newInviteRelation.JoinedUserId,
                    ParentRelationId = newInviteRelation.ParentRelationId,
                    HerarchyLevel = newInviteRelation.HerarchyLevel,
                    JoiningFirstPurchaseMoney = newInviteRelation.JoiningFirstPurchaseMoney,
                    JoiningFirstPurchaseMoneyGranted = newInviteRelation.JoiningFirstPurchaseMoneyGranted,
                    JoiningBonusCommissionMoney = newInviteRelation.JoiningBonusCommissionMoney,
                    RemainingBonusCommissionMoney = newInviteRelation.RemainingBonusCommissionMoney,
                    JoiningBonusCommissionPercentage = newInviteRelation.JoiningBonusCommissionPercentage,
                    AncestorFirstPurchaseMoney = newInviteRelation.AncestorFirstPurchaseMoney,
                    AncestorFirstPurchaseMoneyGranted = newInviteRelation.AncestorFirstPurchaseMoneyGranted,
                    AncestorBonusCommisionPercentage = newInviteRelation.AncestorBonusCommisionPercentage,
                    CreatedDate = newInviteRelation.CreatedDate,
                    UpdatedDate = newInviteRelation.UpdatedDate,
                    JoiningBonusExpirationDate = newInviteRelation.JoiningBonusExpirationDate,
                    AncestorBonusExpirationDate = newInviteRelation.AncestorBonusExpirationDate
                };
            }
            catch (Exception e)
            {
                inviteRelation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return inviteRelation;
        }

        public bool Put(Guid id, bool joiningFirstPurchaseMoneyGranted, bool ancestorFirstPurchaseMoneyGranted, decimal usedJoiningBonusComissionMoney)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpuserInviteRelations
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltpuserInviteRelations inviteRelation = null;

                    foreach (OltpuserInviteRelations item in query)
                    {
                        inviteRelation = item;
                    }

                    if (inviteRelation != null)
                    {
                        inviteRelation.JoiningFirstPurchaseMoneyGranted = joiningFirstPurchaseMoneyGranted;
                        inviteRelation.AncestorFirstPurchaseMoneyGranted = ancestorFirstPurchaseMoneyGranted;
                        inviteRelation.RemainingBonusCommissionMoney -= usedJoiningBonusComissionMoney;
                        inviteRelation.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
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
        public UserInviteRelationManager(BusinessObjects businessObjects)
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
