using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Stargazer.Abp.Wechat.Domain.Wechat
{
	public class OpenPlatformComponentVerifyTicketData : AuditedAggregateRoot<Guid>
    {
        public OpenPlatformComponentVerifyTicketData(Guid id, string appId, string componentVerifyTicket) : base(id)
        {
            AppId = appId;
            ComponentVerifyTicket = componentVerifyTicket;
        }

        /// <summary>
        /// 开放平台 appid
        /// </summary>
        /// <value>The appid.</value>
        public string AppId { get; set; } = "";

        /// <summary>
        /// 开放平台ticket
        /// </summary>
        public string ComponentVerifyTicket { get; set; } = "";

        public void SetData(string appId, string componentVerifyTicket)
        {
            this.AppId = appId;
            this.ComponentVerifyTicket = componentVerifyTicket;
        }
    }
}

