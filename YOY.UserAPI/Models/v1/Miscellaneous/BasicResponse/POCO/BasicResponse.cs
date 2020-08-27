﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO
{
    public class BasicResponse
    {
        public int StatusCode { set; get; }
        public int CustomAction { set; get; }
        public bool DisplayMsgToUser { set; get; }
        public string DevError { set; get; }
        public string MsgContent { set; get; }
        public string MsgTitle { set; get; }
        public object ExtraData { set; get; }
    }
}
