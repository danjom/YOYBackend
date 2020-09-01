using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.CashierAPI.Models.Authentication
{
    public class TokenRevokeRequest
    {
        [Required]
        public string userName { set; get; }

        [Required]
        public string refreshToken { set; get; }
    }
}
