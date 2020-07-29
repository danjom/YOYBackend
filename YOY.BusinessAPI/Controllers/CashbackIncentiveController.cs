using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YOY.BusinessAPI.Models.v1.CashbackIncentive.POCO;
using YOY.BusinessAPI.Models.v1.CashbackIncentive.SET;
using YOY.BusinessAPI.Models.v1.Misc.BasicResponse.POCO;
using YOY.DAO.Entities;
using YOY.DAO.Entities.Manager.Misc.Image;
using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.BusinessAPI.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CashbackIncentiveController : ControllerBase
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


        private const int nameMinLength = 10;
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

        private IncentiveData GetIncentiveContent(CashbackIncentive data)
        {
            IncentiveData incentive = null;

            incentive = new IncentiveData
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
                Description = data.Description,
                Keywords = data.Keywords,
                IsActive = data.IsActive,
                IsSponsored = data.IsSponsored,
                ValidHours = data.ValidHours,
                ValidWeekDays = data.ValidWeekDays,
                ValidMonthDays = data.ValidMonthDays,
                MaxUsagePerUser = data.MaxUsagePerUser,
                MinPurchasesCountToUse = data.MinPurchasesCountToUse,
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

        public DateTime LocalToUtc(DateTime date, string hourString, int utcTimeDiff)
        {
            DateTime finalDate = DateTime.MinValue;

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

        private List<IncentiveData> GetIncentivesData(int pageSize, int pageNumber)
        {
            List<IncentiveData> incentivesData;

            try
            {
                List<CashbackIncentive> incentives = this._businessObjects.CashbackIncentives.Gets(ExpiredStates.All, ActiveStates.All, ReleaseStates.All, DateTime.UtcNow, pageSize, pageNumber);

                if (incentives?.Count > 0)
                {
                    incentivesData = new List<IncentiveData>();

                    foreach (CashbackIncentive item in incentives)
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
                count = this._businessObjects.CashbackIncentives.GetCashbackIncentivesCountByDateRange(this._businessObjects.Tenant.TenantId, SearchableObjectTypes.Commerce, startDate, endDate);
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

                    Task<List<IncentiveData>> getIncentivesDataTask = new Task<List<IncentiveData>>(() => this.GetIncentivesData(pageSize, pageNumber));
                    getIncentivesDataTask.Start();

                    Task<int> getIncentivesCountTask = new Task<int>(() => this.GetTotalIncentivesCount(startDate, endDate));
                    getIncentivesCountTask.Start();

                    incentives.Incentives = await getIncentivesDataTask;
                    incentives.Count = incentives.Incentives?.Count ?? 0;

                    incentives.TotalRecords = await getIncentivesCountTask;

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



        #endregion

    }
}
