using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.Wechat.Domain.Wechat
{
    public class WechatJsapiTicketData : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public WechatJsapiTicketData(Guid id, string appId, string jsapiTicket, int expiresIn) : base(id)
        {
            this.AppId = appId;
            this.JsapiTicket = jsapiTicket;
            this.ExpiresIn = expiresIn;
        }

        public Guid? TenantId { get; set; }
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

        public void SetData(string appId, string jsapiTicket, int expiresIn)
        {
            this.AppId = appId;
            this.JsapiTicket = jsapiTicket;
            this.ExpiresIn = expiresIn;
        }
    }
}

