using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Xml.Linq;
using YOY.Values;

namespace YOY.DAO.Entities.DB.StoredProcedures
{
    public class StoredProceduresHandler
    {
        #region PRIVATE_PROPERTIES

        private yoyIj7qM58dCjContext _dbContext { set; get; }

        #endregion

        #region STORED_PROCEDURES

        public bool? AddAccountCode(string userId, string accCode)
        {
            bool? validCode = false;

            try
            {

                var userIdParam = new SqlParameter("userId", userId);
                var accCodeParam = new SqlParameter("accountCode", accCode);
                var validCodeParam = new SqlParameter("validCode", validCode) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[AddAccountCode] @userId, @accountCode, @validCode output", new[] { userIdParam, accCodeParam, validCodeParam });
                validCode = Convert.ToBoolean(validCodeParam.Value);
            }
            catch (Exception e)
            {
                validCode = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return validCode;

        }

        public bool? AddExceptionLogging(int layer, string thrownClass, string exceptionMsg, string exceptionType, string exceptionSource, string exceptionURL)
        {
            bool? added;
            try
            {

                var layerParam = new SqlParameter("Layer", layer);
                var thrownClassParam = new SqlParameter("ThrownClass", thrownClass);
                var exceptionMsgParam = new SqlParameter("ExceptionMsg", exceptionMsg);
                var exceptionTypeParam = new SqlParameter("ExceptionType", exceptionType);
                var exceptionSourceParam = new SqlParameter("ExceptionSource", exceptionSource);
                var exceptionUrlParam = new SqlParameter("ExceptionURL", exceptionURL);

                // Processing.  
                int result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[AddExceptionLogging] @Layer, @ThrownClass, @ExceptionMsg, @ExceptionType, @ExceptionSource, @ExceptionURL", new[] { layerParam, thrownClassParam, exceptionMsgParam, exceptionTypeParam, exceptionSourceParam, exceptionUrlParam });

                if (result > 0)
                    added = true;
                else
                    added = false;
            }
            catch (Exception)
            {
                added = null;
            }

            return added;

        }

        public int CheckConversionOperationCodeExistence(string operationCode, DateTime limitDate)
        {
            int codeCount = -1;

            try
            {

                var operationCodeParam = new SqlParameter("operationCode", operationCode);
                var limitDateParam = new SqlParameter("limitDate", limitDate);
                var codeCountParam = new SqlParameter("codeCount", codeCount) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[CheckConversionOperationCodeExistence] @operationCode, @limitDate, @codeCount output", new[] { operationCodeParam, limitDateParam, codeCountParam });
                codeCount = Convert.ToInt32(codeCountParam.Value);
            }
            catch (Exception e)
            {
                codeCount = -1;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return codeCount;

        }


        public int CheckPhoneNumberUniqueness(string phoneNumber, string countryPhonePrefix)
        {
            int phoneCount = -1;

            try
            {

                var phoneNumberParam = new SqlParameter("phoneNumber", phoneNumber);
                var countryPhonePrefixParam = new SqlParameter("countryPhonePrefix", countryPhonePrefix);
                var phoneCountParam = new SqlParameter("phoneCount", phoneCount) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[CheckPhoneNumberUniqueness] @phoneNumber, @countryPhonePrefix, @phoneCount output", new[] { phoneNumberParam, countryPhonePrefixParam, phoneCountParam });
                phoneCount = Convert.ToInt32(phoneCountParam.Value);
            }
            catch (Exception e)
            {
                phoneCount = -1;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return phoneCount;

        }


        public int? GetCashIncentivesCountForCommerceByDateRange(Guid tenantId, DateTime minDate, DateTime maxDate)
        {
            int? incentivesCount = -1;

            try
            {

                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var minDateParam = new SqlParameter("minDate", minDate);
                var maxDateParam = new SqlParameter("maxDate", maxDate);
                var incentivesCountParam = new SqlParameter("incentivesCount", incentivesCount) { Direction = ParameterDirection.Output };

                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();//This context is created because this method is part of an async logic

                // Processing.  
                context.Database.ExecuteSqlRaw("EXEC [dbo].[GetCashIncentivesCountForCommerceByDateRange] @tenantId, @minDate, @maxDate, @incentivesCount output", new[] { tenantIdParam, minDateParam, maxDateParam, incentivesCountParam });
                incentivesCount = Convert.ToInt32(incentivesCountParam.Value);
            }
            catch (Exception e)
            {
                incentivesCount = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return incentivesCount;

        }

        public bool? DeleteBranch(Guid id)
        {
            bool? success;
            try
            {

                var idParam = new SqlParameter("id", id);

                // Processing.  
                int result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[DeleteBranch] @id", new[] { idParam });


                if (result > 0)
                    success = true;
                else
                    success = false;
            }
            catch (Exception e)
            {
                success = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;

        }

        public string GetCategoryKeywords(Guid categoryId, int herarchyLevel)
        {
            string keywordsString = "";

            try
            {

                var categoryIdParam = new SqlParameter("categoryId", categoryId);
                var herarchyLevelParam = new SqlParameter("herarchyLevel", herarchyLevel);
                var keywordsStringParam = new SqlParameter("keywordsString", SqlDbType.VarChar, 4096) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[GetCategoryKeywords] @categoryId, @herarchyLevel, @keywordsString output", new[] { categoryIdParam, herarchyLevelParam, keywordsStringParam });
                keywordsString = Convert.ToString(keywordsStringParam.Value);
            }
            catch (Exception e)
            {
                keywordsString = "";
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return keywordsString;

        }

        public int? GetEnabledCheckInsCountForUser(string userId, Guid tenantId, DateTime startDate, DateTime endDate)
        {
            int? checkInCount = -1;

            try
            {

                var userIdParam = new SqlParameter("userId", userId);
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var startDateParam = new SqlParameter("startDate", startDate);
                var endDateParam = new SqlParameter("endDate", endDate);
                var checkInCountParam = new SqlParameter("checkInCount", checkInCount) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[GetEnabledCheckInsCountForUser] @userId, @tenantId, @startDate, @endDate, @checkInCount output", new[] { userIdParam, tenantIdParam, startDateParam, endDateParam, checkInCountParam });
                checkInCount = Convert.ToInt32(checkInCountParam.Value);
            }
            catch (Exception e)
            {
                checkInCount = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return checkInCount;

        }

        public int? GetMembershipActionsCountForUser(Guid mebershipId, Guid tenantId, DateTime startDate, DateTime endDate)
        {
            int? actionsCount = -1;

            try
            {

                var mebershipIdParam = new SqlParameter("mebershipId", mebershipId);
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var startDateParam = new SqlParameter("startDate", startDate);
                var endDateParam = new SqlParameter("endDate", endDate);
                var actionsCountParam = new SqlParameter("actionsCount", actionsCount) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[GetMembershipActionsCountForUser] @mebershipId, @tenantId, @startDate, @endDate, @actionsCount output", new[] { mebershipIdParam, tenantIdParam, startDateParam, endDateParam, actionsCountParam });
                actionsCount = Convert.ToInt32(actionsCountParam.Value);
            }
            catch (Exception e)
            {
                actionsCount = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return actionsCount;

        }


        public int? GetOffersCountForCommerce(Guid tenantId, DateTime dateTime, int offerPurpose)
        {
            int? offersCount = -1;

            try
            {

                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var offersCountParam = new SqlParameter("offersCount", offersCount) { Direction = ParameterDirection.Output };

                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();//This context is created because this method is part of an async logic

                // Processing.  
                context.Database.ExecuteSqlRaw("EXEC [dbo].[GetOffersCountForCommerce] @tenantId, @dateTime, @offerPurpose, @offersCount output", new[] { tenantIdParam, dateTimeParam, offerPurposeParam, offersCountParam });
                offersCount = Convert.ToInt32(offersCountParam.Value);
            }
            catch (Exception e)
            {
                offersCount = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offersCount;

        }

        public int? GetOffersCountForCommerceByDateRange(Guid tenantId, DateTime minDate, DateTime maxDate, int offerPurpose)
        {
            int? offersCount = -1;

            try
            {

                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var minDateParam = new SqlParameter("minDate", minDate);
                var maxDateParam = new SqlParameter("maxDate", maxDate);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var offersCountParam = new SqlParameter("offersCount", offersCount) { Direction = ParameterDirection.Output };

                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();//This context is created because this method is part of an async logic

                // Processing.  
                context.Database.ExecuteSqlRaw("EXEC [dbo].[GetOffersCountForCommerceByDateRange] @tenantId, @minDate, @maxDate, @offerPurpose, @offersCount output", new[] { tenantIdParam, minDateParam, maxDateParam, offerPurposeParam, offersCountParam });
                offersCount = Convert.ToInt32(offersCountParam.Value);
            }
            catch (Exception e)
            {
                offersCount = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offersCount;

        }

        public int? GetOffersCountForPreference(Guid preferenceId, DateTime dateTime, int offerPurpose)
        {
            int? offersCount = -1;

            try
            {

                var preferenceIdParam = new SqlParameter("preferenceId", preferenceId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var offersCountParam = new SqlParameter("offersCount", offersCount) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[GetOffersCountForPreference] @preferenceId, @dateTime, @offerPurpose, @offersCount output", new[] { preferenceIdParam, dateTimeParam, offerPurposeParam, offersCountParam });
                offersCount = Convert.ToInt32(offersCountParam.Value);
            }
            catch (Exception e)
            {
                offersCount = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offersCount;

        }

        public string GetParentCategoryName(Guid categoryId, int herarchyLevel)
        {
            string parentCategoryName;
            try
            {

                var categoryIdParam = new SqlParameter("categoryId", categoryId);
                var herarchyLevelParam = new SqlParameter("herarchyLevel", herarchyLevel);
                var parentCategoryNameParam = new SqlParameter("parentCategoryName", SqlDbType.VarChar, 120) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[GetParentCategoryName] @categoryId, @herarchyLevel, @parentCategoryName output", new[] { categoryIdParam, herarchyLevelParam, parentCategoryNameParam });
                parentCategoryName = Convert.ToString(parentCategoryNameParam.Value);
            }
            catch (Exception e)
            {
                parentCategoryName = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return parentCategoryName;

        }

        public Guid GetPreferenceIdForCommerceCategory(Guid categoryId, int herarchyLevel)
        {
            Guid preferenceId = Guid.Empty;

            try
            {
                Guid? preferenceIdAux = Guid.Empty;

                var categoryIdParam = new SqlParameter("categoryId", categoryId);
                var herarchyLevelParam = new SqlParameter("herarchyLevel", herarchyLevel);
                var preferenceIdParam = new SqlParameter("preferenceId", preferenceIdAux) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[GetPreferenceIdForCommerceCategory] @categoryId, @herarchyLevel, @preferenceId output", new[] { categoryIdParam, herarchyLevelParam, preferenceIdParam });
                
                if(preferenceIdParam.Value != null)
                    Guid.TryParse(preferenceIdParam.Value.ToString(), out preferenceId);

                //preferenceId = Convert.ToString(preferenceIdParam.Value);
            }
            catch (Exception e)
            {
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return preferenceId;

        }

        public string GetPreferenceNameForCommerceCategory(Guid categoryId, int herarchyLevel)
        {
            string preferenceName;

            try
            {

                var categoryIdParam = new SqlParameter("categoryId", categoryId);
                var herarchyLevelParam = new SqlParameter("herarchyLevel", herarchyLevel);
                var preferenceNameParam = new SqlParameter("preferenceName", SqlDbType.VarChar, 120) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[GetPreferenceNameForCommerceCategory] @categoryId, @herarchyLevel, @preferenceName output", new[] { categoryIdParam, herarchyLevelParam, preferenceNameParam });
                preferenceName = Convert.ToString(preferenceNameParam.Value);
            }
            catch (Exception e)
            {
                preferenceName = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return preferenceName;

        }

        public Guid GetPreferenceIdForProductCategory(Guid categoryId, int herarchyLevel)
        {
            Guid preferenceId = Guid.Empty;

            try
            {

                Guid? preferenceIdAux = Guid.Empty;

                var categoryIdParam = new SqlParameter("categoryId", categoryId);
                var herarchyLevelParam = new SqlParameter("herarchyLevel", herarchyLevel);
                var preferenceIdParam = new SqlParameter("preferenceId", preferenceIdAux) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[GetPreferenceIdForProductCategory] @categoryId, @herarchyLevel, @preferenceId output", new[] { categoryIdParam, herarchyLevelParam, preferenceIdParam });

                if (preferenceIdParam.Value != null)
                    Guid.TryParse(preferenceIdParam.Value.ToString(), out preferenceId);
            }
            catch (Exception e)
            {
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return preferenceId;

        }

        public string GetPreferenceNameForProductCategory(Guid categoryId, int herarchyLevel)
        {
            string preferenceName;

            try
            {

                var categoryIdParam = new SqlParameter("categoryId", categoryId);
                var herarchyLevelParam = new SqlParameter("herarchyLevel", herarchyLevel);
                var preferenceNameParam = new SqlParameter("preferenceName", SqlDbType.VarChar, 120) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[GetPreferenceNameForProductCategory] @categoryId, @herarchyLevel, @preferenceName output", new[] { categoryIdParam, herarchyLevelParam, preferenceNameParam });
                preferenceName = Convert.ToString(preferenceNameParam.Value);
            }
            catch (Exception e)
            {
                preferenceName = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return preferenceName;

        }

        public bool? InsertBranch(Guid id, Guid tenantId, Guid? franchiseeId, Guid stateId, Guid cityId, Guid? branchHolderId, Guid? branchHolderDepartmentId, int type, string name, string postCode,
             string email, string contactName, string contactPhone, string contactEmail, string ordersPhone, bool independantOwner,  decimal latitude, decimal longitude, XElement locationAddress, 
             string descriptiveAddress, int orderTakingType, Guid? geofenceId, string hashedCode)
        {
            bool? created;

            try
            {
                var idParam = new SqlParameter("id", id);
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var franchiseeIdParam = new SqlParameter("franchiseeId", (object)franchiseeId ?? DBNull.Value);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var cityIdParam = new SqlParameter("cityId", cityId);
                var branchHolderIdParam = new SqlParameter("branchHolderId", (object)branchHolderId ?? DBNull.Value);
                var branchHolderDepartmentIdParam = new SqlParameter("branchHolderDepartmentId", (object)branchHolderDepartmentId ?? DBNull.Value);
                var typeParam = new SqlParameter("type", type);
                var nameParam = new SqlParameter("name", name);
                var postCodeParam = new SqlParameter("postCode", postCode);
                var emailParam = new SqlParameter("email", email);
                var contactNameParam = new SqlParameter("contactName", contactName);
                var contactPhoneParam = new SqlParameter("contactPhone", contactPhone);
                var contactEmailParam = new SqlParameter("contactEmail", contactEmail);
                var ordersPhoneParam = new SqlParameter("ordersPhone", ordersPhone);
                var independantOwnerParam = new SqlParameter("independantOwner", independantOwner);
                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var locationAddressParam = new SqlParameter
                {
                    ParameterName = "locationAddress",
                    SqlDbType = SqlDbType.Xml,
                    Value = new SqlXml(locationAddress.CreateReader())
                };
                var descriptiveAddressParam = new SqlParameter("descriptiveAddress", descriptiveAddress);
                var orderTakingTypeParam = new SqlParameter("orderTakingType", orderTakingType);
                var geofenceIdParam = new SqlParameter("geofenceId", (object)geofenceId ?? DBNull.Value);
                var hashedCodeParam = new SqlParameter("hashedCode", hashedCode);

                int result = -1;
                var resultParam = new SqlParameter("result", result) { Direction = ParameterDirection.Output };

                // Processing.  
                 _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[InsertBranch] @id, @tenantId, @franchiseeId, @stateId, @cityId, @branchHolderId, @branchHolderDepartmentId, @type, @name, @postCode, @email, @contactName, @contactPhone, @contactEmail, @ordersPhone, @independantOwner, @latitude, @longitude, @locationAddress, @descriptiveAddress, @orderTakingType, @geofenceId, @hashedCode, @result OUTPUT", 
                    new[] { idParam, tenantIdParam, franchiseeIdParam, stateIdParam, cityIdParam, branchHolderIdParam, branchHolderDepartmentIdParam, typeParam, nameParam, postCodeParam, emailParam, contactNameParam, contactPhoneParam, contactEmailParam, ordersPhoneParam, independantOwnerParam, latitudeParam, longitudeParam, locationAddressParam, descriptiveAddressParam, orderTakingTypeParam, geofenceIdParam, hashedCodeParam, resultParam});

                result = Convert.ToInt32(resultParam.Value);

                if (result > 0)
                    created = true;
                else
                    created = false;

            }
            catch(Exception e)
            {
                created = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return created;
        }

        public bool? SetBranchGeoPoint(Guid id, decimal latitude, decimal longitude)
        {
            bool? success;
            try
            {

                var idParam = new SqlParameter("id", id);
                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", latitude);

                // Processing.  
                int result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SetBranchGeoPoint] @id, @latitude, @longitude", new[] { idParam, latitudeParam, longitudeParam });


                if (result > 0)
                    success = true;
                else
                    success = false;
            }
            catch (Exception e)
            {
                success = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;

        }

        public bool? SetPaymentRequestOpCode(Guid id, string opCode, DateTime date)
        {
            bool? validCode = false;

            try
            {

                var idParam = new SqlParameter("id", id);
                var dateParam = new SqlParameter("date", date);
                var opCodeParam = new SqlParameter("opCode", opCode);
                var validCodeParam = new SqlParameter("validCode", validCode) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SetPaymentRequestOpCode] @id, @date, @opCode, @validCode output", new[] { idParam, dateParam, opCodeParam, validCodeParam });
                validCode = Convert.ToBoolean(validCodeParam.Value);
            }
            catch (Exception e)
            {
                validCode = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return validCode;

        }

        public bool? SetUserPersonalId(string userId, Guid countryId, string personalId)
        {
            bool? validPersonalId = false;

            try
            {

                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var personalIdParam = new SqlParameter("personalId", personalId);
                var validPersonalIdParam = new SqlParameter("validPersonalId", validPersonalId) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SetUserPersonalId] @userId, @countryId, @personalId, @validPersonalId output", new[] { userIdParam, countryIdParam, personalIdParam, validPersonalIdParam });
                validPersonalId = Convert.ToBoolean(validPersonalIdParam.Value);
            }
            catch (Exception e)
            {
                validPersonalId = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return validPersonalId;

        }


        public bool? UpdateBranch(Guid id, Guid? franchiseeId, Guid? branchHolderId, Guid? branchHolderDepartmentId, string name, string postCode,
             string email, string contactName, string contactPhone, string contactEmail, string ordersPhone, bool independantOwner, decimal latitude,
             decimal longitude, XElement locationAddress, string descriptiveAddress, int orderTakingType, Guid? geofenceId)
        {
            bool? created;

            try
            {
                var idParam = new SqlParameter("id", id);
                var franchiseeIdParam = new SqlParameter("franchiseeId", (object)franchiseeId ?? DBNull.Value);
                var branchHolderIdParam = new SqlParameter("branchHolderId", (object)branchHolderId ?? DBNull.Value);
                var branchHolderDepartmentIdParam = new SqlParameter("branchHolderDepartmentId", (object)branchHolderDepartmentId ?? DBNull.Value);
                var nameParam = new SqlParameter("name", name);
                var postCodeParam = new SqlParameter("postCode", postCode);
                var emailParam = new SqlParameter("email", email);
                var contactNameParam = new SqlParameter("contactName", contactName);
                var contactPhoneParam = new SqlParameter("contactPhone", contactPhone);
                var contactEmailParam = new SqlParameter("contactEmail", contactEmail);
                var ordersPhoneParam = new SqlParameter("ordersPhone", ordersPhone);
                var independantOwnerParam = new SqlParameter("independantOwner", independantOwner);
                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var locationAddressParam = new SqlParameter
                {
                    ParameterName = "locationAddress",
                    SqlDbType = SqlDbType.Xml,
                    Value = new SqlXml(locationAddress.CreateReader())
                };
                var descriptiveAddressParam = new SqlParameter("descriptiveAddress", descriptiveAddress);
                var orderTakingTypeParam = new SqlParameter("orderTakingType", orderTakingType);
                var geofenceIdParam = new SqlParameter("geofenceId", (object)geofenceId ?? DBNull.Value);

                int result = -1;
                var resultParam = new SqlParameter("result", result) { Direction = ParameterDirection.Output };

                // Processing.  
                _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[UpdateBranch] @id, @franchiseeId, @branchHolderId, @branchHolderDepartmentId, @name, @postCode, @email, @contactName, @contactPhone, @contactEmail, @ordersPhone, @independantOwner, @latitude, @longitude, @locationAddress, @descriptiveAddress, @orderTakingType, @geofenceId, @result OUTPUT",
                    new[] { idParam, franchiseeIdParam, branchHolderIdParam, branchHolderDepartmentIdParam, nameParam, postCodeParam, emailParam, contactNameParam, contactPhoneParam, contactEmailParam, ordersPhoneParam, independantOwnerParam, latitudeParam, longitudeParam, locationAddressParam, descriptiveAddressParam, orderTakingTypeParam, geofenceIdParam, resultParam });

                result = Convert.ToInt32(resultParam.Value);

                if (result > 0)
                    created = true;
                else
                    created = false;

            }
            catch (Exception e)
            {
                created = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return created;
        }

        public bool? UpdateBranchActiveState(Guid id)
        {
            bool? success;
            try
            {

                var idParam = new SqlParameter("id", id);

                // Processing.  
                int result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[UpdateBranchActiveState] @id", new[] { idParam });


                if (result > 0)
                    success = true;
                else
                    success = false;
            }
            catch (Exception e)
            {
                success = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;

        }

        public int? UpdateCashIncentiveQuantity(Guid id, int quantity)
        {
            int? incentiveQuantity = -1;
            try
            {

                var idParam = new SqlParameter("id", id);
                var quantityParam = new SqlParameter("quantity", quantity);
                var incentiveQuantityParam = new SqlParameter("incentiveQuantity", incentiveQuantity) { Direction = ParameterDirection.Output };

                // Processing.  
                int result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[UpdateCashIncentiveQuantity] @id, @quantity, @incentiveQuantity", new[] { idParam, quantityParam, incentiveQuantityParam });
                incentiveQuantity = Convert.ToInt32(incentiveQuantityParam.Value);
            }
            catch (Exception e)
            {
                incentiveQuantity = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return incentiveQuantity;

        }

        public bool? UpdateCashIncentiveUsageCount(Guid id, int quantity)
        {
            bool? success;
            try
            {

                var idParam = new SqlParameter("id", id);
                var quantityParam = new SqlParameter("quantity", quantity);
                
                // Processing.  
                int result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[UpdateCashIncentiveUsageCount] @id, @quantity", new[] { idParam, quantityParam });

                if (result > 0)
                    success = true;
                else
                    success = false;
            }
            catch (Exception e)
            {
                success = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;

        }


        public int? UpdateOfferAvailableQuantity(Guid id, int quantity)
        {
            int? offerQuantity = -1;
            try
            {

                var idParam = new SqlParameter("id", id);
                var quantityParam = new SqlParameter("quantity", quantity);
                var offerQuantityParam = new SqlParameter("offerQuantity", offerQuantity) { Direction = ParameterDirection.Output };

                // Processing.  
                int result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[UpdateOfferAvailableQuantity] @id, @quantity, @offerQuantity", new[] { idParam, quantityParam, offerQuantityParam });
                offerQuantity = Convert.ToInt32(offerQuantityParam.Value);
            }
            catch (Exception e)
            {
                offerQuantity = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerQuantity;

        }

        public bool? UpdateOfferClaimCount(Guid id, int quantity)
        {
            bool? success;
            try
            {

                var idParam = new SqlParameter("id", id);
                var quantityParam = new SqlParameter("quantity", quantity);

                // Processing.  
                int result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[UpdateOfferClaimCount] @id, @quantity", new[] { idParam, quantityParam });

                if (result > 0)
                    success = true;
                else
                    success = false;
            }
            catch (Exception e)
            {
                success = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;

        }

        public bool? UpdateOfferRedeemCount(Guid id, int quantity)
        {
            bool? success;
            try
            {

                var idParam = new SqlParameter("id", id);
                var quantityParam = new SqlParameter("quantity", quantity);

                // Processing.  
                int result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[UpdateOfferRedeemCount] @id, @quantity", new[] { idParam, quantityParam });

                if (result > 0)
                    success = true;
                else
                    success = false;
            }
            catch (Exception e)
            {
                success = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;

        }

        #endregion

        #region CONSTRUCTORS

        public StoredProceduresHandler(yoyIj7qM58dCjContext context)
        {
            this._dbContext = context;
        }

        #endregion
    }
}
