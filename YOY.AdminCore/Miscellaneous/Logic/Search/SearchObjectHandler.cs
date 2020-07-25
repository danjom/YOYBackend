using YOY.DTO.Services.Search.Algolia;
using YOY.ThirdpartyServices.Services.Search.Algolia;
using System;

namespace YOY.AdminCore.Miscellaneous.Logic.Search
{
    public class SearchObjectHandler
    {

        #region PROPERTIES_AND_RESOURCES
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private static string appName;
        private static string indexName;

        #endregion

        #region METHODS

        private static int DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (Int32)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static void SetParams(string app, string index)
        {
            appName = app;
            indexName = index;
        }

        public static bool AddObject(Guid id, Guid tenantId, string name, int type, string img, string category, string keywords, string details, bool isActive, DateTime releaseDate, DateTime? expirationDate)
        {
            bool success = false;

            try
            {
                SearchableObject obj = new SearchableObject
                {
                    objectID = id + "",
                    commerceId = tenantId + "",
                    type = type,
                    name = name,
                    keywords = keywords,
                    category = category,
                    details = details,
                    isActive = isActive ? "1" : "0",
                    releaseDate = DateTimeToUnixTimestamp(releaseDate),
                    expirationDate = expirationDate != null ? DateTimeToUnixTimestamp((DateTime)expirationDate) : DateTimeToUnixTimestamp(DateTime.MaxValue),
                    icon = img
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = SearchIndexer.SingleAdd(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        public static bool UpdateObject(Guid id, string name, int type, string category, string keywords, string details, DateTime releaseDate, DateTime? expirationDate)
        {
            bool success = false;

            try
            {


                SearchableObjectData obj = new SearchableBaseObject
                {
                    objectID = id + "",
                    type = type,
                    name = name,
                    keywords = keywords,
                    category = category,
                    details = details,
                    releaseDate = DateTimeToUnixTimestamp(releaseDate),
                    expirationDate = expirationDate != null ? DateTimeToUnixTimestamp((DateTime)expirationDate) : DateTimeToUnixTimestamp(DateTime.MaxValue)

                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = SearchIndexer.Update(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        public static bool UpdateObject(Guid id, string img)
        {
            bool success = false;

            try
            {

                SearchableObjectData obj = new SearchableObjectIcon
                {
                    objectID = id + "",
                    icon = img
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = SearchIndexer.Update(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        public static bool UpdateObject(Guid id, bool isActive)
        {
            bool success = false;

            try
            {


                SearchableObjectData obj = new SearchableObjectActiveState
                {
                    objectID = id + "",
                    isActive = isActive ? "1" : "0"
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = SearchIndexer.Update(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        public static bool DeleteObject(string id)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = SearchIndexer.Delete(appName, indexName, id);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        #endregion
    }
}