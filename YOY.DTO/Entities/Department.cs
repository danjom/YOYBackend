using YOY.Values.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class Department
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public bool IsActive { set; get; }
        public bool CoversLocation { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public List<Category> Categories { set; get; }
    }
}
