using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;

namespace YOY.DAO.Entities.Manager
{
    public class PaymentMethodManager
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

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS METHODS                                                                                                                                  //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        #region METHODS

        public List<PaymentMethod> Gets()
        {
            List<PaymentMethod> payments = new List<PaymentMethod>();

            try
            {
                var query = from x in this._businessObjects.Context.DefpaymentMethods
                            select x;

                PaymentMethod method = null;
                foreach (DefpaymentMethods item in query)
                {
                    method = new PaymentMethod
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        AllowProgrammedOrders = item.AllowProgrammedOrders,
                        PaymentBeforeShipping = (bool)item.PaymentBeforeShipping,
                        RequiresCall = item.RequieresCall,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        OnlyOnline = item.OnlyOnline,
                        IconName = item.IconName
                    };

                    payments.Add(method);
                }
            }
            catch (Exception e)
            {
                payments = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return payments;
        }

        public List<PaymentMethod> Gets(int allowProgrammedOrderType, int onlyOnlineType)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

            try
            {
                var query = (dynamic)null;

                switch (allowProgrammedOrderType)
                {
                    case ProcessingOrderTypes.All:
                        switch (onlyOnlineType)
                        {
                            case OnlyOnlineTypes.All:
                                query = from x in this._businessObjects.Context.DefpaymentMethods
                                        select x;
                                break;
                            case OnlyOnlineTypes.NotOnlineLimited:
                                query = from x in this._businessObjects.Context.DefpaymentMethods
                                        where !x.OnlyOnline
                                        select x;
                                break;
                            case OnlyOnlineTypes.OnlineExclusively:
                                query = from x in this._businessObjects.Context.DefpaymentMethods
                                        where x.OnlyOnline
                                        select x;
                                break;
                        }
                        break;
                    case ProcessingOrderTypes.Instantly:
                        switch (onlyOnlineType)
                        {
                            case OnlyOnlineTypes.All:
                                query = from x in this._businessObjects.Context.DefpaymentMethods
                                        where !x.AllowProgrammedOrders
                                        select x;
                                break;
                            case OnlyOnlineTypes.NotOnlineLimited:
                                query = from x in this._businessObjects.Context.DefpaymentMethods
                                        where !x.OnlyOnline && !x.AllowProgrammedOrders
                                        select x;
                                break;
                            case OnlyOnlineTypes.OnlineExclusively:
                                query = from x in this._businessObjects.Context.DefpaymentMethods
                                        where x.OnlyOnline && !x.AllowProgrammedOrders
                                        select x;
                                break;
                        }
                        break;
                    case ProcessingOrderTypes.Scheduled:
                        switch (onlyOnlineType)
                        {
                            case OnlyOnlineTypes.All:
                                query = from x in this._businessObjects.Context.DefpaymentMethods
                                        where x.AllowProgrammedOrders
                                        select x;
                                break;
                            case OnlyOnlineTypes.NotOnlineLimited:
                                query = from x in this._businessObjects.Context.DefpaymentMethods
                                        where !x.OnlyOnline && x.AllowProgrammedOrders
                                        select x;
                                break;
                            case OnlyOnlineTypes.OnlineExclusively:
                                query = from x in this._businessObjects.Context.DefpaymentMethods
                                        where x.OnlyOnline && x.AllowProgrammedOrders
                                        select x;
                                break;
                        }
                        break;
                }

                if (query != null)
                {
                    PaymentMethod paymentMethod = null;
                    foreach (DefpaymentMethods item in query)
                    {
                        paymentMethod = new PaymentMethod
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            AllowProgrammedOrders = item.AllowProgrammedOrders,
                            PaymentBeforeShipping = (bool)item.PaymentBeforeShipping,
                            RequiresCall = item.RequieresCall,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            OnlyOnline = item.OnlyOnline,
                            IconName = item.IconName
                        };

                        paymentMethods.Add(paymentMethod);
                    }
                }

            }
            catch (Exception e)
            {
                paymentMethods = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentMethods;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        public List<PaymentMethod> Gets(Guid branchId, int activeState, int allowProgrammedOrderType, int onlyOnlineType)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (allowProgrammedOrderType)
                        {
                            case ProcessingOrderTypes.All:
                                switch (onlyOnlineType)
                                {
                                    case OnlyOnlineTypes.All:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.NotOnlineLimited:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.OnlyOnline && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.OnlineExclusively:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.OnlyOnline && x.BranchId == branchId
                                                select x;
                                        break;
                                }
                                break;
                            case ProcessingOrderTypes.Instantly:
                                switch (onlyOnlineType)
                                {
                                    case OnlyOnlineTypes.All:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.AllowProgrammedOrders && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.NotOnlineLimited:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.OnlyOnline && !x.AllowProgrammedOrders && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.OnlineExclusively:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.OnlyOnline && !x.AllowProgrammedOrders && x.BranchId == branchId
                                                select x;
                                        break;
                                }
                                break;
                            case ProcessingOrderTypes.Scheduled:
                                switch (onlyOnlineType)
                                {
                                    case OnlyOnlineTypes.All:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.AllowProgrammedOrders && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.NotOnlineLimited:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.OnlyOnline && x.AllowProgrammedOrders && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.OnlineExclusively:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.OnlyOnline && x.AllowProgrammedOrders && x.BranchId == branchId
                                                select x;
                                        break;
                                }
                                break;
                        }
                        break;
                    case ActiveStates.Active:
                        switch (allowProgrammedOrderType)
                        {
                            case ProcessingOrderTypes.All:
                                switch (onlyOnlineType)
                                {
                                    case OnlyOnlineTypes.All:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.NotOnlineLimited:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.OnlyOnline && x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.OnlineExclusively:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.OnlyOnline && x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                }
                                break;
                            case ProcessingOrderTypes.Instantly:
                                switch (onlyOnlineType)
                                {
                                    case OnlyOnlineTypes.All:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.AllowProgrammedOrders && x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.NotOnlineLimited:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.OnlyOnline && !x.AllowProgrammedOrders && x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.OnlineExclusively:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.OnlyOnline && !x.AllowProgrammedOrders && x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                }
                                break;
                            case ProcessingOrderTypes.Scheduled:
                                switch (onlyOnlineType)
                                {
                                    case OnlyOnlineTypes.All:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.AllowProgrammedOrders && x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.NotOnlineLimited:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.OnlyOnline && x.AllowProgrammedOrders && x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.OnlineExclusively:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.OnlyOnline && x.AllowProgrammedOrders && x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                }
                                break;
                        }
                        break;
                    case ActiveStates.Inactive:
                        switch (allowProgrammedOrderType)
                        {
                            case ProcessingOrderTypes.All:
                                switch (onlyOnlineType)
                                {
                                    case OnlyOnlineTypes.All:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.NotOnlineLimited:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.OnlyOnline && !x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.OnlineExclusively:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.OnlyOnline && !x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                }
                                break;
                            case ProcessingOrderTypes.Instantly:
                                switch (onlyOnlineType)
                                {
                                    case OnlyOnlineTypes.All:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.AllowProgrammedOrders && !x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.NotOnlineLimited:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.OnlyOnline && !x.AllowProgrammedOrders && !x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.OnlineExclusively:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.OnlyOnline && !x.AllowProgrammedOrders && !x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                }
                                break;
                            case ProcessingOrderTypes.Scheduled:
                                switch (onlyOnlineType)
                                {
                                    case OnlyOnlineTypes.All:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.AllowProgrammedOrders && !x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.NotOnlineLimited:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where !x.OnlyOnline && x.AllowProgrammedOrders && !x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                    case OnlyOnlineTypes.OnlineExclusively:
                                        query = from x in this._businessObjects.Context.DefpaymentMethodsView
                                                where x.OnlyOnline && x.AllowProgrammedOrders && !x.IsActive && x.BranchId == branchId
                                                select x;
                                        break;
                                }
                                break;
                        }
                        break;
                }

                

                if (query != null)
                {

                    PaymentMethod paymentMethod = null;
                    foreach (DefpaymentMethodsView item in query)
                    {

                        paymentMethod = new PaymentMethod
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            AllowProgrammedOrders = item.AllowProgrammedOrders,
                            PaymentBeforeShipping = item.PaymentBeforeShipping,
                            RequiresCall = item.RequieresCall,
                            OnlyOnline = item.OnlyOnline,
                            IconName = item.IconName,
                            BranchId = item.BranchId,
                            IsActive = item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };


                        paymentMethods.Add(paymentMethod);

                    }
                }

            }
            catch (Exception e)
            {
                paymentMethods = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentMethods;

        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves a payment method
        /// </summary>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public PaymentMethod Get(Guid methodId)
        {
            PaymentMethod method = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefpaymentMethods
                            where x.Id == methodId
                            select x;

                foreach (DefpaymentMethods item in query)
                {
                    method = new PaymentMethod
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        AllowProgrammedOrders = item.AllowProgrammedOrders,
                        PaymentBeforeShipping = (bool)item.PaymentBeforeShipping,
                        RequiresCall = item.RequieresCall,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        OnlyOnline = item.OnlyOnline,
                        IconName = item.IconName
                    };
                }
            }
            catch (Exception e)
            {
                method = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return method;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Retrieves a payment method
        /// </summary>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public PaymentMethod Get(Guid methodId, Guid branchId)
        {
            PaymentMethod method = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefpaymentMethodsView
                            where x.Id == methodId && x.BranchId == branchId
                            select x;

                foreach (DefpaymentMethodsView item in query)
                {
                    method = new PaymentMethod
                    {
                        Id = item.Id,
                        BranchId = item.BranchId,
                        Name = item.Name,
                        Description = item.Description,
                        AllowProgrammedOrders = item.AllowProgrammedOrders,
                        PaymentBeforeShipping = item.PaymentBeforeShipping,
                        RequiresCall = item.RequieresCall,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        OnlyOnline = item.OnlyOnline,
                        IsActive = item.IsActive
                    };
                }
            }
            catch (Exception e)
            {
                method = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return method;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Creates a new payment method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="allowProgrammedOrders"></param>
        /// <param name="paymentBeforeShipping"></param>
        /// <param name="requiresCall"></param>
        /// <param name="onlyOnline"></param>
        /// <param name="iconName"></param>
        /// <returns></returns>
        public PaymentMethod Post(string name, string description, bool allowProgrammedOrders,
            bool paymentBeforeShipping, bool requiresCall, bool onlyOnline, string iconName)
        {
            DefpaymentMethods newMethod = null;
            PaymentMethod method;
            try
            {
                newMethod = new DefpaymentMethods
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    OnlyOnline = onlyOnline,
                    RequieresCall = requiresCall,
                    AllowProgrammedOrders = allowProgrammedOrders,
                    PaymentBeforeShipping = paymentBeforeShipping,
                    IconName = iconName
                };

                this._businessObjects.Context.DefpaymentMethods.Add(newMethod);
                this._businessObjects.Context.SaveChanges();

                method = new PaymentMethod
                {
                    Id = newMethod.Id,
                    Name = newMethod.Name,
                    Description = newMethod.Description,
                    AllowProgrammedOrders = newMethod.AllowProgrammedOrders,
                    PaymentBeforeShipping = (bool)newMethod.PaymentBeforeShipping,
                    RequiresCall = newMethod.RequieresCall,
                    CreatedDate = newMethod.CreatedDate,
                    UpdatedDate = newMethod.UpdatedDate,
                    OnlyOnline = newMethod.OnlyOnline,
                    IconName = newMethod.IconName
                };
            }
            catch (Exception e)
            {
                this._businessObjects.Context.DefpaymentMethods.Remove(newMethod);
                this._businessObjects.Context.SaveChanges();

                method = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return method;
        }//POST ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Creates a new payment method branch relation
        /// </summary>
        /// <param name="methodId"></param>
        /// <param name="branchId"></param>
        /// <param name="bankFee"></param>
        /// <returns></returns>
        public PaymentMethod Post(Guid methodId, Guid branchId)
        {
           PaymentMethod method = null;

            DefbranchPaymentMethods newMethod = null;
            try
            {
                newMethod = new DefbranchPaymentMethods
                {
                    BranchId = branchId,
                    MethodId = methodId,
                    IsActive = true,//On creation always is active
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.DefbranchPaymentMethods.Add(newMethod);
                this._businessObjects.Context.SaveChanges();

                DefpaymentMethodsView newMethodView = (from x in this._businessObjects.Context.DefpaymentMethodsView
                                                       where x.Id == newMethod.MethodId && x.BranchId == newMethod.BranchId
                                                       select x).FirstOrDefault();

                if(newMethodView != null)
                {

                    method = new PaymentMethod
                    {
                        Id = newMethodView.Id,
                        BranchId = newMethodView.BranchId,
                        Name = newMethodView.Name,
                        Description = newMethodView.Description,
                        AllowProgrammedOrders = newMethodView.AllowProgrammedOrders,
                        PaymentBeforeShipping = newMethodView.PaymentBeforeShipping,
                        RequiresCall = newMethodView.RequieresCall,
                        CreatedDate = newMethodView.CreatedDate,
                        UpdatedDate = newMethodView.UpdatedDate,
                        OnlyOnline = newMethodView.OnlyOnline,
                        IsActive = newMethodView.IsActive
                    };
                }
            }
            catch (Exception e)
            {
                this._businessObjects.Context.DefbranchPaymentMethods.Remove(newMethod);
                this._businessObjects.Context.SaveChanges();

                method = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return method;
        }//POST ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Updates a payment method
        /// </summary>
        /// <param name="methodId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="allowProgrammedOrders"></param>
        /// <param name="paymentBeforeShipping"></param>
        /// <param name="requiresCall"></param>
        /// <param name="onlyOnline"></param>
        /// <returns></returns>
        public PaymentMethod Put(Guid methodId, string name, string description, bool allowProgrammedOrders,
            bool paymentBeforeShipping, bool requiresCall, bool onlyOnline)
        {
            PaymentMethod currentMethod = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefpaymentMethods
                            where x.Id == methodId
                            select x;

                DefpaymentMethods method = null;
                foreach (DefpaymentMethods item in query)
                {
                    method = item;
                }

                if (method != null)
                {
                    method.Name = name;
                    method.Description = description;
                    method.AllowProgrammedOrders = allowProgrammedOrders;
                    method.PaymentBeforeShipping = paymentBeforeShipping;
                    method.RequieresCall = requiresCall;
                    method.OnlyOnline = onlyOnline;
                    method.UpdatedDate = DateTime.UtcNow;


                    this._businessObjects.Context.SaveChanges();

                    currentMethod = new PaymentMethod
                    {
                        Id = method.Id,
                        Name = method.Name,
                        Description = method.Description,
                        AllowProgrammedOrders = method.AllowProgrammedOrders,
                        PaymentBeforeShipping = (bool)method.PaymentBeforeShipping,
                        RequiresCall = method.RequieresCall,
                        CreatedDate = method.CreatedDate,
                        UpdatedDate = method.UpdatedDate,
                        OnlyOnline = method.OnlyOnline
                    };
                }
            }
            catch (Exception e)
            {
                currentMethod = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return currentMethod;
        }//PUT ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Changes payment method active state for a branch
        /// </summary>
        /// <returns></returns>
        public bool Put(Guid methodId, Guid brachId)
        {
            bool success = false;

            try
            {
                DefbranchPaymentMethods method = null;

                var query = from x in this._businessObjects.Context.DefbranchPaymentMethods
                            where x.MethodId == methodId && x.BranchId == brachId
                            select x;

                foreach (var item in query)
                {
                    method = item;
                }

                if (method != null)
                {
                    method.IsActive = !method.IsActive;
                    method.UpdatedDate = DateTime.UtcNow;

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



        public bool Delete(Guid methodId)
        {
            bool success = false;

            try
            {
                DefpaymentMethods method = null;

                var query = from x in this._businessObjects.Context.DefpaymentMethods
                            where x.Id == methodId
                            select x;

                foreach (var item in query)
                {
                    method = item;
                }

                if (method != null)
                {

                    this._businessObjects.Context.DefpaymentMethods.Remove(method);
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
        }//DELETE ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        public bool Delete(Guid methodId, Guid branchId)
        {
            bool success = false;

            try
            {
                DefbranchPaymentMethods method = null;

                var query = from x in this._businessObjects.Context.DefbranchPaymentMethods
                            where x.MethodId == methodId && x.BranchId == branchId
                            select x;

                foreach (DefbranchPaymentMethods item in query)
                {
                    method = item;
                }

                if (method != null)
                {

                    this._businessObjects.Context.DefbranchPaymentMethods.Remove(method);
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
        }//DELETE ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new PaymentMethodManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public PaymentMethodManager(BusinessObjects businessObjects)
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
