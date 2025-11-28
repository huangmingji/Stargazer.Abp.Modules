using Volo.Abp;

namespace Stargazer.Abp.Users.Domain.Shared.Users
{
    public class VerifiedEmailException(Guid userId, string email) : UserFriendlyException(message: "电子邮件地址验证失败！",
        details: $"The email {email} does not belong to the user {userId}");
}