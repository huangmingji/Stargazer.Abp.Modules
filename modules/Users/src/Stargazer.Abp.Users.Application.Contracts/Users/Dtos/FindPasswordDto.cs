namespace Stargazer.Abp.Users.Application.Contracts;

public class FindPasswordDto
{
    public string Captcha { get; set; }

    public string Email { get; set; } = "";
}