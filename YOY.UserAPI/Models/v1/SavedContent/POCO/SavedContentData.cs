using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.SavedContent.POCO
{
    public class SavedContentData
    {
        public string UserId { set; get; }
        public int ContentType { set; get; }
        public Guid ContentId { set; get; }
    }
}
