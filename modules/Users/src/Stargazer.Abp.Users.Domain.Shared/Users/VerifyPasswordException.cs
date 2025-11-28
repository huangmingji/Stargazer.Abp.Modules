using Volo.Abp;

namespace Stargazer.Abp.Users.Domain.Shared.Users
{
    public class VerifyPasswordException(Guid userId, string password, string passwordHash, string secretKey) : UserFriendlyException(message: "账户密码错误",
        details: $"User ({userId}) password ({password}) error. passwordHash: {passwordHash}, secretKey: {secretKey}.");
}