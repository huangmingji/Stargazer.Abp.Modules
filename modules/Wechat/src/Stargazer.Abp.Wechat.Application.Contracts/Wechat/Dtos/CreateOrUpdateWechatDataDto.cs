using System;
using Stargazer.Abp.Wechat.Domain.Shared.Enumes;

namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos
{
	public class CreateOrUpdateWechatDataDto
	{
        /// <summary>
        /// 公众号appid
        /// </summary>
        /// <value>The appid.</value>
        public string AppId { get; set; } = "";

        /// <summary>
        /// 公众号appsecret
        /// </summary>
        /// <value>The appsecret.</value>
        public string AppSecret { get; set; } = "";

        /// <summary>
        /// 微信响应token验证
        /// </summary>
        /// <value>The token.</value>
        public string Token { get; set; } = "";

        /// <summary>
        /// 消息加密密钥由43位字符组成，可随机修改，字符范围为A-Z，a-z，0-9
        /// </summary>
        /// <value>The encoding AESK ey.</value>
        public string EncodingAESKey { get; set; } = "";

        public EncodeType EncodeType { get; set; } = EncodeType.None;

        /// <summary>
        /// 公众号的主体名称
        /// </summary>
        /// <value>The name.</value>
        public string PrincipalName { get; set; } = "";

        /// <summary>
        /// 授权方公众号的原始ID
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; } = "";

        /// <summary>
        /// 授权方公众号所设置的微信号
        /// </summary>
        /// <value>The alias.</value>
        public string Alias { get; set; } = "";

        /// <summary>
        /// 是否授权
        /// </summary>
        /// <value><c>true</c> if authorizer; otherwise, <c>false</c>.</value>
        public bool Authorizer { get; set; }

        /// <summary>
        /// 接口调用凭据刷新令牌
        /// </summary>
        /// <value>The authorizer refresh token.</value>
        public string AuthorizerRefreshToken { get; set; } = "";

        /// <summary>
        /// 授权方公众号类型，0代表订阅号，1代表由历史老帐号升级后的订阅号，2代表服务号
        /// </summary>
        /// <value>The type of the service.</value>
        public int ServiceType { get; set; }

        /// <summary>
        /// 授权方认证类型，-1代表未认证，0代表微信认证，1代表新浪微博认证，2代表腾讯微博认证，3代表已资质认证通过但还未通过名称认证，4代表已资质认证通过、还未通过名称认证，但通过了新浪微博认证，5代表已资质认证通过、还未通过名称认证，但通过了腾讯微博认证
        /// </summary>
        /// <value>The type of the verify.</value>
        public int VerifyType { get; set; }

        /// <summary>
        /// 授权方昵称
        /// </summary>
        /// <value>The name of the nick.</value>
        public string NickName { get; set; } = "";

        /// <summary>
        /// 授权方头像
        /// </summary>
        /// <value>The head image.</value>
        public string HeadImg { get; set; } = "";

        /// <summary>
        /// 二维码图片的URL，开发者最好自行也进行保存
        /// </summary>
        /// <value>The qrcode URL.</value>
        public string QrcodeUrl { get; set; } = "";
    }
}

