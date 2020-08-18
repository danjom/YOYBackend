using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.DTO.Entities.Misc.Offer;
using YOY.UserAPI.Models.v1.Deal.POCO;

namespace YOY.UserAPI.Logic.Deal
{
    public static class DealDataConverter
    {
        public static List<DealDisplayData> ProccessDeals(List<FlattenedOfferData> offers, IStringLocalizer<SharedResources> localizer)
        {
            List<DealDisplayData> dealDisplayData;

            try
            {
                dealDisplayData = new List<DealDisplayData>();
                DealDisplayData displayData;

                if(offers?.Count > 0)
                {
                    foreach(FlattenedOfferData item in offers)
                    {
                        displayData = new DealDisplayData
                        {

                        };
                    }
                }
            }
            catch (Exception)
            {
                dealDisplayData = null;
            }

            return dealDisplayData;
        }
    }
}
