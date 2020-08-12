using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YOY.BusinessAPI.Handlers.Search;
using YOY.BusinessAPI.Models.v1.CashIncentive.POCO;
using YOY.BusinessAPI.Models.v1.CashIncentive.SET;
using YOY.BusinessAPI.Models.v1.Misc.BasicResponse.POCO;
using YOY.DAO.Entities;
using YOY.DAO.Entities.Manager.Misc.Image;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.ObjectState.POCO;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.BusinessAPI.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CashIncentiveController : ControllerBase
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

        private const int mainHintMinLength = 3;
        private const int mainHintMaxLength = 10;
        private const int complementaryHintMinLength = 3;
        private const int complementaryHintMaxLength = 22;
        private const int nameMinLength = 5;
        private const int nameMaxLength = 60;
        private const int descriptionMinLength = 10;
        private const int descriptionMaxLength = 300;
        private const int keywordsMaxLength = 512;
        private const int availableQuantityMinValue = 30;
        public const int infiteAvailableQuantity = -1;
        public const int infiteMaxUsagesPerUser = -1;

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

        private IncentiveData GetIncentiveContent(CashIncentive data)
        {
            IncentiveData incentive = new IncentiveData
            {
                Id = data.Id,
                TenantId = data.TenantId,
                ApplyType = data.ApplyType,
                BenefitAmountType = data.BenefitAmountType,
                BenefitAmountTypeName = data.BenefitAmountTypeName,
                DisplayType = data.DisplayType,
                DisplayTypeName = data.DisplayTypeName,
                Type = data.Type,
                TypeName = data.TypeName,
                DealType = data.DealType,
                DealTypeName = data.DealTypeName,
                MaxCombinedIncentives = data.MaxCombinedIncentives,
                UnitValue = data.UnitValue,
                PreviousUnitValue = data.PreviousUnitValue,
                MinMembershipLevel = data.MinMembershipLevel,
                MinMembershipLevelName = data.MinMembershipLevelName,
                MinPurchasedAmount = data.MinPurchasedAmount,
                PurchasedAmountBlock = data.PurchasedAmountBlock,
                MaxValue = data.MaxValue,
                AvailableQuantity = data.AvailableQuantity,
                Name = data.Name,
                MainHint = data.MainHint,
                ComplementaryHint = data.ComplementaryHint,
                Description = data.Description,
                Keywords = data.Keywords,
                IsActive = data.IsActive,
                IsSponsored = data.IsSponsored,
                ValidHours = data.ValidHours,
                ValidWeekDays = data.ValidWeekDays,
                ValidMonthDays = data.ValidMonthDays,
                MaxUsagePerUser = data.MaxUsagePerUser,
                MinPurchasesCountToUse = data.MinPurchasesCountToUse,
                MinPurchasedTotalAmount = data.MinPurchasedTotalAmount,
                UsageCount = data.UsageCount,
                GeoSegmentationType = data.GeoSegmentationType,
                GeoSegmentationTypeName = data.GeoSegmentationTypeName,
                Rules = data.Rules,
                Conditions = data.Conditions,
                RelevanceRate = data.RelevanceRate,
                ReleaseFullDateTime = UtcToLocal((DateTime)data.ReleaseDate, this._businessObjects.Tenant.UtcTimeDiff),
                ExpirationFullDateTime = UtcToLocal(data.ExpirationDate, this._businessObjects.Tenant.UtcTimeDiff),
                PublishState = data.PublishState
            };

            incentive.ReleaseDateComponent = incentive.ReleaseFullDateTime.ToString("yyyy-MM-dd");
            incentive.ReleaseHourComponent = incentive.ReleaseFullDateTime.ToString("hh:mm tt");

            incentive.ExpirationDateComponent = incentive.ExpirationFullDateTime.ToString("yyyy-MM-dd");
            incentive.ExpirationHourComponent = incentive.ExpirationFullDateTime.ToString("hh:mm tt");

            return incentive;
        }

        public DateTime LocalToUtc(DateTime date, int utcTimeDiff)
        {
            DateTime finalDate = date.AddHours(utcTimeDiff * -1);


            return finalDate;
        }

        public DateTime UtcToLocal(DateTime date, int utcTimeDiff)
        {
            //ADDS THE UTC TIME DIFF TO MEET LOCAL DATE
            DateTime finalDate = date.AddHours(utcTimeDiff);

            return finalDate;
        }

        private List<IncentiveData> GetIncentivesData(int pageSize, int pageNumber, DateTime start, DateTime end)
        {
            List<IncentiveData> incentivesData;

            try
            {
                List<CashIncentive> incentives = this._businessObjects.CashIncentives.Gets(ExpiredStates.All, ActiveStates.All, ReleaseStates.All, DateTime.UtcNow, pageSize, pageNumber);

                if (incentives?.Count > 0)
                {
                    incentivesData = new List<IncentiveData>();

                    foreach (CashIncentive item in incentives)
                    {
                        incentivesData.Add(GetIncentiveContent(item));
                    }
                }
                else
                {
                    incentivesData = new List<IncentiveData>();
                }
            }
            catch (Exception)
            {
                incentivesData = null;
            }

            return incentivesData;
        }

        private int GetTotalIncentivesCount(DateTime startDate, DateTime endDate)
        {
            int? count;

            int callId = 0;

            try
            {
                count = this._businessObjects.CashIncentives.GetCashbackIncentivesCountByDateRange(this._businessObjects.Tenant.TenantId, SearchableObjectTypes.Commerce, startDate, endDate);
            }
            catch (Exception e)
            {
                count = -1;

                string errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(this._businessObjects.Tenant.TenantId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, "", 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return count ?? -1;
        }


        /// <summary>
        /// Retrieve all the cash incentives from a given tenant
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="userId"></param>
        /// <param name="tenantId"></param>
        /// <param name="branchId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("gets")]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Gets(Guid employeeId, string userId, Guid tenantId, Guid branchId, DateTime startDate, DateTime endDate, int pageSize, int pageNumber)
        {
            IActionResult result = new BadRequestResult();
            IncentiveDataSet incentives = new IncentiveDataSet
            {
                StartDate = startDate,
                EndDate = endDate,
                TenantId = tenantId,
                BranchId = branchId,
                Incentives = new List<IncentiveData>(),
                Count = 0
            };

            string errorMsg;
            int callId = 1;
            string parameters = "EmployeeId: " + employeeId + " - StartDate: " + startDate + " - EndDate: " + endDate + " - PageSize: " + pageSize + " - PageNumber: " + pageNumber;
            try
            {
                if (_businessObjects == null)
                {
                    Initialize(tenantId, userId);
                }

                if (this._businessObjects.Tenant.TenantId != Guid.Empty)
                {

                    Task<List<IncentiveData>> getDealsDataTask = new Task<List<IncentiveData>>(() => this.GetIncentivesData(pageSize, pageNumber, startDate, endDate));
                    getDealsDataTask.Start();

                    Task<int> getDealsCountTask = new Task<int>(() => this.GetTotalIncentivesCount(startDate, endDate));
                    getDealsCountTask.Start();

                    incentives.Incentives = await getDealsDataTask;
                    incentives.Count = incentives.Incentives?.Count ?? 0;

                    incentives.TotalRecords = await getDealsCountTask;

                    result = Ok(incentives);

                }


            }
            catch (Exception e)
            {
                errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(employeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return result;
        }


        [Route("post")]
        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] NewIncentive model)
        {
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;
            string dataErrors = "";

            Initialize(model.TenantId, model.UserId);
            IActionResult result;

            if (!ModelState.IsValid && model.TenantId != Guid.Empty && !string.IsNullOrWhiteSpace(model.UserId))
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);

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
                    Employee currenEmployee = null;

                    if (model.EmployeeId != Guid.Empty)
                        currenEmployee = this._businessObjects.Employees.Get(model.EmployeeId, false);


                    if (model.EmployeeId == Guid.Empty || (currenEmployee != null && currenEmployee.TenantId == model.TenantId))
                    {
                        bool valid = true;

                        Decimal.TryParse(model.UnitValue, out decimal unitValue);

                        decimal? previousUnitValue;

                        if (!string.IsNullOrWhiteSpace(model.PreviousUnitValue))
                        {

                            Decimal.TryParse(model.PreviousUnitValue, out decimal previousUnitValueAux);
                            previousUnitValue = previousUnitValueAux;
                        }
                        else
                            previousUnitValue = -1;

                        Decimal.TryParse(model.MinPurchasedAmount, out decimal minPurchasedAmount);
                        Decimal.TryParse(model.MinPurchasedTotalAmount, out decimal minPurchasedTotalAmount);
                        Decimal.TryParse(model.PurchasedAmountBlock, out decimal purchasedAmountBlock);

                        double relevanceRate;

                        if (!string.IsNullOrWhiteSpace(model.RelevanceRate))
                        {
                            Double.TryParse(model.RelevanceRate, out relevanceRate);

                        }
                        else
                            relevanceRate = -1;


                        if (!(model.DealType >= DealTypes.InStore && model.DealType <= DealTypes.Phone))
                        {
                            valid = false;
                            dataErrors += "-Tipo de incentivo debe ser seleccionado\n";
                        }

                        if (!(model.ApplyType >= CashIncentiveApplyTypes.WalletIncrease && model.ApplyType <= CashIncentiveApplyTypes.DirectDiscount))
                        {
                            valid = false;
                            dataErrors += "- La forma de aplicación debe ser seleccionado\n";
                        }

                        if (!(model.BenefitAmountType >= CashIncentiveBenefitAmountTypes.ByTotalAmount && model.BenefitAmountType <= CashIncentiveBenefitAmountTypes.ByAmountBlock))
                        {
                            valid = false;
                            dataErrors += "- La forma de calculo debe ser seleccionado\n";
                        }

                        if (!(model.Type >= CashbackTypes.Percentage && model.Type <= CashbackTypes.Points))
                        {
                            valid = false;
                            dataErrors += "- El tipo de incentivo debe ser seleccionado\n";
                        }

                        if (model.MaxCombinedIncentives < 0)
                        {
                            valid = false;
                            dataErrors += "- Máximo de incentivos acumulables debe ser mayor o igual a 0\n";
                        }

                        if (model.MinMembershipLevel < MembershipLevels.Bronze || model.MinMembershipLevel > MembershipLevels.Diamond)
                        {
                            valid = false;
                            dataErrors += "- El mínimo nivel de lealtad debe ser seleccionado\n";
                        }

                        if (minPurchasedAmount < 0)
                        {
                            valid = false;
                            dataErrors += "- El monto mínimo de compra debe ser mayor que 0\n";
                        }

                        if (purchasedAmountBlock < 0)
                        {
                            valid = false;
                            dataErrors += "- El bloque de compra debe ser mayor que 0\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.MainHint) || model.MainHint.Length < mainHintMinLength || model.MainHint.Length > mainHintMaxLength)
                        {
                            valid = false;
                            dataErrors += "La frase principal debe tener de " + mainHintMinLength + " a " + mainHintMaxLength + " caracteres\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.ComplementaryHint) || model.ComplementaryHint.Length < complementaryHintMinLength || model.ComplementaryHint.Length > complementaryHintMaxLength)
                        {
                            valid = false;
                            dataErrors += "-La frase secundaria debe tener de " + complementaryHintMinLength + " a " + complementaryHintMaxLength + " caracteres\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < nameMinLength || model.Name.Length > nameMaxLength)
                        {
                            valid = false;
                            dataErrors += "-El nombre debe tener de " + nameMinLength + " a " + nameMaxLength + " caracteres\n";
                        }

                        if (model.Keywords?.Length > keywordsMaxLength)
                        {
                            valid = false;
                            dataErrors += "-El total de palabras clave excede la cantidad permitida\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length < descriptionMinLength || model.Description.Length > descriptionMaxLength)
                        {
                            valid = false;
                            dataErrors += "-La descripción debe tener de " + descriptionMinLength + " a " + descriptionMaxLength + " caracteres\n";
                        }

                        if (model.AvailableQuantity < infiteAvailableQuantity || model.AvailableQuantity == 0 || model.AvailableQuantity < availableQuantityMinValue)
                        {
                            valid = false;
                            dataErrors += "-La cantidad disponibles debe ser mayor que " + availableQuantityMinValue + " \n";
                        }

                        if (unitValue <= 0)
                        {
                            valid = false;
                            dataErrors += "-El monto del beneficio debe ser mayor o igual que 0\n";
                        }

                        if (previousUnitValue != null && previousUnitValue > -1 && unitValue >= previousUnitValue)
                        {
                            valid = false;
                            dataErrors += "-El beneficio anterior debe ser menor que el monto del beneficio\n";
                        }

                        if (model.ValidMonthDays?.Count == 0)
                        {
                            valid = false;
                            dataErrors += "-Las fechas de validez deben ser indicadas\n";
                        }

                        if (model.ValidWeekDays?.Count == 0)
                        {
                            valid = false;
                            dataErrors += "-Los días de la semana válidos deben ser indicados\n";
                        }

                        if (model.ValidHours?.Count == 0)
                        {
                            valid = false;
                            dataErrors += "-Las horas de validez deben ser indicadas\n";
                        }

                        if (model.MaxUsagePerUser < -1)
                        {
                            valid = false;
                            dataErrors += "-El máximo de usos por usuario debe ser indicado\n";
                        }

                        if (model.MinPurchaseCountToUse < -1)
                        {
                            valid = false;
                            dataErrors += "-El mínimo de compras realizadas debe ser indicado\n";
                        }

                        if (minPurchasedTotalAmount < 0)
                        {
                            valid = false;
                            dataErrors += "-El mínimo total de pagos debe ser indicado\n";
                        }

                        if (relevanceRate < 0)
                        {
                            valid = false;
                            dataErrors += "-El nivel de relevancia debe ser indicado\n";
                        }

                        if (model.ReleaseDate >= model.ExpirationDate)
                        {
                            valid = false;
                            dataErrors += "-El periodo de validez es incorrecto, la fecha de lanzamiento debe ser menor que la fecha de expiración\n";
                        }

                        if (!valid)
                        {

                            result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        ErrorCode = Values.StatusCodes.BadRequest,
                                        ShowErrorToUser = false,
                                        InnerError = "Invalid Payload",
                                        PublicError = dataErrors
                                    });
                        }
                        else
                        {
                            //Need to convert dates to UTC
                            model.ReleaseDate = LocalToUtc(model.ReleaseDate, this._businessObjects.Tenant.UtcTimeDiff);
                            model.ExpirationDate = LocalToUtc(model.ExpirationDate, this._businessObjects.Tenant.UtcTimeDiff);


                            string validMonthDays = "";

                            foreach(string item in model.ValidMonthDays)
                            {
                                validMonthDays = item + "*";
                            }

                            string validWeekDays = "";

                            foreach (string item in model.ValidWeekDays)
                            {
                                validWeekDays = item + "*";
                            }

                            string validHours = "";

                            foreach (string item in model.ValidHours)
                            {
                                validHours = item + "*";
                            }

                            previousUnitValue = previousUnitValue <= 0 ? null : previousUnitValue;

                            //Retrieve tenant to get the rules and conditions
                            TenantInfo tenantInfo = this._businessObjects.Commerces.Get(model.TenantId, CommerceKeys.TenantKey);

                            if (tenantInfo != null)
                            {

                                string claimInstructions = model.DealType switch
                                {
                                    DealTypes.InStore => tenantInfo.InStoreDealClaimInstructions,
                                    DealTypes.Online => tenantInfo.OnlineDealClaimInstructions,
                                    DealTypes.Phone => tenantInfo.PhoneDealClaimInstructions,
                                    _ => "--"
                                };


                                CashIncentive newIncentive = this._businessObjects.CashIncentives.Post(model.Type, DisplayTypes.BroadcastingAndListings, model.ApplyType, model.BenefitAmountType, model.DealType, model.MaxCombinedIncentives, unitValue, previousUnitValue ?? -1, model.MinMembershipLevel,
                                    minPurchasedAmount, purchasedAmountBlock, -1, model.AvailableQuantity, model.Name, model.MainHint, model.ComplementaryHint, model.Description, model.Keywords, model.IsSponsored, validWeekDays, validMonthDays, validHours, model.MaxUsagePerUser, null, model.MinPurchaseCountToUse,
                                    minPurchasedTotalAmount, GeoSegmentationTypes.Country, tenantInfo.IncentiveRules, tenantInfo.IncentiveConditions, relevanceRate, model.ReleaseDate, model.ExpirationDate);



                                if (newIncentive != null)
                                {
                                    //Needs to add it to Algolia 1st
                                    if (newIncentive.DisplayType < DisplayTypes.BroadcastingOnly)//If the offer will be publicly accessible
                                    {

                                        ImageHandler imgHandler = new ImageHandler();

                                        string indexName;

                                        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                                        {
                                            indexName = SearchIndexNames.DevAppend + SearchIndexNames.CashbackIncentives;

                                        }
                                        else
                                        {
                                            indexName = SearchIndexNames.ProdAppend + SearchIndexNames.CashbackIncentives;
                                        }

                                        SearchObjectHandler.SetParams(SearchIndexNames.AppName, indexName);
                                        
                                        string applyTypeName = newIncentive.ApplyType switch
                                        {
                                            CashIncentiveApplyTypes.WalletIncrease => "Cashback, Devolucion, Retorno",
                                            CashIncentiveApplyTypes.DirectDiscount => "Descuento, Discount",
                                            _ => "",
                                        };

                                        bool success = await SearchObjectHandler.AddCashIncentiveSearchableObjectAsync(newIncentive.Id, newIncentive.TenantId, tenantInfo.CountryId, newIncentive.Keywords, imgHandler.GetImgUrl((Guid)tenantInfo.Logo, ImageStorages.Cloudinary, ImageRequesters.App).ImgUrl, newIncentive.IsSponsored, newIncentive.IsActive, newIncentive.DealType, newIncentive.RelevanceRate,
                                            0, newIncentive.ReleaseDate, newIncentive.ExpirationDate, SearchableObjectTypes.CashbackIncentive, newIncentive.ApplyType, applyTypeName, newIncentive.MainHint + " " + newIncentive.ComplementaryHint, newIncentive.Name, (double)newIncentive.UnitValue, (double)newIncentive.PreviousUnitValue);
                                    }

                                    result = Ok(this.GetIncentiveContent(newIncentive));
                                }
                                else
                                {
                                    result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        ErrorCode = Values.StatusCodes.BadRequest,
                                        ShowErrorToUser = true,
                                        InnerError = "Error at creation procceess",
                                        PublicError = "No hemos podido crear este incentivo, por favor revisa el formulario y trata de nuevo"
                                    });
                                }
                            }
                            else
                            {
                                result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        ErrorCode = Values.StatusCodes.BadRequest,
                                        ShowErrorToUser = true,
                                        InnerError = "Wrong commerce",
                                        PublicError = "No hemos podido crear este incentivo, por favor revisa el formulario y trata de nuevo"
                                    });
                            }
                        }
                    }
                    else
                    {
                        result = new ConflictObjectResult(
                                       new ErrorResponse
                                       {
                                           ErrorCode = Values.StatusCodes.BadRequest,
                                           ShowErrorToUser = true,
                                           InnerError = "Invalid employee",
                                           PublicError = "No hemos podido crear este incentivo, por favor revisa el formulario y trata de nuevo"
                                       });

                    }

                }
                catch (Exception e)
                {
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, e.InnerException != null ? e.InnerException.Message : e.Message);
                }
            }

            return result;
        }


        [Route("put")]
        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync([FromBody] UpdateIncentive model)
        {
            int callId = 3;
            string parameters = model.ToString();
            string errorMsg;
            string dataErrors = "";

            IActionResult result;

            Initialize(model.TenantId, model.UserId);

            if (!ModelState.IsValid)
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
                    Employee currenEmployee = null;

                    if (model.EmployeeId != Guid.Empty)
                        currenEmployee = this._businessObjects.Employees.Get(model.EmployeeId, false);


                    if (model.EmployeeId == Guid.Empty || (currenEmployee != null && currenEmployee.TenantId == model.TenantId))
                    {
                        bool valid = true;

                        Decimal.TryParse(model.UnitValue, out decimal unitValue);

                        decimal? previousUnitValue;

                        if (!string.IsNullOrWhiteSpace(model.PreviousUnitValue))
                        {

                            Decimal.TryParse(model.PreviousUnitValue, out decimal previousUnitValueAux);
                            previousUnitValue = previousUnitValueAux;
                        }
                        else
                            previousUnitValue = -1;

                        Decimal.TryParse(model.MinPurchasedAmount, out decimal minPurchasedAmount);
                        Decimal.TryParse(model.MinPurchasedTotalAmount, out decimal minPurchasedTotalAmount);
                        Decimal.TryParse(model.PurchasedAmountBlock, out decimal purchasedAmountBlock);

                        double relevanceRate;

                        if (!string.IsNullOrWhiteSpace(model.RelevanceRate))
                        {
                            Double.TryParse(model.RelevanceRate, out relevanceRate);

                        }
                        else
                            relevanceRate = -1;


                        if (!(model.DealType >= DealTypes.InStore && model.DealType <= DealTypes.Phone))
                        {
                            valid = false;
                            dataErrors += "-Tipo de incentivo debe ser seleccionado\n";
                        }

                        if (!(model.ApplyType >= CashIncentiveApplyTypes.WalletIncrease && model.ApplyType <= CashIncentiveApplyTypes.DirectDiscount))
                        {
                            valid = false;
                            dataErrors += "- La forma de aplicación debe ser seleccionado\n";
                        }

                        if (!(model.BenefitAmountType >= CashIncentiveBenefitAmountTypes.ByTotalAmount && model.BenefitAmountType <= CashIncentiveBenefitAmountTypes.ByAmountBlock))
                        {
                            valid = false;
                            dataErrors += "- La forma de calculo debe ser seleccionado\n";
                        }

                        if (!(model.Type >= CashbackTypes.Percentage && model.Type <= CashbackTypes.Points))
                        {
                            valid = false;
                            dataErrors += "- El tipo de incentivo debe ser seleccionado\n";
                        }

                        if (model.MaxCombinedIncentives < 0)
                        {
                            valid = false;
                            dataErrors += "- Máximo de incentivos acumulables debe ser mayor o igual a 0\n";
                        }

                        if (model.MinMembershipLevel < MembershipLevels.Bronze || model.MinMembershipLevel > MembershipLevels.Diamond)
                        {
                            valid = false;
                            dataErrors += "- El mínimo nivel de lealtad debe ser seleccionado\n";
                        }

                        if (minPurchasedAmount < 0)
                        {
                            valid = false;
                            dataErrors += "- El monto mínimo de compra debe ser mayor que 0\n";
                        }

                        if (purchasedAmountBlock < 0)
                        {
                            valid = false;
                            dataErrors += "- El bloque de compra debe ser mayor que 0\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.MainHint) || model.MainHint.Length < mainHintMinLength || model.MainHint.Length > mainHintMaxLength)
                        {
                            valid = false;
                            dataErrors += "La frase principal debe tener de " + mainHintMinLength + " a " + mainHintMaxLength + " caracteres\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.ComplementaryHint) || model.ComplementaryHint.Length < complementaryHintMinLength || model.ComplementaryHint.Length > complementaryHintMaxLength)
                        {
                            valid = false;
                            dataErrors += "-La frase secundaria debe tener de " + complementaryHintMinLength + " a " + complementaryHintMaxLength + " caracteres\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < nameMinLength || model.Name.Length > nameMaxLength)
                        {
                            valid = false;
                            dataErrors += "-El nombre debe tener de " + nameMinLength + " a " + nameMaxLength + " caracteres\n";
                        }

                        if (model.Keywords?.Length > keywordsMaxLength)
                        {
                            valid = false;
                            dataErrors += "-El total de palabras clave excede la cantidad permitida\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length < descriptionMinLength || model.Description.Length > descriptionMaxLength)
                        {
                            valid = false;
                            dataErrors += "-La descripción debe tener de " + descriptionMinLength + " a " + descriptionMaxLength + " caracteres\n";
                        }

                        if (model.AvailableQuantity < infiteAvailableQuantity || model.AvailableQuantity == 0 || model.AvailableQuantity < availableQuantityMinValue)
                        {
                            valid = false;
                            dataErrors += "-La cantidad disponibles debe ser mayor que " + availableQuantityMinValue + " \n";
                        }

                        if (unitValue <= 0)
                        {
                            valid = false;
                            dataErrors += "-El monto del beneficio debe ser mayor o igual que 0\n";
                        }

                        if (previousUnitValue != null && previousUnitValue > -1 && unitValue >= previousUnitValue)
                        {
                            valid = false;
                            dataErrors += "-El beneficio anterior debe ser menor que el monto del beneficio\n";
                        }

                        if (model.ValidMonthDays?.Count == 0)
                        {
                            valid = false;
                            dataErrors += "-Las fechas de validez deben ser indicadas\n";
                        }

                        if (model.ValidWeekDays?.Count == 0)
                        {
                            valid = false;
                            dataErrors += "-Los días de la semana válidos deben ser indicados\n";
                        }

                        if (model.ValidHours?.Count == 0)
                        {
                            valid = false;
                            dataErrors += "-Las horas de validez deben ser indicadas\n";
                        }

                        if (model.MaxUsagePerUser < -1)
                        {
                            valid = false;
                            dataErrors += "-El máximo de usos por usuario debe ser indicado\n";
                        }

                        if (model.MinPurchaseCountToUse < -1)
                        {
                            valid = false;
                            dataErrors += "-El mínimo de compras realizadas debe ser indicado\n";
                        }

                        if (minPurchasedTotalAmount < 0)
                        {
                            valid = false;
                            dataErrors += "-El mínimo total de pagos debe ser indicado\n";
                        }

                        if (relevanceRate < 0)
                        {
                            valid = false;
                            dataErrors += "-El nivel de relevancia debe ser indicado\n";
                        }

                        if (model.ReleaseDate >= model.ExpirationDate)
                        {
                            valid = false;
                            dataErrors += "-El periodo de validez es incorrecto, la fecha de lanzamiento debe ser menor que la fecha de expiración\n";
                        }

                        if (!valid)
                        {

                            result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        ErrorCode = Values.StatusCodes.BadRequest,
                                        ShowErrorToUser = false,
                                        InnerError = "Invalid Payload",
                                        PublicError = dataErrors
                                    });
                        }
                        else
                        {
                            //Need to convert dates to UTC
                            model.ReleaseDate = LocalToUtc(model.ReleaseDate, this._businessObjects.Tenant.UtcTimeDiff);
                            model.ExpirationDate = LocalToUtc(model.ExpirationDate, this._businessObjects.Tenant.UtcTimeDiff);


                            string validMonthDays = "";

                            foreach (string item in model.ValidMonthDays)
                            {
                                validMonthDays = item + "*";
                            }

                            string validWeekDays = "";

                            foreach (string item in model.ValidWeekDays)
                            {
                                validWeekDays = item + "*";
                            }

                            string validHours = "";

                            foreach (string item in model.ValidHours)
                            {
                                validHours = item + "*";
                            }

                            previousUnitValue = previousUnitValue <= 0 ? null : previousUnitValue;

                            //Retrieve tenant to get the rules and conditions
                            TenantInfo tenantInfo = this._businessObjects.Commerces.Get(model.TenantId, CommerceKeys.TenantKey);

                            if (tenantInfo != null)
                            {
                                CashIncentive incentive = this._businessObjects.CashIncentives.Get(model.Id, false);

                                if (incentive != null)
                                {

                                    CashIncentive updatedCashIncentive = this._businessObjects.CashIncentives.Put(model.Id, model.Type, DisplayTypes.BroadcastingAndListings, model.ApplyType, model.BenefitAmountType, model.DealType, model.MaxCombinedIncentives,
                                        unitValue, previousUnitValue ?? -1, model.MinMembershipLevel, minPurchasedAmount, purchasedAmountBlock, -1, model.AvailableQuantity, model.Name, model.MainHint, model.ComplementaryHint, model.Description, model.Keywords, model.IsSponsored,
                                        validWeekDays, validMonthDays, validHours, model.MaxUsagePerUser, null, model.MinPurchaseCountToUse, minPurchasedTotalAmount, GeoSegmentationTypes.Country, tenantInfo.IncentiveRules, tenantInfo.IncentiveConditions,
                                        relevanceRate, model.ReleaseDate, model.ExpirationDate);



                                    if (updatedCashIncentive != null)
                                    {

                                        //Needs to update it to Algolia
                                        if (updatedCashIncentive.DisplayType < DisplayTypes.BroadcastingOnly)//If the offer will be publicly accessible
                                        {

                                            string indexName;

                                            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                                            {
                                                indexName = SearchIndexNames.DevAppend + SearchIndexNames.CashbackIncentives;

                                            }
                                            else
                                            {
                                                indexName = SearchIndexNames.ProdAppend + SearchIndexNames.CashbackIncentives;
                                            }

                                            SearchObjectHandler.SetParams(SearchIndexNames.AppName, indexName);

                                            string applyTypeName = updatedCashIncentive.ApplyType switch
                                            {
                                                CashIncentiveApplyTypes.WalletIncrease => "Cashback, Devolucion, Retorno",
                                                CashIncentiveApplyTypes.DirectDiscount => "Descuento, Discount",
                                                _ => "",
                                            };

                                            bool success = await SearchObjectHandler.UpdateCashIncentiveSearchableObjectAsync(updatedCashIncentive.Id, updatedCashIncentive.Keywords, updatedCashIncentive.IsSponsored, updatedCashIncentive.IsActive, updatedCashIncentive.DealType, updatedCashIncentive.RelevanceRate, updatedCashIncentive.ReleaseDate, updatedCashIncentive.ExpirationDate,
                                                updatedCashIncentive.ApplyType, applyTypeName, updatedCashIncentive.MainHint + " " + updatedCashIncentive.ComplementaryHint, updatedCashIncentive.Name, (double)updatedCashIncentive.UnitValue, (double)updatedCashIncentive.PreviousUnitValue);
                                        }

                                        result = Ok(this.GetIncentiveContent(updatedCashIncentive));
                                    }
                                    else
                                    {
                                        result = new BadRequestObjectResult(
                                        new ErrorResponse
                                        {
                                            ErrorCode = Values.StatusCodes.BadRequest,
                                            ShowErrorToUser = true,
                                            InnerError = "Error at creation procceess",
                                            PublicError = "No hemos podido editar este incentivo, por favor revisa el formulario y trata de nuevo"
                                        });
                                    }
                                }
                                else
                                {
                                    result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        ErrorCode = Values.StatusCodes.BadRequest,
                                        ShowErrorToUser = true,
                                        InnerError = "Error at update procceess",
                                        PublicError = "No hemos podido actualizar este incentivo, por favor revisa el formulario y trata de nuevo"
                                    });
                                }

                            }
                            else
                            {
                                result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        ErrorCode = Values.StatusCodes.BadRequest,
                                        ShowErrorToUser = true,
                                        InnerError = "Error at update procceess",
                                        PublicError = "No hemos podido actualizar este incentivo, por favor revisa el formulario y trata de nuevo"
                                    });
                            }
                        }
                    }
                    else
                    {
                        result = new ConflictObjectResult(
                                       new ErrorResponse
                                       {
                                           ErrorCode = Values.StatusCodes.BadRequest,
                                           ShowErrorToUser = true,
                                           InnerError = "Invalid employee",
                                           PublicError = "No hemos podido crear este incentivo, por favor revisa el formulario y trata de nuevo"
                                       });
                    }


                }
                catch (Exception e)
                {
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, e.InnerException != null ? e.InnerException.Message : e.Message);
                }
            }

            return result;
        }


        [Route("putState")]
        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutActiveStateAsync([FromBody] IncentiveModifierById model)
        {
            int callId = 4;
            string parameters = model.ToString();
            string errorMsg;

            IActionResult result;

            Initialize(model.TenantId, model.UserId);

            if (!ModelState.IsValid)
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

                    ObjectStateUpdate stateUpdate = this._businessObjects.CashIncentives.Put(model.Id, ChangeTypes.ActiveState);

                    if (stateUpdate != null)
                    {
                        CashIncentive updatedIncentive = this._businessObjects.CashIncentives.Get(model.Id, true);

                        //Needs to update it to Algolia
                        if (updatedIncentive.DisplayType < DisplayTypes.BroadcastingOnly)//If the offer will be publicly accessible
                        {

                            string indexName;

                            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                            {
                                indexName = SearchIndexNames.DevAppend + SearchIndexNames.CashbackIncentives;

                            }
                            else
                            {
                                indexName = SearchIndexNames.ProdAppend + SearchIndexNames.CashbackIncentives;
                            }

                            SearchObjectHandler.SetParams(SearchIndexNames.AppName, indexName);

                            bool success = await SearchObjectHandler.UpdateSearchableObjectActiveStateAsync(updatedIncentive.Id, updatedIncentive.IsActive);
                        }

                        SuccessResponse response = new SuccessResponse
                        {
                            StatusCode = Values.StatusCodes.Ok,
                            ShowMsgToUser = true,
                        };

                        if (stateUpdate.NewState)
                        {
                            response.MessageToDisplay = "El incentivo ha sido activada éxitosamente";
                        }
                        else
                        {
                            response.MessageToDisplay = "El incentivo ha sido desactivada éxitosamente";
                        }

                        result = Ok(this.GetIncentiveContent(updatedIncentive));
                    }
                    else
                    {
                        result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    }
                }
                catch (Exception e)
                {
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, e.InnerException != null ? e.InnerException.Message : e.Message);
                }

            }

            return result;
        }

        [Route("putDates")]
        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDatesAsync([FromBody] IncentiveDatesModifier model)
        {
            int callId = 5;
            string parameters = model.ToString();
            string errorMsg;

            IActionResult result;

            Initialize(model.TenantId, model.UserId);

            if (!ModelState.IsValid)
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
                    bool valid = true;
                    string dataErrors = "";

                    if (model.ReleaseDate >= model.ExpirationDate)
                    {
                        valid = false;
                        dataErrors += "-El periodo de validez es incorrecto, la fecha de lanzamiento debe ser menor que la fecha de expiración\n";
                    }

                    if (valid)
                    {
                        //Need to convert dates to UTC
                        model.ReleaseDate = LocalToUtc(model.ReleaseDate, this._businessObjects.Tenant.UtcTimeDiff);
                        model.ExpirationDate = LocalToUtc(model.ExpirationDate, this._businessObjects.Tenant.UtcTimeDiff);

                        bool datesUpdated = this._businessObjects.CashIncentives.Put(model.Id, model.ReleaseDate, model.ExpirationDate);

                        if (datesUpdated)
                        {
                            CashIncentive updatedIncentive = this._businessObjects.CashIncentives.Get(model.Id, true);

                            //Needs to update it to Algolia
                            if (updatedIncentive.DisplayType < DisplayTypes.BroadcastingOnly)//If the offer will be publicly accessible
                            {

                                string indexName;

                                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                                {
                                    indexName = SearchIndexNames.DevAppend + SearchIndexNames.CashbackIncentives;

                                }
                                else
                                {
                                    indexName = SearchIndexNames.ProdAppend + SearchIndexNames.CashbackIncentives;
                                }

                                SearchObjectHandler.SetParams(SearchIndexNames.AppName, indexName);

                                bool success = await SearchObjectHandler.UpdateSearchableObjectDateRangeAsync(updatedIncentive.Id, updatedIncentive.ReleaseDate, updatedIncentive.ExpirationDate);
                            }

                            SuccessResponse response = new SuccessResponse
                            {
                                StatusCode = Values.StatusCodes.Ok,
                                ShowMsgToUser = true,
                            };

                            if (datesUpdated)
                            {
                                response.MessageToDisplay = "Las fechas de vigencia se actualizaron éxitosamente";
                            }

                            result = Ok(this.GetIncentiveContent(updatedIncentive));
                        }
                        else
                        {
                            result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                        }
                    }
                    else
                    {
                        result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        ErrorCode = Values.StatusCodes.BadRequest,
                                        ShowErrorToUser = false,
                                        InnerError = "Invalid Payload",
                                        PublicError = dataErrors
                                    });
                    }

                }
                catch (Exception e)
                {
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, e.InnerException != null ? e.InnerException.Message : e.Message);
                }

            }

            return result;
        }

        [Route("delete")]
        [HttpDelete]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromBody] IncentiveModifierById model)
        {
            int callId = 6;
            string parameters = model.ToString();
            string errorMsg;

            IActionResult result;

            Initialize(model.TenantId, model.UserId);

            if (!ModelState.IsValid)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Delete, errorMsg);

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
                    Guid? deletedId = this._businessObjects.CashIncentives.Delete(model.Id);

                    if (deletedId != null)
                    {

                        string indexName;

                        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                        {
                            indexName = SearchIndexNames.DevAppend + SearchIndexNames.CashbackIncentives;

                        }
                        else
                        {
                            indexName = SearchIndexNames.ProdAppend + SearchIndexNames.CashbackIncentives;
                        }

                        SearchObjectHandler.SetParams(SearchIndexNames.AppName, indexName);

                        bool success = await SearchObjectHandler.DeleteSearchableObjectAsync(((Guid)deletedId).ToString());

                        SuccessResponse response = new SuccessResponse
                        {
                            StatusCode = Values.StatusCodes.Ok,
                            ShowMsgToUser = true,
                            MessageToDisplay = "El incentivo se ha eliminado éxitosamente"
                        };

                        result = Ok(response);
                    }
                    else
                    {
                        result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    }
                }
                catch (Exception e)
                {
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Delete, e.InnerException != null ? e.InnerException.Message : e.Message);
                }

            }

            return result;
        }

        #endregion

    }
}
