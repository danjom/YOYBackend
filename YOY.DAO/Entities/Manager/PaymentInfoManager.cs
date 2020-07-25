using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class PaymentInfoManager
    {
        #region PROPERTIES_AND_RESOURCES

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private readonly BusinessObjects _businessObjects;

        #endregion

        #region METHODS

        public string GetFundingTypeName(int fundingType)
        {
            string typeName = fundingType switch
            {
                FundingTypes.Debit => Resources.Debit,
                FundingTypes.Credit => Resources.Credit,
                _ => "--",
            };
            return typeName;

        }

        public string GetStatusName(int status)
        {
            string statusName = status switch
            {
                PaymentInfoStatuses.Added => Resources.Added,
                PaymentInfoStatuses.ValidationPending => Resources.ValidationPending,
                PaymentInfoStatuses.PayEnabled => Resources.PayEnabled,
                PaymentInfoStatuses.OnHold => Resources.OnHold,
                PaymentInfoStatuses.PayDisabled => Resources.PayDisabled,
                PaymentInfoStatuses.Blocked => Resources.Blocked,
                _ => "--",
            };
            return statusName;

        }

        public List<PaymentInfo> Gets(string userId, int fundingType, int status)
        {
            List<PaymentInfo> paymentInfos = null;

            try
            {
                var query = (dynamic)null;

                if (fundingType != FundingTypes.All)
                {
                    if (status != PaymentInfoStatuses.All)
                    {
                        query = from x in this._businessObjects.Context.OltppaymentInfosView
                                where x.UserId == userId && x.Status == status && x.Funding == fundingType
                                select x;
                    }
                    else
                    {
                        query = from x in this._businessObjects.Context.OltppaymentInfosView
                                where x.UserId == userId && x.Funding == fundingType
                                select x;
                    }
                }
                else
                {
                    if (status != PaymentInfoStatuses.All)
                    {
                        query = from x in this._businessObjects.Context.OltppaymentInfosView
                                where x.UserId == userId && x.Status == status
                                select x;
                    }
                    else
                    {
                        query = from x in this._businessObjects.Context.OltppaymentInfosView
                                where x.UserId == userId
                                select x;
                    }
                }

                if (query != null)
                {
                    paymentInfos = new List<PaymentInfo>();
                    PaymentInfo paymentInfo;

                    foreach (OltppaymentInfosView item in query)
                    {
                        paymentInfo = new PaymentInfo
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            CardHolder = item.CardHolder,
                            CardLastDigits = item.CardLastDigits,
                            Funding = item.Funding,
                            FundingName = item.Funding != null ? GetFundingTypeName((int)item.Funding) : "",
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            Token = item.Token,
                            Brand = item.Brand,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            CountryFlag = item.CountryFlag,
                            CountryCurrencySymbol = item.CountryCurrencySymbol,
                            CountryCurrencyType = item.CountryCurrencyType,
                            Cvc_Check = item.CvcCheck,
                            Exp_Month = item.ExpMonth,
                            Exp_Year = item.ExpYear
                        };

                        paymentInfos.Add(paymentInfo);
                    }

                }
            }
            catch (Exception e)
            {
                paymentInfos = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentInfos;
        }


        public int Gets(int fundingType, int status, string brand)
        {
            int count = -1;

            try
            {

                if (fundingType != FundingTypes.All)
                {
                    if (status != PaymentInfoStatuses.All)
                    {
                        if (!string.IsNullOrEmpty(brand))
                        {
                            count = (from x in this._businessObjects.Context.OltppaymentInfosView
                                     where x.Status == status && x.Funding == fundingType && x.Brand == brand
                                     select x).Count();
                        }
                        else
                        {
                            count = (from x in this._businessObjects.Context.OltppaymentInfosView
                                     where x.Status == status && x.Funding == fundingType
                                     select x).Count();
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(brand))
                        {
                            count = (from x in this._businessObjects.Context.OltppaymentInfosView
                                     where x.Funding == fundingType && x.Brand == brand
                                     select x).Count();
                        }
                        else
                        {
                            count = (from x in this._businessObjects.Context.OltppaymentInfosView
                                     where x.Funding == fundingType
                                     select x).Count();
                        }
                    }
                }
                else
                {
                    if (status != PaymentInfoStatuses.All)
                    {
                        if (!string.IsNullOrEmpty(brand))
                        {
                            count = (from x in this._businessObjects.Context.OltppaymentInfosView
                                     where x.Status == status && x.Brand == brand
                                     select x).Count();
                        }
                        else
                        {
                            count = (from x in this._businessObjects.Context.OltppaymentInfosView
                                     where x.Status == status
                                     select x).Count();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(brand))
                        {
                            count = (from x in this._businessObjects.Context.OltppaymentInfosView
                                     where x.Brand == brand
                                     select x).Count();
                        }
                        else
                        {

                            count = (from x in this._businessObjects.Context.OltppaymentInfosView
                                     select x).Count();
                        }
                    }
                }

            }
            catch (Exception e)
            {
                count = -1;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return count;
        }

        public PaymentInfo Get(Guid id)
        {
            PaymentInfo paymentInfo = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltppaymentInfosView
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (OltppaymentInfosView item in query)
                    {
                        paymentInfo = new PaymentInfo
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            CardHolder = item.CardHolder,
                            CardLastDigits = item.CardLastDigits,
                            Funding = item.Funding,
                            FundingName = item.Funding != null ? GetFundingTypeName((int)item.Funding) : "",
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            Token = item.Token,
                            Brand = item.Brand,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            CountryFlag = item.CountryFlag,
                            CountryCurrencySymbol = item.CountryCurrencySymbol,
                            CountryCurrencyType = item.CountryCurrencyType,
                            Cvc_Check = item.CvcCheck,
                            Exp_Month = item.ExpMonth,
                            Exp_Year = item.ExpYear
                        };
                    }
                }
            }
            catch (Exception e)
            {
                paymentInfo = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentInfo;
        }

        public PaymentInfo Post(string userId, string cardHolder, string cardLastDigits, int? funding, int status, string token, string brand, Guid countryId, string cvc_check, string exp_year, string exp_month)
        {
            PaymentInfo paymentInfo = null;

            try
            {
                OltppaymentInfos newPaymentInfo = new OltppaymentInfos
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CardHolder = cardHolder,
                    CardLastDigits = cardLastDigits,
                    Funding = funding,
                    Status = status,
                    Token = token,
                    Brand = brand,
                    CountryId = countryId,
                    CvcCheck = cvc_check,
                    ExpYear = exp_year,
                    ExpMonth = exp_month,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltppaymentInfos.Add(newPaymentInfo);
                this._businessObjects.Context.SaveChanges();

                OltppaymentInfosView newPaymentInfoView = (from x in this._businessObjects.Context.OltppaymentInfosView
                                                           where x.Id == newPaymentInfo.Id
                                                           select x).FirstOrDefault();

                if(newPaymentInfoView != null)
                {
                    paymentInfo = new PaymentInfo
                    {
                        Id = newPaymentInfoView.Id,
                        UserId = newPaymentInfoView.UserId,
                        CardHolder = newPaymentInfoView.CardHolder,
                        CardLastDigits = newPaymentInfoView.CardLastDigits,
                        Funding = newPaymentInfoView.Funding,
                        FundingName = newPaymentInfoView.Funding != null ? GetFundingTypeName((int)newPaymentInfoView.Funding) : "",
                        Status = newPaymentInfoView.Status,
                        StatusName = GetStatusName(newPaymentInfoView.Status),
                        CreatedDate = newPaymentInfoView.CreatedDate,
                        UpdatedDate = newPaymentInfoView.UpdatedDate,
                        Token = newPaymentInfoView.Token,
                        Brand = newPaymentInfoView.Brand,
                        CountryId = newPaymentInfoView.CountryId,
                        CountryName = newPaymentInfoView.CountryName,
                        CountryFlag = newPaymentInfoView.CountryFlag,
                        CountryCurrencySymbol = newPaymentInfoView.CountryCurrencySymbol,
                        CountryCurrencyType = newPaymentInfoView.CountryCurrencyType,
                        Cvc_Check = newPaymentInfoView.CvcCheck,
                        Exp_Month = newPaymentInfoView.ExpMonth,
                        Exp_Year = newPaymentInfoView.ExpYear
                    };
                }
            }
            catch (Exception e)
            {
                paymentInfo = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentInfo;
        }

        public PaymentInfo Put(Guid id, string cardHolder, string cardLastDigits, int? funding, int status, string token, string brand, Guid countryId, string cvc_check, string exp_year, string exp_month)
        {
            PaymentInfo paymentInfo = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltppaymentInfos
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltppaymentInfos currentPaymentInfo = null;

                    foreach (OltppaymentInfos item in query)
                    {
                        currentPaymentInfo = item;
                    }

                    if (currentPaymentInfo != null)
                    {
                        currentPaymentInfo.CardHolder = cardHolder;
                        currentPaymentInfo.CardLastDigits = cardLastDigits;
                        currentPaymentInfo.Funding = funding;
                        currentPaymentInfo.Status = status;
                        currentPaymentInfo.Token = token;
                        currentPaymentInfo.Brand = brand;
                        currentPaymentInfo.CountryId = countryId;
                        currentPaymentInfo.CvcCheck = cvc_check;
                        currentPaymentInfo.ExpMonth = exp_month;
                        currentPaymentInfo.ExpYear = exp_year;
                        currentPaymentInfo.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        OltppaymentInfosView currentPaymentInfoView = (from x in this._businessObjects.Context.OltppaymentInfosView
                                                                   where x.Id == currentPaymentInfo.Id
                                                                   select x).FirstOrDefault();

                        if (currentPaymentInfoView != null)
                        {
                            paymentInfo = new PaymentInfo
                            {
                                Id = currentPaymentInfoView.Id,
                                UserId = currentPaymentInfoView.UserId,
                                CardHolder = currentPaymentInfoView.CardHolder,
                                CardLastDigits = currentPaymentInfoView.CardLastDigits,
                                Funding = currentPaymentInfoView.Funding,
                                FundingName = currentPaymentInfoView.Funding != null ? GetFundingTypeName((int)currentPaymentInfoView.Funding) : "",
                                Status = currentPaymentInfoView.Status,
                                StatusName = GetStatusName(currentPaymentInfoView.Status),
                                CreatedDate = currentPaymentInfoView.CreatedDate,
                                UpdatedDate = currentPaymentInfoView.UpdatedDate,
                                Token = currentPaymentInfoView.Token,
                                Brand = currentPaymentInfoView.Brand,
                                CountryId = currentPaymentInfoView.CountryId,
                                CountryName = currentPaymentInfoView.CountryName,
                                CountryFlag = currentPaymentInfoView.CountryFlag,
                                CountryCurrencySymbol = currentPaymentInfoView.CountryCurrencySymbol,
                                CountryCurrencyType = currentPaymentInfoView.CountryCurrencyType,
                                Cvc_Check = currentPaymentInfoView.CvcCheck,
                                Exp_Month = currentPaymentInfoView.ExpMonth,
                                Exp_Year = currentPaymentInfoView.ExpYear
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                paymentInfo = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentInfo;
        }

        public bool Put(Guid id, int status)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltppaymentInfos
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltppaymentInfos currentPaymentInfo = null;

                    foreach (OltppaymentInfos item in query)
                    {
                        currentPaymentInfo = item;
                    }

                    if (currentPaymentInfo != null)
                    {
                        currentPaymentInfo.Status = status;
                        currentPaymentInfo.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();
                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltppaymentInfos
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltppaymentInfos currentPaymentInfo = null;

                    foreach (OltppaymentInfos item in query)
                    {
                        currentPaymentInfo = item;
                    }

                    if (currentPaymentInfo != null)
                    {
                        this._businessObjects.Context.OltppaymentInfos.Remove(currentPaymentInfo);

                        this._businessObjects.Context.SaveChanges();
                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new PaymentInfoManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public PaymentInfoManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD FILE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
