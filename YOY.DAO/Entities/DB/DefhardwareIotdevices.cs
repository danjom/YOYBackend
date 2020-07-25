using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefhardwareIotdevices
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string UniqueKey { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string FirmwareVersion { get; set; }
        public string HardwareVersion { get; set; }
        public bool? IsActive { get; set; }
        public bool Deleted { get; set; }
        public DateTime? LastRequestDate { get; set; }
        public int EffectiveRequestsCount { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
