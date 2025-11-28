using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos;
using Stargazer.Abp.Wechat.Domain.Wechat;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Wechat.Application.Wechat
{
    public class WechatService : ApplicationService, IWechatService
    {
        private readonly IDistributedCache<WechatData> _wechatDataCache;
        private readonly IDistributedCache<WechatAccessTokenData> _wechatAccessTokenDataCache;
        private readonly IDistributedCache<WechatJsapiTicketData> _wechatJsapiTicketDataCache;
        private readonly IRepository<WechatData> _wechatRepository;
        private readonly IRepository<WechatAccessTokenData> _wechatAccessTokenRepository;
        private readonly IRepository<WechatJsapiTicketData> _wechatJsapiTicketReposition;
        public WechatService(
            IDistributedCache<WechatData> wechatDataCache,
            IDistributedCache<WechatAccessTokenData> wechatAccessTokenDataCache,
            IDistributedCache<WechatJsapiTicketData> wechatJsapiTicketDataCache,
            IRepository<WechatData> wechatRepository,
            IRepository<WechatAccessTokenData> wechatAccessTokenRepository,
            IRepository<WechatJsapiTicketData> wechatJsapiTicketReposition)
        {
            _wechatDataCache = wechatDataCache;
            _wechatAccessTokenDataCache = wechatAccessTokenDataCache;
            _wechatJsapiTicketDataCache = wechatJsapiTicketDataCache;
            _wechatRepository = wechatRepository;
            _wechatAccessTokenRepository = wechatAccessTokenRepository;
            _wechatJsapiTicketReposition = wechatJsapiTicketReposition;
        }

        private const string WechatCacheKey = "Wechat";
        private const string WechatAccessTokenCacheKey = "WechatAccessToken";
        private const string WechatJsapiTicketCacheKey = "WechatJsapiTicket";

        public async Task<WechatDataDto> GetAsync(Guid? tenantId)
        {
            var key = tenantId == null ? $"{WechatCacheKey}" : $"{WechatCacheKey}:{tenantId}";
            var data = await _wechatDataCache.GetAsync(key);
            if (data == null)
            {
                data = await _wechatRepository.GetAsync(x => x.TenantId == tenantId);
                await _wechatDataCache.SetAsync(key, data, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(1)
                });
            }
            return ObjectMapper.Map<WechatData, WechatDataDto>(data);
        }

        public async Task<WechatAccessTokenDataDto> GetWechatAccessTokenAsync(string appId)
        {
            var key = CurrentTenant.Id == null ? $"{WechatAccessTokenCacheKey}:{appId}" : $"{WechatAccessTokenCacheKey}:{CurrentTenant.Id}:{appId}";
            var data = await _wechatAccessTokenDataCache.GetAsync(key);
            if (data == null)
            {
                data = await _wechatAccessTokenRepository.GetAsync(x => x.AppId == appId);
                await _wechatAccessTokenDataCache.SetAsync(key, data, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                });
            }
            return ObjectMapper.Map<WechatAccessTokenData, WechatAccessTokenDataDto>(data);
        }

        public async Task<WechatJsapiTicketDataDto> GetWechatJsapiTicketAsync(string appId)
        {
            var key = CurrentTenant.Id == null ? $"{WechatJsapiTicketCacheKey}:{appId}" : $"{WechatJsapiTicketCacheKey}:{CurrentTenant.Id}:{appId}";
            var data = await _wechatJsapiTicketDataCache.GetAsync(key);
            if (data == null)
            {
                data = await _wechatJsapiTicketReposition.GetAsync(x => x.AppId == appId);
                await _wechatJsapiTicketDataCache.SetAsync(key, data, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                });
            }
            return ObjectMapper.Map<WechatJsapiTicketData, WechatJsapiTicketDataDto>(data);
        }

        public async Task<WechatDataDto> UpdateAsync(CreateOrUpdateWechatDataDto input)
        {
            var data = await _wechatRepository.FindAsync(x => x.TenantId == CurrentTenant.Id);
            if (data == null)
            {
                data = new WechatData(
                    id: GuidGenerator.Create(),
                    appId: input.AppId,
                    appSecret: input.AppSecret,
                    encodingAESKey: input.EncodingAESKey,
                    encodeType: input.EncodeType,
                    token: input.Token,
                    principalName: input.PrincipalName,
                    userName: input.UserName,
                    alias: input.Alias,
                    authorizerRefreshToken: input.AuthorizerRefreshToken,
                    serviceType: input.ServiceType,
                    verifyType: input.VerifyType,
                    nickName: input.NickName,
                    headImg: input.HeadImg,
                    qrcodeUrl: input.QrcodeUrl);
                data = await _wechatRepository.InsertAsync(data);
            }
            else
            {
                data.SetData(
                    appId: input.AppId,
                    appSecret: input.AppSecret,
                    encodingAESKey: input.EncodingAESKey,
                    token: input.Token,
                    principalName: input.PrincipalName,
                    encodeType: input.EncodeType,
                    userName: input.UserName,
                    alias: input.Alias,
                    authorizerRefreshToken: input.AuthorizerRefreshToken,
                    serviceType: input.ServiceType,
                    verifyType: input.VerifyType,
                    nickName: input.NickName,
                    headImg: input.HeadImg,
                    qrcodeUrl: input.QrcodeUrl);
                data = await _wechatRepository.UpdateAsync(data);
            }
            var key = CurrentTenant.Id == null ? $"{WechatCacheKey}" : $"{WechatCacheKey}:{CurrentTenant.Id}";
            await _wechatDataCache.SetAsync(key, data, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1)
            });
            return ObjectMapper.Map<WechatData, WechatDataDto>(data);
        }

        public async Task<WechatDataDto> UpdateSimpleDataAsync(CreateOrUpdateSimpleWechatDataDto input)
        {
            var data = await _wechatRepository.FindAsync(x => x.TenantId == CurrentTenant.Id);
            if (data == null)
            {
                data = new WechatData(
                    id: GuidGenerator.Create(),
                    appId: input.AppId,
                    appSecret: input.AppSecret,
                    encodingAESKey: input.EncodingAESKey,
                    encodeType: input.EncodeType);
                data = await _wechatRepository.InsertAsync(data);
            }
            else
            {
                data.SetData(
                    appId: input.AppId,
                    appSecret: input.AppSecret,
                    encodingAESKey: input.EncodingAESKey,
                    encodeType: input.EncodeType);
                data = await _wechatRepository.UpdateAsync(data);
            }
            var key = CurrentTenant.Id == null ? $"{WechatCacheKey}" : $"{WechatCacheKey}:{CurrentTenant.Id}";
            await _wechatDataCache.SetAsync(key, data, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1)
            });
            return ObjectMapper.Map<WechatData, WechatDataDto>(data);
        }

        public async Task<WechatAccessTokenDataDto> UpdateWechatAccessTokenAsync(CreateOrUpdateWechatAccessTokenDataDto input)
        {
            var data = await _wechatAccessTokenRepository.FindAsync(x => x.TenantId == CurrentTenant.Id && x.AppId == input.AppId);
            if (data == null)
            {
                data = new WechatAccessTokenData(
                    id: GuidGenerator.Create(),
                    appId: input.AppId,
                    accessToken: input.AccessToken,
                    expiresIn: input.ExpiresIn);
                data = await _wechatAccessTokenRepository.InsertAsync(data);
            }
            else
            {
                data.SetData(
                    appId: input.AppId,
                    accessToken: input.AccessToken,
                    expiresIn: input.ExpiresIn);
                data = await _wechatAccessTokenRepository.UpdateAsync(data);
            }
            var key = CurrentTenant.Id == null ? $"{WechatAccessTokenCacheKey}:{input.AppId}" : $"{WechatAccessTokenCacheKey}:{CurrentTenant.Id}:{input.AppId}";
            await _wechatAccessTokenDataCache.SetAsync(key, data, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30)
            });
            return ObjectMapper.Map<WechatAccessTokenData, WechatAccessTokenDataDto>(data);
        }

        public async Task<WechatJsapiTicketDataDto> UpdateWechatJsapiTicketAsync(CreateOrUpdateWechatJsapiTicketDataDto input)
        {
            var data = await _wechatJsapiTicketReposition.FindAsync(x => x.TenantId == CurrentTenant.Id && x.AppId == input.AppId);
            if (data == null)
            {
                data = new WechatJsapiTicketData(
                    id: GuidGenerator.Create(),
                    appId: input.AppId,
                    jsapiTicket: input.JsapiTicket,
                    expiresIn: input.ExpiresIn);
                data = await _wechatJsapiTicketReposition.InsertAsync(data);
            }
            else
            {
                data.SetData(
                    appId: input.AppId,
                    jsapiTicket: input.JsapiTicket,
                    expiresIn: input.ExpiresIn);
                data = await _wechatJsapiTicketReposition.UpdateAsync(data);
            }
            var key = CurrentTenant.Id == null ? $"{WechatJsapiTicketCacheKey}:{input.AppId}" : $"{WechatJsapiTicketCacheKey}:{CurrentTenant.Id}:{input.AppId}";
            await _wechatJsapiTicketDataCache.SetAsync(key, data, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30)
            });
            return ObjectMapper.Map<WechatJsapiTicketData, WechatJsapiTicketDataDto>(data);
        }

        public async Task UnAuthorized(string appId)
        {
            var data = await _wechatRepository.FindAsync(x => x.AppId == appId);
            if (data != null)
            {
                data.UnAuthorized();
                await _wechatRepository.UpdateAsync(data);
            }
        }

        public async Task<WechatDataDto> FindAsync(string appId)
        {
            var data = await _wechatRepository.FindAsync(x=> x.AppId == appId);
            return ObjectMapper.Map<WechatData, WechatDataDto>(data);
        }
    }
}

