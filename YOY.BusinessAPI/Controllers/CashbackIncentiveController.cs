using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YOY.DAO.Entities;
using YOY.DAO.Entities.Manager.Misc.Image;

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

        private const int mainHintMinLength = 3;
        private const int mainHintMaxLength = 9;
        private const int complementaryHintMinLength = 3;
        private const int complementaryHintMaxLength = 9;
        private const int nameMinLength = 10;
        private const int nameMaxLength = 64;
        private const int keywordsMaxLength = 1000;
        public const int codeMaxLenght = 15;
        private const int descriptionMinLength = 10;
        private const int descriptionMaxLength = 64;
        private const int availableQuantityMinValue = 30;
        public const int infiteAvailableQuantity = -1;

        #endregion
    }
}
