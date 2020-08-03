using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DAO.Entities
{
    public class Tenant
    {
        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // CLASS PRIVATE PROPERTIES                                                                                                                               //
        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //

        // The unique Id of the tenant used to reference all the tenant context
        private readonly Guid _tenantId;

        // The unique Id of the tenant category used to reference all the tenant context
        private readonly Guid _tenantCategoryId;

        // The unique Id of the currency symbol used to reference all prices in the context
        private readonly string _currencySymbol;

        // The unique Id of the UTC timediff the tenant has
        private readonly int _utcTimeDiff;

        // The unique Id of the fee percentage the tenant has
        private readonly double _feePercentage;

        // Instance of Business Objects DAO
        private BusinessObjects _businessObjs;

        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // CLASS PUBLIC PROPERTIES                                                                                                                                //
        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //
        /// <summary>
        /// 
        /// </summary>
        public Guid TenantId
        {
            get { return this._tenantId; } // METHOD GET ENDS
        } // METHOD TENANT ID ----------------------------------------------------------------------------------------------------------------------------------- //

        public Guid TenantCategoryId
        {
            get { return this._tenantCategoryId; } // METHOD GET ENDS
        } // METHOD TENANT ID ----------------------------------------------------------------------------------------------------------------------------------- //

        public string CurrencySymbol
        {
            get { return this._currencySymbol; } // METHOD GET ENDS
        } // METHOD TENANT ID ----------------------------------------------------------------------------------------------------------------------------------- //

        public int UtcTimeDiff
        {
            get { return this._utcTimeDiff; } // METHOD GET ENDS
        } // METHOD TENANT ID ----------------------------------------------------------------------------------------------------------------------------------- //

        public double FeePercentage
        {
            get { return this._feePercentage; } // METHOD GET ENDS
        } // METHOD FEE PERCENTAGE ----------------------------------------------------------------------------------------------------------------------------------- //


        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // BUSINESS OBJETC                                                                                                                                        //
        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //
        /// <summary>
        ///     Gets the reference to the business objects DAO for the tenant using dynamic loader
        /// </summary>
        public BusinessObjects BusinessObject
        {
            // Property Dynamic Loader
            get { return this._businessObjs ?? (this._businessObjs = BusinessObjects.GetInstance(this)); } // GET ENDS
        } // PROPERTY BUSINESS OBJECT ENDS ---------------------------------------------------------------------------------------------------------------------- //

        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // CONSTRUCTOR MEHTOD                                                                                                                                     //
        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //
        /// <summary>
        /// Creates instances of a tenant based on its id.
        /// </summary>
        /// <param name="id"></param>
        protected Tenant(Guid id, Guid categoryId, string currencySymbol, int timeDiff, double feePercentage)
        {
            this._tenantId = id;
            this._tenantCategoryId = categoryId;
            this._currencySymbol = currencySymbol;
            this._utcTimeDiff = timeDiff;
            this._feePercentage = feePercentage;
        } // CONSTRUCTOR MEHTOD ENDS ---------------------------------------------------------------------------------------------------------------------------- //


        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //
        // MEMTHOD GET INSTANCE                                                                                                                                   //
        // ------------------------------------------------------------------------------------------------------------------------------------------------------ //
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Tenant GetInstance(Guid tenantId, Guid categoryId, string currencySymbol, int timeDiff, double feePercentage)
        {
            return new Tenant(tenantId, categoryId, currencySymbol, timeDiff, feePercentage);
        } // METHOD GET INSTANCE ENDS --------------------------------------------------------------------------------------------------------------------------- //


        public static Tenant GetInstance(Guid tenantId)
        {
            return new Tenant(tenantId, Guid.Empty, "", int.MinValue, 0);
        } // METHOD GET INSTANCE ENDS --------------------------------------------------------------------------------------------------------------------------- //


    } // CLASS TENANT ENDS -------------------------------------------------------------------------------------------------------------------------------------- //
}
