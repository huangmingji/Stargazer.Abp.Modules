using System.Xml.Schema;
using AutoMapper;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Abp.Users.Application.Contracts.Roles.Dtos;
using Stargazer.Abp.Users.Domain.Roles;
using Stargazer.Abp.Users.Domain.Users;
using Volo.Abp.AutoMapper;

namespace Stargazer.Abp.Users.Application
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            CreateMap<UserData, UserDataDto>().Ignore(x=>x.Permissions);
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<PermissionData, PermissionDataDto>();
            CreateMap<RoleData, RoleDto>();
            CreateMap<RolePermissionData, RolePermissionDto>();
            CreateMap<PermissionGroup, PermissionGroupDto>();
        }
    }
}