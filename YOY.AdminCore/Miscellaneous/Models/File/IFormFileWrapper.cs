using Microsoft.AspNetCore.Http;
using System;

namespace YOY.AdminCore.Miscellaneous.Models.File
{
    public class IFormFileWrapper
    {
        public Guid ReferenceId { get; set; }

        public string MimeType { get; set; }

        public IFormFile File { get; set; }
    }
}
