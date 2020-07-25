using System;

namespace YOY.DTO.Entities.Misc.TextProcessing.TicketValidation
{
    public class RequestedValidationReferenceData
    {
        public Guid Id { set; get; }
        public int Type { set; get; }
        public DateTime CreationDate { set; get; }
        public string ValidatorString { set; get; }
        public string RefCode { set; get; }
    }
}