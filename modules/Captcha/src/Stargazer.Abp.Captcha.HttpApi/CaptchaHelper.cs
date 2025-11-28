using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Security.Encryption;

namespace Stargazer.Abp.Captcha.HttpApi;
public class CaptchaHelper() : ApplicationService, ICaptchaHelper
{
    private IDistributedCache<string> Cache => this.LazyServiceProvider.LazyGetRequiredService<IDistributedCache<string>>();

    private const string CaptchaKey = "captcha";
    public async Task<bool> VerifyCaptcha(string key, string code)
    {
        string? captcha = await Cache.GetAsync($"{CaptchaKey}:{key}");
        if (code.IsNullOrWhiteSpace() || captcha.IsNullOrWhiteSpace() || captcha != code)
        {
            await Cache.RemoveAsync($"{CaptchaKey}:{key}");
            return false;
        }
        await Cache.RemoveAsync($"{CaptchaKey}:{key}");
        return true;
    }

    public async Task SetValueAsync(string key, string code)
    {
        string? captcha = await Cache.GetAsync($"{CaptchaKey}:{key}");
        if (!captcha.IsNullOrWhiteSpace())
        {
            await Cache.RemoveAsync($"{CaptchaKey}:{key}");
        }
        await Cache.SetAsync($"{CaptchaKey}:{key}", code, new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        });
    }
}