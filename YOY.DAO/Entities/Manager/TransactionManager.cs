using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.ClaimRecord;
using YOY.DTO.Entities.Misc.Transaction;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class TransactionManager
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

        private static string GenerateRandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS METHODS                                                                                                                                  //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        #region TRANSACTIONS

        private string GetDealTypeName(int dealType)
        {
            string typeName = "";

            switch (dealType)
            {
                case DealTypes.InStore:
                    typeName = Resources.Instore;
                    break;
                case DealTypes.Online:
                    typeName = Resources.Online;
                    break;
                case DealTypes.Phone:
                    typeName = Resources.PhoneCall;
                    break;
            }

            return typeName;
        }

        /// <summary>
        /// Retrieves all the tenant's transactions
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Transaction> Gets(int type, int pageSize, int pageNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                var query = (from x in this._businessObjects.Context.OltptransactionsView
                             where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == type
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                Transaction transaction = null;
                foreach (OltptransactionsView item in query)
                {
                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };

                    transactions.Add(transaction);
                }
            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves all the user's transactions for a given reference(reward, offer, coupon, ...)
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Transaction> Gets(Guid referenceId, string userId, int pageSize, int pageNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                var query = (from x in this._businessObjects.Context.OltptransactionsView
                             where x.TenantId == this._businessObjects.Tenant.TenantId && x.UserId == userId && x.ReferenceId == referenceId
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                Transaction transaction = null;
                foreach (OltptransactionsView item in query)
                {
                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };

                    transactions.Add(transaction);
                }
            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves all tenant's transactions between a date interval
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Transaction> Gets(DateTime fromDate, DateTime toDate, int type, int pageSize, int pageNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                var query = (from x in this._businessObjects.Context.OltptransactionsView
                             where x.TenantId == this._businessObjects.Tenant.TenantId && x.CreatedDate >= fromDate && x.CreatedDate <= toDate && x.TransactionType == type
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                Transaction transaction = null;
                foreach (OltptransactionsView item in query)
                {

                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };

                    transactions.Add(transaction);
                }
            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Retrieves all the user's transactions 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="transactionType"></param>
        /// <param name="completeType"></param>
        /// <param name="expiredState"></param>
        /// <returns></returns>
        public List<Transaction> Gets(Guid tenantId, int transactionType, int completeState, int expiredState, DateTime date, DateTime minCreatedDate, int pageSize, int pageNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {

                var query = (dynamic)null;

                switch (completeState)
                {
                    case CompleteStates.All:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.TransactionType == transactionType && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ExpiredStates.Expired:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.TransactionType == transactionType && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Valid:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.TransactionType == transactionType && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }

                        break;
                    case CompleteStates.Complete:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.TransactionType == transactionType && x.Completed && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.Completed && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.TransactionType == transactionType && x.Completed && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.Completed && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ExpiredStates.Valid:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.TransactionType == transactionType && x.Completed && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.Completed && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }

                        break;
                    case CompleteStates.Incomplete:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.TransactionType == transactionType && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.TransactionType == transactionType && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Valid:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.TransactionType == transactionType && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }

                        break;
                }


                Transaction transaction = null;
                foreach (OltptransactionsView item in query)
                {

                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };

                    transactions.Add(transaction);
                }
            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves all the user's transactions 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="transactionType"></param>
        /// <param name="completeType"></param>
        /// <param name="expiredState"></param>
        /// <returns></returns>
        public List<Transaction> Gets(string userId, int transactionType, int completeType, int expiredState, DateTime date, DateTime minCreatedDate, int pageSize, int pageNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {

                var query = (dynamic)null;

                switch (completeType)
                {
                    case CompleteStates.All:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.TransactionType == transactionType && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ExpiredStates.Expired:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.TransactionType == transactionType && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Valid:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.TransactionType == transactionType && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }

                        break;
                    case CompleteStates.Complete:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.TransactionType == transactionType && x.Completed && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.Completed && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.TransactionType == transactionType && x.Completed && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.Completed && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }

                                break;
                            case ExpiredStates.Valid:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.TransactionType == transactionType && x.Completed && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.Completed && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }

                        break;
                    case CompleteStates.Incomplete:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.TransactionType == transactionType && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.TransactionType == transactionType && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate && (x.ExpirationDate != null && x.ExpirationDate < date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Valid:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && x.TransactionType == transactionType && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.UserId == userId && (!x.Completed || !x.OneTimeClaim) && x.CreatedDate > minCreatedDate && (x.ExpirationDate == null || x.ExpirationDate >= date)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }

                        break;
                }


                Transaction transaction = null;
                foreach (OltptransactionsView item in query)
                {

                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };

                    transactions.Add(transaction);
                }
            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves all the tenant's transactions by user
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="transactionType"></param>
        /// <param name="completeType"></param>
        /// <param name="expiredState"></param>
        /// <returns></returns>
        public List<Transaction> Gets(Guid tenantId, string userId, int transactionType, int completeType, int expiredState, DateTime date, int pageSize, int pageNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {

                var query = (dynamic)null;

                switch (completeType)
                {
                    case CompleteStates.All:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.TransactionType == transactionType
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Valid:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.TransactionType == transactionType
                                                     && x.ExpirationDate > date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId
                                             && x.ExpirationDate > date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.TransactionType == transactionType
                                                     && x.ExpirationDate <= date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId
                                             && x.ExpirationDate <= date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }


                        break;
                    case CompleteStates.Complete:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.TransactionType == transactionType && x.Completed
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.Completed
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Valid:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.TransactionType == transactionType
                                                 && x.Completed && x.ExpirationDate > date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.Completed
                                                 && x.ExpirationDate > date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.TransactionType == transactionType
                                                 && x.Completed && x.ExpirationDate <= date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.Completed
                                                 && x.ExpirationDate <= date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }

                        break;
                    case CompleteStates.Incomplete:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.TransactionType == transactionType && (!x.Completed || !x.OneTimeClaim)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && (!x.Completed || !x.OneTimeClaim)
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Valid:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.TransactionType == transactionType
                                                 && (!x.Completed || !x.OneTimeClaim) && x.ExpirationDate > date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && (!x.Completed || !x.OneTimeClaim)
                                                 && x.ExpirationDate > date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                            case ExpiredStates.Expired:
                                if (transactionType != TransactionTypes.All)
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && x.TransactionType == transactionType
                                                 && (!x.Completed || !x.OneTimeClaim) && x.ExpirationDate <= date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.OltptransactionsView
                                             where x.TenantId == tenantId && x.UserId == userId && (!x.Completed || !x.OneTimeClaim)
                                                 && x.ExpirationDate <= date
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                break;
                        }

                        break;
                }


                Transaction transaction = null;
                foreach (OltptransactionsView item in query)
                {

                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };

                    transaction.CreatedDateLiteral = transaction.CreatedDate.ToShortDateString();

                    transactions.Add(transaction);
                }
            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves all the tenant's membership transactions
        /// </summary>
        /// <param name="transactionType"></param>
        /// <param name="completeType"></param>
        /// <param name="expirationState"></param>
        /// <returns></returns>
        public List<Transaction> Gets(int transactionType, int completeType, int expirationState, int pageSize, int pageNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {

                var query = (dynamic)null;

                switch (completeType)
                {
                    case CompleteStates.All:
                        switch (expirationState)
                        {
                            case ExpiredStates.All:
                                query = (from x in this._businessObjects.Context.OltptransactionsView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == transactionType
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ExpiredStates.Valid:
                                query = (from x in this._businessObjects.Context.OltptransactionsView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == transactionType && x.ExpirationDate > DateTime.UtcNow
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ExpiredStates.Expired:
                                query = (from x in this._businessObjects.Context.OltptransactionsView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == transactionType && x.ExpirationDate <= DateTime.UtcNow
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }

                        break;
                    case CompleteStates.Complete:
                        switch (expirationState)
                        {
                            case ExpiredStates.All:
                                query = (from x in this._businessObjects.Context.OltptransactionsView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == transactionType && x.Completed
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ExpiredStates.Valid:
                                query = (from x in this._businessObjects.Context.OltptransactionsView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == transactionType && x.Completed && x.ExpirationDate > DateTime.UtcNow
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ExpiredStates.Expired:
                                query = (from x in this._businessObjects.Context.OltptransactionsView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == transactionType && x.Completed && x.ExpirationDate <= DateTime.UtcNow
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }

                        break;
                    case CompleteStates.Incomplete:
                        switch (expirationState)
                        {
                            case ExpiredStates.All:
                                query = (from x in this._businessObjects.Context.OltptransactionsView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == transactionType && (!x.Completed || !x.OneTimeClaim)
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ExpiredStates.Valid:
                                query = (from x in this._businessObjects.Context.OltptransactionsView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == transactionType && !x.Completed && x.ExpirationDate > DateTime.UtcNow
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ExpiredStates.Expired:
                                query = (from x in this._businessObjects.Context.OltptransactionsView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.TransactionType == transactionType && !x.Completed && x.ExpirationDate <= DateTime.UtcNow
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }
                        break;
                }

                AspNetUsers currentUser;
                Transaction transaction = null;
                foreach (OltptransactionsView item in query)
                {

                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };


                    currentUser = (from x in this._businessObjects.Context.AspNetUsers
                                   where x.Id == transaction.UserId
                                   select x).Single();
                    transaction.UserName = currentUser.Name;

                    if (item.CreatorId != null)
                    {
                        OltpemployeesView employeeCreator = (from x in this._businessObjects.Context.OltpemployeesView
                                                               where x.Id == (Guid)item.CreatorId
                                                               select x).FirstOrDefault();

                        if(employeeCreator != null)
                            transaction.CreatorName = employeeCreator.Name;
                    }
                    transaction.CreatedDateLiteral = transaction.CreatedDate.ToShortDateString();

                    transactions.Add(transaction);
                }
            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves all tenant's transactions between a date interval
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="completeState"></param>
        /// <returns></returns>
        public List<Transaction> Gets(DateTime fromDate, DateTime toDate, string userId, int type, int completeState, int pageSize, int pageNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                var query = (dynamic)null;
                switch (completeState)
                {
                    case CompleteStates.All:
                        query = (from x in this._businessObjects.Context.OltptransactionsView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.UserId == userId && x.CreatedDate >= fromDate && x.CreatedDate <= toDate && x.TransactionType == type
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case CompleteStates.Complete:
                        query = (from x in this._businessObjects.Context.OltptransactionsView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.UserId == userId && x.CreatedDate >= fromDate && x.CreatedDate <= toDate && x.TransactionType == type && x.Completed
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case CompleteStates.Incomplete:
                        query = (from x in this._businessObjects.Context.OltptransactionsView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.UserId == userId && x.CreatedDate >= fromDate && x.CreatedDate <= toDate && x.TransactionType == type && (!x.Completed || !x.OneTimeClaim)
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;

                }

                Transaction transaction = null;
                foreach (OltptransactionsView item in query)
                {

                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };

                    transactions.Add(transaction);
                }
            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        public double? Get(Guid offerId, Guid tenantId)
        {
            double? score = null;

            try
            {
                score = (from x in this._businessObjects.Context.Oltptransactions
                         where x.TenantId == tenantId && x.ReferenceId == offerId && x.Score != null
                         select x.Score).Average();
            }

            catch (Exception e)
            {
                score = 0;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return score;
        }

        /// <summary>
        /// Retrieves the 1st transaction based in chronologica order for a given reference(reward, offer, coupon, ...)
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Transaction Get(Guid referenceId, Guid tenantId, string userId, int chronogicalType)
        {
            Transaction transaction = null;

            try
            {
                OltptransactionsView item = null;

                switch (chronogicalType)
                {
                    case ChronologicalOrders.Descending:

                        item = (from x in this._businessObjects.Context.OltptransactionsView
                                where x.TenantId == tenantId && x.UserId == userId && x.ReferenceId == referenceId
                                orderby x.CreatedDate descending
                                select x).FirstOrDefault();

                        break;
                    case ChronologicalOrders.Ascending:

                        item = (from x in this._businessObjects.Context.OltptransactionsView
                                where x.TenantId == tenantId && x.UserId == userId && x.ReferenceId == referenceId
                                orderby x.CreatedDate ascending
                                select x).FirstOrDefault();

                        break;
                }

                if (item != null)
                {
                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated,
                    };
                }

            }
            catch (Exception e)
            {
                transaction = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transaction;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves a transaction
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public Transaction Get(Guid transactionId, bool filterByTenant)
        {
            Transaction transaction = null;

            try
            {
                var query = (dynamic)null;

                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.OltptransactionsView
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == transactionId
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltptransactionsView
                            where x.Id == transactionId
                            select x;
                }

                foreach (OltptransactionsView item in query)
                {
                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };
                }
            }
            catch (Exception e)
            {
                transaction = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transaction;
        }

        /// <summary>
        /// Returns a valid to claim deal transaction corresponding to the code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="expiredState"></param>
        /// <param name="dateTime"></param>
        /// <param name="chronologicalOrder"></param>
        /// <param name="filterByTenant"></param>
        /// <returns></returns>
        public Transaction Get(string code, Guid tenantId, int expiredState, DateTime dateTime, int chronologicalOrder, bool filterByTenant)
        {
            Transaction transaction = null;

            try
            {
                OltptransactionsView item = (dynamic)null;

                if (filterByTenant)
                {
                    switch (expiredState)
                    {
                        case ExpiredStates.All:
                            switch (chronologicalOrder)
                            {
                                case ChronologicalOrders.Descending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate descending
                                            select x).FirstOrDefault();
                                    break;
                                case ChronologicalOrders.Ascending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate ascending
                                            select x).FirstOrDefault();
                                    break;
                            }

                            break;
                        case ExpiredStates.Valid:
                            switch (chronologicalOrder)
                            {
                                case ChronologicalOrders.Descending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && x.ExpirationDate > dateTime && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate descending
                                            select x).FirstOrDefault();
                                    break;
                                case ChronologicalOrders.Ascending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && x.ExpirationDate > dateTime && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate ascending
                                            select x).FirstOrDefault();
                                    break;
                            }

                            break;
                        case ExpiredStates.Expired:
                            switch (chronologicalOrder)
                            {
                                case ChronologicalOrders.Descending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && x.ExpirationDate <= dateTime && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate descending
                                            select x).FirstOrDefault();
                                    break;
                                case ChronologicalOrders.Ascending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && x.ExpirationDate <= dateTime && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate ascending
                                            select x).FirstOrDefault();
                                    break;
                            }

                            break;
                    }

                }
                else
                {
                    switch (expiredState)
                    {
                        case ExpiredStates.All:
                            switch (chronologicalOrder)
                            {
                                case ChronologicalOrders.Descending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate descending
                                            select x).FirstOrDefault();
                                    break;
                                case ChronologicalOrders.Ascending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate ascending
                                            select x).FirstOrDefault();
                                    break;
                            }

                            break;
                        case ExpiredStates.Valid:
                            switch (chronologicalOrder)
                            {
                                case ChronologicalOrders.Descending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && x.ExpirationDate > dateTime && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate descending
                                            select x).FirstOrDefault();
                                    break;
                                case ChronologicalOrders.Ascending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && x.ExpirationDate > dateTime && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate ascending
                                            select x).FirstOrDefault();
                                    break;
                            }

                            break;
                        case ExpiredStates.Expired:
                            switch (chronologicalOrder)
                            {
                                case ChronologicalOrders.Descending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && x.ExpirationDate <= dateTime && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate descending
                                            select x).FirstOrDefault();
                                    break;
                                case ChronologicalOrders.Ascending:
                                    item = (from x in this._businessObjects.Context.OltptransactionsView
                                            where x.TenantId == tenantId && x.Code == code && x.ExpirationDate <= dateTime && (!x.OneTimeClaim || x.TransactionClaimCount == 0)
                                            orderby x.CreatedDate ascending
                                            select x).FirstOrDefault();
                                    break;
                            }

                            break;
                    }
                }

                if (item != null)
                {
                    transaction = new Transaction
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        TransactionType = item.TransactionType,
                        Completed = item.Completed,
                        OriginId = item.OriginId,
                        Code = item.Code,
                        CodeImg = item.CodeImg,
                        ReferenceType = item.ReferenceType,
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        ClaimPoints = item.ClaimPoints,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        ReleaseDate = item.ReleaseDate,
                        CompletedDate = item.CompletedDate,
                        ExpirationDate = item.ExpirationDate,
                        Creator = item.CreatorId,
                        ReferenceId = item.ReferenceId,
                        TransactionClaimCount = item.TransactionClaimCount,
                        TotalClaimCount = item.TotalClaimCount != null ? (int)item.TotalClaimCount : 0,
                        OneTimeClaim = item.OneTimeClaim,
                        ShowToUser = item.ShowToUser,
                        Score = item.Score,
                        Comment = item.Comment,
                        CategoryId = item.CategoryId,
                        PointsEarnStatus = item.PointsEarnStatus,
                        GeneratedPoints = item.GeneratedPoints,
                        Validated = item.Validated
                    };
                }
            }
            catch (Exception e)
            {
                transaction = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transaction;
        }


        /// <summary>
        /// Creates a new transaction for deals redemption
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tenantId"></param>
        /// <param name="transactionType"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="claimPoints"></param>
        /// <param name="value"></param>
        /// <param name="regularValue"></param>
        /// <param name="creatorId"></param>
        /// <param name="origin"></param>
        /// <param name="completed"></param>
        /// <param name="referenceType"></param>
        /// <param name="code"></param>
        /// <param name="codeImg"></param>
        /// <param name="redemptionDate"></param>
        /// <param name="referenceId"></param>
        /// <param name="oneTimeClaim"></param>
        /// <param name="showToUser"></param>
        /// <param name="minsToClaim"></param>
        /// <param name="minsToUnlock"></param>
        /// <param name="dealType"></param>
        /// <param name="categoryId"></param>
        /// <param name="pointsEarnStatus"></param>
        /// <param name="generatedPoints"></param>
        /// <param name="validated"></param>
        /// <returns></returns>
        public Transaction Post(string userId, Guid tenantId, int transactionType, string name, string description,
            int claimPoints, decimal value, decimal? regularValue, Guid? creatorId, int origin, bool completed, int? referenceType,
            string code, Guid? codeImg, DateTime redemptionDate, Guid? referenceId, bool oneTimeClaim, bool showToUser, int minsToClaim, 
            int minsToUnlock, int dealType, Guid? categoryId, int pointsEarnStatus, int generatedPoints, bool validated)
        {
            Oltptransactions transaction = null;

            Transaction newTransaction;
            try
            {

                transaction = new Oltptransactions
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    UserId = userId,
                    TransactionType = transactionType,
                    Code = code,
                    CodeImg = codeImg,
                    Name = name,
                    Description = description,
                    ClaimPoints = claimPoints,
                    CreatorId = creatorId,
                    Value = value,
                    RegularValue = regularValue,
                    CreatedDate = redemptionDate,
                    UpdatedDate = DateTime.UtcNow,
                    ReleaseDate = redemptionDate,
                    Completed = completed,
                    OriginId = origin,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    ClaimCount = 0,//At creation has no claims
                    OneTimeClaim = oneTimeClaim,
                    ShowToUser = showToUser,
                    Score = null,
                    Comment = null,
                    DealType = dealType,
                    CategoryId = categoryId,
                    PointsEarnStatus = pointsEarnStatus,
                    GeneratedPoints = generatedPoints,
                    Validated = validated
                };

                //If needs to register a claim
                if ((transactionType == TransactionTypes.RedeemDeal || transactionType == TransactionTypes.ClaimDeal) && completed)
                {
                    transaction.ClaimCount = 1;//If completed means that 
                }


                //If the transaction is an offer redemption
                if (referenceType != null)
                {
                    transaction.ExpirationDate = transaction.CreatedDate.AddMinutes(minsToUnlock + minsToClaim);
                    transaction.ReleaseDate = transaction.CreatedDate.AddMinutes(minsToUnlock);
                }

                this._businessObjects.Context.Oltptransactions.Add(transaction);

                this._businessObjects.Context.SaveChanges();

                newTransaction = new Transaction
                {
                    Id = transaction.Id,
                    UserId = transaction.UserId,
                    TenantId = transaction.TenantId,
                    TransactionType = transaction.TransactionType,
                    Completed = (bool)transaction.Completed,
                    OriginId = transaction.OriginId,
                    Code = transaction.Code,
                    CodeImg = transaction.CodeImg,
                    ReferenceType = transaction.ReferenceType,
                    DealType = transaction.DealType,
                    DealTypeName = GetDealTypeName(transaction.DealType),
                    ClaimPoints = transaction.ClaimPoints,
                    Value = transaction.Value,
                    RegularValue = transaction.RegularValue,
                    Name = transaction.Name,
                    Description = transaction.Description,
                    CreatedDate = transaction.CreatedDate,
                    UpdatedDate = transaction.UpdatedDate,
                    ReleaseDate = transaction.ReleaseDate,
                    CompletedDate = transaction.CompletedDate,
                    ExpirationDate = transaction.ExpirationDate,
                    Creator = transaction.CreatorId,
                    ReferenceId = transaction.ReferenceId,
                    TransactionClaimCount = transaction.ClaimCount,
                    TotalClaimCount = -1,//Not used
                    OneTimeClaim = transaction.OneTimeClaim,
                    ShowToUser = (bool)transaction.ShowToUser,
                    Score = transaction.Score,
                    Comment = transaction.Comment,
                    CategoryId = transaction.CategoryId,
                    PointsEarnStatus = transaction.PointsEarnStatus,
                    GeneratedPoints = transaction.GeneratedPoints,
                    Validated = (bool)transaction.Validated
                };


            }
            catch (Exception e)
            {
                this._businessObjects.Context.Oltptransactions.Remove(transaction);
                this._businessObjects.Context.SaveChanges();

                newTransaction = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return newTransaction;
        }//POST ENDS ------------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Creates a transaction, it can be for multiple purposes
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="transactionType"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="claimPoints"></param>
        /// <param name="value"></param>
        /// <param name="regularValue"></param>
        /// <param name="creatorId"></param>
        /// <param name="origin"></param>
        /// <param name="completed"></param>
        /// <param name="referenceType"></param>
        /// <param name="code"></param>
        /// <param name="codeImg"></param>
        /// <param name="redemptionDate"></param>
        /// <param name="referenceId"></param>
        /// <param name="oneTimeClaim"></param>
        /// <param name="showToUser"></param>
        /// <param name="minsToClaim"></param>
        /// <param name="minsToUnlock"></param>
        /// <param name="dealType"></param>
        /// <param name="categoryId"></param>
        /// <param name="pointsEarnStatus"></param>
        /// <param name="generatedPoints"></param>
        /// <param name="validated"></param>
        /// <returns></returns>
        public Transaction Post(Guid tenantId, string userId, int transactionType, string name, string description,
            int claimPoints, decimal value, decimal? regularValue, Guid? creatorId, int origin, bool completed, int? referenceType,
            string code, Guid? codeImg, DateTime redemptionDate, Guid? referenceId, bool oneTimeClaim, bool showToUser, int minsToClaim, 
            int minsToUnlock, int dealType, Guid? categoryId, int pointsEarnStatus, int generatedPoints, bool validated)
        {
            Oltptransactions transaction = null;
            Transaction newTransaction;

            try
            {

                transaction = new Oltptransactions
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    UserId = userId,
                    TransactionType = transactionType,
                    Code = code,
                    CodeImg = codeImg,
                    Name = name,
                    Description = description,
                    ClaimPoints = claimPoints,
                    CreatorId = creatorId,
                    Value = value,
                    RegularValue = regularValue,
                    CreatedDate = redemptionDate,
                    UpdatedDate = DateTime.UtcNow,
                    ReleaseDate = redemptionDate,
                    Completed = completed,
                    OriginId = origin,
                    ReferenceId = referenceId,
                    ClaimCount = 0,//On the beginnig it hasn't been claimed
                    OneTimeClaim = oneTimeClaim,
                    ShowToUser = showToUser,
                    ReferenceType = referenceType,
                    Score = null,
                    Comment = null,
                    DealType = dealType,
                    CategoryId = categoryId,
                    PointsEarnStatus = pointsEarnStatus,
                    GeneratedPoints = generatedPoints,
                    Validated = validated
                };


                //If the transaction is an offer redemption
                if (referenceType != null)
                {
                    transaction.ExpirationDate = transaction.CreatedDate.AddMinutes(minsToUnlock + minsToClaim);
                    transaction.ReleaseDate = transaction.CreatedDate.AddMinutes(minsToUnlock);
                }

                this._businessObjects.Context.Oltptransactions.Add(transaction);
                this._businessObjects.Context.SaveChanges();

                newTransaction = new Transaction
                {
                    Id = transaction.Id,
                    UserId = transaction.UserId,
                    TenantId = transaction.TenantId,
                    TransactionType = transaction.TransactionType,
                    Completed = (bool)transaction.Completed,
                    OriginId = transaction.OriginId,
                    Code = transaction.Code,
                    CodeImg = transaction.CodeImg,
                    ReferenceType = transaction.ReferenceType,
                    DealType = transaction.DealType,
                    DealTypeName = GetDealTypeName(transaction.DealType),
                    ClaimPoints = transaction.ClaimPoints,
                    Value = transaction.Value,
                    RegularValue = transaction.RegularValue,
                    Name = transaction.Name,
                    Description = transaction.Description,
                    CreatedDate = transaction.CreatedDate,
                    UpdatedDate = transaction.UpdatedDate,
                    ReleaseDate = transaction.ReleaseDate,
                    CompletedDate = transaction.CompletedDate,
                    ExpirationDate = transaction.ExpirationDate,
                    Creator = transaction.CreatorId,
                    ReferenceId = transaction.ReferenceId,
                    TransactionClaimCount = transaction.ClaimCount,
                    TotalClaimCount = -1,//Not used
                    OneTimeClaim = transaction.OneTimeClaim,
                    ShowToUser = (bool)transaction.ShowToUser,
                    Score = transaction.Score,
                    Comment = transaction.Comment,
                    CategoryId = transaction.CategoryId,
                    PointsEarnStatus = transaction.PointsEarnStatus,
                    GeneratedPoints = transaction.GeneratedPoints,
                    Validated = (bool)transaction.Validated
                };


            }
            catch (Exception e)
            {
                this._businessObjects.Context.Oltptransactions.Remove(transaction);
                this._businessObjects.Context.SaveChanges();

                newTransaction = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return newTransaction;
        }//POST ENDS ------------------------------------------------------------------------------------------------------------------------------------ //

        /*
        /// <summary>
        /// Change transaction state to completed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="branchClaimId"></param>
        /// <param name="claimRequests"></param>
        /// <param name="completedDate"></param>
        /// <param name="score"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public TransactionClaimValues Put(Guid id, int deliveryType, int claimRequests, DateTime completedDate, double? score, string comment, int pointsEarnStatus, int generatedPoints)
        {
            TransactionClaimValues claimValues = null;

            try
            {

                Oltptransactions transaction = null;

                //Needs to retrieve the transaction to update
                var query = from x in this._businessObjects.Context.Oltptransactions
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (var item in query)
                {
                    transaction = item;
                }

                //if the transaction exists
                if (transaction != null)
                {

                    //If the transaction accepts claims
                    if (!(bool)transaction.Completed || !transaction.OneTimeClaim)
                    {
                        //Now needs to check the usage record
                        //Now needs to fill the usage record
                        OltpclaimRecords matchClaimRecord = (from x in this._businessObjects.Context.OltpclaimRecords
                                                            where x.UserId == transaction.UserId && x.ReferenceId == transaction.ReferenceId
                                                            select x).FirstOrDefault();

                        if ((transaction.MaxClaimsPerUser == -1) || ((matchClaimRecord == null || matchClaimRecord.ReferenceId == Guid.Empty) && transaction.MaxClaimsPerUser >= claimRequests) || (transaction.MaxClaimsPerUser >= (matchClaimRecord.Count + claimRequests)))
                        {

                            //If the last redemption was too early ago
                            if (matchClaimRecord != null && (matchClaimRecord.Count > GenericConfigValues.FrequentClaimAcceptance && (DateTime.UtcNow - matchClaimRecord.LastUsage).Minutes < GenericConfigValues.FrequentClaimMinsLaps))
                            {
                                generatedPoints = (int)Math.Floor(generatedPoints * GenericConfigValues.FrequentClaimPenalizer);
                            }

                            transaction.Completed = true;
                            transaction.BranchClaimId = branchClaimId;
                            transaction.CompletedDate = completedDate;
                            transaction.ClaimCount += claimRequests;
                            transaction.Score = score;
                            transaction.Comment = comment;
                            transaction.PointsEarnStatus = pointsEarnStatus;
                            transaction.GeneratedPoints = generatedPoints;
                            transaction.UpdatedDate = DateTime.UtcNow;

                            if (matchClaimRecord != null && matchClaimRecord.ReferenceId != Guid.Empty)
                            {
                                matchClaimRecord.Count += claimRequests;
                                matchClaimRecord.PreviousUsage = matchClaimRecord.LastUsage;
                                matchClaimRecord.LastUsage = DateTime.UtcNow;
                            }
                            else
                            {
                                //Needs to create a new record
                                matchClaimRecord = new OltpclaimRecords
                                {
                                    Id = Guid.NewGuid(),
                                    UserId = transaction.UserId,
                                    ReferenceId = (Guid)transaction.ReferenceId,
                                    ReferenceType = (int)transaction.ReferenceType,
                                    Count = claimRequests,
                                    TenantId = transaction.TenantId,
                                    CreatedDate = DateTime.UtcNow,
                                    LastUsage = DateTime.UtcNow,
                                    PreviousUsage = null
                                };

                                this._businessObjects.Context.OltpclaimRecords.Add(matchClaimRecord);
                            }

                            //Will determine the claim reference code
                            string claimRefCode = "";


                            //Creates the record line
                            OltpclaimRecordLines recordLine = new OltpclaimRecordLines
                            {
                                Id = Guid.NewGuid(),
                                RecordId = matchClaimRecord.Id,
                                RecordNumber = claimRequests,
                                TransactionId = transaction.Id,
                                TenantId = transaction.TenantId,
                                BranchId = null, //transaction.BranchClaimId,
                                ClaimRefCode = claimRefCode,
                                DeliveryType = deliveryType,
                                Validated = false,
                                ReceiptId = null,
                                ValidationDate = null,
                                CreatedDate = DateTime.UtcNow,
                                UpdatedDate = DateTime.UtcNow
                            };

                            this._businessObjects.Context.OltpclaimRecordLines.Add(recordLine);

                            this._businessObjects.Context.SaveChanges();

                            claimValues = new TransactionClaimValues
                            {
                                TransactionId = transaction.Id,
                                UsageRecordLineId = recordLine.Id,
                                ClaimRefCode = recordLine.ClaimRefCode
                            };

                            //If it's a reward it's a special case where what matter it's the current transaction claim count
                            if (transaction.ReferenceType == TransactionReferenceTypes.Reward)
                            {
                                claimValues.ClaimCount = transaction.ClaimCount;
                            }
                            else
                            {
                                claimValues.ClaimCount = matchClaimRecord.Count;
                            }

                        }
                        else
                        {
                            claimValues = new TransactionClaimValues
                            {
                                TransactionId = id,
                                ClaimCount = -1,
                                UsageRecordLineId = Guid.Empty,
                                ClaimRefCode = "-"
                            };//This means that the max claims has been already reached out
                        }

                    }
                }
            }
            catch (Exception e)
            {
                claimValues = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return claimValues;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //
        */

        /// <summary>
        /// Sets the satisfaction grade and comment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="satisfactionGrade"></param>
        /// <returns></returns>
        public bool Put(Guid id, double? score, string comment)
        {
            bool success = false;

            try
            {
                Oltptransactions transaction = null;

                //Needs to retrieve the transaction to update
                var query = from x in this._businessObjects.Context.Oltptransactions
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (var item in query)
                {
                    transaction = item;
                }

                //if the transaction exists
                if (transaction != null)
                {
                    transaction.Score = score;
                    transaction.Comment = comment;
                    transaction.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Updates the validated state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Put(Guid id, bool validated, int pointsEarnStatus, int generatedPoints)
        {
            bool success = false;

            try
            {
                Oltptransactions transaction = null;

                //Needs to retrieve the transaction to update
                var query = from x in this._businessObjects.Context.Oltptransactions
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (var item in query)
                {
                    transaction = item;
                }

                //if the transaction exists
                if (transaction != null)
                {
                    transaction.Validated = validated;
                    transaction.PointsEarnStatus = pointsEarnStatus;
                    transaction.GeneratedPoints = generatedPoints;
                    transaction.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        #endregion

        #region CLAIMRECORDS

        public ClaimRecordLineValidationData Get(bool filterByTenant, Guid id)
        {
            ClaimRecordLineValidationData claimRecordLine = null;

            try
            {
                var query = (dynamic)null;

                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.OltpclaimRecordLinesView
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpclaimRecordLinesView
                            where x.Id == id
                            select x;
                }

                if (query != null)
                {
                    foreach (OltpclaimRecordLinesView item in query)
                    {
                        claimRecordLine = new ClaimRecordLineValidationData
                        {
                            Id = item.Id,
                            TransactionId = item.TransactionId,
                            ClaimRefCode = item.ClaimRefCode,
                            ClaimDate = item.CreatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                claimRecordLine = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return claimRecordLine;
        }

        public bool Put(Guid id, Guid tenantId, bool validated, Guid receiptId, DateTime validationDate)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpclaimRecordLines
                            where x.TenantId == tenantId && x.Id == id
                            select x;

                if (query != null)
                {
                    OltpclaimRecordLines claimRecordLine = null;

                    foreach (OltpclaimRecordLines item in query)
                    {
                        claimRecordLine = item;
                    }

                    if (claimRecordLine != null)
                    {
                        claimRecordLine.Validated = validated;
                        claimRecordLine.ValidationDate = validationDate;
                        claimRecordLine.ReceiptId = receiptId;
                        claimRecordLine.UpdatedDate = DateTime.UtcNow;

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

        #region FULLDATA

        private List<FlattenedTransactionData> GetClaimableTransactionsDataForUser(string userId, Guid countryId, Guid stateId, DateTime dateTime)
        {
            List<FlattenedTransactionData> transactions = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetClaimableTransactionsByStateTenantFocus(stateId, countryId, userId, dateTime)
                                //where (x.AvailableQuantity == -1 || x.AvailableQuantity > 0)
                            select x;

                if (query != null)
                {
                    transactions = new List<FlattenedTransactionData>();
                    FlattenedTransactionData transaction = null;

                    foreach (TempclaimableTransactions item in query)
                    {
                        transaction = new FlattenedTransactionData
                        {
                            Transaction = new Transaction
                            {
                                Id = item.Id,
                                TenantId = item.TenantId,
                                UserId = item.UserId,
                                CategoryId = item.CategoryId,
                                TransactionType = item.TransactionType,
                                ReferenceId = item.ReferenceId,
                                ReferenceType = item.ReferenceType,
                                ClaimPoints = item.ClaimPoints,
                                Value = item.Value,
                                RegularValue = item.RegularValue,
                                Description = item.Description,
                                Name = item.Name,
                                Code = item.Code,
                                CodeImg = item.CodeImg,
                                Completed = item.Completed,
                                CreatedDate = item.CreatedDate,
                                CompletedDate = item.CompletedDate,
                                OriginId = item.OriginId,
                                ReleaseDate = item.ReleaseDate,
                                ExpirationDate = item.ExpirationDate,
                                TotalClaimCount = item.TotalClaimsCount != null ? (int)item.TransactionClaimCount : 0,
                                TransactionClaimCount = item.TransactionClaimCount,
                                OneTimeClaim = item.OneTimeClaim,
                                ShowToUser = item.ShowToUser,
                                DealType = item.DealType,
                                DealTypeName = GetDealTypeName(item.DealType)
                            },
                            Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                            {
                                Id = (Guid)item.TenantId,
                                Name = item.TenantName,
                                Logo = item.TenantLogo,
                                LandingImg = null,
                                CountryId = Guid.Empty,//USELESS
                                CurrencySymbol = item.CurrencySymbol,
                                Type = item.TenantType,
                                CategoryId = item.TenantCategoryId,
                                CategoryName = item.TenantCategoryName,
                                RelevanceScore = null,//When its selector is category, there is no info about tenant relevance
                                NearestBranchId = Guid.Empty,
                                NearestBranchName = "",
                                NearestBranchLatitude = null,
                                NearestBranchLongitude = null,
                                MemberShipId = Guid.Empty,
                                IsMember = true,
                                PointsBalance = 0
                            },
                            Branch = new BasicBranchData
                            {
                                Id = item.BranchId,
                                Name = item.BranchName,
                                InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                DescriptiveAddress = item.BranchDescriptiveAddress,
                                Latitude = item.BranchLatitude,
                                Longitude = item.BranchLongitude,
                                Distance = -1,//USELESS
                                CityId = item.BranchCityId,
                                StateId = item.BranchStateId,
                                Enabled = false
                            },

                            AvailableQuantity = item.AvailableQuantity != null ? (int)item.AvailableQuantity : 0,
                            GeosegmentationType = item.GeoSegmentationType != null ? (int)item.GeoSegmentationType : 0,
                            TotalClaimCount = item.TotalClaimsCount != null ? (int)item.TotalClaimsCount : 0
                        };

                        transactions.Add(transaction);
                    }
                }

            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }

        private List<FlattenedTransactionData> GetTransactionDetails(Guid transactionId, string userId)
        {
            List<FlattenedTransactionData> transactions = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetTransactionDetailsWithLocations(transactionId, userId)
                            select x;

                if (query != null)
                {
                    FlattenedTransactionData transaction;

                    transactions = new List<FlattenedTransactionData>();

                    foreach (TempclaimableTransactions item in query)
                    {
                        transaction = new FlattenedTransactionData
                        {
                            Transaction = new Transaction
                            {
                                Id = item.Id,
                                TenantId = item.TenantId,
                                UserId = item.UserId,
                                CategoryId = item.CategoryId,
                                TransactionType = item.TransactionType,
                                ReferenceId = item.ReferenceId,
                                ReferenceType = item.ReferenceType,
                                ClaimPoints = item.ClaimPoints,
                                Value = item.Value,
                                RegularValue = item.RegularValue,
                                Description = item.Description,
                                Name = item.Name,
                                Code = item.Code,
                                CodeImg = item.CodeImg,
                                Completed = item.Completed,
                                CreatedDate = item.CreatedDate,
                                CompletedDate = item.CompletedDate,
                                OriginId = item.OriginId,
                                ReleaseDate = item.ReleaseDate,
                                ExpirationDate = item.ExpirationDate,
                                TotalClaimCount = item.TotalClaimsCount != null ? (int)item.TransactionClaimCount : 0,
                                TransactionClaimCount = item.TransactionClaimCount,
                                OneTimeClaim = item.OneTimeClaim,
                                ShowToUser = item.ShowToUser,
                                DealType = item.DealType,
                                DealTypeName = GetDealTypeName(item.DealType)
                            },
                            Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                            {
                                Id = (Guid)item.TenantId,
                                Name = item.TenantName,
                                Logo = item.TenantLogo,
                                LandingImg = null,
                                CountryId = Guid.Empty,//USELESS
                                CurrencySymbol = item.CurrencySymbol,
                                Type = item.TenantType,
                                CategoryId = item.TenantCategoryId,
                                CategoryName = item.TenantCategoryName,
                                RelevanceScore = null,//When its selector is category, there is no info about tenant relevance
                                NearestBranchId = Guid.Empty,
                                NearestBranchName = "",
                                NearestBranchLatitude = null,
                                NearestBranchLongitude = null,
                                MemberShipId = Guid.Empty,
                                IsMember = true,
                                PointsBalance = 0
                            },
                            Branch = new BasicBranchData
                            {
                                Id = item.BranchId,
                                Name = item.BranchName,
                                InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                DescriptiveAddress = item.BranchDescriptiveAddress,
                                Latitude = item.BranchLatitude,
                                Longitude = item.BranchLongitude,
                                Distance = -1,//USELESS
                                CityId = item.BranchCityId,
                                StateId = item.BranchStateId,
                                Enabled = false
                            },

                            AvailableQuantity = item.AvailableQuantity != null ? (int)item.AvailableQuantity : 0,
                            GeosegmentationType = item.GeoSegmentationType != null ? (int)item.GeoSegmentationType : 0,
                            TotalClaimCount = item.TotalClaimsCount != null ? (int)item.TotalClaimsCount : 0
                        };

                        transactions.Add(transaction);
                    }
                }

            }
            catch (Exception e)
            {
                transactions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactions;
        }


        private void BuildFullTransactionList(ref List<FullTransactionData> transactionsData, ref List<TransactionDataWithBranches> enabledTransactions, bool includeBranchList)
        {
            TransactionDataWithBranches currentTransaction;
            List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
            List<BasicBranchData> availableBranches;
            IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
            int? branchGroupsCount;

            if (!includeBranchList)
            {
                bool locationMatch = false;

                for (int i = 0; i < enabledTransactions.Count; i++)
                {
                    currentTransaction = enabledTransactions[i];
                    locationMatch = false;

                    //This means it's enabled for the complete country, then it's enabled for all the potential branches
                    if (currentTransaction.GeosegmentationType != GeoSegmentationTypes.Country)
                    {
                        enabledLocations = this._businessObjects.ContentLocations.Gets(enabledTransactions[i].Transaction.Id);
                        availableBranches = currentTransaction.Branches.GroupBy(x => x.Id)
                                                                           .Select(grp => grp.First())
                                                                           .ToList();

                        if (enabledLocations?.Count > 0)
                        {

                            //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                            switch (currentTransaction.GeosegmentationType)
                            {
                                case GeoSegmentationTypes.State:
                                    //Will group by state
                                    branchesGrouped = currentTransaction.Branches.GroupBy(x => x.StateId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    locationMatch = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        locationMatch = false;
                                    }

                                    break;
                                case GeoSegmentationTypes.City:
                                    //Will group by state
                                    branchesGrouped = currentTransaction.Branches.GroupBy(x => x.CityId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    locationMatch = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        locationMatch = false;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            //If no enabled locations needs to be removed
                            locationMatch = false;
                        }
                    }
                    else
                    {
                        //If the offer is enabled to be avaiable to the complete country, then it's valid
                        locationMatch = true;
                    }

                    if (locationMatch)
                    {

                        transactionsData.Add(new FullTransactionData
                        {
                            AvailableQuantity = currentTransaction.AvailableQuantity,
                            GeosegmentationType = currentTransaction.GeosegmentationType,
                            TotalClaimCount = currentTransaction.TotalClaimCount,
                            Transaction = currentTransaction.Transaction,
                            Tenant = currentTransaction.Tenant,
                            Branches = new List<BasicBranchData>()
                        }
                        );
                    }

                }
            }
            else
            {
                List<BasicBranchData> enabledBranches = null;

                for (int i = 0; i < enabledTransactions.Count; i++)
                {
                    currentTransaction = enabledTransactions[i];

                    if (currentTransaction.GeosegmentationType != GeoSegmentationTypes.Country)
                    {
                        enabledLocations = this._businessObjects.ContentLocations.Gets((Guid)enabledTransactions[i].Transaction.ReferenceId);
                    }
                    else
                    {
                        enabledLocations = new List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>>();
                    }


                    availableBranches = currentTransaction.Branches.GroupBy(x => x.Id)
                                                                           .Select(grp => grp.First())
                                                                           .ToList();
                    enabledBranches = new List<BasicBranchData>();

                    //If it isn't a loyalty reward
                    if (currentTransaction.Transaction.OriginId != TransactionOrigins.AppLoyaltyClub)
                    {
                        //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                        switch (currentTransaction.GeosegmentationType)
                        {
                            case GeoSegmentationTypes.State:
                                //Will group by state
                                branchesGrouped = currentTransaction.Branches.GroupBy(x => x.StateId);
                                branchGroupsCount = branchesGrouped?.Count();


                                if (branchGroupsCount != null && branchGroupsCount > 0)
                                {
                                    for (int j = 0; j < enabledLocations.Count; j++)
                                    {
                                        for (int k = 0; k < branchGroupsCount; k++)
                                        {
                                            //If the location has the same geosegmentation and has the same location reference Id
                                            if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                            {
                                                enabledBranches.AddRange(availableBranches.Where(x => x.StateId == enabledLocations[j].Value).ToList());
                                            }
                                        }
                                    }
                                }

                                break;
                            case GeoSegmentationTypes.City:
                                //Will group by state
                                branchesGrouped = currentTransaction.Branches.GroupBy(x => x.CityId);
                                branchGroupsCount = branchesGrouped?.Count();


                                if (branchGroupsCount != null && branchGroupsCount > 0)
                                {
                                    for (int j = 0; j < enabledLocations.Count; j++)
                                    {
                                        for (int k = 0; k < branchGroupsCount; k++)
                                        {
                                            //If the location has the same geosegmentation and has the same location reference Id
                                            if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                            {
                                                enabledBranches.AddRange(availableBranches.Where(x => x.CityId == enabledLocations[j].Value).ToList());
                                            }
                                        }
                                    }
                                }

                                break;
                            case GeoSegmentationTypes.Country:
                                //This means the offer is enabled to all the existing branches
                                enabledBranches = availableBranches;

                                break;
                        }
                    }


                    if (enabledBranches?.Count > 0)
                    {

                        transactionsData.Add(new FullTransactionData
                        {
                            AvailableQuantity = currentTransaction.AvailableQuantity,
                            GeosegmentationType = currentTransaction.GeosegmentationType,
                            TotalClaimCount = currentTransaction.TotalClaimCount,
                            Transaction = currentTransaction.Transaction,
                            Tenant = currentTransaction.Tenant,
                            Branches = enabledBranches
                        }
                        );
                    }
                    else
                    {
                        transactionsData.Add(new FullTransactionData
                        {
                            AvailableQuantity = currentTransaction.AvailableQuantity,
                            GeosegmentationType = currentTransaction.GeosegmentationType,
                            TotalClaimCount = currentTransaction.TotalClaimCount,
                            Transaction = currentTransaction.Transaction,
                            Tenant = currentTransaction.Tenant,
                            Branches = new List<BasicBranchData>()
                        }
                        );
                    }

                }
            }
        }

        private void BuildFullTransactionList(ref List<FullTransactionData> transactionsData, ref List<TransactionDataWithBranches> enabledTransactions, bool includeBranchList, bool includeNearestBranch)
        {
            TransactionDataWithBranches currentTransaction;
            List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
            List<BasicBranchData> availableBranches;
            BasicBranchData nearestBranch;
            IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
            int? branchGroupsCount;

            List<BasicBranchData> enabledBranches = null;

            for (int i = 0; i < enabledTransactions.Count; i++)
            {
                currentTransaction = enabledTransactions[i];

                enabledLocations = this._businessObjects.ContentLocations.Gets(enabledTransactions[i].Transaction.Id);
                availableBranches = currentTransaction.Branches.GroupBy(x => x.Id)
                                                                       .Select(grp => grp.First())
                                                                       .ToList();
                enabledBranches = new List<BasicBranchData>();

                //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                switch (currentTransaction.GeosegmentationType)
                {
                    case GeoSegmentationTypes.State:
                        //Will group by state
                        branchesGrouped = currentTransaction.Branches.GroupBy(x => x.StateId);
                        branchGroupsCount = branchesGrouped?.Count();


                        if (branchGroupsCount != null && branchGroupsCount > 0)
                        {
                            for (int j = 0; j < enabledLocations.Count; j++)
                            {
                                for (int k = 0; k < branchGroupsCount; k++)
                                {
                                    //If the location has the same geosegmentation and has the same location reference Id
                                    if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                    {
                                        enabledBranches.AddRange(availableBranches.Where(x => x.StateId == enabledLocations[j].Value).ToList());
                                    }
                                }
                            }
                        }

                        break;
                    case GeoSegmentationTypes.City:
                        //Will group by state
                        branchesGrouped = currentTransaction.Branches.GroupBy(x => x.CityId);
                        branchGroupsCount = branchesGrouped?.Count();


                        if (branchGroupsCount != null && branchGroupsCount > 0)
                        {
                            for (int j = 0; j < enabledLocations.Count; j++)
                            {
                                for (int k = 0; k < branchGroupsCount; k++)
                                {
                                    //If the location has the same geosegmentation and has the same location reference Id
                                    if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                    {
                                        enabledBranches.AddRange(availableBranches.Where(x => x.CityId == enabledLocations[j].Value).ToList());
                                    }
                                }
                            }
                        }

                        break;
                    case GeoSegmentationTypes.Country:
                        //This means the offer is available to all the branches in the country
                        enabledBranches = availableBranches;

                        break;
                }

                if (enabledBranches?.Count > 0)
                {

                    enabledBranches = enabledBranches.OrderBy(x => x.Distance).ToList();

                    nearestBranch = enabledBranches.ElementAt(0);

                    if (includeNearestBranch && nearestBranch != null)
                    {
                        currentTransaction.Tenant.NearestBranchId = nearestBranch.Id;
                        currentTransaction.Tenant.NearestBranchName = nearestBranch.Name;
                        currentTransaction.Tenant.NearestBranchLatitude = nearestBranch.Latitude;
                        currentTransaction.Tenant.NearestBranchLongitude = nearestBranch.Longitude;
                        currentTransaction.Tenant.NearesBranchDistance = nearestBranch.Distance;
                    }

                    if (includeBranchList)
                    {
                        transactionsData.Add(new FullTransactionData
                        {
                            AvailableQuantity = currentTransaction.AvailableQuantity,
                            GeosegmentationType = currentTransaction.GeosegmentationType,
                            TotalClaimCount = currentTransaction.TotalClaimCount,
                            Transaction = currentTransaction.Transaction,
                            Tenant = currentTransaction.Tenant,
                            Branches = enabledBranches
                        }
                        );
                    }
                    else
                    {

                        transactionsData.Add(new FullTransactionData
                        {
                            AvailableQuantity = currentTransaction.AvailableQuantity,
                            GeosegmentationType = currentTransaction.GeosegmentationType,
                            TotalClaimCount = currentTransaction.TotalClaimCount,
                            Transaction = currentTransaction.Transaction,
                            Tenant = currentTransaction.Tenant,
                            Branches = new List<BasicBranchData>()
                        }
                        );

                    }

                }

            }
        }

        public List<FullTransactionData> GetEnabledTransactionsForUserByState(Guid countryId, Guid stateId, string userId, DateTime dateTime, bool includeBranchList)
        {
            List<FullTransactionData> transactionsData = new List<FullTransactionData>();

            try
            {
                List<FlattenedTransactionData> flattenedTransactions = this.GetClaimableTransactionsDataForUser(userId, countryId, stateId, dateTime);

                if (flattenedTransactions?.Count > 0)
                {
                    TransactionDataWithBranches currentTransaction;
                    IEnumerable<IGrouping<Guid, FlattenedTransactionData>> groupedByOfferId = flattenedTransactions.GroupBy(x => x.Transaction.Id);
                    List<TransactionDataWithBranches> enabledTransactions = new List<TransactionDataWithBranches>();


                    foreach (IGrouping<Guid, FlattenedTransactionData> transactionDataGroup in groupedByOfferId)
                    {
                        FlattenedTransactionData[] transactionsGroup = transactionDataGroup.ToArray();

                        currentTransaction = new TransactionDataWithBranches
                        {
                            Transaction = transactionsGroup[0].Transaction,
                            Tenant = transactionsGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            AvailableQuantity = transactionsGroup[0].AvailableQuantity,
                            GeosegmentationType = transactionsGroup[0].GeosegmentationType,
                            TotalClaimCount = transactionsGroup[0].TotalClaimCount
                        };

                        for (int i = 0; i < transactionsGroup.Length; ++i)
                        {
                            currentTransaction.Branches.Add(transactionsGroup[i].Branch);
                        }

                        enabledTransactions.Add(currentTransaction);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled

                    this.BuildFullTransactionList(ref transactionsData, ref enabledTransactions, includeBranchList);

                }

            }
            catch (Exception e)
            {
                transactionsData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transactionsData;

        }

        public FullTransactionData GetTransactionData(Guid id, string userId, bool includeBranchList)
        {
            FullTransactionData transaction = null;

            try
            {
                List<FlattenedTransactionData> flattenedTransactions = this.GetTransactionDetails(id, userId);

                if (flattenedTransactions?.Count > 0)
                {
                    TransactionDataWithBranches currentTransaction;
                    IEnumerable<IGrouping<Guid, FlattenedTransactionData>> groupedByTransactionId = flattenedTransactions.GroupBy(x => x.Transaction.Id);
                    List<TransactionDataWithBranches> enabledTransactions = new List<TransactionDataWithBranches>();

                    foreach (IGrouping<Guid, FlattenedTransactionData> transactionDataGroup in groupedByTransactionId)
                    {
                        FlattenedTransactionData[] transactionsGroup = transactionDataGroup.ToArray();

                        currentTransaction = new TransactionDataWithBranches
                        {
                            Transaction = transactionsGroup[0].Transaction,
                            Tenant = transactionsGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            AvailableQuantity = transactionsGroup[0].AvailableQuantity,
                            GeosegmentationType = transactionsGroup[0].GeosegmentationType,
                            TotalClaimCount = transactionsGroup[0].TotalClaimCount
                        };

                        for (int i = 0; i < transactionsGroup.Length; ++i)
                        {
                            currentTransaction.Branches.Add(transactionsGroup[i].Branch);
                        }

                        enabledTransactions.Add(currentTransaction);
                    }

                    //At this point the transactions have all the branches where it can be enabled, now it's time to verify in which branches
                    //each transaction can be actually enabled
                    List<FullTransactionData> transactionsData = new List<FullTransactionData>();

                    this.BuildFullTransactionList(ref transactionsData, ref enabledTransactions, includeBranchList);

                    if (transactionsData?.Count > 0)
                    {
                        transaction = transactionsData.ElementAt(0);
                    }
                }
            }
            catch (Exception e)
            {
                transaction = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return transaction;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new FileManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public TransactionManager(BusinessObjects businessObjects)
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
