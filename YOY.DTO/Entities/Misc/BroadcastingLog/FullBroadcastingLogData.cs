using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.TenantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.BroadcastingLog
{
    public class FullBroadcastingLogData
    {
        public Guid Id { set; get; }
        public Guid BroadcasterId { set; get; }
        public int BroadcasterType { set; get; }
        public int ContainedContentCount { set; get; }
        public string UserId { set; get; }
        public DateTime SentDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string MsgTitle { set; get; }
        public string MsgContent { set; get; }
        public List<BroadcastingLogContentFullData> ContentList { set; get; }
    }
}
