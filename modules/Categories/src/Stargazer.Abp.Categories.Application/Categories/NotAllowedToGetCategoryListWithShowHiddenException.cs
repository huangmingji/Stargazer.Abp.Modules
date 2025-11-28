using Volo.Abp;

namespace Stargazer.Abp.Categories.Application.Categories
{
    public class NotAllowedToGetCategoryListWithShowHiddenException()
        : BusinessException(message: $"You have no permission to get category list with hidden categories.");
}