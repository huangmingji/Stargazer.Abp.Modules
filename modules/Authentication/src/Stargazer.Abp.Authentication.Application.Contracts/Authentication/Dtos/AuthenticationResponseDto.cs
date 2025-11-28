namespace Stargazer.Abp.Authentication.Application.Contracts;

public class AuthenticationResponseDto
{
    public Guid SessionId { get; set; }

    public string AccessToken { get; set; } = "";
    
    public string RefreshToken { get; set; } = "";
    
    public long Expires { get; set; }
}