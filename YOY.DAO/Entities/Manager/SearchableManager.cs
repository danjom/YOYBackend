using YOY.DTO.Entities;
using YOY.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class SearchableManager
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

        public List<Searchable> Gets(Guid indexOwner, int referenceType, int activeState, int releaseState, int expiredState, DateTime date, int pageSize, int pageNumber)
        {
            List<Searchable> searchables = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (releaseState)
                        {
                            case ReleaseStates.All:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReferenceType == referenceType
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                                
                                        break;
                                    case ExpiredStates.Valid:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ExpirationDate > date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ExpirationDate > date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ExpirationDate <= date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ExpirationDate <= date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                }
                                break;
                            case ReleaseStates.Released:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate <= date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReleaseDate <= date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }

                                        break;
                                    case ExpiredStates.Valid:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReleaseDate <= date && x.ExpirationDate > date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                }
                                break;
                            case ReleaseStates.NotReleased:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate > date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReleaseDate > date
                                                     orderby x.ContentType ascending, x.Name ascending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }

                                        break;
                                    case ExpiredStates.Valid:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate > date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReleaseDate > date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IndexOwner == indexOwner && x.ReleaseDate > date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                    case ActiveStates.Active:
                        switch (releaseState)
                        {
                            case ReleaseStates.All:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }

                                        break;
                                    case ExpiredStates.Valid:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                }
                                break;
                            case ReleaseStates.Released:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }

                                        break;
                                    case ExpiredStates.Valid:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate <= date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                }
                                break;
                            case ReleaseStates.NotReleased:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }

                                        break;
                                    case ExpiredStates.Valid:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate > date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate > date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate > date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:
                        switch (releaseState)
                        {
                            case ReleaseStates.All:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }

                                        break;
                                    case ExpiredStates.Valid:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                }
                                break;
                            case ReleaseStates.Released:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }

                                        break;
                                    case ExpiredStates.Valid:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate <= date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate <= date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                }
                                break;
                            case ReleaseStates.NotReleased:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }

                                        break;
                                    case ExpiredStates.Valid:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate > date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate > date && x.ExpirationDate > date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                    case ExpiredStates.Expired:
                                        if (referenceType != SearchableObjectTypes.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReferenceType == referenceType && x.ReleaseDate > date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                    where !x.IsActive && x.IndexOwner == indexOwner && x.ReleaseDate > date && x.ExpirationDate <= date
                                                    orderby x.ContentType ascending, x.Name ascending
                                                    select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                }

                if(query != null)
                {
                    searchables = new List<Searchable>();
                    Searchable searchable = null;

                    foreach(OltpsearchablesView item in query)
                    {
                        searchable = new Searchable
                        {
                            TenantId = item.TenantId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            Name = item.Name,
                            Icon = item.Icon,
                            Classification = item.Classification,
                            Category = item.Category,
                            Keywords = item.Keywords,
                            ContentType = item.ContentType,
                            Details = item.Details,
                            IsActive = item.IsActive,
                            ExpirationDate = item.ExpirationDate,
                            ReleaseDate = (DateTime)item.ReleaseDate,
                            IndexId = item.IndexOwner,
                            IndexName = item.IndexName,
                            AppName = item.AppName,
                            Service = item.IndexService,
                            SearchCount = item.SearchCount,
                            LastSearch = item.LastSearch,
                            CountryId = item.CountryId,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        searchables.Add(searchable);
                    }
                }
                
            }
            catch(Exception e)
            {
                searchables = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return searchables;
        }

        public List<Searchable> Gets(Guid referenceId, int activeState, int releaseState, int expiredState, DateTime date, int pageSize, int pageNumber)
        {
            List<Searchable> searchables = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (releaseState)
                        {
                            case ReleaseStates.All:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.ReferenceId == referenceId
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);

                                        break;
                                    case ExpiredStates.Valid:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.ReferenceId == referenceId && x.ExpirationDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ExpiredStates.Expired:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.ReferenceId == referenceId && x.ExpirationDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                                break;
                            case ReleaseStates.Released:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.ReferenceId == referenceId && x.ReleaseDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);

                                        break;
                                    case ExpiredStates.Valid:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.ReferenceId == referenceId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ExpiredStates.Expired:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.ReferenceId == referenceId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                                break;
                            case ReleaseStates.NotReleased:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.ReferenceId == referenceId && x.ReleaseDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);

                                        break;
                                    case ExpiredStates.Valid:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.ReferenceId == referenceId && x.ReleaseDate > date && x.ExpirationDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ExpiredStates.Expired:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.ReferenceId == referenceId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                                break;
                        }
                        break;
                    case ActiveStates.Active:
                        switch (releaseState)
                        {
                            case ReleaseStates.All:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.IsActive && x.ReferenceId == referenceId
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);

                                        break;
                                    case ExpiredStates.Valid:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.IsActive && x.ReferenceId == referenceId && x.ExpirationDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ExpiredStates.Expired:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.IsActive && x.ReferenceId == referenceId && x.ExpirationDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                                break;
                            case ReleaseStates.Released:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);

                                        break;
                                    case ExpiredStates.Valid:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ExpiredStates.Expired:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                                break;
                            case ReleaseStates.NotReleased:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);

                                        break;
                                    case ExpiredStates.Valid:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate > date && x.ExpirationDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ExpiredStates.Expired:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:
                        switch (releaseState)
                        {
                            case ReleaseStates.All:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where !x.IsActive && x.ReferenceId == referenceId
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);

                                        break;
                                    case ExpiredStates.Valid:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where !x.IsActive && x.ReferenceId == referenceId && x.ExpirationDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ExpiredStates.Expired:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where !x.IsActive && x.ReferenceId == referenceId && x.ExpirationDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                                break;
                            case ReleaseStates.Released:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where !x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);

                                        break;
                                    case ExpiredStates.Valid:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where !x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate <= date && x.ExpirationDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ExpiredStates.Expired:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where !x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate <= date && x.ExpirationDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                                break;
                            case ReleaseStates.NotReleased:
                                switch (expiredState)
                                {
                                    case ExpiredStates.All:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where !x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);

                                        break;
                                    case ExpiredStates.Valid:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where !x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate > date && x.ExpirationDate > date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ExpiredStates.Expired:
                                        query = (from x in this._businessObjects.Context.OltpsearchablesView
                                                where !x.IsActive && x.ReferenceId == referenceId && x.ReleaseDate > date && x.ExpirationDate <= date
                                                orderby x.ContentType ascending, x.Name ascending
                                                select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }
                                break;
                        }
                        break;
                }

                if (query != null)
                {
                    searchables = new List<Searchable>();
                    Searchable searchable = null;

                    foreach (OltpsearchablesView item in query)
                    {
                        searchable = new Searchable
                        {
                            TenantId = item.TenantId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            Name = item.Name,
                            Icon = item.Icon,
                            Classification = item.Classification,
                            Category = item.Category,
                            Keywords = item.Keywords,
                            ContentType = item.ContentType,
                            Details = item.Details,
                            IsActive = item.IsActive,
                            ExpirationDate = item.ExpirationDate,
                            ReleaseDate = (DateTime)item.ReleaseDate,
                            IndexId = item.IndexOwner,
                            IndexName = item.IndexName,
                            AppName = item.AppName,
                            Service = item.IndexService,
                            SearchCount = item.SearchCount,
                            LastSearch = item.LastSearch,
                            CountryId = item.CountryId,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        searchables.Add(searchable);
                    }
                }

            }
            catch (Exception e)
            {
                searchables = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return searchables;
        }

        public Searchable Get(Guid referenceId, Guid indexOwner)
        {
            Searchable searchable = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpsearchablesView
                            where x.IndexOwner == indexOwner && x.ReferenceId == referenceId
                            select x;

                if(query != null)
                {
                    foreach(OltpsearchablesView item in query)
                    {
                        searchable = new Searchable
                        {
                            TenantId = item.TenantId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            Name = item.Name,
                            Icon = item.Icon,
                            Classification = item.Classification,
                            Category = item.Category,
                            Keywords = item.Keywords,
                            ContentType = item.ContentType,
                            Details = item.Details,
                            IsActive = item.IsActive,
                            ExpirationDate = item.ExpirationDate,
                            ReleaseDate = (DateTime)item.ReleaseDate,
                            IndexId = item.IndexOwner,
                            IndexName = item.IndexName,
                            AppName = item.AppName,
                            Service = item.IndexService,
                            SearchCount = item.SearchCount,
                            LastSearch = item.LastSearch,
                            CountryId = item.CountryId,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch(Exception e)
            {
                searchable = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return searchable;
        }

        public Searchable Post(Guid tenantId, Guid referenceId, int referenceType, string name, string icon, string classification,
            string category, string keywords, string details, int contentType, DateTime releaseDate, DateTime? expirationDate, Guid indexOwner,
            Guid? countryId)
        {
            Searchable searchable = null;
            try
            {
                Oltpsearchables newSearchable = new Oltpsearchables
                {
                    TenantId = tenantId,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    Name = name,
                    Icon = icon,
                    Classification = classification,
                    Category = category,
                    Keywords = keywords,
                    Details = details,
                    ContentType = contentType,
                    IsActive = true,//By default
                    ReleaseDate = releaseDate,
                    ExpirationDate = expirationDate,
                    IndexOwner = indexOwner,
                    SearchCount = 0,//By default is set to 0
                    CountryId = countryId,
                    LastSearch = null,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.Oltpsearchables.Add(newSearchable);
                this._businessObjects.Context.SaveChanges();

                OltpsearchablesView newSearchableView = (from x in this._businessObjects.Context.OltpsearchablesView
                                                         where x.TenantId == newSearchable.TenantId && x.IndexOwner == newSearchable.IndexOwner && x.Name == newSearchable.Name
                                                         select x).FirstOrDefault();

                if(newSearchableView != null)
                {
                    searchable = new Searchable
                    {
                        TenantId = newSearchableView.TenantId,
                        ReferenceId = newSearchableView.ReferenceId,
                        ReferenceType = newSearchableView.ReferenceType,
                        Name = newSearchableView.Name,
                        Icon = newSearchableView.Icon,
                        Classification = newSearchableView.Classification,
                        Category = newSearchableView.Category,
                        Keywords = newSearchableView.Keywords,
                        ContentType = newSearchableView.ContentType,
                        Details = newSearchableView.Details,
                        IsActive = newSearchableView.IsActive,
                        ExpirationDate = newSearchableView.ExpirationDate,
                        ReleaseDate = (DateTime)newSearchableView.ReleaseDate,
                        IndexId = newSearchableView.IndexOwner,
                        IndexName = newSearchableView.IndexName,
                        AppName = newSearchableView.AppName,
                        Service = newSearchableView.IndexService,
                        SearchCount = newSearchableView.SearchCount,
                        LastSearch = newSearchableView.LastSearch,
                        CountryId = newSearchableView.CountryId,
                        CreatedDate = newSearchableView.CreatedDate,
                        UpdatedDate = newSearchableView.UpdatedDate
                    };
                }
            }
            catch(Exception e)
            {
                searchable = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return searchable;
        }

        public bool Put(Guid indexOwner, Guid referenceId, string name, string classification, string category, string keywords, string details, DateTime releaseDate, DateTime expirationDate)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpsearchables
                            where x.IndexOwner == indexOwner && x.ReferenceId == referenceId
                            select x;

                if(query != null)
                {
                    Oltpsearchables searchable = null;
                    foreach(Oltpsearchables item in query)
                    {
                        searchable = item;
                    }

                    if(searchable != null)
                    {
                        searchable.Name = name;
                        searchable.Classification = classification;
                        searchable.Category = category;
                        searchable.Keywords = keywords;
                        searchable.Details = details;
                        searchable.ReleaseDate = releaseDate;
                        searchable.ExpirationDate = expirationDate;
                        searchable.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
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

        public bool Put(Guid indexOwner, Guid referenceId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpsearchables
                            where x.IndexOwner == indexOwner && x.ReferenceId == referenceId
                            select x;

                if (query != null)
                {
                    Oltpsearchables searchable = null;
                    foreach (Oltpsearchables item in query)
                    {
                        searchable = item;
                    }

                    if (searchable != null)
                    {
                        searchable.IsActive = !searchable.IsActive;
                        searchable.UpdatedDate = DateTime.UtcNow;

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

        public bool Put(Guid indexOwner, Guid referenceId, string icon)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpsearchables
                            where x.IndexOwner == indexOwner && x.ReferenceId == referenceId
                            select x;

                if (query != null)
                {
                    Oltpsearchables searchable = null;
                    foreach (Oltpsearchables item in query)
                    {
                        searchable = item;
                    }

                    if (searchable != null)
                    {
                        searchable.Icon = icon;
                        searchable.UpdatedDate = DateTime.UtcNow;

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

        public bool Put(Guid indexOwner, Guid referenceId, DateTime searchDate)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpsearchables
                            where x.IndexOwner == indexOwner && x.ReferenceId == referenceId
                            select x;

                if (query != null)
                {
                    Oltpsearchables searchable = null;
                    foreach (Oltpsearchables item in query)
                    {
                        searchable = item;
                    }

                    if (searchable != null)
                    {
                        searchable.SearchCount += 1;
                        searchable.LastSearch = searchDate;
                        searchable.UpdatedDate = DateTime.UtcNow;

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

        public bool Delete(Guid indexOwner, Guid referenceId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpsearchables
                            where x.IndexOwner == indexOwner && x.ReferenceId == referenceId
                            select x;

                if (query != null)
                {
                    Oltpsearchables searchable = null;
                    foreach (Oltpsearchables item in query)
                    {
                        searchable = item;
                    }

                    if (searchable != null)
                    {
                        this._businessObjects.Context.Oltpsearchables.Remove(searchable);

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
        /// Creates a new PaymentMethodManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public SearchableManager(BusinessObjects businessObjects)
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
