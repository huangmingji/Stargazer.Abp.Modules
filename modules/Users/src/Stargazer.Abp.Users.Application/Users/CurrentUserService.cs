using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Abp.Users.Domain.Users;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Stargazer.Abp.Users.Application.Users;

[Authorize]
public class CurrentUserService : ApplicationService, ICurrentUserService
{
    private IUserRepository UserRepository => this.LazyServiceProvider.LazyGetRequiredService<IUserRepository>();
    private EmailService EmailService => this.LazyServiceProvider.LazyGetRequiredService<EmailService>();
    private IConfiguration Configuration => this.LazyServiceProvider.LazyGetRequiredService<IConfiguration>();
    private IDistributedCache<UserDataDto> UserCache => this.LazyServiceProvider.LazyGetRequiredService<IDistributedCache<UserDataDto>>();
    private IDistributedCache<string> Cache => this.LazyServiceProvider.LazyGetRequiredService<IDistributedCache<string>>();
    private ILogger<CurrentUserService> UserServiceLogger => this.LazyServiceProvider.LazyGetRequiredService<ILogger<CurrentUserService>>();
    
    private string CacheKeyPrefix => "User:";
    
    [Authorize]
    public async Task<UserDataDto> GetAsync(CancellationToken cancellationToken = default)
    {
        var userId = CurrentUser.GetId();
        var user = await UserCache.GetAsync(CacheKeyPrefix + userId, token: cancellationToken);
        if (user != null)
        {
            return user;
        }

        var userData = await UserRepository.GetAsync(userId, cancellationToken: cancellationToken);
        user = ObjectMapper.Map<UserData, UserDataDto>(userData);
        await UserCache.SetAsync(CacheKeyPrefix + userId, user, new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        }, token: cancellationToken);
        return user;
    }

    [Authorize]
    public async Task<UserDataDto> UpdateUserNameAsync(UpdateUserNameDto input, CancellationToken cancellationToken = default)
    {
        var userId = CurrentUser.GetId();
        UserData userData = await UserRepository.GetAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        userData.SetNickName(input.Name);
        var result = await UserRepository.UpdateAsync(userData, cancellationToken: cancellationToken);
        return ObjectMapper.Map<UserData, UserDataDto>(result);
    }

    [Authorize]
    public async Task<UserDataDto> UpdatePhoneNumberAsync(UpdatePhoneNumberDto input, CancellationToken cancellationToken = default)
    {
        var userId = CurrentUser.GetId();
        string? captcha = await Cache.GetAsync($"UpdatePhoneNumberCaptcha:{input.PhoneNumber}", token: cancellationToken);
        if (captcha == null || captcha != input.Captcha)
        {
            throw new UserFriendlyException("Captcha error.");
        }
        if (await UserRepository.AnyAsync(x => x.Id != userId && x.PhoneNumber == input.PhoneNumber, cancellationToken: cancellationToken))
        {
            throw new UserFriendlyException("The phone number is already in use.");
        }

        var user = await UserRepository.GetAsync(userId, cancellationToken: cancellationToken);
        user.SetPhoneNumber(input.PhoneNumber);
        user.SetPhoneNumberVerified(true);
        var result = await UserRepository.UpdateAsync(user, cancellationToken: cancellationToken);
        return ObjectMapper.Map<UserData, UserDataDto>(result);
    }

    [Authorize]
    public async Task<UserDataDto> UpdateEmailAsync(UpdateEmailDto email, CancellationToken cancellationToken = default)
    {
        var userId = CurrentUser.GetId();
        string? captcha = await Cache.GetAsync($"UpdateEmailCaptcha:{email.Email}", token: cancellationToken);
        if (captcha == null || captcha != email.Captcha)
        {
            throw new UserFriendlyException("Captcha error.");
        }
        if (await UserRepository.AnyAsync(x => x.Id != userId && x.Email == email.Email, cancellationToken: cancellationToken))
        {
            throw new UserFriendlyException("The email address is already in use.");
        }
        throw new NotImplementedException();
    }

    [Authorize]
    public async Task<UserDataDto> UpdateAvatarAsync(string avatar, CancellationToken cancellationToken = default)
    {
        var userId = CurrentUser.GetId();
        var userData = await UserRepository.GetAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        userData.SetAvatar(avatar);
        var result = await UserRepository.UpdateAsync(userData, cancellationToken: cancellationToken);
        return ObjectMapper.Map<UserData, UserDataDto>(result);
    }

    [Authorize]
    public async Task<UserDataDto> UpdatePasswordAsync(UpdateUserPasswordDto input, CancellationToken cancellationToken = default)
    {
        var userId = CurrentUser.GetId();
        var userData = await UserRepository.GetAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        userData.VerifyPassword(input.OldPassword);
        userData.SetPassword(input.Password);
        var result = await UserRepository.UpdateAsync(userData, cancellationToken: cancellationToken);
        return ObjectMapper.Map<UserData, UserDataDto>(result);
    }
    
    [Authorize]
    public async Task<UserDataDto> UpdatePersonalSettingsAsync(UpdatePersonalSettingsDto input, CancellationToken cancellationToken = default)
    {
        var userId = CurrentUser.GetId();
        UserData userData = await UserRepository.GetAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        if(await UserRepository.AnyAsync(x => x.Id != userId && x.NickName == input.Name, cancellationToken: cancellationToken))
        {
            throw new UserFriendlyException("昵称已存在");
        }

        userData.SetNickName(input.Name);
        userData.SetPersonalProfile(input.PersonalProfile);
        userData.SetAddress(input.Country, input.Province, input.City, input.District, input.Address);
        userData.SetTelephoneNumber(input.TelephoneNumberAreaCode, input.TelephoneNumber);
        var result = await UserRepository.UpdateAsync(userData, cancellationToken: cancellationToken);
        return ObjectMapper.Map<UserData, UserDataDto>(userData);
    }

    [Authorize]
    public async Task<UserDataDto> UpdateAccountAsync(UpdateAccountDto input, CancellationToken cancellationToken = default)
    {
        var userId = CurrentUser.GetId();
        UserData userData = await UserRepository.GetAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        userData.SetAccount(input.Account);
        var result = await UserRepository.UpdateAsync(userData, cancellationToken: cancellationToken);
        return ObjectMapper.Map<UserData, UserDataDto>(result);
    }

    [Authorize]
    public async Task ResetPasswordAsync(ResetPasswordDto input, CancellationToken cancellationToken = default)
    {
        var user = await UserRepository.FindAsync(x => x.Email == input.Email, cancellationToken: cancellationToken);
        if (user == null)
        {
            UserServiceLogger.LogError($"###ResetPasswordAsync###------{input.Email} not found.");
            throw new UserFriendlyException("token已过期。");
        }

        var token = await Cache.GetAsync($"FindPasswordToken:{input.Email}", token: cancellationToken);
        if (token != input.Token)
        {
            UserServiceLogger.LogError($"###ResetPasswordAsync###------{input.Email} verify token error.");
            throw new UserFriendlyException("token已过期。");
        }
        await Cache.RemoveAsync($"FindPasswordToken:{input.Email}", token: cancellationToken);
        user.SetPassword(input.Password);
        await UserRepository.UpdateAsync(user, cancellationToken: cancellationToken);
    }

    [Authorize]
    public async Task FindPasswordAsync(FindPasswordDto input, CancellationToken cancellationToken = default)
    {
        var user = await UserRepository.FindAsync(x => x.Email == input.Email, cancellationToken: cancellationToken);
        if (user == null) return;
        await EmailService.FindPassword(new FindPasswordEvent(user, input.Email));
    }
}