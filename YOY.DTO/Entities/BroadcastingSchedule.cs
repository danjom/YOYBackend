using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class BroadcastingSchedule
    {
        public Guid Id { set; get; }
        public Guid ContentId { set; get; }
        public int ContentType { set; get; }

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
        public int FromHour { set; get; }

        [Display(Name = "FromHour", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources),
                  ErrorMessageResourceName = "FromHourRequired")]
        public int FromMinutes { set; get; }

        [Display(Name = "ToHour", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources),
                  ErrorMessageResourceName = "ToHourRequired")]
        public int ToHour { set; get; }

        [Display(Name = "ToHour", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources),
                  ErrorMessageResourceName = "ToHourRequired")]
        public int ToMinutes { set; get; }

        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
