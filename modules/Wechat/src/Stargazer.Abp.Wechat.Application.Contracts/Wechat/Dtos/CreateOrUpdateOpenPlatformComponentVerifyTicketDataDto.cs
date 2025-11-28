using System;
namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos
{
	public class CreateOrUpdateOpenPlatformComponentVerifyTicketDataDto
	{
        /// <summary>
        /// 开放平台 appid
        /// </summary>
        /// <value>The appid.</value>
        public string AppId { get; set; } = "";

        /// <summary>
        /// 开放平台ticket
        /// </summary>
        public string ComponentVerifyTicket { get; set; } = "";
    }
}

