using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.DTO.Services.Search.Algolia;
using YOY.ThirdpartyServices.Services.Search.Algolia;

namespace YOY.BusinessAPI.Handlers.Search
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

        private static int DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (Int32)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static void SetParams(string app, string index)
        {
            appName = app;
            indexName = index;
        }

        #endregion

        #region SHARED



        public static async Task<bool> UpdateSearchableObjectActiveStateAsync(Guid id, bool isActive)
        {
            bool success = false;

            try
            {

                UpdateSearchableObjectActiveState obj = new UpdateSearchableObjectActiveState
                {
                    ObjectID = id + "",
                    IsActive = isActive
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = await SearchIndexer.UpdateActiveStateAsync(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }



        public static async Task<bool> UpdateSearchableObjectDateRangeAsync(Guid id, DateTime releaseDate, DateTime? expirationDate)
        {
            bool success = false;

            try
            {

                UpdateSearchableObjectDateRange obj = new UpdateSearchableObjectDateRange
                {
                    ObjectID = id + "",
                    ReleaseDate = DateTimeToUnixTimestamp(releaseDate),
                    ExpirationDate = expirationDate != null ? DateTimeToUnixTimestamp((DateTime)expirationDate) : DateTimeToUnixTimestamp(DateTime.MaxValue)
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = await SearchIndexer.UpdateDateRangeAsync(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        public static async Task<bool> DeleteSearchableObjectAsync(string id)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = await SearchIndexer.SingleDeleteAsync(appName, indexName, id);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        #endregion

        #region GENERAL_CONTENT_INDEX

        public static async Task<bool> AddGeneralSearchableObjectAsync(Guid id, Guid tenantId, Guid countryId, string keywords, string imgUrl, bool isSponsored, bool isActive, int dealType, double relevanceRate, int usageCount, DateTime releaseDate, DateTime expirationDate, int searchableType, string searchClueKey, string details, string mainCategory, string relatedCategories, string classifications, decimal value, double cashbackPercentage)
        {
            bool success = false;

            try
            {
                SearchableObjectCore obj = new GeneralContentSearchableObject
                {
                    //CORE DATA
                    ObjectID = id + "",
                    CommerceId = tenantId + "",
                    CountryId = countryId + "",
                    Keywords = keywords,
                    ImgUrl = imgUrl,
                    IsSponsored = isSponsored,
                    IsActive = isActive,
                    DealType = dealType,
                    RelevanceRate = relevanceRate,
                    UsageCount = usageCount,
                    ReleaseDate = DateTimeToUnixTimestamp(releaseDate),
                    ExpirationDate = DateTimeToUnixTimestamp(expirationDate),
                    //GENERAL CONTENT DATA
                    SearchableObjectType = searchableType,
                    SearchClueKey = searchClueKey,
                    Details = details,
                    MainCategory = mainCategory,
                    Categories = relatedCategories,
                    Classifications = classifications,
                    Value = value,
                    CashbackPercentage = cashbackPercentage
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = await SearchIndexer.SingleAddAsync(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        public static async Task<bool> UpdateGeneralSearchableObjectAsync(Guid id, string keywords, bool isSponsored, bool isActive, int dealType, double relevanceRate, DateTime releaseDate, DateTime? expirationDate, string searchClueKey, string details, string mainCategory, string categories, string classifications, decimal value, double cashbackPercentage)
        {
            bool success = false;

            try
            {

                UpdateSearchableObjectCore obj = new UpdateGeneralContentSearchableObject
                {
                    //CORE DATA
                    ObjectID = id + "",
                    Keywords = keywords,
                    IsSponsored = isSponsored,
                    IsActive = isActive,
                    DealType = dealType,
                    RelevanceRate = relevanceRate,
                    ReleaseDate = DateTimeToUnixTimestamp(releaseDate),
                    ExpirationDate = expirationDate != null ? DateTimeToUnixTimestamp((DateTime)expirationDate) : DateTimeToUnixTimestamp(DateTime.MaxValue),
                    //GENERAL CONTENT DATA
                    SearchClueKey = searchClueKey,
                    Details = details,
                    MainCategory = mainCategory,
                    Categories = categories,
                    Classifications = classifications,
                    Value = value,
                    CashbackPercentage = cashbackPercentage
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = await SearchIndexer.PartiallySingleUpdateAsync(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        public static async Task<bool> UpdateGeneralSearchableObjectCategoryDataAsync(Guid id, string categories, string classifications)
        {
            bool success = false;

            try
            {

                UpdateSearchableObjectCategoryData obj = new UpdateSearchableObjectCategoryData
                {
                    ObjectID = id + "",
                    Categories = categories,
                    Classifications = classifications
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = await SearchIndexer.UpdateCategoryDataAsync(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        #endregion

        #region CASHINCENTIVE_INDEX

        public static async Task<bool> AddCashIncentiveSearchableObjectAsync(Guid id, Guid tenantId, Guid countryId, string keywords, string imgUrl, bool isSponsored, bool isActive, int dealType, double relevanceRate, int usageCount, DateTime releaseDate, DateTime expirationDate, int searchableType, int applyType, string applyTypeName, string searchClueKey, string details, double incentiveAmount, double previousAmount)
        {
            bool success = false;

            try
            {
                SearchableObjectCore obj = new CashbackIncentiveSearchableObject
                {
                    //CORE DATA
                    ObjectID = id + "",
                    CommerceId = tenantId + "",
                    CountryId = countryId + "",
                    Keywords = keywords,
                    ImgUrl = imgUrl,
                    IsSponsored = isSponsored,
                    IsActive = isActive,
                    DealType = dealType,
                    RelevanceRate = relevanceRate,
                    UsageCount = usageCount,
                    ReleaseDate = DateTimeToUnixTimestamp(releaseDate),
                    ExpirationDate = DateTimeToUnixTimestamp(DateTime.MaxValue),
                    //CASH INCENTIVE CONTENT DATA
                    ApplyType = applyType,
                    ApplyTypeName = applyTypeName,
                    SearchClueKey = searchClueKey,
                    Details = details,
                    IncentiveAmount = incentiveAmount,
                    PreviousAmount = previousAmount
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = await SearchIndexer.SingleAddAsync(appName, indexName, obj);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLER
            }

            return success;
        }

        public static async Task<bool> UpdateCashIncentiveSearchableObjectAsync(Guid id, string keywords, bool isSponsored, bool isActive, int dealType, double relevanceRate, DateTime releaseDate, DateTime? expirationDate, int applyType, string applyTypeName, string searchClueKey, string details, double incentiveAmount, double previousAmount)
        {
            bool success = false;

            try
            {

                UpdateSearchableObjectCore obj = new UpdateCashbackIncentiveSearchableObject
                {
                    //CORE DATA
                    ObjectID = id + "",
                    Keywords = keywords,
                    IsSponsored = isSponsored,
                    IsActive = isActive,
                    DealType = dealType,
                    RelevanceRate = relevanceRate,
                    ReleaseDate = DateTimeToUnixTimestamp(releaseDate),
                    ExpirationDate = expirationDate != null ? DateTimeToUnixTimestamp((DateTime)expirationDate) : DateTimeToUnixTimestamp(DateTime.MaxValue),
                    //CASH INCENTIVE CONTENT DATA
                    ApplyType = applyType,
                    ApplyTypeName = applyTypeName,
                    SearchClueKey = searchClueKey,
                    Details = details,
                    IncentiveAmount = incentiveAmount,
                    PreviousAmount = previousAmount
                };

                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(indexName))
                    success = await SearchIndexer.PartiallySingleUpdateAsync(appName, indexName, obj);
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