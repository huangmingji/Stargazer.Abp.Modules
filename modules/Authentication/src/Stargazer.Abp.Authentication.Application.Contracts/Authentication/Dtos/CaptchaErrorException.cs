using Volo.Abp;

namespace Stargazer.Abp.Authentication.Application.Contracts;

public class CaptchaErrorException() : UserFriendlyException(message: "Captcha Error！", code:"CaptchaError",
    details: $"The captcha is incorrect. Please check the captcha and try again.");