using System;

namespace YOY.DTO.Entities
{
    public class OfferPreference
    {
        public Guid Id { set; get; }
        public Guid OfferId { set; get; }
        public string Name { set; get; }
        public string Hint { set; get; }
        public int InputType { set; get; }
        public string InputTypeName { set; get; }
        public int MinOptionsSelected { set; get; }
        public int MaxOptionsSelected { set; get; }
        public bool Mandatory { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
