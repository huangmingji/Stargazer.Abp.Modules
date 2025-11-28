namespace Stargazer.Abp.Users.Application.Contracts
{
    public class VerifyPasswordDto
    {
        public string Name { get; set; } = "";
        
        public string Password { get; set; } = "";
    }
}