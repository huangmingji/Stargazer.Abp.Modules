using AutoMapper;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.DataModels.EventHandler;
using Stargazer.Abp.Wechat.Application.Contracts.Wechat.Dtos;
using Stargazer.Abp.Wechat.Domain.Wechat;

namespace Stargazer.Abp.Wechat.Application
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            CreateMap<WechatData, WechatDataDto>();
            CreateMap<WechatAccessTokenData, WechatAccessTokenDataDto>();
            CreateMap<WechatJsapiTicketData, WechatJsapiTicketDataDto>();
            CreateMap<WechatUserData, WechatUserDataDto>();

            CreateMap<CreateOrUpdateWechatUserDataDto, WechatUserData>();
            CreateMap<CreateOrUpdateWechatAccessTokenDataDto, WechatAccessTokenData>();
            CreateMap<CreateOrUpdateWechatJsapiTicketDataDto, WechatJsapiTicketData>();

            CreateMap<RequestData, ClickRequest>();
            CreateMap<RequestData, ImageRequest>();
            CreateMap<RequestData, LinkRequest>();
            CreateMap<RequestData, LocationRequest>();
            CreateMap<RequestData, MassSendJobFinishRequest>();
            CreateMap<RequestData, ScanRequest>();
            CreateMap<RequestData, ShortVideoRequest>();
            CreateMap<RequestData, SubscribeRequest>();
            CreateMap<RequestData, TemplateSendJobFinishRequest>();
            CreateMap<RequestData, TextRequest>();
            CreateMap<RequestData, UnSubscribeRequest>();
            CreateMap<RequestData, VideoRequest>();
            CreateMap<RequestData, ViewRequest>();
            CreateMap<RequestData, VoiceRequest>();
        }
    }
}