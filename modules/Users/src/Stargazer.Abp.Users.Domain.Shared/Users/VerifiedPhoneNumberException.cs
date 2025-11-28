using Volo.Abp;

namespace Stargazer.Abp.Users.Domain.Shared.Users
{
    public class VerifiedPhoneNumberException(Guid userId, string phoneNumber) : UserFriendlyException(
        message: "电话号码验证失败！", details: $"The phone number {phoneNumber} does not belong to the user {userId}");
}