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
        private static int minAvailableQuantityToDisplayVeryFewHint = 7; 
        private static int minAvailableQuantityToDisplayLastOnesHint = 1;
        private static int minDaysLeftToDisplayHint = 5;
        private static int maxDaysToConsiderAsNew = 7;
        private static double extraHintCashbackPercentageLowerReference = 10;
        private static double extraHintCashbackPercentageUpperReference = 25;
        private static int extraHintCashbackAmountLengthReference = 5;
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

        private static string FormatDecimalNumberString(decimal number)
        {
            var s = string.Format("{0:n}", number);

            if (s.EndsWith(".00"))
            {
                return s.Replace(".00","");
            }
            else
            {
                return s;
            }
        }

        private static string FormatDoubleNumberString(double number)
        {
            var s = string.Format("{0:n}", number);

            if (s.EndsWith(".00"))
            {
                return s.Replace(".00", "");
            }
            else
            {
                return s;
            }
        }

        public static List<DealDisplayData> ProccessDeals(List<FlattenedOfferData> offers, IStringLocalizer<SharedResources> localizer)
        {
            List<DealDisplayData> dealDisplayData;

            _localizer = localizer;

            double cashbackAmount;

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
                                CommerceId = item.Offer.TenantId,
                                DealType = item.Offer.DealType,
                                DealTypeName = GetDealTypeName(item.Offer.DealType),
                                DealTypeIcon = GetDealTypeIconUrl(item.Offer.DealType),
                                Favorite = false,
                                CommerceLogoUrl = item.Tenant.LogoUrl,
                                CommerceWhiteLogoUrl = item.Tenant.WhiteLogoUrl,
                                DisplayImgUrl = item.Offer.DisplayImgUrl,
                                MainHint = item.Offer.MainHint,
                                ComplementaryHint = item.Offer.ComplementaryHint,
                                Name = item.Offer.ProductHint,
                                DisplayPrice = item.Offer.OfferType == OfferTypes.Offer,
                                Price = item.Offer.Value,
                                PriceLiteral = item.Tenant.CurrencySymbol + FormatDecimalNumberString(item.Offer.Value),
                                DisplayRegularPrice = item.Offer.RegularValue != null,
                                RegularPrice = item.Offer.RegularValue ?? 0,
                                RegularPriceLiteral = item.Offer.RegularValue != null ? item.Tenant.CurrencySymbol + FormatDecimalNumberString((decimal)item.Offer.RegularValue) : "",
                                CurrencySymbol =  item.Tenant.CurrencySymbol,
                                CashbackHint = "",
                                DisplayCashbackHint = false,
                                AvailableQuantity = item.Offer.AvailableQuantity != -1 ? item.Offer.AvailableQuantity : int.MaxValue,
                                DisplayAvailableQuantityHint = item.Offer.AvailableQuantity != -1 && item.Offer.AvailableQuantity <= minAvailableQuantityToDisplayHint,
                                ExpirationDate = item.Offer.ExpirationDate.ToString("yyyy-MM-dd HH':'mm':'ss"),
                                DisplayExpirationHint = (item.Offer.ExpirationDate - DateTime.UtcNow).TotalDays <= minDaysLeftToDisplayHint,
                                HasPreferences = item.Offer.HasPreferences,
                                IsNew = (DateTime.UtcNow - item.Offer.CreatedDate).TotalDays < maxDaysToConsiderAsNew

                            };

                            if (displayData.DisplayAvailableQuantityHint)
                            {

                                if (displayData.AvailableQuantity > minAvailableQuantityToDisplayVeryFewHint)
                                {
                                    displayData.AvailableQuantityHint = _localizer["LeftHint", displayData.AvailableQuantity].Value;
                                }
                                else
                                {
                                    if (displayData.AvailableQuantity > minAvailableQuantityToDisplayLastOnesHint)
                                    {
                                        displayData.AvailableQuantityHint = _localizer["OnlyLeftHintMoreThanOne", displayData.AvailableQuantity].Value;
                                    }
                                    else
                                    {
                                        displayData.AvailableQuantityHint = _localizer["OnlyOneLeft"];
                                    }
                                }
                            }

                            if(!displayData.DisplayAvailableQuantityHint && !displayData.DisplayExpirationHint)
                            {
                                if (item.Offer.ExtraBonusType != ExtraBonusTypes.None)
                                {
                                    switch (item.Offer.ExtraBonusType)
                                    {
                                        case ExtraBonusTypes.FixedAmount:
                                            displayData.CashbackHint = _localizer["CashbackPercentageLabel", displayData.CurrencySymbol + FormatDoubleNumberString(item.Offer.ExtraBonus)];
                                            break;
                                        case ExtraBonusTypes.Percentage:
                                            if (item.Offer.ExtraBonus < extraHintCashbackPercentageLowerReference)
                                            {
                                                cashbackAmount = ((double)item.Offer.Value * item.Offer.ExtraBonus) / 100;

                                                if (cashbackAmount.ToString().Length > extraHintCashbackAmountLengthReference)
                                                    displayData.CashbackHint = _localizer["CashbackPercentageLabel", displayData.CurrencySymbol + FormatDoubleNumberString(cashbackAmount)];
                                                else
                                                    displayData.CashbackHint = _localizer["CashbackPercentageLabel", FormatDoubleNumberString(item.Offer.ExtraBonus) + "%"];
                                            }
                                            else
                                            {
                                                if (item.Offer.ExtraBonus < extraHintCashbackPercentageUpperReference)
                                                {
                                                    cashbackAmount = ((double)item.Offer.Value * item.Offer.ExtraBonus) / 100;

                                                    displayData.CashbackHint = _localizer["CashbackPercentageLabel", displayData.CurrencySymbol + FormatDoubleNumberString(cashbackAmount)];
                                                }
                                                else
                                                {
                                                    displayData.CashbackHint = _localizer["CashbackPercentageLabel", FormatDoubleNumberString(item.Offer.ExtraBonus) + "%"];
                                                }
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    displayData.CashbackHint = item.Tenant.CashbackPercentage > 0 ? _localizer["CashbackPercentageLabel", FormatDoubleNumberString(item.Tenant.CashbackPercentage) + "%"].Value : "";

                                    displayData.DisplayCashbackHint = true;
                                }
                                    

                            }

                            dealDisplayData.Add(displayData);
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
