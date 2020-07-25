using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;

namespace YOY.DAO.Entities.Manager
{
    public class ImageManager
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
        /// Returns all url images that have the reference passed by parameter
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public List<Image> Gets(Guid reference, Guid tenantId, int pageSize, int pageNumber)
        {
            List<Image> images = new List<Image>();
            try
            {
                var query = (from x in this._businessObjects.Context.Oltpimages
                             where x.TenantId == tenantId && x.ReferenceId == reference
                             orderby x.ExternalId ascending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                Image img = null;

                foreach (Oltpimages item in query)
                {
                    img = new Image
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        ReferenceId = item.ReferenceId,
                        Folder = item.Folder,
                        Format = item.Format,
                        Version = item.Version,
                        Width = item.Width,
                        Height = item.Height,
                        ExternalId = item.ExternalId,
                        WebTransformation = item.WebTransformation,
                        AppTransformation = item.AppTransformation,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };


                    images.Add(img);
                } // FOREACH ENDS
            } // TRY ENDS
            catch (Exception e)
            {
                images = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            } // CATCH ENDS
            return images;
        } // METHOD GETS ENDS ---------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Allows to download an specific url image from the platform
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public Image Get(Guid imageId)
        {
            Image file = null;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpimages
                            where x.Id == imageId
                            select x;


                Oltpimages image = null;

                foreach (Oltpimages item in query)
                {
                    image = item;

                } // FOREACH ENDS

                if (image != null)
                {
                    file = new Image
                    {
                        Id = image.Id,
                        TenantId = image.TenantId,
                        ReferenceId = image.ReferenceId,
                        Folder = image.Folder,
                        Version = image.Version,
                        Format = image.Format,
                        Width = image.Width,
                        Height = image.Height,
                        ExternalId = image.ExternalId,
                        WebTransformation = image.WebTransformation,
                        AppTransformation = image.AppTransformation,
                        CreatedDate = image.CreatedDate,
                        UpdatedDate = image.UpdatedDate
                    };
                }

            } // TRY ENDS
            catch (Exception e)
            {
                file = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

                //this.log.AddEvent(null, Logger.Severity.CRITICAL, "Could not retrieve image reference from database", e.Message, e.StackTrace);
            } // CATCH ENDS

            return file;

        } // METHOD GET WITH FILE ID ENDS --------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Allows to download an specific url image from the platform
        /// </summary>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public Image Get(string externalId)
        {
            Image file = null;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpimages
                            where x.ExternalId == externalId
                            select x;


                Oltpimages image = null;

                foreach (Oltpimages item in query)
                {
                    image = item;

                } // FOREACH ENDS

                if (image != null)
                {
                    file = new Image
                    {
                        Id = image.Id,
                        TenantId = image.TenantId,
                        ReferenceId = image.ReferenceId,
                        Folder = image.Folder,
                        Version = image.Version,
                        Format = image.Format,
                        Width = image.Width,
                        Height = image.Height,
                        ExternalId = image.ExternalId,
                        WebTransformation = image.WebTransformation,
                        AppTransformation = image.AppTransformation,
                        CreatedDate = image.CreatedDate,
                        UpdatedDate = image.UpdatedDate
                    };
                }

            } // TRY ENDS
            catch (Exception e)
            {
                file = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

                //this.log.AddEvent(null, Logger.Severity.CRITICAL, "Could not retrieve image reference from database", e.Message, e.StackTrace);
            } // CATCH ENDS

            return file;

        } // METHOD GET WITH FILE ID ENDS --------------------------------------------------------------------------------------------------------------- //


        public Image Post(Guid tenantId, Guid referenceId, string folder, string format, string version, string externalId, string webDimensions, string appDimensions, int width, int height)
        {
            Image newImg;
            try
            {
                Oltpimages img = new Oltpimages
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    ReferenceId = referenceId,
                    Folder = folder,
                    Format = format,
                    Version = version,
                    ExternalId = externalId,
                    WebTransformation = webDimensions,
                    AppTransformation = appDimensions,
                    Width = width,
                    Height = height,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                };
                this._businessObjects.Context.Oltpimages.Add(img);
                this._businessObjects.Context.SaveChanges();

                newImg = new Image
                {
                    Id = img.Id,
                    TenantId = img.TenantId,
                    ReferenceId = img.ReferenceId,
                    Folder = img.Folder,
                    Format = img.Format,
                    Version = img.Version,
                    ExternalId = img.ExternalId,
                    WebTransformation = img.WebTransformation,
                    AppTransformation = img.AppTransformation,
                    Width = img.Width,
                    Height = img.Height,
                    CreatedDate = img.CreatedDate,
                    UpdatedDate = img.UpdatedDate
                };

            } // TRY ENDS
            catch (Exception e)
            {
                newImg = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

                //this.log.AddEvent(null, Logger.Severity.CRITICAL, "Error storing image in database", e.Message, e.StackTrace);
            } // CATCH ENDS
            return newImg;
        } // METHOD CREATE IMAGE ENDS ====================================================================================================== //


        /// <summary>
        /// Changes the file reference
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public bool Put(Guid id, Guid reference)
        {
            bool success = false;

            Oltpimages oltpImg = null;
            try
            {
                var query = from x in this._businessObjects.Context.Oltpimages
                            where x.Id == id
                            select x;

                foreach (Oltpimages item in query)
                {
                    oltpImg = item;
                }

                if (oltpImg != null)
                {
                    oltpImg.ReferenceId = reference;
                    oltpImg.UpdatedDate = DateTime.UtcNow;

                    // Persist reference in database
                    this._businessObjects.Context.SaveChanges();
                    success = true;
                }

            } // TRY ENDS
            catch (Exception e)
            {

                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            } // CATCH ENDS
            return success;
        } // METHOD PUT ENDS ---------------------------------------------------------------------------------------------------------------------------- //


        public string Delete(Guid imgId)
        {
            string externalId = "";
            try
            {
                var query = (dynamic)null;

                Oltpimages img = null;

                query = from x in this._businessObjects.Context.Oltpimages
                        where x.Id == imgId
                        select x;

                foreach (Oltpimages item in query)
                {
                    img = item;
                } // FOREACH ENDS

                if (img != null)
                {
                    externalId = img.ExternalId;

                    this._businessObjects.Context.Oltpimages.Remove(img);
                    this._businessObjects.Context.SaveChanges();

                }


            } // TRY ENDS
            catch (Exception e)
            {
                externalId = "";
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            } // CATCH ENDS
            return externalId;
        } // METHOD GET WITH FILE ID ENDS --------------------------------------------------------------------------------------------------------------- //


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
        public ImageManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD FILE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion

    } // CLASS FILE MANAGER ENDS ------------------------------------------------------------------------------------------------------------------------ //

}