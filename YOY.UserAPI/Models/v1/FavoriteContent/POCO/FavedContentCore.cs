using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.FavoriteContent.POCO
{
    public class FavedContentCore
    {
        [Required]
        [NotNull]
        [DataType(DataType.Text)]
        public string UserId { set; get; }
        [Required]
        [NotNull]
        public Guid CommerceId { set; get; }
        [Required]
        [NotNull]
        public int ContentType { set; get; }
        [Required]
        [NotNull]
        public Guid ContentId { set; get; }
    }
}
