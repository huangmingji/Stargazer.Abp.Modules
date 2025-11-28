using System;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler;

namespace Stargazer.Abp.Wechat.Application.Contracts.EventHandler
{
	public interface IScanEvent
	{
        Task<NormalMessage> HandleAsync(WechatDataDto wechat, ScanRequest request);
    }
}

