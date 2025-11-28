namespace Stargazer.Abp.Users.Application.Contracts;

public class VerifyEmailDto
{
    public string Email { get; set; } = "";

    public string Token { get; set; } = "";
}