using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;

namespace YOY.UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedContentController : ControllerBase
    {
        #region PROPERTIES
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private static Tenant _tenant;
        private BusinessObjects _businessObjects;
        private readonly IStringLocalizer<SharedResources> _localizer;


        private const double logoWithWidthProp = 1;
        private const double dealImgWidthProp = 1;
        private const double commerceCarrouselImgWidthProp = 0.375;

        private const int MaxMinsToStoreHeaderContent = 30;
        public const int MaxMinsToStoreBodyContent = 10;

        public const int MaxMetersToStoreContent = 1000;

        private const int MaxContentCellsOnCarrousel = 8;
        private const int MaxContentCellsOnGrid = 16;

        private const int controllerVersion = 1;
        #endregion

        #region CONSTRUCTORS
        public SavedContentController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }
        #endregion
    }
}
