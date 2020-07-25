using EnjoyIt.DTO.Services.SMS.Nexmo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;
namespace YOY.DAO.Entities.Manager
{
    public class TextMessageLogManager
    {
        #region PROPERTIES_AND_RESOURCES

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private readonly BusinessObjects _businessObjects;

        #endregion

        #region METHODS

        private string GetPurposeTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case TextMessageLogPurposeTypes.AccountValidation:
                    typeName = Resources.AccountValidation;
                    break;
                case TextMessageLogPurposeTypes.Marketing:
                    typeName = Resources.Marketing;
                    break;
                case TextMessageLogPurposeTypes.GeneratePaymentRequest:
                    typeName = Resources.GeneratePaymentRequest;
                    break;
                case TextMessageLogPurposeTypes.DispatchPurchase:
                    typeName = Resources.DispatchPurchase;
                    break;
            }

            return typeName;
        }
        private string GetChannelTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case TextMessageChannels.SMS:
                    typeName = "SMS";
                    break;
                case TextMessageChannels.Whatsapp:
                    typeName = "Whatsapp";
                    break;
            }

            return typeName;
        }

        private string GetStatusName(int status)
        {
            string statusName = "";

            switch (status)
            {
                case TextMessageLogStatuses.Unknown:
                    statusName = Resources.Unknown;
                    break;
                case TextMessageLogStatuses.SentRequested:
                    statusName = Resources.SentRequested;
                    break;
                case TextMessageLogStatuses.SuccessfullyDelivered:
                    statusName = Resources.SuccessfullyDelivered;
                    break;
                case TextMessageLogStatuses.ReadByUser:
                    statusName = Resources.ReadByUser;
                    break;
                case TextMessageLogStatuses.FailedDelivery:
                    statusName = Resources.FailedDelivery;
                    break;
            }

            return statusName;
        }

        private string GetReferenceTypeName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case TextMessageLogReferenceTypes.None:
                    typeName = Resources.None;
                    break;
                case TextMessageLogReferenceTypes.Offer:
                    typeName = Resources.Offer;
                    break;
                case TextMessageLogReferenceTypes.CashbackIncentive:
                    typeName = Resources.CashbackIncentive;
                    break;
            }

            return typeName;
        }

        private string GetGatewayName(int type)
        {
            string typeName = "";

            switch (type)
            {
                case TextMessageGateways.Twilio:
                    typeName = Resources.Twilio;
                    break;
            }

            return typeName;
        }

        public List<TextMessageLog> Gets(string receiverUserId, Guid? tenantId, int referenceType, Guid? referenceId, int purposeType, int status, int gateway, int pageSize, int pageNumber)
        {
            List<TextMessageLog> smsLogs = null;

            try
            {
                var query = (dynamic)null;

                if (!string.IsNullOrEmpty(receiverUserId))
                {
                    if (tenantId != null)
                    {
                        if (referenceType != TextMessageLogReferenceTypes.All)
                        {
                            if (referenceId != null)
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceType == referenceType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (referenceId != null)
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceId == referenceId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceId == referenceId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceId == referenceId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceId == referenceId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.ReferenceId == referenceId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.TenantId == tenantId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (referenceType != TextMessageLogReferenceTypes.All)
                        {
                            if (referenceId != null)
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.ReferenceId == referenceId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceType == referenceType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (referenceId != null)
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceId == referenceId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceId == referenceId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceId == referenceId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceId == referenceId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.ReferenceId == referenceId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReceiverUserId == receiverUserId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (tenantId != null)
                    {
                        if (referenceType != TextMessageLogReferenceTypes.All)
                        {
                            if (referenceId != null)
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceType == referenceType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (referenceId != null)
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceId == referenceId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceId == referenceId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceId == referenceId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceId == referenceId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.ReferenceId == referenceId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.TenantId == tenantId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (referenceType != TextMessageLogReferenceTypes.All)
                        {
                            if (referenceId != null)
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.ReferenceId == referenceId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceType == referenceType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (referenceId != null)
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceId == referenceId && x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceId == referenceId && x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceId == referenceId && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceId == referenceId && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceId == referenceId && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.ReferenceId == referenceId
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (purposeType != TextMessageLogPurposeTypes.All)
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.PurposeType == purposeType && x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.PurposeType == purposeType && x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.PurposeType == purposeType && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.PurposeType == purposeType
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != TextMessageLogStatuses.All)
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.Status == status && x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.Status == status
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                    else
                                    {
                                        if (gateway != TextMessageGateways.All)
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     where x.Gateway == gateway
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                        else
                                        {
                                            query = (from x in this._businessObjects.Context.OltptextMessageLogs
                                                     orderby x.GatewaySentDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (query != null)
                {
                    smsLogs = new List<TextMessageLog>();
                    TextMessageLog smsLog;

                    foreach (OltptextMessageLogs item in query)
                    {
                        smsLog = new TextMessageLog
                        {
                            Id = item.Id,
                            ReceiverUserId = item.ReceiverUserId,
                            TenantId = item.TenantId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetReferenceTypeName(item.ReferenceType),
                            ReferenceId = item.ReferenceId,
                            SenderPhoneNumber = item.SenderPhoneNumber,
                            TargerPhoneNumber = item.TargetPhoneNumber,
                            Message = item.Message,
                            LocationData = item.LocationData,
                            MediaUrl = item.MediaUrl,
                            ContainedCode = item.ContainedCode,
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            ChannelType = item.ChannelType,
                            ChannelTypeName = GetChannelTypeName(item.PurposeType),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            Gateway = item.Gateway,
                            GatewayName = GetGatewayName(item.Gateway),
                            GatewayMessageId = item.GatewayMsgId,
                            GatewayMediaCount = item.GatewayMediaCount,
                            GatewayPrice = item.GatewayPrice,
                            GatewayPriceCurrency = item.GatewayPriceCurrency,
                            GatewayMsgStatus = item.GatewayMsgStatus,
                            GatewayDirection = item.GatewayDirection,
                            GatewayErrorCode = item.GatewayErrorCode,
                            GatewayErrorMsg = item.GatewayErrorMsg,
                            GatewayMsgUri = item.GatewayMsgUri,
                            GatewaySentDate = item.GatewaySentDate,
                            GatewayUpdateDate = item.GatewayUpdateDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        smsLogs.Add(smsLog);
                    }
                }
            }
            catch (Exception e)
            {
                smsLogs = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return smsLogs;
        }

        public List<TextMessageLog> Gets(string receiverUserId, string containedMessage)
        {
            List<TextMessageLog> smsLogs = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltptextMessageLogs
                            where x.ReceiverUserId == receiverUserId && x.Message.Contains(containedMessage)
                            select x;

                if (query != null)
                {
                    smsLogs = new List<TextMessageLog>();
                    TextMessageLog smsLog;

                    foreach (OltptextMessageLogs item in query)
                    {
                        smsLog = new TextMessageLog
                        {
                            Id = item.Id,
                            ReceiverUserId = item.ReceiverUserId,
                            TenantId = item.TenantId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetReferenceTypeName(item.ReferenceType),
                            ReferenceId = item.ReferenceId,
                            SenderPhoneNumber = item.SenderPhoneNumber,
                            TargerPhoneNumber = item.TargetPhoneNumber,
                            Message = item.Message,
                            LocationData = item.LocationData,
                            MediaUrl = item.MediaUrl,
                            ContainedCode = item.ContainedCode,
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            ChannelType = item.ChannelType,
                            ChannelTypeName = GetChannelTypeName(item.PurposeType),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            Gateway = item.Gateway,
                            GatewayName = GetGatewayName(item.Gateway),
                            GatewayMessageId = item.GatewayMsgId,
                            GatewayMediaCount = item.GatewayMediaCount,
                            GatewayPrice = item.GatewayPrice,
                            GatewayPriceCurrency = item.GatewayPriceCurrency,
                            GatewayMsgStatus = item.GatewayMsgStatus,
                            GatewayDirection = item.GatewayDirection,
                            GatewayErrorCode = item.GatewayErrorCode,
                            GatewayErrorMsg = item.GatewayErrorMsg,
                            GatewayMsgUri = item.GatewayMsgUri,
                            GatewaySentDate = item.GatewaySentDate,
                            GatewayUpdateDate = item.GatewayUpdateDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate
                        };

                        smsLogs.Add(smsLog);
                    }
                }
            }
            catch (Exception e)
            {
                smsLogs = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return smsLogs;
        }

        public TextMessageLog Get(Guid id)
        {
            TextMessageLog smsLog = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltptextMessageLogs
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (OltptextMessageLogs item in query)
                    {
                        smsLog = new TextMessageLog
                        {
                            Id = item.Id,
                            ReceiverUserId = item.ReceiverUserId,
                            TenantId = item.TenantId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetReferenceTypeName(item.ReferenceType),
                            ReferenceId = item.ReferenceId,
                            SenderPhoneNumber = item.SenderPhoneNumber,
                            TargerPhoneNumber = item.TargetPhoneNumber,
                            Message = item.Message,
                            LocationData = item.LocationData,
                            MediaUrl = item.MediaUrl,
                            ContainedCode = item.ContainedCode,
                            PurposeType = item.PurposeType,
                            PurposeTypeName = GetPurposeTypeName(item.PurposeType),
                            ChannelType = item.ChannelType,
                            ChannelTypeName = GetChannelTypeName(item.PurposeType),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            Gateway = item.Gateway,
                            GatewayName = GetGatewayName(item.Gateway),
                            GatewayMessageId = item.GatewayMsgId,
                            GatewayMediaCount = item.GatewayMediaCount,
                            GatewayPrice = item.GatewayPrice,
                            GatewayPriceCurrency = item.GatewayPriceCurrency,
                            GatewayMsgStatus = item.GatewayMsgStatus,
                            GatewayDirection = item.GatewayDirection,
                            GatewayErrorCode = item.GatewayErrorCode,
                            GatewayErrorMsg = item.GatewayErrorMsg,
                            GatewayMsgUri = item.GatewayMsgUri,
                            GatewaySentDate = item.GatewaySentDate,
                            GatewayUpdateDate = item.GatewayUpdateDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            ExpirationDate = item.ExpirationDate
                        };

                    }
                }
            }
            catch (Exception e)
            {
                smsLog = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return smsLog;
        }

        public TextMessageLog Get(string userId, string phoneNumber, string mark, string containedCode, int purposeType, DateTime? date)
        {
            TextMessageLog smsLog = null;

            try
            {
                OltptextMessageLogs oltptextMessageLog = (dynamic)null;

                if(purposeType != TextMessageLogPurposeTypes.All)
                {

                    if (!string.IsNullOrWhiteSpace(containedCode))
                    {

                        if (!string.IsNullOrWhiteSpace(mark))
                        {
                            if (date != null)
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.PurposeType == purposeType && x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ContainedCode == containedCode && x.Message.Contains(mark) && x.ExpirationDate > date
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                            else
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.PurposeType == purposeType && x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ContainedCode == containedCode && x.Message.Contains(mark)
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                        }
                        else
                        {
                            if (date != null)
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.PurposeType == purposeType && x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ContainedCode == containedCode && x.ExpirationDate > date
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                            else
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.PurposeType == purposeType && x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ContainedCode == containedCode
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                        }
                    }
                    else
                    {

                        if (!string.IsNullOrWhiteSpace(mark))
                        {
                            if (date != null)
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.PurposeType == purposeType && x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.Message.Contains(mark) && x.ExpirationDate > date
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                            else
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.PurposeType == purposeType && x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.Message.Contains(mark)
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                        }
                        else
                        {
                            if (date != null)
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.PurposeType == purposeType && x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ExpirationDate > date
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                            else
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.PurposeType == purposeType && x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                        }
                    }
                }
                else
                {

                    if (!string.IsNullOrWhiteSpace(containedCode))
                    {

                        if (!string.IsNullOrWhiteSpace(mark))
                        {
                            if (date != null)
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ContainedCode == containedCode && x.Message.Contains(mark) && x.ExpirationDate > date
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                            else
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ContainedCode == containedCode && x.Message.Contains(mark)
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                        }
                        else
                        {
                            if (date != null)
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ContainedCode == containedCode && x.ExpirationDate > date
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                            else
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ContainedCode == containedCode
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                        }
                    }
                    else
                    {

                        if (!string.IsNullOrWhiteSpace(mark))
                        {
                            if (date != null)
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.Message.Contains(mark) && x.ExpirationDate > date
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                            else
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.Message.Contains(mark)
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                        }
                        else
                        {
                            if (date != null)
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber && x.ExpirationDate > date
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                            else
                            {
                                oltptextMessageLog = (from x in this._businessObjects.Context.OltptextMessageLogs
                                              where x.ReceiverUserId == userId && x.TargetPhoneNumber == phoneNumber
                                              orderby x.CreatedDate descending
                                              select x).FirstOrDefault();
                            }
                        }
                    }
                }
                    
                    

                if (oltptextMessageLog != null)
                {
                    smsLog = new TextMessageLog
                    {
                        Id = oltptextMessageLog.Id,
                        ReceiverUserId = oltptextMessageLog.ReceiverUserId,
                        TenantId = oltptextMessageLog.TenantId,
                        ReferenceType = oltptextMessageLog.ReferenceType,
                        ReferenceTypeName = GetReferenceTypeName(oltptextMessageLog.ReferenceType),
                        ReferenceId = oltptextMessageLog.ReferenceId,
                        SenderPhoneNumber = oltptextMessageLog.SenderPhoneNumber,
                        TargerPhoneNumber = oltptextMessageLog.TargetPhoneNumber,
                        Message = oltptextMessageLog.Message,
                        LocationData = oltptextMessageLog.LocationData,
                        MediaUrl = oltptextMessageLog.MediaUrl,
                        ContainedCode = oltptextMessageLog.ContainedCode,
                        PurposeType = oltptextMessageLog.PurposeType,
                        PurposeTypeName = GetPurposeTypeName(oltptextMessageLog.PurposeType),
                        ChannelType = oltptextMessageLog.ChannelType,
                        ChannelTypeName = GetChannelTypeName(oltptextMessageLog.PurposeType),
                        Status = oltptextMessageLog.Status,
                        StatusName = GetStatusName(oltptextMessageLog.Status),
                        Gateway = oltptextMessageLog.Gateway,
                        GatewayName = GetGatewayName(oltptextMessageLog.Gateway),
                        GatewayMessageId = oltptextMessageLog.GatewayMsgId,
                        GatewayMediaCount = oltptextMessageLog.GatewayMediaCount,
                        GatewayPrice = oltptextMessageLog.GatewayPrice,
                        GatewayPriceCurrency = oltptextMessageLog.GatewayPriceCurrency,
                        GatewayMsgStatus = oltptextMessageLog.GatewayMsgStatus,
                        GatewayDirection = oltptextMessageLog.GatewayDirection,
                        GatewayErrorCode = oltptextMessageLog.GatewayErrorCode,
                        GatewayErrorMsg = oltptextMessageLog.GatewayErrorMsg,
                        GatewayMsgUri = oltptextMessageLog.GatewayMsgUri,
                        GatewaySentDate = oltptextMessageLog.GatewaySentDate,
                        GatewayUpdateDate = oltptextMessageLog.GatewayUpdateDate,
                        CreatedDate = oltptextMessageLog.CreatedDate,
                        UpdatedDate = oltptextMessageLog.UpdatedDate,
                        ExpirationDate = oltptextMessageLog.ExpirationDate
                    };

                }
            }
            catch (Exception e)
            {
                smsLog = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return smsLog;
        }

        public TextMessageLog Post(string receiverUserId, Guid? tenantId, int referenceType, Guid? referenceId, string senderPhoneNumber, string targetPhoneNumber, string message, string locationData, string mediaUrl, string containedCode, int purposeType, int channelType, int status, int gateway, string gatewayMsgId, int gatewayMediaCount, string gatewayDirection, string gatewayMsgStatus, int? gatewayErrorCode, string gatewayErrorMsg, string gatewayMsgUri, DateTime gatewaySentDate, DateTime? expirationDate)
        {
            TextMessageLog messageLog;

            try
            {
                OltptextMessageLogs newLog = new OltptextMessageLogs
                {
                    Id = Guid.NewGuid(),
                    ReceiverUserId = receiverUserId,
                    TenantId = tenantId,
                    ReferenceType = referenceType,
                    ReferenceId = referenceId,
                    SenderPhoneNumber = senderPhoneNumber,
                    TargetPhoneNumber = targetPhoneNumber,
                    Message = message,
                    LocationData = locationData,
                    MediaUrl = mediaUrl,
                    ContainedCode = containedCode,
                    PurposeType = purposeType,
                    ChannelType = channelType,
                    Status = status,
                    Gateway = gateway,
                    GatewayMsgId = gatewayMsgId,
                    GatewayMediaCount = gatewayMediaCount,
                    GatewayDirection = gatewayDirection,
                    GatewayMsgStatus = gatewayMsgStatus,
                    GatewayErrorCode = gatewayErrorCode,
                    GatewayErrorMsg = gatewayErrorMsg,
                    GatewayMsgUri = gatewayMsgUri,
                    GatewaySentDate = gatewaySentDate,
                    GatewayUpdateDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    ExpirationDate = expirationDate
                };

                this._businessObjects.Context.OltptextMessageLogs.Add(newLog);
                this._businessObjects.Context.SaveChanges();

                messageLog = new TextMessageLog
                {
                    Id = newLog.Id,
                    ReceiverUserId = newLog.ReceiverUserId,
                    TenantId = newLog.TenantId,
                    ReferenceType = newLog.ReferenceType,
                    ReferenceTypeName = GetReferenceTypeName(newLog.ReferenceType),
                    ReferenceId = newLog.ReferenceId,
                    SenderPhoneNumber = newLog.SenderPhoneNumber,
                    TargerPhoneNumber = newLog.TargetPhoneNumber,
                    Message = newLog.Message,
                    LocationData = newLog.LocationData,
                    MediaUrl = newLog.MediaUrl,
                    ContainedCode = newLog.ContainedCode,
                    PurposeType = newLog.PurposeType,
                    PurposeTypeName = GetPurposeTypeName(newLog.PurposeType),
                    ChannelType = newLog.ChannelType,
                    ChannelTypeName = GetChannelTypeName(newLog.PurposeType),
                    Status = newLog.Status,
                    StatusName = GetStatusName(newLog.Status),
                    Gateway = newLog.Gateway,
                    GatewayName = GetGatewayName(newLog.Gateway),
                    GatewayMessageId = newLog.GatewayMsgId,
                    GatewayMediaCount = newLog.GatewayMediaCount,
                    GatewayPrice = newLog.GatewayPrice,
                    GatewayPriceCurrency = newLog.GatewayPriceCurrency,
                    GatewayMsgStatus = newLog.GatewayMsgStatus,
                    GatewayDirection = newLog.GatewayDirection,
                    GatewayErrorCode = newLog.GatewayErrorCode,
                    GatewayErrorMsg = newLog.GatewayErrorMsg,
                    GatewayMsgUri = newLog.GatewayMsgUri,
                    GatewaySentDate = newLog.GatewaySentDate,
                    GatewayUpdateDate = newLog.GatewayUpdateDate,
                    CreatedDate = newLog.CreatedDate,
                    UpdatedDate = newLog.UpdatedDate,
                    ExpirationDate = newLog.ExpirationDate
                };
            }
            catch (Exception e)
            {
                messageLog = null;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return messageLog;
        }

        public bool Put(Guid id, int status)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltptextMessageLogs
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltptextMessageLogs currentLog = null;

                    foreach (OltptextMessageLogs item in query)
                    {
                        currentLog = item;
                    }

                    if (currentLog != null)
                    {
                        currentLog.Status = status;
                        currentLog.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }


        public bool Put(string sId, string gatewayStatus)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltptextMessageLogs
                            where x.GatewayMsgId == sId
                            select x;

                if (query != null)
                {
                    OltptextMessageLogs currentLog = null;

                    foreach (OltptextMessageLogs item in query)
                    {
                        currentLog = item;
                    }

                    if (currentLog != null)
                    {
                        currentLog.GatewayMsgStatus = gatewayStatus;
                        currentLog.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }


        public bool Put(string sId, string gatewayStatus, decimal? price, string priceCurrency, int? errorCode, string errorMsg)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltptextMessageLogs
                            where x.GatewayMsgId == sId
                            select x;

                if (query != null)
                {
                    OltptextMessageLogs currentLog = null;

                    foreach (OltptextMessageLogs item in query)
                    {
                        currentLog = item;
                    }

                    if (currentLog != null)
                    {
                        currentLog.GatewayMsgStatus = gatewayStatus;
                        currentLog.GatewayPrice = price;
                        currentLog.GatewayPriceCurrency = priceCurrency;
                        currentLog.GatewayErrorCode = errorCode;
                        currentLog.GatewayErrorMsg = errorMsg;
                        currentLog.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR MANAGEMENT 
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return success;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new SmsLogManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public TextMessageLogManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD TEXT MESSAGE LOG MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
