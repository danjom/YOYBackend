using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.UserAPI.Logic.Image
{
    public static class ImageAdapter
    {
        private static StringBuilder stringBuilder = new StringBuilder();
        private const string toBeSearched = "upload";

        public static string TransformImg(string url, int imgHeight, int imgWidth)
        {
            string transformedUrl;

            try
            {
                string transformation = "/w_" + imgHeight + ",h_" + imgHeight + ",c_scale";

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
