using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class FeaturedSlide
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Guid? ImageId { set; get; }
        public Guid? TenantId { set; get; }
        public Guid CountryId { set; get; }
        public Guid? StateId { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public int? RouteContentType { set; get; }
        public string RouteContentTypeName { set; get; }
        public string AccessRoute { set; get; }
        public int MaxViews { set; get; }
        public bool IsActive { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
