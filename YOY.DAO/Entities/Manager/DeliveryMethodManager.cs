using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;

namespace YOY.DAO.Entities.Manager
{
    public class DeliveryMethodManager
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

        /// <summary>
        /// Retrieves all delivery method
        /// </summary>
        /// <returns></returns>
        public List<DeliveryMethod> Gets()
        {
            List<DeliveryMethod> deliveryMethods = new List<DeliveryMethod>();

            try
            {
                var query = from x in this._businessObjects.Context.DefdeliveryMethods
                            select x;


                if (query != null)
                {
                    DeliveryMethod currentDeliveryMethod = null;
                    foreach (DefdeliveryMethods item in query)
                    {
                        currentDeliveryMethod = new DeliveryMethod
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            IconName = item.IconName,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        deliveryMethods.Add(currentDeliveryMethod);
                    }
                }
                else
                {
                    deliveryMethods = null;
                }

            }
            catch (Exception e)
            {
                deliveryMethods = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return deliveryMethods;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves all branch's delivery methods
        /// </summary>
        /// <param name="activeState"></param>
        /// <returns></returns>
        /// <summary>
        /// Retrieves all the methos related to a Branch
        /// according to its active state
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public List<DeliveryMethod> Gets(Guid branchId, int activeState)
        {
            List<DeliveryMethod> deliveryMethods = new List<DeliveryMethod>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = from x in this._businessObjects.Context.DefdeliveryMethodsView
                                where x.BranchId == branchId
                                select x;
                        break;
                    case ActiveStates.Active:
                        query = from x in this._businessObjects.Context.DefdeliveryMethodsView
                                where x.IsActive && x.BranchId == branchId
                                select x;
                        break;
                    case ActiveStates.Inactive:
                        query = from x in this._businessObjects.Context.DefdeliveryMethodsView
                                where !x.IsActive && x.BranchId == branchId
                                select x;
                        break;
                }

                if (query != null)
                {
                    DeliveryMethod currentDeliveryMethod = null;
                    foreach (DefdeliveryMethodsView item in query)
                    {
                        currentDeliveryMethod = new DeliveryMethod
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            IconName = item.IconName,
                            BranchId = item.BranchId,
                            IsActive = item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            FixedPrice = item.FixedPrice,
                            MaxItemsPerDelivery = item.MaxItemsPerDelivery,
                            UnitPrice = item.UnitPrice,
                            DistanceRange = item.UnitDistance
                        };

                        deliveryMethods.Add(currentDeliveryMethod);
                    }
                }
                else
                {
                    deliveryMethods = null;
                }

            }
            catch (Exception e)
            {
                deliveryMethods = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return deliveryMethods;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieves a delivery method
        /// </summary>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public DeliveryMethod Get(Guid methodId)
        {
            DeliveryMethod method = null;


            try
            {
                var query = from x in this._businessObjects.Context.DefdeliveryMethods
                            where x.Id == methodId
                            select x;

                foreach (var item in query)
                {
                    method = new DeliveryMethod
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        IconName = item.IconName,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                }
            }
            catch (Exception)
            {
                method = null;
                //TODO ERROR HANDLING
            }

            return method;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Retrieves all branch's delivery method
        /// </summary>
        /// <param name="activeState"></param>
        /// <returns></returns>
        /// <summary>
        /// Retrieves a metho related to a Branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public DeliveryMethod Get(Guid branchId, Guid methodId)
        {
            DeliveryMethod currentDeliveryMethod = new DeliveryMethod();

            try
            {
                var query = from x in this._businessObjects.Context.DefdeliveryMethodsView
                            where x.BranchId == branchId && x.Id == methodId
                            select x;

                if (query != null)
                {
                    foreach (DefdeliveryMethodsView item in query)
                    {
                        currentDeliveryMethod = new DeliveryMethod
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            IconName = item.IconName,
                            BranchId = item.BranchId,
                            IsActive = item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            FixedPrice = item.FixedPrice,
                            MaxItemsPerDelivery = item.MaxItemsPerDelivery,
                            UnitPrice = item.UnitPrice,
                            DistanceRange = item.UnitDistance
                        };

                    }
                }
                else
                {
                    currentDeliveryMethod = null;
                }

            }
            catch (Exception e)
            {
                currentDeliveryMethod = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return currentDeliveryMethod;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Creates a new delivery method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="unitPrice"></param>
        /// <param name="fixedPrice"></param>
        /// <param name="maxItemsPerDelivery"></param>
        /// <returns></returns>
        public DeliveryMethod Post(string name, string description, string iconName)
        {
            DefdeliveryMethods deliveryMethod = null;

            DeliveryMethod newMethod;
            try
            {
                deliveryMethod = new DefdeliveryMethods
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description,
                    IconName = iconName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.DefdeliveryMethods.Add(deliveryMethod);
                this._businessObjects.Context.SaveChanges();

                newMethod = new DeliveryMethod
                {
                    Id = deliveryMethod.Id,
                    Name = deliveryMethod.Name,
                    Description = deliveryMethod.Description,
                    IconName = deliveryMethod.IconName,
                    CreatedDate = deliveryMethod.CreatedDate,
                    UpdatedDate = deliveryMethod.UpdatedDate
                };

            }
            catch (Exception e)
            {
                this._businessObjects.Context.DefdeliveryMethods.Remove(deliveryMethod);
                this._businessObjects.Context.SaveChanges();

                newMethod = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return newMethod;
        }//POST ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Creates a new delivery method branch relation
        /// </summary>
        /// <param name="methodId"></param>
        /// <param name="branchId"></param>
        /// <param name="unitPrice"></param>
        /// <param name="maxItemsPerDelivery"></param>
        /// <param name="unitDistance"></param>
        /// <param name="fixedPrice"></param>
        /// <returns></returns>
        public DeliveryMethod Post(Guid methodId, Guid branchId, decimal unitPrice, int maxItemsPerDelivery, float unitDistance, bool fixedPrice)
        {
            DefbranchDeliveryMethods relation = null;

            DeliveryMethod newMethod = null;
            try
            {
                relation = new DefbranchDeliveryMethods
                {
                    MethodId = methodId,
                    BranchId = branchId,
                    UnitPrice = unitPrice,
                    MaxItemsPerDelivery = maxItemsPerDelivery,
                    FixedPrice = fixedPrice,
                    UnitDistance = unitDistance,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true//On creation always is active
                };

                this._businessObjects.Context.DefbranchDeliveryMethods.Add(relation);
                this._businessObjects.Context.SaveChanges();

                DefdeliveryMethodsView relationView = (from x in this._businessObjects.Context.DefdeliveryMethodsView
                                                       where x.Id == relation.MethodId && x.BranchId == branchId
                                                       select x).FirstOrDefault();

                if(relationView != null)
                {

                    newMethod = new DeliveryMethod
                    {
                        Id = relationView.Id,
                        BranchId = relationView.BranchId,
                        Name = relationView.Name,
                        Description = relationView.Description,
                        IconName = relationView.IconName,
                        CreatedDate = relationView.CreatedDate,
                        UpdatedDate = relationView.UpdatedDate,
                        UnitPrice = relationView.UnitPrice,
                        DistanceRange = relationView.UnitDistance,
                        MaxItemsPerDelivery = relationView.MaxItemsPerDelivery,
                        FixedPrice = relationView.FixedPrice
                    };
                }

            }
            catch (Exception e)
            {
                this._businessObjects.Context.DefbranchDeliveryMethods.Remove(relation);
                this._businessObjects.Context.SaveChanges();

                newMethod = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return newMethod;
        }//POST ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Updates a delivery method
        /// </summary>
        /// <param name="methodId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="unitPrice"></param>
        /// <param name="fixedPrice"></param>
        /// <param name="isActive"></param>
        /// <param name="maxItemsPerDelivery"></param>
        /// <returns></returns>
        public DeliveryMethod Put(Guid methodId, string name, string description, string iconName)
        {
            DeliveryMethod currentMethod = null;


            try
            {
                DefdeliveryMethods deliveryMethod = null;

                var query = from x in this._businessObjects.Context.DefdeliveryMethods
                            where x.Id == methodId
                            select x;

                foreach (DefdeliveryMethods item in query)
                {
                    deliveryMethod = item;
                }

                if (deliveryMethod != null)
                {
                    deliveryMethod.Name = name;
                    deliveryMethod.Description = description;
                    deliveryMethod.IconName = iconName;
                    deliveryMethod.UpdatedDate = DateTime.UtcNow;


                    this._businessObjects.Context.SaveChanges();

                    currentMethod = new DeliveryMethod
                    {
                        Id = deliveryMethod.Id,
                        Name = deliveryMethod.Name,
                        Description = deliveryMethod.Description,
                        IconName = deliveryMethod.IconName,
                        CreatedDate = deliveryMethod.CreatedDate,
                        UpdatedDate = deliveryMethod.UpdatedDate
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
        /// Updates a delivery method relation
        /// </summary>
        /// <param name="methodId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="unitPrice"></param>
        /// <param name="minMembershipLevelId"></param>
        /// <param name="fixedPrice"></param>
        /// <param name="isActive"></param>
        /// <param name="maxItemsPerDelivery"></param>
        /// <returns></returns>
        public DeliveryMethod Put(Guid methodId, Guid branchId, decimal unitPrice, float unitDistance, int maxItemsPerDelivery, bool fixedPrice)
        {
            DeliveryMethod currentMethod = null;


            try
            {
                DefbranchDeliveryMethods relation = null;

                var query = from x in this._businessObjects.Context.DefbranchDeliveryMethods
                            where x.MethodId == methodId && x.BranchId == branchId
                            select x;

                foreach (DefbranchDeliveryMethods item in query)
                {
                    relation = item;
                }

                if (relation != null)
                {
                    relation.UnitDistance = unitDistance;
                    relation.UnitPrice = unitPrice;
                    relation.FixedPrice = fixedPrice;
                    relation.MaxItemsPerDelivery = maxItemsPerDelivery;
                    relation.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    DefdeliveryMethodsView relationView = (from x in this._businessObjects.Context.DefdeliveryMethodsView
                                                           where x.Id == relation.MethodId && x.BranchId == branchId
                                                           select x).FirstOrDefault();

                    if(relationView != null)
                    {
                        currentMethod = new DeliveryMethod
                        {
                            Id = relationView.Id,
                            BranchId = relationView.BranchId,
                            Name = relationView.Name,
                            Description = relationView.Description,
                            IconName = relationView.IconName,
                            CreatedDate = relationView.CreatedDate,
                            UpdatedDate = relationView.UpdatedDate,
                            DistanceRange = relationView.UnitDistance,
                            UnitPrice = relationView.UnitPrice,
                            FixedPrice = relationView.FixedPrice,
                            MaxItemsPerDelivery = relationView.MaxItemsPerDelivery,
                            IsActive = relationView.IsActive
                        };
                    }
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
        /// Changes the active state for
        /// a branch delivery method
        /// </summary>
        /// <param name="methodId"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public bool Put(Guid methodId, Guid branchId)
        {
            bool success = false;

            try
            {
                DefbranchDeliveryMethods relation = null;

                var query = from x in this._businessObjects.Context.DefbranchDeliveryMethods
                            where x.BranchId == branchId && x.MethodId == methodId
                            select x;

                foreach (DefbranchDeliveryMethods item in query)
                {
                    relation = item;
                }

                if (relation != null)
                {
                    relation.IsActive = !relation.IsActive;
                    relation.UpdatedDate = DateTime.UtcNow;

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
                DefdeliveryMethods deliveryMethod = null;

                var query = from x in this._businessObjects.Context.DefdeliveryMethods
                            where x.Id == methodId
                            select x;

                foreach (DefdeliveryMethods item in query)
                {
                    deliveryMethod = item;
                }

                if (deliveryMethod != null)
                {
                    this._businessObjects.Context.DefdeliveryMethods.Remove(deliveryMethod);
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
        }

        public bool Delete(Guid methodId, Guid branchId)
        {
            bool success = false;

            try
            {
                DefbranchDeliveryMethods relation = null;

                var query = from x in this._businessObjects.Context.DefbranchDeliveryMethods
                            where x.BranchId == branchId && x.MethodId == methodId
                            select x;

                foreach (DefbranchDeliveryMethods item in query)
                {
                    relation = item;
                }

                if (relation != null)
                {
                    this._businessObjects.Context.DefbranchDeliveryMethods.Remove(relation);
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
        public DeliveryMethodManager(BusinessObjects businessObjects)
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
