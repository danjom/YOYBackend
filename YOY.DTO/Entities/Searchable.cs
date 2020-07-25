using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class Searchable
    {
        public Guid TenantId { set; get; }
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public string Name { set; get; }
        public string Icon { set; get; }
        public string Classification { set; get; }
        public string Category { set; get; }
        public string Keywords { set; get; }
        public string Details { set; get; }
        public int ContentType { set; get; }
        public bool IsActive { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime? ExpirationDate { set; get; }
        public Guid IndexId { set; get; }
        public string IndexName { set; get; }
        public string AppName { set; get; }
        public int Service { set; get; }
        public DateTime? LastSearch { set; get; }
        public long SearchCount { set; get; }
        public Guid? CountryId { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
