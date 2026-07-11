using JetKings.Models.DTOs;

namespace JetKings.IService;

public interface IBuyerService
{
    Task<ApiResponseDto<BuyerResponseDto>> GetByIdAsync(int id, CancellationToken ct = default);

    Task<ApiResponseDto<PagedResponseDto<BuyerResponseDto>>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken ct = default);

    Task<ApiResponseDto<BuyerResponseDto>> CreateAsync(
        CreateBuyerRequestDto dto,
        CancellationToken ct = default);

    Task<ApiResponseDto<BuyerResponseDto>> UpdateAsync(
        int id,
        UpdateBuyerRequestDto dto,
        CancellationToken ct = default);

    Task<ApiResponseDto<bool>> DeleteAsync(
        int id,
        CancellationToken ct = default);
}