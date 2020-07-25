using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.Misc.BasicResponse.POCO
{
    public class SuccessResponse
    {
        public int StatusCode { set; get; }
        public bool ShowMsgToUser { set; get; }
        public string MessageToDisplay { set; get; }
    }
}
