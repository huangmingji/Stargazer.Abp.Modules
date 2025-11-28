using Volo.Abp;

namespace Stargazer.Abp.Users.Domain.Shared.Users;

public class UserNotAllowLoginException(Guid userId) : UserFriendlyException("User is not allow login",
    "UserNotAllowLogin", $"User({userId}) is not allow login");