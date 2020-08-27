using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YOY.BusinessAPI.Models.v1.Sale.POCO;
using YOY.BusinessAPI.Models.v1.Sale.SET;
using YOY.DAO.Entities;
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
    public class SaleController : ControllerBase
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
        private readonly string[] monthAbbreviations = { "", Resources.JanuaryAbbr, Resources.FebruaryAbbr, Resources.MarchAbbr, Resources.AprilAbbr, Resources.MayAbbr, Resources.JuneAbbr, Resources.JulyAbbr, Resources.AugustAbbr, Resources.SeptemberAbbr, Resources.OctoberAbbr, Resources.NovemberAbbr, Resources.DecemberAbbr };

        #endregion

        #region METHODS

        private static double SignificantTruncate(double num, int significantDigits)
        {
            double y = Math.Pow(10, significantDigits);
            return Math.Truncate(num * y) / y;
        }

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

        private SaleDataSet BuildSaleDataFromPurchases(Guid branchId, Guid tenantId, string userId, DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            SaleDataSet  saleDataSet = new SaleDataSet
            {
                StartDate = start,
                EndDate = end,
                TenantId = tenantId,
                BranchId = branchId,
                Sales = new List<SaleData>(),
                PendingAmount = "",
                TotalRecords = 0,
                TotalAmount = "",
                WithdrawedAmount = ""
            };


            //Use test data
            int daysDiff = (int)Math.Floor((end - start).TotalDays);

            string branchName = "Branch";
            string commerceName = "Commerce";

            if (branchId != Guid.Empty)
            {
                Branch branch = this._businessObjects.Branches.Get(branchId, false, DateTime.UtcNow);

                if (branch != null)
                {
                    branchName = branch.Name;
                }
            }

            if (tenantId != null)
            {
                TenantInfo info = this._businessObjects.Commerces.Get(tenantId, CommerceKeys.TenantKey);

                if (info != null)
                    commerceName = info.Name;
            }


            Random random = new Random();
            SaleData saleData;
            DateTime date;
            int status;
            decimal total;
            decimal totalAmount = 0;
            decimal totalPending = 0;
            //int[] purchaseStatuses = { PurchaseStatuses.Placed, PurchaseStatuses.Payed, PurchaseStatuses.Delivered };
            string[] purchaseStatusesName = { "Completada", "Pagada", "Entregada" };

            for(int i = 0; i< 500 && i < pageSize; ++i)
            {
                date = start.AddDays(random.Next(daysDiff + 1));
                status = random.Next(3);
                total = (decimal)SignificantTruncate((random.NextDouble() * 25000),2);


                saleData = new SaleData
                {
                    Id = Guid.Empty,
                    BranchName = branchName + i +"-"+random.Next(400,999),
                    CommerceName = commerceName + tenantId,
                    PaymentStatusName = "Completado",
                    SaleStatusName = purchaseStatusesName[status],
                    CompletedDate = date,
                    CompletedDateLiteral = date.Day+ "/" + monthAbbreviations[date.Month] + "/" + date.Year + " " + date.Hour + ":" + date.Minute,
                    CreatedDate = date.AddMinutes(-1 * random.Next(30)),
                    ReferenceCode = i+"IRNDJE3"+random.Next(5860),
                    SaleType = SaleTypes.Purchase,
                    SaleTypeName = "Compra de promo",
                    CommerceEarnings = total * 0.8M,
                    TotalAmount = total,
                    LiquidationStatusName = "Depositado",
                    LiquidationReferenceCode = i + "FGURH34N" + random.Next(7450)
                };

                saleData.CreatedDateLiteral = saleData.CreatedDate.Day + "/" + monthAbbreviations[date.Month] + "/" + date.Year + " " + saleData.CreatedDate.Hour + ":" + saleData.CreatedDate.Minute;

                date = date.AddDays(random.Next(10));
                saleData.LiquidationDateLiteral = date.Day + "/" + monthAbbreviations[date.Month] + "/" + date.Year + " " + date.Hour + ":" + date.Minute;

                totalAmount += total;

                if (i % 2 == 0)
                    totalPending += total;

                saleDataSet.Sales.Add(saleData);
            }

            saleDataSet.TotalRecords = saleDataSet?.Sales?.Count ?? 0;
            saleDataSet.TotalAmount = "$" + totalAmount;
            saleDataSet.PendingAmount = "$" + totalPending;
            saleDataSet.WithdrawedAmount = "$" + (totalAmount - totalPending);

            return saleDataSet;
        }

        private SaleDataSet BuildSaleDataFromAppPayments(Guid branchId, Guid tenantId, string userId, DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            SaleDataSet saleDataSet = new SaleDataSet
            {
                StartDate = start,
                EndDate = end,
                TenantId = tenantId,
                BranchId = branchId,
                Sales = new List<SaleData>(),
                PendingAmount = "",
                TotalRecords = 0,
                TotalAmount = "",
                WithdrawedAmount = ""
            };

            string branchName = "Branch";
            string commerceName = "Commerce";

            if(branchId != Guid.Empty)
            {
                Branch branch = this._businessObjects.Branches.Get(branchId, false, DateTime.UtcNow);

                if (branch != null)
                {
                    branchName = branch.Name;
                }
            }

            if(tenantId != null)
            {
                TenantInfo info = this._businessObjects.Commerces.Get(tenantId, CommerceKeys.TenantKey);

                if (info != null)
                    commerceName = info.Name;
            }

            //Use test data
            int daysDiff = (int)Math.Floor((end - start).TotalDays);


            Random random = new Random();
            SaleData saleData;
            DateTime date;
            int status;
            decimal total;
            decimal totalAmount = 0;
            decimal totalPending = 0;

            for (int i = 0; i < 500 && i < pageSize; ++i)
            {
                date = start.AddDays(random.Next(daysDiff + 1));
                status = random.Next(3);
                total = (decimal)SignificantTruncate((random.NextDouble() * 25000),2);



                saleData = new SaleData
                {
                    Id = Guid.Empty,
                    BranchName = i + branchName + random.Next(350),
                    CommerceName = commerceName + tenantId,
                    PaymentStatusName = "Completado",
                    SaleStatusName = "Pagado por el usuario",
                    CompletedDate = date,
                    CompletedDateLiteral = date.Day + "/" + monthAbbreviations[date.Month] + "/" + date.Year + " " + date.Hour + ":" + date.Minute,
                    CreatedDate = date.AddMinutes(-1 * random.Next(30)),
                    ReferenceCode = i + "KFE4T4IG" + random.Next(1200),
                    SaleType = SaleTypes.Payment,
                    SaleTypeName = "Pago con YOY",
                    CommerceEarnings = total * 0.8M,
                    TotalAmount = total,
                    LiquidationStatusName = "Depositado",
                    LiquidationReferenceCode = i + "ZJFRGR9JF" + random.Next(4200),
                };

                saleData.CreatedDateLiteral = saleData.CreatedDate.Day + "/" + monthAbbreviations[date.Month] + "/" + date.Year + " " + saleData.CreatedDate.Hour + ":" + saleData.CreatedDate.Minute;

                date = date.AddDays(random.Next(10));
                saleData.LiquidationDateLiteral = date.Day + "/" + monthAbbreviations[date.Month] + "/" + date.Year + " " + date.Hour + ":" + date.Minute;

                totalAmount += total;

                if (i % 2 == 0)
                    totalPending += total;

                saleDataSet.Sales.Add(saleData);
            }

            saleDataSet.TotalRecords = saleDataSet?.Sales?.Count ?? 0;
            saleDataSet.TotalAmount = "$" + totalAmount;
            saleDataSet.PendingAmount = "$" + totalPending;
            saleDataSet.WithdrawedAmount = "$" + (totalAmount - totalPending);

            return saleDataSet;
        }

        [Route("gets")]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult Gets(Guid employeeId, Guid branchId, Guid tenantId, string userId, int saleType, DateTime startDate, DateTime endDate, int pageSize, int pageNumber)
        {
            IActionResult result;
            SaleDataSet saleDataSet = new SaleDataSet
            {
                StartDate = startDate,
                EndDate = endDate,
                TenantId = tenantId,
                BranchId = branchId,
                Sales = new List<SaleData>(),
                PendingAmount = "",
                TotalRecords = 0,
                TotalAmount = "",
                WithdrawedAmount = ""
            };

            string errorMsg;
            int callId = 1;
            string parameters = "EmployeeId: " + employeeId + " - StartDate: " + startDate + " - EndDate: " + endDate + " - PageSize: " + pageSize + " - PageNumber: " + pageNumber;

            try
            {
                Initialize(tenantId, userId);

                switch (saleType)
                {
                    case SaleTypes.Purchase:
                        saleDataSet = this.BuildSaleDataFromPurchases(branchId, tenantId, userId, startDate, endDate, pageSize, pageNumber);
                        break;
                    case SaleTypes.Payment:
                        saleDataSet = this.BuildSaleDataFromAppPayments(branchId, tenantId, userId, startDate, endDate, pageSize, pageNumber);
                        break;
                }

                result = Ok(saleDataSet);
            }
            catch(Exception e)
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
