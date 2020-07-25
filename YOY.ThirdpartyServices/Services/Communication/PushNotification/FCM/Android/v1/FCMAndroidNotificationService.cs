using YOY.ThirdpartyServices.ResponseModels.FCM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace YOY.ThirdpartyServices.Services.Communication.PushNotification.FCM.Android.v1
{
    public class FCMAndroidNotificationService
    {

        public PushNotificationSendingResponse SendAndroidNotification(List<string> deviceIds, string showMessage, string title, string message, string pushContentType, string contentCount, string commerceId, string commerceLogo, string commerceName, string campaignId, string contentData)
        {

            PushNotificationSendingResponse result = new PushNotificationSendingResponse
            {
                Response = "",
                Error = null,
                Successful = true
            };

            try
            {
                var tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json;charset=UTF-8";
                tRequest.Headers.Add($"Authorization: key={Settings.Default.fcmApiKey}");

                tRequest.Headers.Add($"Sender: id={Settings.Default.fcmSenderId}");

                string stringRegIds = string.Join("\",\"", deviceIds);

                string postData;

                if (!string.IsNullOrWhiteSpace(contentData))
                {
                    if (deviceIds.Count > 1)
                    {
                        postData = "{\"collapse_key\":\"test\",\"time_to_live\":108,\"data\": { \"message\" : " + "\"" + message + "\",\"time\": " + "\"" + System.DateTime.Now.ToString() +
                                "\",\"pushContentType\": " + "\"" + pushContentType + "\",\"contentCount\": " + "\"" + contentCount + "\",\"showMessage\": " + "\"" + showMessage + "\",\"title\": " + "\"" + title + "\",\"commerceId\": " + "\"" + commerceId +
                                "\",\"commerceName\": " + "\"" + commerceName + "\",\"commerceLogo\": " + "\"" + commerceLogo +
                                "\",\"campaignId\": " + "\"" + campaignId + "\",\"content\": " + contentData + "},\"registration_ids\":[\"" + stringRegIds + "\"]}";
                    }
                    else
                    {
                        postData = "{\"collapse_key\":\"test\",\"time_to_live\":108,\"data\": { \"message\" : " + "\"" + message + "\",\"time\": " + "\"" + System.DateTime.Now.ToString() +
                                "\",\"pushContentType\": " + "\"" + pushContentType + "\",\"contentCount\": " + "\"" + contentCount + "\",\"showMessage\": " + "\"" + showMessage + "\",\"title\": " + "\"" + title + "\",\"commerceId\": " + "\"" + commerceId +
                                "\",\"commerceName\": " + "\"" + commerceName + "\",\"commerceLogo\": " + "\"" + commerceLogo +
                                "\",\"campaignId\": " + "\"" + campaignId + "\",\"content\": " + contentData + "},\"to\":\"" + stringRegIds + "\"}";
                    }
                }
                else
                {
                    if (deviceIds.Count > 1)
                    {
                        postData = "{\"collapse_key\":\"test\",\"time_to_live\":108,\"data\": { \"message\" : " + "\"" + message + "\",\"time\": " + "\"" + System.DateTime.Now.ToString() +
                                "\",\"contentCount\": " + "\"" + contentCount + "\",\"showMessage\": " + "\"" + showMessage + "\",\"title\": " + "\"" + title + "\",\"commerceId\": " + "\"" + commerceId +
                                "\",\"campaignId\": " + "\"" + campaignId + "\"},\"registration_ids\":[\"" + stringRegIds + "\"]}";
                    }
                    else
                    {
                        postData = "{\"collapse_key\":\"test\",\"time_to_live\":108,\"data\": { \"message\" : " + "\"" + message + "\",\"time\": " + "\"" + System.DateTime.Now.ToString() +
                                "\",\"contentCount\": " + "\"" + contentCount + "\",\"showMessage\": " + "\"" + showMessage + "\",\"title\": " + "\"" + title + "\",\"commerceId\": " + "\"" + commerceId +
                                "\",\"campaignId\": " + "\"" + campaignId + "\" },\"to\":\"" + stringRegIds + "\"}";
                    }
                }

                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                String sResponseFromServer;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = e;
            }

            return result;
        }
    }
}
