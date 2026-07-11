using JetKings.IService;
using JetKings.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JetKings.Controllers;

public class BuyerProductPricesController : BaseApiController
{
    private readonly IBuyerProductPriceService _service;

    public BuyerProductPricesController(
        IBuyerProductPriceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await _service.GetAllAsync(page, pageSize, ct);
        return OkResponse(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken ct = default)
    {
        var result = await _service.GetByIdAsync(id, ct);
        return NotFoundResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateBuyerProductPriceRequestDto dto,
        CancellationToken ct = default)
    {
        var result = await _service.CreateAsync(dto, ct);

        return CreatedResponse(
            result,
            $"/api/buyerproductprices/{result.Data?.Id}");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateBuyerProductPriceRequestDto dto,
        CancellationToken ct = default)
    {
        var result = await _service.UpdateAsync(id, dto, ct);
        return NotFoundResponse(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken ct = default)
    {
        var result = await _service.DeleteAsync(id, ct);
        return NotFoundResponse(result);
    }
}