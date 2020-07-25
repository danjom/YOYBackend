﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Miscellaneous.Location.POCO
{
    public class ProcessedClaimLocation
    {
        public bool ValidLocation { set; get; }
        public double? Latitude { set; get; }
        public double? Longitude { set; get; }
        public string SignalId { set; get; }
    }
}
