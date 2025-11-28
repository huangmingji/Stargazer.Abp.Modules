using Microsoft.AspNetCore.Http;

namespace Stargazer.Abp.Captcha.HttpApi;

public interface ICaptchaHelper
{

    Task SetValueAsync(string key, string code);

    Task<bool> VerifyCaptcha(string key, string code);

}