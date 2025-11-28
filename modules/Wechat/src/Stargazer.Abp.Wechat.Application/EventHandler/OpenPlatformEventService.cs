using Stargazer.Abp.Wechat.Application.Contracts.EventHandler;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Wechat.Application.EventHandler;

public class OpenPlatformEventService : ApplicationService, IOpenPlatformEventService
{
    public Task<string> Execute(string appId, string requestMsg, OpenPlatform openPlatform)
    {
        throw new NotImplementedException();
    }
}