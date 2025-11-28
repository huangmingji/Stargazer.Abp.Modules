using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Authentication.Domain.Authentication;

public interface IUserSessionRepository : IRepository<UserSession, Guid>
{
    
}