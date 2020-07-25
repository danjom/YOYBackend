using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.FeaturedSlide;
using YOY.DTO.Entities.Misc.Location;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class FeaturedSlideManager
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
                case FeaturedSlideTypes.PlatformPropietary:
                    typeName = Resources.PlatformPropietary;
                    break;
                case FeaturedSlideTypes.Sponsored:
                    typeName = Resources.Sponsored;
                    break;
                case FeaturedSlideTypes.SpecificCampaign:
                    typeName = Resources.SpecificCampaign;
                    break;
            }

            return typeName;
        }

        private string GetRouteContentTypeName(int type)
        {
            string  typeName = type switch
            {
                FeaturedSlideAccessRouteTypes.None => Resources.None,
                FeaturedSlideAccessRouteTypes.DealsListMultipleCommerces => Resources.DealsListMultipleCommerces,
                FeaturedSlideAccessRouteTypes.DealsListSingleCommerce => Resources.DealsListSingleCommerce,
                FeaturedSlideAccessRouteTypes.CashbackListMultipleCommerces => Resources.CashbackListMultipleCommerces,
                FeaturedSlideAccessRouteTypes.CashbackListSingleCommerces => Resources.CashbackListMultipleCommerces,
                FeaturedSlideAccessRouteTypes.DealDetail => Resources.DealDetail,
                FeaturedSlideAccessRouteTypes.CashbackDetail => Resources.CashbackDetail,
                FeaturedSlideAccessRouteTypes.Image => Resources.Image,
                FeaturedSlideAccessRouteTypes.Video => Resources.Video,
                _ => "--",
            };
            return typeName;
        }

        public List<FeaturedSlide> Gets(Guid? countryId, Guid? stateId, Guid? tenantId, int type, int routeAccessType, DateTime minDate, DateTime maxDate, int pageSize, int pageNumber)
        {
            List<FeaturedSlide> featuredSlides = null;

            try
            {
                var query = (dynamic)null;

                if (countryId != null)
                {
                    if (stateId != null)
                    {
                        if (tenantId != null)
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                        else
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tenantId != null)
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                        else
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.Type == type && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CountryId == countryId && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (stateId != null)
                    {
                        if (tenantId != null)
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                        else
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.StateId == stateId && x.Type == type && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.StateId == stateId && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tenantId != null)
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.CreatedDate < minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.TenantId == tenantId && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                        else
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.Type == type && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.RouteContentType == routeAccessType && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.DeffeaturedSlides
                                             where !x.Deleted && x.CreatedDate > minDate && x.CreatedDate < maxDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                    }
                }

                if (query != null)
                {
                    featuredSlides = new List<FeaturedSlide>();
                    FeaturedSlide featuredSlide = null;

                    foreach (DeffeaturedSlides item in query)
                    {
                        featuredSlide = new FeaturedSlide
                        {
                            Id = item.Id,
                            Name = item.Description,
                            Description = item.Description,
                            ImageId = item.ImageId,
                            TenantId = item.TenantId,
                            CountryId = item.CountryId,
                            StateId = item.StateId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            RouteContentType = item.RouteContentType,
                            RouteContentTypeName = item.RouteContentType != null ? GetRouteContentTypeName((int)item.RouteContentType) : "-",
                            AccessRoute = item.AccessRoute,
                            MaxViews = item.MaxViews,
                            IsActive = (bool)item.IsActive,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        featuredSlides.Add(featuredSlide);
                    }
                }
            }
            catch (Exception e)
            {
                featuredSlides = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return featuredSlides;
        }

        public List<FeaturedSlide> Gets(Guid? countryId, Guid? stateId, Guid? tenantId, int type, int routeAccessType, int releaseState, int expiredState, DateTime date)
        {
            List<FeaturedSlide> featuredSlides = null;

            try
            {
                var query = (dynamic)null;

                if (countryId != null)
                {
                    if (stateId != null)
                    {
                        if (tenantId != null)
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.StateId == stateId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tenantId != null)
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted &&  x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate < date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate < date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate < date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate >= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate >= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate >= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.TenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.CountryId == countryId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (stateId != null)
                    {
                        if (tenantId != null)
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.TenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type & x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type & x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.ReleaseDate > date & x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.ReleaseDate > date & x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.ReleaseDate <= date & x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.Type == type && x.ReleaseDate <= date & x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;

                                    }
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.StateId == stateId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tenantId != null)
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.TenantId == tenantId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (type != FeaturedSlideTypes.All)
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.Type == type && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (routeAccessType != FeaturedSlideAccessRouteTypes.All)
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.RouteContentType == routeAccessType
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.RouteContentType == routeAccessType && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.RouteContentType == routeAccessType && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.RouteContentType == routeAccessType && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.RouteContentType == routeAccessType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.RouteContentType == routeAccessType && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.RouteContentType == routeAccessType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (releaseState)
                                    {
                                        case ReleaseStates.All:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.NotReleased:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.ReleaseDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.ReleaseDate > date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.ReleaseDate > date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                        case ReleaseStates.Released:
                                            switch (expiredState)
                                            {
                                                case ExpiredStates.All:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.ReleaseDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Valid:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.ReleaseDate <= date && x.ExpirationDate > date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                                case ExpiredStates.Expired:
                                                    query = from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where !x.Deleted && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                            orderby x.CreatedDate descending
                                                            select x;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (query != null)
                {
                    featuredSlides = new List<FeaturedSlide>();
                    FeaturedSlide featuredSlide = null;

                    foreach (DeffeaturedSlides item in query)
                    {
                        featuredSlide = new FeaturedSlide
                        {
                            Id = item.Id,
                            Name = item.Description,
                            Description = item.Description,
                            ImageId = item.ImageId,
                            TenantId = item.TenantId,
                            CountryId = item.CountryId,
                            StateId = item.StateId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            RouteContentType = item.RouteContentType,
                            RouteContentTypeName = item.RouteContentType != null ? GetRouteContentTypeName((int)item.RouteContentType) : "-",
                            AccessRoute = item.AccessRoute,
                            MaxViews = item.MaxViews,
                            IsActive = (bool)item.IsActive,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        featuredSlides.Add(featuredSlide);
                    }
                }
            }
            catch (Exception e)
            {
                featuredSlides = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return featuredSlides;
        }

        public FeaturedSlide Get(Guid id)
        {
            FeaturedSlide featuredSlide = null;

            try
            {
                var query = from x in this._businessObjects.Context.DeffeaturedSlides
                            where !x.Deleted && x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (DeffeaturedSlides item in query)
                    {
                        featuredSlide = new FeaturedSlide
                        {
                            Id = item.Id,
                            Name = item.Description,
                            Description = item.Description,
                            ImageId = item.ImageId,
                            TenantId = item.TenantId,
                            CountryId = item.CountryId,
                            StateId = item.StateId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            RouteContentType = item.RouteContentType,
                            RouteContentTypeName = item.RouteContentType != null ? GetRouteContentTypeName((int)item.RouteContentType) : "-",
                            AccessRoute = item.AccessRoute,
                            MaxViews = item.MaxViews,
                            IsActive = (bool)item.IsActive,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                    }
                }
            }
            catch (Exception e)
            {
                featuredSlide = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return featuredSlide;
        }

        public FeaturedSlide Post(Guid countryId, Guid? state, Guid? tenantId, Guid? imageId, string name, string description, int type, int routeContentType, string accessRoute, int maxViews, DateTime releaseDate, DateTime expirationDate)
        {
            FeaturedSlide featuredSlide;

            try
            {
                DeffeaturedSlides newFeaturedSlide = new DeffeaturedSlides
                {
                    Id = Guid.NewGuid(),
                    CountryId = countryId,
                    StateId = state,
                    TenantId = tenantId,
                    ImageId = imageId,
                    Name = name,
                    Description = description,
                    Type = type,
                    RouteContentType = routeContentType,
                    AccessRoute = accessRoute,
                    MaxViews = maxViews,
                    IsActive = true,
                    ReleaseDate = releaseDate,
                    ExpirationDate = expirationDate,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.DeffeaturedSlides.Add(newFeaturedSlide);
                this._businessObjects.Context.SaveChanges();

                featuredSlide = new FeaturedSlide
                {
                    Id = newFeaturedSlide.Id,
                    Name = newFeaturedSlide.Description,
                    Description = newFeaturedSlide.Description,
                    ImageId = newFeaturedSlide.ImageId,
                    TenantId = newFeaturedSlide.TenantId,
                    CountryId = newFeaturedSlide.CountryId,
                    StateId = newFeaturedSlide.StateId,
                    Type = newFeaturedSlide.Type,
                    TypeName = GetTypeName(newFeaturedSlide.Type),
                    RouteContentType = newFeaturedSlide.RouteContentType,
                    RouteContentTypeName = newFeaturedSlide.RouteContentType != null ? GetRouteContentTypeName((int)newFeaturedSlide.RouteContentType) : "-",
                    AccessRoute = newFeaturedSlide.AccessRoute,
                    MaxViews = newFeaturedSlide.MaxViews,
                    IsActive = (bool)newFeaturedSlide.IsActive,
                    ReleaseDate = newFeaturedSlide.ReleaseDate,
                    ExpirationDate = newFeaturedSlide.ExpirationDate,
                    CreatedDate = newFeaturedSlide.CreatedDate,
                    UpdatedDate = newFeaturedSlide.UpdatedDate
                };

            }
            catch (Exception e)
            {
                featuredSlide = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return featuredSlide;
        }

        public FeaturedSlide Put(Guid id, Guid? stateId, string name, string description, string accessRoute, int maxViews, DateTime releaseDate, DateTime expirationDate)
        {
            FeaturedSlide featuredSlide = null;

            try
            {
                var query = from x in this._businessObjects.Context.DeffeaturedSlides
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DeffeaturedSlides currentFeaturedSlide = null;

                    foreach (DeffeaturedSlides item in query)
                    {
                        currentFeaturedSlide = item;
                    }

                    if (currentFeaturedSlide != null)
                    {
                        currentFeaturedSlide.StateId = stateId;
                        currentFeaturedSlide.Name = name;
                        currentFeaturedSlide.Description = description;
                        currentFeaturedSlide.AccessRoute = accessRoute;
                        currentFeaturedSlide.MaxViews = maxViews;
                        currentFeaturedSlide.ReleaseDate = releaseDate;
                        currentFeaturedSlide.ExpirationDate = expirationDate;
                        currentFeaturedSlide.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        featuredSlide = new FeaturedSlide
                        {
                            Id = currentFeaturedSlide.Id,
                            Name = currentFeaturedSlide.Description,
                            Description = currentFeaturedSlide.Description,
                            ImageId = currentFeaturedSlide.ImageId,
                            TenantId = currentFeaturedSlide.TenantId,
                            CountryId = currentFeaturedSlide.CountryId,
                            StateId = currentFeaturedSlide.StateId,
                            Type = currentFeaturedSlide.Type,
                            TypeName = GetTypeName(currentFeaturedSlide.Type),
                            RouteContentType = currentFeaturedSlide.RouteContentType,
                            RouteContentTypeName = currentFeaturedSlide.RouteContentType != null ? GetRouteContentTypeName((int)currentFeaturedSlide.RouteContentType) : "-",
                            AccessRoute = currentFeaturedSlide.AccessRoute,
                            MaxViews = currentFeaturedSlide.MaxViews,
                            IsActive = (bool)currentFeaturedSlide.IsActive,
                            ReleaseDate = currentFeaturedSlide.ReleaseDate,
                            ExpirationDate = currentFeaturedSlide.ExpirationDate,
                            CreatedDate = currentFeaturedSlide.CreatedDate,
                            UpdatedDate = currentFeaturedSlide.UpdatedDate
                        };

                    }
                }
            }
            catch (Exception e)
            {
                featuredSlide = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return featuredSlide;
        }

        public bool Put(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DeffeaturedSlides
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DeffeaturedSlides currenctFeaturedSlide = null;

                    foreach (DeffeaturedSlides item in query)
                    {
                        currenctFeaturedSlide = item;
                    }

                    if (currenctFeaturedSlide != null)
                    {
                        currenctFeaturedSlide.IsActive = !currenctFeaturedSlide.IsActive;
                        currenctFeaturedSlide.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }

        public bool Put(Guid id, Guid imgId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DeffeaturedSlides
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DeffeaturedSlides currenctFeaturedSlide = null;

                    foreach (DeffeaturedSlides item in query)
                    {
                        currenctFeaturedSlide = item;
                    }

                    if (currenctFeaturedSlide != null)
                    {
                        currenctFeaturedSlide.ImageId = imgId;
                        currenctFeaturedSlide.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                DeffeaturedSlides currenctFeaturedSlide = (from x in this._businessObjects.Context.DeffeaturedSlides
                                                            where x.Id == id
                                                            select x).FirstOrDefault();

                if (currenctFeaturedSlide != null)
                {
                    currenctFeaturedSlide.Deleted = true;
                    currenctFeaturedSlide.UpdatedDate = DateTime.UtcNow;


                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }

        #endregion

        #region CONTENTS

        public List<FeaturedSlideContent> Gets(Guid featuredSlideId, int activeState)
        {
            List<FeaturedSlideContent> featuredSlideContents = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = from x in this._businessObjects.Context.DeffeaturedSlideContents
                                where x.FeaturedSlideId == featuredSlideId
                                select x;
                        break;
                    case ActiveStates.Active:
                        query = from x in this._businessObjects.Context.DeffeaturedSlideContents
                                where (bool)x.IsActive && x.FeaturedSlideId == featuredSlideId
                                select x;
                        break;
                    case ActiveStates.Inactive:
                        query = from x in this._businessObjects.Context.DeffeaturedSlideContents
                                where !(bool)x.IsActive && x.FeaturedSlideId == featuredSlideId
                                select x;
                        break;

                }

                if (query != null)
                {
                    featuredSlideContents = new List<FeaturedSlideContent>();
                    FeaturedSlideContent featuredSlideContent = null;

                    foreach (DeffeaturedSlideContents item in query)
                    {
                        featuredSlideContent = new FeaturedSlideContent
                        {
                            Id = item.Id,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetRouteContentTypeName(item.ReferenceType),
                            IsActive = (bool)item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate

                        };

                        featuredSlideContents.Add(featuredSlideContent);
                    }
                }


            }
            catch (Exception e)
            {
                featuredSlideContents = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return featuredSlideContents;
        }

        public FeaturedSlideContent Get(Guid id, int nothing)
        {
            FeaturedSlideContent featuredSlideContent = null;

            try
            {
                var query = from x in this._businessObjects.Context.DeffeaturedSlideContents
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (DeffeaturedSlideContents item in query)
                    {
                        featuredSlideContent = new FeaturedSlideContent
                        {
                            Id = item.Id,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetRouteContentTypeName(item.ReferenceType),
                            IsActive = (bool)item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }


            }
            catch (Exception e)
            {
                featuredSlideContent = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return featuredSlideContent;
        }

        public bool Post(Guid featuredSlideId, Guid referenceId, int referenceType)
        {
            bool success = false;

            try
            {
                DeffeaturedSlideContents newContent = new DeffeaturedSlideContents
                {
                    Id = Guid.NewGuid(),
                    FeaturedSlideId = featuredSlideId,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.DeffeaturedSlideContents.Add(newContent);
                this._businessObjects.Context.SaveChanges();

                success = true;
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }

        public bool Put(Guid id, int nothing)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DeffeaturedSlideContents
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DeffeaturedSlideContents currenctContent = null;

                    foreach (DeffeaturedSlideContents item in query)
                    {
                        currenctContent = item;
                    }

                    if (currenctContent != null)
                    {
                        currenctContent.IsActive = !currenctContent.IsActive;
                        currenctContent.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }

            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }

        public bool Delete(Guid id, int nothing)
        {
            bool success = false;

            try
            {
                DeffeaturedSlideContents currenctContent = (from x in this._businessObjects.Context.DeffeaturedSlideContents
                                                            where x.Id == id
                                                            select x).FirstOrDefault();

                if (currenctContent != null)
                {
                    currenctContent.Deleted = true;
                    currenctContent.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }

            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
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
        /// Creates a new FeaturedSlideManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public FeaturedSlideManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD FEATURED SLIDES MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
