using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class UserAction
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string UserId { set; get; }
        public int ActionType { set; get; }

        [Display(Name = "ActionType", ResourceType = typeof(Resources))]
        public string ActionTypeName { set; get; }
        public Guid? Reference { set; get; }
        public int ReferenceType { set; get; }
        public string ReferenceTypeName { set; get; }
        public DateTime StartDate { set; get; }
        public long? ElapsedSeconds { set; get; }
    }
}
