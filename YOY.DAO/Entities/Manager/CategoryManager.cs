using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.ObjectState.POCO;
using YOY.DTO.Entities.Misc.Category;

namespace YOY.DAO.Entities.Manager
{
    public class CategoryManager
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

        // ------------------------------------------------------------------------------------------------------------------------------ //
        // CLASS PUBLIC METHODS                                                                                                           //
        // ------------------------------------------------------------------------------------------------------------------------------ //
        #region CATEGORY

        private string GetHerarchyName(int herarchyLevel)
        {
            string herarchyName;
            switch (herarchyLevel)
            {
                case CategoryHerarchyLevels.Preference:
                    herarchyName = Resources.Preference;
                    break;
                case CategoryHerarchyLevels.TenantClassification:
                    herarchyName = Resources.CommerceCategory;
                    break;
                case CategoryHerarchyLevels.TenantCategory:
                    herarchyName = Resources.CommerceType;
                    break;
                case CategoryHerarchyLevels.ProductClassification:
                    herarchyName = Resources.ProductClassification;
                    break;
                case CategoryHerarchyLevels.ProductCategory:
                    herarchyName = Resources.ProductCategory;
                    break;
                default:
                    herarchyName = Resources.Undefined;
                    break;
            }

            return herarchyName;
        }

        private string GetRelevanceStatusName(int relevanceStatus)
        {
            string relevanceStatusName = "";

            switch (relevanceStatus)
            {
                case CategoryPreferenceRelevanceStatuses.Miscellaneous:
                    relevanceStatusName = Resources.Miscellaneous;
                    break;
                case CategoryPreferenceRelevanceStatuses.ThirdNeed:
                    relevanceStatusName = Resources.ThirdNeed;
                    break;
                case CategoryPreferenceRelevanceStatuses.SecondNeed:
                    relevanceStatusName = Resources.SecondNeed;
                    break;
                case CategoryPreferenceRelevanceStatuses.FirstNeed:
                    relevanceStatusName = Resources.FirstNeed;
                    break;
            }

            return relevanceStatusName;
        }

        /// <summary>
        /// Returns all the inmediate child categories of higher herarchy category
        /// </summary>
        /// <param name="PurposeType"></param>
        /// <param name="activeState"></param>
        /// <param name="parentType"></param>
        /// <param name="purposeType"></param>
        /// <returns></returns>
        public List<Category> Gets(Guid? parentId, int categoryType, int purposeType, int activeState, int pageSize, int pageNumber)
        {
            List<Category> categories = new List<Category>();

            try
            {
                var query = (dynamic)null;

                switch (categoryType)
                {
                    case CategoryTypes.All:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.PurposeType == purposeType && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.PurposeType == purposeType && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ActiveStates.Inactive:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.PurposeType == purposeType && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.PurposeType == purposeType && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ActiveStates.All:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.PurposeType == purposeType
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.PurposeType == purposeType
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    case CategoryTypes.Regular:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.PurposeType == purposeType && !x.IsSystemCategory && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.PurposeType == purposeType && !x.IsSystemCategory && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ActiveStates.Inactive:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.PurposeType == purposeType && !x.IsSystemCategory && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.PurposeType == purposeType && !x.IsSystemCategory && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ActiveStates.All:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.PurposeType == purposeType && !x.IsSystemCategory
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.PurposeType == purposeType && !x.IsSystemCategory
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    case CategoryTypes.System:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.PurposeType == purposeType && x.IsSystemCategory && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.PurposeType == purposeType && x.IsSystemCategory && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ActiveStates.Inactive:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.PurposeType == purposeType && x.IsSystemCategory && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.PurposeType == purposeType && x.IsSystemCategory && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ActiveStates.All:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.PurposeType == purposeType && x.IsSystemCategory
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.PurposeType == purposeType && x.IsSystemCategory
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    default:
                        throw new ArgumentException("No category PurposeType matches");
                }

                Category category = null;
                foreach (OltpcategoriesView item in query)
                {
                    category = new Category()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Icon = item.Icon,
                        CarrouselImg = item.CarrouselImg,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        IsSystemCategory = item.IsSystemCategory,
                        ParentCategoryId = item.ParentCategoryId,
                        PurposeType = item.PurposeType,
                        Herarchy = item.HerarchyLevel,
                        RelevanceStatus = item.RelevanceStatus,
                        RelevanceStatusName = item.HerarchyLevel == CategoryHerarchyLevels.Preference && item.RelevanceStatus != null ? this.GetRelevanceStatusName((int)item.RelevanceStatus) : "-",
                        ParentCategoryName = item.ParentCategoryId != null ? item.ParentCategoryName : "-"
                    };

                    category.HerarchyName = this.GetHerarchyName((int)item.HerarchyLevel);

                    categories.Add(category);
                }
            }
            catch (Exception e)
            {
                categories = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return categories;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Returns all the categories of the corresponding herarchy
        /// </summary>
        /// <param name="herarchyLevel"></param>
        /// <param name="categoryType"></param>
        /// <param name="purposeType"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public List<Category> Gets(int herarchyLevel, int categoryType, int purposeType, int activeState, int pageSize, int pageNumber)
        {
            List<Category> categories = new List<Category>();

            try
            {
                var query = (dynamic)null;

                switch (categoryType)
                {
                    case CategoryTypes.All:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && !x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.All:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    case CategoryTypes.Regular:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && !x.IsSystemCategory && x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && !x.IsSystemCategory && !x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.All:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && !x.IsSystemCategory
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    case CategoryTypes.System:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && x.IsSystemCategory && x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && x.IsSystemCategory && !x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.All:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && x.IsSystemCategory
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    default:
                        throw new ArgumentException("No category PurposeType matches");
                }

                Category category = null;
                foreach (OltpcategoriesView item in query)
                {
                    category = new Category()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Icon = item.Icon,
                        CarrouselImg = item.CarrouselImg,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        IsSystemCategory = item.IsSystemCategory,
                        ParentCategoryId = item.ParentCategoryId,
                        PurposeType = item.PurposeType,
                        Herarchy = item.HerarchyLevel,
                        RelevanceStatus = item.RelevanceStatus,
                        RelevanceStatusName = item.HerarchyLevel == CategoryHerarchyLevels.Preference && item.RelevanceStatus != null ? this.GetRelevanceStatusName((int)item.RelevanceStatus) : "-",
                        ParentCategoryName = item.ParentCategoryId != null ? item.ParentCategoryName : "-"
                    };

                    category.HerarchyName = this.GetHerarchyName((int)item.HerarchyLevel);

                    categories.Add(category);
                }
            }
            catch (Exception e)
            {
                categories = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return categories;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Returns all the inmediate child categories of higher herarchy category
        /// </summary>
        /// <param name="PurposeType"></param>
        /// <param name="activeState"></param>
        /// <param name="parentType"></param>
        /// <param name="purposeType"></param>
        /// <returns></returns>
        public List<Category> Gets(Guid? parentId, int categoryType, int activeState, int pageSize, int pageNumber)
        {
            List<Category> categories = new List<Category>();

            try
            {
                var query = (dynamic)null;

                switch (categoryType)
                {
                    case CategoryTypes.All:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ActiveStates.Inactive:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ActiveStates.All:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    case CategoryTypes.Regular:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && !x.IsSystemCategory && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && !x.IsSystemCategory && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ActiveStates.Inactive:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && !x.IsSystemCategory && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && !x.IsSystemCategory && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ActiveStates.All:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && !x.IsSystemCategory
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && !x.IsSystemCategory
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    case CategoryTypes.System:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.IsSystemCategory && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.IsSystemCategory && x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ActiveStates.Inactive:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.IsSystemCategory && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.IsSystemCategory && !x.IsActive
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ActiveStates.All:
                                if (parentId != null)
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == parentId && x.IsSystemCategory
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltpcategoriesView
                                             where x.ParentCategoryId == null && x.IsSystemCategory
                                             orderby x.Name ascending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    default:
                        throw new ArgumentException("No category PurposeType matches");
                }

                Category category = null;
                foreach (OltpcategoriesView item in query)
                {
                    category = new Category()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Icon = item.Icon,
                        CarrouselImg = item.CarrouselImg,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        IsSystemCategory = item.IsSystemCategory,
                        ParentCategoryId = item.ParentCategoryId,
                        PurposeType = item.PurposeType,
                        Herarchy = item.HerarchyLevel,
                        RelevanceStatus = item.RelevanceStatus,
                        RelevanceStatusName = item.HerarchyLevel == CategoryHerarchyLevels.Preference && item.RelevanceStatus != null ? this.GetRelevanceStatusName((int)item.RelevanceStatus) : "-",
                        ParentCategoryName = item.ParentCategoryId != null ? item.ParentCategoryName : "-"
                    };

                    category.HerarchyName = this.GetHerarchyName((int)item.HerarchyLevel);

                    categories.Add(category);
                }
            }
            catch (Exception e)
            {
                categories = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return categories;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// 
        /// </summary>
        /// <param name="herarchyLevel"></param>
        /// <param name="purposeType"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public List<Category> Gets(int herarchyLevel, int purposeType, int activeState, int pageSize, int pageNumber)
        {
            List<Category> categories = new List<Category>();

            try
            {
                var query = (dynamic)null;

                switch (purposeType)
                {
                    case CategoryTypes.All:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && !x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.All:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    case CategoryTypes.Regular:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && !x.IsSystemCategory && x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && !x.IsSystemCategory && !x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.All:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && !x.IsSystemCategory
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    case CategoryTypes.System:
                        switch (activeState)
                        {
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && x.IsSystemCategory && x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && x.IsSystemCategory && !x.IsActive
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            case ActiveStates.All:
                                query = (from x in this._businessObjects.Context.OltpcategoriesView
                                         where x.HerarchyLevel == herarchyLevel && x.PurposeType == purposeType && x.IsSystemCategory
                                         orderby x.Name ascending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);

                                break;
                            default:
                                throw new ArgumentException("No active status matches");
                        }
                        break;
                    default:
                        throw new ArgumentException("No category PurposeType matches");
                }

                Category category = null;
                foreach (OltpcategoriesView item in query)
                {
                    category = new Category()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Icon = item.Icon,
                        CarrouselImg = item.CarrouselImg,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        IsSystemCategory = item.IsSystemCategory,
                        ParentCategoryId = item.ParentCategoryId,
                        PurposeType = item.PurposeType,
                        Herarchy = item.HerarchyLevel,
                        RelevanceStatus = item.RelevanceStatus,
                        RelevanceStatusName = item.HerarchyLevel == CategoryHerarchyLevels.Preference && item.RelevanceStatus != null ? this.GetRelevanceStatusName((int)item.RelevanceStatus) : "-",
                        ParentCategoryName = item.ParentCategoryId != null ? item.ParentCategoryName : "-"
                    };

                    category.HerarchyName = this.GetHerarchyName((int)item.HerarchyLevel);

                    categories.Add(category);
                }
            }
            catch (Exception e)
            {
                categories = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return categories;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //



        /// <summary>
        /// Retrieves categories linked to a specific department for a tenant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Category> Gets(Guid tenantId, Guid departmentId, int activeState, int pageSize, int pageNumber)
        {
            List<Category> categories = new List<Category>();

            try
            {
                var query = (dynamic)null;

                //1st retrieve the relation between categories for this department
                switch (activeState)
                {
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.DefdepartmentCategoryView
                                 where x.TenantId == tenantId && x.DepartmentId == departmentId
                                 orderby x.CategoryName ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.DefdepartmentCategoryView
                                 where x.TenantId == tenantId && x.DepartmentId == departmentId && x.IsActive
                                 orderby x.CategoryName ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.DefdepartmentCategoryView
                                 where x.TenantId == tenantId && x.DepartmentId == departmentId && !x.IsActive
                                 orderby x.CategoryName ascending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                Category category = null;

                foreach (DefdepartmentCategoryView item in query)
                {
                    category = new Category()
                    {
                        Id = item.CategoryId,
                        Name = item.CategoryName,
                        Icon = item.CategoryIcon,
                        CarrouselImg = item.CategoryCarrouselImg,
                        Description = item.CategoryDescription,
                        CreatedDate = DateTime.MinValue,
                        UpdatedDate = DateTime.MinValue,
                        IsActive = item.CategoryIsActive,
                        IsSystemCategory = item.CategoryIsSystem,
                        ParentCategoryId = item.CategoryParentId,
                        PurposeType = item.CategoryPurposeType,
                        Herarchy = item.CategoryHerarchyLevel,
                        RelevanceStatus = 0,
                        RelevanceStatusName = "-",
                        ParentCategoryName = item.CategoryParentId != null ? item.ParentCategoryName : "-"
                    };

                    category.HerarchyName = this.GetHerarchyName((int)item.CategoryHerarchyLevel);

                    categories.Add(category);
                }
            }
            catch (Exception e)
            {
                categories = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return categories;
        }

        /// <summary>
        /// Retrieves a category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category Get(Guid id)
        {
            Category category = null;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();//New context is created because this call is part of an async logic

                var query = from x in context.OltpcategoriesView
                            where x.Id == id
                            select x;

                foreach (OltpcategoriesView item in query)
                {
                    category = new Category()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Icon = item.Icon,
                        CarrouselImg = item.CarrouselImg,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,
                        IsSystemCategory = item.IsSystemCategory,
                        ParentCategoryId = item.ParentCategoryId,
                        PurposeType = item.PurposeType,
                        Herarchy = item.HerarchyLevel,
                        RelevanceStatus = item.RelevanceStatus,
                        RelevanceStatusName = item.HerarchyLevel == CategoryHerarchyLevels.Preference && item.RelevanceStatus != null ? this.GetRelevanceStatusName((int)item.RelevanceStatus) : "-",
                        ParentCategoryName = item.ParentCategoryId != null ? item.ParentCategoryName : "-"
                    };

                    category.HerarchyName = item.HerarchyLevel != null ? this.GetHerarchyName((int)item.HerarchyLevel) : "-";
                }
            }
            catch (Exception e)
            {
                category = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return category;
        }


        /// <summary>
        /// Creates a new product category
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="herarchy"></param>
        /// <param name="isSystemCategory"></param>
        /// <param name="parentCategoryId"></param>
        /// <param name="purposeType"></param>
        /// <returns></returns>
        public Category Post(string name, string description, int herarchy, bool isSystemCategory,
            Guid? parentCategoryId, int purposeType, string icon, string carrouselImg, int? relevanceStatus)
        {
            Category newCategory = null;
            Oltpcategories category = null;

            try
            {

                if (parentCategoryId == Guid.Empty)
                {
                    parentCategoryId = null;
                }

                if (purposeType != CategoryPurposes.Tenants)
                {
                    if (herarchy == 0 && parentCategoryId != null)
                    {
                        //Is an invalid category, adds in to the context and then thorws an exception to be deleted from context as on the valid case
                        this._businessObjects.Context.Oltpcategories.Add(category);

                        throw new ArgumentException("A preference mustn't have a parent category");
                    }

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(icon) && parentCategoryId == null)
                    {
                        //Is an invalid category, adds in to the context and then thorws an exception to be deleted from context as on the valid case
                        this._businessObjects.Context.Oltpcategories.Add(category);

                        throw new ArgumentException("A preference category must have an icon");
                    }
                }

                if (parentCategoryId != null)
                {
                    var parentQuery = from x in this._businessObjects.Context.Oltpcategories
                                      where x.Id == parentCategoryId
                                      select x;

                    //Verifies if the parent category is actually a parent PurposeType one
                    bool go = true;
                    foreach (var item in parentQuery)
                    {
                        //The parent category is always in the previous herarchy level
                        if (item.HerarchyLevel != herarchy - 1)
                        {
                            go = false;
                        }

                    }

                    if (go)
                    {
                        category = new Oltpcategories
                        {
                            Id = Guid.NewGuid(),
                            Name = name,
                            Icon = icon,
                            CarrouselImg = carrouselImg,
                            Description = description,
                            IsSystemCategory = isSystemCategory,
                            IsActive = true,//By default at creation is set to active
                            HerarchyLevel = herarchy,
                            RelevanceStatus = relevanceStatus,
                            ParentCategory = parentCategoryId,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow,
                            PurposeType = purposeType
                        };

                        this._businessObjects.Context.Oltpcategories.Add(category);
                        this._businessObjects.Context.SaveChanges();

                        newCategory = new Category
                        {
                            Id = category.Id,
                            Name = category.Name,
                            Icon = category.Icon,
                            CarrouselImg = category.CarrouselImg,
                            Description = category.Description,
                            CreatedDate = category.CreatedDate,
                            UpdatedDate = category.UpdatedDate,
                            IsActive = (bool)category.IsActive,
                            Herarchy = category.HerarchyLevel,
                            RelevanceStatus = category.RelevanceStatus,
                            RelevanceStatusName = category.HerarchyLevel == CategoryHerarchyLevels.Preference && category.RelevanceStatus != null ? this.GetRelevanceStatusName((int)category.RelevanceStatus) : "-",
                            IsSystemCategory = category.IsSystemCategory,
                            ParentCategoryId = category.ParentCategory,
                            PurposeType = category.PurposeType
                        };
                    }
                    else
                    {
                        this._businessObjects.Context.Oltpcategories.Add(category);

                        throw new ArgumentException("Category's parent category isn't valid");
                    }

                }
                else
                {
                    category = new Oltpcategories
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Icon = icon,
                        CarrouselImg = carrouselImg,
                        Description = description,
                        IsSystemCategory = isSystemCategory,
                        IsActive = true,//By default at creation is set to active
                        HerarchyLevel = herarchy,
                        RelevanceStatus = relevanceStatus,
                        ParentCategory = null,//Parent category is null in here
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        PurposeType = purposeType
                    };

                    this._businessObjects.Context.Oltpcategories.Add(category);
                    this._businessObjects.Context.SaveChanges();

                    newCategory = new Category
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Icon = category.Icon,
                        CarrouselImg = category.CarrouselImg,
                        Description = category.Description,
                        CreatedDate = category.CreatedDate,
                        UpdatedDate = category.UpdatedDate,
                        IsActive = (bool)category.IsActive,
                        Herarchy = category.HerarchyLevel,
                        RelevanceStatus = category.RelevanceStatus,
                        RelevanceStatusName = category.HerarchyLevel == CategoryHerarchyLevels.Preference && category.RelevanceStatus != null ? this.GetRelevanceStatusName((int)category.RelevanceStatus) : "-",
                        IsSystemCategory = category.IsSystemCategory,
                        ParentCategoryId = category.ParentCategory,
                        PurposeType = category.PurposeType
                    };
                }


            }
            catch (Exception e)
            {
                this._businessObjects.Context.Oltpcategories.Remove(category);
                this._businessObjects.Context.SaveChanges();

                newCategory = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return newCategory;
        }//METHOD POST ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Updates a product category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="parentCategoryId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public Category Put(Guid categoryId, string name, string description, bool isActive,
             Guid? parentCategoryId, string icon, string carrouselImg, int? relevanceStatus)
        {
            Category currentCategory = null;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpcategories
                            where x.Id == categoryId
                            select x;

                Oltpcategories category = null;
                foreach (Oltpcategories item in query)
                {
                    category = item;
                }

                if (category != null)
                {
                    //Needs to determine if the update of the object is valid
                    if ((category.HerarchyLevel == 0 && parentCategoryId == null) || category.HerarchyLevel > 0)
                    {
                        if (parentCategoryId != null)
                        {
                            var parentQuery = from x in this._businessObjects.Context.Oltpcategories
                                              where x.Id == parentCategoryId
                                              select x;

                            bool go = true;

                            foreach (var item in parentQuery)
                            {
                                if (item.ParentCategory == categoryId)
                                {
                                    go = false;
                                }
                            }

                            if (go)
                            {
                                category.Name = name;
                                category.Icon = icon;
                                category.CarrouselImg = carrouselImg;
                                category.Description = description;
                                category.IsActive = isActive;
                                category.ParentCategory = parentCategoryId;
                                category.RelevanceStatus = relevanceStatus;
                                category.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();

                                currentCategory = new Category
                                {
                                    Id = category.Id,
                                    Name = category.Name,
                                    Icon = category.Icon,
                                    CarrouselImg = category.CarrouselImg,
                                    Description = category.Description,
                                    CreatedDate = category.CreatedDate,
                                    UpdatedDate = category.UpdatedDate,
                                    IsActive = (bool)category.IsActive,
                                    Herarchy = category.HerarchyLevel,
                                    RelevanceStatus = category.RelevanceStatus,
                                    RelevanceStatusName = category.HerarchyLevel == CategoryHerarchyLevels.Preference && category.RelevanceStatus != null ? this.GetRelevanceStatusName((int)category.RelevanceStatus) : "-",
                                    IsSystemCategory = category.IsSystemCategory,
                                    ParentCategoryId = category.ParentCategory
                                };
                            }
                            else
                            {
                                throw new ArgumentException("Parent category can't be a child category, it causes cycles");
                            }
                        }
                        else
                        {
                            category.Name = name;
                            category.Description = description;
                            category.IsActive = isActive;
                            category.ParentCategory = null;//Parent category sent is null
                            category.RelevanceStatus = relevanceStatus;
                            category.UpdatedDate = DateTime.UtcNow;

                            this._businessObjects.Context.SaveChanges();

                            currentCategory = new Category
                            {
                                Id = category.Id,
                                Name = category.Name,
                                Description = category.Description,
                                CreatedDate = category.CreatedDate,
                                UpdatedDate = category.UpdatedDate,
                                IsActive = (bool)category.IsActive,
                                Herarchy = category.HerarchyLevel,
                                RelevanceStatus = category.RelevanceStatus,
                                RelevanceStatusName = category.HerarchyLevel == CategoryHerarchyLevels.Preference && category.RelevanceStatus != null ? this.GetRelevanceStatusName((int)category.RelevanceStatus) : "-",
                                IsSystemCategory = category.IsSystemCategory,
                                ParentCategoryId = category.ParentCategory
                            };
                        }

                    }
                    else
                    {
                        throw new ArgumentException("A preference mustn't have a parent category");
                    }

                }

            }
            catch (Exception e)
            {
                currentCategory = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return currentCategory;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Changes category's active state
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="changeType"></param>
        /// <returns></returns>
        public ObjectStateUpdate Put(Guid categoryId, int changeType)
        {
            ObjectStateUpdate result = new ObjectStateUpdate();

            try
            {
                var query = from x in this._businessObjects.Context.Oltpcategories
                            where x.Id == categoryId
                            select x;

                Oltpcategories category = null;
                foreach (Oltpcategories item in query)
                {
                    category = item;
                }

                if (category != null)
                {
                    switch (changeType)
                    {
                        case ChangeTypes.ActiveState:
                            category.IsActive = !category.IsActive;
                            category.UpdatedDate = DateTime.UtcNow;
                            this._businessObjects.Context.SaveChanges();

                            result.NewState = (bool)category.IsActive;
                            result.Success = true;
                            break;
                    }

                }

            }
            catch (Exception e)
            {
                result.Success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return result;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Deletes a product category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                Oltpcategories category = null;

                var query = from x in this._businessObjects.Context.Oltpcategories
                            where x.Id == id
                            select x;

                foreach (var item in query)
                {
                    category = item;
                }

                if (category != null)
                {
                    this._businessObjects.Context.Oltpcategories.Remove(category);
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
        }//METHOD DELETE ENDS --------------------------------------------------------------------------------------------------------------------------- //


        public string GetPreferenceName(Guid id, int herarchyLevel)
        {
            string preferenceName = "";

            try
            {

                switch (herarchyLevel)
                {
                    case CategoryHerarchyLevels.ProductCategory:
                        preferenceName = this._businessObjects.StoredProcsHandler.GetPreferenceNameForProductCategory(id, herarchyLevel);
                        break;
                    case CategoryHerarchyLevels.TenantCategory:
                        preferenceName = this._businessObjects.StoredProcsHandler.GetPreferenceNameForCommerceCategory(id, herarchyLevel);
                        break;
                }



                if (string.IsNullOrEmpty(preferenceName))
                {
                    preferenceName = "*";
                }
            }
            catch (Exception e)
            {
                preferenceName = "*";
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }


            return preferenceName;
        }

        public string GetParentCategory(Guid id, int herarchyLevel)
        {
            string parentName;
            try
            {
                parentName = this._businessObjects.StoredProcsHandler.GetParentCategoryName(id, herarchyLevel);

                if (string.IsNullOrEmpty(parentName))
                {
                    parentName = "*";
                }
            }
            catch (Exception e)
            {
                parentName = "*";
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }


            return parentName;
        }

        #endregion

        #region CATEGORYRELATIONS

        public List<CategoryRelation> Gets(Guid referenceId, int referenceType)
        {
            List<CategoryRelation> categoryRelations = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpcategoryRelationsView
                            where x.ReferenceType == referenceType && x.ReferenceId == referenceId
                            select x;

                if (query != null)
                {
                    categoryRelations = new List<CategoryRelation>();
                    CategoryRelation categoryRelation;

                    foreach (OltpcategoryRelationsView item in query)
                    {
                        categoryRelation = new CategoryRelation
                        {
                            Id = item.Id,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName,
                            HerarchyLevel = item.HerarchyLevel,
                            ParentCategoryId = item.ParentCategoryId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            GeneratorRelationId = item.GeneratorRelationId
                        };

                        categoryRelations.Add(categoryRelation);
                    }
                }
            }
            catch (Exception e)
            {
                categoryRelations = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return categoryRelations;
        }

        public List<EnabledCategoryForRelation> Gets(int referenceType, Guid referenceId)
        {
            List<EnabledCategoryForRelation> enabledCategories = null;

            try
            {
                var query = (dynamic)null;

                switch (referenceType)
                {
                    case CategoryRelationTypes.Tenant:
                        //PENDING!!!!!!
                        break;
                    case CategoryRelationTypes.Offer:
                        query = from x in this._businessObjects.Context.EnabledProductCategoriesByTenantCategoryRelationView
                                where x.ReferenceType == referenceType && x.ReferenceId == referenceId
                                select x;
                        break;
                }

                if(query != null)
                {
                    enabledCategories = new List<EnabledCategoryForRelation>();
                    EnabledCategoryForRelation enabledCategory;
                    
                    foreach(EnabledProductCategoriesByTenantCategoryRelationView item in query)
                    {
                        enabledCategory = new EnabledCategoryForRelation
                        {
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ReferenceMainCategoryId = item.ReferenceMainCategoryId,
                            TenantId = item.TenantId,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName,
                            HerarchyLevel = item.HerarchyLevel,
                            RelationReferenceId = item.RelationReferenceId,
                            RelationReferenceType = item.RelationReferenceType
                        };

                        enabledCategories.Add(enabledCategory);
                    }
                }
            }
            catch(Exception e)
            {
                enabledCategories = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return enabledCategories;
        }

        public bool Post(Guid categoryId, int herarchyLevel, Guid referenceId, int referenceType)
        {
            bool success;
            try
            {
                OltpcategoryRelations newCategoryRelation = new OltpcategoryRelations
                {
                    Id = Guid.NewGuid(),
                    CategoryId = categoryId,
                    HerarchyLevel = herarchyLevel,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    GeneratorRelationId = null,
                    CreatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpcategoryRelations.Add(newCategoryRelation);

                //Now needs to generate the preference relation
                Guid preferenceId = Guid.Empty;

                switch (referenceType)
                {
                    case CategoryRelatiomReferenceTypes.Tenant:

                        preferenceId = this._businessObjects.StoredProcsHandler.GetPreferenceIdForCommerceCategory(categoryId, herarchyLevel) ;

                        break;
                    case CategoryRelatiomReferenceTypes.Offer:

                        preferenceId = this._businessObjects.StoredProcsHandler.GetPreferenceIdForProductCategory(categoryId, herarchyLevel);


                        break;
                }

                if(preferenceId != Guid.Empty)
                {
                    OltpcategoryRelations newPreferenceRelation = new OltpcategoryRelations
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = preferenceId,
                        HerarchyLevel = CategoryHerarchyLevels.Preference,
                        ReferenceId = referenceId,
                        ReferenceType = referenceType,
                        GeneratorRelationId = newCategoryRelation.Id,
                        CreatedDate = DateTime.UtcNow
                    };
                }
                
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

        public bool Delete(Guid categoryId, Guid referenceId, int referenceType)
        {
            bool success = false;

            try
            {

                OltpcategoryRelations categoryRelation = (from x in this._businessObjects.Context.OltpcategoryRelations
                                                         where x.CategoryId == categoryId && x.ReferenceType == referenceType && x.ReferenceId == referenceId
                                                         select x).FirstOrDefault();

                if (categoryRelation != null)
                {

                    //1st deletes the relations
                    var query = from x in this._businessObjects.Context.OltpcategoryRelations
                                where x.GeneratorRelationId == categoryRelation.Id
                                select x;

                    foreach (OltpcategoryRelations item in query)
                    {
                        this._businessObjects.Context.OltpcategoryRelations.Remove(item);
                    }

                    this._businessObjects.Context.OltpcategoryRelations.Remove(categoryRelation);

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
        public CategoryManager(BusinessObjects businessObjects)
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
