using System;
namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos
{
	public class CreateOrUpdateWechatAccessTokenDataDto
	{
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
    }
}

