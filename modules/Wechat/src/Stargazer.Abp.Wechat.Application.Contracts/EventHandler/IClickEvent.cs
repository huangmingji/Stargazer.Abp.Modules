using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler;

namespace Stargazer.Abp.Wechat.Application.Contracts.EventHandler
{
	public interface IClickEvent
	{
        Task<NormalMessage> HandleAsync(WechatDataDto wechat, ClickRequest request);
    }
}

