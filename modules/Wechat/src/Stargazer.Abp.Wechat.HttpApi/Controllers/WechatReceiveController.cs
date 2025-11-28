using Stargazer.Common.Extend;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Mvc;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Tencent;
using Stargazer.Abp.Wechat.Application.Contracts.EventHandler;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos;
using Stargazer.Abp.Wechat.Domain.Shared.Enumes;

namespace Stargazer.Abp.Wechat.HttpApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/wechat")]
    public class WechatReceiveController: AbpController
    {
        private ILogger<WechatReceiveController> _logger;
        private IWechatService _wechatService;
        private IConfiguration _configuration;
        private IOpenPlatformService _openPlatformService;
        private IOpenPlatformEventService _openPlatformEventService;
        private IWechatEventService _wechatEventService;
        public WechatReceiveController(
            ILogger<WechatReceiveController> logger,
            IWechatService wechatService,
            IConfiguration configuration,
            IOpenPlatformService openPlatformService,
            IOpenPlatformEventService openPlatformEventService,
            IWechatEventService wechatEventService)
        {
            _logger = logger;
            _wechatService = wechatService;
            _configuration = configuration;
            _openPlatformService = openPlatformService;
            _openPlatformEventService = openPlatformEventService;
            _wechatEventService = wechatEventService;
        }

        /// <summary>
        /// 1、开放平台ticket更新，用于获取第三方平台接口调用凭据
        /// 2、取消授权回调
        /// </summary>
        /// <returns></returns>
        [HttpPost("message")]
        public async Task<ActionResult<string>> OpenPlatformPostAsync(
            [FromQuery]string signature, [FromQuery] string timestamp, [FromQuery] string nonce,
            [FromQuery] string encrypt_type, [FromQuery] string msg_signature)
        {
            Stream requeStream = Request.Body;
            byte[] b = new byte[requeStream.Length];
            requeStream.Read(b, 0, (int)requeStream.Length);
            string post_data = Encoding.UTF8.GetString(b);

            string msg = string.Empty;
            OpenPlatform? openPlatform = _configuration.GetSection("OpenPlatform").Get<OpenPlatform>();
            if (openPlatform == null)
            {
                throw new NotSupportedException();
            }

            WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(openPlatform.Token, openPlatform.AesKey, openPlatform.AppId);
            if (wxcpt.DecryptMsg(msg_signature, timestamp, nonce, post_data, ref msg) != 0)
            {
                return string.Empty;
            }

            OpenPlatformRequestData requestData = msg.ConvertToObject<OpenPlatformRequestData>();
            if (requestData.InfoType == "component_verify_ticket")
            {
                var input = new CreateOrUpdateOpenPlatformComponentVerifyTicketDataDto()
                {
                    AppId = openPlatform.AppId,
                    ComponentVerifyTicket = requestData.ComponentVerifyTicket
                };
                var data = await _openPlatformService.UpdateOpenPlatformTicketAsync(input);
                if (data != null)
                {
                    return "success";
                }

                return "fail";
            }
            else if (requestData.InfoType == "unauthorized")
            {
                await _wechatService.UnAuthorized(requestData.AuthorizerAppid);
                return "success";
            }
            return string.Empty;
        }

        /// <summary>
        /// 微信公众号接收消息、事件推送，兼容微信公众号开放平台
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        [HttpPost("eventmsg/{appid}")]
        public async Task<ActionResult<string>> EnventPost([FromRoute]string appid,
            [FromQuery] string signature, [FromQuery] string timestamp, [FromQuery] string nonce,
            [FromQuery] string encrypt_type, [FromQuery] string msg_signature)
        {
            var wechat = await _wechatService.FindAsync(appid);
            if (wechat == null)
            {
                return "";
            }

            var reader = new StreamReader(Request.Body);
            var post_data = reader.ReadToEnd();

            if (wechat.Authorizer)
            {
                #region 授权开放平台的公众号事件处理

                string msg = string.Empty;
                OpenPlatform? openPlatform = _configuration.GetSection("OpenPlatform").Get<OpenPlatform>();
                if (openPlatform == null)
                {
                    throw new NotSupportedException();
                }
                WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(openPlatform.Token, openPlatform.AesKey, openPlatform.AppId);
                if (wxcpt.DecryptMsg(msg_signature, timestamp, nonce, post_data, ref msg) != 0)
                {
                    return string.Empty;
                }

                return await _openPlatformEventService.Execute(appid, msg, openPlatform);

                #endregion
            }
            else
            {
                #region 非授权开放平台的公众号事件处理

                string msg = string.Empty;
                if (wechat.EncodeType != EncodeType.None)
                {
                    WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(wechat.Token, wechat.EncodingAESKey, wechat.AppId);
                    int result = wxcpt.DecryptMsg(msg_signature, timestamp, nonce, post_data, ref msg);
                    if (result != 0)
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    msg = post_data;
                }

                return await _wechatEventService.Execute(appid, msg);

                #endregion
            }
        }

        /// <summary>
        /// 验证微信公众号回调接口token，开放平台授权的公众号不需要这个接口
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        [HttpGet("eventmsg/{appid}")]
        public async Task<ActionResult<string>> EnventGetAsync([FromRoute]string appid, [FromQuery]string echostr, [FromQuery] string signature,
            [FromQuery] string timestamp, [FromQuery] string nonce)
        {
            var wechat = await _wechatService.FindAsync(appid);
            if (wechat == null)
            {
                return string.Empty;
            }

            string[] arrTmp = { wechat.Token, timestamp, nonce };
            Array.Sort(arrTmp);     //字典排序
            if (signature == string.Join("", arrTmp).SHA1())
            {
                return string.IsNullOrWhiteSpace(echostr) ? string.Empty : echostr;
            }

            return string.Empty;
        }
    }
}

