using JetKings.Application.DTOs.Request;
using JetKings.Application.DTOs.Response;

namespace JetKings.Application.Interfaces.IServices;

public interface IUserService
{
    Task<ApiResponseDto<UserResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiResponseDto<PagedResponseDto<UserResponseDto>>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ApiResponseDto<UserResponseDto>> CreateAsync(CreateUserRequestDto dto, CancellationToken cancellationToken = default);
    Task<ApiResponseDto<UserResponseDto>> UpdateAsync(Guid id, UpdateUserRequestDto dto, CancellationToken cancellationToken = default);
    Task<ApiResponseDto<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
