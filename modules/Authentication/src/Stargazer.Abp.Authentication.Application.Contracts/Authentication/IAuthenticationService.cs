using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Authentication.Application.Contracts.Authentication;

public interface IAuthenticationService : IApplicationService   
{
    Task<AuthenticationResponseDto> VerifyPasswordAsync(AuthenticationRequestDto request,
        CancellationToken cancellationToken = default);

    Task<AuthenticationResponseDto> RefreshAsync(RefreshRequestDto request,
        CancellationToken cancellationToken = default);

    Task LogoutAsync(LogoutRequestDto request, CancellationToken cancellationToken = default);
}