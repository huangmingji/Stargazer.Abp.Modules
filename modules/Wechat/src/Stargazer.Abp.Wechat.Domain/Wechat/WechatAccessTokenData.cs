using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.Wechat.Domain.Wechat
{
    public class WechatAccessTokenData : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public WechatAccessTokenData() { }

        public WechatAccessTokenData(Guid id, string appId, string accessToken, int expiresIn) : base(id)
        {
            this.AppId = appId;
            this.AccessToken = accessToken;
            this.ExpiresIn = expiresIn;
        }

        public Guid? TenantId { get; set; }

        /// <summary>
        /// 公众号appid
        /// </summary>
        /// <value>The appid.</value>
        public string AppId { get; set; } = "";

        /// <summary>
        /// 公众号的全局唯一接口调用凭据
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; } = "";

        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        /// <value>The expires in.</value>
        public int ExpiresIn { get; set; }


        public void SetData(string appId, string accessToken, int expiresIn)
        {
            this.AppId = appId;
            this.AccessToken = accessToken;
            this.ExpiresIn = expiresIn;
        }
    }
}