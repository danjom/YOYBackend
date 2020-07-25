using YOY.DTO.Entities;

namespace YOY.DTO.Entities.Misc.TextProcessing.TicketValidation
{
    public class RequestedValidationFullData
    {
        public ReceiptRequestedValidation RequestedValidation { set; get; }
        public RequestedValidationReferenceData ReferenceData { set; get; }
    }
}