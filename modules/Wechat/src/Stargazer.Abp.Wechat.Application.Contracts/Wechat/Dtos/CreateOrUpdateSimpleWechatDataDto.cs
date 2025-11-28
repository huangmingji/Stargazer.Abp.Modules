using System;
using Stargazer.Abp.Wechat.Domain.Shared.Enumes;

namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos
{
	public class CreateOrUpdateSimpleWechatDataDto
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
        /// 消息加密密钥由43位字符组成，可随机修改，字符范围为A-Z，a-z，0-9
        /// </summary>
        /// <value>The encoding AESK ey.</value>
        public string EncodingAESKey { get; set; } = "";
        
        public EncodeType EncodeType { get; set; } = EncodeType.None;
    }
}

