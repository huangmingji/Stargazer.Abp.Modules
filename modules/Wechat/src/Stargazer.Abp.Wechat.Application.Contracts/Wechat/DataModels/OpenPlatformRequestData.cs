using System;
using System.Xml.Serialization;

namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels
{
    [XmlRoot("xml")]
    public class OpenPlatformRequestData : RequestData
    {
        /// <summary>
        /// 第三方平台appid
        /// </summary>
        public string AppId { get; set; } = "";

        /// <summary>
        /// component_verify_ticket, 如为none，代表该消息推送给服务
        /// </summary>
        public string InfoType { get; set; } = "";

        /// <summary>
        /// Ticket内容
        /// </summary>
        public string ComponentVerifyTicket { get; set; } = "";

        /// <summary>
        /// 取消授权公众号appid
        /// </summary>
        public string AuthorizerAppid { get; set; } = "";

        /// <summary>
        /// 授权码
        /// </summary>
        public string AuthorizationCode { get; set; } = "";

        /// <summary>
        /// 过期时间
        /// </summary>
        public string AuthorizationCodeExpiredTime { get; set; } = "";
    }
}

