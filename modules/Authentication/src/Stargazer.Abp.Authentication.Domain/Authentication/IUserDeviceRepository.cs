using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Authentication.Domain.Authentication;

public interface IUserDeviceRepository : IRepository<UserDevice, Guid>
{
    
}