using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.Values;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class KeywordManager
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

        /// <summary>
        /// Retrieves all the keywords related to a category
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public List<Keyword> Gets(Guid? categoryId)
        {
            List<Keyword> keywords = null;

            try
            {
                var query = (dynamic)null;

                if (categoryId != null)
                {
                    query = from x in this._businessObjects.Context.DefkeywordsView
                            where x.CategoryId == categoryId
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.DefkeywordsView
                            select x;
                }

                if (query != null)
                {
                    Keyword keyword;
                    keywords = new List<Keyword>();

                    foreach (DefkeywordsView item in query)
                    {
                        keyword = new Keyword
                        {
                            Id = item.Id,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName ?? "",
                            CategoryRelevanceStatus = item.CategoryRelevanceStatus,
                            ParentCategoryId = item.ParentCategoryId,
                            Word = item.Word,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            IsActive = item.IsActive,
                            Language = item.Language
                        };

                        keywords.Add(keyword);
                    }
                }


            }
            catch (Exception e)
            {
                keywords = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return keywords;
        }

        /// <summary>
        /// Retrieves all the keywords related to a category
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public List<Keyword> Gets(string word, bool exactMatch)
        {
            List<Keyword> keywords = null;

            try
            {
                var query = (dynamic)null;

                if (exactMatch)
                {
                    query = from x in this._businessObjects.Context.DefkeywordsView
                            where x.Word == word
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.DefkeywordsView
                            where x.Word.Contains(word) || word.Contains(x.Word)
                            select x;
                }

                if (query != null)
                {
                    Keyword keyword;
                    keywords = new List<Keyword>();

                    foreach (DefkeywordsView item in query)
                    {
                        keyword = new Keyword
                        {
                            Id = item.Id,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName ?? "",
                            CategoryRelevanceStatus = item.CategoryRelevanceStatus,
                            ParentCategoryId = item.ParentCategoryId,
                            Word = item.Word,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            IsActive = item.IsActive,
                            Language = item.Language
                        };

                        keywords.Add(keyword);
                    }
                }


            }
            catch (Exception e)
            {
                keywords = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return keywords;
        }

        /// <summary>
        /// Retrieves all the keywords related to a category
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public Keyword Gets(Guid id)
        {
            Keyword keyword = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefkeywordsView
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (DefkeywordsView item in query)
                    {
                        keyword = new Keyword
                        {
                            Id = item.Id,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName ?? "",
                            CategoryRelevanceStatus = item.CategoryRelevanceStatus,
                            ParentCategoryId = item.ParentCategoryId,
                            Word = item.Word,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            IsActive = item.IsActive,
                            Language = item.Language
                        };

                    }
                }


            }
            catch (Exception e)
            {
                keyword = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return keyword;
        }

        public Keyword Post(Guid? categoryId, string word, int language)
        {
            Keyword keyword = null;

            try
            {
                Defkeywords newKeyword = new Defkeywords
                {
                    Id = Guid.NewGuid(),
                    Word = word,
                    CategoryId = categoryId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true,//At creation is active by default
                    Language = language
                };

                this._businessObjects.Context.Defkeywords.Add(newKeyword);
                this._businessObjects.Context.SaveChanges();

                var newKeywordView = (from x in this._businessObjects.Context.DefkeywordsView
                                      where x.Id == newKeyword.Id
                                      select x).FirstOrDefault();

                if(newKeywordView != null)
                {

                    keyword = new Keyword
                    {
                        Id = newKeywordView.Id,
                        CategoryId = newKeywordView.CategoryId,
                        CategoryName = newKeywordView.CategoryName ?? "",
                        CategoryRelevanceStatus = newKeywordView.CategoryRelevanceStatus,
                        ParentCategoryId = newKeywordView.ParentCategoryId,
                        Word = newKeywordView.Word,
                        CreatedDate = newKeywordView.CreatedDate,
                        UpdatedDate = newKeywordView.UpdatedDate,
                        IsActive = newKeywordView.IsActive,
                        Language = newKeywordView.Language
                    };
                }
            }
            catch (Exception e)
            {
                keyword = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return keyword;
        }

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defkeywords
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (Defkeywords item in query)
                    {
                        this._businessObjects.Context.Defkeywords.Remove(item);
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
        }

        #endregion

        #region PREFERENCES_AND_COMMERCES

        public string Gets(Guid referenceId, int herarchyLevel)
        {
            string keywords = "";

            try
            {
                keywords = this._businessObjects.StoredProcsHandler.GetCategoryKeywords(referenceId, herarchyLevel);


                if (!string.IsNullOrEmpty(keywords))
                {
                    if (keywords.ElementAt(0) == ',')
                    {
                        keywords = keywords.Substring(1);
                    }
                    if (keywords.ElementAt(keywords.Length - 1) == ',')
                    {
                        keywords = keywords.Substring(0, keywords.Length - 1);
                    }
                }
                else
                {
                    keywords = "";
                }
            }
            catch (Exception e)
            {
                keywords = "";
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }


            return keywords;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new KeywordManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public KeywordManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD TABLE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
