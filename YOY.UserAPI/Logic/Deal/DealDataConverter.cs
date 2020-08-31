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

        private static double CalculateOverAllScore(double mainCategoryRelevanceScore, double preferenceRelevanceScore, double tenantRelevanceScore, double dealRelevanceRate, double tenantRelevanceRate, bool isSponsored, char genderParam, int minAgeParam, int maxAgeParam, char userGender, int? userAge)
        {
            double score = 0;

            try
            {
                //Current logic to calculate score is 35% comes from MainCategoryRelevanceScore, 25% from Preferce Score
                //20% from tenant Score.

                double mainCategoryRelevanceScoreWeght = 0.35;
                double preferenceRelevanceScoreWeight = 0.25;
                double tenantRelevanceScoreWeight = 0.2;

                if(mainCategoryRelevanceScore > 0)
                {
                    if (preferenceRelevanceScore > 0)
                    {
                        if(tenantRelevanceScore > 0)
                        {
                            score = mainCategoryRelevanceScore * mainCategoryRelevanceScoreWeght + preferenceRelevanceScore * preferenceRelevanceScoreWeight + tenantRelevanceScore * tenantRelevanceScoreWeight;
                        }
                        else
                        {
                            score = mainCategoryRelevanceScore * mainCategoryRelevanceScoreWeght + (preferenceRelevanceScore * (preferenceRelevanceScoreWeight + tenantRelevanceScoreWeight))*0.7;
                        }
                    }
                    else
                    {
                        if (tenantRelevanceScore > 0)
                        {
                            score = (mainCategoryRelevanceScore*(mainCategoryRelevanceScoreWeght + preferenceRelevanceScoreWeight))*0.7 + tenantRelevanceScore * tenantRelevanceScoreWeight;
                        }
                        else
                        {
                            score = (mainCategoryRelevanceScore * (mainCategoryRelevanceScoreWeght + preferenceRelevanceScoreWeight + tenantRelevanceScoreWeight))*0.4;
                        }
                    }
                }
                else
                {
                    if (preferenceRelevanceScore > 0)
                    {
                        if (tenantRelevanceScore > 0)
                        {
                            score = (preferenceRelevanceScore * (mainCategoryRelevanceScoreWeght + preferenceRelevanceScoreWeight))*0.7 + tenantRelevanceScore * tenantRelevanceScoreWeight;
                        }
                        else
                        {
                            score = (preferenceRelevanceScore * (mainCategoryRelevanceScoreWeght + preferenceRelevanceScoreWeight + tenantRelevanceScoreWeight)) * 0.4;
                        }
                    }
                    else
                    {
                        if (tenantRelevanceScore > 0)
                        {
                            score = (tenantRelevanceScore * (mainCategoryRelevanceScoreWeght + preferenceRelevanceScoreWeight + tenantRelevanceScoreWeight)) * 0.4;
                        }
                        else
                        {
                            score = 1;
                        }
                    }
                }

                //Now check the target params
                switch (userGender)
                {
                    case ProfileGenders.Male:

                        if (string.Equals(genderParam, GenderParams.OnlyFemale))
                        {
                            score *= 0.4;
                        }
                        else
                        {
                            if(string.Equals(genderParam, GenderParams.MostLikelyFemale))
                            {
                                score *= 0.7;
                            }
                        }

                        break;
                    case ProfileGenders.Female:

                        if (string.Equals(genderParam, GenderParams.OnlyMale))
                        {
                            score *= 0.4;
                        }
                        else
                        {
                            if (string.Equals(genderParam, GenderParams.MostLikelyMale))
                            {
                                score *= 0.7;
                            }
                        }

                        break;
                }

                if(userAge != null)
                {
                    if (userAge < minAgeParam || userAge > maxAgeParam)
                    {
                        score *= 0.65;
                    }
                }

                if(score > 0)
                {
                    //Tenant relevance status can increase score upto 10 %, deal relevance status can increse score upto 20 %

                    double tenantRateIncreaser = ((tenantRelevanceRate) / TenantRelevanceStatuses.AnchorBusiness)*0.1;

                    double dealRateIncreaser = ((dealRelevanceRate) / 5)*0.2;//max deal rate is 5

                    score *= 1 + (tenantRateIncreaser + dealRateIncreaser);
                }

                if (isSponsored)
                {
                    //If it's sponsored increases score 50%
                    score *= 1 + 0.5;
                }

            }
            catch (Exception)
            {

            }

            return score;
        }

        public static List<DealDisplayData> ProccessDeals(List<FlattenedOfferData> offers, IStringLocalizer<SharedResources> localizer, char userGender, int? userAge)
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
                                CurrencySymbol = item.Tenant.CurrencySymbol,
                                CashbackHint = "",
                                DisplayCashbackHint = false,
                                AvailableQuantity = item.Offer.AvailableQuantity != -1 ? item.Offer.AvailableQuantity : int.MaxValue,
                                DisplayAvailableQuantityHint = item.Offer.AvailableQuantity != -1 && item.Offer.AvailableQuantity <= minAvailableQuantityToDisplayHint,
                                ExpirationDate = item.Offer.ExpirationDate.ToString("yyyy-MM-dd HH':'mm':'ss"),
                                DisplayExpirationHint = (item.Offer.ExpirationDate - DateTime.UtcNow).TotalDays <= minDaysLeftToDisplayHint,
                                HasPreferences = item.Offer.HasPreferences,
                                IsSponsored = item.Offer.IsSponsored,
                                IsNew = (DateTime.UtcNow - item.Offer.CreatedDate).TotalDays < maxDaysToConsiderAsNew,
                                DealRelevanceRate = item.Offer.RelevanceRate,
                                TenantRelevanceScore = item.Tenant.RelevanceScore ?? -1,
                                TenantRelevanceRate = item.Tenant.RelevanceStatus,
                                PurchaseCount = item.Offer.ClaimCount,
                                MainCategoryRelevanceScore = item.Offer.RelevanceScoreForMainCategory ?? -1,
                                PreferenceRelevanceScore = item.Preference.RelevanceScore ?? -1,
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

                            if (!string.IsNullOrWhiteSpace(item.Offer.TargettingParams))
                            {
                                char[] paramsSeparator = { TargettingParamMarks.ParamsSeparator[0] };
                                //1st split all the params
                                string[] paramsByType = item.Offer.TargettingParams.Split(paramsSeparator);

                                for (int i = 0; i < paramsByType.Length; ++i)
                                {
                                    char[] paramValueSeparator = { TargettingParamMarks.TypeValueSeparator[0] };

                                    string[] paramValues = paramsByType[i].Split(paramValueSeparator);

                                    char[] valuesSeparator = { TargettingParamMarks.ValueSeparator[0] };

                                    switch (paramValues[0])
                                    {
                                        case TargettingParamMarks.Gender:

                                            displayData.GenderFocus = paramValues[1][0];

                                            break;
                                        case TargettingParamMarks.AgeInterval:

                                            string[] values = paramValues[1].Split(valuesSeparator);

                                            int startAge;
                                            int.TryParse(values[0], out startAge);
                                            displayData.MinAge = startAge;

                                            int endAge;
                                            int.TryParse(values[1], out endAge);
                                            displayData.MaxAge = endAge;

                                            break;
                                    }

                                }
                            }
                            else
                            {
                                displayData.GenderFocus = GenderParams.Any;
                                displayData.MinAge = 1;
                                displayData.MaxAge = 99;
                            }

                            displayData.OverallScore = CalculateOverAllScore((double)displayData.MainCategoryRelevanceScore, (double)displayData.PreferenceRelevanceScore, (double)displayData.TenantRelevanceScore, displayData.DealRelevanceRate, displayData.TenantRelevanceRate, displayData.IsSponsored, displayData.GenderFocus, displayData.MinAge, displayData.MaxAge, userGender, userAge);

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
