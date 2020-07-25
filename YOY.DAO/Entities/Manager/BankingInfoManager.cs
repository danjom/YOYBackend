using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class BankingInfoManager
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

        private string GetCurrencyTypeName(int currencyType)
        {
            string typeName = currencyType switch
            {
                CurrencyTypes.CostaRicanColon => Resources.CostaRicaColons,
                CurrencyTypes.USDollar => Resources.USDollars,
                CurrencyTypes.GuatemalanQuetzal => Resources.GuatemalanQuetzal,
                CurrencyTypes.HonduranLempira => Resources.HonduranLempira,
                CurrencyTypes.NicaraguanCordoba => Resources.NicaraguanCordoba,
                CurrencyTypes.MexicanPeso => Resources.MexicanPeso,
                CurrencyTypes.ColombianPeso => Resources.ColombianPeso,
                _ => "--",
            };
            return typeName;

        }

        public string GetBankAccTypeName(int type)
        {
            string typeName = "";

            typeName = type switch
            {
                BankAccTypes.IndividualAcc => Resources.IndividualAcc,
                BankAccTypes.EnterpriseAcc => Resources.EnterpriseAcc,
                _ => "--",
            };
            return typeName;

        }

        public List<BankingInfo> Gets(int activeState, int mainAccState, Guid? countryId)
        {
            List<BankingInfo> bankingInfos = null;

            try
            {
                var query = (dynamic)null;

                if (countryId != null)
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:
                            switch (mainAccState)
                            {
                                case MainStates.All:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.CountryId == countryId
                                            select x;
                                    break;
                                case MainStates.Main:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId && x.CountryId == countryId
                                            select x;
                                    break;

                                case MainStates.Others:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where !x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId && x.CountryId == countryId
                                            select x;
                                    break;
                            }
                            break;
                        case ActiveStates.Active:
                            switch (mainAccState)
                            {
                                case MainStates.All:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where (bool)x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.CountryId == countryId
                                            select x;
                                    break;
                                case MainStates.Main:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where (bool)x.IsActive && x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId && x.CountryId == countryId
                                            select x;
                                    break;

                                case MainStates.Others:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where (bool)x.IsActive && !x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId && x.CountryId == countryId
                                            select x;
                                    break;
                            }
                            break;
                        case ActiveStates.Inactive:
                            switch (mainAccState)
                            {
                                case MainStates.All:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where !(bool)x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.CountryId == countryId
                                            select x;
                                    break;
                                case MainStates.Main:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where !(bool)x.IsActive && x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId && x.CountryId == countryId
                                            select x;
                                    break;

                                case MainStates.Others:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where !(bool)x.IsActive && !x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId && x.CountryId == countryId
                                            select x;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:
                            switch (mainAccState)
                            {
                                case MainStates.All:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where x.TenantId == this._businessObjects.Tenant.TenantId
                                            select x;
                                    break;
                                case MainStates.Main:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId
                                            select x;
                                    break;

                                case MainStates.Others:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where !x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId
                                            select x;
                                    break;
                            }
                            break;
                        case ActiveStates.Active:
                            switch (mainAccState)
                            {
                                case MainStates.All:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where (bool)x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                            select x;
                                    break;
                                case MainStates.Main:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where (bool)x.IsActive && x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId
                                            select x;
                                    break;

                                case MainStates.Others:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where (bool)x.IsActive && !x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId
                                            select x;
                                    break;
                            }
                            break;
                        case ActiveStates.Inactive:
                            switch (mainAccState)
                            {
                                case MainStates.All:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where !(bool)x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                            select x;
                                    break;
                                case MainStates.Main:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where !(bool)x.IsActive && x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId
                                            select x;
                                    break;

                                case MainStates.Others:
                                    query = from x in this._businessObjects.Context.DefbankingInfosView
                                            where !(bool)x.IsActive && !x.MainAcc && x.TenantId == this._businessObjects.Tenant.TenantId
                                            select x;
                                    break;
                            }
                            break;
                    }
                }

                if (query != null)
                {
                    bankingInfos = new List<BankingInfo>();
                    BankingInfo bankingInfo;

                    foreach (DefbankingInfosView item in query)
                    {
                        bankingInfo = new BankingInfo
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            OwnerId = item.OwnerId,
                            OwnerName = item.OwnerName,
                            BankName = item.BankName,
                            AccNum1 = item.AccNum1,
                            AccNum2 = item.AccNum2,
                            AccNum3 = item.AccNum3,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            Type = item.Type,
                            TypeName = GetBankAccTypeName(item.Type),
                            CurrencyType = item.CurrencyType,
                            CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType),
                            IsActive = (bool)item.IsActive,
                            MainAcc = item.MainAcc,
                            CreationDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        bankingInfos.Add(bankingInfo);
                    }
                }
            }
            catch (Exception e)
            {
                bankingInfos = null;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return bankingInfos;
        }

        public BankingInfo Get(Guid id)
        {
            BankingInfo bankingInfo = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefbankingInfosView
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (DefbankingInfosView item in query)
                    {
                        bankingInfo = new BankingInfo
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            OwnerId = item.OwnerId,
                            OwnerName = item.OwnerName,
                            BankName = item.BankName,
                            AccNum1 = item.AccNum1,
                            AccNum2 = item.AccNum2,
                            AccNum3 = item.AccNum3,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            Type = item.Type,
                            TypeName = GetBankAccTypeName(item.Type),
                            CurrencyType = item.CurrencyType,
                            CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType),
                            IsActive = item.IsActive,
                            MainAcc = item.MainAcc,
                            CreationDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }

            }
            catch (Exception e)
            {
                bankingInfo = null;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return bankingInfo;
        }

        public BankingInfo Post(Guid countryId, string ownerId, string ownerName, string bankName, string accNum1, string accNum2, string accNum3, int type, int currencyType, bool mainAcc)
        {
            BankingInfo bankingInfo;
            try
            {
                DefbankingInfos newBankingInfo = new DefbankingInfos
                {
                    Id = Guid.NewGuid(),
                    TenantId = this._businessObjects.Tenant.TenantId,
                    OwnerId = ownerId,
                    OwnerName = ownerName,
                    BankName = bankName,
                    AccNum1 = accNum1,
                    AccNum2 = accNum2,
                    AccNum3 = accNum3,
                    CountryId = countryId,
                    Type = type,
                    CurrencyType = currencyType,
                    MainAcc = mainAcc,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.DefbankingInfos.Add(newBankingInfo);
                this._businessObjects.Context.SaveChanges();

                DefbankingInfosView newInfo = (from x in this._businessObjects.Context.DefbankingInfosView
                                               where x.Id == newBankingInfo.Id
                                               select x).FirstOrDefault();

                if (newInfo != null)
                {
                    bankingInfo = new BankingInfo
                    {
                        Id = newInfo.Id,
                        TenantId = newInfo.TenantId,
                        OwnerId = newInfo.OwnerId,
                        OwnerName = newInfo.OwnerName,
                        BankName = newInfo.BankName,
                        AccNum1 = newInfo.AccNum1,
                        AccNum2 = newInfo.AccNum2,
                        AccNum3 = newInfo.AccNum3,
                        CountryId = newInfo.CountryId,
                        CountryName = newInfo.CountryName,
                        Type = newInfo.Type,
                        TypeName = GetBankAccTypeName(newInfo.Type),
                        CurrencyType = newInfo.CurrencyType,
                        CurrencyTypeName = GetCurrencyTypeName(newInfo.CurrencyType),
                        IsActive = newInfo.IsActive,
                        MainAcc = newInfo.MainAcc,
                        CreationDate = newInfo.CreatedDate,
                        UpdatedDate = newInfo.UpdatedDate
                    };
                }
                else
                {
                    bankingInfo = new BankingInfo();
                }
            }
            catch (Exception e)
            {
                bankingInfo = null;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return bankingInfo;
        }

        public BankingInfo Put(Guid id, string ownerId, string ownerName, string bankName, string accNum1, string accNum2, string accNum3, int type, int currencyType, bool mainAcc)
        {
            BankingInfo bankingInfo = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefbankingInfos
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    DefbankingInfos currentBankingInfo = null;

                    foreach (DefbankingInfos item in query)
                    {
                        currentBankingInfo = item;
                    }

                    if (currentBankingInfo != null)
                    {
                        currentBankingInfo.OwnerId = ownerId;
                        currentBankingInfo.OwnerName = ownerName;
                        currentBankingInfo.BankName = ownerName;
                        currentBankingInfo.AccNum1 = accNum1;
                        currentBankingInfo.AccNum2 = accNum2;
                        currentBankingInfo.AccNum3 = accNum3;
                        currentBankingInfo.Type = type;
                        currentBankingInfo.CurrencyType = currencyType;
                        currentBankingInfo.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        DefbankingInfosView currentInfo = (from x in this._businessObjects.Context.DefbankingInfosView
                                                           where x.Id == currentBankingInfo.Id
                                                           select x).FirstOrDefault();

                        if(currentInfo != null)
                        {

                            bankingInfo = new BankingInfo
                            {
                                Id = currentInfo.Id,
                                TenantId = currentInfo.TenantId,
                                OwnerId = currentInfo.OwnerId,
                                OwnerName = currentInfo.OwnerName,
                                BankName = currentInfo.BankName,
                                AccNum1 = currentInfo.AccNum1,
                                AccNum2 = currentInfo.AccNum2,
                                AccNum3 = currentInfo.AccNum3,
                                CountryId = currentInfo.CountryId,
                                CountryName = currentInfo.CountryName,
                                Type = currentInfo.Type,
                                TypeName = GetBankAccTypeName(currentInfo.Type),
                                CurrencyType = currentInfo.CurrencyType,
                                CurrencyTypeName = GetCurrencyTypeName(currentInfo.CurrencyType),
                                IsActive = currentInfo.IsActive,
                                MainAcc = currentInfo.MainAcc,
                                CreationDate = currentInfo.CreatedDate,
                                UpdatedDate = currentInfo.UpdatedDate
                            };
                        }
                        else
                        {
                            bankingInfo = new BankingInfo();
                        }

                    }
                }
            }
            catch (Exception e)
            {
                bankingInfo = null;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return bankingInfo;
        }

        public bool Put(Guid id, int changeType)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefbankingInfos
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefbankingInfos currentBankingInfo = null;

                    foreach (DefbankingInfos item in query)
                    {
                        currentBankingInfo = item;
                    }

                    if (currentBankingInfo != null)
                    {
                        switch (changeType)
                        {
                            case ChangeTypes.ActiveState:
                                currentBankingInfo.IsActive = !currentBankingInfo.IsActive;
                                currentBankingInfo.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();

                                success = true;
                                break;
                            case ChangeTypes.MainStatus:
                                currentBankingInfo.MainAcc = !currentBankingInfo.MainAcc;
                                currentBankingInfo.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();

                                success = true;
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                success = false;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }

        public bool Delete(Guid id, int changeType)
        {
            bool success = false;

            try
            {
                DefbankingInfos currentBankingInfo = (from x in this._businessObjects.Context.DefbankingInfos
                                                        where x.Id == id
                                                        select x).FirstOrDefault();

                if (currentBankingInfo != null)
                {
                    currentBankingInfo.Deleted = true;
                    currentBankingInfo.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;

                //ERROR MANAGEMENT 
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
        /// Creates a new BankingInfoManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public BankingInfoManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD BANK INFO MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
