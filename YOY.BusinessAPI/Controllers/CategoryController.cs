using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YOY.BusinessAPI.Handlers.Search;
using YOY.BusinessAPI.Models.v1.Category.POCO;
using YOY.BusinessAPI.Models.v1.Category.SET;
using YOY.BusinessAPI.Models.v1.Misc.BasicResponse.POCO;
using YOY.DAO.Entities;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Category;
using YOY.Values;

namespace YOY.BusinessAPI.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        #region PROPERTIES_AND_RESOURCES
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private static Tenant _tenant;
        private BusinessObjects _businessObjects;

        private const int controllerVersion = 1;

        private const int maxRetrievedCategories = 300;

        #endregion

        #region METHODS

        private void Initialize(Guid commerceId, string userId)
        {
            //1st initialize in order to get tenant data
            _tenant = Tenant.GetInstance(Guid.Empty);
            _businessObjects = BusinessObjects.GetInstance(_tenant);

            if (commerceId != Guid.Empty)
            {

                int utcTimeDiff = int.MinValue;

                TenantInfo currentTenant = this._businessObjects.Commerces.Get(commerceId, CommerceKeys.TenantKey);

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    UserData user = this._businessObjects.Users.Get(userId, UserKeys.UserId);
                    utcTimeDiff = user.UtcTimeDiff;
                }

                _tenant = Tenant.GetInstance(currentTenant.TenantId, currentTenant.CategoryId, currentTenant.CurrencySymbol, utcTimeDiff, currentTenant.DefaultCommissionFeePercentage);
            }
            else
            {
                _tenant = Tenant.GetInstance(commerceId, Guid.Empty, "", int.MinValue, 0);
            }


            _businessObjects = BusinessObjects.GetInstance(_tenant);
        }

        [Route("gets")]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult Gets(Guid dealId, Guid employeeId, Guid tenantId, string userId)
        {
            IActionResult result;

            CategoriesForDeal categories = new CategoriesForDeal
            {
                Categories = new List<CategoryData>(),
                DealId = dealId,
                Count = 0
            };

            string errorMsg;
            int callId = 1;
            string parameters = "EmployeeId: " + employeeId + " - UserId: " + userId + " - OfferId: " + dealId;

            try
            {
                if (_businessObjects == null)
                {
                    Initialize(tenantId, userId);
                }

                if (dealId != Guid.Empty)
                {
                    List<EnabledCategoryForRelation> categoriesForRelation = this._businessObjects.Categories.Gets(CategoryRelationTypes.Offer, dealId, maxRetrievedCategories);

                    if (categoriesForRelation?.Count > 0)
                    {
                        CategoryData categoryData;

                        foreach (EnabledCategoryForRelation item in categoriesForRelation)
                        {
                            categoryData = new CategoryData
                            {
                                Id = item.CategoryId,
                                Name = item.CategoryName,
                                Selected = false
                            };

                            if (item.RelationReferenceId != null)
                                categoryData.Selected = true;

                            categories.Categories.Add(categoryData);

                        }

                        categories.Count = categories.Categories?.Count ?? 0;
                    }
                }
                else
                {
                    List<EnabledCategoryForRelation> categoriesForRelation = this._businessObjects.Categories.Gets();

                    if (categoriesForRelation?.Count > 0)
                    {
                        CategoryData categoryData;

                        foreach (EnabledCategoryForRelation item in categoriesForRelation)
                        {
                            categoryData = new CategoryData
                            {
                                Id = item.CategoryId,
                                Name = item.CategoryName,
                                Selected = false
                            };

                            categories.Categories.Add(categoryData);

                        }

                        categories.Count = categories.Categories?.Count ?? 0;
                    }
                }

                result = Ok(categories);
            }
            catch(Exception e)
            {
                errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return result;
        }

        /// <summary>
        /// This call creates a set of categories linked to an offer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("post")]
        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CategoryOpDataList model)
        {
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            Initialize(model.TenantId, "");
            IActionResult result;

            if (!ModelState.IsValid && model.DealId != Guid.Empty && !string.IsNullOrWhiteSpace(model.UserId) && model.DealId != Guid.Empty && model.Categories?.Count > 0)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);

                result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    ErrorCode = Values.StatusCodes.BadRequest,
                                    ShowErrorToUser = false,
                                    InnerError = "Invalid Payload",
                                    PublicError = ""
                                });

            }
            else
            {
                try
                {
                    bool success;
                    bool valid = true;

                    foreach(CategoryOpData item in model.Categories)
                    {
                        success = this._businessObjects.Categories.Post(item.Id, CategoryHerarchyLevels.ProductCategory, model.DealId, CategoryRelationReferenceTypes.Offer);

                        if (!success)
                            valid = false;
                    }

                    if (valid)
                    {
                        //Needs to update offer categories on search index

                        Offer updatedOffer = this._businessObjects.Offers.Get(model.DealId, true);

                        if (updatedOffer.DisplayType > DisplayTypes.BroadcastingOnly)
                        {
                            //Need to retrieve all categories
                            List<CategoryRelation> categoryRelations = this._businessObjects.Categories.Gets(updatedOffer.Id, CategoryRelationTypes.Offer);

                            string categories = "";
                            string classifications = "";

                            if (categoryRelations?.Count > 0)
                            {
                                foreach (CategoryRelation relation in categoryRelations)
                                {
                                    if (relation.HerarchyLevel == CategoryHerarchyLevels.ProductCategory)
                                    {
                                        categories += relation.CategoryName;
                                        classifications += this._businessObjects.Categories.GetParentCategory(relation.CategoryId, CategoryHerarchyLevels.ProductCategory);
                                    }
                                }
                            }

                            string indexName;

                            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                            {
                                indexName = SearchIndexNames.DevAppend + SearchIndexNames.GeneralContent;

                            }
                            else
                            {
                                indexName = SearchIndexNames.ProdAppend + SearchIndexNames.GeneralContent;
                            }

                            SearchObjectHandler.SetParams(SearchIndexNames.AppName, indexName);

                            await SearchObjectHandler.UpdateGeneralSearchableObjectCategoryDataAsync(updatedOffer.Id, categories, classifications);

                        }

                        SuccessResponse response = new SuccessResponse
                        {
                            StatusCode = Values.StatusCodes.Ok,
                            ShowMsgToUser = true,
                            MessageToDisplay = "Agregación completada éxitosamente"
                        };

                        result = Ok(response);
                    }
                    else
                        result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                }
                catch(Exception e)
                {
                    errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }
            }

            return result;
        }

        /// <summary>
        /// This call deletes a list of categories linked to an offer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("delete")]
        [HttpDelete]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromBody] CategoryOpDataList model)
        {
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            Initialize(model.TenantId, "");
            IActionResult result;

            if (!ModelState.IsValid && model.DealId != Guid.Empty && !string.IsNullOrWhiteSpace(model.UserId) && model.DealId != Guid.Empty && model.Categories?.Count > 0)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);

                result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    ErrorCode = Values.StatusCodes.BadRequest,
                                    ShowErrorToUser = false,
                                    InnerError = "Invalid Payload",
                                    PublicError = ""
                                });

            }
            else
            {
                try
                {
                    bool success;
                    bool valid = true;

                    foreach (CategoryOpData item in model.Categories)
                    {
                        success = this._businessObjects.Categories.Delete(item.Id, model.DealId, CategoryRelationReferenceTypes.Offer);

                        if (!success)
                            valid = false;
                    }

                    if (valid)
                    {
                        //Needs to update offer categories on search index

                        Offer updatedOffer = this._businessObjects.Offers.Get(model.DealId, true);

                        if (updatedOffer.DisplayType > DisplayTypes.BroadcastingOnly)
                        {
                            //Need to retrieve all categories
                            List<CategoryRelation> categoryRelations = this._businessObjects.Categories.Gets(updatedOffer.Id, CategoryRelationTypes.Offer);

                            string categories = "";
                            string classifications = "";

                            if (categoryRelations?.Count > 0)
                            {
                                foreach (CategoryRelation relation in categoryRelations)
                                {
                                    if (relation.HerarchyLevel == CategoryHerarchyLevels.ProductCategory)
                                    {
                                        categories += relation.CategoryName;
                                        classifications += this._businessObjects.Categories.GetParentCategory(relation.CategoryId, CategoryHerarchyLevels.ProductCategory);
                                    }
                                }
                            }

                            string indexName;

                            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                            {
                                indexName = SearchIndexNames.DevAppend + SearchIndexNames.GeneralContent;

                            }
                            else
                            {
                                indexName = SearchIndexNames.ProdAppend + SearchIndexNames.GeneralContent;
                            }

                            SearchObjectHandler.SetParams(SearchIndexNames.AppName, indexName);

                            await SearchObjectHandler.UpdateGeneralSearchableObjectCategoryDataAsync(updatedOffer.Id, categories, classifications);

                        }

                        SuccessResponse response = new SuccessResponse
                        {
                            StatusCode = Values.StatusCodes.Ok,
                            ShowMsgToUser = true,
                            MessageToDisplay = "Eliminación completada éxitosamente"
                        };


                        result = Ok(response);
                    }
                    else
                        result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                }
                catch (Exception e)
                {
                    errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }
            }

            return result;
        }

        #endregion
    }
}
