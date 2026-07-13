using JetKings.IService;
using JetKings.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JetKings.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await _productService.GetAllAsync(page, pageSize, ct);
        return OkResponse(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken ct = default)
    {
        var result = await _productService.GetByIdAsync(id, ct);
        return NotFoundResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductRequestDto dto,
        CancellationToken ct = default)
    {
        var result = await _productService.CreateAsync(dto, ct);
        return CreatedResponse(result, $"/api/products/{result.Data?.Id}");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateProductRequestDto dto,
        CancellationToken ct = default)
    {
        var result = await _productService.UpdateAsync(id, dto, ct);
        return NotFoundResponse(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken ct = default)
    {
        var result = await _productService.DeleteAsync(id, ct);
        return NotFoundResponse(result);
    }


    [HttpGet("category")]
    public async Task<IActionResult> GetByCategory(
        [FromQuery] int? categoryId,
        CancellationToken ct = default)
    {
        var result = await _productService.GetByCategoryAsync(categoryId, ct);
        return OkResponse(result);
    }


}