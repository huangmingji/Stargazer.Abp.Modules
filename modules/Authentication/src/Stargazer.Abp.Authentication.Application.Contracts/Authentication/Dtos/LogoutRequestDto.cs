namespace Stargazer.Abp.Authentication.Application.Contracts;

public class LogoutRequestDto
{
    public Guid SessionId { get; set; }
}