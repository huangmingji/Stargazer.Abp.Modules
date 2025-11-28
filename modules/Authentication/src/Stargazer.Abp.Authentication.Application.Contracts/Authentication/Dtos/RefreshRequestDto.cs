namespace Stargazer.Abp.Authentication.Application.Contracts;

public class RefreshRequestDto
{
    public Guid SessionId { get; set; }
    
    public string RefreshToken { get; set; } = "";
}