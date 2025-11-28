using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
namespace Stargazer.Abp.Wechat.Application.Contracts.EventHandler;
public interface IOpenPlatformEventService
{
    Task<string> Execute(string appId, string requestMsg, OpenPlatform openPlatform);   
}