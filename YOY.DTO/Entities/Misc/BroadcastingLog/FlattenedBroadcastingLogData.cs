using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.Offer;
using YOY.DTO.Entities.Misc.InterestPreference;
using YOY.DTO.Entities.Misc.TenantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.BroadcastingLog
{
    public class FlattenedBroadcastingLogData
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
        public Guid LogRecordReferenceId { set; get; }
        public int LogRecordReferenceType { set; get; }
        public DateTime LogRecordExpirationDate { set; get; }
        public int BroadcastingTimerType { set; get; }
        public int BroadcastingMinsToRedeem { set; get; }
        public Entities.Offer Offer { set; get; }
        public BasicBranchData Branch { set; get; }
        public BasicUserPreferenceData Preference { set; get; }
        public BasicTenantData Tenant { set; get; }
        public bool ExactLocationBased { set; get; }
    }
}
