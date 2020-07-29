using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YOY.BusinessAPI.Models.v1.Deal.POCO;
using YOY.BusinessAPI.Models.v1.Deal.SET;
using YOY.BusinessAPI.Models.v1.Misc.BasicResponse.POCO;
using YOY.DAO.Entities;
using YOY.DAO.Entities.Manager.Misc.Image;
using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;
using YOY.BusinessAPI.Handlers.Search;
using YOY.DTO.Entities.Misc.ObjectState.POCO;
using YOY.DTO.Entities.Misc.Category;

namespace YOY.BusinessAPI.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class DealController : ControllerBase
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
        private ImageHandler _imgHandler;

        private const int controllerVersion = 1;

        private const int mainHintMinLength = 3;
        private const int mainHintMaxLength = 10;
        private const int complementaryHintMinLength = 3;
        private const int complementaryHintMaxLength = 22;
        private const int nameMinLength = 10;
        private const int nameMaxLength = 64;
        private const int keywordsMaxLength = 1000;
        public const int codeMaxLenght = 15;
        private const int descriptionMinLength = 10;
        private const int descriptionMaxLength = 360;
        private const int availableQuantityMinValue = 30;
        public const int infiteAvailableQuantity = -1;

        public const int minEnabledAgeParam = 10;
        public const int maxEnabledAgeParam = 99;

        #endregion

        #region METHODS

        private void Initialize(Guid commerceId, string userId)
        {
            //1st initialize in order to get tenant data
            _tenant = Tenant.GetInstance(Guid.Empty);
            _businessObjects = BusinessObjects.GetInstance(_tenant);

            if (_imgHandler == null)
                _imgHandler = new ImageHandler();

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

        private DealData GetDealContent(Offer data, int type)
        {
            DealData offer = null;

            switch (type)
            {
                case OfferTypes.Reward:

                    //NO REWARDS CREATED FROM HERE

                    break;
                case OfferTypes.Offer:

                    offer = new DealData
                    {
                        Id = data.Id,
                        TenantId = data.TenantId,
                        MainCategoryId = data.MainCategoryId,
                        MainCategoryName = data.MainCategoryName,
                        OfferType = data.OfferType,
                        OfferTypeName = data.OfferTypeName,
                        DealType = data.DealType,
                        DealTypeName = data.DealTypeName,
                        Name = data.Name,
                        MainHint = data.MainHint,
                        ComplementaryHint = data.ComplementaryHint,
                        Keywords = data.Keywords,
                        Code = data.Code,
                        Description = data.Description,
                        IsActive = data.IsActive,
                        IsExclusive = data.IsExclusive,
                        IsSponsored = data.IsSponsored,
                        HasPreferences = data.HasPreferences,
                        AvailableQuantity = data.AvailableQuantity,
                        ClaimLocation = data.ClaimLocation,
                        Value = data.Value,
                        RegularValue = data.RegularValue ?? -1,
                        HasExtraBonus = data.ExtraBonus > 0,
                        ExtraBonus = data.ExtraBonus,
                        ExtraBonusType = data.ExtraBonusType,
                        ExtraBonusTypeName = data.ExtraBonusTypeName,
                        ReleaseFullDateTime = UtcToLocal((DateTime)data.ReleaseDate, this._businessObjects.Tenant.UtcTimeDiff),
                        ExpirationFullDateTime = UtcToLocal(data.ExpirationDate, this._businessObjects.Tenant.UtcTimeDiff),
                        DisplayImageUrl = data.DisplayImgId != null ? this._imgHandler.GetImgUrl((Guid)data.DisplayImgId, ImageStorages.Cloudinary, ImageRequesters.Website).ImgUrl : "",
                        PurchasedCount = data.ClaimCount,
                        CreatedDate = data.CreatedDate,
                        PublishedState = data.PublishState
                    };


                    break;
                case OfferTypes.Coupon:

                    offer = new DealData
                    {
                        Id = data.Id,
                        TenantId = data.TenantId,
                        MainCategoryId = data.MainCategoryId,
                        MainCategoryName = data.MainCategoryName,
                        OfferType = data.OfferType,
                        OfferTypeName = data.OfferTypeName,
                        DealType = data.DealType,
                        DealTypeName = data.DealTypeName,
                        Name = data.Name,
                        MainHint = data.MainHint,
                        ComplementaryHint = data.ComplementaryHint,
                        Keywords = data.Keywords,
                        Code = data.Code,
                        Description = data.Description,
                        IsActive = data.IsActive,
                        IsExclusive = data.IsExclusive,
                        IsSponsored = data.IsSponsored,
                        HasPreferences = data.HasPreferences,
                        AvailableQuantity = data.AvailableQuantity,
                        ClaimLocation = data.ClaimLocation,
                        Value = -1,
                        RegularValue = -1,
                        HasExtraBonus = data.ExtraBonus > 0,
                        ExtraBonus = data.ExtraBonus,
                        ExtraBonusType = data.ExtraBonusType,
                        ExtraBonusTypeName = data.ExtraBonusTypeName,
                        ReleaseFullDateTime = UtcToLocal((DateTime)data.ReleaseDate, this._businessObjects.Tenant.UtcTimeDiff),
                        ExpirationFullDateTime = UtcToLocal(data.ExpirationDate, this._businessObjects.Tenant.UtcTimeDiff),
                        DisplayImageUrl = data.DisplayImgId != null ? this._imgHandler.GetImgUrl((Guid)data.DisplayImgId, ImageStorages.Cloudinary, ImageRequesters.Website).ImgUrl : "",
                        PurchasedCount = data.ClaimCount,
                        CreatedDate = data.CreatedDate,
                        PublishedState = data.PublishState
                    };


                    break;
            }

            offer.ReleaseDateComponent = offer.ReleaseFullDateTime.ToString("yyyy-MM-dd");
            offer.ReleaseHourComponent = offer.ReleaseFullDateTime.ToString("hh:mm tt");

            offer.ExpirationDateComponent = offer.ExpirationFullDateTime.ToString("yyyy-MM-dd");
            offer.ExpirationHourComponent = offer.ExpirationFullDateTime.ToString("hh:mm tt");


            if (!string.IsNullOrWhiteSpace(data.TargettingParams))
            {
                char[] paramsSeparator = { TargettingParamMarks.ParamsSeparator[0] };
                //1st split all the params
                string[] paramsByType = data.TargettingParams.Split(paramsSeparator);

                for (int i = 0; i < paramsByType.Length; ++i)
                {
                    char[] paramValueSeparator = { TargettingParamMarks.TypeValueSeparator[0] };

                    string[] paramValues = paramsByType[i].Split(paramValueSeparator);

                    char[] valuesSeparator = { TargettingParamMarks.ValueSeparator[0] };

                    switch (paramValues[0])
                    {
                        case TargettingParamMarks.Gender:

                            offer.GenderParam = paramValues[1][0];

                            break;
                        case TargettingParamMarks.AgeInterval:

                            string[] values = paramValues[1].Split(valuesSeparator);

                            int startAge;
                            int.TryParse(values[0], out startAge);
                            offer.StartAgeParam = startAge;

                            int endAge;
                            int.TryParse(values[1], out endAge);
                            offer.EndAgeParam = endAge;

                            break;
                    }

                }
            }
            else
            {
                offer.GenderParam = GenderParams.Any;
                offer.StartAgeParam = 1;
                offer.EndAgeParam = 99;
            }

            return offer;
        }

        public DateTime LocalToUtc(DateTime date, string hourString, int utcTimeDiff)
        {
            DateTime finalDate;
            try
            {
                //-------------------------------------------------------------------------------------------------//
                //Logic to transform schedules to universal format
                //-------------------------------------------------------------------------------------------------//
                //START HOUR
                string[] timeComponents = hourString.Split(':');
                int hour = -1;
                int mins = -1;
                int.TryParse(timeComponents[0], out hour);
                string minutes = timeComponents[1].Substring(0, 2);
                int.TryParse(minutes, out mins);
                string meridian = timeComponents[1].Substring(2);

                switch (meridian)
                {
                    case "AM":
                        //Nothing
                        break;
                    case "PM":
                        hour += 12;
                        break;
                }

                finalDate = date.AddHours(hour);
                finalDate = finalDate.AddMinutes(mins);

                //ADDS THE INVERSE OF THE UTC TIME DIFF TO MEET UTC DATE
                finalDate = finalDate.AddHours(utcTimeDiff * -1);

                //----------------------------------------------------------------------------------------------------//

            }
            catch (Exception)
            {
                finalDate = DateTime.MinValue;
            }


            return finalDate;
        }

        public DateTime UtcToLocal(DateTime date, int utcTimeDiff)
        {
            //ADDS THE UTC TIME DIFF TO MEET LOCAL DATE
            DateTime finalDate = date.AddHours(utcTimeDiff);

            return finalDate;
        }

        public string ConvertMinutesToTimeLiteral(int totalMinutes)
        {
            string timeLiteral = "";

            int remainingMins;
            int weeks = 0;


            //Calculate the days
            remainingMins = totalMinutes % 1440;
            int days = totalMinutes / 1440;

            //If it's more then 1 week
            if (days > 7)
            {
                days %= 7;
                weeks = days / 7;
            }

            //Gets the hours and minutes
            int minutes = remainingMins % 60;
            int hours = remainingMins / 60;

            if (weeks > 0)
            {
                timeLiteral = weeks + "";

                if (weeks > 1)
                {
                    timeLiteral += " " + Resources.weeks;
                }
                else
                {
                    timeLiteral += " " + Resources.week;
                }
            }

            if (days > 0)
            {
                timeLiteral += " " + days;

                if (days > 1)
                {
                    timeLiteral += " " + Resources.days;
                }
                else
                {
                    timeLiteral += " " + Resources.day;
                }
            }

            if (hours > 0)
            {
                timeLiteral += " " + hours;

                if (days > 1)
                {
                    timeLiteral += " " + Resources.hours;
                }
                else
                {
                    timeLiteral += " " + Resources.hour;
                }
            }

            if (minutes > 0)
            {
                timeLiteral += " " + minutes;

                if (days > 1)
                {
                    timeLiteral += " " + Resources.minutes;
                }
                else
                {
                    timeLiteral += " " + Resources.minute;
                }
            }

            return timeLiteral;
        }


        private List<DealData> GetDealsData(int pageSize, int pageNumber)
        {
            List<DealData> dealsData;

            try
            {
                List<Offer> offers = this._businessObjects.Offers.Gets(ExpiredStates.All, ActiveStates.All, ReleaseStates.All, DateTime.UtcNow, pageSize, pageNumber);

                if (offers?.Count > 0)
                {
                    dealsData = new List<DealData>();

                    foreach (Offer item in offers)
                    {
                        if (item.OfferType == OfferTypes.Offer || item.OfferType == OfferTypes.Coupon)
                            dealsData.Add(GetDealContent(item, item.OfferType));
                    }
                }
                else
                {
                    dealsData = new List<DealData>();
                }
            }
            catch (Exception)
            {
                dealsData = null;
            }

            return dealsData;
        }

        private int GetTotalDealsCount(DateTime startDate, DateTime endDate)
        {
            int? count;

            int callId = 0;

            try
            {
                count = this._businessObjects.Offers.GetOffersCountByDateRange(this._businessObjects.Tenant.TenantId, SearchableObjectTypes.Commerce, startDate, endDate, OfferPurposeTypes.Deal);
            }
            catch(Exception e)
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
        /// Retrieve all the offer from a given tenant
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
            DealDataSet deals = new DealDataSet
            {
                StartDate = startDate,
                EndDate = endDate,
                TenantId = tenantId,
                BranchId = branchId,
                Deals = new List<DealData>(),
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

                    Task<List<DealData>> getDealsDataTask = new Task<List<DealData>>(() => this.GetDealsData(pageSize, pageNumber));
                    getDealsDataTask.Start();

                    Task<int> getDealsCountTask = new Task<int>(() => this.GetTotalDealsCount(startDate, endDate));
                    getDealsCountTask.Start();

                    deals.Deals = await getDealsDataTask;
                    deals.Count = deals.Deals?.Count ?? 0;

                    deals.TotalRecords = await getDealsCountTask;

                    result = Ok(deals);

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
        public async Task<IActionResult> PostAsync([FromBody] NewDeal model)
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

                    Employee currenEmployee = this._businessObjects.Employees.Get(model.EmployeeId, false);

                    if(currenEmployee != null && currenEmployee.TenantId == model.TenantId)
                    {
                        bool valid = true;

                        this.Initialize(currenEmployee.TenantId, currenEmployee.UserId);

                        if (model.MainCategoryId == null || model.MainCategoryId == Guid.Empty)
                        {
                            valid = false;
                            dataErrors = "-Categoría principal debe ser seleccionada\n";
                        }

                        if (!(model.DealType >= DealTypes.InStore && model.DealType <= DealTypes.Phone))
                        {
                            valid = false;
                            dataErrors = "-Tipo de incentivo debe ser seleccionado\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.MainHint) || model.MainHint.Length < mainHintMinLength || model.MainHint.Length > mainHintMaxLength)
                        {
                            valid = false;
                            dataErrors = "La frase principal debe tener de " + mainHintMinLength + " a " + mainHintMaxLength + " caracteres\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.ComplementaryHint) || model.ComplementaryHint.Length < complementaryHintMinLength || model.ComplementaryHint.Length > complementaryHintMaxLength)
                        {
                            valid = false;
                            dataErrors = "-La frase secundaria debe tener de " + complementaryHintMinLength + " a " + complementaryHintMaxLength + " caracteres\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < nameMinLength || model.Name.Length > nameMaxLength)
                        {
                            valid = false;
                            dataErrors = "-El nombre debe tener de " + nameMinLength + " a " + nameMaxLength + " caracteres\n";
                        }

                        if (model.Keywords?.Length > keywordsMaxLength)
                        {
                            valid = false;
                            dataErrors = "-El total de palabras clave excede la cantidad permitida\n";
                        }

                        if (model.Code?.Length > codeMaxLenght)
                        {
                            valid = false;
                            dataErrors = "-El código debe ser más corto\n";
                        }


                        if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length < descriptionMinLength || model.Description.Length > descriptionMaxLength)
                        {
                            valid = false;
                            dataErrors = "-La descripción debe tener de " + descriptionMinLength + " a " + descriptionMaxLength + " caracteres\n";
                        }

                        if (model.AvailableQuantity < infiteAvailableQuantity || model.AvailableQuantity == 0 || model.AvailableQuantity < availableQuantityMinValue)
                        {
                            valid = false;
                            dataErrors = "-La cantidad disponibles debe ser mayor que " + availableQuantityMinValue + " \n";
                        }

                        if (string.IsNullOrEmpty(model.ClaimLocation))
                        {
                            valid = false;
                            dataErrors = "-El lugar de retiro debe ser indicarse\n";
                        }

                        if (model.Value <= 0)
                        {
                            valid = false;
                            dataErrors = "-El precio debe ser mayor o igual que 0\n";
                        }

                        if (model.RegularValue >= -1 && model.Value >= model.RegularValue)
                        {
                            valid = false;
                            dataErrors = "-El precio regular debe ser menor que el precio promocional\n";
                        }

                        if (model.ExtraBonusType > 0 && model.ExtraBonus <= 0)
                        {
                            valid = false;
                            dataErrors = "-El tipo de incentivo extra debe ser mayor que 0\n";
                        }

                        if (model.StartAgeParam >= minEnabledAgeParam && model.EndAgeParam <= maxEnabledAgeParam && model.StartAgeParam <= model.EndAgeParam)
                        {
                            valid = false;
                            dataErrors = "-El rango de edad es incorrecto, la edad incial debe ser menor que la edad final\n";
                        }

                        if (model.ReleaseDate <= model.ExpirationDate)
                        {
                            valid = false;
                            dataErrors = "-El periodo de validez es incorrecto, la fecha de lanzamiento debe ser menor que la fecha de expiración\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.ReleaseHour) || string.IsNullOrWhiteSpace(model.ExpirationHour))
                        {
                            valid = false;
                            dataErrors = "-Las horas de lanzamiento y vencimiento deben ser especificadas";
                        }


                        if (model.DisplayImgData == null)
                        {
                            valid = false;

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
                            model.ReleaseDate = LocalToUtc(model.ReleaseDate, model.ReleaseHour, this._businessObjects.Tenant.UtcTimeDiff);
                            model.ExpirationDate = LocalToUtc(model.ExpirationDate, model.ExpirationHour, this._businessObjects.Tenant.UtcTimeDiff);

                            //Now needs to store the image in database
                            Guid? imgId = null;

                            string targettingParams = "";

                            if (!char.IsWhiteSpace(model.GenderParam))
                            {
                                targettingParams += TargettingParamMarks.Gender + TargettingParamMarks.TypeValueSeparator + model.GenderParam;
                            }
                            else
                            {
                                targettingParams += TargettingParamMarks.Gender + TargettingParamMarks.TypeValueSeparator + GenderParams.Any;
                            }

                            if (model.StartAgeParam < model.EndAgeParam)
                            {
                                targettingParams += TargettingParamMarks.ParamsSeparator + TargettingParamMarks.AgeInterval + TargettingParamMarks.TypeValueSeparator + model.StartAgeParam + "-" + model.EndAgeParam;
                            }
                            else
                            {
                                targettingParams += TargettingParamMarks.ParamsSeparator + TargettingParamMarks.AgeInterval + TargettingParamMarks.TypeValueSeparator + TargettingParamMarks.AnyValue;
                            }

                            //When price defines, then it's an offer, otherwise is a coupon
                            int offerType = model.RegularValue > 0 ? OfferTypes.Offer : OfferTypes.Coupon;


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

                                double relevanceRate = model.RelevanceRate ?? -1;


                                Offer newOffer = this._businessObjects.Offers.Post(model.MainCategoryId, offerType, model.DealType, RewardTypes.Deal, OfferPurposeTypes.Deal, GeoSegmentationTypes.Country,
                                    DisplayTypes.BroadcastingAndListings, model.Name, model.MainHint, model.ComplementaryHint, model.Keywords, model.Code, null, model.Description, MinsToUnlockByObjectiveTypes.GenericPurpose,
                                    model.IsExclusive, model.IsSponsored, false, model.AvailableQuantity, false, -1, 0, null, model.ClaimLocation, model.Value, model.RegularValue, model.ExtraBonus, model.ExtraBonusType,
                                    model.Value, model.Value, 0, 0, 0, imgId, targettingParams, model.ReleaseDate, model.ExpirationDate, tenantInfo.DealRules, tenantInfo.DealConditions, claimInstructions, ScheduleTypes.Continously, TimerTypes.CountDown,
                                    BroadcastingTimerByDisplayTypes.BroadcastingAndListings, "", "", relevanceRate);



                                if (newOffer != null)
                                {
                                    //Creates the category relation
                                    this._businessObjects.Categories.Post(model.MainCategoryId, CategoryHerarchyLevels.ProductCategory, newOffer.Id, CategoryRelatiomReferenceTypes.Offer);

                                    //Needs to add it to Algolia 1st
                                    if (newOffer.DisplayType < DisplayTypes.BroadcastingOnly)//If the offer will be publicly accessible
                                    {

                                        ImageHandler imgHandler = new ImageHandler();

                                        string indexName;

                                        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                                        {
                                            indexName = SearchIndexNames.DevAppend+SearchIndexNames.GeneralContent;

                                        }
                                        else
                                        {
                                            indexName = SearchIndexNames.ProdAppend + SearchIndexNames.GeneralContent;
                                        }

                                        SearchObjectHandler.SetParams(SearchIndexNames.AppName, indexName);

                                        bool success = await SearchObjectHandler.AddGeneralSearchableObjectAsync(newOffer.Id, newOffer.TenantId, tenantInfo.CountryId, newOffer.Keywords, imgHandler.GetImgUrl((Guid)tenantInfo.Logo, ImageStorages.Cloudinary, ImageRequesters.App).ImgUrl, newOffer.IsSponsored, newOffer.IsActive, newOffer.RelevanceRate,
                                            0, newOffer.ReleaseDate, newOffer.ExpirationDate, SearchableObjectTypes.Deal, newOffer.MainHint + " " + newOffer.ComplementaryHint, newOffer.Name, newOffer.MainCategoryName, newOffer.MainCategoryName,
                                            this._businessObjects.Categories.GetParentCategory(newOffer.MainCategoryId, CategoryHerarchyLevels.ProductCategory), newOffer.Value, 1);
                                    }

                                    result = Ok(this.GetDealContent(newOffer, newOffer.OfferType));
                                }
                                else
                                {
                                    result = new BadRequestObjectResult(
                                    new ErrorResponse
                                    {
                                        ErrorCode = Values.StatusCodes.BadRequest,
                                        ShowErrorToUser = true,
                                        InnerError = "Error at creation procceess",
                                        PublicError = "No hemos podido crear esta promo, por favor revisa el formulario y trata de nuevo"
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
                                        PublicError = "No hemos podido crear esta promo, por favor revisa el formulario y trata de nuevo"
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
                                           PublicError = "No hemos podido crear esta promo, por favor revisa el formulario y trata de nuevo"
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
        public async Task<IActionResult> PutAsync([FromBody] UpdatedDeal model)
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
                    Employee currenEmployee = this._businessObjects.Employees.Get(model.EmployeeId, false);

                    if(currenEmployee != null && currenEmployee.TenantId == model.TenantId)
                    {

                        bool valid = true;


                        this.Initialize(currenEmployee.TenantId, currenEmployee.UserId);

                        if (model.MainCategoryId == Guid.Empty)
                        {
                            valid = false;
                            dataErrors = "-Categoría principal debe ser seleccionada\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.MainHint) || model.MainHint.Length < mainHintMinLength || model.MainHint.Length > mainHintMaxLength)
                        {
                            valid = false;
                            dataErrors = "La frase principal debe tener de " + mainHintMinLength + " a " + mainHintMaxLength + " caracteres\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.ComplementaryHint) || model.ComplementaryHint.Length < complementaryHintMinLength || model.ComplementaryHint.Length > complementaryHintMaxLength)
                        {
                            valid = false;
                            dataErrors = "-La frase secundaria debe tener de " + complementaryHintMinLength + " a " + complementaryHintMaxLength + " caracteres\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < nameMinLength || model.Name.Length > nameMaxLength)
                        {
                            valid = false;
                            dataErrors = "-El nombre debe tener de " + nameMinLength + " a " + nameMaxLength + " caracteres\n";
                        }

                        if (model.Keywords?.Length > keywordsMaxLength)
                        {
                            valid = false;
                            dataErrors = "-El total de palabras clave excede la cantidad permitida\n";
                        }

                        if (model.Code?.Length > codeMaxLenght)
                        {
                            valid = false;
                            dataErrors = "-El código debe ser más corto\n";
                        }


                        if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length < descriptionMinLength || model.Description.Length > descriptionMaxLength)
                        {
                            valid = false;
                            dataErrors = "-La descripción debe tener de " + descriptionMinLength + " a " + descriptionMaxLength + " caracteres\n";
                        }

                        if (model.AvailableQuantity < infiteAvailableQuantity || model.AvailableQuantity == 0 || model.AvailableQuantity < availableQuantityMinValue)
                        {
                            valid = false;
                            dataErrors = "-La cantidad disponibles debe ser mayor que " + availableQuantityMinValue + " \n";
                        }

                        if (string.IsNullOrEmpty(model.ClaimLocation))
                        {
                            valid = false;
                            dataErrors = "-El lugar de retiro debe ser indicarse\n";
                        }

                        if (model.Value <= 0)
                        {
                            valid = false;
                            dataErrors = "-El precio debe ser mayor o igual que 0\n";
                        }

                        if (model.RegularValue >= -1 && model.Value >= model.RegularValue)
                        {
                            valid = false;
                            dataErrors = "-El precio regular debe ser menor que el precio promocional\n";
                        }

                        if (model.ExtraBonusType > 0 && model.ExtraBonus <= 0)
                        {
                            valid = false;
                            dataErrors = "-El tipo de incentivo extra debe ser mayor que 0\n";
                        }

                        if (model.StartAgeParam <= model.EndAgeParam)
                        {
                            valid = false;
                            dataErrors = "-El rango de edad es incorrecto, la edad incial debe ser menor que la edad final\n";
                        }

                        if (model.ReleaseDate <= model.ExpirationDate)
                        {
                            valid = false;
                            dataErrors = "-El periodo de validez es incorrecto, la fecha de lanzamiento debe ser menor que la fecha de expiración\n";
                        }

                        if (string.IsNullOrWhiteSpace(model.ReleaseHour) || string.IsNullOrWhiteSpace(model.ExpirationHour))
                        {
                            valid = false;
                            dataErrors = "-Las horas de lanzamiento y vencimiento deben ser especificadas";
                        }

                        if (model.DisplayImgData == null || string.IsNullOrEmpty(model.DisplayImgData.ExternalId))
                        {
                            valid = false;

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
                            model.ReleaseDate = LocalToUtc(model.ReleaseDate, model.ReleaseHour, this._businessObjects.Tenant.UtcTimeDiff);
                            model.ExpirationDate = LocalToUtc(model.ExpirationDate, model.ExpirationHour, this._businessObjects.Tenant.UtcTimeDiff);

                            //Now needs to store the image in database
                            Guid? imgId = null;

                            string targettingParams = "";

                            if (!char.IsWhiteSpace(model.GenderParam))
                            {
                                targettingParams += TargettingParamMarks.Gender + TargettingParamMarks.TypeValueSeparator + model.GenderParam;
                            }
                            else
                            {
                                targettingParams += TargettingParamMarks.Gender + TargettingParamMarks.TypeValueSeparator + GenderParams.Any;
                            }

                            if (model.StartAgeParam < model.EndAgeParam)
                            {
                                targettingParams += TargettingParamMarks.ParamsSeparator + TargettingParamMarks.AgeInterval + TargettingParamMarks.TypeValueSeparator + model.StartAgeParam + "-" + model.EndAgeParam;
                            }
                            else
                            {
                                targettingParams += TargettingParamMarks.ParamsSeparator + TargettingParamMarks.AgeInterval + TargettingParamMarks.TypeValueSeparator + TargettingParamMarks.AnyValue;
                            }

                            //Retrieve tenant to get the rules and conditions
                            TenantInfo tenantInfo = this._businessObjects.Commerces.Get(model.TenantId, CommerceKeys.TenantKey);

                            if (tenantInfo != null)
                            {
                                Offer offer = this._businessObjects.Offers.Get(model.Id, false);
                                Guid currentMainCategoryId;

                                if(offer != null)
                                {
                                    currentMainCategoryId = offer.MainCategoryId;

                                    double relevanceRate = model.RelevanceRate ?? -1;

                                    Offer updatedOffer = this._businessObjects.Offers.Put(model.Id, model.OfferType, model.MainCategoryId, model.Name, model.MainHint, model.ComplementaryHint, model.Keywords, model.Code, null, model.Description, (bool)model.IsActive, model.IsExclusive,
                                        model.IsSponsored, false, model.AvailableQuantity, -1, 0, null, model.ClaimLocation, model.Value, model.RegularValue, model.ExtraBonus, model.ExtraBonusType, model.Value,
                                        model.Value, 0, 0, 0, imgId, targettingParams, model.ReleaseDate, model.ExpirationDate, relevanceRate);



                                    if (updatedOffer != null)
                                    {
                                        if(currentMainCategoryId != updatedOffer.MainCategoryId)
                                        {
                                            //Needs to update the category relation
                                            this._businessObjects.Categories.Delete(offer.MainCategoryId, model.Id, CategoryRelatiomReferenceTypes.Offer);

                                            //Creates the category relation
                                            this._businessObjects.Categories.Post(model.MainCategoryId, CategoryHerarchyLevels.ProductCategory, updatedOffer.Id, CategoryRelatiomReferenceTypes.Offer);
                                        }

                                        //Needs to update it to Algolia
                                        if (updatedOffer.DisplayType < DisplayTypes.BroadcastingOnly)//If the offer will be publicly accessible
                                        {

                                            //Need to retrieve all categories
                                            List<CategoryRelation> categoryRelations = this._businessObjects.Categories.Gets(updatedOffer.Id, CategoryRelationTypes.Offer);

                                            string categories = "";
                                            string classifications = "";

                                            if (categoryRelations?.Count > 0)
                                            {
                                                foreach (CategoryRelation item in categoryRelations)
                                                {
                                                    if (item.HerarchyLevel == CategoryHerarchyLevels.ProductCategory)
                                                    {
                                                        categories += item.CategoryName;
                                                        classifications += this._businessObjects.Categories.GetParentCategory(item.CategoryId, CategoryHerarchyLevels.ProductCategory);
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

                                            bool success = await SearchObjectHandler.UpdateGeneralSearchableObjectAsync(updatedOffer.Id, updatedOffer.Keywords, updatedOffer.IsSponsored, updatedOffer.IsActive, updatedOffer.RelevanceRate, updatedOffer.ReleaseDate, updatedOffer.ExpirationDate, 
                                                updatedOffer.MainHint + " " + updatedOffer.ComplementaryHint, updatedOffer.Name, updatedOffer.MainCategoryName, categories, classifications, updatedOffer.Value, 1 );
                                        }

                                        result = Ok(this.GetDealContent(updatedOffer, updatedOffer.OfferType));
                                    }
                                    else
                                    {
                                        result = new BadRequestObjectResult(
                                        new ErrorResponse
                                        {
                                            ErrorCode = Values.StatusCodes.BadRequest,
                                            ShowErrorToUser = true,
                                            InnerError = "Error at creation procceess",
                                            PublicError = "No hemos podido editar esta promo, por favor revisa el formulario y trata de nuevo"
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
        public async Task<IActionResult> PutActiveStateAsync([FromBody] DealModifierById model)
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

                    ObjectStateUpdate stateUpdate = this._businessObjects.Offers.Put(model.Id, model.OfferType, ChangeTypes.ActiveState);

                    if (stateUpdate != null)
                    {
                        Offer updatedOffer = this._businessObjects.Offers.Get(model.Id, model.OfferType, true);

                        //Needs to update it to Algolia
                        if (updatedOffer.DisplayType < DisplayTypes.BroadcastingOnly)//If the offer will be publicly accessible
                        {

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

                            bool success = await SearchObjectHandler.UpdateSearchableObjectActiveStateAsync(updatedOffer.Id, updatedOffer.IsActive);
                        }

                        SuccessResponse response = new SuccessResponse
                        {
                            StatusCode = Values.StatusCodes.Ok,
                            ShowMsgToUser = true,
                        };

                        if (stateUpdate.NewState)
                        {
                            response.MessageToDisplay = "La promo ha sido activada éxitosamente";
                        }
                        else
                        {
                            response.MessageToDisplay = "La promo ha sido desactivada éxitosamente";
                        }

                        result = Ok(response);
                    }
                    else
                    {
                        result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    }
                }
                catch(Exception e)
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
        public async Task<IActionResult> DeleteAsync([FromBody] DealModifierById model)
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
                    Guid? deletedId = this._businessObjects.Offers.Delete(model.Id, model.OfferType);

                    if (deletedId != null)
                    {

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

                        bool success = await SearchObjectHandler.DeleteSearchableObjectAsync(((Guid)deletedId).ToString());

                        SuccessResponse response = new SuccessResponse
                        {
                            StatusCode = Values.StatusCodes.Ok,
                            ShowMsgToUser = true,
                            MessageToDisplay = "La promo se ha eliminado éxitosamente"
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
