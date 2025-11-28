using Microsoft.Extensions.Caching.Distributed;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Abp.Users.Domain.Users;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;

namespace Stargazer.Abp.Users.Application.Users;

[RemoteService(IsEnabled = false)]
public class ExternalUserLookupServiceProvider : ApplicationService, IExternalUserLookupServiceProvider
{
    private IUserRepository UserRepository => this.LazyServiceProvider.LazyGetRequiredService<IUserRepository>();
    private IDistributedCache<UserDataDto> Cache => this.LazyServiceProvider.LazyGetRequiredService<IDistributedCache<UserDataDto>>();
    
    private string CacheKeyPrefix => "User:";
    public async Task<UserDataDto?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await Cache.GetAsync(CacheKeyPrefix + id, token: cancellationToken);
        if (user != null)
        {
            return user;
        }
        var userData = await UserRepository.FindAsync(id, cancellationToken: cancellationToken);
        if (userData == null) return null;
        user = ObjectMapper.Map<UserData, UserDataDto>(userData);
        await Cache.SetAsync(CacheKeyPrefix + id, user, new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        }, token: cancellationToken);
        return user;
    }

}