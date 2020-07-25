using System;

namespace YOY.ThirdpartyServices.ResponseModels.FCM
{
    public class PushNotificationSendingResponse
    {
        public bool Successful
        {
            get;
            set;
        }

        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }
    }
}
