using YOY.DTO.Entities;
using YOY.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class FranchiseeManager
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

        public List<Franchisee> Gets(int activeState, Guid? tenantId, int pageSize, int pageNumber)
        {
            List<Franchisee> franchisees = new List<Franchisee>();

            try
            {
                var query = (dynamic)null;

                if (tenantId != null)
                {
                    switch (activeState)
                    {
                        case ActiveStates.Active:
                            query = (from x in this._businessObjects.Context.Deffranchisees
                                     where !x.Deleted && (bool)x.IsActive && x.TenantId == tenantId
                                     orderby x.LegalName ascending
                                     select x).Skip(pageNumber * pageSize).Take(pageSize);
                            break;
                        case ActiveStates.Inactive:
                            query = (from x in this._businessObjects.Context.Deffranchisees
                                     where !x.Deleted && !(bool)x.IsActive && x.TenantId == tenantId
                                     orderby x.LegalName ascending
                                     select x).Skip(pageNumber * pageSize).Take(pageSize);
                            break;
                        case ActiveStates.All:
                            query = (from x in this._businessObjects.Context.Deffranchisees
                                     where !x.Deleted && x.TenantId == tenantId
                                     orderby x.LegalName ascending
                                     select x).Skip(pageNumber * pageSize).Take(pageSize);
                            break;
                    }
                }
                else
                {
                    switch (activeState)
                    {
                        case ActiveStates.Active:
                            query = (from x in this._businessObjects.Context.Deffranchisees
                                     where !x.Deleted && (bool)x.IsActive
                                     orderby x.LegalName ascending
                                     select x).Skip(pageNumber * pageSize).Take(pageSize);
                            break;
                        case ActiveStates.Inactive:
                            query = (from x in this._businessObjects.Context.Deffranchisees
                                     where !x.Deleted && !(bool)x.IsActive
                                     orderby x.LegalName ascending
                                     select x).Skip(pageNumber * pageSize).Take(pageSize);
                            break;
                        case ActiveStates.All:
                            query = (from x in this._businessObjects.Context.Deffranchisees
                                     where !x.Deleted
                                     orderby x.LegalName ascending
                                     select x).Skip(pageNumber * pageSize).Take(pageSize);
                            break;
                    }
                }


                if (query != null)
                {
                    Franchisee franchisee = null;

                    foreach (Deffranchisees item in query)
                    {
                        franchisee = new Franchisee
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            IsActive = (bool)item.IsActive,
                            ContactName = item.ContactName,
                            ContactEmail = item.ContactEmail,
                            ContactPhoneNumber = item.ContactPhoneNumber,
                            LegalName = item.LegalName,
                            TaxId = item.TaxId,
                            TaxAddress = item.TaxAddress,
                            PaymentSubject = item.PaymentSubject,
                            AdditionalNotes = item.AdditionalNotes,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        franchisees.Add(franchisee);
                    }
                }

            }
            catch (Exception e)
            {
                franchisees = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return franchisees;
        }

        public Franchisee Get(Guid id)
        {
            Franchisee franchisee = null;

            try
            {
                var query = from x in this._businessObjects.Context.Deffranchisees
                            where !x.Deleted && x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (Deffranchisees item in query)
                    {
                        franchisee = new Franchisee
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            IsActive = (bool)item.IsActive,
                            ContactName = item.ContactName,
                            ContactEmail = item.ContactEmail,
                            ContactPhoneNumber = item.ContactPhoneNumber,
                            LegalName = item.LegalName,
                            TaxId = item.TaxId,
                            TaxAddress = item.TaxAddress,
                            PaymentSubject = item.PaymentSubject,
                            AdditionalNotes = item.AdditionalNotes,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                franchisee = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return franchisee;
        }

        public Franchisee Post(Guid tenantId, string contactName, string contactEmail, string contactPhoneNumber, string legalName, string taxId, string taxAddress, string paymentSubject, string additionalSubject)
        {
            Franchisee franchisee;
            try
            {
                Deffranchisees newFranchisee = new Deffranchisees
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    IsActive = true,
                    ContactName = contactName,
                    ContactEmail = contactName,
                    ContactPhoneNumber = contactPhoneNumber,
                    LegalName = legalName,
                    TaxId = taxId,
                    TaxAddress = taxAddress,
                    PaymentSubject = paymentSubject,
                    AdditionalNotes = additionalSubject,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.Deffranchisees.Add(newFranchisee);
                this._businessObjects.Context.SaveChanges();

                franchisee = new Franchisee
                {
                    Id = newFranchisee.Id,
                    TenantId = newFranchisee.TenantId,
                    IsActive = (bool)newFranchisee.IsActive,
                    ContactName = newFranchisee.ContactName,
                    ContactEmail = newFranchisee.ContactEmail,
                    ContactPhoneNumber = newFranchisee.ContactPhoneNumber,
                    LegalName = newFranchisee.LegalName,
                    TaxId = newFranchisee.TaxId,
                    TaxAddress = newFranchisee.TaxAddress,
                    PaymentSubject = newFranchisee.PaymentSubject,
                    AdditionalNotes = newFranchisee.AdditionalNotes,
                    CreatedDate = newFranchisee.CreatedDate,
                    UpdatedDate = newFranchisee.UpdatedDate
                };
            }
            catch (Exception e)
            {
                franchisee = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return franchisee;
        }

        public Franchisee Put(Guid id, string contactName, string contactEmail, string contactPhoneNumber, string legalName, string taxId, string taxAddress, string paymentSubject, string additionalNotes)
        {
            Franchisee franchisee = null;

            try
            {
                var query = from x in this._businessObjects.Context.Deffranchisees
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Deffranchisees currentFranchisee = null;

                    foreach (Deffranchisees item in query)
                    {
                        currentFranchisee = item;
                    }

                    if (currentFranchisee != null)
                    {
                        currentFranchisee.ContactName = contactName;
                        currentFranchisee.ContactEmail = contactEmail;
                        currentFranchisee.ContactPhoneNumber = contactPhoneNumber;
                        currentFranchisee.LegalName = legalName;
                        currentFranchisee.TaxId = taxId;
                        currentFranchisee.TaxAddress = taxAddress;
                        currentFranchisee.PaymentSubject = paymentSubject;
                        currentFranchisee.AdditionalNotes = additionalNotes;
                        currentFranchisee.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        franchisee = new Franchisee
                        {
                            Id = currentFranchisee.Id,
                            TenantId = currentFranchisee.TenantId,
                            IsActive = (bool)currentFranchisee.IsActive,
                            ContactName = currentFranchisee.ContactName,
                            ContactEmail = currentFranchisee.ContactEmail,
                            ContactPhoneNumber = currentFranchisee.ContactPhoneNumber,
                            LegalName = currentFranchisee.LegalName,
                            TaxId = currentFranchisee.TaxId,
                            TaxAddress = currentFranchisee.TaxAddress,
                            PaymentSubject = currentFranchisee.PaymentSubject,
                            AdditionalNotes = currentFranchisee.AdditionalNotes,
                            CreatedDate = currentFranchisee.CreatedDate,
                            UpdatedDate = currentFranchisee.UpdatedDate
                        };

                    }
                }
            }
            catch (Exception e)
            {
                franchisee = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return franchisee;
        }

        public bool Put(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Deffranchisees
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Deffranchisees currentFranchisee = null;

                    foreach (Deffranchisees item in query)
                    {
                        currentFranchisee = item;
                    }

                    if (currentFranchisee != null)
                    {
                        currentFranchisee.IsActive = !currentFranchisee.IsActive;
                        currentFranchisee.UpdatedDate = DateTime.UtcNow;

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
                Deffranchisees currentFranchisee = (from x in this._businessObjects.Context.Deffranchisees
                                                    where x.Id == id
                                                    select x).FirstOrDefault();

                if (currentFranchisee != null)
                {
                    currentFranchisee.Deleted = true;
                    currentFranchisee.UpdatedDate = DateTime.UtcNow;

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
        /// Creates a new FranchiseeManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public FranchiseeManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD TABLE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
