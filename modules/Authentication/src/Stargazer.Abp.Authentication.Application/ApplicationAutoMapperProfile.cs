using AutoMapper;
using Stargazer.Abp.Authentication.Application.Contracts;
using Stargazer.Abp.Authentication.Domain.Authentication;

namespace Stargazer.Abp.Authentication.Application
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            CreateMap<UserDevice, UserDeviceDto>();
            CreateMap<UserSession, UserSessionDto>();
        }
    }
}