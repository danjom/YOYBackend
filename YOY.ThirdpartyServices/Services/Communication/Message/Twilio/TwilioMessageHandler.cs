using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using YOY.DTO.Entities.Misc.TextMessaging.Twilio;
using YOY.Values;

namespace YOY.ThirdpartyServices.Services.Communication.SMS.TwilioSMS
{
    public class TwilioMessageHandler
    {
        private static int MaxAttempts = 2;
        private static string StatusCallbackUri = "https://yoy-twlwebhk.azurewebsites.net/api/v1/msgStatusListener/post";

        #region SENDING  
        
        public static MessageResourceContent SendPlainTextMessage(int type, string msg, string senderPhoneNumber, string receiverPhoneNumber, int? validySeconds)
        {
            MessageResourceContent resourceContent = null;

            switch (type)
            {
                case TextMessageChannels.SMS:
                    resourceContent = SendPlainTextSMS(msg, senderPhoneNumber, receiverPhoneNumber, validySeconds);
                    break;
                case TextMessageChannels.Whatsapp:
                    resourceContent = SendPlainTextWhatsappMsg(msg, senderPhoneNumber, receiverPhoneNumber, validySeconds);
                    break;
            }

            return resourceContent;
        }

        private static MessageResourceContent SendPlainTextSMS(string msg, string senderPhoneNumber, string receiverPhoneNumber, int? validitySeconds)
        {
            MessageResourceContent resourceContent;

            try
            {
                TwilioClient.Init(Settings.Default.twilioAccountSID, Settings.Default.twilioAuthToken);

                var message = MessageResource.Create(
                    body: msg,
                    from: new Twilio.Types.PhoneNumber(senderPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(receiverPhoneNumber),
                    attempt: MaxAttempts,
                    statusCallback: new Uri(StatusCallbackUri)
                );

                if (message != null)
                {
                    resourceContent = new MessageResourceContent
                    {
                        ApiVersion = message.ApiVersion,
                        CreatedDate = message.DateCreated,
                        Direction = message.Direction.ToString(),
                        MessagingServiceSid = message.MessagingServiceSid,
                        SId = message.Sid,
                        Uri = message.Uri,
                        Status = message.Status.ToString()
                    };


                    Int32.TryParse(message.NumMedia, out int aux);
                    resourceContent.NumMedia = aux;



                    Int32.TryParse(message.NumSegments, out aux);
                    resourceContent.NumSegments = aux;

                }
                else
                {
                    resourceContent = null;
                }

            }
            catch (Exception e)
            {
                resourceContent = null;
            }

            return resourceContent;

        }

        private static MessageResourceContent SendPlainTextWhatsappMsg(string msg, string senderPhoneNumber, string receiverPhoneNumber, int? validitySeconds)
        {
            MessageResourceContent resourceContent;

            try
            {
                TwilioClient.Init(Settings.Default.twilioAccountSID, Settings.Default.twilioAuthToken);

                var message = MessageResource.Create(
                    body: msg,
                    from: new Twilio.Types.PhoneNumber("whatsapp:" + senderPhoneNumber),
                    to: new Twilio.Types.PhoneNumber("whatsapp:" + receiverPhoneNumber),
                    attempt: MaxAttempts,
                    statusCallback: new Uri(StatusCallbackUri)
                );

                if(message != null)
                {
                    resourceContent = new MessageResourceContent
                    {
                        ApiVersion = message.ApiVersion,
                        CreatedDate = message.DateCreated,
                        Direction = message.Direction.ToString(),
                        MessagingServiceSid = message.MessagingServiceSid,
                        SId = message.Sid,
                        Uri = message.Uri,
                        Status = message.Status.ToString()
                    };


                    Int32.TryParse(message.NumMedia, out int aux);
                    resourceContent.NumMedia = aux;



                    Int32.TryParse(message.NumSegments, out aux);
                    resourceContent.NumSegments = aux;

                }
                else
                {
                    resourceContent = null;
                }

            }
            catch (Exception)
            {
                resourceContent = null;
            }

            return resourceContent;
        }

        #endregion
    }
}