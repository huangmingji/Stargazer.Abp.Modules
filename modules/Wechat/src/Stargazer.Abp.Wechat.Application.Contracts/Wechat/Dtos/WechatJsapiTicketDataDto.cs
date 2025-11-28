using Volo.Abp.Application.Dtos;

namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat
{
    public class WechatJsapiTicketDataDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 公众号appid
        /// </summary>
        /// <value>The appid.</value>
        public string AppId { get; set; } = "";

        /// <summary>
        /// 公众号用于调用微信JS接口的临时票据
        /// </summary>
        /// <value>The jsapi ticket.</value>
        public string JsapiTicket { get; set; } = "";

        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        /// <value>The expires in.</value>
        public int ExpiresIn { get; set; }
    }
}

