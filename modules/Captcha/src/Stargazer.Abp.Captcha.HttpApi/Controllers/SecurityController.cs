using Hei.Captcha;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Mvc;

namespace Stargazer.Abp.Captcha.HttpApi.Controllers
{
    [Route("security")]
    public class SecurityController(
        ILogger<SecurityController> logger,
        ICaptchaHelper captchaHelper,
        SecurityCodeHelper securityCodeHelper)
        : AbpController
    {
        [HttpGet("captcha/{key}")]
        public async Task<IActionResult> Captcha(string key)
        {
            try
            {
                var code = securityCodeHelper.GetRandomCnText(4);
                var imgbyte = securityCodeHelper.GetGifBubbleCodeByte(code);
                await captchaHelper.SetValueAsync(key, code);
                return File(imgbyte, "image/gif");
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}

