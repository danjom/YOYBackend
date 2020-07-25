using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.ValidationAPI.APIKeyAuth.Models.v1.Misc.BasicResponse.POCO
{
    public class ErrorResponse
    {
        public int StatusCode { set; get; }
        public bool DisplayMsgToUser { set; get; }
        public string DevError { set; get; }
        public string MsgContent { set; get; }
        public string MsgTitle { set; get; }
    }
}
