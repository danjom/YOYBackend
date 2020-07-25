using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class HardwareIOTDevice
    {
        public Guid Id { set; get; }
        public string Alias { set; get; }
        public string UniqueKey { set; get; }
        public Guid? TenantId { set; get; }
        public Guid? BranchId { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public string FirmwareVersion { set; get; }
        public string HardwareVersion { set; get; }
        public bool IsActive { set; get; }
        public DateTime? LastRequestDate { set; get; }
        public int EffectiveRequestCount { set; get; }
        public DateTime? LastMaintenanceDate { set; get; }
        public DateTime? InstallationDate { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
