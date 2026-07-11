using JetKings.IService;
using Microsoft.AspNetCore.Mvc;

namespace JetKings.Controllers;

public class GenerateBillController : BaseApiController
{
    private readonly IGenerateBillService _generateBillService;

    public GenerateBillController(IGenerateBillService generateBillService)
    {
        _generateBillService = generateBillService;
    }

    /// <summary>
    /// Returns all buyers for the Select Buyer dropdown (Step 1).
    /// GET /api/GenerateBill/buyers
    /// </summary>
    [HttpGet("buyers")]
    public async Task<IActionResult> GetBuyers(CancellationToken ct = default)
    {
        var result = await _generateBillService.GetBuyersDropdownAsync(ct);
        return OkResponse(result);
    }

    /// <summary>
    /// Returns all product categories for the filter buttons (Step 2).
    /// GET /api/GenerateBill/categories
    /// </summary>
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories(CancellationToken ct = default)
    {
        var result = await _generateBillService.GetCategoriesAsync(ct);
        return OkResponse(result);
    }

    /// <summary>
    /// Returns products with buyer-specific pricing for the Select Products list (Step 3).
    /// GET /api/GenerateBill/products?buyerId=1
    /// GET /api/GenerateBill/products?buyerId=1&amp;categoryId=2
    /// </summary>
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int buyerId,
        [FromQuery] int? categoryId = null,
        CancellationToken ct = default)
    {
        if (buyerId <= 0)
            return BadRequest(new { success = false, message = "buyerId is required." });

        var result = await _generateBillService.GetProductsForBuyerAsync(buyerId, categoryId, ct);
        return NotFoundResponse(result);
    }
}
