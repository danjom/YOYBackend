﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.Authentication.RefreshTokenGeneration
{
    public class RefreshTokenValue
    {
        public string Value { set; get; }
        public string HashedValue { set; get; }
    }
}
