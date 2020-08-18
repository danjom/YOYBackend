using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YOY.BusinessAPI.Models.v1.PurchasedItem.POCO;
using YOY.BusinessAPI.Models.v1.PurchasedItem.SET;
using YOY.DAO.Entities;
using YOY.DTO.Entities;
using YOY.Values;

namespace YOY.BusinessAPI.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchasedItemController : ControllerBase
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
        public IActionResult Gets(Guid saleId, Guid employeeId, Guid tenantId, string userId)
        {
            IActionResult result;

            PurchasedItemSet purchasedItems = new PurchasedItemSet
            {
                Items = new List<PurchasedItemData>(),
                SaleId = saleId,
                Count = 0
            };

            string errorMsg;
            int callId = 1;
            string parameters = "EmployeeId: " + employeeId + " - UserId: " + userId + " - PurchaseId: " + saleId;

            try
            {
                if (_businessObjects == null)
                {
                    Initialize(tenantId, userId);
                }

                List<PurchaseItem> purchaseItems = this._businessObjects.Purchases.Gets(saleId, PurchaseItemRelatedReferences.PurchaseId);
                PurchasedItemData purchasedItemData;

                if (purchaseItems?.Count > 0)
                {
                    foreach (PurchaseItem item in purchaseItems)
                    {

                        purchasedItemData = new PurchasedItemData
                        {
                            Id = item.Id,
                            Quantity = item.OfferPurchasedQuantity,
                            Preferences = item.TextChosenPreferences.Replace('*','\n'),
                            Name = item.OfferMainHint + " " + item.OfferComplementaryHint + " - " + item.OfferName
                        };

                        purchasedItems.Items.Add(purchasedItemData);

                    }
                }

                if(purchasedItems.Items?.Count == 0)
                {
                    //Create dummy data
                    Random random = new Random();

                    for(int i = 0; i<5; ++i)
                    {
                        purchasedItemData = new PurchasedItemData
                        {
                            Id = Guid.Empty,
                            Quantity = random.Next(10),
                            Preferences = "Preference 1: Opcion 1, Opcion 2\nPreference 2: Opcion 1\nPreference 3: Opcion 4, Opcion 5",
                            Name = "50% De Descuento En Promos especiales de tu gusto"
                        };

                        purchasedItems.Items.Add(purchasedItemData);
                    }
                }

                result = Ok(purchasedItems);
            }
            catch (Exception e)
            {
                errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return result;

        }

        #endregion
    }
}
