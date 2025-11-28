using Stargazer.Abp.Categories.Domain;
using Stargazer.Abp.Categories.Domain.Categories;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Categories.EntityFrameworkCore.Repository;

public class CategoryRepository(IDbContextProvider<EntityFrameworkCoreDbContext> dbContextProvider)
    : EfCoreRepository<EntityFrameworkCoreDbContext, Category, Guid>(dbContextProvider), ICategoryRepository
{
    public async Task CheckUniqueNameAsync(Category entity)
    {
        if (entity.UniqueName.IsNullOrWhiteSpace())
        {
            return;
        }

        var queryable = await GetQueryableAsync();
        if (queryable.Any(x => x.Id != entity.Id && x.ParentId == entity.ParentId && x.UniqueName == entity.UniqueName))
        {
            throw new DuplicateCategoryUniqueNameException();
        }
    }
}