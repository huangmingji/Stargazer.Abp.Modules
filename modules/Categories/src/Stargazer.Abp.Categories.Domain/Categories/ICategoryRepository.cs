using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Categories.Domain.Categories;

public interface ICategoryRepository : IRepository<Category, Guid>
{
    Task CheckUniqueNameAsync(Category entity);
}