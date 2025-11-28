using Microsoft.AspNetCore.Authorization;
using Stargazer.Abp.Categories.Application.Contracts;
using Stargazer.Abp.Categories.Application.Contracts.Permissions;
using Stargazer.Abp.Categories.Domain;
using Stargazer.Abp.Categories.Domain.Categories;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Categories.Application.Categories
{
    [Authorize]
    public class CategoryService(ICategoryRepository repository) : ApplicationService, ICategoryService
    {
        private async Task<Category> MapToEntityAsync(CreateUpdateCategoryDto input)
        {
            if (await repository.AnyAsync(x => x.UniqueName == input.UniqueName))
            {
                throw new DuplicateCategoryUniqueNameException();
            }
        
            return new Category(GuidGenerator.Create(), input.StoreId, input.UniqueName,
                input.DisplayName, "", input.Description, input.MediaResources, input.IsHidden,
                input.ParentId);
        }

        private async Task<Category> MapToEntityAsync(CreateUpdateCategoryDto input, Category entity)
        {
            if (await repository.AnyAsync(x => x.UniqueName == input.UniqueName && x.Id != entity.Id))
            {
                throw new DuplicateCategoryUniqueNameException();
            }
        
            entity.Update(input.UniqueName, input.DisplayName, input.Code, input.Description,
                input.MediaResources,
                input.IsHidden, input.ParentId);
            return entity;
        }
        
        private ICategoryRepository Repository => this.LazyServiceProvider.LazyGetRequiredService<ICategoryRepository>();
        
        [Authorize(CategoriesPermissions.Categories.Create)]
        public async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            var category = await MapToEntityAsync(input);
            var result = await Repository.InsertAsync(category);
            return ObjectMapper.Map<Category, CategoryDto>(result);
        }

        [Authorize(CategoriesPermissions.Categories.Update)]
        public async Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            var category = await Repository.GetAsync(id);
            category = await MapToEntityAsync(input, category);
            var result = await Repository.UpdateAsync(category);
            return ObjectMapper.Map<Category, CategoryDto>(result);
        }

        [Authorize(CategoriesPermissions.Categories.Delete)]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [Authorize(CategoriesPermissions.Categories.Manage)]
        public async Task<CategoryDto> GetAsync(Guid id)
        {
            var result = await Repository.GetAsync(id);
            return ObjectMapper.Map<Category, CategoryDto>(result);
        }

        [Authorize(CategoriesPermissions.Categories.Manage)]
        public async Task<List<CategoryDto>> GetListAsync()
        {
            var result = await Repository.GetListAsync();
            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(result);
        }

        [AllowAnonymous]
        public async Task<List<CategorySummaryDto>> GetSummaryListAsync()
        {
            var result = await Repository.GetListAsync(x => x.IsHidden == false);
            return ObjectMapper.Map<List<Category>, List<CategorySummaryDto>>(result);
        }
    }
}