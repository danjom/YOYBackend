using System;

namespace YOY.DTO.Entities.Misc.Location
{
    public class District
    {
        public Guid Id { set; get; }
        public Guid CityId { set; get; }
        public string Name { set; get; }
        public bool IsActive { set; get; }
    }
}
