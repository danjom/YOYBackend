﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Services.Search.Algolia
{
    public class UpdateCashbackIncentiveSearchableObject : UpdateSearchableObjectCore
    {
        public string SearchKey { set; get; }
        public string Details { set; get; }
        public double CashbackPercentage { set; get; }
    }
}