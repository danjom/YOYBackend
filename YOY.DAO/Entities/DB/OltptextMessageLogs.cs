using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltptextMessageLogs
    {
        public Guid Id { get; set; }
        public string ReceiverUserId { get; set; }
        public Guid? TenantId { get; set; }
        public int ReferenceType { get; set; }
        public Guid? ReferenceId { get; set; }
        public string SenderPhoneNumber { get; set; }
        public string TargetPhoneNumber { get; set; }
        public string Message { get; set; }
        public string LocationData { get; set; }
        public string MediaUrl { get; set; }
        public string ContainedCode { get; set; }
        public int PurposeType { get; set; }
        public int ChannelType { get; set; }
        public int Status { get; set; }
        public int Gateway { get; set; }
        public string GatewayMsgId { get; set; }
        public int GatewayMediaCount { get; set; }
        public decimal? GatewayPrice { get; set; }
        public string GatewayPriceCurrency { get; set; }
        public string GatewayMsgStatus { get; set; }
        public string GatewayDirection { get; set; }
        public int? GatewayErrorCode { get; set; }
        public string GatewayErrorMsg { get; set; }
        public DateTime GatewaySentDate { get; set; }
        public DateTime GatewayUpdateDate { get; set; }
        public string GatewayMsgUri { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
