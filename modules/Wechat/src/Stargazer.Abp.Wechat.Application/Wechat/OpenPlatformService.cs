using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Senparc.NeuChar.NeuralSystems;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos;
using Stargazer.Abp.Wechat.Domain.Wechat;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Wechat.Application.Wechat
{
	public class OpenPlatformService : ApplicationService, IOpenPlatformService
    {
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private IRepository<OpenPlatformComponentVerifyTicketData, Guid> _repository;
        public OpenPlatformService(IDistributedCache cache, IConfiguration configuration, IRepository<OpenPlatformComponentVerifyTicketData, Guid> repository)
		{
            _cache = cache;
            _configuration = configuration;
            _repository = repository;
		}

        private const string AccessTokenKey = "OpenPlatform:AccessTokenKey";
        private const string TicketKey = "OpenPlatform:Ticket";

        public async Task<string?> GetOpenPlatformAccessTokenAsync()
        {
            return await _cache.GetStringAsync(AccessTokenKey);
        }

        public async Task<string?> GetOpenPlatformTicketAsync()
        {
            var ticket =  await _cache.GetStringAsync(TicketKey);
            if (string.IsNullOrEmpty(ticket))
            {
                OpenPlatform? openPlatform = _configuration.GetSection("OpenPlatform").Get<OpenPlatform>();
                if (openPlatform == null)
                {
                    throw new NotSupportedException();
                }
                var data = await _repository.GetAsync(x=> x.AppId == openPlatform.AppId);
                await _cache.SetStringAsync(TicketKey, data.ComponentVerifyTicket);
                return data.ComponentVerifyTicket;
            }
            return ticket;
        }

        public async Task<bool> UpdateOpenPlatformAccessTokenAsync(string value, int expiresIn)
        {
            try
            {
                await _cache.SetStringAsync(AccessTokenKey, value, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(expiresIn)
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<OpenPlatformComponentVerifyTicketDataDto> UpdateOpenPlatformTicketAsync(CreateOrUpdateOpenPlatformComponentVerifyTicketDataDto input)
        {
            var data = await _repository.FindAsync(x => x.AppId == input.AppId);
            if (data == null)
            {
                data = new OpenPlatformComponentVerifyTicketData(GuidGenerator.Create(), input.AppId, input.ComponentVerifyTicket);
                data = await _repository.InsertAsync(data);
            }
            else {
                data.SetData(appId: input.AppId, componentVerifyTicket: input.ComponentVerifyTicket);
                data = await _repository.UpdateAsync(data);
            }
            await _cache.SetStringAsync(TicketKey, input.ComponentVerifyTicket);
            return ObjectMapper.Map<OpenPlatformComponentVerifyTicketData, OpenPlatformComponentVerifyTicketDataDto>(data);
        }
    }
}

