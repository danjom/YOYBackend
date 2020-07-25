using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class CheckInManager
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

        private string GetTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case CheckInTypes.YOYWalkIn:
                    typeName = Resources.YOYWalkInCheckIn;
                    break;
                case CheckInTypes.LoyaltyClubWalkIn:
                    typeName = Resources.LoyaltyClubWalkInCheckIn;
                    break;
                case CheckInTypes.LoyaltyClubCashier:
                    typeName = Resources.LoyaltyClubCashierCheckIn;
                    break;
            }

            return typeName;
        }

        private string GetPointsAppliedTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case CheckInPointsAppliedTypes.YOY:
                    typeName = Resources.YOYPointsApplied;
                    break;
                case CheckInPointsAppliedTypes.Commerce:
                    typeName = Resources.CommercePointsApplied;
                    break;
                case CheckInPointsAppliedTypes.SponsoredCommerce:
                    typeName = Resources.SponsoredCommercePointsApplied;
                    break;
            }

            return typeName;
        }

        public List<CheckIn> Gets(object refId, int refType, int checkInType, int pointsAppliedType, int activeState, int pageNumber, int pageSize)
        {
            List<CheckIn> checkIns = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        switch (refType)
                        {
                            case CheckInRefTypes.All:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.TenantId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.TenantId == (Guid)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantId == (Guid)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantId == (Guid)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BranchId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.BranchId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.BranchId == (Guid)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BranchId == (Guid)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BranchId == (Guid)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.UserId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.UserId == (string)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.UserId == (string)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.UserId == (string)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.UserId == (string)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BroadcasterId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.BroadcasterId == (Guid)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BroadcasterId == (Guid)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BroadcasterId == (Guid)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantIdPointsGranter:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantIdPointsGranter == (Guid)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantIdPointsGranter == (Guid)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                        }

                        break;
                    case ActiveStates.Active:

                        switch (refType)
                        {
                            case CheckInRefTypes.All:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.TenantId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.TenantId == (Guid)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantId == (Guid)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantId == (Guid)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BranchId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.BranchId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.BranchId == (Guid)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BranchId == (Guid)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BranchId == (Guid)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.UserId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.UserId == (string)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.UserId == (string)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.UserId == (string)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.UserId == (string)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BroadcasterId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.BroadcasterId == (Guid)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BroadcasterId == (Guid)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BroadcasterId == (Guid)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantIdPointsGranter:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.PointsAppliedType == pointsAppliedType
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantIdPointsGranter == (Guid)refId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                        }

                        break;
                }

                if (query != null)
                {
                    CheckIn checkIn = null;
                    checkIns = new List<CheckIn>();

                    foreach (OltpcheckInsView item in query)
                    {
                        checkIn = new CheckIn
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            BranchId = item.BranchId,
                            TenantId = item.TenantId,
                            TenantIdPointsGranter = item.TenantIdPointsGranter,
                            BroadcasterId = item.BroadcasterId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            PointsAppliedType = item.PointsAppliedType,
                            PointsAppliedTypeName = GetPointsAppliedTypeName(item.PointsAppliedType),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate,
                            IsActive = item.IsActive,
                            EarnedPoints = item.EarnedPoints,
                            UsedForRewardClaim = item.UsedForRewardClaim,
                            BranchName = item.BranchName,
                            TenantName = item.TenantName,
                            BroadcasterName = item.BroadcasterName,
                            AccountNumber = item.AccountNumber,
                            Username = item.UserName,
                            UserName = item.Name
                        };

                        checkIns.Add(checkIn);
                    }
                }

            }
            catch (Exception e)
            {
                checkIns = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkIns;
        }

        public List<CheckIn> Gets(object refId, int refType, int checkInType, int pointsAppliedType, int activeState, DateTime minDate, int pageSize, int pageNumber)
        {
            List<CheckIn> checkIns = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        switch (refType)
                        {
                            case CheckInRefTypes.All:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.TenantId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.TenantId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantId == (Guid)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BranchId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.BranchId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.BranchId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BranchId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BranchId == (Guid)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.UserId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.UserId == (string)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.UserId == (string)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.UserId == (string)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.UserId == (string)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BroadcasterId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BroadcasterId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BroadcasterId == (Guid)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantIdPointsGranter:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantIdPointsGranter == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantIdPointsGranter == (Guid)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                        }

                        break;
                    case ActiveStates.Active:

                        switch (refType)
                        {
                            case CheckInRefTypes.All:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.TenantId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.TenantId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantId == (Guid)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BranchId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.BranchId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.BranchId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BranchId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BranchId == (Guid)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.UserId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.UserId == (string)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.UserId == (string)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.UserId == (string)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.UserId == (string)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BroadcasterId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BroadcasterId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BroadcasterId == (Guid)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantIdPointsGranter:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                 where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageSize).Take(pageSize);
                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                        }
                                    }
                                }

                                break;
                        }

                        break;
                }

                if (query != null)
                {
                    CheckIn checkIn = null;
                    checkIns = new List<CheckIn>();

                    foreach (OltpcheckInsView item in query)
                    {
                        checkIn = new CheckIn
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            BranchId = item.BranchId,
                            TenantId = item.TenantId,
                            TenantIdPointsGranter = item.TenantIdPointsGranter,
                            BroadcasterId = item.BroadcasterId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            PointsAppliedType = item.PointsAppliedType,
                            PointsAppliedTypeName = GetPointsAppliedTypeName(item.PointsAppliedType),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate,
                            IsActive = item.IsActive,
                            EarnedPoints = item.EarnedPoints,
                            UsedForRewardClaim = item.UsedForRewardClaim,
                            BranchName = item.BranchName,
                            TenantName = item.TenantName,
                            BroadcasterName = item.BroadcasterName,
                            AccountNumber = item.AccountNumber,
                            Username = item.UserName,
                            UserName = item.Name
                        };

                        checkIns.Add(checkIn);
                    }
                }

            }
            catch (Exception e)
            {
                checkIns = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkIns;
        }

        public List<CheckIn> Gets(object refId, int refType, int checkInType, int pointsAppliedType, int activeState, int chronologicalOrder, DateTime minDate, int pageSize, int pageNumber)
        {
            List<CheckIn> checkIns = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:

                        switch (refType)
                        {
                            case CheckInRefTypes.All:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.TenantId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.TenantId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.TenantId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.TenantId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.TenantId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.TenantId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BranchId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BranchId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BranchId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.BranchId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.BranchId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.BranchId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.BranchId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.BranchId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.BranchId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.UserId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.UserId == (string)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.UserId == (string)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.UserId == (string)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.UserId == (string)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.UserId == (string)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.UserId == (string)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.UserId == (string)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.UserId == (string)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BroadcasterId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.BroadcasterId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.BroadcasterId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.BroadcasterId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.BroadcasterId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantIdPointsGranter:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.TenantIdPointsGranter == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.TenantIdPointsGranter == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.TenantIdPointsGranter == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.TenantIdPointsGranter == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                        }

                        break;
                    case ActiveStates.Active:

                        switch (refType)
                        {
                            case CheckInRefTypes.All:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.TenantId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.TenantId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.TenantId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.TenantId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.TenantId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.TenantId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BranchId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BranchId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BranchId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.BranchId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.BranchId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.BranchId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.BranchId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.BranchId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.BranchId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.UserId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.UserId == (string)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.UserId == (string)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.UserId == (string)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.UserId == (string)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.UserId == (string)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.UserId == (string)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.UserId == (string)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.UserId == (string)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.BroadcasterId:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.BroadcasterId == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.BroadcasterId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.BroadcasterId == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.BroadcasterId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.BroadcasterId == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                            case CheckInRefTypes.TenantIdPointsGranter:
                                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                {
                                    switch (chronologicalOrder)
                                    {
                                        case ChronologicalOrders.Descending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                        case ChronologicalOrders.Ascending:
                                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                     where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                     orderby x.CreatedDate ascending
                                                     select x).Skip(pageSize * pageSize).Take(pageSize);
                                            break;
                                    }

                                }
                                else
                                {
                                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                                    {
                                        switch (chronologicalOrder)
                                        {
                                            case ChronologicalOrders.Descending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                            case ChronologicalOrders.Ascending:
                                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                         where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.Type == checkInType && x.CreatedDate >= minDate
                                                         orderby x.CreatedDate ascending
                                                         select x).Skip(pageSize * pageSize).Take(pageSize);
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                        else
                                        {
                                            switch (chronologicalOrder)
                                            {
                                                case ChronologicalOrders.Descending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate descending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                                case ChronologicalOrders.Ascending:
                                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                                             where x.IsActive && x.TenantIdPointsGranter == (Guid)refId && x.CreatedDate >= minDate
                                                             orderby x.CreatedDate ascending
                                                             select x).Skip(pageSize * pageSize).Take(pageSize);
                                                    break;
                                            }

                                        }
                                    }
                                }

                                break;
                        }

                        break;
                }

                if (query != null)
                {
                    CheckIn checkIn = null;
                    checkIns = new List<CheckIn>();

                    foreach (OltpcheckInsView item in query)
                    {
                        checkIn = new CheckIn
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            BranchId = item.BranchId,
                            TenantId = item.TenantId,
                            TenantIdPointsGranter = item.TenantIdPointsGranter,
                            BroadcasterId = item.BroadcasterId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            PointsAppliedType = item.PointsAppliedType,
                            PointsAppliedTypeName = GetPointsAppliedTypeName(item.PointsAppliedType),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate,
                            IsActive = item.IsActive,
                            EarnedPoints = item.EarnedPoints,
                            UsedForRewardClaim = item.UsedForRewardClaim,
                            BranchName = item.BranchName,
                            TenantName = item.TenantName,
                            BroadcasterName = item.BroadcasterName,
                            AccountNumber = item.AccountNumber,
                            Username = item.UserName,
                            UserName = item.Name
                        };

                        checkIns.Add(checkIn);
                    }
                }

            }
            catch (Exception e)
            {
                checkIns = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkIns;
        }

        /// <summary>
        /// Gets the count of enabled checkins that can be used for reward claim
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tenantId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int Get(string userId, Guid tenantId, DateTime startDate, DateTime endDate)
        {
            int? availableCheckIns = -1;

            try
            {
              availableCheckIns =  this._businessObjects.StoredProcsHandler.GetEnabledCheckInsCountForUser(userId, tenantId, startDate, endDate);
            }
            catch (Exception e)
            {
                availableCheckIns = -1;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return (int)availableCheckIns;
        }

        public CheckIn Get(Guid id)
        {
            CheckIn checkIn = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpcheckInsView
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (OltpcheckInsView item in query)
                    {
                        checkIn = new CheckIn
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            BranchId = item.BranchId,
                            TenantId = item.TenantId,
                            TenantIdPointsGranter = item.TenantIdPointsGranter,
                            BroadcasterId = item.BroadcasterId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            PointsAppliedType = item.PointsAppliedType,
                            PointsAppliedTypeName = GetPointsAppliedTypeName(item.PointsAppliedType),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate,
                            IsActive = item.IsActive,
                            EarnedPoints = item.EarnedPoints,
                            UsedForRewardClaim = item.UsedForRewardClaim,
                            BranchName = item.BranchName,
                            TenantName = item.TenantName,
                            BroadcasterName = item.BroadcasterName,
                            AccountNumber = item.AccountNumber,
                            Username = item.UserName,
                            UserName = item.Name
                        };
                    }
                }
            }
            catch (Exception e)
            {
                checkIn = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkIn;
        }

        /// <summary>
        /// Retrieves the 1st checkIn based in chronologica order
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CheckIn Get(Guid tenantId, Guid branchId, Guid tenantIdPointsGranter, string userId, int chronogicalType, int type)
        {
            CheckIn checkIn = null;

            try
            {
                Oltpcheckins item = null;

                switch (chronogicalType)
                {
                    case ChronologicalOrders.Descending:

                        if (type == CheckInTypes.All)
                        {
                            item = (from x in this._businessObjects.Context.Oltpcheckins
                                    where x.TenantId == tenantId && x.BranchId == branchId && x.TenantIdPointsGranter == tenantIdPointsGranter && x.UserId == userId
                                    orderby x.CreatedDate descending
                                    select x).FirstOrDefault();
                        }
                        else
                        {
                            item = (from x in this._businessObjects.Context.Oltpcheckins
                                    where x.TenantId == tenantId && x.Type == type && x.BranchId == branchId && x.TenantIdPointsGranter == tenantIdPointsGranter && x.UserId == userId
                                    orderby x.CreatedDate descending
                                    select x).FirstOrDefault();
                        }

                        break;
                    case ChronologicalOrders.Ascending:

                        if (type == CheckInTypes.All)
                        {
                            item = (from x in this._businessObjects.Context.Oltpcheckins
                                    where x.TenantId == tenantId && x.BranchId == branchId && x.TenantIdPointsGranter == tenantIdPointsGranter && x.UserId == userId
                                    orderby x.CreatedDate ascending
                                    select x).FirstOrDefault();
                        }
                        else
                        {
                            item = (from x in this._businessObjects.Context.Oltpcheckins
                                    where x.TenantId == tenantId && x.Type == type && x.BranchId == branchId && x.TenantIdPointsGranter == tenantIdPointsGranter && x.UserId == userId
                                    orderby x.CreatedDate ascending
                                    select x).FirstOrDefault();
                        }

                        break;
                }

                if (item != null)
                {
                    checkIn = new CheckIn
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        BranchId = item.BranchId,
                        TenantId = item.TenantId,
                        TenantIdPointsGranter = item.TenantIdPointsGranter,
                        BroadcasterId = item.BroadcasterId,
                        Type = item.Type,
                        TypeName = GetTypeName(item.Type),
                        PointsAppliedType = item.PointsAppliedType,
                        PointsAppliedTypeName = GetPointsAppliedTypeName(item.PointsAppliedType),
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ExpirationDate = item.ExpirationDate,
                        IsActive = item.IsActive,
                        EarnedPoints = item.EarnedPoints,
                        UsedForRewardClaim = item.UsedForRewardClaim
                    };
                }

            }
            catch (Exception e)
            {
                checkIn = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkIn;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves the 1st checkIn based in chronologica order
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CheckIn Get(Guid tenantId, Guid branchId, string userId, int chronogicalType, int type)
        {
            CheckIn checkIn = null;

            try
            {
                Oltpcheckins item = null;

                switch (chronogicalType)
                {
                    case ChronologicalOrders.Descending:

                        if (type == CheckInTypes.All)
                        {
                            item = (from x in this._businessObjects.Context.Oltpcheckins
                                    where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId
                                    orderby x.CreatedDate descending
                                    select x).FirstOrDefault();
                        }
                        else
                        {
                            item = (from x in this._businessObjects.Context.Oltpcheckins
                                    where x.TenantId == tenantId && x.Type == type && x.BranchId == branchId && x.UserId == userId
                                    orderby x.CreatedDate descending
                                    select x).FirstOrDefault();
                        }

                        break;
                    case ChronologicalOrders.Ascending:

                        if (type == CheckInTypes.All)
                        {
                            item = (from x in this._businessObjects.Context.Oltpcheckins
                                    where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId
                                    orderby x.CreatedDate ascending
                                    select x).FirstOrDefault();
                        }
                        else
                        {
                            item = (from x in this._businessObjects.Context.Oltpcheckins
                                    where x.TenantId == tenantId && x.Type == type && x.BranchId == branchId && x.UserId == userId
                                    orderby x.CreatedDate ascending
                                    select x).FirstOrDefault();
                        }

                        break;
                }

                if (item != null)
                {
                    checkIn = new CheckIn
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        BranchId = item.BranchId,
                        TenantId = item.TenantId,
                        TenantIdPointsGranter = item.TenantIdPointsGranter,
                        BroadcasterId = item.BroadcasterId,
                        Type = item.Type,
                        TypeName = GetTypeName(item.Type),
                        PointsAppliedType = item.PointsAppliedType,
                        PointsAppliedTypeName = GetPointsAppliedTypeName(item.PointsAppliedType),
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ExpirationDate = item.ExpirationDate,
                        IsActive = item.IsActive,
                        EarnedPoints = item.EarnedPoints,
                        UsedForRewardClaim = item.UsedForRewardClaim
                    };
                }

            }
            catch (Exception e)
            {
                checkIn = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkIn;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Retrieves the 1st checkIn based in chronologica order
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CheckIn Get(Guid tenantId, string userId, int chronogicalType, int type, bool requirePoints)
        {
            CheckIn checkIn = null;

            try
            {
                Oltpcheckins item = null;

                switch (chronogicalType)
                {
                    case ChronologicalOrders.Descending:

                        if (type == CheckInTypes.All)
                        {
                            if (requirePoints)
                            {
                                item = (from x in this._businessObjects.Context.Oltpcheckins
                                        where x.TenantId == tenantId && x.UserId == userId && x.EarnedPoints > 0
                                        orderby x.CreatedDate descending
                                        select x).FirstOrDefault();
                            }
                            else
                            {
                                item = (from x in this._businessObjects.Context.Oltpcheckins
                                        where x.TenantId == tenantId && x.UserId == userId
                                        orderby x.CreatedDate descending
                                        select x).FirstOrDefault();
                            }

                        }
                        else
                        {
                            if (requirePoints)
                            {
                                item = (from x in this._businessObjects.Context.Oltpcheckins
                                        where x.TenantId == tenantId && x.Type == type && x.UserId == userId && x.EarnedPoints > 0
                                        orderby x.CreatedDate descending
                                        select x).FirstOrDefault();
                            }
                            else
                            {
                                item = (from x in this._businessObjects.Context.Oltpcheckins
                                        where x.TenantId == tenantId && x.Type == type && x.UserId == userId
                                        orderby x.CreatedDate descending
                                        select x).FirstOrDefault();
                            }
                        }

                        break;
                    case ChronologicalOrders.Ascending:

                        if (type == CheckInTypes.All)
                        {
                            if (requirePoints)
                            {
                                item = (from x in this._businessObjects.Context.Oltpcheckins
                                        where x.TenantId == tenantId && x.UserId == userId && x.EarnedPoints > 0
                                        orderby x.CreatedDate ascending
                                        select x).FirstOrDefault();
                            }
                            else
                            {
                                item = (from x in this._businessObjects.Context.Oltpcheckins
                                        where x.TenantId == tenantId && x.UserId == userId
                                        orderby x.CreatedDate ascending
                                        select x).FirstOrDefault();
                            }

                        }
                        else
                        {
                            if (requirePoints)
                            {
                                item = (from x in this._businessObjects.Context.Oltpcheckins
                                        where x.TenantId == tenantId && x.Type == type && x.UserId == userId && x.EarnedPoints > 0
                                        orderby x.CreatedDate ascending
                                        select x).FirstOrDefault();
                            }
                            else
                            {
                                item = (from x in this._businessObjects.Context.Oltpcheckins
                                        where x.TenantId == tenantId && x.Type == type && x.UserId == userId
                                        orderby x.CreatedDate ascending
                                        select x).FirstOrDefault();
                            }

                        }

                        break;
                }

                if (item != null)
                {
                    checkIn = new CheckIn
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        BranchId = item.BranchId,
                        TenantId = item.TenantId,
                        TenantIdPointsGranter = item.TenantIdPointsGranter,
                        BroadcasterId = item.BroadcasterId,
                        Type = item.Type,
                        TypeName = GetTypeName(item.Type),
                        PointsAppliedType = item.PointsAppliedType,
                        PointsAppliedTypeName = GetPointsAppliedTypeName(item.PointsAppliedType),
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ExpirationDate = item.ExpirationDate,
                        IsActive = item.IsActive,
                        EarnedPoints = item.EarnedPoints,
                        UsedForRewardClaim = item.UsedForRewardClaim
                    };
                }

            }
            catch (Exception e)
            {
                checkIn = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkIn;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        public CheckIn Post(string userId, Guid branchId, Guid tenantId, Guid tenantPointsGranterId, Guid? broadcasterId, int type, int pointsAppliedType, DateTime? expirationDate, int earnedPoints)
        {
            CheckIn checkIn;
            try
            {
                Oltpcheckins newCheckIn = new Oltpcheckins
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    BranchId = branchId,
                    TenantId = tenantId,
                    TenantIdPointsGranter = tenantPointsGranterId,
                    BroadcasterId = broadcasterId,
                    Type = type,
                    PointsAppliedType = pointsAppliedType,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    ExpirationDate = expirationDate,
                    IsActive = true,
                    EarnedPoints = earnedPoints,
                    UsedForRewardClaim = false
                };

                this._businessObjects.Context.Oltpcheckins.Add(newCheckIn);
                this._businessObjects.Context.SaveChanges();

                checkIn = new CheckIn
                {
                    Id = newCheckIn.Id,
                    UserId = newCheckIn.UserId,
                    BranchId = newCheckIn.BranchId,
                    TenantId = newCheckIn.TenantId,
                    TenantIdPointsGranter = newCheckIn.TenantIdPointsGranter,
                    BroadcasterId = newCheckIn.BroadcasterId,
                    Type = newCheckIn.Type,
                    TypeName = GetTypeName(newCheckIn.Type),
                    PointsAppliedType = newCheckIn.PointsAppliedType,
                    PointsAppliedTypeName = GetPointsAppliedTypeName(newCheckIn.PointsAppliedType),
                    CreatedDate = newCheckIn.CreatedDate,
                    UpdatedDate = newCheckIn.UpdatedDate,
                    ExpirationDate = newCheckIn.ExpirationDate,
                    IsActive = newCheckIn.IsActive,
                    EarnedPoints = newCheckIn.EarnedPoints,
                    UsedForRewardClaim = newCheckIn.UsedForRewardClaim
                };
            }
            catch (Exception e)
            {
                checkIn = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkIn;
        }

        public bool Put(Guid id, int changeType)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpcheckins
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Oltpcheckins checkin = null;

                    foreach (Oltpcheckins item in query)
                    {
                        checkin = item;
                    }

                    if (checkin != null)
                    {
                        switch (changeType)
                        {
                            case CheckInChangeTypes.ActiveState:
                                checkin.IsActive = !checkin.IsActive;
                                checkin.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();
                                success = true;
                                break;
                            case CheckInChangeTypes.UsedForRewardClaim:
                                checkin.UsedForRewardClaim = !checkin.UsedForRewardClaim;
                                checkin.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();
                                success = true;
                                break;

                        }
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

        /// <summary>
        /// Sets an amount of checkIns to usedForRewardClaim = true
        /// </summary>
        /// <param name="checkInsCount"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool Put(Guid tenantId, string userId, int checkInsCount, DateTime startDate, DateTime endDate)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpcheckins
                            where x.TenantId == tenantId && x.UserId == userId && !x.UsedForRewardClaim && x.CreatedDate >= startDate && x.CreatedDate < endDate
                            select x;

                if (query != null)
                {
                    int count = 0;

                    foreach (Oltpcheckins item in query)
                    {
                        item.UsedForRewardClaim = true;

                        ++count;
                    }

                    if (checkInsCount == count)
                    {
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

        #region MISCELLANEOUS

        public List<CheckIn> GetRecentBranchCheckIns(Guid branchId, int checkInType, int pointsAppliedType, int chronologicalOrder, DateTime minDate)
        {
            List<CheckIn> checkIns = null;

            try
            {
                var query = (dynamic)null;

                if (checkInType != CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                {
                    switch (chronologicalOrder)
                    {
                        case ChronologicalOrders.Descending:
                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                     where x.BranchId == branchId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                     select x)
                                    .GroupBy(x => x.UserId)
                                    .Select(s => s.FirstOrDefault())
                                    .OrderByDescending(s => s.CreatedDate);
                            break;
                        case ChronologicalOrders.Ascending:
                            query = (from x in this._businessObjects.Context.OltpcheckInsView
                                     where x.BranchId == branchId && x.Type == checkInType && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                     select x)
                                    .GroupBy(x => x.UserId)
                                    .Select(s => s.FirstOrDefault())
                                    .OrderBy(s => s.CreatedDate);
                            break;
                    }

                }
                else
                {
                    if (checkInType != CheckInTypes.All && pointsAppliedType == CheckInPointsAppliedTypes.All)
                    {
                        switch (chronologicalOrder)
                        {
                            case ChronologicalOrders.Descending:
                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                         where x.BranchId == branchId && x.Type == checkInType && x.CreatedDate >= minDate
                                         select x)
                                        .GroupBy(x => x.UserId)
                                        .Select(s => s.FirstOrDefault())
                                        .OrderByDescending(s => s.CreatedDate);
                                break;
                            case ChronologicalOrders.Ascending:
                                query = (from x in this._businessObjects.Context.OltpcheckInsView
                                         where x.BranchId == branchId && x.Type == checkInType && x.CreatedDate >= minDate
                                         select x)
                                        .GroupBy(x => x.UserId)
                                        .Select(s => s.FirstOrDefault())
                                        .OrderBy(s => s.CreatedDate);
                                break;
                        }

                    }
                    else
                    {
                        if (checkInType == CheckInTypes.All && pointsAppliedType != CheckInPointsAppliedTypes.All)
                        {
                            switch (chronologicalOrder)
                            {
                                case ChronologicalOrders.Descending:
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.BranchId == branchId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             select x)
                                            .GroupBy(x => x.UserId)
                                            .Select(s => s.FirstOrDefault())
                                            .OrderByDescending(s => s.CreatedDate);
                                    break;
                                case ChronologicalOrders.Ascending:
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.BranchId == branchId && x.PointsAppliedType == pointsAppliedType && x.CreatedDate >= minDate
                                             select x)
                                            .GroupBy(x => x.UserId)
                                            .Select(s => s.FirstOrDefault())
                                            .OrderBy(s => s.CreatedDate);
                                    break;
                            }

                        }
                        else
                        {
                            switch (chronologicalOrder)
                            {
                                case ChronologicalOrders.Descending:
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.BranchId == branchId && x.CreatedDate >= minDate
                                             select x)
                                            .GroupBy(x => x.UserId)
                                            .Select(s => s.FirstOrDefault())
                                            .OrderByDescending(s => s.CreatedDate);
                                    break;
                                case ChronologicalOrders.Ascending:
                                    query = (from x in this._businessObjects.Context.OltpcheckInsView
                                             where x.BranchId == branchId && x.CreatedDate >= minDate
                                             select x)
                                            .GroupBy(x => x.UserId)
                                            .Select(s => s.FirstOrDefault())
                                            .OrderBy(s => s.CreatedDate);
                                    break;
                            }

                        }
                    }
                }

                if (query != null)
                {
                    CheckIn checkIn = null;
                    checkIns = new List<CheckIn>();

                    foreach (OltpcheckInsView item in query)
                    {
                        checkIn = new CheckIn
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            BranchId = item.BranchId,
                            TenantId = item.TenantId,
                            TenantIdPointsGranter = item.TenantIdPointsGranter,
                            BroadcasterId = item.BroadcasterId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            PointsAppliedType = item.PointsAppliedType,
                            PointsAppliedTypeName = GetPointsAppliedTypeName(item.PointsAppliedType),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate,
                            IsActive = item.IsActive,
                            EarnedPoints = item.EarnedPoints,
                            UsedForRewardClaim = item.UsedForRewardClaim,
                            BranchName = item.BranchName,
                            TenantName = item.TenantName,
                            BroadcasterName = item.BroadcasterName,
                            AccountNumber = item.AccountNumber,
                            Username = item.UserName,
                            UserName = item.Name
                        };

                        checkIns.Add(checkIn);
                    }
                }
            }
            catch (Exception e)
            {
                checkIns = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkIns;
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
        public CheckInManager(BusinessObjects businessObjects)
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
