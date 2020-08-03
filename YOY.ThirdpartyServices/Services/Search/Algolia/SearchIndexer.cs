using Algolia.Search.Clients;
using Algolia.Search.Models.Settings;
using YOY.DTO.Services.Search.Algolia;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YOY.ThirdpartyServices.Services.Search.Algolia
{
    public class SearchIndexer
    {
        private static SearchClient client;
        private static SearchIndex index;

        private static void Initialize(string appName, string indexName)
        {
            client = new SearchClient(appName, Settings.Default.algoliaApiKey);
            index = client.InitIndex(indexName);
        }

        public static bool SetIndexSettings(string appName, string indexName)
        {
            bool success;
            try
            {
                Initialize(appName, indexName);

                //Sets the searchable attributes



                IndexSettings settings = new IndexSettings
                {
                    SearchableAttributes = new List<string> { "name", "category", "classification", "unordered(keywords)", "detail" },
                    AttributesForFaceting = new List<string> { "type", "searchable(category)", "searchable(classification)", "filterOnly(detail)", "filterOnly(countryId)" }
                };

                index.SetSettings(settings, forwardToReplicas: true);

                /*
                index.SetSettings(
                  JObject.Parse(@"{""searchableAttributes"":[""name"",""category"",""classification"",""unordered(keywords)"",""detail""]}")
                );

                index.SetSettings(
                    JObject.Parse(@"{""attributesForFaceting"":[""type"",""searchable(category)"",""searchable(classification)""],""filterOnly(detail)"",""filterOnly(countryId)""}")
                );
                */

                success = true;
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }


            return success;
        }

        public static async Task<bool> SingleAddAsync(string appName, string indexName, SearchableObjectCore searchableObject)
        {

            bool success;
            try
            {
                Initialize(appName, indexName);

                await index.SaveObjectAsync(searchableObject);

                success = true;
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }

        public static async Task<bool> PartiallySingleUpdateAsync(string appName, string indexName, UpdateSearchableObjectCore searchableObject)
        {

            bool success;

            try
            {
                Initialize(appName, indexName);

                await index.PartialUpdateObjectAsync(searchableObject);

                success = true;
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }

        public static async Task<bool> UpdateActiveStateAsync(string appName, string indexName, UpdateSearchableObjectActiveState searchableObject)
        {

            bool success;

            try
            {
                Initialize(appName, indexName);

                await index.PartialUpdateObjectAsync(searchableObject);

                success = true;
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }

        public static async Task<bool> UpdateDateRangeAsync(string appName, string indexName, UpdateSearchableObjectDateRange searchableObject)
        {

            bool success;

            try
            {
                Initialize(appName, indexName);

                await index.PartialUpdateObjectAsync(searchableObject);

                success = true;
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }

        public static async Task<bool> UpdateCategoryDataAsync(string appName, string indexName, UpdateSearchableObjectCategoryData searchableObject)
        {

            bool success;

            try
            {
                Initialize(appName, indexName);

                await index.PartialUpdateObjectAsync(searchableObject);

                success = true;
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }

        public static async Task<bool> SingleDeleteAsync(string appName, string indexName, string id)
        {
            bool success = false;

            try
            {
                Initialize(appName, indexName);

                await index.DeleteObjectAsync(id);

            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }

        public static bool Delete(List<string> ids)
        {
            bool success = false;

            try
            {
                var res = index.DeleteObjects(ids);
                // Asynchronous
                // var res = await index.DeleteObjectsAsync(ids);
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }


    }
}
