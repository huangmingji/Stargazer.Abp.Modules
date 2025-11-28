namespace Stargazer.Abp.Users.Application.Contracts;

public class UpdatePhoneNumberDto
{

    public string Captcha { get; set; } = "";
    /// <summary>
    /// 手机号码
    /// </summary>
    public string PhoneNumber { get; set; } = "";

}