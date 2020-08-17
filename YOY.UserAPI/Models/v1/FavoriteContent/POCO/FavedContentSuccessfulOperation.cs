using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.FavoriteContent.POCO
{
    public class FavedContentSuccessfulOperation
    {
        public int OperationType { set; get; }
        public Guid CommerceId { set; get; }
        public int ContentType { set; get; }
        public Guid ContentId { set; get; }
        public string Message { set; get; }
    }
}
