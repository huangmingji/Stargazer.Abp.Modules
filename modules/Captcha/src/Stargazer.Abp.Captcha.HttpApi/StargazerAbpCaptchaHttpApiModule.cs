using System.Text;
using Hei.Captcha;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp.Security;
using Volo.Abp.Security.Encryption;
using Stargazer.Common.Extend;

namespace Stargazer.Abp.Captcha.HttpApi;

[DependsOn(
    typeof(AbpSecurityModule),
    typeof(AbpAspNetCoreMvcModule)
)]
public class StargazerAbpCaptchaHttpApiModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHeiCaptcha();
        context.Services.AddTransient<ICaptchaHelper, CaptchaHelper>();
    }

}
