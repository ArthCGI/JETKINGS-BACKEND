using JetKings.IService;
using JetKings.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JetKings.Controllers;

public class BuyersController : BaseApiController
{
    private readonly IBuyerService _buyerService;

    public BuyersController(IBuyerService buyerService)
    {
        _buyerService = buyerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await _buyerService.GetAllAsync(page, pageSize, ct);
        return OkResponse(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken ct = default)
    {
        var result = await _buyerService.GetByIdAsync(id, ct);
        return NotFoundResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateBuyerRequestDto dto,
        CancellationToken ct = default)
    {
        var result = await _buyerService.CreateAsync(dto, ct);
        return CreatedResponse(result, $"/api/buyers/{result.Data?.Id}");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateBuyerRequestDto dto,
        CancellationToken ct = default)
    {
        var result = await _buyerService.UpdateAsync(id, dto, ct);
        return NotFoundResponse(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken ct = default)
    {
        var result = await _buyerService.DeleteAsync(id, ct);
        return NotFoundResponse(result);
    }
}