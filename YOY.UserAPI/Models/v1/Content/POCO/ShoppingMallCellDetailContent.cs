﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class ShoppingMallCellDetailContent : CellDetailContent
    {
        public string ImgUrl { set; get; }
        public string MainName { set; get; }
        public string BranchName { set; get; }
    }
}
