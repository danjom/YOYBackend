using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.Values;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class ExternallyStoredFileManager
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
        /// Returns all files that have the reference passed by parameter
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public List<ExternallyStoredFile> Gets(Guid reference, Guid tenantId)
        {
            List<ExternallyStoredFile> files = new List<ExternallyStoredFile>();
            try
            {
                var query = from x in this._businessObjects.Context.OltpexternallyStoredFiles
                            where x.TenantId == tenantId && x.ReferenceId == reference
                            select x;

                ExternallyStoredFile file;

                foreach (OltpexternallyStoredFiles item in query)
                {
                    file = new ExternallyStoredFile
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        ReferenceId = item.ReferenceId,
                        ReferenceType = item.ReferenceType,
                        ExternalId = item.ExternalId,
                        StorageSource = item.StorageSource,
                        FileType = item.FileType,
                        MimeType = item.MimeType,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };


                    files.Add(file);
                } // FOREACH ENDS
            } // TRY ENDS
            catch (Exception e)
            {
                files = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            } // CATCH ENDS
            return files;
        } // METHOD GETS ENDS ---------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Allows to download an specific file from the platform
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public ExternallyStoredFile Get(Guid fileId)
        {
            ExternallyStoredFile file = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpexternallyStoredFiles
                            where x.Id == fileId
                            select x;


                OltpexternallyStoredFiles externalFile = null;

                foreach (OltpexternallyStoredFiles item in query)
                {
                    externalFile = item;

                } // FOREACH ENDS

                if (externalFile != null)
                {
                    file = new ExternallyStoredFile
                    {
                        Id = externalFile.Id,
                        TenantId = externalFile.TenantId,
                        ReferenceId = externalFile.ReferenceId,
                        ReferenceType = externalFile.ReferenceType,
                        ExternalId = externalFile.ExternalId,
                        StorageSource = externalFile.StorageSource,
                        FileType = externalFile.FileType,
                        MimeType = externalFile.MimeType,
                        CreatedDate = externalFile.CreatedDate,
                        UpdatedDate = externalFile.UpdatedDate
                    };
                }

            } // TRY ENDS
            catch (Exception e)
            {
                file = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            } // CATCH ENDS

            return file;

        } // METHOD GET WITH FILE ID ENDS --------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Allows to download an specific url image from the platform
        /// </summary>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public ExternallyStoredFile Get(string externalId)
        {
            ExternallyStoredFile file = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpexternallyStoredFiles
                            where x.ExternalId == externalId
                            select x;


                OltpexternallyStoredFiles externalFile = null;

                foreach (OltpexternallyStoredFiles item in query)
                {
                    externalFile = item;

                } // FOREACH ENDS

                if (externalFile != null)
                {
                    file = new ExternallyStoredFile
                    {
                        Id = externalFile.Id,
                        TenantId = externalFile.TenantId,
                        ReferenceId = externalFile.ReferenceId,
                        ReferenceType = externalFile.ReferenceType,
                        ExternalId = externalFile.ExternalId,
                        StorageSource = externalFile.StorageSource,
                        FileType = externalFile.FileType,
                        MimeType = externalFile.MimeType,
                        CreatedDate = externalFile.CreatedDate,
                        UpdatedDate = externalFile.UpdatedDate
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


        public ExternallyStoredFile Post(Guid tenantId, Guid referenceId, int referenceType, string externalId, int storageSource, int fileType, string mimeType)
        {
            ExternallyStoredFile newFile;

            try
            {
                OltpexternallyStoredFiles file = new OltpexternallyStoredFiles
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    ExternalId = externalId,
                    StorageSource = storageSource,
                    FileType = fileType,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    MimeType = mimeType
                };
                this._businessObjects.Context.OltpexternallyStoredFiles.Add(file);
                this._businessObjects.Context.SaveChanges();

                newFile = new ExternallyStoredFile
                {
                    Id = file.Id,
                    TenantId = file.TenantId,
                    ReferenceId = file.ReferenceId,
                    ReferenceType = file.ReferenceType,
                    ExternalId = file.ExternalId,
                    StorageSource = file.StorageSource,
                    FileType = file.FileType,
                    MimeType = file.MimeType,
                    CreatedDate = file.CreatedDate,
                    UpdatedDate = file.UpdatedDate
                };

            } // TRY ENDS
            catch (Exception e)
            {
                newFile = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            } // CATCH ENDS
            return newFile;
        } // METHOD CREATE IMAGE ENDS ====================================================================================================== //


        /// <summary>
        /// Changes the file reference
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public bool Put(Guid id, Guid referenceId, int referenceType)
        {
            bool success = false;
            try
            {
                var query = from x in this._businessObjects.Context.OltpexternallyStoredFiles
                            where x.Id == id
                            select x;

                OltpexternallyStoredFiles oltpExternallyStoredFile = null;

                foreach (OltpexternallyStoredFiles item in query)
                {
                    oltpExternallyStoredFile = item;
                }

                if (oltpExternallyStoredFile != null)
                {
                    oltpExternallyStoredFile.ReferenceId = referenceId;
                    oltpExternallyStoredFile.ReferenceType = referenceType;
                    oltpExternallyStoredFile.UpdatedDate = DateTime.UtcNow;

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


        public string Delete(Guid fileId)
        {
            string externalId = "";
            try
            {
                var query = (dynamic)null;

                OltpexternallyStoredFiles file = null;

                query = from x in this._businessObjects.Context.OltpexternallyStoredFiles
                        where x.Id == fileId
                        select x;

                foreach (OltpexternallyStoredFiles item in query)
                {
                    file = item;
                } // FOREACH ENDS

                if (file != null)
                {
                    externalId = file.ExternalId;

                    this._businessObjects.Context.OltpexternallyStoredFiles.Remove(file);
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
        public ExternallyStoredFileManager(BusinessObjects businessObjects)
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
