using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.BroadcastingDevice
{
    public class BroadcastingDevicePlacement
    {
        public Guid DepartmentId { set; get; }
        public Guid BranchId { set; get; }
        public Guid DeviceId { set; get; }
        public string InStoreLocation { set; get; }
    }
}
