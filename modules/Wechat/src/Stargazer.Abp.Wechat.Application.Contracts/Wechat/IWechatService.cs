using Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos;

namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat
{
	public interface IWechatService
	{
        Task<WechatDataDto> GetAsync(Guid? tenantId);
        Task<WechatDataDto> FindAsync(string appId);
        Task<WechatDataDto> UpdateAsync(CreateOrUpdateWechatDataDto input);
        Task<WechatDataDto> UpdateSimpleDataAsync(CreateOrUpdateSimpleWechatDataDto input);

        Task<WechatAccessTokenDataDto> UpdateWechatAccessTokenAsync(CreateOrUpdateWechatAccessTokenDataDto input);
        Task<WechatJsapiTicketDataDto> UpdateWechatJsapiTicketAsync(CreateOrUpdateWechatJsapiTicketDataDto input);
        Task<WechatAccessTokenDataDto> GetWechatAccessTokenAsync(string appId);
        Task<WechatJsapiTicketDataDto> GetWechatJsapiTicketAsync(string appId);

        Task UnAuthorized(string appId);
    }
}

