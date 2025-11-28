namespace Stargazer.Abp.Users.Application.Contracts;

public class UpdateEmailDto
{
    public string Captcha { get; set; } = "";
    /// <summary>
    /// 电子邮件
    /// </summary>
    public string Email { get; set; } = "";
}