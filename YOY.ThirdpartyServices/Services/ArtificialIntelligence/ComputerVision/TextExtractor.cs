using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using YOY.DTO.Entities.Misc.ComputerVision;

namespace YOY.ThirdpartyServices.Services.ArtificialIntelligence.ComputerVision
{
    public class TextExtractor
    {


        public string ReadImageAsync(string imageFilePath)
        {
            string extractedText = "";

            if (File.Exists(imageFilePath))
            {
                // Call the REST API method.
                extractedText = MakeOCRRequest(imageFilePath);
            }

            return extractedText;
        }

        /// <summary>
        /// Gets the text visible in the specified image file by using
        /// the Computer Vision REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file with printed text.</param>
        public string MakeOCRRequest(string imageFilePath)
        {

            string extractedText = "";

            try
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", Settings.Default.cognitiveServicesSubscriptionKey);

                // Request parameters. 
                // The language parameter doesn't specify a language, so the 
                // method detects it automatically.
                // The detectOrientation parameter is set to true, so the method detects and
                // and corrects text orientation before detecting text.
                string requestParameters = "language=unk&detectOrientation=true";

                // Assemble the URI for the REST API method.
                string uri = Settings.Default.visionOCRServicesUriBase + "?" + requestParameters;

                HttpResponseMessage response;

                // Read the contents of the specified local image
                // into a byte array.
                byte[] byteData = GetImageAsByteArray(imageFilePath);

                // Add the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json"
                    // and "multipart/form-data".
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");

                    // Asynchronously call the REST API method.
                    response = client.PostAsync(uri, content).Result;
                }

                // Asynchronously get the JSON response.
                string ocrResponse = response.Content.ReadAsStringAsync().Result;

                ExtractedText jsonExtractedText = JsonConvert.DeserializeObject<ExtractedText>(ocrResponse);

                if (jsonExtractedText != null && jsonExtractedText.Regions?.Count > 0)
                {
                    foreach (TextRegion item in jsonExtractedText.Regions)
                    {
                        foreach (TextLine lineItem in item.Lines)
                        {
                            foreach (Word wordItem in lineItem.Words)
                            {
                                extractedText += wordItem.Text + " ";
                            }

                            extractedText += "\n";
                        }
                    }
                }



            }
            catch (Exception)
            {
                extractedText = "";
            }

            return extractedText;
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                // Read the file's contents into a byte array.
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
    }
}