using JetKings.Entity;
using JetKings.IService;
using JetKings.Models.DTOs;
using JetKings.Models.DTOs.GenerateBill;
using Microsoft.EntityFrameworkCore;

namespace JetKings.Services;

public class GenerateBillService : IGenerateBillService
{
    private readonly JetKingsDbContext _db;

    public GenerateBillService(JetKingsDbContext db)
    {
        _db = db;
    }

    public async Task<ApiResponseDto<List<BuyerDropdownDto>>> GetBuyersDropdownAsync(CancellationToken ct = default)
    {
        var buyers = await _db.Buyers
            .OrderBy(b => b.PartyName)
            .Select(b => new BuyerDropdownDto
            {
                Id       = b.Id,
                PartyName = b.PartyName,
                Gstin    = b.Gstin,
                City     = b.City,
                State    = b.State
            })
            .ToListAsync(ct);

        return ApiResponseDto<List<BuyerDropdownDto>>.Ok(buyers);
    }

    public async Task<ApiResponseDto<List<GenerateBillCategoryDto>>> GetCategoriesAsync(CancellationToken ct = default)
    {
        var categories = await _db.Categories
            .OrderBy(c => c.Name)
            .Select(c => new GenerateBillCategoryDto
            {
                Id   = c.Id,
                Name = c.Name
            })
            .ToListAsync(ct);

        return ApiResponseDto<List<GenerateBillCategoryDto>>.Ok(categories);
    }

    public async Task<ApiResponseDto<List<GenerateBillProductDto>>> GetProductsForBuyerAsync(
        int buyerId, int? categoryId, CancellationToken ct = default)
    {
        // Verify buyer exists
        var buyerExists = await _db.Buyers.AnyAsync(b => b.Id == buyerId, ct);
        if (!buyerExists)
            return ApiResponseDto<List<GenerateBillProductDto>>.Fail($"Buyer with ID {buyerId} not found.");

        // Load buyer-specific rates as a dictionary for O(1) lookup
        var buyerRates = await _db.BuyerProductPrices
            .Where(bpp => bpp.BuyerId == buyerId)
            .ToDictionaryAsync(bpp => bpp.ProductId, bpp => bpp.Rate, ct);

        var query = _db.Products
            .Include(p => p.Category)
            .AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        var products = await query
            .OrderBy(p => p.Category.Name)
            .ThenBy(p => p.ModelName)
            .ToListAsync(ct);

        var result = products.Select(p =>
        {
            var hasSpecialRate = buyerRates.TryGetValue(p.Id, out var specialRate);
            var effectivePrice = hasSpecialRate ? specialRate : p.DefaultPrice;

            return new GenerateBillProductDto
            {
                Id              = p.Id,
                ModelName       = p.ModelName,
                CategoryId      = p.CategoryId,
                CategoryName    = p.Category.Name,
                DefaultPrice    = p.DefaultPrice,
                EffectivePrice  = effectivePrice,
                HasSpecialPrice = hasSpecialRate && specialRate != p.DefaultPrice
            };
        }).ToList();

        return ApiResponseDto<List<GenerateBillProductDto>>.Ok(result);
    }
}
