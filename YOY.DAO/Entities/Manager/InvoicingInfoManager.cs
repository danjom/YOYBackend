using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class InvoicingInfoManager
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

        private string GetInvoiceTypeName(int invoiceType)
        {
            string typeName = invoiceType switch
            {
                InvoiceTypes.Invoice => Resources.Invoice,
                _ => "--"
            };

            return typeName;
        }
        private string GetInvoiceIdTypeName(int invoiceIdType)
        {
            string typeName = invoiceIdType switch
            {
                InvoiceIdTypes.PersonalId => Resources.PersonalId,
                InvoiceIdTypes.LegallId => Resources.LegalId,
                _ => "--"
            };

            return typeName;
        }

        public List<InvoicingInfo> Gets(string userId, int invoiceType, int invoiceIdType, int pageSize, int pageNumber)
        {
            List<InvoicingInfo> infos = null;

            try
            {
                var query = (dynamic)null;

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    if (invoiceType != InvoiceTypes.All)
                    {
                        if (invoiceIdType != InvoiceIdTypes.All)
                        {
                            query = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                     where x.UserId == userId && x.Type == invoiceType && x.InvoicingIdType == invoiceIdType
                                     orderby x.CreatedDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                     where x.UserId == userId && x.Type == invoiceType
                                     orderby x.CreatedDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                    }
                    else
                    {
                        if (invoiceIdType != InvoiceIdTypes.All)
                        {
                            query = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                     where x.UserId == userId && x.InvoicingIdType == invoiceIdType
                                     orderby x.CreatedDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                     where x.UserId == userId
                                     orderby x.CreatedDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                    }
                }
                else
                {
                    if (invoiceType != InvoiceTypes.All)
                    {
                        if (invoiceIdType != InvoiceIdTypes.All)
                        {
                            query = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                     where x.Type == invoiceType && x.InvoicingIdType == invoiceIdType
                                     orderby x.CreatedDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                     where x.Type == invoiceType
                                     orderby x.CreatedDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                    }
                    else
                    {
                        if (invoiceIdType != InvoiceIdTypes.All)
                        {
                            query = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                     where x.InvoicingIdType == invoiceIdType
                                     orderby x.CreatedDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                        else
                        {
                            query = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                     orderby x.CreatedDate descending
                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                        }
                    }
                }


                if(query != null)
                {
                    infos = new List<InvoicingInfo>();
                    InvoicingInfo info;

                    foreach(OltpinvoicingInfos item in query)
                    {
                        info = new InvoicingInfo
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            Type = item.Type,
                            TypeName = GetInvoiceTypeName(item.Type),
                            InvoicingIdType = item.InvoicingIdType,
                            InvoicingIdTypeName = GetInvoiceIdTypeName(item.InvoicingIdType),
                            InvoicingId = item.InvoicingId,
                            InvoicingName = item.InvoicingName,
                            AdditionalDetails = item.AdditionalDetails,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        infos.Add(info);
                    }
                }
            }
            catch(Exception e)
            {
                infos = null;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return infos;
        }

        public InvoicingInfo Get(string userId)
        {
            InvoicingInfo info = null;

            try
            {
                OltpinvoicingInfos invoicingInfo = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                                    where x.UserId == userId
                                                    orderby x.CreatedDate descending
                                                    select x).FirstOrDefault();

                if(invoicingInfo != null)
                {
                    info = new InvoicingInfo
                    {
                        Id = invoicingInfo.Id,
                        UserId = invoicingInfo.UserId,
                        Type = invoicingInfo.Type,
                        TypeName = GetInvoiceTypeName(invoicingInfo.Type),
                        InvoicingIdType = invoicingInfo.InvoicingIdType,
                        InvoicingIdTypeName = GetInvoiceIdTypeName(invoicingInfo.InvoicingIdType),
                        InvoicingId = invoicingInfo.InvoicingId,
                        InvoicingName = invoicingInfo.InvoicingName,
                        AdditionalDetails = invoicingInfo.AdditionalDetails,
                        CreatedDate = invoicingInfo.CreatedDate,
                        UpdatedDate = invoicingInfo.UpdatedDate
                    };
                }
            }
            catch(Exception e)
            {
                info = null;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return info;
        }

        public InvoicingInfo Get(Guid id)
        {
            InvoicingInfo info = null;

            try
            {
                OltpinvoicingInfos invoicingInfo = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                                    where x.Id == id
                                                    orderby x.CreatedDate descending
                                                    select x).FirstOrDefault();

                if (invoicingInfo != null)
                {
                    info = new InvoicingInfo
                    {
                        Id = invoicingInfo.Id,
                        UserId = invoicingInfo.UserId,
                        Type = invoicingInfo.Type,
                        TypeName = GetInvoiceTypeName(invoicingInfo.Type),
                        InvoicingIdType = invoicingInfo.InvoicingIdType,
                        InvoicingIdTypeName = GetInvoiceIdTypeName(invoicingInfo.InvoicingIdType),
                        InvoicingId = invoicingInfo.InvoicingId,
                        InvoicingName = invoicingInfo.InvoicingName,
                        AdditionalDetails = invoicingInfo.AdditionalDetails,
                        CreatedDate = invoicingInfo.CreatedDate,
                        UpdatedDate = invoicingInfo.UpdatedDate
                    };
                }
            }
            catch (Exception e)
            {
                info = null;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return info;
        }

        public InvoicingInfo Post(string userId, int type, int invoicingIdType, string invoicingId, string invoicingName, string additionalDetails)
        {
            InvoicingInfo info = null;

            try
            {
                OltpinvoicingInfos newInfo = new OltpinvoicingInfos
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Type = type,
                    InvoicingIdType = invoicingIdType,
                    InvoicingId = invoicingId,
                    InvoicingName = invoicingName,
                    AdditionalDetails = additionalDetails,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpinvoicingInfos.Add(newInfo);

                this._businessObjects.Context.SaveChanges();

                info = new InvoicingInfo
                {
                    Id = newInfo.Id,
                    UserId = newInfo.UserId,
                    Type = newInfo.Type,
                    TypeName = GetInvoiceTypeName(newInfo.Type),
                    InvoicingIdType = newInfo.InvoicingIdType,
                    InvoicingIdTypeName = GetInvoiceIdTypeName(newInfo.InvoicingIdType),
                    InvoicingId = newInfo.InvoicingId,
                    InvoicingName = newInfo.InvoicingName,
                    AdditionalDetails = newInfo.AdditionalDetails,
                    CreatedDate = newInfo.CreatedDate,
                    UpdatedDate = newInfo.UpdatedDate
                };
            }
            catch(Exception e)
            {
                info = null;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return info;
        }

        public InvoicingInfo Put(Guid id, int type, int invoicingIdType, string invoicingId, string invoicingName, string additionalDetails)
        {
            InvoicingInfo info = null;

            try
            {
                OltpinvoicingInfos currentInfo = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                                  where x.Id == id
                                                  select x).FirstOrDefault();

                if(currentInfo != null)
                {
                    currentInfo.Type = type;
                    currentInfo.InvoicingIdType = invoicingIdType;
                    currentInfo.InvoicingId = invoicingId;
                    currentInfo.InvoicingName = invoicingName;
                    currentInfo.AdditionalDetails = additionalDetails;
                    currentInfo.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    info = new InvoicingInfo
                    {
                        Id = currentInfo.Id,
                        UserId = currentInfo.UserId,
                        Type = currentInfo.Type,
                        TypeName = GetInvoiceTypeName(currentInfo.Type),
                        InvoicingIdType = currentInfo.InvoicingIdType,
                        InvoicingIdTypeName = GetInvoiceIdTypeName(currentInfo.InvoicingIdType),
                        InvoicingId = currentInfo.InvoicingId,
                        InvoicingName = currentInfo.InvoicingName,
                        AdditionalDetails = currentInfo.AdditionalDetails,
                        CreatedDate = currentInfo.CreatedDate,
                        UpdatedDate = currentInfo.UpdatedDate
                    };
                }
            }
            catch(Exception e)
            {
                info = null;

                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return info;
        }

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                OltpinvoicingInfos info = (from x in this._businessObjects.Context.OltpinvoicingInfos
                                           where x.Id == id
                                           select x).FirstOrDefault();

                if(info != null)
                {
                    this._businessObjects.Context.Remove(info);
                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }
            }
            catch(Exception e)
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
        /// Creates a new InvoicingInfoManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public InvoicingInfoManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD INVOICING INFO MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
