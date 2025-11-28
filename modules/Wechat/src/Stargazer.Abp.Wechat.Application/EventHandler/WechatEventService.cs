using Stargazer.Common.Extend;
using Microsoft.Extensions.Logging;
using Stargazer.Abp.Wechat.Application.Contracts.EventHandler;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Wechat.Application.EventHandler;

public class WechatEventService : ApplicationService, IWechatEventService
{
    private ILogger<WechatEventService> _logger;
    private IWechatService _wechatService;
    private IClickEvent _clickEvent;
    private IImageEvent _imageEvent;
    private ILinkEvent _linkEvent;
    private ILocationEvent _locationEvent;
    private IMassSendJobFinishEvent _massSendJobFinishEvent;
    private IScanEvent _scanEvent;
    private IShortVideoEvent _shortVideoEvent;
    private ISubscribeEvent _subscribeEvent;
    private IUnSubscribeEvent _unSubscribeEvent;
    private ITemplateSendJobFinishEvent _templateSendJobFinishEvent;
    private ITextEvent _textEvent;
    private IVideoEvent _videoEvent;
    private IViewEvent _viewEvent;
    private IVoiceEvent _voiceEvent;

    public WechatEventService(ILogger<WechatEventService> logger, IWechatService wechatService, IClickEvent clickEvent, IImageEvent imageEvent, ILinkEvent linkEvent, ILocationEvent locationEvent, IMassSendJobFinishEvent massSendJobFinishEvent, IScanEvent scanEvent, IShortVideoEvent shortVideoEvent, ISubscribeEvent subscribeEvent, IUnSubscribeEvent unSubscribeEvent, ITemplateSendJobFinishEvent templateSendJobFinishEvent, ITextEvent textEvent, IVideoEvent videoEvent, IViewEvent viewEvent, IVoiceEvent voiceEvent)
    {
        _logger = logger;
        _wechatService = wechatService;
        _clickEvent = clickEvent;
        _imageEvent = imageEvent;
        _linkEvent = linkEvent;
        _locationEvent = locationEvent;
        _massSendJobFinishEvent = massSendJobFinishEvent;
        _scanEvent = scanEvent;
        _shortVideoEvent = shortVideoEvent;
        _subscribeEvent = subscribeEvent;
        _unSubscribeEvent = unSubscribeEvent;
        _templateSendJobFinishEvent = templateSendJobFinishEvent;
        _textEvent = textEvent;
        _videoEvent = videoEvent;
        _viewEvent = viewEvent;
        _voiceEvent = voiceEvent;
    }

    private WechatDataDto wechat;
    public async Task<string> Execute(string appId, string requestMsg)
    {
        wechat = await _wechatService.FindAsync(appId);
        RequestData requestXml = requestMsg.ConvertToObject<RequestData>();
        var msgType = (RequestMsgType)System.Enum.Parse(typeof(RequestMsgType), requestXml.MsgType, true);
        NormalMessage result = null;
        switch (msgType)
        {
            case RequestMsgType.Event:
                switch (requestXml.Event)
                {
                    case "subscribe":
                        result = await HandleSubscribeEvent(requestXml);
                        break;
                    case "unsubscribe":
                        result = await HandleUnSubscribeEvent(requestXml);
                        break;
                    case "SCAN":
                        result = await HandleScanEvent(requestXml);
                        break;
                    case "CLICK":
                        result = await HandleClickEvent(requestXml);
                        break;
                    case "TEMPLATESENDJOBFINISH":
                        result = await HandleTemplateSendJobFinishEvent(requestXml);
                        break;
                    case "MASSSENDJOBFINISH":
                        result = await HandleMassSendJobFinishEvent(requestXml);
                        break;
                    case "VIEW":
                        result = await HandleViewEvent(requestXml);
                        break;
                }
                break;
            case RequestMsgType.Image:
                result = await HandleImageEvent(requestXml);
                break;
            case RequestMsgType.Link:
                result = await HandleLinkEvent(requestXml);
                break;
            case RequestMsgType.Location:
                result = await HandleLocationEvent(requestXml);
                break;
            case RequestMsgType.ShortVideo:
                result = await HandleShortVideoEvent(requestXml);
                break;
            case RequestMsgType.Text:
                result = await HandleTextEvent(requestXml);
                break;
            case RequestMsgType.Video:
                result = await HandleVideoEvent(requestXml);
                break;
            case RequestMsgType.Voice:
                result = await HandleVoiceEvent(requestXml);
                break;
        }
        if(result == null) return String.Empty;
        return result.SerializeObject();
    }

    #region handle

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleClickEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, ClickRequest>(requestData);
        return await _clickEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleImageEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, ImageRequest>(requestData);
        return await _imageEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleLinkEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, LinkRequest>(requestData);
        return await _linkEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleLocationEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, LocationRequest>(requestData);
        return await _locationEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleMassSendJobFinishEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, MassSendJobFinishRequest>(requestData);
        return await _massSendJobFinishEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleScanEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, ScanRequest>(requestData);
        return await _scanEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleSubscribeEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, SubscribeRequest>(requestData);
        return await _subscribeEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleTemplateSendJobFinishEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, TemplateSendJobFinishRequest>(requestData);
        return await _templateSendJobFinishEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleTextEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, TextRequest>(requestData);
        return await _textEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleUnSubscribeEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, UnSubscribeRequest>(requestData);
        return await _unSubscribeEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleVideoEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, VideoRequest>(requestData);
        return await _videoEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleViewEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, ViewRequest>(requestData);
        return await _viewEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleVoiceEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, VoiceRequest>(requestData);
        return await _voiceEvent.HandleAsync(wechat, request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestData"></param>
    /// <returns></returns>
    private async Task<NormalMessage> HandleShortVideoEvent(RequestData requestData)
    {
        var request = ObjectMapper.Map<RequestData, ShortVideoRequest>(requestData);
        return await _shortVideoEvent.HandleAsync(wechat, request);
    }

    #endregion
}