using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.User.POCO
{
    public class UserProfileUpdatedField
    {
        [Required]
        [NotNull]
        public string UserId { set; get; }
        [Required]
        [NotNull]
        public string FieldValue { set; get; }
        [Required]
        public int FieldType { set; get; }

        public override string ToString()
        {
            return "UserId: " + UserId + ", FieldType: " + FieldType +
                ", FieldValue: " + FieldValue;
        }
    }
}