using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.VisitorInspector.POCO
{
    public class VisitorDeviceInfo
    {
        [Required]
        [AllowNull]
        public long? AccNumber { set; get; }
        [Required]
        [NotNull]
        public int DeviceType { set; get; }
        [Required]
        [NotNull]
        [DataType(DataType.Text)]
        public string DeviceModel { set; get; }
        [Required]
        [NotNull]
        [DataType(DataType.Text)]
        public string OsVersion { set; get; }
        [Required]
        [AllowNull]
        public double? Latitude { set; get; }
        [Required]
        [AllowNull]
        public double? Longitude { set; get; }
    }
}
