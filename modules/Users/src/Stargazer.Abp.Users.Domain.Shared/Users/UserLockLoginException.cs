using Volo.Abp;

namespace Stargazer.Abp.Users.Domain.Shared.Users;

public class UserLockLoginException(Guid userId)
    : UserFriendlyException("User is locked", "UserLocked", $"User({userId}) is locked");