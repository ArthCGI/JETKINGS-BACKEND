using JetKings.Models.DTOs;

namespace JetKings.IService;

public interface IProductService
{
    Task<ApiResponseDto<ProductResponseDto>> GetByIdAsync(int id, CancellationToken ct = default);

    Task<ApiResponseDto<PagedResponseDto<ProductResponseDto>>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken ct = default);

    Task<ApiResponseDto<ProductResponseDto>> CreateAsync(
        CreateProductRequestDto dto,
        CancellationToken ct = default);

    Task<ApiResponseDto<ProductResponseDto>> UpdateAsync(
        int id,
        UpdateProductRequestDto dto,
        CancellationToken ct = default);

    Task<ApiResponseDto<bool>> DeleteAsync(
        int id,
        CancellationToken ct = default);


    Task<ApiResponseDto<IEnumerable<ProductResponseDto>>> GetByCategoryAsync(
        int categoryId,
        CancellationToken ct = default);

}