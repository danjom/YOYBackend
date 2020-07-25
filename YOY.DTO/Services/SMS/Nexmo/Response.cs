using System.Collections.Generic;

namespace EnjoyIt.DTO.Services.SMS.Nexmo
{
    public class Response
    {
        public string Messagecount { get; set; }
        public List<MessageStatus> Messages { get; set; }
    }
}
