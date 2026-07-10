using JetKings.Models.DTOs;

namespace JetKings.IService;

public interface IUserService
{
    Task<ApiResponseDto<UserResponseDto>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ApiResponseDto<PagedResponseDto<UserResponseDto>>> GetAllAsync(int page, int pageSize, CancellationToken ct = default);
    Task<ApiResponseDto<UserResponseDto>> CreateAsync(CreateUserRequestDto dto, CancellationToken ct = default);
    Task<ApiResponseDto<UserResponseDto>> UpdateAsync(Guid id, UpdateUserRequestDto dto, CancellationToken ct = default);
    Task<ApiResponseDto<bool>> DeleteAsync(Guid id, CancellationToken ct = default);
}
