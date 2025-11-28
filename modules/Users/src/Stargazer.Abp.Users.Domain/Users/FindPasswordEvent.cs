namespace Stargazer.Abp.Users.Domain.Users;

public class FindPasswordEvent(UserData user, string email)
{
    public UserData User { get; set; } = user;

    public string Email { get; set; } = email;
}