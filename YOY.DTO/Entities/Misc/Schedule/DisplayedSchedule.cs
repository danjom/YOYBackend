using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities.Misc.Schedule
{
    public class DisplayedSchedule
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid ReferenceId { set; get; }

        [Display(Name = "FromDay", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources),
                  ErrorMessageResourceName = "FromDayRequired")]
        public int FromDay { set; get; }

        [Display(Name = "ToDay", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources),
                  ErrorMessageResourceName = "ToDayRequired")]
        public int ToDay { set; get; }

        [Display(Name = "FromHour", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources),
                  ErrorMessageResourceName = "FromHourRequired")]
        public string BeginningTime { set; get; }

        [Display(Name = "ToHour", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources),
                  ErrorMessageResourceName = "ToHourRequired")]
        public string EndTime { set; get; }

        public DateTime CreationDate { set; get; }
    }
}
