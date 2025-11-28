namespace Stargazer.Abp.Wechat.Application.Contracts.EventHandler;

public interface IWechatEventService {
    Task<string> Execute(string appId, string requestMsg);
}