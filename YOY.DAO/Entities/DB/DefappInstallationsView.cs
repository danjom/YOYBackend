using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefappInstallationsView
    {
        public Guid Id { get; set; }
        public string InstallationId { get; set; }
        public string Username { get; set; }
        public int DeviceType { get; set; }
        public DateTime LastDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public long AccountNumber { get; set; }
        public string AccountCode { get; set; }
        public string Name { get; set; }
    }
}
