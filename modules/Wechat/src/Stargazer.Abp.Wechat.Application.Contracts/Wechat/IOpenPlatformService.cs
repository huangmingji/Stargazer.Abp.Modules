using System;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos;

namespace Stargazer.Abp.Wechat.Application.Contracts.Wechat
{
	public interface IOpenPlatformService
	{
        Task<OpenPlatformComponentVerifyTicketDataDto> UpdateOpenPlatformTicketAsync(CreateOrUpdateOpenPlatformComponentVerifyTicketDataDto input);

        Task<string?> GetOpenPlatformTicketAsync();

        Task<bool> UpdateOpenPlatformAccessTokenAsync(string value, int expiresIn);

        Task<string?> GetOpenPlatformAccessTokenAsync();
    }
}

