namespace Stargazer.Abp.Users.Application.Contracts;

public interface IExternalUserLookupServiceProvider
{
    Task<UserDataDto?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
}