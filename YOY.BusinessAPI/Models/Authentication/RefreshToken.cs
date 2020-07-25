using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.Authentication
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string ClientId { set; get; }
        public DateTime IssuedUTC { set; get; }
        public DateTime? ExpiresUTC { set; get; }
        public bool Revoked { get; set; }
        public string Value { get; set; }
    }
}
