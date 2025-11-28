using AutoMapper;
using Stargazer.Abp.Categories.Application.Contracts;
using Stargazer.Abp.Categories.Domain;
using Volo.Abp.AutoMapper;

namespace Stargazer.Abp.Categories.Application
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategorySummaryDto>().Ignore(x=> x.TreedDisplayName);
        }
    }
}