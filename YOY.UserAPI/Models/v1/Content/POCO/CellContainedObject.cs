using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class CellContainedObject
    {
        public int ContentType { set; get; }
        public Guid Id { set; get; }
        public Guid CommerceId { set; get; }
    }
}
