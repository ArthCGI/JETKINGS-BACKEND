using JetKings.Models.DTOs;

namespace JetKings.IService;

public interface IBuyerProductPriceService
{
    Task<ApiResponseDto<BuyerProductPriceResponseDto>> GetByIdAsync(int id, CancellationToken ct = default);

    Task<ApiResponseDto<PagedResponseDto<BuyerProductPriceResponseDto>>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken ct = default);

    Task<ApiResponseDto<BuyerProductPriceResponseDto>> CreateAsync(
        CreateBuyerProductPriceRequestDto dto,
        CancellationToken ct = default);

    Task<ApiResponseDto<BuyerProductPriceResponseDto>> UpdateAsync(
        int id,
        UpdateBuyerProductPriceRequestDto dto,
        CancellationToken ct = default);

    Task<ApiResponseDto<bool>> DeleteAsync(
        int id,
        CancellationToken ct = default);
}