using JetKings.Models.DTOs;
using JetKings.Models.DTOs.GenerateBill;

namespace JetKings.IService;

public interface IGenerateBillService
{
    /// <summary>Returns all active buyers for the Select Buyer dropdown.</summary>
    Task<ApiResponseDto<List<BuyerDropdownDto>>> GetBuyersDropdownAsync(CancellationToken ct = default);

    /// <summary>Returns all product categories for the category filter buttons.</summary>
    Task<ApiResponseDto<List<GenerateBillCategoryDto>>> GetCategoriesAsync(CancellationToken ct = default);

    /// <summary>
    /// Returns products with buyer-specific pricing applied.
    /// If buyerId has a custom rate for a product it is used; otherwise the product's default price is used.
    /// Optionally filtered by categoryId.
    /// </summary>
    Task<ApiResponseDto<List<GenerateBillProductDto>>> GetProductsForBuyerAsync(int buyerId, int? categoryId, CancellationToken ct = default);
}
