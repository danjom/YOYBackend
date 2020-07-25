using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class TextMessageLog
    {
        public Guid Id { set; get; }
        public string ReceiverUserId { set; get; }
        public Guid? TenantId { set; get; }
        public int ReferenceType { set; get; }
        public string ReferenceTypeName { set; get; }
        public Guid? ReferenceId { set; get; }
        public string SenderPhoneNumber { set; get; }
        public string TargerPhoneNumber { set; get; }
        public string Message { set; get; }
        public string LocationData { set; get; }
        public string MediaUrl { set; get; }
        public string ContainedCode { set; get; }
        public int PurposeType { set; get; }
        public string PurposeTypeName { set; get; }
        public int ChannelType { set; get; }
        public string ChannelTypeName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public int Gateway { set; get; }
        public string GatewayName { set; get; }
        public string GatewayMessageId { set; get; }
        public int GatewayMediaCount { set; get; }
        public decimal? GatewayPrice { set; get; }
        public string GatewayPriceCurrency { set; get; }
        public string GatewayMsgStatus { set; get; }
        public string GatewayDirection { set; get; }
        public int? GatewayErrorCode { set; get; }
        public string GatewayErrorMsg { set; get; }
        public string GatewayMsgUri { set; get; }
        public DateTime GatewaySentDate { set; get; }
        public DateTime GatewayUpdateDate { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime? ExpirationDate { set; get; }

    }
}
