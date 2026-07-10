using AutoMapper;
using JetKings.IService;
using JetKings.Models;
using JetKings.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace JetKings.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResponseDto<UserResponseDto>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user is null)
            return ApiResponseDto<UserResponseDto>.Fail($"User with ID {id} not found.");
        return ApiResponseDto<UserResponseDto>.Ok(_mapper.Map<UserResponseDto>(user));
    }

    public async Task<ApiResponseDto<PagedResponseDto<UserResponseDto>>> GetAllAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var (items, totalCount) = await _userRepository.GetPagedAsync(page, pageSize, u => !u.IsDeleted, ct);
        var result = new PagedResponseDto<UserResponseDto>
        {
            Items = _mapper.Map<IEnumerable<UserResponseDto>>(items),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
        return ApiResponseDto<PagedResponseDto<UserResponseDto>>.Ok(result);
    }

    public async Task<ApiResponseDto<UserResponseDto>> CreateAsync(CreateUserRequestDto dto, CancellationToken ct = default)
    {
        if (await _userRepository.EmailExistsAsync(dto.Email, ct))
            return ApiResponseDto<UserResponseDto>.Fail($"Email '{dto.Email}' is already in use.");

        var user = _mapper.Map<User>(dto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var created = await _userRepository.AddAsync(user, ct);
        _logger.LogInformation("User {UserId} created", created.Id);
        return ApiResponseDto<UserResponseDto>.Ok(_mapper.Map<UserResponseDto>(created), "User created successfully.");
    }

    public async Task<ApiResponseDto<UserResponseDto>> UpdateAsync(Guid id, UpdateUserRequestDto dto, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user is null)
            return ApiResponseDto<UserResponseDto>.Fail($"User with ID {id} not found.");

        _mapper.Map(dto, user);
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user, ct);
        _logger.LogInformation("User {UserId} updated", id);
        return ApiResponseDto<UserResponseDto>.Ok(_mapper.Map<UserResponseDto>(user), "User updated successfully.");
    }

    public async Task<ApiResponseDto<bool>> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user is null)
            return ApiResponseDto<bool>.Fail($"User with ID {id} not found.");

        await _userRepository.DeleteAsync(id, ct);
        _logger.LogInformation("User {UserId} soft-deleted", id);
        return ApiResponseDto<bool>.Ok(true, "User deleted successfully.");
    }
}
