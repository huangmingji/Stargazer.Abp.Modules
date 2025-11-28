using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.EventHandler;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Wechat.Application.EventHandler
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultUnSubscribeEvent : ApplicationService, IUnSubscribeEvent
    {
        private IWechatUserService _wechatUserService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wechatUserStore"></param>
        /// <param name="wechatAccessTokenStore"></param>
        public DefaultUnSubscribeEvent(IWechatUserService wechatUserService)
        {
            _wechatUserService = wechatUserService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wechat"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<NormalMessage> HandleAsync(WechatDataDto wechat, UnSubscribeRequest request)
        {
            await _wechatUserService.DeleteAsync(wechat.AppId, request.FromUserName);
            return null;
        }
    }
}