using System;

namespace YOY.DTO.Entities
{
    public class Notification
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public int TypeId { set; get; }
        public string UserId { set; get; }
        public string Subject { set; get; }
        public string Content { set; get; }
        public DateTime CreationDate { set; get; }
        public bool Displayed { set; get; }
        public string Icon { set; get; }

    }
}
