using Microsoft.AspNetCore.Http;
using System;

namespace YOY.AdminCore.Miscellaneous.Models.File
{
    public class IFormFileImgWrapper
    {
        public Guid ReferenceId { get; set; }

        public int ImgType { get; set; }

        public IFormFile File { get; set; }
    }
}
