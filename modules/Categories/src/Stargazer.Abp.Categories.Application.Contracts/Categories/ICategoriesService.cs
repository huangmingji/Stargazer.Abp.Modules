using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Categories.Application.Contracts
{
    public interface ICategoryService : IApplicationService
    {
        Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input);
        
        Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input);
        
        Task DeleteAsync(Guid id);
        
        Task<CategoryDto> GetAsync(Guid id);
        
        Task<List<CategoryDto>> GetListAsync();

        Task<List<CategorySummaryDto>> GetSummaryListAsync();
    }
}