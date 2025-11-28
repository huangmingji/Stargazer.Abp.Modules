namespace Stargazer.Abp.Users.Application.Contracts
{
    public class UpdateUserPasswordDto : UpdatePasswordDto
    {
        public string OldPassword { get; set; } = "";
        
    }
}