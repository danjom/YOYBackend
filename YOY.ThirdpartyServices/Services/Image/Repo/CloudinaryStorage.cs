using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using YOY.ThirdpartyServices.ResponseModels.Image;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace YOY.ThirdpartyServices.Services.Image.Repo
{
    public class CloudinaryStorage
    {
        #region PROPERTIES_AND_RESOURCES

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        //private static string baseDeliveryUrl = "https://res.cloudinary.com/yoyimgs";
        //private static string baseApiUrl = "https://api.cloudinary.com/v1_1/yoyimgs";

        private static Cloudinary repo;
        #endregion

        #region METHODS

        private static void Initialize()
        {
            Account account = new Account(
              Settings.Default.cloudinaryCloudName,
              Settings.Default.cloudinaryApiKey,
              Settings.Default.cloudinaryApiSecret);

            repo = new Cloudinary(account);
        }

        public static UploadResponse UploadImage(string imgUrl, string foldername, string imgType, int width, int height)
        {
            UploadResponse response;

            try
            {
                Initialize();

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imgUrl),
                    EagerTransforms = new List<Transformation>()
                  {
                    new Transformation().Width(width).Height(height).Crop("fit").FetchFormat("png")//default w:640 h:514
                  },
                    Tags = imgType
                };

                if (!string.IsNullOrWhiteSpace(foldername))
                {
                    uploadParams.Folder = foldername;
                }

                var uploadResult = repo.Upload(uploadParams);

                response = JObject.Parse(uploadResult.JsonObj.ToString()).ToObject<UploadResponse>();

            }
            catch (Exception)
            {
                response = null;
            }

            return response;
        }

        public static bool DeleteImage(string publicId)
        {
            bool success = false;

            try
            {
                Initialize();

                var delParams = new DelResParams()
                {
                    PublicIds = new List<string>() { publicId },
                    Invalidate = true
                };
                var delResult = repo.DeleteResources(delParams);

                if (delResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    success = true;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        #endregion
    }
}
