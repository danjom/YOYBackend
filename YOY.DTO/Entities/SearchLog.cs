using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class SearchLog
    {
        public string UserId { set; get; }
        public Guid Reference { set; get; }
        public int ReferenceType { set; get; }
        public int ContentType { set; get; }
        public string Name { set; get; }
        public string Details { set; get; }
        public string Icon { set; get; }
        public DateTime Date { set; get; }
        public int SearchCount { set; get; }
        public DateTime? ReleaseDate { set; get; }
        public DateTime? ExpirationDate { set; get; }

    }
}
