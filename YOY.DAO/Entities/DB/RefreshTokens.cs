using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class RefreshTokens
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime? ExpiresUtc { get; set; }
        public bool Revoked { get; set; }
        public string HashedValue { get; set; }
    }
}
