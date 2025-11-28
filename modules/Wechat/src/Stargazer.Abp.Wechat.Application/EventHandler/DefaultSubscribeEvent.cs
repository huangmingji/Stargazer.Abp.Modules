using Stargazer.Common.Extend;
using Microsoft.Extensions.Logging;
using Senparc.Weixin.MP.AdvancedAPIs;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.EventHandler;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Wechat.Application.EventHandler
{
    public class DefaultSubscribeEvent : ApplicationService, ISubscribeEvent
    {
         private ILogger<DefaultSubscribeEvent> _logger;
         private IWechatService _wechatService;
         private IWechatUserService _wechatUserService;
         
         public DefaultSubscribeEvent(
             ILogger<DefaultSubscribeEvent> logger,
             IWechatService wechatService,
             IWechatUserService wechatUserService)
         {
             _logger = logger;
             _wechatService = wechatService;
             _wechatUserService = wechatUserService;
         }

        public async Task<NormalMessage> HandleAsync(WechatDataDto wechat, SubscribeRequest request)
        {
             var wechatAccessToken = await _wechatService.GetWechatAccessTokenAsync(wechat.AppId);
             try
             {
                 var weixinUserInfo = await OAuthApi.GetUserInfoAsync(wechatAccessToken.AccessToken, request.FromUserName);
                 var updateWechatUserRequestModel = new CreateOrUpdateWechatUserDataDto()
                 {
                     AppId = wechat.AppId,
                     OpenId = weixinUserInfo.openid,
                     NickName = weixinUserInfo.nickname,
                     Subscribe = weixinUserInfo.subscribe == 0 ? false : true,
                     Sex = weixinUserInfo.sex,
                     Country = weixinUserInfo.country,
                     Province = weixinUserInfo.province,
                     City = weixinUserInfo.city,
                     HeadImgUrl = weixinUserInfo.headimgurl,
                     SubscribeTime = weixinUserInfo.subscribe_time.ToString().ToDateTime(),
                     Unionid = weixinUserInfo.unionid,
                     Remark = weixinUserInfo.remark,
                     GroupId = weixinUserInfo.groupid,
                     TagidList = string.Join(",", weixinUserInfo.tagid_list)
                 };

                 await _wechatUserService.UpdateAsync(updateWechatUserRequestModel);
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, ex.Message);
             }
            return null;
        }
    }
}