using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Users.Application.Contracts;

public interface ICurrentUserService : IApplicationService
{
    
    Task<UserDataDto> GetAsync(CancellationToken cancellationToken = default);
    
    Task<UserDataDto> UpdateUserNameAsync(UpdateUserNameDto input, CancellationToken cancellationToken = default);

    Task<UserDataDto> UpdatePhoneNumberAsync(UpdatePhoneNumberDto input, CancellationToken cancellationToken = default);
        
    Task<UserDataDto> UpdateEmailAsync(UpdateEmailDto email, CancellationToken cancellationToken = default);
    
    Task<UserDataDto> UpdateAvatarAsync(string avatar, CancellationToken cancellationToken = default);
    
    Task<UserDataDto> UpdatePasswordAsync(UpdateUserPasswordDto input, CancellationToken cancellationToken = default);
    
    Task<UserDataDto> UpdatePersonalSettingsAsync(UpdatePersonalSettingsDto input, CancellationToken cancellationToken = default);
    
    Task<UserDataDto> UpdateAccountAsync(UpdateAccountDto input, CancellationToken cancellationToken = default);

    Task ResetPasswordAsync(ResetPasswordDto input, CancellationToken cancellationToken = default);

    Task FindPasswordAsync(FindPasswordDto input, CancellationToken cancellationToken = default);
}