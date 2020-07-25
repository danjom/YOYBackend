using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefappInstallations
    {
        public Guid Id { get; set; }
        public string InstallationId { get; set; }
        public string Username { get; set; }
        public int DeviceType { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastDate { get; set; }

        public virtual AspNetUsers UsernameNavigation { get; set; }
    }
}
