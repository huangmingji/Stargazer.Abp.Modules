using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Abp.Users.Domain.Users;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Users.Application.Users;

[Authorize]
public class UserService : ApplicationService, IUserService
{
    private IUserRepository UserRepository => this.LazyServiceProvider.LazyGetRequiredService<IUserRepository>();
    private EmailService EmailService => this.LazyServiceProvider.LazyGetRequiredService<EmailService>();
    private IConfiguration Configuration => this.LazyServiceProvider.LazyGetRequiredService<IConfiguration>();
    private IDistributedCache<UserDataDto> Cache => this.LazyServiceProvider.LazyGetRequiredService<IDistributedCache<UserDataDto>>();
    private ILogger<UserService> UserServiceLogger => this.LazyServiceProvider.LazyGetRequiredService<ILogger<UserService>>();
    
    private string CacheKeyPrefix => "User:";

    public async Task<UserDataDto> RegisterAsync(CreateUserDto input, CancellationToken cancellationToken = default)
    {
        if (await UserRepository.AnyAsync(x => x.Email == input.Email, cancellationToken: cancellationToken))
        {
            throw new UserFriendlyException("电子邮箱地址已注册");
        }

        var userData = new UserData(GuidGenerator.Create(), input.Email, input.UserName);
        userData.SetPassword(input.Password);
        userData.SetEmail(input.Email);
        await UserRepository.InsertAsync(userData, cancellationToken: cancellationToken);
        return ObjectMapper.Map<UserData, UserDataDto>(userData);
    }

    [Authorize(policy: UsersPermissions.Users.Create)]
    public async Task<UserDataDto> CreateAsync(CreateOrUpdateUserWithRolesDto input, CancellationToken cancellationToken = default)
    {
        var userData = new UserData(id: GuidGenerator.Create(),
            account: input.Account,
            nickName: input.UserName);

        if (!string.IsNullOrWhiteSpace(input.Password))
        {
            userData.SetPassword(input.Password);
        }

        userData.SetEmail(input.Email);
        userData.SetPhoneNumber(input.PhoneNumber);

        userData.AllowUser(input.AllowStartTime, input.AllowEndTime);
        userData.LockUser(input.LockStartTime, input.LockEndDate);

        input.RoleIds.ForEach(roleId => { userData.AddRole(GuidGenerator.Create(), roleId); });
        await UserRepository.InsertAsync(userData, cancellationToken: cancellationToken);
        return ObjectMapper.Map<UserData, UserDataDto>(userData);
    }

    [Authorize(policy: UsersPermissions.Users.Manage)]
    public async Task<UserDataDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await Cache.GetAsync(CacheKeyPrefix + id, token: cancellationToken);
        if (user != null)
        {
            return user;
        }

        var userData = await UserRepository.GetAsync(id, cancellationToken: cancellationToken);
        user = ObjectMapper.Map<UserData, UserDataDto>(userData);
        await Cache.SetAsync(CacheKeyPrefix + id, user, new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        } ,token: cancellationToken);
        return user;
    }
    
    [Authorize(policy: UsersPermissions.Users.Manage)]
    public async Task<PagedResultDto<UserDataDto>> GetListAsync(int pageIndex, int pageSize, string? searchText = null, CancellationToken cancellationToken = default)
    {
        var queryable = await UserRepository.GetQueryableAsync();
        queryable = queryable.WhereIf(!string.IsNullOrWhiteSpace(searchText), x => x.NickName.Contains(searchText!))
            .WhereIf(!string.IsNullOrWhiteSpace(searchText), x => x.Account.Contains(searchText!))
            .WhereIf(!string.IsNullOrWhiteSpace(searchText), x => x.Email.Contains(searchText!))
            .WhereIf(!string.IsNullOrWhiteSpace(searchText), x => x.PhoneNumber.Contains(searchText!));
        int total = queryable.Count();
        var data = await queryable.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToDynamicListAsync<UserData>(cancellationToken);
        return new PagedResultDto<UserDataDto>()
        {
            TotalCount = total,
            Items = ObjectMapper.Map<List<UserData>, List<UserDataDto>>(data)
        };
    }

    [Authorize(policy: UsersPermissions.Users.Delete)]
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await UserRepository.DeleteAsync(id, cancellationToken: cancellationToken);
        await Cache.RemoveAsync(CacheKeyPrefix + id, token: cancellationToken);
    }

    public async Task<UserDataDto> VerifyPasswordAsync(VerifyPasswordDto input, CancellationToken cancellationToken = default)
    {
        UserData? userData = await UserRepository.FindAsync(x =>
            x.Account == input.Name
            || x.PhoneNumber == input.Name
            || x.Email == input.Name, cancellationToken: cancellationToken);
        if (userData == null)
        {
            throw new UserFriendlyException("账户密码错误");
        }

        if (input.Name == userData.Email && !userData.EmailVerified)
        {
            // await EmailService.EmailChanged(new EmailChangedEvent(userData, input.Name));
            throw new UserFriendlyException("电子邮箱地址未通过验证，请查看邮箱进行验证");
        }

        userData.VerifyPassword(input.Password);
        userData.CheckAllowTime();
        userData.CheckLockTime();
        return ObjectMapper.Map<UserData, UserDataDto>(userData);
    }

    [Authorize(policy: UsersPermissions.Users.Update)]
    public async Task<UserDataDto> UpdateAsync(Guid id, CreateOrUpdateUserWithRolesDto input,
        CancellationToken cancellationToken = default)
    {
        UserData userData = await UserRepository.GetAsync(x => x.Id == id, cancellationToken: cancellationToken);

        if (await UserRepository.AnyAsync(x => x.Id != id && x.Account == input.Account,
                cancellationToken: cancellationToken))
        {
            throw new UserFriendlyException("该账号已注册");
        }

        if (!string.IsNullOrWhiteSpace(input.Email) &&
            await UserRepository.AnyAsync(x => x.Id != id && x.Email == input.Email,
                cancellationToken: cancellationToken))
        {
            throw new UserFriendlyException("电子邮件地址已注册");
        }

        if (!string.IsNullOrWhiteSpace(input.PhoneNumber) &&
            await UserRepository.AnyAsync(x => x.Id != id && x.PhoneNumber == input.PhoneNumber,
                cancellationToken: cancellationToken))
        {
            throw new UserFriendlyException("手机号码已注册");
        }

        if (input.RoleIds is {Count: 0})
        {
            throw new UserFriendlyException("请选择用户角色");
        }
        Dictionary<Guid, Guid> roles = new Dictionary<Guid, Guid>();
        input.RoleIds.ForEach(item => { roles.Add(GuidGenerator.Create(), item); });
        userData.SetRoles(roles);

        if (!string.IsNullOrWhiteSpace(input.Password))
        {
            userData.SetPassword(input.Password);
        }

        userData.SetNickName(input.UserName);
        userData.SetAccount(input.Account);
        userData.SetEmail(input.Email);
        userData.SetPhoneNumber(input.PhoneNumber);
        var result = await UserRepository.UpdateAsync(userData, cancellationToken: cancellationToken);
        var user = ObjectMapper.Map<UserData, UserDataDto>(result);
        await Cache.RemoveAsync(CacheKeyPrefix + id, token: cancellationToken);
        return ObjectMapper.Map<UserData, UserDataDto>(userData);
    }
}