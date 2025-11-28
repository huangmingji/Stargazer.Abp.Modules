using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stargazer.Abp.Authentication.Application.Contracts;
using Stargazer.Abp.Authentication.Application.Contracts.Authentication;
using Stargazer.Abp.Authentication.Domain.Authentication;
using Stargazer.Abp.Authentication.Domain.Shared.Authentication;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Common.Extend;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Security.Claims;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Users;

namespace Stargazer.Abp.Authentication.Application.Services;

[Authorize]
public class AuthenticationService : ApplicationService, IAuthenticationService
{
    private IUserService UserService => LazyServiceProvider.LazyGetRequiredService<IUserService>();
    private IUserSessionRepository UserSessionRepository => LazyServiceProvider.LazyGetRequiredService<IUserSessionRepository>();
    private IUserDeviceRepository UserDeviceRepository => LazyServiceProvider.LazyGetRequiredService<IUserDeviceRepository>();
    private TokenGenerator TokenGenerator => LazyServiceProvider.LazyGetRequiredService<TokenGenerator>();
    private IStringEncryptionService StringEncryptionService => LazyServiceProvider.LazyGetRequiredService<IStringEncryptionService>();
    private IHttpContextAccessor HttpContextAccessor => LazyServiceProvider.LazyGetRequiredService<IHttpContextAccessor>();
    private IConfiguration Configuration => LazyServiceProvider.LazyGetRequiredService<IConfiguration>();
    private IDistributedCache<string> Cache => this.LazyServiceProvider.LazyGetRequiredService<IDistributedCache<string>>();
    private const string CaptchaKey = "captcha";
    private long DateTimeToUnixTimestampMillis(DateTime dateTime)
    {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan timeSpan = dateTime.ToUniversalTime() - epoch;
        return (long)timeSpan.TotalMilliseconds;
    }
    
    private string GenerateDeviceId(string userAgent, string ipAddress)
    {
        if (string.IsNullOrWhiteSpace(userAgent) || string.IsNullOrWhiteSpace(ipAddress))
        {
            return "";
        }

        // 使用用户代理和IP地址生成设备ID
        return $"{userAgent}-{ipAddress}";
    }

    private DeviceType DetectDeviceType(string userAgent)
    {
        // 根据用户代理字符串判断设备类型
        // 例如：iPhone、Android、Windows Phone 等
        // 返回 DeviceType.Mobile、DeviceType.Desktop 或 DeviceType.Other
        if (userAgent.Contains("iPhone") || userAgent.Contains("iPad"))
        {
            return DeviceType.iOS;
        }

        if (userAgent.Contains("Android"))
        {
            return DeviceType.Android;
        }

        if (userAgent.Contains("Windows"))
        {
            return DeviceType.Desktop;
        }

        if (userAgent.Contains("Macintosh"))
        {
            return DeviceType.Desktop;
        }

        if (userAgent.Contains("Linux"))
        {
            return DeviceType.Desktop;
        }

        return DeviceType.Other;
    }

    private string GenerateDeviceName(string userAgent)
    {
        if (userAgent.Contains("iPhone") || userAgent.Contains("iPad"))
        {
            return "iPhone";
        }

        if (userAgent.Contains("Android"))
        {
            return "Android";
        }

        if (userAgent.Contains("Windows"))
        {
            return "Windows";
        }

        if (userAgent.Contains("Macintosh"))
        {
            return "MacOS";
        }

        if (userAgent.Contains("Linux"))
        {
            return "Linux";
        }

        return "Unknown";
    }

    private string DetectDeviceOS(string userAgent)
    {
        if (userAgent.Contains("Windows"))
        {
            return "Windows";
        }
        if (userAgent.Contains("Macintosh"))
        {
            return "MacOS";
        }
        if (userAgent.Contains("Linux"))
        {
            return "Linux";
        } 
        return "Unknown";
    }

    [AllowAnonymous]
    public async Task<AuthenticationResponseDto> VerifyPasswordAsync(AuthenticationRequestDto request,
        CancellationToken cancellationToken = default)
    {
        string? captcha = await Cache.GetAsync($"{CaptchaKey}:{request.Name}", token: cancellationToken);
        if (!string.IsNullOrWhiteSpace(captcha))
        {
            if (string.IsNullOrWhiteSpace(request.Captcha) || captcha != request.Captcha)
            {
                throw new CaptchaErrorException();
            }
            await Cache.RemoveAsync($"{CaptchaKey}:{request.Name}", token: cancellationToken);
        }

        var input = new VerifyPasswordDto()
        {
            Name = request.Name,
            Password = request.Password,
        };
        var user = await UserService.VerifyPasswordAsync(input, cancellationToken);
        var refreshTime = DateTime.Now.AddMonths(1);
        var refreshToken = TokenGenerator.GenerateToken(
            user.Id.ToString(),
            refreshTime
        );
        
        string expiresTime = Configuration?.GetSection("Authentication:ExpiresTime").Value ?? "1800";
        var expires = DateTime.Now.AddMinutes(expiresTime.ToInt());
        var accessToken = TokenGenerator.GenerateToken(
            user.Id.ToString(),
            user.UserRoles.FirstOrDefault()?.TenantId,
            user.GetPermissions(),
            expires
        );

        // 获取设备信息（增强版）
        var userAgent = HttpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
        var ipAddress = HttpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        var deviceId = GenerateDeviceId(userAgent, ipAddress);
        var deviceType = DetectDeviceType(userAgent);
        var deviceName = GenerateDeviceName(userAgent);
        var deviceOS = DetectDeviceOS(userAgent);

        // 创建设备记录
        var device = await UserDeviceRepository.FirstOrDefaultAsync(x => x.DeviceId == deviceId && x.UserId == user.Id,
            cancellationToken: cancellationToken);
        if (device == null)
        {
            device = new UserDevice(GuidGenerator.Create(), user.Id, deviceId, deviceName, deviceType, deviceOS,
                "", "", "", ipAddress, "", userAgent, "");
            await UserDeviceRepository.InsertAsync(device, cancellationToken: cancellationToken);
        }

        var userSession = await UserSessionRepository.FirstOrDefaultAsync(x => x.UserId == user.Id && x.DeviceId == device.Id && x.IsActive,
            cancellationToken: cancellationToken);
        if (userSession != null)
        {
            userSession.SetInactive();
            await UserSessionRepository.UpdateAsync(userSession, cancellationToken: cancellationToken);
        }

        userSession = new UserSession(GuidGenerator.Create(), user.Id, device.Id, accessToken, refreshToken,
            DateTimeToUnixTimestampMillis(refreshTime), ipAddress, "", true);
        await UserSessionRepository.InsertAsync(userSession, cancellationToken: cancellationToken);

        return new AuthenticationResponseDto()
        {
            SessionId = userSession.Id,
            RefreshToken = StringEncryptionService.Encrypt(refreshToken)!,
            AccessToken = accessToken,
            Expires = DateTimeToUnixTimestampMillis(expires)
        };
    }

    [AllowAnonymous]
    public async Task<AuthenticationResponseDto> RefreshAsync(RefreshRequestDto request, CancellationToken cancellationToken = default)
    {
        var refreshToken = StringEncryptionService.Decrypt(request.RefreshToken);
        var tokenValidationResult =  await TokenGenerator.ValidateToken(refreshToken!);
        tokenValidationResult.Claims.TryGetValue(AbpClaimTypes.UserId, out var value);
        var userIdStr = value!.ToString();
        if (!Guid.TryParse(userIdStr, out Guid userId))
        {
            throw new AbpAuthorizationException();
        }
        var user = await UserService.GetAsync(userId, cancellationToken);
        var refreshTime = DateTime.Now.AddMonths(1);
        var newRefreshToken = TokenGenerator.GenerateToken(
            user.Id.ToString(),
            refreshTime
        );
        string expiresTime = Configuration?.GetSection("Authentication:ExpiresTime").Value ?? "1800";
        var expires = DateTime.Now.AddMinutes(expiresTime.ToInt());
        var accessToken = TokenGenerator.GenerateToken(
            user.Id.ToString(),
            user.UserRoles.FirstOrDefault()?.TenantId,
            user.GetPermissions(),
            expires
        );
        
        var userAgent = HttpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
        var ipAddress = HttpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        var deviceId = GenerateDeviceId(userAgent, ipAddress);
        Logger.LogInformation($"userAgent--{userAgent}");
        Logger.LogInformation($"ipAddress--{ipAddress}");
        Logger.LogInformation($"deviceId--{deviceId}");
        Logger.LogInformation($"sessionId--{request.SessionId}");
        Logger.LogInformation($"refreshToken--{refreshToken}");
        Logger.LogInformation($"user id--{userId}");
        var device = await UserDeviceRepository.FirstOrDefaultAsync(x => x.DeviceId == deviceId && x.UserId == user.Id, cancellationToken: cancellationToken);
        if (device == null)
        {
            throw new AbpAuthorizationException();
        }
        
        var userSession = await UserSessionRepository.FirstOrDefaultAsync(x => x.Id == request.SessionId && x.DeviceId == device.Id && x.UserId == userId
                                                                               && x.IsActive, cancellationToken: cancellationToken);
        if (userSession == null || userSession.RefreshToken != refreshToken)
        {
            throw new AbpAuthorizationException();
        }
        userSession.SetAccessToken(accessToken);
        userSession.SetRefreshToken(newRefreshToken, DateTimeToUnixTimestampMillis(refreshTime));
        userSession.SetIpAddress(ipAddress);
        await UserSessionRepository.UpdateAsync(userSession, cancellationToken: cancellationToken);

        return new AuthenticationResponseDto()
        {
            SessionId = userSession.Id,
            RefreshToken = StringEncryptionService.Encrypt(newRefreshToken)!,
            AccessToken = accessToken,
            Expires = DateTimeToUnixTimestampMillis(expires)
        };
    }

    [Authorize]
    public async Task LogoutAsync(LogoutRequestDto request, CancellationToken cancellationToken = default)
    {
        var userAgent = HttpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
        var ipAddress = HttpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        var deviceId = GenerateDeviceId(userAgent, ipAddress);
        var device = await UserDeviceRepository.FirstOrDefaultAsync(x => x.DeviceId == deviceId && x.UserId == CurrentUser.GetId(), cancellationToken: cancellationToken);
        if (device == null)
        {
            throw new AbpAuthorizationException();
        }

        var userSession = await UserSessionRepository.FirstOrDefaultAsync(x => x.Id == request.SessionId && x.UserId == CurrentUser.GetId() && x.DeviceId == device.Id, cancellationToken: cancellationToken);
        if (userSession == null)
        {
            throw new AbpAuthorizationException();
        }
        
        userSession.SetInactive();
        await UserSessionRepository.UpdateAsync(userSession, cancellationToken: cancellationToken);
    }
}