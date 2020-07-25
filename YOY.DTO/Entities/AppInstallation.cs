using System;

namespace YOY.DTO.Entities
{
    public class AppInstallation
    {
        public Guid Id { set; get; }
        public string InstallationId { set; get; }
        public string Username { set; get; }
        public int DeviceType { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime LastLoginDate { set; get; }
        public string UserId { set; get; }
        public long AccountNumber { set; get; }
        public string AccountCode { set; get; }
        public string Name { set; get; }
    }
}
