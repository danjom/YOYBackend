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

        public static bool SingleAdd(string appName, string indexName, SearchableObject searchableObject)
        {

            bool success;
            try
            {
                Initialize(appName, indexName);

                List<JObject> objs = new List<JObject>();
                JObject currentObj = (JObject)JToken.FromObject(searchableObject);
                objs.Add(currentObj);

                //JObject res = index.AddObjects(objs);
                index.SaveObjects(objs, autoGenerateObjectId: false);

                // Asynchronous
                // var res = await index.AddObjectsAsync(objs);

                success = true;
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }

        public static async Task<bool> Add(string appName, string indexName, List<SearchableObject> searchableObjects)
        {

            bool success;
            try
            {
                Initialize(appName, indexName);

                List<JObject> objs = new List<JObject>();

                foreach (SearchableObject searchableObject in searchableObjects)
                {
                    JObject currentObj = (JObject)JToken.FromObject(searchableObject);
                    objs.Add(currentObj);
                }

                //JObject res = index.AddObjects(objs);
                // Asynchronous
                //JObject res = await index.AddObjectsAsync(objs);

                await index.SaveObjectsAsync(objs, autoGenerateObjectId: true);

                success = true;
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }

        public static bool Update(string appName, string indexName, SearchableObjectData searchableObject)
        {

            bool success;

            try
            {
                Initialize(appName, indexName);

                JObject currentObj =  (JObject)JToken.FromObject(searchableObject);

                //JObject res = index.PartialUpdateObject(currentObj, false);

                List<JObject> objs = new List<JObject>
                {
                    currentObj
                };

                index.PartialUpdateObjects(objs);

                // Asynchronous
                // await index.PartialUpdateObjectAsync(JObject.Parse(@"{""city"":""San Francisco"",
                //                                                       ""objectID"":""myID""}"), false)

                success = true;
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }

        public static bool Delete(string appName, string indexName, string id)
        {
            bool success = false;

            try
            {
                Initialize(appName, indexName);

                var res = index.DeleteObject(id);
                // Asynchronous
                // var res = await index.DeleteObjectAsync(ids);
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
