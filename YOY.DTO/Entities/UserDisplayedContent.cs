using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities
{
    public class UserDisplayedContent
    {
        public Guid Id { set; get; }
        public string UserId { set; get; }
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public Guid? OwnerId { set; get; }
        public int TargetScreen { set; get; }
        public DateTime CreatedDate { set; get; }
    }
}
