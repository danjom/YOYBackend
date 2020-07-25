using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.Structure.POCO;

namespace YOY.DAO.Entities.Manager
{
    public class BTLContentItemManager
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

        #region METHOD

        public List<BTLContentItem> Gets(Guid contentId)
        {
            List<BTLContentItem> items = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbtlcontentItems
                            where x.ContentId == contentId
                            select x;

                if (query != null)
                {

                    BTLContentItem contentItem;
                    items = new List<BTLContentItem>();

                    foreach (OltpbtlcontentItems item in query)
                    {
                        contentItem = new BTLContentItem
                        {
                            Id = item.Id,
                            ContentId = item.ContentId,
                            ReferenceURL = item.ReferenceUrl,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ContentType = item.ContentType,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ViewCount = item.ViewCount,
                            Position = item.Position,
                            ContainedProducts = item.ContainedProducts
                        };

                        items.Add(contentItem);
                    }
                }
            }
            catch (Exception e)
            {
                items = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return items;
        }

        public BTLContentItem Get(Guid referenceId, int referenceType)
        {

            BTLContentItem contentItem = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbtlcontentItems
                            where x.ReferenceType == referenceType && x.ReferenceId == referenceId
                            select x;

                if (query != null)
                {

                    foreach (OltpbtlcontentItems item in query)
                    {
                        contentItem = new BTLContentItem
                        {
                            Id = item.Id,
                            ContentId = item.ContentId,
                            ReferenceURL = item.ReferenceUrl,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ContentType = item.ContentType,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ViewCount = item.ViewCount,
                            Position = item.Position,
                            ContainedProducts = item.ContainedProducts
                        };

                    }
                }
            }
            catch (Exception e)
            {
                contentItem = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return contentItem;
        }

        public BTLContentItem Post(Guid contentId, string referenceUrl, Guid? referenceId, int referenceType, int contentType, int position, string containedProducts)
        {
            BTLContentItem item;

            try
            {

                if (contentType != BTLContentTypes.Catalog)
                {
                    IQueryable<OltpbtlcontentItems> query = from x in this._businessObjects.Context.OltpbtlcontentItems
                                where x.ContentId == contentId
                                select x;

                    if (query != null)
                    {
                        this._businessObjects.Context.OltpbtlcontentItems.RemoveRange(query);
                        this._businessObjects.Context.SaveChanges();
                    }
                }

                OltpbtlcontentItems newItem = new OltpbtlcontentItems
                {
                    Id = Guid.NewGuid(),
                    ContentId = contentId,
                    ReferenceUrl = referenceUrl,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    ContentType = contentType,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    ViewCount = 0,
                    Position = position,
                    ContainedProducts = containedProducts
                };

                this._businessObjects.Context.OltpbtlcontentItems.Add(newItem);
                this._businessObjects.Context.SaveChanges();

                item = new BTLContentItem
                {
                    Id = newItem.Id,
                    ContentId = newItem.ContentId,
                    ReferenceURL = newItem.ReferenceUrl,
                    ReferenceId = newItem.ReferenceId,
                    ReferenceType = newItem.ReferenceType,
                    ContentType = newItem.ContentType,
                    CreatedDate = newItem.CreatedDate,
                    UpdatedDate = newItem.UpdatedDate,
                    ViewCount = newItem.ViewCount,
                    Position = newItem.Position,
                    ContainedProducts = newItem.ContainedProducts
                };
            }
            catch (Exception e)
            {
                item = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return item;
        }

        public bool Put(Guid id, int newPos)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbtlcontentItems
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltpbtlcontentItems contentItem = null;

                    foreach (OltpbtlcontentItems item in query)
                    {
                        contentItem = item;
                    }

                    if (contentItem != null)
                    {
                        contentItem.Position = newPos;
                        contentItem.UpdatedDate = DateTime.UtcNow;

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

        public bool Put(Guid contentId, int oldPos, int newPos)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbtlcontentItems
                            where x.ContentId == contentId && x.Position == oldPos
                            select x;

                if (query != null)
                {
                    OltpbtlcontentItems contentItem = null;

                    foreach (OltpbtlcontentItems item in query)
                    {
                        contentItem = item;
                    }

                    if (contentItem != null)
                    {
                        contentItem.Position = newPos;
                        contentItem.UpdatedDate = DateTime.UtcNow;

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

        /// <summary>
        /// Updates the order of the complete set of items of a single content
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="positions"></param>
        /// <returns></returns>
        public bool Put(Guid contentId, List<Pair<int, int>> positions)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbtlcontentItems
                            where x.ContentId == contentId
                            select x;

                if (query != null)
                {
                    bool updated = false;

                    foreach (OltpbtlcontentItems item in query)
                    {
                        updated = false;
                        for (int i = 0; i < positions.Count && !updated; ++i)
                        {
                            if (item.Position == positions[i].Key)
                            {
                                item.Position = positions[i].Value;
                                positions.RemoveAt(i);
                                updated = true;
                            }
                        }

                        if (updated)
                        {
                            item.UpdatedDate = DateTime.UtcNow;
                            this._businessObjects.Context.SaveChanges();
                            success = true;
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

        public Guid? Put(Guid itemId, Guid referenceId, int referenceType)
        {
            Guid? oldImg = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbtlcontentItems
                            where x.Id == itemId && x.ReferenceType == referenceType
                            select x;

                if (query != null)
                {
                    OltpbtlcontentItems contentItem = null;

                    foreach (OltpbtlcontentItems item in query)
                    {
                        contentItem = item;
                    }

                    if (contentItem != null)
                    {
                        oldImg = contentItem.ReferenceId;
                        contentItem.ReferenceId = referenceId;
                        contentItem.UpdatedDate = DateTime.UtcNow;

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

        /// <summary>
        /// Deletes an element and updates the position of the ones that are after it in cardinal order.
        /// Also returns the amount of remaining elements
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(Guid id, Guid contentId)
        {
            int remainingElements = -1;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbtlcontentItems
                            where x.ContentId == contentId && x.Id == id
                            select x;

                if (query != null)
                {
                    OltpbtlcontentItems contentItem = null;

                    foreach (OltpbtlcontentItems item in query)
                    {
                        contentItem = item;
                    }

                    if (contentItem != null)
                    {

                        if (contentItem.ContentType == BTLContentTypes.Catalog)
                        {
                            //NEEDS TO UPDATE THE POSITION OF THE REST OF ELEMENTS

                            var queryItems = from x in this._businessObjects.Context.OltpbtlcontentItems
                                             where x.ContentId == contentItem.ContentId && x.Position > contentItem.Position
                                             select x;

                            remainingElements = contentItem.Position - 1;//The amount of elements in cardinal order before the one that will be deleted

                            foreach (OltpbtlcontentItems item in queryItems)
                            {
                                --item.Position;
                                ++remainingElements;
                            }
                        }
                        else
                        {
                            remainingElements = 0;
                        }

                        this._businessObjects.Context.OltpbtlcontentItems.Remove(contentItem);

                        this._businessObjects.Context.SaveChanges();

                    }
                }
            }
            catch (Exception e)
            {
                remainingElements = -1;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return remainingElements;
        }

        /// <summary>
        /// Deletes an element and updates the position of the ones that are after it in cardinal order.
        /// Also returns the amount of remaining elements
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Guid? Delete(Guid id, int contentType)
        {
            Guid? imgId = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpbtlcontentItems
                            where x.Id == id && x.ContentType == contentType
                            select x;

                if (query != null)
                {
                    OltpbtlcontentItems contentItem = null;

                    foreach (OltpbtlcontentItems item in query)
                    {
                        contentItem = item;
                    }

                    if (contentItem != null)
                    {

                        imgId = contentItem.ReferenceId;

                        if (contentItem.ContentType == BTLContentTypes.Catalog)
                        {
                            //NEEDS TO UPDATE THE POSITION OF THE REST OF ELEMENTS

                            var queryItems = from x in this._businessObjects.Context.OltpbtlcontentItems
                                             where x.ContentId == contentItem.ContentId && x.Position > contentItem.Position
                                             select x;

                            foreach (OltpbtlcontentItems item in queryItems)
                            {
                                --item.Position;
                            }
                        }

                        this._businessObjects.Context.OltpbtlcontentItems.Remove(contentItem);

                        this._businessObjects.Context.SaveChanges();

                    }
                }
            }
            catch (Exception e)
            {
                imgId = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return imgId;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new BTLContentItemManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public BTLContentItemManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD BTL CONTENT ITEM MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
