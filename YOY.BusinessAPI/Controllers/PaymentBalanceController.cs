using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YOY.BusinessAPI.Models.v1.Misc.BasicResponse.POCO;
using YOY.BusinessAPI.Models.v1.PaymentBalance.POCO;
using YOY.BusinessAPI.Models.v1.PaymentBalance.SET;
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
    public class PaymentBalanceController : ControllerBase
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

        private MoneyTransferDataSet BuildMoneyTransfersData(Guid branchId, Guid tenantId, string userId, DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            MoneyTransferDataSet moneyTransferDataSet = new MoneyTransferDataSet
            {
                StartDate = start,
                EndDate = end,
                TenantId = tenantId,
                BranchId = branchId,
                Transfers = new List<MoneyTransferData>(),
                TotalRecords = 0,
                TotalAmount = 0
            };


            //Use test data
            int daysDiff = (int)Math.Floor((end - start).TotalDays);


            Random random = new Random();
            MoneyTransferData moneyTransferData;
            DateTime date;
            int status;
            decimal total;
            decimal totalAmount = 0;
            //int[] purchaseStatuses = { PurchaseStatuses.Placed, PurchaseStatuses.Payed, PurchaseStatuses.Delivered };
            string[] purchaseStatusesName = { "Completada", "Pagada", "Entregada" };

            for (int i = 0; i < 550 && i < pageSize; ++i)
            {
                date = start.AddDays(random.Next(daysDiff + 1));
                status = random.Next(3);
                total = (decimal)(random.NextDouble() * 25000);


                moneyTransferData = new MoneyTransferData
                {
                    Id = Guid.Empty,
                    DestinationId = i+"IBANC85955-5U4JD3",
                    Subject = "Pago 2da-Agosto 2020"+random.Next(8455),
                    Details = "Pago correspondiente por las ventas y pagos generados del 1 de Agosto a 14 de Agosto, 2020. Depositamos el día 17 de Agosto. " + random.Next(43540),
                    DestinationTypeName = "Cuenta bancaria",
                    DestinationName = "BAC Credomatic"+i,
                    BeneficiaryId = i + "3-101-48566-848485",
                    BeneficiaryTypeName  = "Cédula jurídica",
                    BeneficiaryName = i + "Propietario comercio",
                    StatusName = "Transferido",
                    TransferredAmount = "$"+total,
                    CreatedDate = date.AddMinutes(-1 * random.Next(30)),
                    ReferenceCode = i + "IRNDJE3" + random.Next(8494),
                    TotalSalesCount = random.Next(500),
                    EnableReportRequest = random.Next(1300)%2 == 0 
                };

                moneyTransferData.CreatedDateLiteral = moneyTransferData.CreatedDate.Day + "/" + monthAbbreviations[date.Month] + "/" + date.Year + " " + moneyTransferData.CreatedDate.Hour + ":" + moneyTransferData.CreatedDate.Minute;

                date = date.AddDays(random.Next(10));
                moneyTransferData.UpdatedDate = date;
                moneyTransferData.UpdatedDateLiteral = date.Day + "/" + monthAbbreviations[date.Month] + "/" + date.Year + " " + date.Hour + ":" + date.Minute;

                totalAmount += total;

                moneyTransferDataSet.Transfers.Add(moneyTransferData);
            }

            moneyTransferDataSet.TotalRecords = moneyTransferDataSet?.Transfers?.Count ?? 0;
            moneyTransferDataSet.TotalAmount = totalAmount;

            return moneyTransferDataSet;
        }


        [Route("gets")]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult Gets(Guid employeeId, Guid branchId, Guid tenantId, string userId, DateTime startDate, DateTime endDate, int pageSize, int pageNumber)
        {
            IActionResult result;
            MoneyTransferDataSet moneyTransferDataSet;

            string errorMsg;
            int callId = 1;
            string parameters = "EmployeeId: " + employeeId + " - StartDate: " + startDate + " - EndDate: " + endDate + " - PageSize: " + pageSize + " - PageNumber: " + pageNumber;

            try
            {
                Initialize(tenantId, userId);

                moneyTransferDataSet = this.BuildMoneyTransfersData(branchId, tenantId, userId, startDate, endDate, pageSize, pageNumber);

                result = Ok(moneyTransferDataSet);
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

        [Route("generateReport")]
        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult GenerateReport([FromBody] ReportRequest model)
        {
            IActionResult result;

            string errorMsg;
            int callId = 2;
            string parameters = model.ToString();

            try
            {
                Initialize(model.TenantId, model.UserId);

                SuccessResponse response = new SuccessResponse
                {
                    StatusCode = Values.StatusCodes.Ok,
                    ShowMsgToUser = true,
                    MessageToDisplay = "Te enviaremos un reporte detallado por correo electrónico"
                };

                result = Ok(response);
            }
            catch (Exception e)
            {
                errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
            }

            return result;
        }

        #endregion
    }
}
