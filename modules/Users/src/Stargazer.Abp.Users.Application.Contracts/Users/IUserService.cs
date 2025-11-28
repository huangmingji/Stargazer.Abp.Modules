using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Users.Application.Contracts
{
    public interface IUserService : IApplicationService
    {
        Task<UserDataDto> RegisterAsync(CreateUserDto input, CancellationToken cancellationToken = default);

        Task<UserDataDto> VerifyPasswordAsync(VerifyPasswordDto input, CancellationToken cancellationToken = default);
        
        Task<UserDataDto> CreateAsync(CreateOrUpdateUserWithRolesDto input, CancellationToken cancellationToken = default);
        
        Task<UserDataDto> GetAsync(Guid id, CancellationToken cancellationToken = default);

        
        Task<PagedResultDto<UserDataDto>> GetListAsync(int pageIndex, int pageSize, string? searchText, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<UserDataDto> UpdateAsync(Guid id, CreateOrUpdateUserWithRolesDto input, CancellationToken cancellationToken = default);

        // Task<UserDto> UpdateUserRoleAsync(Guid id, UpdateUserRoleDto input, CancellationToken cancellationToken = default);

    }
}