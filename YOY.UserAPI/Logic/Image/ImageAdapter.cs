using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.UserAPI.Logic.Image
{
    public static class ImageAdapter
    {
        private const string toBeSearched = "upload";

        public static string TransformImg(string url, int imgHeight, int imgWidth)
        {
            string transformedUrl;

            try
            {
                
                StringBuilder stringBuilder = new StringBuilder();

                string transformation = "/w_" + imgWidth + ",h_" + imgHeight + ",c_scale";

                stringBuilder.Append(url);
                stringBuilder.Insert(url.IndexOf(toBeSearched) + toBeSearched.Length, transformation);
                transformedUrl = stringBuilder.ToString();
                stringBuilder.Clear();
            }
            catch (Exception)
            {
                transformedUrl = "";
            }

            return transformedUrl;

        }
    }
}
