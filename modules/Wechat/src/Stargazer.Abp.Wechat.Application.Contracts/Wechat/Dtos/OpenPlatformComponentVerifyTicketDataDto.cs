using System;
using Volo.Abp.Application.Dtos;

namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos
{
	public class OpenPlatformComponentVerifyTicketDataDto : AuditedEntityDto<Guid>
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

