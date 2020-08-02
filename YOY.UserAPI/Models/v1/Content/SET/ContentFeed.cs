using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.SET
{
    public class ContentFeed
    {
        public int ReferenceType { set; get; }
        public Guid ReferenceId { set; get; }
        public string UserId { set; get; }
        public int StructresCount { set; get; }
        public int FeedType { set; get; }
        public int ContentRetrieveType { set; get; }
        public List<ContentStructure> ContentStructures { set; get; }
    }
}
