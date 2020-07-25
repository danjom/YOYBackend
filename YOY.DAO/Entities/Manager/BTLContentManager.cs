using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class BTLContentManager
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

        private string GetDealTypeName(int typeId)
        {
            string typeName = "";

            switch (typeId)
            {
                case DealTypes.InStore:
                    typeName = Resources.Instore;
                    break;
                case DealTypes.Online:
                    typeName = Resources.Online;
                    break;
                case DealTypes.Phone:
                    typeName = Resources.PhoneCall;
                    break;

            }

            return typeName;
        }

        private string GetContentTypeName(int typeId)
        {
            string typeName = "";

            switch (typeId)
            {
                case BTLContentTypes.Catalog:
                    typeName = Resources.Catalog;
                    break;
                case BTLContentTypes.SingleImage:
                    typeName = Resources.Image;
                    break;
                case BTLContentTypes.AnimatedImage:
                    typeName = Resources.AnimatedImage;
                    break;
                case BTLContentTypes.Video:
                    typeName = Resources.Video;
                    break;
                case BTLContentTypes.Link:
                    typeName = Resources.Link;
                    break;

            }

            return typeName;
        }

        private string GetObjectiveTypeName(int objectiveType)
        {
            string typeName = "";

            switch (objectiveType)
            {

                case ObjectiveTypes.PointsRedemption:
                    typeName = Resources.PointsRedemption;
                    break;
                case ObjectiveTypes.RewardPurchases:
                    typeName = Resources.RewardPurchases;
                    break;
                case ObjectiveTypes.MediaInteraction:
                    typeName = Resources.MediaInteraction;
                    break;
                case ObjectiveTypes.GenerateTraffic:
                    typeName = Resources.GenerateTraffic;
                    break;
                case ObjectiveTypes.UpSelling:
                    typeName = Resources.UpSelling;
                    break;
                case ObjectiveTypes.ReturningIncentive:
                    typeName = Resources.ReturningIncentive;
                    break;
                case ObjectiveTypes.CrossSelling:
                    typeName = Resources.CrossSelling;
                    break;

            }

            return typeName;
        }

        public string GetGeoSegmentationTypeName(int segmentationType)
        {
            string typeName = "";

            switch (segmentationType)
            {
                case GeoSegmentationTypes.Country:
                    typeName = Resources.CountrySegmentation;
                    break;
                case GeoSegmentationTypes.State:
                    typeName = Resources.StateSegmentation;
                    break;
                case GeoSegmentationTypes.City:
                    typeName = Resources.CitySegmentation;
                    break;
            }

            return typeName;
        }

        private string GetPublishState(DateTime releaseDate, DateTime? expirationDate, DateTime refDate)
        {
            string publishState;
            if (releaseDate > refDate)
            {
                publishState = Resources.NotReleased;
            }
            else
            {
                publishState = refDate > expirationDate ? Resources.Expired : Resources.Released;
            }

            return publishState;
        }

        public List<BTLContent> Gets(int activeState, int releasedState, int expiredState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<BTLContent> contents = null;

            try
            {
                var query = (dynamic)null;

                switch (expiredState)
                {
                    case ExpiredStates.All:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releasedState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releasedState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate.Date > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releasedState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                        }

                        break;
                    case ExpiredStates.Expired://If product is expired makes no sense evaluate release state, product was released before being expired
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                        where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                        where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                        where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }

                        break;
                    case ExpiredStates.Valid:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releasedState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releasedState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate.Date > dateTime.Date
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releasedState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate.Date >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                        }

                        break;
                }

                if (query != null)
                {
                    BTLContent content = null;
                    contents = new List<BTLContent>();
                    foreach (OltpbtlcontentsView item in query)
                    {
                        content = new BTLContent
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName,
                            Name = item.Name,
                            Description = item.Description,
                            Keywords = item.Keywords,
                            DealType = item.DealType,
                            DealTypeName = this.GetDealTypeName(item.DealType),
                            ContentType = item.ContentType,
                            ContentTypeName = this.GetContentTypeName(item.ContentType),
                            ObjectiveType = item.ObjectiveType,
                            ObjectiveTypeName = this.GetObjectiveTypeName(item.ObjectiveType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = this.GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayImgId = item.DisplayImageId,
                            ViewCount = item.ViewCount,
                            SavedCount = item.SaveCount,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            IsActive = item.IsActive
                        };

                        content.PublishState = this.GetPublishState((DateTime)content.ReleaseDate, content.ExpirationDate, DateTime.UtcNow);

                        contents.Add(content);
                    }
                }
            }
            catch(Exception e)
            {
                contents = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return contents;
        }

        public BTLContent Get(Guid id, bool filterByTenant)
        {
            BTLContent content = null;

            try
            {
                var query = (dynamic)null;

                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.OltpbtlcontentsView
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpbtlcontentsView
                            where x.Id == id
                            select x;
                }



                if (query != null)
                {
                    foreach (OltpbtlcontentsView item in query)
                    {
                        content = new BTLContent
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName,
                            Name = item.Name,
                            Description = item.Description,
                            Keywords = item.Keywords,
                            DealType = item.DealType,
                            DealTypeName = this.GetDealTypeName(item.DealType),
                            ContentType = item.ContentType,
                            ContentTypeName = this.GetContentTypeName(item.ContentType),
                            ObjectiveType = item.ObjectiveType,
                            ObjectiveTypeName = this.GetObjectiveTypeName(item.ObjectiveType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = this.GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayImgId = item.DisplayImageId,
                            ViewCount = item.ViewCount,
                            SavedCount = item.SaveCount,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            IsActive = item.IsActive
                        };

                        content.PublishState = this.GetPublishState((DateTime)content.ReleaseDate, content.ExpirationDate, DateTime.UtcNow);
                    }
                }
            }
            catch (Exception e)
            {
                content = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return content;
        }

        public BTLContent Get(Guid id, int typeId, bool filterByTenant)
        {
            BTLContent content = null;

            try
            {
                var query = (dynamic)null;

                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.OltpbtlcontentsView
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.ContentType == typeId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpbtlcontentsView
                            where x.ContentType == typeId && x.Id == id
                            select x;
                }

                

                if(query != null)
                {
                    foreach(OltpbtlcontentsView item in query)
                    {
                        content = new BTLContent
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName,
                            Name = item.Name,
                            Description = item.Description,
                            Keywords = item.Keywords,
                            DealType = item.DealType,
                            DealTypeName = this.GetDealTypeName(item.DealType),
                            ContentType = item.ContentType,
                            ContentTypeName = this.GetContentTypeName(item.ContentType),
                            ObjectiveType = item.ObjectiveType,
                            ObjectiveTypeName = this.GetObjectiveTypeName(item.ObjectiveType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = this.GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayImgId = item.DisplayImageId,
                            ViewCount = item.ViewCount,
                            SavedCount = item.SaveCount,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            IsActive = item.IsActive
                        };

                        content.PublishState = this.GetPublishState((DateTime)content.ReleaseDate, content.ExpirationDate, DateTime.UtcNow);
                    }
                }
            }
            catch(Exception e)
            {
                content = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return content;
        }


        public BTLContent Post(Guid tenantId, Guid categoryId, string name, string description, string keywords, int dealType, int contentType, int objectiveType, int geoSegmentationType, Guid? displayImgId, DateTime releaseDate, DateTime expirationDate)
        {
            BTLContent content;
            try
            {
                Oltpbtlcontents newContent = new Oltpbtlcontents
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    CategoryId = categoryId,
                    Name = name,
                    Description = description,
                    Keywords = keywords,
                    DealType = dealType,
                    ContentType = contentType,
                    ObjectiveType = objectiveType,
                    GeoSegmentationType = geoSegmentationType,
                    DisplayImageId = displayImgId,
                    ViewCount = 0,
                    SaveCount = 0,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    ReleaseDate = releaseDate,
                    ExpirationDate = expirationDate,
                    IsActive = true
                };

                this._businessObjects.Context.Oltpbtlcontents.Add(newContent);
                this._businessObjects.Context.SaveChanges();

                OltpbtlcontentsView newContentView = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                      where x.Id == newContent.Id
                                                      select x).FirstOrDefault();

                if(newContentView != null)
                {
                    content = new BTLContent
                    {
                        Id = newContentView.Id,
                        TenantId = newContentView.TenantId,
                        CategoryId = newContentView.CategoryId,
                        CategoryName = newContentView.CategoryName,
                        Name = newContentView.Name,
                        Description = newContentView.Description,
                        Keywords = newContentView.Keywords,
                        DealType = newContentView.DealType,
                        DealTypeName = this.GetDealTypeName(newContentView.DealType),
                        ContentType = newContentView.ContentType,
                        ContentTypeName = this.GetContentTypeName(newContentView.ContentType),
                        ObjectiveType = newContentView.ObjectiveType,
                        ObjectiveTypeName = this.GetObjectiveTypeName(newContentView.ObjectiveType),
                        GeoSegmentationType = newContentView.GeoSegmentationType,
                        GeoSegmentationTypeName = this.GetGeoSegmentationTypeName(newContentView.GeoSegmentationType),
                        DisplayImgId = newContentView.DisplayImageId,
                        ViewCount = newContentView.ViewCount,
                        SavedCount = newContentView.SaveCount,
                        CreatedDate = newContentView.CreatedDate,
                        UpdatedDate = newContentView.UpdatedDate,
                        ReleaseDate = newContentView.ReleaseDate,
                        ExpirationDate = newContentView.ExpirationDate,
                        IsActive = newContentView.IsActive
                    };
                }
                else
                {
                    content = new BTLContent();
                }
            }
            catch(Exception e)
            {
                content = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return content;
        }

        public BTLContent Put(Guid id, Guid categoryId, string name, string description, string keywords, int objectiveType, Guid? displayImgId, DateTime releaseDate, DateTime expirationDate)
        {
            BTLContent content = null;

            try
            {

                var query = from x in this._businessObjects.Context.Oltpbtlcontents
                            where x.Id == id
                            select x;

                if(query != null)
                {
                    Oltpbtlcontents currentContent = null;

                    foreach(Oltpbtlcontents item in query)
                    {
                        currentContent = item;
                    }

                    if(currentContent != null)
                    {
                        currentContent.CategoryId = categoryId;
                        currentContent.Name = name;
                        currentContent.Description = description;
                        currentContent.Keywords = keywords;
                        currentContent.ObjectiveType = objectiveType;
                        currentContent.DisplayImageId = displayImgId;
                        currentContent.ReleaseDate = releaseDate;
                        currentContent.ExpirationDate = expirationDate;
                        currentContent.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        OltpbtlcontentsView currentContentView = (from x in this._businessObjects.Context.OltpbtlcontentsView
                                                              where x.Id == currentContent.Id
                                                              select x).FirstOrDefault();

                        if(currentContentView != null)
                        {
                            content = new BTLContent
                            {
                                Id = currentContentView.Id,
                                TenantId = currentContentView.TenantId,
                                CategoryId = currentContentView.CategoryId,
                                CategoryName = currentContentView.CategoryName,
                                Name = currentContentView.Name,
                                Description = currentContentView.Description,
                                Keywords = currentContentView.Keywords,
                                DealType = currentContentView.DealType,
                                DealTypeName = this.GetDealTypeName(currentContentView.DealType),
                                ContentType = currentContentView.ContentType,
                                ContentTypeName = this.GetContentTypeName(currentContentView.ContentType),
                                ObjectiveType = currentContentView.ObjectiveType,
                                ObjectiveTypeName = this.GetObjectiveTypeName(currentContentView.ObjectiveType),
                                GeoSegmentationType = currentContentView.GeoSegmentationType,
                                GeoSegmentationTypeName = this.GetGeoSegmentationTypeName(currentContentView.GeoSegmentationType),
                                DisplayImgId = currentContentView.DisplayImageId,
                                ViewCount = currentContentView.ViewCount,
                                SavedCount = currentContentView.SaveCount,
                                CreatedDate = currentContentView.CreatedDate,
                                UpdatedDate = currentContentView.UpdatedDate,
                                ReleaseDate = currentContentView.ReleaseDate,
                                ExpirationDate = currentContentView.ExpirationDate,
                                IsActive = currentContentView.IsActive
                            };

                        }
                        else
                        {
                            content = new BTLContent();
                        }

                    }
                }
            }
            catch (Exception e)
            {
                content = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return content;
        }

        public bool Put(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpbtlcontents
                            where x.Id == id
                            select x;

                if(query != null)
                {
                    Oltpbtlcontents content = null;

                    foreach(Oltpbtlcontents item in query)
                    {
                        content = item;
                    }

                    if(content != null)
                    {
                        content.IsActive = !content.IsActive;
                        content.UpdatedDate = DateTime.UtcNow;
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

        public Guid? Put(Guid id, Guid imgId)
        {
            Guid? oldImg = null;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpbtlcontents
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Oltpbtlcontents content = null;

                    foreach (Oltpbtlcontents item in query)
                    {
                        content = item;
                    }

                    if (content != null)
                    {
                        oldImg = content.DisplayImageId;

                        content.DisplayImageId = imgId;
                        content.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                oldImg = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return oldImg;
        }

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpbtlcontents
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Oltpbtlcontents content = null;

                    foreach (Oltpbtlcontents item in query)
                    {
                        content = item;
                    }

                    if (content != null)
                    {
                        this._businessObjects.Context.Oltpbtlcontents.Remove(content);
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
        /// Creates a new BTLContentManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public BTLContentManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD BTL CONTENT MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
