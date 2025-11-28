namespace Stargazer.Abp.Authentication.Application.Contracts;

public class AuthenticationRequestDto
{
    public string Name { get; set; } = "";

    public string Password { get; set; } = "";

    public string Captcha { get; set; } = "";
}