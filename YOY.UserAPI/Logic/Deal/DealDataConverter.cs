using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.DTO.Entities.Misc.Offer;
using YOY.UserAPI.Models.v1.Deal.POCO;
using YOY.Values;

namespace YOY.UserAPI.Logic.Deal
{

    public static class DealDataConverter
    {

        private static int minAvailableQuantityToDisplayHint = 20;
        private static int minDaysLeftToDisplayHint = 5;
        private static int maxDaysToConsiderAsNew = 7;
        private static IStringLocalizer<SharedResources> _localizer;

        private static string GetDealTypeName(int type)
        {
            string typeName = type switch {
                DealTypes.InStore => _localizer["InStore"].Value,
                DealTypes.Online => _localizer["Online"].Value,
                DealTypes.Phone => _localizer["PhoneDeal"].Value,
                _ => ""
            };

            return typeName;
        }
        private static string GetDealTypeIconUrl(int type)
        {
            string typeName = type switch
            {
                DealTypes.InStore => "https://res.cloudinary.com/yoyimgs/image/upload/v1597767894/global/deal_icons/instore-deal.png",
                DealTypes.Online => "https://res.cloudinary.com/yoyimgs/image/upload/v1597767894/global/deal_icons/online-deal.png",
                DealTypes.Phone => "https://res.cloudinary.com/yoyimgs/image/upload/v1597767894/global/deal_icons/phone-deal.png",
                _ => ""
            };

            return typeName;
        }

        public static List<DealDisplayData> ProccessDeals(List<FlattenedOfferData> offers, IStringLocalizer<SharedResources> localizer)
        {
            List<DealDisplayData> dealDisplayData;

            _localizer = localizer;

            try
            {
                dealDisplayData = new List<DealDisplayData>();
                DealDisplayData displayData;

                if(offers?.Count > 0)
                {
                    foreach(FlattenedOfferData item in offers)
                    {
                        if(item.Offer.AvailableQuantity == -1 || item.Offer.AvailableQuantity > 0)
                        {
                            displayData = new DealDisplayData
                            {
                                Id = item.Offer.Id,
                                ExtraHint = "",
                                DisplayExtraHint = false,
                                DealType = item.Offer.DealType,
                                DealTypeName = GetDealTypeName(item.Offer.DealType),
                                DealTypeIcon = GetDealTypeIconUrl(item.Offer.DealType),
                                Favorite = false,
                                CommerceLogo = item.Tenant.LogoUrl,
                                ImgUrl = item.Offer.DisplayImgUrl,
                                MainHint = item.Offer.MainHint,
                                ComplementaryHint = item.Offer.ComplementaryHint,
                                DisplayPrice = item.Offer.OfferType == OfferTypes.Offer,
                                Price = item.Offer.Value,
                                DisplayRegularPrice = item.Offer.RegularValue != null,
                                RegularPrice = item.Offer.RegularValue ?? 0,
                                CurrencySymbol = item.Tenant.CurrencySymbol,
                                CashbackHint = item.Tenant.CashbackPercentage + "% de cashback",
                                DisplayCashbackHint = true,
                                AvailableQuantity = item.Offer.AvailableQuantity != -1 ? item.Offer.AvailableQuantity : int.MaxValue,
                                DisplayAvailableQuantityHint = item.Offer.AvailableQuantity != -1 && item.Offer.AvailableQuantity <= minAvailableQuantityToDisplayHint,
                                ExpirationDate = item.Offer.ExpirationDate.ToString("yyyy-MM-dd HH':'mm':'ss"),
                                DisplayExpirationHint = (item.Offer.ExpirationDate - DateTime.UtcNow).TotalDays <= minDaysLeftToDisplayHint,
                                HasPreferences = item.Offer.HasPreferences,
                                IsNew = (DateTime.UtcNow - item.Offer.CreatedDate).TotalDays < maxDaysToConsiderAsNew

                            };

                            if (displayData.DisplayAvailableQuantityHint)
                            {
                                if (displayData.AvailableQuantity > 5)
                                {
                                    displayData.AvailableQuantityHint = _localizer["LeftHint", displayData.AvailableQuantity].Value;
                                }
                                else
                                {
                                    if (displayData.AvailableQuantity > 1)
                                    {
                                        displayData.AvailableQuantityHint = _localizer["OnlyLeftHintMoreThanOne", displayData.AvailableQuantity].Value;
                                    }
                                    else
                                    {
                                        displayData.AvailableQuantityHint = _localizer["OnlyOneLeft"];
                                    }
                                }
                            }
                        }
                        
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
