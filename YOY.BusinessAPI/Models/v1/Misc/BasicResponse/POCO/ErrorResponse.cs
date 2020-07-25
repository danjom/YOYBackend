using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.Misc.BasicResponse.POCO
{
    public class ErrorResponse
    {
        public int ErrorCode { set; get; }
        public bool ShowErrorToUser { set; get; }
        public string InnerError { set; get; }
        public string PublicError { set; get; }
    }
}
