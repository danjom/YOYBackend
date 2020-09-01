using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.CashierAPI.Models.v1.IdentityModel
{
    public class AppUser : IdentityUser
    {
        [Key]
        override public string Id { get; set; }

        [Required]
        public string Name { set; get; }
        public string ProfilePicUrl { set; get; }
    }
}
