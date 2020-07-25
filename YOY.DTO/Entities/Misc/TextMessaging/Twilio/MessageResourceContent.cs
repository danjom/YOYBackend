using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities.Misc.TextMessaging.Twilio
{
    public class MessageResourceContent
    {
        public string ApiVersion { set; get; }
        public DateTime? CreatedDate { set; get; }
        public string Direction { set; get; }
        public string MessagingServiceSid { set; get; }
        public int NumMedia { set; get; }
        public int NumSegments { set; get; }
        public string SId { set; get; }
        public string Status { set; get; }
        public string Uri { set; get; }
    }
}
