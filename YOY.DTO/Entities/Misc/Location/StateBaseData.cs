using System;

namespace YOY.DTO.Entities.Misc.Location
{
    public class StateBaseData
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public decimal Latitude { set; get; }
        public decimal Longitude { set; get; }
    }
}
