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
    public class DefaultTextEvent : ApplicationService, ITextEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wechat"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<NormalMessage> HandleAsync(WechatDataDto wechat, TextRequest request)
        {
            return await Task.Factory.StartNew<NormalMessage>(() =>
            {
                return null;
            });
        }
    }
}