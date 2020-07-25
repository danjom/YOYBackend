using System;
using System.Collections.Generic;
using System.Text;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager.Misc.Image
{
    public class ImageHandler
    {
        #region PROPERTIES_AND_RESOURCES
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private static Tenant _tenant = null;
        private BusinessObjects _businessObjects = null;
        //private static string _imgsLocalUrl = "https://enjoyitbrain.azurewebsites.net/Data/LocalFile?id=";
        private string _imgsCloudUrl = "https://res.cloudinary.com/yoyimgs/image/upload/v";
        private readonly string[] _imgTypes = { Resources.DisplayImage, Resources.Code };
        private readonly int[] _imgStorage = { ImageStorages.Cloudinary };

        #endregion

        #region METODS

        private void Initialize(Guid commerceId)
        {

            if (_tenant == null || _tenant.TenantId != commerceId)
            {
                _tenant = YOY.DAO.Entities.Tenant.GetInstance(commerceId);
            }

            if (_businessObjects == null)
            {
                _businessObjects = BusinessObjects.GetInstance(_tenant);
            }
        }

        public DTO.Entities.Misc.Image.ImageResource GetImgUrl(Guid imgId, int storageType, int imageRequester)
        {
            DTO.Entities.Misc.Image.ImageResource imgFile = new DTO.Entities.Misc.Image.ImageResource();

            Initialize(Guid.Empty);

            try
            {
                switch (storageType)
                {
                    case ImageStorages.Cloudinary:

                        DTO.Entities.Image img = this._businessObjects.Images.Get(imgId);

                        //If image exists
                        if (img != null)
                        {
                            switch (imageRequester)
                            {
                                case ImageRequesters.Website:

                                    imgFile.ImgUrl = _imgsCloudUrl + img.Version + "/" + img.ExternalId + "." + img.Format;
                                    imgFile.FileExtension = img.Format;

                                    break;
                                case ImageRequesters.App:
                                    //------------------Add the app transformation!!!!!!!!!!!!!!!

                                    imgFile.ImgUrl = _imgsCloudUrl + img.Version + "/" + img.ExternalId + "." + img.Format;
                                    imgFile.FileExtension = img.Format;

                                    break;
                            }
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                imgFile = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return imgFile;
        }

        public DTO.Entities.Misc.Image.ImageResource GetImgUrl(string externalId, string version, string format, int storageType, int imageRequester)
        {
            DTO.Entities.Misc.Image.ImageResource imgFile = new DTO.Entities.Misc.Image.ImageResource();

            Initialize(Guid.Empty);

            switch (storageType)
            {
                case ImageStorages.Cloudinary:
                    switch (imageRequester)
                    {
                        case ImageRequesters.Website:

                            imgFile.ImgUrl = _imgsCloudUrl + version + "/" + externalId + "." + format;
                            imgFile.FileExtension = format;

                            break;
                        case ImageRequesters.App:
                            //------------------Add the app transformation!!!!!!!!!!!!!!!

                            imgFile.ImgUrl = _imgsCloudUrl + version + "/" + externalId + "." + format;
                            imgFile.FileExtension = format;

                            break;
                    }
                    break;

            }

            return imgFile;
        }


        #endregion
    }
}