using Volo.Abp;

namespace Stargazer.Abp.Categories.Domain
{
    public class DuplicateCategoryUniqueNameException() : BusinessException("DuplicateCategoryUniqueName");
}