using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Users.Domain.Users
{
    public interface IUserRepository : IRepository<UserData, Guid>
    {
        
    }
}