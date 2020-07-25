using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class BTLContent
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid CategoryId { set; get; }
        public string CategoryName { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Keywords { set; get; }
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public int ContentType { set; get; }
        public string ContentTypeName { set; get; }
        public int ObjectiveType { set; get; }
        public string ObjectiveTypeName { set; get; }
        public int GeoSegmentationType { set; get; }
        public string GeoSegmentationTypeName { set; get; }
        public Guid? DisplayImgId { set; get; }
        public int ViewCount { set; get; }
        public int SavedCount { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string PublishState { set; get; }
        public bool IsActive { set; get; }

        //RELEASE DATE COMPONENTS
        public DateTime? ReleaseDate { set; get; }
        public string ReleaseHour { set; get; }

        //EXPIRATION DATE COMPONENTS
        public DateTime ExpirationDate { set; get; }
        public string ExpirationHour { set; get; }
    }
}
