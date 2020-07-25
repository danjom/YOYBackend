using System;

namespace YOY.DTO.Entities.Misc.Location
{
    public class City
    {
        public Guid Id { set; get; }
        public Guid StateId { set; get; }
        public string Name { set; get; }
        public int UtcDiffTime { set; get; }
        public bool IsActive { set; get; }
    }
}
