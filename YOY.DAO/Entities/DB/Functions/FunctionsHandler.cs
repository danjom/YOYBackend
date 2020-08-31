using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOY.Values;

namespace YOY.DAO.Entities.DB.Functions
{
    public class FunctionsHandler
    {
        #region PRIVATE_PROPERTIES

        private yoyIj7qM58dCjContext dbContext { set; get; }

        #endregion

        #region METHODS

        private bool? AddExceptionLogging(int layer, string thrownClass, string exceptionMsg, string exceptionType, string exceptionSource, string exceptionURL)
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
                int result = dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[AddExceptionLogging] @Layer, @ThrownClass, @ExceptionMsg, @ExceptionType, @ExceptionSource, @ExceptionURL", new[] { layerParam, thrownClassParam, exceptionMsgParam, exceptionTypeParam, exceptionSourceParam, exceptionUrlParam });

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

        public List<TemprewardOverviews> GetAllRewardsForTenantWithoutRedemptions(Guid tenantId, int offerPurpose)
        {
            List<TemprewardOverviews> temprewards;

            try
            {
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);

                temprewards = dbContext.Set<TemprewardOverviews>().FromSqlRaw("SELECT * FROM [dbo].[GetAllRewardsForTenantWithoutRedemptions](@tenantId, @offerPurpose)", new[] { tenantIdParam, offerPurposeParam }).ToList();
            }
            catch(Exception e)
            {
                temprewards = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temprewards;
        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByCountryBranchHolderFocus(Guid countryId, string userId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByCountryBranchHolderFocus](@countryId, @userId, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByCountryForBranchHolder(Guid countryId, string userId, Guid branchHolderId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var branchHolderIdParam = new SqlParameter("branchHolderId", branchHolderId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByCountryForBranchHolder](@countryId, @userId, @branchHolderId, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, branchHolderIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByCountryForTenant(Guid countryId, string userId, Guid tenantId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByCountryForTenant](@countryId, @userId, @tenantId, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, tenantIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByCountryTenantFocus(Guid countryId, string userId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByCountryTenantFocus](@countryId, @userId, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByCountryWithLocationBranchHolderFocus(decimal latitude, decimal longitude, double radius, Guid countryId, string userId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentive;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentive = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByCountryWithLocationBranchHolderFocus](@latitude, @longitude, @radius, @countryId, @userId, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, countryIdParam, userIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentive = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentive;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByCountryWithLocationTenantFocus(decimal latitude, decimal longitude, double radius, Guid countryId, string userId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashbackIncentivesByCountryWithLocationTenantFocus](@latitude, @longitude, @radius, @countryId, @userId, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, countryIdParam, userIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByStateBranchHolderFocus(Guid stateId, Guid countryId, string userId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByStateBranchHolderFocus](@stateId, @countryId, @userId, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByStateForBranchHolder(Guid stateId, Guid countryId, string userId, Guid branchHolderId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var branchHolderIdParam = new SqlParameter(" branchHolderId", branchHolderId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByStateForBranchHolder](@stateId, @countryId, @userId, @branchHolderId, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, branchHolderIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByStateForTenant(Guid stateId, Guid countryId, string userId, Guid tenantId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByStateForTenant](@stateId, @countryId, @userId, @tenantId, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, tenantIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByStateTenantFocus(Guid stateId, Guid countryId, string userId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByStateTenantFocus](@stateId, @countryId, @userId, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByStateWithLocationBranchHolderFocus(decimal latitude, decimal longitude, double radius, Guid stateId, Guid countryId, string userId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentive;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var stateIdParam = new SqlParameter(" stateId", stateId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentive = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByStateWithLocationBranchHolderFocus](@latitude, @longitude, @radius, @stateId, @countryId, @userId, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, stateIdParam, countryIdParam, userIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentive = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentive;


        }

        public List<TempcashIncentivesDisplayContents> GetAvailableCashIncentivesByStateWithLocationTenantFocus(decimal latitude, decimal longitude, double radius, Guid stateId, Guid countryId, string userId, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var userIdParam = new SqlParameter("userId", userId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempcashIncentives = context.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableCashIncentivesByStateWithLocationTenantFocus](@latitude, @longitude, @radius, @stateId, @countryId, @userId, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, stateIdParam, countryIdParam, userIdParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByCountryBranchHolderFocus(Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByCountryBranchHolderFocus]( @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByCountryForBranchHolder(Guid countryId, string userId, Guid branchHolderId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var branchHolderIdParam = new SqlParameter("branchHolderId", branchHolderId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByCountryForBranchHolder]( @countryId, @userId, @branchHolderId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, branchHolderIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByCountryForPreference(Guid countryId, string userId, Guid preferenceId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var preferenceIdParam = new SqlParameter("preferenceId", preferenceId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByCountryForPreference]( @countryId, @userId, @preferenceId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, preferenceIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByCountryForTenant(Guid countryId, string userId, Guid tenantId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByCountryForTenant]( @countryId, @userId, @tenantId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, tenantIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByCountryPreferenceFocus(Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByCountryPreferenceFocus]( @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;

        }

        public List<TempofferDisplayContents> GetAvailableOffersByCountryTenantFocus(Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByCountryTenantFocus]( @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByCountryWithLocationBranchHolderFocus(decimal latitude, decimal longitude, double radius, Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByCountryWithLocationBranchHolderFocus](@latitude, @longitude, @radius, @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByCountryWithLocationPreferenceFocus(decimal latitude, decimal longitude, double radius, Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByCountryWithLocationPreferenceFocus](@latitude, @longitude, @radius, @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByCountryWithLocationTenantFocus(decimal latitude, decimal longitude, double radius, Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByCountryWithLocationTenantFocus](@latitude, @longitude, @radius, @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByStateBranchHolderFocus(Guid stateId, Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("stateId", stateId);
                var stateIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByStateBranchHolderFocus](@stateId, @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByStateForBranchHolder(Guid stateId, Guid countryId, string userId, Guid branchHolderId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("stateId", stateId);
                var stateIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var branchHolderIdParam = new SqlParameter("branchHolderId", branchHolderId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByStateForBranchHolder](@stateId, @countryId, @userId, @branchHolderId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, branchHolderIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByStateForPreference(Guid stateId, Guid countryId, string userId, Guid preferenceId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("stateId", stateId);
                var stateIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var preferenceIdParam = new SqlParameter("preferenceId", preferenceId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByStateForPreference](@stateId, @countryId, @userId, @preferenceId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, preferenceIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByStateForTenant(Guid stateId, Guid countryId, string userId, Guid tenantId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("stateId", stateId);
                var stateIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByStateForTenant](@stateId, @countryId, @userId, @tenantId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, tenantIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByStatePreferenceFocus(Guid stateId, Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("stateId", stateId);
                var stateIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByStatePreferenceFocus](@stateId, @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByStateTenantFocus(Guid stateId, Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("stateId", stateId);
                var stateIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByStateTenantFocus](@stateId, @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { stateIdParam, countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByStateWithLocationBranchHolderFocus(decimal latitude, decimal longitude, double radius, Guid stateId, Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("stateId", stateId);
                var stateIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByStateWithLocationBranchHolderFocus](@latitude, @longitude, @radius, @stateId, @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, stateIdParam, countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByStateWithLocationPreferenceFocus(decimal latitude, decimal longitude, double radius, Guid stateId, Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("stateId", stateId);
                var stateIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByStateWithLocationPreferenceFocus](@latitude, @longitude, @radius, @stateId, @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, stateIdParam, countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempofferDisplayContents> GetAvailableOffersByStateWithLocationTenantFocus(decimal latitude, decimal longitude, double radius, Guid stateId, Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("stateId", stateId);
                var stateIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableOffersByStateWithLocationTenantFocus](@latitude, @longitude, @radius, @stateId, @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, stateIdParam, countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TemprewardDetails> GetAvailableRewardsForUserByState(string userId, Guid stateId, int offerPurpose, int raffleUsageType, DateTime dateTime)
        {
            List<TemprewardDetails> temprewardDetails;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var raffleUsageTypeParam = new SqlParameter("raffleUsageType", raffleUsageType);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);

                //Processing
                temprewardDetails = dbContext.Set<TemprewardDetails>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableRewardsForUserByState](@userId, @stateId, @offerPurpose, @raffleUsageType, @dateTime)", new[] { userIdParam, stateIdParam, offerPurposeParam, raffleUsageTypeParam, dateTimeParam }).ToList();
            }
            catch (Exception e)
            {
                temprewardDetails = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temprewardDetails;


        }

        public List<TemprewardDetails> GetAvailableRewardsForUserByTenant(string userId, Guid stateId, Guid tenantId, int offerPurpose, int raffleUsageType, DateTime dateTime)
        {
            List<TemprewardDetails> temprewardDetails;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var raffleUsageTypeParam = new SqlParameter("raffleUsageType", raffleUsageType);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);

                //Processing
                temprewardDetails = dbContext.Set<TemprewardDetails>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableRewardsForUserByTenant](@userId, @stateId, @tenantId, @offerPurpose, @raffleUsageType, @dateTime)", new[] { userIdParam, stateIdParam, tenantIdParam, offerPurposeParam, raffleUsageTypeParam, dateTimeParam }).ToList();
            }
            catch (Exception e)
            {
                temprewardDetails = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temprewardDetails;


        }

        public List<Tempstates> GetAvailableStates()
        {
            List<Tempstates> tempstates;

            try
            {

                /* var userIdParam = new SqlParameter("userId", "iegiwfjiwfj");
                 var userAccParam = new SqlParameter("userAcc", 551215);

                 // Processing.  
                 tempstates = dbContext.Set<Tempstates>().FromSqlRaw("SELECT * FROM [dbo].[_TestFunction](@userAcc, @userId)", new[] { userAccParam, userIdParam }).ToList();
                */

                tempstates = dbContext.Set<Tempstates>().FromSqlRaw("SELECT * FROM [dbo].[GetAvailableStates]()").ToList();

            }
            catch (Exception e)
            {
                tempstates = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempstates;

        }


        public List<TempbranchHolderDisplayContents> GetBranchHoldersByCountry(Guid countryId, int pageSize, int pageNumber)
        {
            List<TempbranchHolderDisplayContents> tempbranchHolders;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                tempbranchHolders = context.Set<TempbranchHolderDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetBranchHoldersByCountry](@countryId, @pageSize, @pageNumber)", new[] { countryIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempbranchHolders = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempbranchHolders;
        }


        public List<TempbranchHolderDisplayContents> GetBranchHoldersByCountryAndLocation(double latitude, double longitude, double radius, Guid countryId, int pageSize, int pageNumber)
        {
            List<TempbranchHolderDisplayContents> tempbranchHolders;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                tempbranchHolders = context.Set<TempbranchHolderDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetBranchHoldersByCountryAndLocation](@latitude, @longitude, @radius, @countryId, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, countryIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempbranchHolders = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempbranchHolders;
        }

        public List<TempbranchHolderDisplayContents> GetBranchHoldersByState(Guid countryId, Guid stateId, int pageSize, int pageNumber)
        {
            List<TempbranchHolderDisplayContents> tempbranchHolders;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                tempbranchHolders = context.Set<TempbranchHolderDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetBranchHoldersByState](@countryId, @stateId, @pageSize, @pageNumber)", new[] { countryIdParam, stateIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempbranchHolders = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempbranchHolders;
        }

        public List<TempbranchHolderDisplayContents> GetBranchHoldersByStateAndLocation(double latitude, double longitude, double radius, Guid countryId, Guid stateId, int pageSize, int pageNumber)
        {
            List<TempbranchHolderDisplayContents> tempbranchHolders;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                tempbranchHolders = context.Set<TempbranchHolderDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetBranchHolderByStateAndLocation](@latitude, @longitude, @radius, @countryId, @stateId, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, countryIdParam, stateIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempbranchHolders = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempbranchHolders;
        }


        public List<TempbroadcastingOffersLogs> GetBroadcastingOfferLogsWithLocation(decimal latitude, decimal longitude, double radius, string userId, DateTime dateTime)
        {
            List<TempbroadcastingOffersLogs> offersLogs;

            try
            {
                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var userIdParam = new SqlParameter("userId", userId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);

                //Processing
                offersLogs = dbContext.Set<TempbroadcastingOffersLogs>().FromSqlRaw("SELECT * FROM [dbo].[GetBroadcastingOfferLogsWithLocation](@latitude, @longitude, @radius, @userId, @dateTime)", new[] { latitudeParam, longitudeParam, radiusParam, userIdParam, dateTimeParam }).ToList();
            }
            catch(Exception e)
            {
                offersLogs = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offersLogs;


        }

        public List<TempbroadcastingOffersLogs> GetBroadcastingOfferLogs(string userId, DateTime dateTime)
        {
            List<TempbroadcastingOffersLogs> offersLogs;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);

                //Processing
                offersLogs = dbContext.Set<TempbroadcastingOffersLogs>().FromSqlRaw("SELECT * FROM [dbo].[GetBroadcastingOfferLogs](@userId, @dateTime)", new[] { userIdParam, dateTimeParam }).ToList();
            }
            catch (Exception e)
            {
                offersLogs = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offersLogs;

        }

        public List<TempcashIncentivesDisplayContents> GetCashIncentiveDisplayContent(Guid incentiveId, string userId, DateTime dateTime)
        {
            List<TempcashIncentivesDisplayContents> tempcashIncentives;

            try
            {
                var incentiveIdParam = new SqlParameter("incentiveId", incentiveId);
                var userIdParam = new SqlParameter("userId", userId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);

                //Processing
                tempcashIncentives = dbContext.Set<TempcashIncentivesDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetCashIncentiveDisplayContent](@incentiveId, @userId, @dateTime)", new[] { incentiveIdParam, userIdParam, dateTimeParam }).ToList();
            }
            catch (Exception e)
            {
                tempcashIncentives = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempcashIncentives;


        }

        public List<Temppreferences> GetCategoryInterestForUser(string userId, Guid categoryId)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);
                var categoryIdParam = new SqlParameter("categoryId", categoryId);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetCategoryInterestForUser](@userId, @categoryId)", new[] { userIdParam, categoryIdParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }

        public List<Temppreferences> GetCategoryInterestsForUser(string userId)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetCategoryInterestForUser](@userId, @categoryId)", new[] { userIdParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }

        public List<Temppreferences> GetCategoryPreferencesForUser(string userId)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetCategoryPreferencesForUser](@userId)", new[] { userIdParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }

        public List<Temppreferences> GetCategoryPreferences(string userId)
        {
            List<Temppreferences> temppreferences;

            try
            {
                //An independen context is created beacuse this is part of a multithreading logic
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);

                temppreferences = context.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetCategoryPreferences](@userId)", new[] { userIdParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }

        public List<TempclaimableTransactions> GetClaimableTransactionsByStateTenantFocus(Guid stateId, Guid countryId, string userId, DateTime dateTime)
        {
            List<TempclaimableTransactions> tempclaimableTransactions;

            try
            {
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);

                tempclaimableTransactions = dbContext.Set<TempclaimableTransactions>().FromSqlRaw("SELECT * FROM [dbo].[GetClaimableTransactionsByStateTenantFocus](@stateId, @countryId, @userId, @dateTime)", new[] { stateIdParam, countryIdParam, userIdParam, dateTimeParam }).ToList();
            }
            catch (Exception e)
            {
                tempclaimableTransactions = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempclaimableTransactions;
        }

        public List<TempofferDisplayContents> GetOfferDisplayContent(Guid offerId, string userId, int offerPurpose, DateTime dateTime)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                var offerIdParam = new SqlParameter("offerId", offerId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);

                //Processing
                tempoffers = dbContext.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetOfferDisplayContent](@offerId, @userId, @offerPurpose, @dateTime)", new[] { offerIdParam, userIdParam, offerPurposeParam, dateTimeParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TemprewardOverviews> GetRewardDetails(Guid tenantId, Guid id)
        {
            List<TemprewardOverviews> temprewards;

            try
            {
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var idParam = new SqlParameter("id", id);

                temprewards = dbContext.Set<TemprewardOverviews>().FromSqlRaw("SELECT * FROM [dbo].[GetRewardDetails](@tenantId, @id)", new[] { tenantIdParam, idParam }).ToList();
            }
            catch (Exception e)
            {
                temprewards = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temprewards;
        }

        public List<TemprewardOverviews> GetRewardsByUsageTypeForTenantWithoutRedemptions(Guid tenantId, int offerPurpose, int raffleUsageType)
        {
            List<TemprewardOverviews> temprewards;

            try
            {
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var raffleUsageTypeParam = new SqlParameter("raffleUsageType", raffleUsageType);

                temprewards = dbContext.Set<TemprewardOverviews>().FromSqlRaw("SELECT * FROM [dbo].[GetRewardsByUsageTypeForTenantWithoutRedemptions](@tenantId, @offerPurpose, @raffleUsageType)", new[] { tenantIdParam, offerPurposeParam, raffleUsageTypeParam }).ToList();
            }
            catch (Exception e)
            {
                temprewards = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temprewards;
        }
        public List<TempofferDisplayContents> GetSavedOffersForUserByCountry(Guid countryId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetSavedOffersForUserByCountry]( @countryId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }
        public List<TempofferDisplayContents> GetSavedOffersForUserByState(Guid countryId, Guid stateId, string userId, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<TempofferDisplayContents> tempoffers;

            try
            {
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var countryIdParam = new SqlParameter("countryId", countryId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var userIdParam = new SqlParameter("userId", userId);
                var offerPurposeParam = new SqlParameter("offerPurpose", offerPurpose);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                //Processing
                tempoffers = context.Set<TempofferDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetSavedOffersForUserByState]( @countryId, @stateId, @userId, @offerPurpose, @dateTime, @pageSize, @pageNumber)", new[] { countryIdParam, stateIdParam, userIdParam, offerPurposeParam, dateTimeParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                tempoffers = null;
                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempoffers;


        }

        public List<TempsearchableLogs> GetSearchesByUser(DateTime startDate, DateTime endDate, string userId, Guid indexOwner)
        {
            List<TempsearchableLogs> tempsearchableLogs;

            try
            {
                var startDateParam = new SqlParameter("startDate", startDate);
                var endDateParam = new SqlParameter("endDate", endDate);
                var userIdParam = new SqlParameter("userId", userId);
                var indexOwnerParam = new SqlParameter("indexOwner", indexOwner);

                tempsearchableLogs = dbContext.Set<TempsearchableLogs>().FromSqlRaw("SELECT * FROM [dbo].[GetSearchesByUser](@startDate, @endDate, @userId, @indexOwner)", new[] { startDateParam, endDateParam, userIdParam, indexOwnerParam }).ToList();
            }
            catch (Exception e)
            {
                tempsearchableLogs = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempsearchableLogs;
        }

        public List<TempsearchableLogs> GetSearchesCount(DateTime startDate, DateTime endDate, Guid indexOwner, Guid countryId)
        {
            List<TempsearchableLogs> tempsearchableLogs;

            try
            {
                var startDateParam = new SqlParameter("startDate", startDate);
                var endDateParam = new SqlParameter("endDate", endDate);
                var indexOwnerParam = new SqlParameter("indexOwner", indexOwner);
                var countryIdParam = new SqlParameter("countryId", countryId);

                tempsearchableLogs = dbContext.Set<TempsearchableLogs>().FromSqlRaw("SELECT * FROM [dbo].[GetSearchesCount](@startDate, @endDate, @indexOwner, @countryId)", new[] { startDateParam, endDateParam, indexOwnerParam, countryIdParam }).ToList();
            }
            catch (Exception e)
            {
                tempsearchableLogs = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempsearchableLogs;
        }

        public List<Temppreferences> GetTenantInterestForUser(string userId, Guid tenantId)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);
                var tenantIdParam = new SqlParameter("tenantId", tenantId);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantInterestForUser](@userId, @tenantId)", new[] { userIdParam, tenantIdParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }

        public List<Temppreferences> GetTenantInterestsForUser(string userId)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantInterestsForUser](@userId)", new[] { userIdParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }

        public List<Temppreferences> GetTenantPreferencesByCountry(string userId, Guid countryId, int pageSize, int pageNumber)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantPreferencesByCountry](@userId, @countryId, @pageSize, @pageNumber)", new[] { userIdParam, countryIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }

        public List<Temppreferences> GetTenantPreferencesByCountryAndLocation(double latitude, double longitude, double radius, string userId, Guid countryId, int pageSize, int pageNumber)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantPreferencesByCountryAndLocation](@latitude, @longitude, @radius, @userId, @countryId, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, userIdParam, countryIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }


        public List<Temppreferences> GetTenantPreferencesByGeoLocation(double latitude, double longitude, double radius, Guid countryId, string userId, int pageSize, int pageNumber)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantPreferencesByGeoLocation](@latitude, @longitude, @radius, @countryId, @userId, @pageSize, @pageNumber)", new[] {latitudeParam, longitudeParam, radiusParam, countryIdParam, userIdParam, pageSizeParam, pageNumberParam}).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }

        public List<Temppreferences> GetTenantPreferencesByState(string userId, Guid countryId, Guid stateId, int pageSize, int pageNumber)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantPreferencesByState](@userId, @countryId, @stateId, @pageSize, @pageNumber)", new[] { userIdParam, countryIdParam, stateIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }

        public List<Temppreferences> GetTenantPreferencesByStateAndLocation(double latitude, double longitude, double radius, string userId, Guid countryId, Guid stateId, int pageSize, int pageNumber)
        {
            List<Temppreferences> temppreferences;

            try
            {
                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var userIdParam = new SqlParameter("userId", userId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temppreferences = dbContext.Set<Temppreferences>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantPreferencesByStateAndLocation](@latitude, @longitude, @radius, @userId, @countryId, @stateId, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, userIdParam, countryIdParam, stateIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                temppreferences = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temppreferences;
        }



        public List<TemptenantDisplayContents> GetTenantsForUserByCountry(string userId, Guid countryId, int pageSize, int pageNumber)
        {
            List<TemptenantDisplayContents> temptenants;

            try
            {
                //An independen context is created beacuse this is part of a multithreading logic
                yoyIj7qM58dCjContext context  = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temptenants = context.Set<TemptenantDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantsForUserByCountry](@userId, @countryId, @pageSize, @pageNumber)", new[] { userIdParam, countryIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                temptenants = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temptenants;
        }


        public List<TemptenantDisplayContents> GetTenantsForUserByCountryAndLocation(double latitude, double longitude, double radius, string userId, Guid countryId, int pageSize, int pageNumber)
        {
            List<TemptenantDisplayContents> temptenants;

            try
            {
                //An independen context is created beacuse this is part of a multithreading logic
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temptenants = context.Set<TemptenantDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantsForUserByCountryAndLocation](@latitude, @longitude, @radius, @userId, @countryId, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, userIdParam, countryIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                temptenants = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temptenants;
        }

        public List<TemptenantDisplayContents> GetTenantsForUserByGeoLocation(double latitude, double longitude, double radius, Guid countryId, string userId, int pageSize, int pageNumber)
        {
            List<TemptenantDisplayContents> temptenants;

            try
            {
                //An independen context is created beacuse this is part of a multithreading logic
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temptenants = context.Set<TemptenantDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantsForUserByGeoLocation](@latitude, @longitude, @radius, @countryId, @userId, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, countryIdParam, userIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                temptenants = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temptenants;
        }

        public List<TemptenantDisplayContents> GetTenantsForUserByState(string userId, Guid countryId, Guid stateId, int pageSize, int pageNumber)
        {
            List<TemptenantDisplayContents> temptenants;

            try
            {
                //An independen context is created beacuse this is part of a multithreading logic
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temptenants = context.Set<TemptenantDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantsForUserByState](@userId, @countryId, @stateId, @pageSize, @pageNumber)", new[] { userIdParam, countryIdParam, stateIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                temptenants = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temptenants;
        }

        public List<TemptenantDisplayContents> GetTenantsForUserByStateAndLocation(double latitude, double longitude, double radius, string userId, Guid countryId, Guid stateId, int pageSize, int pageNumber)
        {
            List<TemptenantDisplayContents> temptenants;

            try
            {
                //An independen context is created beacuse this is part of a multithreading logic
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var latitudeParam = new SqlParameter("latitude", latitude);
                var longitudeParam = new SqlParameter("longitude", longitude);
                var radiusParam = new SqlParameter("radius", radius);
                var userIdParam = new SqlParameter("userId", userId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var stateIdParam = new SqlParameter("stateId", stateId);
                var pageSizeParam = new SqlParameter("pageSize", pageSize);
                var pageNumberParam = new SqlParameter("pageNumber", pageNumber);

                temptenants = context.Set<TemptenantDisplayContents>().FromSqlRaw("SELECT * FROM [dbo].[GetTenantsForUserByStateAndLocation](@latitude, @longitude, @radius, @userId, @countryId, @stateId, @pageSize, @pageNumber)", new[] { latitudeParam, longitudeParam, radiusParam, userIdParam, countryIdParam, stateIdParam, pageSizeParam, pageNumberParam }).ToList();
            }
            catch (Exception e)
            {
                temptenants = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return temptenants;
        }

        public List<TempclaimableTransactions> GetTransactionDetailsWithLocations(Guid transactionId, string userId)
        {
            List<TempclaimableTransactions> tempclaimableTransactions;

            try
            {
                var transactionIdParam = new SqlParameter("transactionId", transactionId);
                var userIdParam = new SqlParameter("userId", userId);

                tempclaimableTransactions = dbContext.Set<TempclaimableTransactions>().FromSqlRaw("SELECT * FROM [dbo].[GetTransactionDetailsWithLocations](@transactionId, @userId)", new[] { transactionIdParam, userIdParam }).ToList();
            }
            catch (Exception e)
            {
                tempclaimableTransactions = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempclaimableTransactions;
        }

        public List<TempuserInteractionTimeMetrics> GetUserInteractionMetricsTotalTimeByReferences(string userId, DateTime startDate, DateTime endDate)
        {
            List<TempuserInteractionTimeMetrics> tempuserInteractionTimeMetrics;

            try
            {
                //An independen context is created beacuse this is part of a multithreading logic
                yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();

                var userIdParam = new SqlParameter("userId", userId);
                var startDateParam = new SqlParameter("startDate", startDate);
                var endDateParam = new SqlParameter("endDate", endDate);

                tempuserInteractionTimeMetrics = context.Set<TempuserInteractionTimeMetrics>().FromSqlRaw("SELECT * FROM [dbo].[GetUserInteractionMetricsTotalTimeByReference](@userId, @startDate, @endDate)", new[] { userIdParam, startDateParam, endDateParam }).ToList();
            }
            catch (Exception e)
            {
                tempuserInteractionTimeMetrics = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempuserInteractionTimeMetrics;
        }


        public List<TempmembershipDetails> GetUserMembershipForTenant(Guid tenantId, string userId)
        {
            List<TempmembershipDetails> tempmemberships;

            try
            {
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var userIdParam = new SqlParameter("userId", userId);

                tempmemberships = dbContext.Set<TempmembershipDetails>().FromSqlRaw("SELECT * FROM [dbo].[GetUserMembershipForTenant](@tenantId, @userId)", new[] { tenantIdParam, userIdParam }).ToList();
            }
            catch (Exception e)
            {
                tempmemberships = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempmemberships;
        }


        public List<TempmembershipPointOps> GetUserMembershipPointsByState(Guid stateId, Guid countryId, string userId)
        {
            List<TempmembershipPointOps> tempmembershipPoints;

            try
            {
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);

                tempmembershipPoints = dbContext.Set<TempmembershipPointOps>().FromSqlRaw("SELECT * FROM [dbo].[GetUserMembershipPointsByState](@stateId, @countryId, @userId)", new[] { stateIdParam, countryIdParam, userIdParam }).ToList();
            }
            catch (Exception e)
            {
                tempmembershipPoints = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempmembershipPoints;
        }


        public List<TempmembershipPointOps> GetUserMembershipPointsForTenant(Guid tenantId, string userId, DateTime lastLevelEvaluationDate, DateTime dateTime)
        {
            List<TempmembershipPointOps> tempmembershipPoints;

            try
            {
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var userIdParam = new SqlParameter("userId", userId);
                var lastLevelEvaluationDateParam = new SqlParameter("lastLevelEvaluationDate", lastLevelEvaluationDate);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);


                tempmembershipPoints = dbContext.Set<TempmembershipPointOps>().FromSqlRaw("SELECT * FROM [dbo].[GetUserMembershipPointsForTenant](@tenantId, @userId, @lastLevelEvaluationDate, @dateTime)", new[] { tenantIdParam, userIdParam, lastLevelEvaluationDateParam, dateTimeParam }).ToList();
            }
            catch (Exception e)
            {
                tempmembershipPoints = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempmembershipPoints;
        }


        public List<TempmembershipDetails> GetUserMembershipsByState(Guid stateId, Guid countryId, string userId)
        {
            List<TempmembershipDetails> tempmemberships;

            try
            {
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);

                tempmemberships = dbContext.Set<TempmembershipDetails>().FromSqlRaw("SELECT * FROM [dbo].[GetUserMembershipsByState](@stateId, @countryId, @userId)", new[] { stateIdParam, countryIdParam, userIdParam }).ToList();
            }
            catch (Exception e)
            {
                tempmemberships = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tempmemberships;
        }


        public List<OltpvalidatePurchaseRegistries> GetUserValidPurchaseRegistriesForTenant(Guid tenantId, string userId, DateTime lastLevelEvaluationDate, DateTime dateTime)
        {
            List<OltpvalidatePurchaseRegistries> purchaseRegistries;

            try
            {
                var tenantIdParam = new SqlParameter("tenantId", tenantId);
                var userIdParam = new SqlParameter("userId", userId);
                var lastLevelEvaluationDateParam = new SqlParameter("lastLevelEvaluationDate", lastLevelEvaluationDate);
                var dateTimeParam = new SqlParameter("dateTime", dateTime);

                purchaseRegistries = dbContext.Set<OltpvalidatePurchaseRegistries>().FromSqlRaw("SELECT * FROM [dbo].[GetUserValidPurchaseRegistriesForTenant](@tenantId, @userId, @lastLevelEvaluationDate, @dateTime)", new[] { tenantIdParam, userIdParam, lastLevelEvaluationDateParam, dateTimeParam }).ToList();
            }
            catch (Exception e)
            {
                purchaseRegistries = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchaseRegistries;
        }


        public List<OltpvalidatePurchaseRegistries> GetUserValidPurchasesRegistiriesByState(Guid stateId, Guid countryId, string userId)
        {
            List<OltpvalidatePurchaseRegistries> purchaseRegistries;

            try
            {
                var stateIdParam = new SqlParameter("stateId", stateId);
                var countryIdParam = new SqlParameter("countryId", countryId);
                var userIdParam = new SqlParameter("userId", userId);

                purchaseRegistries = dbContext.Set<OltpvalidatePurchaseRegistries>().FromSqlRaw("SELECT * FROM [dbo].[GetUserValidPurchasesRegistiriesByState](@stateId, @countryId, @userId)", new[] { stateIdParam, countryIdParam, userIdParam }).ToList();
            }
            catch (Exception e)
            {
                purchaseRegistries = null;

                //ERROR HANDLING
                AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchaseRegistries;
        }

        #endregion

        #region CONSTRUCTORS

        public FunctionsHandler(yoyIj7qM58dCjContext context)
        {
            this.dbContext = context;
        }

        #endregion
    }
}
