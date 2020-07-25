
namespace EnjoyIt.DTO.Services.SMS.Nexmo
{
    public class MessageStatus
    {
        public string MessageId { get; set; }
        public string To { get; set; }
        public string ClientRef { set; get; }
        public string Status { get; set; }
        public string ErrorText { get; set; }
        public string RemainingBalance { get; set; }
        public string MessagePrice { get; set; }
        public string Network { set; get; }
    }
}
